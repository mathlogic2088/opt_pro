using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Tool;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace VisionAndMotionPro
{
    //此类用于外部程序调用,VM是VisionAndMotionPro的简称
    public static class VM
    {

        /// <summary>
        /// 初始化框架，一般在程序启动时调用一次
        /// </summary>
        /// <param name="showStartWindow">指示是否显示程序启动时的欢迎窗体</param>
        public static void Init(bool showStartWindow = true)
        {
            try
            {
                Frm_Welcome.Instance.Show();
                System.Windows.Forms.Application.DoEvents();
                var f = Frm_Main.Instance;          //这一句看似没用，实则有用，因为在这里加这一行就会实例化主窗体，不然主窗体就会在后台线程中被实例化，那就会有问题
                Thread th = new Thread(Machine.Init);
                th.IsBackground = true;
                th.Start();
                while (Machine.loading == true)
                {
                    Thread.Sleep(1);
                    System.Windows.Forms.Application.DoEvents();
                }

                f.ShowDialog();
            }
            catch (Exception ex)
            {
                try
                {
                    Frm_Welcome.Instance.ShowDialog();
                }
                catch (System.Exception es)
                {
                    if (ex.Message.Contains("DevComponents.DotNetBar2"))
                    {
                        DialogResult result = System.Windows.Forms.MessageBox.Show(Configuration.language == Language.English ? "An image with the same name already exists, is it overwritten?" : "启动失败，本机未安装此程序所依赖的组件，是否立即安装？", Configuration.language == Language.English ? "Tip:" : "提示：", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            Process.Start(System.Windows.Forms.Application.StartupPath + "\\DotNetBarSetupTrial_140015.msi");
                        }
                    }
                    else
                    {
                        LogHelper.SaveErrorInfo(es);
                    }
                }
            }
        }
        /// <summary>
        /// 显示编辑窗口
        /// </summary>
        public static void ShowWindow()
        {
            try
            {
                Frm_Main.Instance.ShowDialog();
            }
            catch { }
        }
        /// <summary>
        /// 通过流程名获取流程
        /// </summary>
        /// <param name="jobName">流程名</param>
        public static Job GetJob(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (((Job)Project.Instance.L_jobList[i]).jobName == jobName)
                    {
                        return (Job)Project.Instance.L_jobList[i];
                    }
                }
                Frm_MessageBox.Instance.MessageBoxShow("未找到名为" + jobName + "的流程（错误代码：0001）");
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }
        /// <summary>
        /// 从本地读取作业
        /// </summary>
        /// <param name="path">流程文件路径</param>
        public static Job ReadJob(string path)
        {
            try
            {
                object obj = JsonConvert.DeserializeObject<List<ToolInfo>>(File.ReadAllText(path));
                int index = path.LastIndexOf(@"\");
                string pathWithoutName = path.Substring(0, index + 1);

                Job job = new Job();
                job.jobName = Path.GetFileNameWithoutExtension(path);
                job.L_toolList = JsonConvert.DeserializeObject<List<ToolInfo>>(File.ReadAllText(path));

                //反序列化各工具
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(pathWithoutName + job.L_toolList[i].toolName + ".Tool", FileMode.Open, FileAccess.Read, FileShare.None);
                    job.L_toolList[i].tool = formatter.Deserialize(stream);
                    stream.Close();
                }
                return job;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }
        /// <summary>
        /// 从本地加载作业到程序中
        /// </summary>
        /// <param name="path">流程文件路径</param>
        public static Job LoadJob(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Frm_MessageBox.Instance.MessageBoxShow("\r\n流程文件不存在");
                    return null;
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                Job job = (Job)formatter.Deserialize(stream);
                stream.Close();

                Project.Instance.L_jobList.Add(job);
                TreeView tvw_job = new TreeView();
                tvw_job.ItemHeight = 26;
                tvw_job.ShowLines = false;
                tvw_job.AllowDrop = true;
                tvw_job.ImageList = Job.imageList;

                tvw_job.AfterSelect += job.tvw_job_AfterSelect;
                tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
                tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
                tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);

                //以下事件为画线事件
                tvw_job.MouseMove += job.DrawLineWithoutRefresh;
                tvw_job.AfterExpand += job.Draw_Line;
                tvw_job.AfterCollapse += job.Draw_Line;
                Frm_Job.Instance.tbc_jobs.SelectedIndexChanged += job.tbc_jobs_SelectedIndexChanged;


                Frm_Job.Instance.tbc_jobs.TabPages.Add(job.jobName);
                Frm_Job.Instance.tbc_jobs.TabPages[Frm_Job.Instance.tbc_jobs.TabPages.Count - 1].Controls.Add(tvw_job);
                tvw_job.Dock = DockStyle.Fill;
                tvw_job.ShowNodeToolTips = true;
                tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                //反序列化各工具
                job.D_itemAndSource.Clear();
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    TreeNode node = Project.GetJobTree(job.jobName).Nodes.Add(job.L_toolList[i].toolName);
                    for (int j = 0; j < job.L_toolList[i].input.Count ; j++)
                    {
                        TreeNode treeNode;
                        //因为OutputBox只有源，所以此处特殊处理
                        if (job.L_toolList[i].toolType != ToolType.Output)
                            treeNode = node.Nodes.Add(job .L_toolList[i].input [j].IOName  + job.L_toolList[i].input[j].value);
                        else
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName);

                        //if (job.L_toolList[i].input[j].ioType ==DataType.Image )       //图像变量类型
                        //    treeNode.Tag = DataType.Image;
                        //else if (job.L_toolList[i].input[j].ioType==DataType .Region )       //区域变量类型
                        //    treeNode.Tag = "Region";
                        //else
                        treeNode.Tag = job.L_toolList[i].input[j].ioType;       //字符串变量类型
                        treeNode.ForeColor = Color.DarkMagenta;

                        //解析需要连线的节点对

                        if (treeNode.ToString().Contains("《-"))
                        {
                            string toolNodeText = Regex.Split(job.L_toolList[i].input[j].value .ToString(), " . ")[0].Substring(3);
                            string toolIONodeText = "-->" + Regex.Split(job.L_toolList[i].input[j].value .ToString(), " . ")[1];
                            job.D_itemAndSource.Add(treeNode, job.GetToolIONodeByNodeText(toolNodeText, toolIONodeText));
                        }
                        if (job.L_toolList[i].toolType == ToolType.Output)
                        {
                            string toolNodeText = Regex.Split(treeNode.Text, " . ")[0].Substring(3);
                            string toolIONodeText = Regex.Split(treeNode.Text, " . ")[1];
                            job.D_itemAndSource.Add(treeNode, job.GetToolIONodeByNodeText(toolNodeText, toolIONodeText));
                        }
                    }
                   // string[] outputKeys = job.L_toolList[i].output.Keys.ToArray();
                    for (int k = 0; k < job.L_toolList[i].output.Count; k++)
                    {
                        TreeNode treeNode = node.Nodes.Add("-->" +job .L_toolList[i].output [k].IOName );
                        //////if (job.L_toolList[i].output[k].IOName)
                        //////    treeNode.Tag = DataType.Image;
                        //////else if (outputKeys[k].Contains("Region"))
                        //////    treeNode.Tag = "Region";
                        //////else
                        treeNode.Tag = job.L_toolList[i].output[k].ioType ;
                        treeNode.ForeColor = Color.Blue;
                    }
                }

                //更新工具树图标
                for (int j = 0; j < Project.GetJobTree(job.jobName).Nodes.Count; j++)
                {
                    switch (Job.GetToolInfoByToolName(job.jobName, Project.GetJobTree(job.jobName).Nodes[j].Text).toolType)
                    {
                        case ToolType.HalconInterface:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                            break;

                        case ToolType.ShapeMatch:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 2;
                            break;

                        case ToolType.EyeHandCalibration:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 3;
                            break;

                        case ToolType.CoorTrans:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 3;
                            break;

                        case ToolType.SubImage:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 5;
                            break;

                        case ToolType.BlobAnalyse:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 6;
                            break;

                        case ToolType.DownCamAlign:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 7;
                            break;

                        case ToolType.FindLine:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 9;
                            break;

                        case ToolType.FindCircle:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 10;
                            break;

                        case ToolType.DistancePL:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 17;
                            break;

                        case ToolType.DistanceSS:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 8;
                            break;

                        case ToolType.Barcode:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 25;
                            break;

                        case ToolType.Logic:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 7;
                            break;

                        case ToolType.CodeEdit:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 11;
                            break;

                        case ToolType.Label:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 13;
                            break;

                        case ToolType.Output:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 13;
                            break;

                    }
                    for (int k = 0; k < Project.GetJobTree(job.jobName).Nodes[j].Nodes.Count; k++)
                    {
                        Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].SelectedImageIndex = 26;
                    }
                    Frm_ImageWindow.Instance.Update_Last_Run_Result_Image_List();
                }
                return job;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }

    }
}
