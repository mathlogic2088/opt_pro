using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Windows.Resources;
using VisionAndMotionPro;
using VisionAndMotionPro.Properties;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.IO;
using Tool;
using System.Runtime.Serialization.Formatters.Binary;
using ViewROI;
using ViewWindow.Model;
using VisionAndMotionPro._1_ToolLib._25_ConditionTool;

namespace VisionAndMotionPro
{
    [Serializable]
    public class Job
    {
        public Job()
        {
            if (rightClickMenuAtBlank.Items.Count == 0)
            {
                ToolStripItem toolStripItem_展开流程树 = rightClickMenuAtBlank.Items.Add(Configuration.language == Language.English ? "Expand Job Tree" : "展开流程树");
                toolStripItem_展开流程树.Click += toolStripItem_展开流程树_Click;
                ToolStripItem toolStripItem_折叠流程树 = rightClickMenuAtBlank.Items.Add(Configuration.language == Language.English ? "Fold Job Tree" : "折叠流程树");
                toolStripItem_折叠流程树.Click += toolStripItem_折叠流程树_Click;
                ToolStripItem toolStripItem_删除当前流程 = rightClickMenuAtBlank.Items.Add(Configuration.language == Language.English ? "Delete Job" : "删除当前流程");
                toolStripItem_删除当前流程.Click += toolStripItem_删除当前流程_Click;
                ToolStripItem toolStripItem_流程属性 = rightClickMenuAtBlank.Items.Add(Configuration.language == Language.English ? "Job Info" : "流程属性");
                toolStripItem_流程属性.Click += toolStripItem_流程属性_Click;
            }
        }

        /// <summary>
        /// 当前流程树是否处于折叠状态
        /// </summary>
        private static bool jobTreeFold = true;
        /// <summary>
        /// 当前流程此次运行结果
        /// </summary>
        private JobRunStatu jobRunStatu = JobRunStatu.Fail;
        /// <summary>
        /// 工具输入项个数
        /// </summary>
        private int inputItemNum = 0;
        /// <summary>
        /// 工具输出项个数
        /// </summary>
        private int outputItemNum = 0;
        /// <summary>
        /// 指示图像窗口是否为第一次显示窗体，第一次显示时要初始化
        /// </summary>
        internal bool firstDisplayImage = true;
        /// <summary>
        /// 需要连线的节点对，不停的画连线，注意键值对中第一个为连线的结束节点，第二个为起始节点，一个输出可能连接多个输入，而键值对中的键不能重复，所以把源作为值，输入作为键
        /// </summary>
        internal Dictionary<TreeNode, TreeNode> D_itemAndSource = new Dictionary<TreeNode, TreeNode>();
        /// <summary>
        /// 本流程所绑定的图像窗口的句柄
        /// </summary>
        internal HTuple imageWindow = new HTuple();
        /// <summary>
        /// 本流程所绑定的生产窗口的名称
        /// </summary>
        internal string imageWindowName = "无";
        /// <summary>
        /// 流程结果图像所绑定的窗体
        /// </summary>
        internal string debugImageWindow = Configuration.language == Language.English ? "Image" : "图像";
        /// <summary>
        /// 流程运行结果图像
        /// </summary>
        internal HObject jobResultImage;
        /// <summary>
        /// 编辑节点前节点文本，用于修改工具名称
        /// </summary>
        private string nodeTextBeforeEdit = string.Empty;
        /// <summary>
        /// 流程编辑时的右击菜单
        /// </summary>
        private static ContextMenuStrip rightClickMenu = new ContextMenuStrip();
        /// <summary>
        /// 在空白除右击菜单
        /// </summary>
        private static ContextMenuStrip rightClickMenuAtBlank = new ContextMenuStrip();
        /// <summary>
        /// 流程名
        /// </summary>
        internal string jobName = string.Empty;
        /// <summary>
        /// 流程树中节点的最大长度
        /// </summary>
        private int maxLength = 130;
        /// <summary>
        /// 工具对象集合
        /// </summary>
        public List<ToolInfo> L_toolList = new List<ToolInfo>();
        /// <summary>
        /// 正在绘制输入输出指向线
        /// </summary>
        internal static bool isDrawing = false;
        /// <summary>
        /// 记录本工具执行完的耗时，用于计算各工具耗时
        /// </summary>
        private double recordElapseTime = 0;
        /// <summary>
        /// 标准图像字典，用于存储标准图像路径和图像对象
        /// </summary>
        internal static Dictionary<string, HObject> D_standardImage = new Dictionary<string, HObject>();
        /// <summary>
        /// 工具图标列表
        /// </summary>
        internal static ImageList imageList = new ImageList();
        /// <summary>
        /// 记录起始节点和此节点的列坐标值
        /// </summary>
        private static Dictionary<TreeNode, Color> startNodeAndColor = new Dictionary<TreeNode, Color>();
        /// <summary>
        /// 记录前面的划线所跨越的列段，
        /// </summary>
        private static Dictionary<int, Dictionary<TreeNode, TreeNode>> list = new Dictionary<int, Dictionary<TreeNode, TreeNode>>();
        /// <summary>
        /// 每一个列坐标值对应一种颜色
        /// </summary>
        private Dictionary<int, Color> colValueAndColor = new Dictionary<int, Color>();
        /// <summary>
        /// 输入输出指向线的颜色数组
        /// </summary>
        private static Color[] color = new Color[] { Color.Blue, Color.Orange, Color.Black, Color.Red, Color.Green, Color.Brown, Color.Blue, Color.Black, Color.Red, Color.Green, Color.Orange, Color.Brown, Color.Blue, Color.Black, Color.Red, Color.Green, Color.Orange, Color.Brown, Color.Blue, Color.Black, Color.Red, Color.Green, Color.Orange, Color.Brown, Color.Blue, Color.Black, Color.Red, Color.Green, Color.Orange, Color.Brown };


        #region 绘制输入输出指向线
        internal void tvw_job_AfterSelect(object sender, TreeViewEventArgs e)
        {
            nodeTextBeforeEdit = Project.GetJobTree(jobName).SelectedNode.Text;
        }
        internal void Draw_Line(object sender, TreeViewEventArgs e)
        {
            Project.GetJobTree(jobName).Refresh();
            DrawLine();
        }
        internal void tbc_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Project.GetJobTree(jobName).Refresh();
            DrawLine();
        }
        public void DrawLineWithoutRefresh(object sender, MouseEventArgs e)
        {
            Project.GetJobTree(jobName).Update();
            DrawLine();
        }
        #endregion
        void toolStripItem_折叠流程树_Click(object sender, EventArgs e)
        {
            try
            {
                if (Frm_Job.Instance.tbc_jobs.TabCount < 1)
                    return;
                string jobName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
                Job job = Job.GetJobByName(jobName);
                Project.GetJobTree(jobName).CollapseAll();
                job.DrawLine();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        void toolStripItem_展开流程树_Click(object sender, EventArgs e)
        {
            try
            {
                if (Frm_Job.Instance.tbc_jobs.TabCount < 1)
                    return;
                string jobName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
                Job job = Job.GetJobByName(jobName);
                Project.GetJobTree(jobName).ExpandAll();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        void toolStripItem_删除当前流程_Click(object sender, EventArgs e)
        {
            Frm_Job.Instance.pic_deleteJob_Click(null, null);
        }
        void toolStripItem_流程属性_Click(object sender, EventArgs e)
        {
            Frm_Job.Instance.pic_jobInfo_Click(null, null);
        }
        /// <summary>
        /// 拖动工具节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tvw_job_ItemDrag(object sender, ItemDragEventArgs e)//左键拖动  
        {
            try
            {
                if (((TreeView)sender).SelectedNode != null)
                {
                    if (((TreeView)sender).SelectedNode.Level == 1)          //输入输出不允许拖动
                    {
                        Project.GetJobTree(jobName).DoDragDrop(e.Item, DragDropEffects.Move);
                    }

                    else if (e.Button == MouseButtons.Left)
                    {
                        Project.GetJobTree(jobName).DoDragDrop(e.Item, DragDropEffects.Move);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 克隆当前流程
        /// </summary>
        internal static void CloneCurJob()
        {
            try
            {
            Again:
                Frm_InputMessage.Instance.lal_title.Text = (Configuration.language == Language.English ? "Please input job's name" : "请输入新流程名");
                Frm_InputMessage.Instance.btn_confirm.Text = (Configuration.language == Language.English ? "Confirm" : "确定");
                Frm_InputMessage.Instance.passwordChar = false;
                Frm_InputMessage.Instance.txt_input.Clear();
                Frm_InputMessage.Instance.ShowDialog();
                string newJobName = Frm_InputMessage.input;
                if (newJobName == string.Empty)
                    return;

                //检查此名称的流程是否已存在
                if (Job.Job_Exist(newJobName))
                {
                    Frm_MessageBox.Instance.MessageBoxShow((Configuration.language == Language.English ? "\r\nA process with this name already exists. The process name cannot be repeated. Please enter again" : "\r\n已存在此名称的流程，流程名不可重复，请重新输入"));
                    goto Again;
                }
                LogHelper.SaveLog(LogType.Operate, (Configuration.language == Language.English ? "A new process named:" + newJobName : "创建了新流程，流程名为：" + newJobName));

                string sourceJobName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
                Job job = Job.GetJobByName(sourceJobName);
                job.jobName = newJobName;
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Application.StartupPath + "\\Config\\Vision\\Job\\" + newJobName + ".job", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, job);
                stream.Close();
                job.jobName = sourceJobName;           //这里要把被克隆的流程的流程名改回去，否则原流程的流程名也会被修改
                LoadJob(Application.StartupPath + "\\Config\\Vision\\Job\\" + newJobName + ".job");

                Frm_Main.Instance.SaveAll();
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "A new process was imported" : "克隆流程成功");
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 节点拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tvw_job_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 释放被拖动的节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tvw_job_DragDrop(object sender, DragEventArgs e)//拖动  
        {
            try
            {
                //获得拖放中的节点  
                TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                //根据鼠标坐标确定要移动到的目标节点  
                System.Drawing.Point pt;
                TreeNode targeNode;
                pt = ((TreeView)(sender)).PointToClient(new System.Drawing.Point(e.X, e.Y));
                targeNode = Project.GetJobTree(jobName).GetNodeAt(pt);
                //如果目标节点无子节点则添加为同级节点,反之添加到下级节点的未端  

                if (moveNode == targeNode)       //若是把自己拖放到自己，不可，返回
                    return;

                if (targeNode == null)       //目标节点为null，就是把节点拖到了空白区域，不可，直接返回
                    return;

                if (moveNode.Level == 1 && targeNode.Level == 1 && moveNode.Parent == targeNode.Parent)          //都是输入输出节点，内部拖动排序
                {
                    moveNode.Remove();
                    targeNode.Parent.Nodes.Insert(targeNode.Index, moveNode);
                    return;
                }

                if (moveNode.Level == 0)        //被拖动的是子节点，也就是工具节点
                {
                    if (targeNode.Level == 0)
                    {
                        moveNode.Remove();
                        Project.GetJobTree(jobName).Nodes.Insert(targeNode.Index, moveNode);

                        ToolInfo temp = new ToolInfo();
                        for (int i = 0; i < L_toolList.Count; i++)
                        {
                            if (L_toolList[i].toolName == moveNode.Text)
                            {
                                temp = L_toolList[i];
                                L_toolList.RemoveAt(i);
                                L_toolList.Insert(targeNode.Index - 2, temp);
                                break;
                            }
                        }
                    }
                    else
                    {
                        moveNode.Remove();
                        Project.GetJobTree(jobName).Nodes.Insert(targeNode.Parent.Index + 1, moveNode);

                        ToolInfo temp = new ToolInfo();
                        for (int i = 0; i < L_toolList.Count; i++)
                        {
                            if (L_toolList[i].toolName == moveNode.Text)
                            {
                                temp = L_toolList[i];
                                L_toolList.RemoveAt(i);
                                L_toolList.Insert(targeNode.Parent.Index, temp);
                                break;
                            }
                        }
                    }
                }
                else        //被拖动的是输入输出节点
                {
                    if (targeNode.Level == 0 && GetToolInfoByToolName(jobName, targeNode.Text).toolType == ToolType.Output)
                    {
                        string result = moveNode.Parent.Text + " . -->" + moveNode.Text.Substring(3);
                        if (!((DataGridViewComboBoxCell)(Frm_Monitor.Instance.dgv_monitor.Rows[Frm_Monitor.Instance.dgv_monitor.Rows.Count - 1].Cells[0])).Items.Contains(result))
                            ((DataGridViewComboBoxCell)(Frm_Monitor.Instance.dgv_monitor.Rows[Frm_Monitor.Instance.dgv_monitor.Rows.Count - 1].Cells[0])).Items.Add(result);

                        GetToolInfoByToolName(jobName, targeNode.Text).input.Add(new ToolIO("<--" + result, "", DataType.String));
                        TreeNode node = targeNode.Nodes.Add("", "<--" + result, 26, 26);
                        node.ForeColor = Color.DarkMagenta;
                        D_itemAndSource.Add(node, moveNode);
                        targeNode.Expand();
                        DrawLine();
                        return;
                    }
                    else if (targeNode.Level == 0)
                        return;

                    //连线前首先要判断被拖动节点是否为输出项，目标节点是否为输入项
                    if (moveNode.Text.Substring(0, 3) != "-->" || targeNode.Text.Substring(0, 3) != "<--")
                    {
                        Frm_Main.Instance.OutputMsg("被拖动节点和目标节点输入输出不匹配，不可关联", Color.Red);
                        return;
                    }

                    //连线前要判断被拖动节点和目标节点的数据类型是否一致
                    if ((DataType)moveNode.Tag != (DataType)targeNode.Tag)
                    {
                        Frm_Main.Instance.OutputMsg("被拖动节点和目标节点数据类型不一致，不可关联", Color.Red);
                        return;
                    }

                    string input = targeNode.Text;
                    if (input.Contains("《"))       //表示已经连接了源
                        input = Regex.Split(input, "《")[0];
                    else            //第一次连接源就需要添加到输入输出集合
                        D_itemAndSource.Add(targeNode, moveNode);
                    GetToolInfoByToolName(jobName, targeNode.Parent.Text).GetInput(input.Substring(3)).value = "《- " + moveNode.Parent.Text + " . " + moveNode.Text.Substring(3);
                    targeNode.Text = input + "《- " + moveNode.Parent.Text + " . " + moveNode.Text.Substring(3);
                    DrawLine();

                    //移除拖放的节点  
                    if (moveNode.Level == 0)
                        moveNode.Remove();
                }
                //更新当前拖动的节点选择  
                Project.GetJobTree(jobName).SelectedNode = moveNode;
                //展开目标节点,便于显示拖放效果  
                targeNode.Expand();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 初始化图标集合
        /// </summary>
        internal static void Init_Icon_List()
        {
            try
            {
                //工具图标
                imageList.Images.Add(Resources.Folder);
                imageList.Images.Add(Resources.ImageAcquistionTool);
                imageList.Images.Add(Resources.ShapeMatchTool);
                imageList.Images.Add(Resources.EyeHandCalibrationTool);
                imageList.Images.Add(Resources.CoorTransTool);
                imageList.Images.Add(Resources.SubImageTool);
                imageList.Images.Add(Resources.BlobAnalyseTool);
                imageList.Images.Add(Resources.DownCameraAlignTool);
                imageList.Images.Add(Resources.DistanceLLTool);
                imageList.Images.Add(Resources.FindLineTool);
                imageList.Images.Add(Resources.FindCircleTool);         //10
                imageList.Images.Add(Resources.CodeEditTool);
                imageList.Images.Add(Resources.LabelTool);
                imageList.Images.Add(Resources.OutputTool);

                //非工具图标
                imageList.Images.Add(Resources.Image);
                imageList.Images.Add(Resources.ColorToGrayTool);
                imageList.Images.Add(Resources.GrayMatchTool);
                imageList.Images.Add(Resources.UnknownTool);
                imageList.Images.Add(Resources.DistancePPTool);
                imageList.Images.Add(Resources.DistancePLTool);
                imageList.Images.Add(Resources.AngleLLTool);           //20
                imageList.Images.Add(Resources.FitLineTool);
                imageList.Images.Add(Resources.FitCircleTool);
                imageList.Images.Add(Resources.OCRTool);
                imageList.Images.Add(Resources.BarCodeTool);
                imageList.Images.Add(Resources.QRTool);
                imageList.Images.Add(Resources.Empty);
                imageList.Images.Add(Resources.ColorToRGBTool);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 判断流程是否已经存在输出工具，一个流程只能含有一个输出工具
        /// </summary>
        /// <returns></returns>
        internal bool Exist_Output()
        {
            try
            {
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolType == ToolType.Output)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return false;
            }
        }
        /// <summary>
        /// 绘制输入输出指向线
        /// </summary>
        /// <param name="obj"></param>
        public void DrawLine()
        {
            try
            {
                if (!isDrawing)
                {
                    isDrawing = true;
                    Thread th = new Thread(() =>
                    {
                        Project.GetJobTree(jobName).MouseWheel += new MouseEventHandler(numericUpDown1_MouseWheel);          //划线的时候不能滚动，否则画好了线，结果已经滚到其它地方了
                        maxLength = 150;
                        colValueAndColor.Clear();
                        startNodeAndColor.Clear();
                        list.Clear();
                        TreeView tree = Project.GetJobTree(jobName);
                        g = tree.CreateGraphics();
                        tree.CreateGraphics().Dispose();

                        foreach (KeyValuePair<TreeNode, TreeNode> item in D_itemAndSource)
                        {
                            CreateLine(tree, item.Key, item.Value);
                        }
                        Application.DoEvents();
                        Project.GetJobTree(jobName).MouseWheel -= new MouseEventHandler(numericUpDown1_MouseWheel);
                        isDrawing = false;
                    });
                    th.IsBackground = true;
                    th.ApartmentState = ApartmentState.STA;             //此处要加一行，否则画线时会报错
                    th.Start();
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        //取消滚轮事件
        void numericUpDown1_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }
        }
        /// <summary>
        /// 通过流程名获取流程
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>流程</returns>
        public static Job GetJobByName(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (((Job)Project.Instance.L_jobList[i]).jobName == jobName)
                        return (Job)Project.Instance.L_jobList[i];
                }
                Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Can not find job named：" + jobName + "（Error code：0001）" : "未找到名为" + jobName + "的流程（错误代码：0001）");
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }
        private static Graphics g;
        /// <summary>
        /// 画Treeview控件两个节点之间的连线
        /// </summary>
        /// <param name="treeview">要画连线的Treeview</param>
        /// <param name="startNode">结束节点</param>
        /// <param name="endNode">开始节点</param>
        private void CreateLine(TreeView treeview, TreeNode endNode, TreeNode startNode)
        {
            try
            {
                //得到起始与结束节点之间所有节点的最大长度  ，保证画线不穿过节点
                int startNodeParantIndex = startNode.Parent.Index;
                int endNodeParantIndex = endNode.Parent.Index;
                int startNodeIndex = startNode.Index;
                int endNodeIndex = endNode.Index;
                int max = 0;

                if (!startNode.Parent.IsExpanded)
                {
                    max = startNode.Parent.Bounds.X + startNode.Parent.Bounds.Width;
                }
                else
                {
                    for (int i = startNodeIndex; i < startNode.Parent.Nodes.Count - 1; i++)
                    {
                        if (max < treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.Width)
                            max = treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.Width;
                    }
                }
                for (int i = startNodeParantIndex + 1; i < endNodeParantIndex; i++)
                {
                    if (!treeview.Nodes[i].IsExpanded)
                    {
                        if (max < treeview.Nodes[i].Bounds.X + treeview.Nodes[i].Bounds.Width)
                            max = treeview.Nodes[i].Bounds.X + treeview.Nodes[i].Bounds.Width;
                    }
                    else
                    {
                        for (int j = 0; j < treeview.Nodes[i].Nodes.Count; j++)
                        {
                            if (max < treeview.Nodes[i].Nodes[j].Bounds.X + treeview.Nodes[i].Nodes[j].Bounds.Width)
                                max = treeview.Nodes[i].Nodes[j].Bounds.X + treeview.Nodes[i].Nodes[j].Bounds.Width;
                        }
                    }
                }
                if (!endNode.Parent.IsExpanded)
                {
                    if (max < endNode.Parent.Bounds.X + endNode.Parent.Bounds.Width)
                        max = endNode.Parent.Bounds.X + endNode.Parent.Bounds.Width;
                }
                else
                {
                    for (int i = 0; i < endNode.Index; i++)
                    {
                        if (max < treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.Width)
                            max = treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.Width;
                    }
                }
                max += 20;        //箭头不能连着 节点，

                if (!startNode.Parent.IsExpanded)
                    startNode = startNode.Parent;
                if (!endNode.Parent.IsExpanded)
                    endNode = endNode.Parent;

                if (endNode.Bounds.X + endNode.Bounds.Width + 20 > max)
                    max = endNode.Bounds.X + endNode.Bounds.Width + 20;
                if (startNode.Bounds.X + startNode.Bounds.Width + 20 > max)
                    max = startNode.Bounds.X + startNode.Bounds.Width + 20;

                //判断是否可以在当前处划线
                foreach (KeyValuePair<int, Dictionary<TreeNode, TreeNode>> item in list)
                {
                    if (Math.Abs(max - item.Key) < 15)
                    {
                        foreach (KeyValuePair<TreeNode, TreeNode> item1 in item.Value)
                        {
                            if (startNode != item1.Value)
                            {
                                if ((item1.Value.Bounds.X < maxLength && item1.Key.Bounds.X < maxLength) || (item1.Value.Bounds.X < maxLength && item1.Key.Bounds.X < maxLength))
                                {
                                    max += (15 - Math.Abs(max - item.Key));
                                }
                            }
                        }
                    }
                }

                Dictionary<TreeNode, TreeNode> temp = new Dictionary<TreeNode, TreeNode>();
                temp.Add(endNode, startNode);
                if (!list.ContainsKey(max))
                    list.Add(max, temp);
                else
                    list[max].Add(endNode, startNode);

                if (!startNodeAndColor.ContainsKey(startNode))
                    startNodeAndColor.Add(startNode, color[startNodeAndColor.Count]);

                Pen pen = new Pen(startNodeAndColor[startNode], 1);
                Brush brush = new SolidBrush(startNodeAndColor[startNode]);

                g.DrawLine(pen, startNode.Bounds.X + startNode.Bounds.Width,
                    startNode.Bounds.Y + startNode.Bounds.Height / 2,
                max,
                  startNode.Bounds.Y + startNode.Bounds.Height / 2);
                g.DrawLine(pen, max,
                   startNode.Bounds.Y + startNode.Bounds.Height / 2,
                   max,
                  endNode.Bounds.Y + endNode.Bounds.Height / 2);
                g.DrawLine(pen, max,
                   endNode.Bounds.Y + endNode.Bounds.Height / 2,
                   endNode.Bounds.X + endNode.Bounds.Width,
                     endNode.Bounds.Y + endNode.Bounds.Height / 2);
                g.DrawString("<", new Font("微软雅黑", 12F), brush, endNode.Bounds.X + endNode.Bounds.Width - 5,
                     endNode.Bounds.Y + endNode.Bounds.Height / 2 - 12);
                Application.DoEvents();
            }
            catch { }
        }
        /// <summary>
        /// 通过作业名删除作业
        /// </summary>
        /// <param name="jobName">流程名</param>
        internal static void RemoveJobByName(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (((Job)Project.Instance.L_jobList[i]).jobName == jobName)
                    {
                        Project.Instance.L_jobList.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 判断是否已经存在此名称的流程
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>是否已存在</returns>
        internal static bool Job_Exist(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (((Job)Project.Instance.L_jobList[i]).jobName == jobName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return true;
            }
        }
        /// <summary>
        /// 判断TreeView是否已经包含某节点
        /// </summary>
        /// <param name="key">节点文本</param>
        /// <returns>是否包含</returns>
        private bool TreeView_Contains_Key(string key)
        {
            try
            {
                foreach (TreeNode node in Project.GetJobTree(jobName).Nodes)
                {
                    if (node.Text == key)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return false;
            }
        }
        /// <summary>
        /// 放弃重命名
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void ResetName(object obj)
        {
            try
            {
                Thread.Sleep(20);
                Project.GetJobTree(jobName).SelectedNode.Text = nodeTextBeforeEdit;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 修改工具名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EditNodeText(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                string newToolName = e.Label;
                if (newToolName == "" || newToolName == null)
                {
                    ThreadPool.QueueUserWorkItem(ResetName);
                    return;
                }

                //检查是否已经存在此名称的工具
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == newToolName)
                    {
                        ((TreeView)sender).SelectedNode.Text = nodeTextBeforeEdit;
                        Application.DoEvents();
                        Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "\r\nA tool with this name already exists, and the tool name cannot be duplicated" : "\r\n已存在此名称的工具，工具名不可重复");
                        ((TreeView)sender).SelectedNode.BeginEdit();
                        return;
                    }
                }

                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == nodeTextBeforeEdit)
                    {
                        L_toolList[i].toolName = newToolName;
                    }
                }

                for (int i = 0; i < L_toolList.Count; i++)
                {
                    //对OutputBox特殊处理
                    if (L_toolList[i].toolType == ToolType.Output)
                    {
                        for (int j = 0; j < L_toolList[i].input.Count; j++)
                        {
                            string sourceFromItem = L_toolList[i].input[j].IOName;
                            string sourceFromToolName = Regex.Split(sourceFromItem.Substring(3), " . ")[0];
                            if (sourceFromToolName == nodeTextBeforeEdit)
                            {
                                string oldKey = L_toolList[i].input[j].IOName;
                                string value = L_toolList[i].input[j].value.ToString();
                                L_toolList[i].RemoveInputIO(oldKey);
                                string newKey = "<--" + newToolName + " . " + Regex.Split(sourceFromItem.Substring(3), " . ")[1];
                                L_toolList[i].input.Add(new ToolIO(newKey, value, DataType.String));
                                //修改节点文本
                                TreeNode toolNode = GetToolNodeByNodeText(L_toolList[i].toolName);
                                string nodeText = oldKey;
                                foreach (TreeNode item in toolNode.Nodes)
                                {
                                    if (((TreeNode)item).Text == nodeText)
                                    {
                                        ((TreeNode)item).Text = L_toolList[i].input[j].IOName;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < L_toolList[i].input.Count; j++)
                        {
                            string sourceFromItem = L_toolList[i].input[j].value.ToString();
                            string sourceFromToolName = Regex.Split(sourceFromItem.Substring(3), " . ")[0];
                            if (sourceFromToolName == nodeTextBeforeEdit)
                            {
                                L_toolList[i].input[j].value = "《- " + newToolName + " . " + Regex.Split(sourceFromItem.Substring(3), " . ")[1];
                                //修改节点文本
                                TreeNode toolNode = GetToolNodeByNodeText(L_toolList[i].toolName);
                                string nodeText = L_toolList[i].input[j].value + sourceFromItem;
                                foreach (TreeNode item in toolNode.Nodes)
                                {
                                    if (((TreeNode)item).Text == nodeText)
                                    {
                                        ((TreeNode)item).Text = L_toolList[i].input[j].value + "《- " + newToolName + " . " + Regex.Split(sourceFromItem.Substring(3), " . ")[1];
                                    }
                                }
                            }
                        }
                    }
                }
                Project.GetJobTree(jobName).Show();
                Project.GetJobTree(jobName).LabelEdit = false;
                Application.DoEvents();
                Frm_Main.Save(jobName);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
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
                    Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "\r\nThe process file does not exist" : "\r\n流程文件不存在");
                    return null;
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                Job job = (Job)formatter.Deserialize(stream);
                stream.Close();
                foreach (TabPage item in Frm_Job.Instance.tbc_jobs.TabPages)
                {
                    if (item.Text == job.jobName)
                    {
                        Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "\r\nA process with the same name already exists. Please do not add it again" : "\r\n已经存在与此流程名相同的流程，请勿重复添加");
                        return new Job();
                    }
                }
                Project.Instance.L_jobList.Add(job);

                TreeView tvw_job = new TreeView();
                tvw_job.Scrollable = true;
                tvw_job.ItemHeight = 26;
                tvw_job.ShowLines = false;
                tvw_job.AllowDrop = true;
                tvw_job.ImageList = Job.imageList;
                tvw_job.TabStop = false;

                tvw_job.AfterSelect += job.tvw_job_AfterSelect;
                tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
                tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
                tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);

                //节点间拖拽
                tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
                tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
                tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);

                //以下事件为画线事件

                //   tvw_job.DrawMode = TreeViewDrawMode.OwnerDrawText;

                Frm_Job.Instance.Paint += job.Instance_Paint;

                tvw_job.MouseMove += job.DrawLineWithoutRefresh;
                tvw_job.MouseWheel += job.DrawLineWithoutRefresh;

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
                    //  string[] inputKeys = job.L_toolList[i].input.Keys.ToArray();
                    for (int j = 0; j < job.L_toolList[i].input.Count; j++)
                    {
                        TreeNode treeNode;
                        //因为OutputBox只有源，所以此处特殊处理
                        if (job.L_toolList[i].toolType != ToolType.Output)
                            treeNode = node.Nodes.Add("<--" + job.L_toolList[i].input[j].IOName + job.L_toolList[i].input[j].value);
                        else
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName);

                        ////if (inputKeys[j].Contains("Image"))       //图像变量类型
                        ////    treeNode.Tag = DataType.Image;
                        ////else if (inputKeys[j].Contains("Region"))       //区域变量类型
                        ////    treeNode.Tag = "Region";
                        ////else
                        treeNode.Tag = job.L_toolList[i].input[j].ioType;       //字符串变量类型
                        treeNode.ForeColor = Color.DarkMagenta;

                        //解析需要连线的节点对

                        if (treeNode.ToString().Contains("《-"))
                        {
                            string toolNodeText = Regex.Split(job.L_toolList[i].input[j].value.ToString(), " . ")[0].Substring(3);
                            string toolIONodeText = "-->" + Regex.Split(job.L_toolList[i].input[j].value.ToString(), " . ")[1];
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
                        TreeNode treeNode = node.Nodes.Add("-->" + job.L_toolList[i].output[k].IOName);
                        //if (outputKeys[k].Contains("Image") || outputKeys[k].Contains("图像"))
                        //    treeNode.Tag = DataType.Image;
                        //else if (outputKeys[k].Contains("Region"))
                        //    treeNode.Tag = "Region";
                        //else
                        treeNode.Tag = job.L_toolList[i].output[k].ioType;
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

                        case ToolType.SDK_Basler:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                            break;

                        case ToolType.SDK_Congex:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                            break;

                        case ToolType.SDK_PointGray:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                            break;

                        case ToolType.SDK_IMAVision:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                            break;

                        case ToolType.SDK_MindVision:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                            break;

                        case ToolType.SDK_HIKVision:
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

                        case ToolType.CircleCalibration:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 17;
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

                        case ToolType.ColorToRGB:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 27;
                            break;

                        case ToolType.FindLine:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 9;
                            break;

                        case ToolType.FindCircle:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 10;
                            break;

                        case ToolType.DistancePL:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 19;
                            break;

                        case ToolType.DistanceSS:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 8;
                            break;

                        case ToolType.OCR:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 23;
                            break;

                        case ToolType.Barcode:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 24;
                            break;

                        case ToolType.Logic:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 7;
                            break;

                        case ToolType.CodeEdit:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 11;
                            break;

                        case ToolType.Label:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 12;
                            break;

                        case ToolType.Output:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 13;
                            break;

                        case ToolType.Condition:
                            {
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 13;

                                for (int ii = 0; ii < job.L_toolList.Count; ii++)
                                {
                                    job.L_toolList[ii].Id = ii;
                                }

                                Frm_ConditionTool.Instance.InitData(job.L_toolList);
                            }
                            break;

                        default:
                            Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 17;
                            break;

                    }
                    for (int k = 0; k < Project.GetJobTree(job.jobName).Nodes[j].Nodes.Count; k++)
                    {
                        Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].SelectedImageIndex = 26;
                    }
                }
                //默认选中第一个节点
                if (tvw_job.Nodes.Count > 0)
                    tvw_job.SelectedNode = tvw_job.Nodes[0];
                return job;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }

        void Instance_Paint(object sender, PaintEventArgs e)
        {
            DrawLineWithoutRefresh(null, null);
        }

        void tvw_job_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Draw_Line(null, null);
        }
        /// <summary>
        /// 生成新工具的名称
        /// </summary>
        /// <param name="toolName">工具类型</param>
        /// <returns>工具名称</returns>
        internal string GetNewToolName(string toolType)
        {
            try
            {
                if (!TreeView_Contains_Key(toolType))
                {
                    return toolType;
                }
                for (int i = 1; i < 101; i++)
                {
                    if (!TreeView_Contains_Key(toolType + "_" + i))
                    {
                        return toolType + "_" + i;
                    }
                }
                Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "This tool has reached the maximum number of additions and cannot continue to be added (error code: 0002)" : "此工具已添加个数已达到数量上限，无法继续添加（错误代码：0002）");
                return "Error";
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return "Error";
            }
        }
        /// <summary>
        /// 加载流程
        /// </summary>
        /// <param name="job">流程对象</param>
        internal static void InportJob(Job job)
        {
            try
            {
                TreeView tvw_job = new TreeView();
                tvw_job.Scrollable = true;
                tvw_job.ItemHeight = 26;
                tvw_job.ShowLines = false;
                tvw_job.AllowDrop = true;
                tvw_job.ImageList = Job.imageList;

                tvw_job.AfterSelect += job.tvw_job_AfterSelect;
                tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
                tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
                tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);

                //节点间拖拽
                tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
                tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
                tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);

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
                    //string[] inputKeys = job.L_toolList[i].input.Keys.ToArray();
                    for (int j = 0; j < job.L_toolList[i].input.Count; j++)
                    {
                        TreeNode treeNode;
                        //因为OutputBox只有源，所以此处特殊处理
                        if (job.L_toolList[i].toolType != ToolType.Output)
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName + job.L_toolList[i].input[j].value);
                        else
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName);

                        //if (inputKeys[j].Contains("Image"))       //图像变量类型
                        //    treeNode.Tag = DataType.Image;
                        //else if (inputKeys[j].Contains("Region"))       //区域变量类型
                        //    treeNode.Tag = "Region";
                        //else
                        treeNode.Tag = job.L_toolList[i].input[j].ioType;       //字符串变量类型
                        treeNode.ForeColor = Color.DarkMagenta;

                        //解析需要连线的节点对

                        if (treeNode.ToString().Contains("《-"))
                        {
                            string toolNodeText = Regex.Split(job.L_toolList[i].input[j].value.ToString(), " . ")[0].Substring(3);
                            string toolIONodeText = "-->" + Regex.Split(job.L_toolList[i].input[j].value.ToString(), " . ")[1];
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
                        TreeNode treeNode = node.Nodes.Add("-->" + job.L_toolList[i].output[k].IOName);
                        ////if (outputKeys[k].Contains("Image") || outputKeys[k].Contains("图像"))
                        ////    treeNode.Tag = DataType.Image;
                        ////else if (outputKeys[k].Contains("Region"))
                        ////    treeNode.Tag = "Region";
                        ////else
                        treeNode.Tag = job.L_toolList[i].output[k].ioType;
                        treeNode.ForeColor = Color.Blue;
                    }

                    //更新工具树图标
                    for (int j = 0; j < Project.GetJobTree(job.jobName).Nodes.Count; j++)
                    {
                        switch (Job.GetToolInfoByToolName(job.jobName, Project.GetJobTree(job.jobName).Nodes[j].Text).toolType)
                        {
                            case ToolType.HalconInterface:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                                break;

                            case ToolType.SDK_Basler:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                                break;

                            case ToolType.SDK_Congex:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                                break;

                            case ToolType.SDK_PointGray:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                                break;

                            case ToolType.SDK_IMAVision:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                                break;

                            case ToolType.SDK_MindVision:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 1;
                                break;

                            case ToolType.SDK_HIKVision:
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

                            case ToolType.CircleCalibration:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 17;
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

                            case ToolType.OCR:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 23;
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
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 12;
                                break;

                            case ToolType.Output:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 13;
                                break;

                            default:
                                Project.GetJobTree(job.jobName).Nodes[j].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].SelectedImageIndex = 17;
                                break;

                        }
                        for (int k = 0; k < Project.GetJobTree(job.jobName).Nodes[j].Nodes.Count; k++)
                        {
                            Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].ImageIndex = Project.GetJobTree(job.jobName).Nodes[j].Nodes[k].SelectedImageIndex = 26;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 运行当前作业
        /// </summary>
        internal static void RunCurJob()
        {
            try
            {
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count == 0)
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "No jobs to run" : "没有可运行的流程", Color.Green);
                    return;
                }
                Frm_Job.Instance.btn_runOnce.Enabled = false;
                Job job = Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text);
                job.Run();
                Frm_Job.Instance.btn_runOnce.Enabled = true;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 通过工具名获取工具信息
        /// </summary>
        /// <param name="toolName">工具名</param>
        /// <returns>工具信息</returns>
        internal static ToolInfo GetToolInfoByToolName(string jobName, string toolName)
        {
            try
            {
                Job job = new Job();
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (Project.Instance.L_jobList[i].jobName == jobName)
                    {
                        job = Project.Instance.L_jobList[i];
                        break;
                    }
                }
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    if (job.L_toolList[i].toolName == toolName)
                    {
                        return job.L_toolList[i];
                    }
                }
                return new ToolInfo();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return new ToolInfo();
            }
        }
        /// <summary>
        /// 通过流程名和工具名获取工具
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <param name="toolName">工具名</param>
        /// <returns></returns>
        internal static object GetToolByToolName(string jobName, string toolName)
        {
            try
            {
                Job job = new Job();
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (Project.Instance.L_jobList[i].jobName == jobName)
                    {
                        job = Project.Instance.L_jobList[i];
                        break;
                    }
                }
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    if (job.L_toolList[i].toolName == toolName)
                    {
                        return job.L_toolList[i].tool;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }
        /// <summary>
        /// 通过输出项字符串获取输出项的值
        /// </summary>
        /// <param name="outputItem">输出项字符创</param>
        /// <returns>输出项的值</returns>
        public string GetOutputItemValue(string outputItem)
        {
            try
            {
                //寻找输出盒工具
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolType == ToolType.Output)
                    {
                        for (int j = 0; j < L_toolList[i].input.Count; j++)
                        {
                            if (L_toolList[i].input[j].value.ToString() == outputItem)
                            {
                                return L_toolList[i].GetInput(outputItem).value.ToString();
                            }
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// 通过TreeNode节点文本获取节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        /// <returns>节点对象</returns>
        internal TreeNode GetToolNodeByNodeText(string nodeText)
        {
            try
            {
                foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                {
                    if (((TreeNode)toolNode).Text != nodeText)
                    {
                        foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                        {
                            if (((TreeNode)itemNode).Text.Substring(3) == nodeText)
                            {
                                return itemNode;
                            }
                        }
                    }
                    else
                    {
                        return toolNode;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }
        /// <summary>
        /// 通过TreeNode节点文本获取输入输出节点
        /// </summary>
        /// <param name="toolName">工具名称</param>
        /// <returns>IO名称</returns>
        internal TreeNode GetToolIONodeByNodeText(string toolName, string toolIOName)
        {
            try
            {
                foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                {
                    if (toolNode.Text == toolName)
                    {
                        foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                        {
                            if (((TreeNode)itemNode).Text == toolIOName)
                            {
                                return itemNode;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }
        /// <summary>
        /// 指定源事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceFrom(object sender, EventArgs e)
        {
            try
            {
                string nodeText;
                if (Project.GetJobTree(jobName).SelectedNode.Level == 1)
                {
                    nodeText = Project.GetJobTree(jobName).SelectedNode.Parent.Text;
                }
                else
                {
                    nodeText = Project.GetJobTree(jobName).SelectedNode.Text;
                }
                string input = Project.GetJobTree(jobName).SelectedNode.Text;
                if (Project.GetJobTree(jobName).SelectedNode.Text.Contains("《"))       //表示已经连接了源
                {
                    input = Regex.Split(Project.GetJobTree(jobName).SelectedNode.Text, "《")[0];
                }
                GetToolInfoByToolName(jobName, nodeText).GetInput(input).value = sender.ToString();
                Project.GetJobTree(jobName).SelectedNode.Text = input + sender.ToString();
                GetToolInfoByToolName(jobName, nodeText).GetInput(input).value = sender.ToString();

                string toolNodeText = Regex.Split(sender.ToString(), " . ")[0].Substring(3);
                string toolIONodeText = "-->" + Regex.Split(sender.ToString(), " . ")[1];
                D_itemAndSource.Add(Project.GetJobTree(jobName).SelectedNode, GetToolIONodeByNodeText(toolNodeText, toolIONodeText));
                DrawLine();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void RunTool(object sender, EventArgs e)
        {
            try
            {
                ((ToolBase)(GetToolByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text))).Run(jobName, true, true);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItem(object sender, EventArgs e)
        {
            try
            {
                if (Project.GetJobTree(jobName).SelectedNode == null)
                    return;

                string nodeText = Project.GetJobTree(jobName).SelectedNode.Text.ToString();
                int level = Project.GetJobTree(jobName).SelectedNode.Level;
                string fatherNodeText = string.Empty;

                //如果是子节点
                if (level == 1)
                {
                    fatherNodeText = Project.GetJobTree(jobName).SelectedNode.Parent.Text;
                }
                foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                {
                    if (level == 1)
                    {
                        if (toolNode.Text == fatherNodeText)
                        {
                            foreach (var itemNode in ((TreeNode)toolNode).Nodes)
                            {
                                if (itemNode != null)
                                {
                                    if (((TreeNode)itemNode).Text == nodeText)
                                    {
                                        //移除连线集合中的这条连线
                                        for (int i = 0; i < D_itemAndSource.Count; i++)
                                        {
                                            if (((TreeNode)itemNode) == D_itemAndSource.Keys.ToArray()[i] || ((TreeNode)itemNode) == D_itemAndSource[D_itemAndSource.Keys.ToArray()[i]])
                                                D_itemAndSource.Remove(D_itemAndSource.Keys.ToArray()[i]);
                                        }

                                        ((TreeNode)itemNode).Remove();
                                        Project.GetJobTree(jobName).SelectedNode = null;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (((TreeNode)toolNode).Text == nodeText)
                        {
                            ((TreeNode)toolNode).Remove();
                            break;
                        }
                    }
                }

                //如果是父节点
                if (level == 0)
                {
                    for (int i = 0; i < L_toolList.Count; i++)
                    {
                        if (L_toolList[i].toolName == nodeText)
                        {
                            try
                            {
                                //移除连线集合中的这条连线
                                for (int j = D_itemAndSource.Count - 1; j >= 0; j--)
                                {
                                    if (nodeText == D_itemAndSource.Keys.ToArray()[j].Parent.Text || nodeText == D_itemAndSource[D_itemAndSource.Keys.ToArray()[j]].Parent.Text)
                                        D_itemAndSource.Remove(D_itemAndSource.Keys.ToArray()[j]);
                                }
                            }
                            catch { }

                            L_toolList.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < L_toolList.Count; i++)
                    {
                        if (L_toolList[i].toolName == fatherNodeText)
                        {
                            for (int j = 0; j < L_toolList[i].input.Count; j++)
                            {
                                if (L_toolList[i].input[j].value.ToString() == Regex.Split(nodeText, "《")[0])
                                    L_toolList[i].RemoveInputIO(Regex.Split(nodeText, "《")[0]);
                            }
                            for (int j = 0; j < L_toolList[i].output.Count; j++)
                            {
                                if (L_toolList[i].output[j].IOName == nodeText.Substring(3))
                                    L_toolList[i].RemoveOutputIO(nodeText.Substring(3));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 插入工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertTool(object sender, EventArgs e)
        {
            Frm_Tools.Instance.Add_Tool(((ToolStripItem)sender).Text, Project.GetJobTree(jobName).SelectedNode.Index);
        }
        /// <summary>
        /// 启用工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableOrDisenableTool(object sender, EventArgs e)
        {
            string jobName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
            Job.GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).enable = !Job.GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).enable;
        }
        /// <summary>
        /// 获取工具输入项的个数
        /// </summary>
        private int GetInputItemNum(TreeNode toolNode)
        {
            try
            {
                int num = 0;
                foreach (TreeNode item in toolNode.Nodes)
                {
                    if (item.Text.Substring(0, 3) == "<--")
                    {
                        num++;
                    }
                }
                return num;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return 0;
            }
        }
        /// <summary>
        /// 重命名工具                
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameTool(object sender, EventArgs e)
        {
            Project.GetJobTree(jobName).LabelEdit = true;
            Project.GetJobTree(jobName).SelectedNode.BeginEdit();
        }
        /// <summary>
        /// 修改工具备注                
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyTipInfo(object sender, EventArgs e)
        {
            try
            {
                Frm_InputMessage frm_inputMessage = new Frm_InputMessage();
                frm_inputMessage.lal_title.Text = Configuration.language == Language.English ? "Please input name of standard image" : "请输入工具备注信息";
                frm_inputMessage.btn_confirm.Text = Configuration.language == Language.English ? "OK" : "确定";
                frm_inputMessage.txt_input.Text = GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).toolTipInfo;
                frm_inputMessage.TopMost = true;
                frm_inputMessage.ShowDialog();
                if (Frm_InputMessage.input == string.Empty)
                {
                    return;
                }
                GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).toolTipInfo = Frm_InputMessage.input;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 添加输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_input(object sender, EventArgs e)
        {
            try
            {
                string result = sender.ToString();

                //首先检查是否已经有此输入项,若已添加，则返回
                foreach (var item in Project.GetJobTree(jobName).SelectedNode.Nodes)
                {
                    string text;
                    if (((TreeNode)item).Text.Contains("《"))
                    {
                        text = Regex.Split(((TreeNode)item).Text, "《")[0];
                    }
                    else
                    {
                        text = ((TreeNode)item).Text;
                    }
                    if (text == "<--" + result)
                    {
                        Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "This input or output item already exists and cannot be added repeatedly" : "已存在此输入或输出项，不可重复添加", Color.Green);
                        return;
                    }
                }

                int insertPos = GetInputItemNum(Project.GetJobTree(jobName).SelectedNode);        //获取插入位置，要保证输入项在前，输出项在后
                TreeNode node = Project.GetJobTree(jobName).SelectedNode.Nodes.Insert(insertPos, "", "<--" + result, 26, 26);
                node.ForeColor = Color.DarkMagenta;
                Project.GetJobTree(jobName).SelectedNode.Expand();
                DataType ioType = (DataType)((ToolStripItem)sender).Tag;

                //指定输入变量的类型
                //if (result == (Configuration.language == Language.English ? "InputImage" : "输入图像"))
                //    node.Tag = DataType.Image;
                //else if (result == "BlobResult")
                //    node.Tag = "BlobResult";
                //else
                node.Tag = ioType;
                node.Name = "<--" + result;
                GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).input.Add(new ToolIO(result, "", ioType));

                //如果是给输出工具添加输入，则需要连线
                if (GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).toolType == ToolType.Output)
                {
                    if (!((DataGridViewComboBoxCell)(Frm_Monitor.Instance.dgv_monitor.Rows[Frm_Monitor.Instance.dgv_monitor.Rows.Count - 1].Cells[0])).Items.Contains(result))
                        ((DataGridViewComboBoxCell)(Frm_Monitor.Instance.dgv_monitor.Rows[Frm_Monitor.Instance.dgv_monitor.Rows.Count - 1].Cells[0])).Items.Add(result);
                    string toolNodeText = Regex.Split(sender.ToString(), " . ")[0];
                    string toolIONodeText = Regex.Split(sender.ToString(), " . ")[1];
                    D_itemAndSource.Add(GetToolIONodeByNodeText(Project.GetJobTree(jobName).SelectedNode.Text, "<--" + sender.ToString()), GetToolIONodeByNodeText(toolNodeText, toolIONodeText));

                    Draw_Line(null, null);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 添加输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_output(object sender, EventArgs e)
        {
            try
            {
                string result = sender.ToString();
                foreach (var item in Project.GetJobTree(jobName).SelectedNode.Nodes)
                {
                    if (((TreeNode)item).Text == "-->" + result)
                    {
                        return;
                    }
                }
                TreeNode node = Project.GetJobTree(jobName).SelectedNode.Nodes.Add("", "-->" + result, 26, 26);
                node.ForeColor = Color.Blue;
                Project.GetJobTree(jobName).SelectedNode.Expand();
                DataType ioType = (DataType)((ToolStripItem)sender).Tag;

                //指定输出变量的类型
                if (result == (Configuration.language == Language.English ? "OutputImage" : "输出图像"))
                {
                    //  node.Tag = DataType.Image;
                    node.ToolTipText = (Configuration.language == Language.English ? "Graphic variables do not support display" : "图形变量不支持显示");
                }
                //else if (result == "BlobResult")
                //    node.Tag = "BlobResult";
                //else
                node.Tag = ioType;

                node.Name = "-->" + result;
                GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).output.Add(new ToolIO(result, "", ioType));
                node.ToolTipText = (Configuration.language == Language.English ? "NotRun" : "未运行");
                Project.GetJobTree(jobName).ShowNodeToolTips = true;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 工具上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveUp(object sender, EventArgs e)
        {
            try
            {
                if (Project.GetJobTree(jobName).SelectedNode.Index == 0)
                    return;

                TreeNode Node = Project.GetJobTree(jobName).SelectedNode;
                TreeNode PrevNode = Node.PrevNode;
                if (PrevNode != null)
                {
                    TreeNode NewNode = (TreeNode)Node.Clone();
                    if (Node.Parent == null)
                    {
                        Project.GetJobTree(jobName).Nodes.Insert(PrevNode.Index, NewNode);
                    }
                    else
                    {
                        Node.Parent.Nodes.Insert(PrevNode.Index, NewNode);
                    }
                    Node.Remove();
                    Project.GetJobTree(jobName).SelectedNode = NewNode;
                }

                ToolInfo temp = new ToolInfo();
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == Project.GetJobTree(jobName).SelectedNode.Text)
                    {
                        temp = L_toolList[i];
                        L_toolList[i] = L_toolList[i - 1];
                        L_toolList[i - 1] = temp;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 工具下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveDown(object sender, EventArgs e)
        {
            try
            {
                if (Project.GetJobTree(jobName).SelectedNode.Index == Project.GetJobTree(jobName).Nodes.Count - 1)
                    return;
                TreeNode Node = Project.GetJobTree(jobName).SelectedNode;
                TreeNode NextNode = Node.NextNode;
                if (NextNode != null)
                {
                    TreeNode NewNode = (TreeNode)Node.Clone();
                    if (Node.Parent == null)
                    {
                        Project.GetJobTree(jobName).Nodes.Insert(NextNode.Index + 1, NewNode);
                    }
                    else
                    {
                        Node.Parent.Nodes.Insert(NextNode.Index + 1, NewNode);
                    }
                    Node.Remove();
                    Project.GetJobTree(jobName).SelectedNode = NewNode;
                }

                ToolInfo temp = new ToolInfo();
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == Project.GetJobTree(jobName).SelectedNode.Text)
                    {
                        temp = L_toolList[i];
                        L_toolList[i] = L_toolList[i + 1];
                        L_toolList[i + 1] = temp;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 把Double数值格式化成标准的9位的格式
        /// </summary>
        /// <param name="value">需要格式化的数值</param>
        /// <returns>结果字符串</returns>
        private string FormatValueTo9Bit(double value)
        {
            return value >= 0 ? "+" + value.ToString("0000.000") : value.ToString("0000.000");
        }
        /// <summary>
        /// 把节点文本添加到剪切板，用于复制粘贴输出项文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyNodeText(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(Project.GetJobTree(jobName).SelectedNode.Text);
        }
        /// <summary>
        /// 流程树右击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TVW_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                if (e.Button == MouseButtons.Left && Project.GetJobTree(jobName).SelectedNode != null)      //如果是鼠标左击，就改工具名
                {

                    TreeView treeView = (TreeView)sender;
                    if (e.Button == MouseButtons.Right)
                    {
                        if (treeView.SelectedNode != null)
                            treeView.SelectedNode.BeginEdit();
                    }
                    return;
                }

                //判断是否在节点单击
                TreeViewHitTestInfo test = Project.GetJobTree(jobName).HitTest(e.X, e.Y);
                if (test.Node == null || test.Location != TreeViewHitTestLocations.Label && e.Button == MouseButtons.Right)       //单击空白
                {
                    Project.GetJobTree(jobName).ContextMenuStrip = rightClickMenuAtBlank;
                    rightClickMenuAtBlank.Show(e.X, e.Y);
                    return;
                }
                else
                {
                    Project.GetJobTree(jobName).ContextMenuStrip = rightClickMenu;
                }

                rightClickMenu.Items.Clear();
                rightClickMenu.Items.Add(Configuration.language == Language.English ? "Add Input" : "运行");
                rightClickMenu.Items[0].Click += new EventHandler(RunTool);
                rightClickMenu.Items.Add(Configuration.language == Language.English ? "Add Input" : "添加输入项");
                rightClickMenu.Items.Add(Configuration.language == Language.English ? "Add Output" : "添加输出项");
                rightClickMenu.Items.Add(Configuration.language == Language.English ? "InsertTool" : "插入工具");
                rightClickMenu.Items.Add(Configuration.language == Language.English ? "Enable" : "启用/忽略切换");
                rightClickMenu.Items[4].Click += new EventHandler(EnableOrDisenableTool);
                rightClickMenu.Items.Add(Configuration.language == Language.English ? "DeleteItem" : "删除项");
                rightClickMenu.Items[5].Image = Resources.DeleteItem;
                rightClickMenu.Items[5].Click += new EventHandler(DeleteItem);
                rightClickMenu.Items.Add(Configuration.language == Language.English ? "Rename" : "重命名");
                rightClickMenu.Items[6].Click += new EventHandler(RenameTool);
                rightClickMenu.Items.Add(Configuration.language == Language.English ? "Rename" : "修改备注");
                rightClickMenu.Items[7].Click += new EventHandler(ModifyTipInfo);

                //如果不是第一个则添加上移选项
                if (Project.GetJobTree(jobName).SelectedNode == null)
                    return;
                if (Project.GetJobTree(jobName).SelectedNode.Index != 0)
                {
                    rightClickMenu.Items.Add(Configuration.language == Language.English ? "MoveUp" : "上移");
                    rightClickMenu.Items[8].Click += new EventHandler(MoveUp);
                    if (Project.GetJobTree(jobName).SelectedNode.Index != Project.GetJobTree(jobName).Nodes.Count - 1)
                    {
                        rightClickMenu.Items.Add(Configuration.language == Language.English ? "MoveDown" : "下移");
                        rightClickMenu.Items[9].Click += new EventHandler(MoveDown);
                    }
                }
                else
                {
                    rightClickMenu.Items.Add(Configuration.language == Language.English ? "MoveDown" : "下移");
                    rightClickMenu.Items[8].Click += new EventHandler(MoveDown);
                }

                if (e.Button == MouseButtons.Right && e.Clicks == 1)        //如果右击
                {
                    ToolType toolType = GetToolInfoByToolName(jobName, Project.GetJobTree(jobName).SelectedNode.Text).toolType;

                    //清空输入，输出下拉选项
                    ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Clear();
                    ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Clear();
                    Application.DoEvents();

                    bool clickToolNode = true;              //操作的是工具节点
                    switch (toolType)
                    {
                        #region ImageAcquistion
                        case ToolType.HalconInterface:
                            ToolStripItem toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputImage" : "输出图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region SDK_Basler
                        case ToolType.SDK_Basler:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputImage" : "输出图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region SDK_Congex
                        case ToolType.SDK_Congex:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputImage" : "输出图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region SDK_PointGray
                        case ToolType.SDK_PointGray:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputImage" : "输出图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region SDK_IMAVision
                        case ToolType.SDK_IMAVision:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputImage" : "输出图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region SDK_MindVision
                        case ToolType.SDK_MindVision:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputImage" : "输出图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region SDK_HIKVision
                        case ToolType.SDK_HIKVision:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputImage" : "输出图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region ShapeMatch
                        case ToolType.ShapeMatch:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultCount" : "结果个数");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "结果字符串");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "Pose" : "位姿");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Tag = DataType.Pose;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultRow" : "结果行");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[3].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[3].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultCol" : "结果列");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[4].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[4].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultAngle" : "结果角度");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[5].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[5].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region EyeHandCalibration
                        case ToolType.EyeHandCalibration:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputStr" : "输入字符串");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputStr" : "输入点");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[2].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[2].Tag = DataType.Pose;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputStr" : "输入位置");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[3].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[3].Tag = DataType.Pose;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputImage" : "输出图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "输出字符串");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "输出点");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Tag = DataType.Pose;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region CircleCalibration
                        case ToolType.CircleCalibration:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add("输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterRow" : "位姿");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterRow" : "预期圆中心行");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterY" : "预期圆中心列");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleRadius" : "预期圆半径");
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("输出圆");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果圆圆心行坐标");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果圆圆心列坐标");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果圆半径");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("毫米像素比");
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region SubImage
                        case ToolType.SubImage:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add("输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("输出图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region BlobAnalyse
                        case ToolType.BlobAnalyse:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "SearchRegion" : "搜索区域");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Image = Resources.Region;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Tag = DataType.Region;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultCount" : "结果个数");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "BlobResult" : "斑点结果");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultRegion" : "结果区域");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Image = Resources.Region;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Tag = DataType.Region;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region DownCamAlign
                        case ToolType.DownCamAlign:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputAllAddStr" : "输入字符串");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputAllAddStr" : "输入点");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Tag = DataType.Pose;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputAllAddStr" : "输出字符串");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputAllAddStr" : "输出点");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Tag = DataType.Pose;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "OutputAllAddStr" : "格式点");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region ColorToRGB
                        case ToolType.ColorToRGB:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultCount" : "红");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "绿");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "Pose" : "蓝");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region FindLine
                        case ToolType.FindLine:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "Pose" : "位姿");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Tag = DataType.Pose;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add("预期线起点行坐标");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add("预期线起点列坐标");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add("预期线终点行坐标");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add("预期线终点列坐标");
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultSegment" : "结果线段");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Line;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("方向");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果线起点行坐标");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果线起点列坐标");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果线终点行坐标");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果线终点列坐标");
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region FindCircle
                        case ToolType.FindCircle:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add("输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterRow" : "位姿");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterRow" : "预期圆中心行");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterY" : "预期圆中心列");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleRadius" : "预期圆半径");
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("输出圆");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果圆圆心行坐标");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果圆圆心列坐标");
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("结果圆半径");
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region CreateROI
                        case ToolType.CreateROI:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterRow" : "左上点行");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterRow" : "左上点列");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterY" : "右下点行");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleRadius" : "右下点列");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleRadius" : "位姿");
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("输出ROI");
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region CreatePosition
                        case ToolType.CreatePosition:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterRow" : "输入点");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Point;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "ExpectCircleCenterRow" : "输入方向");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add("输出位置");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Pose;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region DistancePL
                        case ToolType.DistancePL:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputPointX" : "输入点");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputPointY" : "输入线段");
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultDistance" : "结果距离");
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region DistanceSS
                        case ToolType.DistanceSS:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputSegment1" : "输入线段1");
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputSegment2" : "输入线段2");
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultDistance" : "结果距离值");
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region LLPoint
                        case ToolType.LLPoint:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputSegment1" : "输入线段1");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Line;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputSegment2" : "输入线段2");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Tag = DataType.Line;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultDistance" : "输出点");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Point;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region  Barcode
                        case ToolType.Barcode:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "结果字符串");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region  RegionFeature
                        case ToolType.RegionFeature:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入区域");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Region;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Region;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "圆度");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "中心点");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Tag = DataType.Point;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "外接仿矩");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Image = Resources.Region;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Tag = DataType.Region;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region  CreateLine
                        case ToolType.CreateLine:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "起点");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Region;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Region;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "终点");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Region;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "输出线");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "输出线方向");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region  RegionOperation
                        case ToolType.RegionOperation:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "区域1");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Region;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Region;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "区域2");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Image = Resources.Region;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Tag = DataType.Region;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "结果区域");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region OCR
                        case ToolType.OCR:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入图像");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.Image;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "结果字符串");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.Image;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region KeyenceSR1000
                        case ToolType.KeyenceSR1000:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "扫码结果");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region CodeEdit
                        case ToolType.CodeEdit:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入项1");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入项2");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入项3");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[2].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[2].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入项4");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[3].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[3].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputImage" : "输入项5");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[4].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[4].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);

                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultCount" : "输出项1");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultStr" : "输出项2");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[1].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems.Add(Configuration.language == Language.English ? "ResultOKNG" : "输出项3");
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[2]).DropDownItems[2].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_output);
                            break;
                        #endregion

                        #region Label
                        case ToolType.Label:
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[01]).DropDownItems.Add(Configuration.language == Language.English ? "InputItem1" : "输入项1");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[0].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputItem2" : "输入项2");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[1].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputItem3" : "输入项3");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[2].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[2].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputItem4" : "输入项4");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[3].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[3].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            toolStripItem = ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems.Add(Configuration.language == Language.English ? "InputItem5" : "输入项5");
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[4].Image = Resources.String;
                            ((ToolStripMenuItem)rightClickMenu.Items[1]).DropDownItems[4].Tag = DataType.String;
                            toolStripItem.Click += new EventHandler(Add_input);
                            break;
                        #endregion

                        #region Output
                        case ToolType.Output:      //输出工具中添加输出项
                            ToolStripMenuItem inputItem = rightClickMenu.Items[1] as ToolStripMenuItem;
                            foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                            {
                                foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                                {
                                    if (((TreeNode)itemNode).Text.Substring(0, 3) != "<--" && ((DataType)((TreeNode)itemNode).Tag) != DataType.Image && ((DataType)((TreeNode)itemNode).Tag) != DataType.Region)
                                    {
                                        string resultStr = ((TreeNode)toolNode).Text + " . " + ((TreeNode)itemNode).Text;
                                        ToolStripItem item = inputItem.DropDownItems.Add(resultStr);
                                        item.Name = resultStr;
                                        item.Click += new EventHandler(Add_input);
                                    }
                                }
                            }
                            break;
                        #endregion

                        case ToolType.Condition:
                            break;
                        #region 显示源
                        default:
                            //指定源
                            clickToolNode = false;
                            string nodeText = Project.GetJobTree(jobName).SelectedNode.Text;
                            string fatherNodeText = Project.GetJobTree(jobName).SelectedNode.Parent.Text;
                            string curNodeType = Project.GetJobTree(jobName).SelectedNode.Tag.ToString();
                            foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                            {
                                foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                                {
                                    if (((TreeNode)itemNode).Text == nodeText)
                                    {
                                        rightClickMenu.Items.Clear();
                                        ToolStripItem sourceFrom = rightClickMenu.Items.Add(Configuration.language == Language.English ? "SourceFrom" : "源于");
                                        ToolStripItem deleteItem = rightClickMenu.Items.Add(Configuration.language == Language.English ? "DeleteItem" : "删除项");
                                        deleteItem.Click += new EventHandler(DeleteItem);
                                        ToolStripItem copyNodeText = rightClickMenu.Items.Add(Configuration.language == Language.English ? "CopyNodeText" : "复制节点文本");
                                        copyNodeText.Click += new EventHandler(CopyNodeText);
                                        deleteItem.Click += new EventHandler(DeleteItem);

                                        ToolStripMenuItem item = rightClickMenu.Items[0] as ToolStripMenuItem;
                                        ((ToolStripMenuItem)rightClickMenu.Items[0]).DropDownItems.Clear();
                                        foreach (TreeNode toolNode1 in Project.GetJobTree(jobName).Nodes)
                                        {
                                            if (toolNode1.Text == fatherNodeText)        //不能指定自己的输出项为源
                                                continue;
                                            if (((TreeNode)toolNode1).Text != (Configuration.language == Language.English ? "OutputTool" : "输出"))
                                            {
                                                foreach (TreeNode itemNode1 in ((TreeNode)toolNode1).Nodes)
                                                {
                                                    string sourceType = itemNode1.Tag.ToString();
                                                    if (sourceType == curNodeType)
                                                    {
                                                        if (((TreeNode)itemNode1).Text.Substring(0, 3) != "<--")
                                                        {
                                                            string resultStr = "《- " + toolNode1.Text + " . " + itemNode1.Text.Substring(3);
                                                            ToolStripItem item1 = item.DropDownItems.Add(resultStr);
                                                            item1.Name = resultStr;
                                                            item1.Click += new EventHandler(SourceFrom);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        #endregion
                    }
                    Project.GetJobTree(jobName).ContextMenuStrip = rightClickMenu;
                    rightClickMenu.Show();
                    Application.DoEvents();

                    #region 插入工具
                    if (clickToolNode)
                    {
                        Thread th = new Thread(() =>
                        {

                            ToolStripItem toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "AcqDevice" : "图像采集");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[0]).DropDownItems.Add("Halcon采集接口");
                                toolStripItem1.Image = Resources.ImageAcquistionTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[0]).DropDownItems.Add("SDK_巴斯勒");
                                toolStripItem1.Image = Resources.ImageAcquistionTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[0]).DropDownItems.Add("SDK_康耐视");
                                toolStripItem1.Image = Resources.ImageAcquistionTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[0]).DropDownItems.Add("SDK_灰点");
                                toolStripItem1.Image = Resources.ImageAcquistionTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[0]).DropDownItems.Add("SDK_迈德威视");
                                toolStripItem1.Image = Resources.ImageAcquistionTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[0]).DropDownItems.Add("SDK_海康威视");
                                toolStripItem1.Image = Resources.ImageAcquistionTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "Match" : "匹配 ");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[1]).DropDownItems.Add("形状匹配");
                                toolStripItem1.Image = Resources.ShapeMatchTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[1]).DropDownItems.Add("灰度匹配");
                                toolStripItem1.Image = Resources.GrayMatchTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "CoorTrans" : "坐标变换");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[2]).DropDownItems.Add("手眼标定");
                                toolStripItem1.Image = Resources.EyeHandCalibrationTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "Calibration" : "标定");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[3]).DropDownItems.Add("圆标定");
                                toolStripItem1.Image = Resources.UnknownTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[3]).DropDownItems.Add("正方形标定");
                                toolStripItem1.Image = Resources.UnknownTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "Detection" : "检测");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[4]).DropDownItems.Add("减图像");
                                toolStripItem1.Image = Resources.SubImageTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "ThresholdSegment" : "阈值分割");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[5]).DropDownItems.Add("斑点分析");
                                toolStripItem1.Image = Resources.BlobAnalyseTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "RobotAlign" : "机械手定位");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[6]).DropDownItems.Add("机械手下相机定位");
                                toolStripItem1.Image = Resources.DownCameraAlignTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[6]).DropDownItems.Add("上相机定位工具(手眼一体)");
                                toolStripItem1.Image = Resources.UnknownTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[6]).DropDownItems.Add("上相机定位工具(手眼分离)");
                                toolStripItem1.Image = Resources.UnknownTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "ImageConvert" : "图像转化");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[7]).DropDownItems.Add("彩图转RGB图");
                                toolStripItem1.Image = Resources.ColorToGrayTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "FindAndFit" : "查找与拟合 ");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[8]).DropDownItems.Add("找边");
                                toolStripItem1.Image = Resources.FindLineTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[8]).DropDownItems.Add("找圆");
                                toolStripItem1.Image = Resources.FindCircleTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[8]).DropDownItems.Add("拟合线");
                                toolStripItem1.Image = Resources.FitLineTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[8]).DropDownItems.Add("拟合圆");
                                toolStripItem1.Image = Resources.FitCircleTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "Measurement" : "测量");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[9]).DropDownItems.Add("点点距离");
                                toolStripItem1.Image = Resources.DistancePPTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[9]).DropDownItems.Add("点线距离");
                                toolStripItem1.Image = Resources.DistancePLTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[9]).DropDownItems.Add("线线角度");
                                toolStripItem1.Image = Resources.AngleLLTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[9]).DropDownItems.Add("线线距离");
                                toolStripItem1.Image = Resources.DistanceLLTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "Identity" : "识别");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[10]).DropDownItems.Add("OCR");
                                toolStripItem1.Image = Resources.OCRTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[10]).DropDownItems.Add("OCV");
                                toolStripItem1.Image = Resources.UnknownTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[10]).DropDownItems.Add("条码");
                                toolStripItem1.Image = Resources.BarCodeTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[10]).DropDownItems.Add("二维码");
                                toolStripItem1.Image = Resources.QRTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "Operation" : "运算");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[11]).DropDownItems.Add("算术");
                                toolStripItem1.Image = Resources.UnknownTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "Light" : "光源");
                            {
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[12]).DropDownItems.Add("奥普特光源控制");
                                toolStripItem1.Image = Resources.UnknownTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[12]).DropDownItems.Add("康视达光源控制");
                                toolStripItem1.Image = Resources.UnknownTool;
                                toolStripItem1.Click += InsertTool;
                                toolStripItem1 = ((ToolStripMenuItem)((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems[12]).DropDownItems.Add("乐视光源控制");
                                toolStripItem1.Image = Resources.UnknownTool;
                                toolStripItem1.Click += InsertTool;
                            }
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "3D" : "3D");
                            toolStripItem1.Image = Resources.UnknownTool;
                            toolStripItem1.Click += InsertTool;
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "CodeEdit" : "脚本编辑");
                            toolStripItem1.Image = Resources.CodeEditTool;
                            toolStripItem1.Click += InsertTool;
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "Label" : "标签");
                            toolStripItem1.Image = Resources.LabelTool;
                            toolStripItem1.Click += InsertTool;
                            toolStripItem1 = ((ToolStripMenuItem)rightClickMenu.Items[3]).DropDownItems.Add(Configuration.language == Language.English ? "Output" : "输出");
                            toolStripItem1.Image = Resources.OutputTool;
                            toolStripItem1.Click += InsertTool;
                        });
                        th.IsBackground = true;
                        th.Start();
                    }
                    // rightClickMenu.Show();
                    #endregion

                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 流程树的双击事件
        /// </summary>
        internal void TVW_DoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //判断是否在节点上双击
                TreeViewHitTestInfo test = Project.GetJobTree(jobName).HitTest(e.X, e.Y);
                if (test.Node == null || test.Location != TreeViewHitTestLocations.Label)       //双击节点
                {
                    if (jobTreeFold)
                    {
                        Project.GetJobTree(jobName).ExpandAll();
                        jobTreeFold = false;
                    }
                    else
                    {
                        Project.GetJobTree(jobName).CollapseAll();
                        jobTreeFold = true;
                    }
                    return;
                }

                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                Frm_Main.ignore = true;
                string toolName = Project.GetJobTree(jobName).SelectedNode.Text;

                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == toolName)
                    {

                        switch (L_toolList[i].toolType)
                        {
                            #region HalconInterface
                            case ToolType.HalconInterface:
                                Frm_HalconInterfaceTool.Instance.Text = "Halcon采集接口 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_HalconInterfaceTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_HalconInterfaceTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_HalconInterfaceTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_HalconInterfaceTool.Instance.TopMost = true;
                                Frm_HalconInterfaceTool.Instance.jobName = this.jobName;
                                Frm_HalconInterfaceTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_HalconInterfaceTool.halconInterfaceTool = ((HalconInterfaceTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_ReadFromLocal.Instance.jobName = this.jobName;
                                Frm_ReadFromLocal.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDevice.Instance.jobName = this.jobName;
                                Frm_AcqFromDevice.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDevice.halconInterfaceTool = ((HalconInterfaceTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_HalconInterfaceTool.Instance.Show();
                                Frm_HalconInterfaceTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_AcqFromDevice.Instance.btn_runHalconInterfaceTool.Focus();
                                HalconInterfaceTool halconInterfaceTool = (HalconInterfaceTool)(L_toolList[i].tool);
                                halconInterfaceTool.jobName = this.jobName;
                                Frm_ReadFromLocal.halconInterfaceTool = halconInterfaceTool;
                                Application.DoEvents();

                                //当前图像显示
                                if (halconInterfaceTool.outputImage != null)
                                    halconInterfaceTool.ShowImage(this.jobName, halconInterfaceTool.outputImage);
                                else
                                    halconInterfaceTool.ClearWindow(this.jobName);

                                if (((HalconInterfaceTool)(L_toolList[i].tool)).imageSourceMode == ImageSourceMode.FormDevice)
                                {
                                    Frm_HalconInterfaceTool.Instance.tsb_realTimeDisplay.Enabled = true;
                                    Frm_HalconInterfaceTool.Instance.btn_fromDevice.BackColor = Color.LimeGreen;
                                    Frm_HalconInterfaceTool.Instance.btn_fromLocal.BackColor = Color.LightGray;
                                    Frm_HalconInterfaceTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_AcqFromDevice.Instance.TopLevel = false;
                                    Frm_AcqFromDevice.Instance.Parent = Frm_HalconInterfaceTool.Instance.pnl_formBox;
                                    Frm_AcqFromDevice.Instance.Dock = DockStyle.Fill;
                                    Frm_AcqFromDevice.Instance.Show();
                                    Frm_AcqFromDevice.Instance.btn_runHalconInterfaceTool.Focus();
                                }
                                else
                                {
                                    Frm_HalconInterfaceTool.Instance.tsb_realTimeDisplay.Enabled = false;
                                    Frm_HalconInterfaceTool.Instance.btn_fromLocal.BackColor = Color.LimeGreen;
                                    Frm_HalconInterfaceTool.Instance.btn_fromDevice.BackColor = Color.LightGray;
                                    Frm_HalconInterfaceTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_ReadFromLocal.Instance.TopLevel = false;
                                    Frm_ReadFromLocal.Instance.Parent = Frm_HalconInterfaceTool.Instance.pnl_formBox;
                                    Frm_ReadFromLocal.Instance.Dock = DockStyle.Fill;
                                    Frm_ReadFromLocal.Instance.Show();
                                    Frm_ReadFromLocal.Instance.btn_runHalconInterfaceTool.Focus();
                                }

                                //将对象信息更新到界面
                                Frm_HalconInterfaceTool.Instance.ckb_halconInterfaceToolEnable.Checked = L_toolList[i].enable;
                                if (halconInterfaceTool.deviceInfoStr == string.Empty)
                                    Frm_AcqFromDevice.Instance.lbl_exposureRange.Text = "曝光范围：" + "0 ~ 0";
                                else
                                    Frm_AcqFromDevice.Instance.lbl_exposureRange.Text = "曝光范围：" + Camera.Get_Min_Exposure(halconInterfaceTool.deviceInfoStr) + " ~ " + Camera.Get_Max_Exposure(halconInterfaceTool.deviceInfoStr);

                                Frm_AcqFromDevice.Instance.tkb_exposure.Minimum = Camera.Get_Min_Exposure(halconInterfaceTool.deviceInfoStr);
                                Frm_AcqFromDevice.Instance.tkb_exposure.Maximum = Camera.Get_Max_Exposure(halconInterfaceTool.deviceInfoStr);
                                Frm_AcqFromDevice.Instance.ckb_RGBToGray.Checked = halconInterfaceTool.RGBToGray;
                                Frm_AcqFromDevice.Instance.cbx_deviceList.Text = halconInterfaceTool.deviceInfoStr;
                                Frm_ReadFromLocal.Instance.rdo_readOneImage.Checked = halconInterfaceTool.workMode == WorkMode.ReadOneImage ? true : false;
                                Frm_ReadFromLocal.Instance.rdo_readMultImage.Checked = halconInterfaceTool.workMode == WorkMode.ReadOneImage ? false : true;
                                Frm_ReadFromLocal.Instance.lbl_imageName.Text = halconInterfaceTool.currentImageName;
                                Frm_ReadFromLocal.Instance.ckb_autoSwitch.Checked = halconInterfaceTool.autoSwitch;
                                Frm_ReadFromLocal.Instance.tbx_imagePath.Text = halconInterfaceTool.imagePath;
                                Frm_ReadFromLocal.Instance.tbx_imageDirectory.Text = halconInterfaceTool.imageDirectoryPath;
                                break;
                            #endregion

                            #region SDK_Basler
                            case ToolType.SDK_Basler:
                                Frm_SDK_BaslerTool.Instance.Text = Configuration.language == Language.English ? "SDK_Basler - " : "SDK_巴斯勒 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_SDK_BaslerTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_SDK_BaslerTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_SDK_BaslerTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_SDK_BaslerTool.Instance.TopMost = true;
                                Frm_SDK_BaslerTool.Instance.jobName = this.jobName;
                                Frm_SDK_BaslerTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_SDK_BaslerTool.SDK_baslerTool = ((SDK_BaslerTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_ReadFromLocalBasler.Instance.jobName = this.jobName;
                                Frm_ReadFromLocalBasler.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceBasler.Instance.jobName = this.jobName;
                                Frm_AcqFromDeviceBasler.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceBasler.SDK_baslerTool = ((SDK_BaslerTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_SDK_BaslerTool.Instance.Show();
                                Frm_SDK_BaslerTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_AcqFromDeviceBasler.Instance.btn_runSDKBaslerTool.Focus();
                                SDK_BaslerTool SDK_baslerTool = (SDK_BaslerTool)(L_toolList[i].tool);
                                Frm_ReadFromLocalBasler.SDK_baslerTool = SDK_baslerTool;
                                SDK_baslerTool.jobName = this.jobName;
                                Application.DoEvents();

                                //当前图像显示
                                if (SDK_baslerTool.outputImage != null)
                                    SDK_baslerTool.ShowImage(this.jobName, SDK_baslerTool.outputImage);
                                else
                                    SDK_baslerTool.ClearWindow(this.jobName);

                                if (((SDK_BaslerTool)(L_toolList[i].tool)).imageSourceMode == ImageSourceMode.FormDevice)
                                {
                                    Frm_SDK_BaslerTool.Instance.tsb_realTimeDisplay.Enabled = true;
                                    Frm_SDK_BaslerTool.Instance.btn_fromDevice.BackColor = Color.LimeGreen;
                                    Frm_SDK_BaslerTool.Instance.btn_fromLocal.BackColor = Color.LightGray;
                                    Frm_SDK_BaslerTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_AcqFromDeviceBasler.Instance.TopLevel = false;
                                    Frm_AcqFromDeviceBasler.Instance.Parent = Frm_SDK_BaslerTool.Instance.pnl_formBox;
                                    Frm_AcqFromDeviceBasler.Instance.Dock = DockStyle.Fill;
                                    Frm_AcqFromDeviceBasler.Instance.Show();
                                    Frm_AcqFromDeviceBasler.Instance.btn_runSDKBaslerTool.Focus();
                                }
                                else
                                {
                                    Frm_SDK_BaslerTool.Instance.tsb_realTimeDisplay.Enabled = false;
                                    Frm_SDK_BaslerTool.Instance.btn_fromLocal.BackColor = Color.LimeGreen;
                                    Frm_SDK_BaslerTool.Instance.btn_fromDevice.BackColor = Color.LightGray;
                                    Frm_SDK_BaslerTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_ReadFromLocalBasler.Instance.TopLevel = false;
                                    Frm_ReadFromLocalBasler.Instance.Parent = Frm_SDK_BaslerTool.Instance.pnl_formBox;
                                    Frm_ReadFromLocalBasler.Instance.Dock = DockStyle.Fill;
                                    Frm_ReadFromLocalBasler.Instance.Show();
                                    Frm_ReadFromLocalBasler.Instance.btn_runSDKBaslerTool.Focus();
                                }

                                //将对象信息更新到界面
                                Frm_SDK_BaslerTool.Instance.ckb_SDKBaslerToolEnable.Checked = L_toolList[i].enable;
                                if (SDK_baslerTool.deviceInfoStr == string.Empty)
                                    Frm_AcqFromDeviceBasler.Instance.lbl_exposureRange.Text = "曝光范围：" + "0 ~ 0";
                                else
                                    Frm_AcqFromDeviceBasler.Instance.lbl_exposureRange.Text = "曝光范围：" + Camera.Get_Min_Exposure(SDK_baslerTool.deviceInfoStr) + " ~ " + Camera.Get_Max_Exposure(SDK_baslerTool.deviceInfoStr);

                                Frm_AcqFromDeviceBasler.Instance.tkb_exposure.Minimum = Camera.Get_Min_Exposure(SDK_baslerTool.deviceInfoStr);
                                Frm_AcqFromDeviceBasler.Instance.tkb_exposure.Maximum = Camera.Get_Max_Exposure(SDK_baslerTool.deviceInfoStr);
                                Frm_AcqFromDeviceBasler.Instance.ckb_RGBToGray.Checked = SDK_baslerTool.RGBToGray;
                                Frm_AcqFromDeviceBasler.Instance.cbx_deviceList.Text = SDK_baslerTool.deviceInfoStr;
                                Frm_ReadFromLocalBasler.Instance.rdo_readOneImage.Checked = SDK_baslerTool.workMode == WorkMode.ReadOneImage ? true : false;
                                Frm_ReadFromLocalBasler.Instance.rdo_readMultImage.Checked = SDK_baslerTool.workMode == WorkMode.ReadOneImage ? false : true;
                                Frm_ReadFromLocalBasler.Instance.lbl_imageName.Text = SDK_baslerTool.currentImageName;
                                Frm_ReadFromLocalBasler.Instance.ckb_autoSwitch.Checked = SDK_baslerTool.autoSwitch;
                                Frm_ReadFromLocalBasler.Instance.tbx_imagePath.Text = SDK_baslerTool.imagePath;
                                Frm_ReadFromLocalBasler.Instance.tbx_imageDirectory.Text = SDK_baslerTool.imageDirectoryPath;
                                break;
                            #endregion

                            #region SDK_Cognex
                            case ToolType.SDK_Congex:
                                Frm_SDK_CognexTool.Instance.Text = Configuration.language == Language.English ? "SDK_Basler - " : "SDK_康耐视 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_SDK_CognexTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_SDK_CognexTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_SDK_CognexTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_SDK_CognexTool.Instance.TopMost = true;
                                Frm_SDK_CognexTool.Instance.jobName = this.jobName;
                                Frm_SDK_CognexTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_SDK_CognexTool.SDK_congexTool = ((SDK_CongexTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_ReadFromLocalCognex.Instance.jobName = this.jobName;
                                Frm_ReadFromLocalCognex.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceCognex.Instance.jobName = this.jobName;
                                Frm_AcqFromDeviceCognex.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceCognex.SDK_congexTool = ((SDK_CongexTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_SDK_CognexTool.Instance.Show();
                                Frm_SDK_CognexTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_AcqFromDeviceCognex.Instance.btn_runSDKCognexTool.Focus();
                                SDK_CongexTool SDK_congexTool = (SDK_CongexTool)(L_toolList[i].tool);
                                Frm_ReadFromLocalCognex.SDK_congexTool = SDK_congexTool;
                                SDK_congexTool.jobName = this.jobName;
                                Application.DoEvents();

                                //当前图像显示
                                if (SDK_congexTool.outputImage != null)
                                    SDK_congexTool.ShowImage(this.jobName, SDK_congexTool.outputImage);
                                else
                                    SDK_congexTool.ClearWindow(this.jobName);

                                if (((SDK_CongexTool)(L_toolList[i].tool)).imageSourceMode == ImageSourceMode.FormDevice)
                                {
                                    Frm_SDK_CognexTool.Instance.tsb_realTimeDisplay.Enabled = true;
                                    Frm_SDK_CognexTool.Instance.btn_fromDevice.BackColor = Color.LimeGreen;
                                    Frm_SDK_CognexTool.Instance.btn_fromLocal.BackColor = Color.LightGray;
                                    Frm_SDK_CognexTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_AcqFromDeviceCognex.Instance.TopLevel = false;
                                    Frm_AcqFromDeviceCognex.Instance.Parent = Frm_SDK_CognexTool.Instance.pnl_formBox;
                                    Frm_AcqFromDeviceCognex.Instance.Dock = DockStyle.Fill;
                                    Frm_AcqFromDeviceCognex.Instance.Show();
                                    Frm_AcqFromDeviceCognex.Instance.btn_runSDKCognexTool.Focus();
                                }
                                else
                                {
                                    Frm_SDK_CognexTool.Instance.tsb_realTimeDisplay.Enabled = false;
                                    Frm_SDK_CognexTool.Instance.btn_fromLocal.BackColor = Color.LimeGreen;
                                    Frm_SDK_CognexTool.Instance.btn_fromDevice.BackColor = Color.LightGray;
                                    Frm_SDK_CognexTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_ReadFromLocalCognex.Instance.TopLevel = false;
                                    Frm_ReadFromLocalCognex.Instance.Parent = Frm_SDK_CognexTool.Instance.pnl_formBox;
                                    Frm_ReadFromLocalCognex.Instance.Dock = DockStyle.Fill;
                                    Frm_ReadFromLocalCognex.Instance.Show();
                                    Frm_ReadFromLocalCognex.Instance.btn_runSDKCognexTool.Focus();
                                }

                                //将对象信息更新到界面
                                Frm_SDK_CognexTool.Instance.ckb_SDKCognexToolEnable.Checked = L_toolList[i].enable;
                                if (SDK_congexTool.deviceInfoStr == string.Empty)
                                    Frm_AcqFromDeviceCognex.Instance.lbl_exposureRange.Text = "曝光范围：" + "0 ~ 0";
                                else
                                    Frm_AcqFromDeviceCognex.Instance.lbl_exposureRange.Text = "曝光范围：" + Camera.Get_Min_Exposure(SDK_congexTool.deviceInfoStr) + " ~ " + Camera.Get_Max_Exposure(SDK_congexTool.deviceInfoStr);

                                Frm_AcqFromDeviceCognex.Instance.tkb_exposure.Minimum = Camera.Get_Min_Exposure(SDK_congexTool.deviceInfoStr);
                                Frm_AcqFromDeviceCognex.Instance.tkb_exposure.Maximum = Camera.Get_Max_Exposure(SDK_congexTool.deviceInfoStr);
                                Frm_AcqFromDeviceCognex.Instance.ckb_RGBToGray.Checked = SDK_congexTool.RGBToGray;
                                Frm_AcqFromDeviceCognex.Instance.cbx_deviceList.Text = SDK_congexTool.deviceInfoStr;
                                Frm_ReadFromLocalCognex.Instance.rdo_readOneImage.Checked = SDK_congexTool.workMode == WorkMode.ReadOneImage ? true : false;
                                Frm_ReadFromLocalCognex.Instance.rdo_readMultImage.Checked = SDK_congexTool.workMode == WorkMode.ReadOneImage ? false : true;
                                Frm_ReadFromLocalCognex.Instance.lbl_imageName.Text = SDK_congexTool.currentImageName;
                                Frm_ReadFromLocalCognex.Instance.ckb_autoSwitch.Checked = SDK_congexTool.autoSwitch;
                                Frm_ReadFromLocalCognex.Instance.ckb_RGBToGray.Checked = SDK_congexTool.RGBToGray;
                                Frm_ReadFromLocalCognex.Instance.tbx_imagePath.Text = SDK_congexTool.imagePath;
                                Frm_ReadFromLocalCognex.Instance.tbx_imageDirectory.Text = SDK_congexTool.imageDirectoryPath;
                                break;
                            #endregion

                            #region SDK_PointGray
                            case ToolType.SDK_PointGray:
                                Frm_SDK_PointGrayTool.Instance.Text = Configuration.language == Language.English ? "SDK_PointGray - " : "SDK_灰点 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_SDK_PointGrayTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_SDK_PointGrayTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_SDK_PointGrayTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_SDK_PointGrayTool.Instance.TopMost = true;
                                Frm_SDK_PointGrayTool.Instance.jobName = this.jobName;
                                Frm_SDK_PointGrayTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_SDK_PointGrayTool.SDK_pointGrayTool = ((SDK_PointGrayTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_ReadFromLocalPointGray.Instance.jobName = this.jobName;
                                Frm_ReadFromLocalPointGray.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDevicePointGray.Instance.jobName = this.jobName;
                                Frm_AcqFromDevicePointGray.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDevicePointGray.SDK_pointGrayTool = ((SDK_PointGrayTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_SDK_PointGrayTool.Instance.Show();
                                Frm_SDK_PointGrayTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_AcqFromDevicePointGray.Instance.btn_runSDKPointGrayTool.Focus();
                                SDK_PointGrayTool SDK_pointGrayTool = (SDK_PointGrayTool)(L_toolList[i].tool);
                                Frm_ReadFromLocalPointGray.SDK_pointGrayTool = SDK_pointGrayTool;
                                SDK_pointGrayTool.jobName = this.jobName;
                                Application.DoEvents();

                                //当前图像显示
                                if (SDK_pointGrayTool.outputImage != null)
                                    SDK_pointGrayTool.ShowImage(this.jobName, SDK_pointGrayTool.outputImage);
                                else
                                    SDK_pointGrayTool.ClearWindow(this.jobName);

                                if (((SDK_PointGrayTool)(L_toolList[i].tool)).imageSourceMode == ImageSourceMode.FormDevice)
                                {
                                    Frm_SDK_PointGrayTool.Instance.tsb_realTimeDisplay.Enabled = true;
                                    Frm_SDK_PointGrayTool.Instance.btn_fromDevice.BackColor = Color.LimeGreen;
                                    Frm_SDK_PointGrayTool.Instance.btn_fromLocal.BackColor = Color.LightGray;
                                    Frm_SDK_PointGrayTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_AcqFromDevicePointGray.Instance.TopLevel = false;
                                    Frm_AcqFromDevicePointGray.Instance.Parent = Frm_SDK_PointGrayTool.Instance.pnl_formBox;
                                    Frm_AcqFromDevicePointGray.Instance.Dock = DockStyle.Fill;
                                    Frm_AcqFromDevicePointGray.Instance.Show();
                                    Frm_AcqFromDevicePointGray.Instance.btn_runSDKPointGrayTool.Focus();
                                }
                                else
                                {
                                    Frm_SDK_PointGrayTool.Instance.tsb_realTimeDisplay.Enabled = false;
                                    Frm_SDK_PointGrayTool.Instance.btn_fromLocal.BackColor = Color.LimeGreen;
                                    Frm_SDK_PointGrayTool.Instance.btn_fromDevice.BackColor = Color.LightGray;
                                    Frm_SDK_PointGrayTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_AcqFromDevicePointGray.Instance.TopLevel = false;
                                    Frm_AcqFromDevicePointGray.Instance.Parent = Frm_SDK_PointGrayTool.Instance.pnl_formBox;
                                    Frm_AcqFromDevicePointGray.Instance.Dock = DockStyle.Fill;
                                    Frm_AcqFromDevicePointGray.Instance.Show();
                                    Frm_AcqFromDevicePointGray.Instance.btn_runSDKPointGrayTool.Focus();
                                }

                                //将对象信息更新到界面
                                Frm_SDK_PointGrayTool.Instance.ckb_SDKPointGrayToolEnable.Checked = L_toolList[i].enable;
                                if (SDK_pointGrayTool.deviceDescriptionStr == string.Empty)
                                    Frm_AcqFromDevicePointGray.Instance.lbl_exposureRange.Text = "曝光范围：" + "0 ~ 0";
                                else
                                    Frm_AcqFromDevicePointGray.Instance.lbl_exposureRange.Text = "曝光范围：" + Camera.Get_Min_Exposure(SDK_pointGrayTool.deviceDescriptionStr) + " ~ " + Camera.Get_Max_Exposure(SDK_pointGrayTool.deviceDescriptionStr);

                                Frm_AcqFromDevicePointGray.Instance.tkb_exposure.Minimum = Camera.Get_Min_Exposure(SDK_pointGrayTool.deviceDescriptionStr);
                                Frm_AcqFromDevicePointGray.Instance.tkb_exposure.Maximum = Camera.Get_Max_Exposure(SDK_pointGrayTool.deviceDescriptionStr);
                                Frm_AcqFromDevicePointGray.Instance.ckb_RGBToGray.Checked = SDK_pointGrayTool.RGBToGray;
                                Frm_AcqFromDevicePointGray.Instance.cbx_deviceList.Text = SDK_pointGrayTool.deviceDescriptionStr;
                                Frm_ReadFromLocalPointGray.Instance.rdo_readOneImage.Checked = SDK_pointGrayTool.workMode == WorkMode.ReadOneImage ? true : false;
                                Frm_ReadFromLocalPointGray.Instance.rdo_readMultImage.Checked = SDK_pointGrayTool.workMode == WorkMode.ReadOneImage ? false : true;
                                Frm_ReadFromLocalPointGray.Instance.lbl_imageName.Text = SDK_pointGrayTool.currentImageName;
                                Frm_ReadFromLocalPointGray.Instance.ckb_autoSwitch.Checked = SDK_pointGrayTool.autoSwitch;
                                Frm_ReadFromLocalPointGray.Instance.ckb_RGBToGray.Checked = SDK_pointGrayTool.RGBToGray;
                                Frm_ReadFromLocalPointGray.Instance.tbx_imagePath.Text = SDK_pointGrayTool.imagePath;
                                Frm_ReadFromLocalPointGray.Instance.tbx_imageDirectory.Text = SDK_pointGrayTool.imageDirectoryPath;
                                break;
                            #endregion

                            #region SDK_IMAVision
                            case ToolType.SDK_IMAVision:
                                Frm_SDK_IMAVisionTool.Instance.Text = Configuration.language == Language.English ? "SDK_PointGray - " : "SDK_大恒 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_SDK_IMAVisionTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_SDK_IMAVisionTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_SDK_IMAVisionTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_SDK_IMAVisionTool.Instance.TopMost = true;
                                Frm_SDK_IMAVisionTool.Instance.jobName = this.jobName;
                                Frm_SDK_IMAVisionTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_SDK_IMAVisionTool.SDK_imaVisionTool = ((SDK_IMAVisionTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_ReadFromLocalIMAVision.Instance.jobName = this.jobName;
                                Frm_ReadFromLocalIMAVision.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceIMAVision.Instance.jobName = this.jobName;
                                Frm_AcqFromDeviceIMAVision.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceIMAVision.SDK_imaVisionTool = ((SDK_IMAVisionTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_SDK_IMAVisionTool.Instance.Show();
                                Frm_SDK_IMAVisionTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_AcqFromDeviceIMAVision.Instance.btn_runIMAVisionTool.Focus();
                                SDK_IMAVisionTool SDK_imaVisionTool = (SDK_IMAVisionTool)(L_toolList[i].tool);
                                Frm_ReadFromLocalIMAVision.SDK_imaVisionTool = SDK_imaVisionTool;
                                SDK_imaVisionTool.jobName = this.jobName;
                                Application.DoEvents();

                                //当前图像显示
                                if (SDK_imaVisionTool.outputImage != null)
                                    SDK_imaVisionTool.ShowImage(this.jobName, SDK_imaVisionTool.outputImage);
                                else
                                    SDK_imaVisionTool.ClearWindow(this.jobName);

                                if (((SDK_IMAVisionTool)(L_toolList[i].tool)).imageSourceMode == ImageSourceMode.FormDevice)
                                {
                                    Frm_SDK_IMAVisionTool.Instance.tsb_realTimeDisplay.Enabled = true;
                                    Frm_SDK_IMAVisionTool.Instance.btn_fromDevice.BackColor = Color.LimeGreen;
                                    Frm_SDK_IMAVisionTool.Instance.btn_fromLocal.BackColor = Color.LightGray;
                                    Frm_SDK_IMAVisionTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_AcqFromDeviceIMAVision.Instance.TopLevel = false;
                                    Frm_AcqFromDeviceIMAVision.Instance.Parent = Frm_SDK_IMAVisionTool.Instance.pnl_formBox;
                                    Frm_AcqFromDeviceIMAVision.Instance.Dock = DockStyle.Fill;
                                    Frm_AcqFromDeviceIMAVision.Instance.Show();
                                    Frm_AcqFromDeviceIMAVision.Instance.btn_runIMAVisionTool.Focus();
                                }
                                else
                                {
                                    Frm_SDK_IMAVisionTool.Instance.tsb_realTimeDisplay.Enabled = false;
                                    Frm_SDK_IMAVisionTool.Instance.btn_fromLocal.BackColor = Color.LimeGreen;
                                    Frm_SDK_IMAVisionTool.Instance.btn_fromDevice.BackColor = Color.LightGray;
                                    Frm_SDK_IMAVisionTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_AcqFromDeviceIMAVision.Instance.TopLevel = false;
                                    Frm_AcqFromDeviceIMAVision.Instance.Parent = Frm_SDK_IMAVisionTool.Instance.pnl_formBox;
                                    Frm_AcqFromDeviceIMAVision.Instance.Dock = DockStyle.Fill;
                                    Frm_AcqFromDeviceIMAVision.Instance.Show();
                                    Frm_AcqFromDeviceIMAVision.Instance.btn_runIMAVisionTool.Focus();
                                }

                                //将对象信息更新到界面
                                Frm_SDK_IMAVisionTool.Instance.ckb_SDKIMAVisionToolEnable.Checked = L_toolList[i].enable;
                                if (SDK_imaVisionTool.deviceDescriptionStr == string.Empty)
                                    Frm_AcqFromDeviceIMAVision.Instance.lbl_exposureRange.Text = "曝光范围：" + "0 ~ 0";
                                else
                                    Frm_AcqFromDeviceIMAVision.Instance.lbl_exposureRange.Text = "曝光范围：" + Camera.Get_Min_Exposure(SDK_imaVisionTool.deviceDescriptionStr) + " ~ " + Camera.Get_Max_Exposure(SDK_imaVisionTool.deviceDescriptionStr);

                                Frm_AcqFromDeviceIMAVision.Instance.tkb_exposure.Minimum = Camera.Get_Min_Exposure(SDK_imaVisionTool.deviceDescriptionStr);
                                Frm_AcqFromDeviceIMAVision.Instance.tkb_exposure.Maximum = Camera.Get_Max_Exposure(SDK_imaVisionTool.deviceDescriptionStr);
                                Frm_AcqFromDeviceIMAVision.Instance.ckb_RGBToGray.Checked = SDK_imaVisionTool.RGBToGray;
                                Frm_AcqFromDeviceIMAVision.Instance.cbx_deviceList.Text = SDK_imaVisionTool.deviceDescriptionStr;
                                Frm_ReadFromLocalIMAVision.Instance.rdo_readOneImage.Checked = SDK_imaVisionTool.workMode == WorkMode.ReadOneImage ? true : false;
                                Frm_ReadFromLocalIMAVision.Instance.rdo_readMultImage.Checked = SDK_imaVisionTool.workMode == WorkMode.ReadOneImage ? false : true;
                                Frm_ReadFromLocalIMAVision.Instance.lbl_imageName.Text = SDK_imaVisionTool.currentImageName;
                                Frm_ReadFromLocalIMAVision.Instance.ckb_autoSwitch.Checked = SDK_imaVisionTool.autoSwitch;
                                Frm_ReadFromLocalIMAVision.Instance.ckb_RGBToGray.Checked = SDK_imaVisionTool.RGBToGray;
                                Frm_ReadFromLocalIMAVision.Instance.tbx_imagePath.Text = SDK_imaVisionTool.imagePath;
                                Frm_ReadFromLocalIMAVision.Instance.tbx_imageDirectory.Text = SDK_imaVisionTool.imageDirectoryPath;
                                break;
                            #endregion

                            #region SDK_MindVision
                            case ToolType.SDK_MindVision:
                                Frm_SDK_MindVisionTool.Instance.Text = Configuration.language == Language.English ? "SDK_PointGray - " : "SDK_迈德威视 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_SDK_MindVisionTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_SDK_MindVisionTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_SDK_MindVisionTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_SDK_MindVisionTool.Instance.TopMost = true;
                                Frm_SDK_MindVisionTool.Instance.jobName = this.jobName;
                                Frm_SDK_MindVisionTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_SDK_MindVisionTool.SDK_mindVisionTool = ((SDK_MindVisionTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_ReadFromLocalMindVision.Instance.jobName = this.jobName;
                                Frm_ReadFromLocalMindVision.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceMindVision.Instance.jobName = this.jobName;
                                Frm_AcqFromDeviceMindVision.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceMindVision.SDK_mindVisionTool = ((SDK_MindVisionTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_SDK_MindVisionTool.Instance.Show();
                                Frm_SDK_MindVisionTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_AcqFromDeviceMindVision.Instance.btn_runMindVisionTool.Focus();
                                SDK_MindVisionTool SDK_mindVisionTool = (SDK_MindVisionTool)(L_toolList[i].tool);
                                Frm_ReadFromLocalMindVision.SDK_mindVisionTool = SDK_mindVisionTool;
                                SDK_mindVisionTool.jobName = this.jobName;
                                Application.DoEvents();

                                //当前图像显示
                                if (SDK_mindVisionTool.outputImage != null)
                                    SDK_mindVisionTool.ShowImage(this.jobName, SDK_mindVisionTool.outputImage);
                                else
                                    SDK_mindVisionTool.ClearWindow(this.jobName);

                                if (((SDK_MindVisionTool)(L_toolList[i].tool)).imageSourceMode == ImageSourceMode.FormDevice)
                                {
                                    Frm_SDK_MindVisionTool.Instance.tsb_realTimeDisplay.Enabled = true;
                                    Frm_SDK_MindVisionTool.Instance.btn_fromDevice.BackColor = Color.LimeGreen;
                                    Frm_SDK_MindVisionTool.Instance.btn_fromLocal.BackColor = Color.LightGray;
                                    Frm_SDK_MindVisionTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_AcqFromDeviceMindVision.Instance.TopLevel = false;
                                    Frm_AcqFromDeviceMindVision.Instance.Parent = Frm_SDK_MindVisionTool.Instance.pnl_formBox;
                                    Frm_AcqFromDeviceMindVision.Instance.Dock = DockStyle.Fill;
                                    Frm_AcqFromDeviceMindVision.Instance.Show();
                                    Frm_AcqFromDeviceMindVision.Instance.btn_runMindVisionTool.Focus();
                                }
                                else
                                {
                                    Frm_SDK_MindVisionTool.Instance.tsb_realTimeDisplay.Enabled = false;
                                    Frm_SDK_MindVisionTool.Instance.btn_fromLocal.BackColor = Color.LimeGreen;
                                    Frm_SDK_MindVisionTool.Instance.btn_fromDevice.BackColor = Color.LightGray;
                                    Frm_SDK_MindVisionTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_ReadFromLocalMindVision.Instance.TopLevel = false;
                                    Frm_ReadFromLocalMindVision.Instance.Parent = Frm_SDK_MindVisionTool.Instance.pnl_formBox;
                                    Frm_ReadFromLocalMindVision.Instance.Dock = DockStyle.Fill;
                                    Frm_ReadFromLocalMindVision.Instance.Show();
                                    Frm_ReadFromLocalMindVision.Instance.btn_runMindVisionTool.Focus();
                                }

                                //将对象信息更新到界面
                                Frm_SDK_MindVisionTool.Instance.ckb_SDKMindVisionToolEnable.Checked = L_toolList[i].enable;
                                if (SDK_mindVisionTool.deviceDescriptionStr == string.Empty)
                                    Frm_AcqFromDeviceMindVision.Instance.lbl_exposureRange.Text = "曝光范围：" + "0 ~ 0";
                                else
                                    Frm_AcqFromDeviceMindVision.Instance.lbl_exposureRange.Text = "曝光范围：" + Camera.Get_Min_Exposure(SDK_mindVisionTool.deviceDescriptionStr) + " ~ " + Camera.Get_Max_Exposure(SDK_mindVisionTool.deviceDescriptionStr);

                                Frm_AcqFromDeviceMindVision.Instance.tkb_exposure.Minimum = Camera.Get_Min_Exposure(SDK_mindVisionTool.deviceDescriptionStr);
                                Frm_AcqFromDeviceMindVision.Instance.tkb_exposure.Maximum = Camera.Get_Max_Exposure(SDK_mindVisionTool.deviceDescriptionStr);
                                Frm_AcqFromDeviceMindVision.Instance.ckb_RGBToGray.Checked = SDK_mindVisionTool.RGBToGray;
                                Frm_AcqFromDeviceMindVision.Instance.cbx_deviceList.Text = SDK_mindVisionTool.deviceDescriptionStr;
                                Frm_ReadFromLocalMindVision.Instance.rdo_readOneImage.Checked = SDK_mindVisionTool.workMode == WorkMode.ReadOneImage ? true : false;
                                Frm_ReadFromLocalMindVision.Instance.rdo_readMultImage.Checked = SDK_mindVisionTool.workMode == WorkMode.ReadOneImage ? false : true;
                                Frm_ReadFromLocalMindVision.Instance.lbl_imageName.Text = SDK_mindVisionTool.currentImageName;
                                Frm_ReadFromLocalMindVision.Instance.ckb_autoSwitch.Checked = SDK_mindVisionTool.autoSwitch;
                                Frm_ReadFromLocalMindVision.Instance.ckb_RGBToGray.Checked = SDK_mindVisionTool.RGBToGray;
                                Frm_ReadFromLocalMindVision.Instance.tbx_imagePath.Text = SDK_mindVisionTool.imagePath;
                                Frm_ReadFromLocalMindVision.Instance.tbx_imageDirectory.Text = SDK_mindVisionTool.imageDirectoryPath;
                                break;
                            #endregion

                            #region SDK_HIKVision
                            case ToolType.SDK_HIKVision:
                                Frm_SDK_HIKVisionTool.Instance.Text = Configuration.language == Language.English ? "SDK_PointGray - " : "SDK_海康威视 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_SDK_HIKVisionTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_SDK_HIKVisionTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_SDK_HIKVisionTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_SDK_HIKVisionTool.Instance.TopMost = true;
                                Frm_SDK_HIKVisionTool.Instance.jobName = this.jobName;
                                Frm_SDK_HIKVisionTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_SDK_HIKVisionTool.SDK_hikVisionTool = ((SDK_HIKVisionTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_ReadFromLocal6.Instance.jobName = this.jobName;
                                Frm_ReadFromLocal6.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceHIKVision.Instance.jobName = this.jobName;
                                Frm_AcqFromDeviceHIKVision.Instance.toolName = L_toolList[i].toolName;
                                Frm_AcqFromDeviceHIKVision.SDK_hikVisionTool = ((SDK_HIKVisionTool)Job.GetToolByToolName(this.jobName, L_toolList[i].toolName));
                                Frm_SDK_HIKVisionTool.Instance.Show();
                                Frm_SDK_HIKVisionTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_AcqFromDeviceHIKVision.Instance.btn_runSDKHIKVisionTool.Focus();
                                SDK_HIKVisionTool SDK_hikVisionTool = (SDK_HIKVisionTool)(L_toolList[i].tool);
                                Frm_ReadFromLocal6.SDK_hikVisionTool = SDK_hikVisionTool;
                                SDK_hikVisionTool.jobName = this.jobName;
                                Application.DoEvents();

                                //当前图像显示
                                if (SDK_hikVisionTool.outputImage != null)
                                    SDK_hikVisionTool.ShowImage(this.jobName, SDK_hikVisionTool.outputImage);
                                else
                                    SDK_hikVisionTool.ClearWindow(this.jobName);

                                if (((SDK_HIKVisionTool)(L_toolList[i].tool)).imageSourceMode == ImageSourceMode.FormDevice)
                                {
                                    Frm_SDK_HIKVisionTool.Instance.tsb_realTimeDisplay.Enabled = true;
                                    Frm_SDK_HIKVisionTool.Instance.btn_fromDevice.BackColor = Color.LimeGreen;
                                    Frm_SDK_HIKVisionTool.Instance.btn_fromLocal.BackColor = Color.LightGray;
                                    Frm_SDK_HIKVisionTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_AcqFromDeviceHIKVision.Instance.TopLevel = false;
                                    Frm_AcqFromDeviceHIKVision.Instance.Parent = Frm_SDK_HIKVisionTool.Instance.pnl_formBox;
                                    Frm_AcqFromDeviceHIKVision.Instance.Dock = DockStyle.Fill;
                                    Frm_AcqFromDeviceHIKVision.Instance.Show();
                                    Frm_AcqFromDeviceHIKVision.Instance.btn_runSDKHIKVisionTool.Focus();
                                }
                                else
                                {
                                    Frm_SDK_HIKVisionTool.Instance.tsb_realTimeDisplay.Enabled = false;
                                    Frm_SDK_HIKVisionTool.Instance.btn_fromLocal.BackColor = Color.LimeGreen;
                                    Frm_SDK_HIKVisionTool.Instance.btn_fromDevice.BackColor = Color.LightGray;
                                    Frm_SDK_HIKVisionTool.Instance.pnl_formBox.Controls.Clear();
                                    Frm_ReadFromLocal6.Instance.TopLevel = false;
                                    Frm_ReadFromLocal6.Instance.Parent = Frm_SDK_HIKVisionTool.Instance.pnl_formBox;
                                    Frm_ReadFromLocal6.Instance.Dock = DockStyle.Fill;
                                    Frm_ReadFromLocal6.Instance.Show();
                                    Frm_ReadFromLocal6.Instance.btn_runSDKHIKVisionTool.Focus();
                                }

                                //将对象信息更新到界面
                                Frm_SDK_HIKVisionTool.Instance.ckb_HIKVisionToolEnable.Checked = L_toolList[i].enable;
                                if (SDK_hikVisionTool.deviceDescriptionStr == string.Empty)
                                    Frm_AcqFromDeviceHIKVision.Instance.lbl_exposureRange.Text = "曝光范围：" + "0 ~ 0";
                                else
                                    Frm_AcqFromDeviceHIKVision.Instance.lbl_exposureRange.Text = "曝光范围：" + Camera.Get_Min_Exposure(SDK_hikVisionTool.deviceDescriptionStr) + " ~ " + Camera.Get_Max_Exposure(SDK_hikVisionTool.deviceDescriptionStr);

                                Frm_AcqFromDeviceHIKVision.Instance.tkb_exposure.Minimum = Camera.Get_Min_Exposure(SDK_hikVisionTool.deviceDescriptionStr);
                                Frm_AcqFromDeviceHIKVision.Instance.tkb_exposure.Maximum = Camera.Get_Max_Exposure(SDK_hikVisionTool.deviceDescriptionStr);
                                Frm_AcqFromDeviceHIKVision.Instance.ckb_RGBToGray.Checked = SDK_hikVisionTool.RGBToGray;
                                Frm_AcqFromDeviceHIKVision.Instance.cbx_deviceList.Text = SDK_hikVisionTool.deviceDescriptionStr;
                                Frm_ReadFromLocal6.Instance.rdo_readOneImage.Checked = SDK_hikVisionTool.workMode == WorkMode.ReadOneImage ? true : false;
                                Frm_ReadFromLocal6.Instance.rdo_readMultImage.Checked = SDK_hikVisionTool.workMode == WorkMode.ReadOneImage ? false : true;
                                Frm_ReadFromLocal6.Instance.lbl_imageName.Text = SDK_hikVisionTool.currentImageName;
                                Frm_ReadFromLocal6.Instance.ckb_autoSwitch.Checked = SDK_hikVisionTool.autoSwitch;
                                Frm_ReadFromLocal6.Instance.ckb_RGBToGray.Checked = SDK_hikVisionTool.RGBToGray;
                                Frm_ReadFromLocal6.Instance.tbx_imagePath.Text = SDK_hikVisionTool.imagePath;
                                Frm_ReadFromLocal6.Instance.tbx_imageDirectory.Text = SDK_hikVisionTool.imageDirectoryPath;
                                break;
                            #endregion

                            #region ShapeMatch
                            case ToolType.ShapeMatch:
                                Frm_ShapeMatchTool.Instance.Text = "形状匹配 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_ShapeMatchTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_ShapeMatchTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_ShapeMatchTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_ShapeMatchTool.Instance.TopMost = true;
                                Frm_ShapeMatchTool.Instance.jobName = this.jobName;
                                Frm_ShapeMatchTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_ShapeMatchTool.Instance.Show();
                                Frm_ShapeMatchTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_ShapeMatchTool.Instance.btn_runShapeMatchTool.Focus();
                                ShapeMatchTool shapeMatchTool = (ShapeMatchTool)(L_toolList[i].tool);
                                Frm_ShapeMatchTool.shapeMatchTool = shapeMatchTool;
                                shapeMatchTool.jobName = this.jobName;
                                Application.DoEvents();

                                if (shapeMatchTool.inputImage != null)
                                    shapeMatchTool.ShowImage(this.jobName, shapeMatchTool.inputImage);
                                else
                                    shapeMatchTool.ClearWindow(this.jobName);

                                if (shapeMatchTool.SearchRegion != null)
                                {
                                    //////Frm_ImageWindow.Instance.roiController = new ROIController();
                                    //////Frm_ImageWindow.Instance.roiController.ROI_Update += new ROIController.UpdateEventHandler(shapeMatchTool.roiController_ROI_Update);
                                    ////////////////Frm_ImageWindow.Instance.viewController = new HWndCtrl(Frm_ImageWindow.Instance.hwc_imageWindow);
                                    //////Frm_ImageWindow.Instance.viewController.useROIController(Frm_ImageWindow.Instance.roiController);
                                    //////Frm_ImageWindow.Instance.roiController.setDrawColor("green", "red", "blue");
                                    //////Frm_ImageWindow.Instance.viewController.setViewState(HWndCtrl.MODE_VIEW_NONE);
                                    //////Frm_ImageWindow.Instance.roiController.setDrawColor("green", "red", "cyan");
                                    //////double CenterX = 500, CenterY = 500, SizeX = 400, SizeY = 400;
                                    //////Frm_ImageWindow.Instance.roiController.setROIShape(new ROIRectangle1()); //handle 0 MATCH
                                    //////CenterX = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "CenterX"));
                                    //////CenterY = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "CenterY"));
                                    //////SizeX = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "Width"));
                                    //////SizeY = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "Height"));


                                    if (shapeMatchTool.searchRegionType == RegionType.Rectangle1)
                                    {

                                        //  Frm_ImageWindow.Instance.roiController.setROIShape(new ROIRectangle1()); //handle 0 MATCH
                                        //////CenterX = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "CenterX"));
                                        //////CenterY = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "CenterY"));
                                        //////SizeX = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "Width"));
                                        //////SizeY = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "Height"));
                                        ////int height = (int)(shapeMatchTool.searchRegionPoint[2] - shapeMatchTool.searchRegionPoint[0]);
                                        ////int width = (int)(shapeMatchTool.searchRegionPoint[3] - shapeMatchTool.searchRegionPoint[1]);
                                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayROI(shapeMatchTool.regions);
                                        // Frm_ImageWindow.Instance.roiController.CreatROI(shapeMatchTool.searchRegionPoint[1] + width / 2, shapeMatchTool.searchRegionPoint[0] + height / 2, width, height);
                                        ////((ROIRectangle1)Frm_ImageWindow.Instance.roiController.ROIList[0]).CenterDraw = ROIRectangle1.CenterDrawMode.Cross;
                                        ////((ROIRectangle1)Frm_ImageWindow.Instance.roiController.ROIList[0]).Name = "搜索区域";
                                    }
                                    else if (shapeMatchTool.searchRegionType == RegionType.Rectangle2)
                                    {
                                        // Frm_ImageWindow.Instance.roiController.setROIShape(new ROIRectangle2()); //handle 0 MATCH
                                        //////CenterX = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "CenterX"));
                                        //////CenterY = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "CenterY"));
                                        //////SizeX = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "Width"));
                                        //////SizeY = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "Height"));
                                        ////Frm_ImageWindow.Instance.roiController.CreatROI(shapeMatchTool.searchRegionPoint[1], shapeMatchTool.searchRegionPoint[0], (int)shapeMatchTool.searchRegionPoint[3], (int)shapeMatchTool.searchRegionPoint[4], shapeMatchTool.searchRegionPoint[2]);
                                        //////  ((ROIRectangle2)Frm_ImageWindow.Instance.roiController.ROIList[0]).CenterDraw = ROIRectangle1.CenterDrawMode.Cross;
                                        ////((ROIRectangle2)Frm_ImageWindow.Instance.roiController.ROIList[0]).Name = "搜索区域";
                                    }
                                    else if (shapeMatchTool.searchRegionType == RegionType.Circle)
                                    {
                                        //  Frm_ImageWindow.Instance.roiController.setROIShape(new ROICircle()); //handle 0 MATCH
                                        //////CenterX = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "CenterX"));
                                        //////CenterY = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "CenterY"));
                                        //////SizeX = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "Width"));
                                        //////SizeY = Convert.ToDouble(ModelIni.ReadValue("MatchRegion", "Height"));
                                        ////Frm_ImageWindow.Instance.roiController.CreatROI(shapeMatchTool.searchRegionPoint[1], shapeMatchTool.searchRegionPoint[0], (int)shapeMatchTool.searchRegionPoint[2]);
                                        //////  ((ROIRectangle2)Frm_ImageWindow.Instance.roiController.ROIList[0]).CenterDraw = ROIRectangle1.CenterDrawMode.Cross;
                                        ////((ROICircle)Frm_ImageWindow.Instance.roiController.ROIList[0]).Name = "搜索区域";
                                    }



                                    //////HImage image;
                                    //////image = Frm_ImageWindow.Instance.HobjectToHimage(Frm_ImageWindow.currentImage);
                                    //////Frm_ImageWindow.Instance.viewController.addIconicVar(image);
                                    //////Frm_ImageWindow.Instance.viewController.repaint();
                                }

                                //显示模板
                                try
                                {
                                    HTuple row, col, row1, col1;
                                    HOperatorSet.SmallestRectangle1(shapeMatchTool.templateRegion, out row, out col, out row1, out col1);
                                    HObject outRectangle1;
                                    HOperatorSet.GenRectangle1(out outRectangle1, row - 30, col - 30, row1 + 30, col1 + 30);
                                    HObject imageReduced;
                                    HOperatorSet.ReduceDomain(shapeMatchTool.standardImage, outRectangle1, out imageReduced);
                                    HOperatorSet.SetPart(Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow, row - 30, col - 30, row1 + 30, col1 + 30);
                                    HOperatorSet.DispObj(imageReduced, Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow);
                                    HOperatorSet.SetDraw(Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow, new HTuple("margin"));
                                    HOperatorSet.SetColor(Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow, new HTuple("green"));
                                    HOperatorSet.DispObj(shapeMatchTool.templateRegion, Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow);

                                    int statu = shapeMatchTool.Create_Template_With_Standard_Image();
                                    if (statu != 0)
                                        return;
                                    HObject contour;
                                    HOperatorSet.GetShapeModelContours(out contour, shapeMatchTool.modelID, (HTuple)1);
                                    HTuple area1, row2, column2;
                                    HOperatorSet.AreaCenter(shapeMatchTool.templateRegion, out area1, out row2, out column2);
                                    HTuple homMat2D;
                                    HOperatorSet.HomMat2dIdentity(out homMat2D);
                                    HOperatorSet.HomMat2dTranslate(homMat2D, row2, column2, out homMat2D);
                                    HOperatorSet.AffineTransContourXld(contour, out contour, homMat2D);
                                    HOperatorSet.SetColor(Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow, new HTuple("orange"));
                                    HOperatorSet.DispObj(contour, Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow);
                                }
                                catch { }

                                //将对象信息更新到界面
                                Frm_ShapeMatchTool.Instance.ckb_shapeMatchToolEnable.Checked = L_toolList[i].enable;
                                Frm_ShapeMatchTool.Instance.ckb_showCross.Checked = shapeMatchTool.showCross;
                                Frm_ShapeMatchTool.Instance.ckb_showFeature.Checked = shapeMatchTool.showFeature;
                                Frm_ShapeMatchTool.Instance.cbx_shapeMatchSearchRegion.Text = shapeMatchTool.searchRegionType == RegionType.None ? "" : shapeMatchTool.searchRegionType.ToString();
                                Frm_ShapeMatchTool.Instance.nud_minScore.Value = Convert.ToDecimal(shapeMatchTool.minScore);
                                Frm_ShapeMatchTool.Instance.nud_matchNum.Value = Convert.ToDecimal(shapeMatchTool.expectMatchNum);
                                Frm_ShapeMatchTool.Instance.nud_angleStart.Value = Convert.ToDecimal(shapeMatchTool.startAngle);
                                Frm_ShapeMatchTool.Instance.nud_angleRange.Value = Convert.ToDecimal(shapeMatchTool.angleRange);
                                Frm_ShapeMatchTool.Instance.nud_angleStep.Value = Convert.ToDecimal(shapeMatchTool.angleStep);
                                Frm_ShapeMatchTool.Instance.tkb_contrast.Value = Convert.ToInt16(shapeMatchTool.contrast);
                                Frm_ShapeMatchTool.Instance.cbx_polarity.Text = shapeMatchTool.polarity;
                                if (shapeMatchTool.angleStep == 0)
                                {
                                    Frm_ShapeMatchTool.Instance.nud_angleStep.Enabled = false;
                                    Frm_ShapeMatchTool.Instance.ckb_angleStep.Checked = true;
                                }
                                else
                                {
                                    Frm_ShapeMatchTool.Instance.ckb_angleStep.Checked = false;
                                }
                                break;
                            #endregion

                            #region EyeHandCalibration
                            case ToolType.EyeHandCalibration:
                                Frm_EyeHandCalibrationTool.Instance.Text = "手眼标定 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_EyeHandCalibrationTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_EyeHandCalibrationTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_EyeHandCalibrationTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_EyeHandCalibrationTool.Instance.TopMost = true;
                                Frm_EyeHandCalibrationTool.Instance.jobName = this.jobName;
                                Frm_EyeHandCalibrationTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_EyeHandCalibrationTool.Instance.Show();
                                Frm_EyeHandCalibrationTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_EyeHandCalibrationTool.Instance.Focus();
                                EyeHandCalibrationTool eyeHandCalibrationTool = (EyeHandCalibrationTool)(L_toolList[i].tool);
                                Frm_EyeHandCalibrationTool.eyeHandCalibrationTool = eyeHandCalibrationTool;
                                Application.DoEvents();

                                if (eyeHandCalibrationTool.inputImage != null)
                                    eyeHandCalibrationTool.ShowImage(this.jobName, eyeHandCalibrationTool.inputImage);
                                else
                                    eyeHandCalibrationTool.ClearWindow(this.jobName);

                                //将对象信息更新到界面
                                Frm_EyeHandCalibrationTool.Instance.ckb_eyeHandCalibrationToolEnable.Checked = L_toolList[i].enable;
                                Frm_EyeHandCalibrationTool.Instance.tbx_translateX.Text = eyeHandCalibrationTool.TranslateY.ToString();
                                Frm_EyeHandCalibrationTool.Instance.tbx_translateY.Text = eyeHandCalibrationTool.TranslateY.ToString();
                                Frm_EyeHandCalibrationTool.Instance.tbx_scaleX.Text = eyeHandCalibrationTool.ScanX.ToString();
                                Frm_EyeHandCalibrationTool.Instance.tbx_scaleY.Text = eyeHandCalibrationTool.ScanY.ToString();
                                Frm_EyeHandCalibrationTool.Instance.tbx_rotation.Text = eyeHandCalibrationTool.Rotation.ToString();
                                Frm_EyeHandCalibrationTool.Instance.tbx_theta.Text = eyeHandCalibrationTool.Theta.ToString();
                                Frm_EyeHandCalibrationTool.Instance.cbo_calibrationType.Text = eyeHandCalibrationTool.calibrationType == CalibrationType.Four_Point ? "四点标定" : "九点标定";
                                Application.DoEvents();

                                //显示标定数据
                                for (int j = 0; j < ((EyeHandCalibrationTool)L_toolList[i].tool).L_calibrationData.Count; j++)
                                {
                                    for (int k = 0; k < 4; k++)
                                    {
                                        Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows[j].Cells[k].Value = eyeHandCalibrationTool.L_calibrationData[j][k];
                                    }
                                }
                                break;
                            #endregion

                            #region CircleCalibration
                            case ToolType.CircleCalibration:
                                Frm_CircleCalibrationTool.Instance.Text = "圆标定 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_CircleCalibrationTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_CircleCalibrationTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_CircleCalibrationTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_CircleCalibrationTool.Instance.TopMost = true;
                                Frm_CircleCalibrationTool.Instance.jobName = this.jobName;
                                Frm_CircleCalibrationTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_CircleCalibrationTool.Instance.Show();
                                Frm_CircleCalibrationTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_CircleCalibrationTool.Instance.btn_runFindCircleTool.Focus();
                                CircleCalibrationTool circleCalibrationTool = (CircleCalibrationTool)(L_toolList[i].tool);
                                Frm_CircleCalibrationTool.findCircleTool = circleCalibrationTool;
                                Application.DoEvents();

                                if (circleCalibrationTool.inputImage != null)
                                    circleCalibrationTool.ShowImage(jobName, circleCalibrationTool.inputImage);
                                else
                                    circleCalibrationTool.ClearWindow(jobName);

                                //将对象信息更新到界面
                                Frm_CircleCalibrationTool.Instance.ckb_findCircleToolEnable.Checked = L_toolList[i].enable;

                                Frm_CircleCalibrationTool.Instance.tbx_expectCircelRow.Text = circleCalibrationTool.expectCircleRow.ToString();
                                Frm_CircleCalibrationTool.Instance.tbx_expectCircleCol.Text = circleCalibrationTool.expectCircleCol.ToString();
                                Frm_CircleCalibrationTool.Instance.tbx_expectCircleradius.Text = circleCalibrationTool.expectCircleRadius.ToString();

                                Frm_CircleCalibrationTool.Instance.tbx_ringRadiusLength.Text = circleCalibrationTool.ringRadiusLength.ToString();
                                Frm_CircleCalibrationTool.Instance.tbx_startAngle.Text = circleCalibrationTool.startAngle.ToString();
                                Frm_CircleCalibrationTool.Instance.tbx_endAngle.Text = circleCalibrationTool.endAngle.ToString();
                                Frm_CircleCalibrationTool.Instance.tbx_threshold.Text = circleCalibrationTool.threshold.ToString();
                                Frm_CircleCalibrationTool.Instance.tbx_cliperNum.Text = circleCalibrationTool.cliperNum.ToString();
                                break;
                            #endregion

                            #region SubImage
                            case ToolType.SubImage:
                                Frm_SubImageTool.Instance.Text = "减图像 - " + Frm_Job.Instance.tbc_jobs.SelectedTab.Text + "." + L_toolList[i].toolName;
                                Frm_SubImageTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_SubImageTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_SubImageTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_SubImageTool.Instance.TopMost = true;
                                Frm_SubImageTool.Instance.jobName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
                                Frm_SubImageTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_SubImageTool.Instance.Show();
                                Frm_SubImageTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_SubImageTool.Instance.btn_runImageSubTool.Focus();
                                SubImageTool subImageTool = (SubImageTool)(L_toolList[i].tool);
                                Frm_SubImageTool.subImageTool = subImageTool;
                                Application.DoEvents();

                                if (subImageTool.inputImage != null)
                                    subImageTool.ShowImage(jobName, subImageTool.inputImage);
                                else
                                    subImageTool.ClearWindow(jobName);

                                Frm_SubImageTool.Instance.ckb_subImageToolEnable.Checked = L_toolList[i].enable;
                                Frm_SubImageTool.Instance.cbx_standardImage.Text = subImageTool.standardImageName;
                                break;
                            #endregion

                            #region BlobAnalyse
                            case ToolType.BlobAnalyse:
                                Frm_BlobAnalyseTool.Instance.Text = "斑点分析 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_BlobAnalyseTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_BlobAnalyseTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_BlobAnalyseTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_BlobAnalyseTool.Instance.TopMost = true;
                                Frm_BlobAnalyseTool.Instance.jobName = this.jobName;
                                Frm_BlobAnalyseTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_BlobAnalyseTool.Instance.Show();
                                Frm_BlobAnalyseTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_BlobAnalyseTool.Instance.btn_runBlobAnalyseTool.Focus();
                                BlobAnalyseTool blobAnalyseTool = (BlobAnalyseTool)(L_toolList[i].tool);
                                Frm_BlobAnalyseTool.blobAnalyseTool = blobAnalyseTool;
                                Frm_ProcessingItemConfig.blobAnalyseTool = blobAnalyseTool;
                                Frm_ProcessingItemConfig1.blobAnalyseTool = blobAnalyseTool;
                                Application.DoEvents();

                                if (blobAnalyseTool.inputImage != null)
                                    blobAnalyseTool.ShowImage(jobName, blobAnalyseTool.inputImage);
                                else
                                    blobAnalyseTool.ClearWindow(jobName);
                                if (blobAnalyseTool.SearchRegion != null)
                                {
                                    blobAnalyseTool.SetColor(jobName, "blue");
                                    blobAnalyseTool.SetDraw(jobName, "margin");
                                    blobAnalyseTool.ShowObj(jobName, blobAnalyseTool.SearchRegion);
                                }

                                Frm_BlobAnalyseTool.Instance.dgv_select.Rows.Clear();
                                for (int j = 0; j < blobAnalyseTool.L_select.Count; j++)
                                {
                                    int index = Frm_BlobAnalyseTool.Instance.dgv_select.Rows.Add();
                                    Frm_BlobAnalyseTool.Instance.dgv_select.Rows[index].Cells[0].Value = blobAnalyseTool.L_select[j].SelectType;
                                    Frm_BlobAnalyseTool.Instance.dgv_select.Rows[index].Cells[1].Value = blobAnalyseTool.L_select[j].AreaDownLimit;
                                    Frm_BlobAnalyseTool.Instance.dgv_select.Rows[index].Cells[2].Value = blobAnalyseTool.L_select[j].AreaUpLimit;
                                }


                                //将预处理项更新到窗体
                                Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows.Clear();
                                for (int j = 0; j < blobAnalyseTool.L_prePorcessing.Count; j++)
                                {
                                    int index = Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows.Add();
                                    Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[0].Value = blobAnalyseTool.L_prePorcessing[j].PreProcessingType;
                                    ((DataGridViewCheckBoxCell)Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[1]).Value = blobAnalyseTool.L_prePorcessing[j].Enable;
                                }

                                Frm_BlobAnalyseTool.Instance.ckb_blobAnalyseToolEnable.Checked = L_toolList[i].enable;
                                Frm_BlobAnalyseTool.Instance.ckb_displaySearchRegion.Checked = blobAnalyseTool.displaySearchRegion;
                                Frm_BlobAnalyseTool.Instance.ckb_displayCross.Checked = blobAnalyseTool.displayCross;
                                Frm_BlobAnalyseTool.Instance.tbx_lineWidth.Text = blobAnalyseTool.marginLineWidth.ToString();
                                Frm_BlobAnalyseTool.Instance.rdo_outCircleFillMode.Checked = blobAnalyseTool.outCircleDrawMode == FillMode.Fill ? true : false;
                                Frm_BlobAnalyseTool.Instance.rdo_outCircleMarginMode.Checked = blobAnalyseTool.outCircleDrawMode == FillMode.Fill ? false : true;
                                Frm_BlobAnalyseTool.Instance.rdo_resultRegionFillMode.Checked = blobAnalyseTool.resultRegionDrawMode == FillMode.Fill ? true : false;
                                Frm_BlobAnalyseTool.Instance.rdo_resultRegionMarginMode.Checked = blobAnalyseTool.resultRegionDrawMode == FillMode.Fill ? false : true;
                                Frm_BlobAnalyseTool.Instance.ckb_showResultRegion.Checked = blobAnalyseTool.showResultRegion;
                                Frm_BlobAnalyseTool.Instance.rdo_outCircleFillMode.Checked = blobAnalyseTool.outCircleDrawMode == FillMode.Fill ? true : false;
                                Frm_BlobAnalyseTool.Instance.ckb_showOutCircle.Checked = blobAnalyseTool.showOutCircle;
                                Frm_BlobAnalyseTool.Instance.cbx_blobAnalyseSearchRegion.Text = blobAnalyseTool.searchRegionType.ToString();
                                Frm_BlobAnalyseTool.Instance.nud_minThreshold.Value = Convert.ToDecimal(blobAnalyseTool.minThreshold);
                                Frm_BlobAnalyseTool.Instance.nud_maxThreshold.Value = Convert.ToDecimal(blobAnalyseTool.maxThreshold);
                                Frm_BlobAnalyseTool.Instance.ckb_fillHole.Checked = blobAnalyseTool.fillHole;
                                Frm_BlobAnalyseTool.Instance.rdo_resultRegionFillMode.Checked = blobAnalyseTool.resultRegionDrawMode == FillMode.Fill ? true : false;
                                break;
                            #endregion

                            #region DownCamAlign
                            case ToolType.DownCamAlign:
                                Frm_DownCamAlignTool.Instance.Text = "机械手下相机定位 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_DownCamAlignTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_DownCamAlignTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_DownCamAlignTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_DownCamAlignTool.Instance.TopMost = true;
                                Frm_DownCamAlignTool.Instance.jobName = this.jobName;
                                Frm_DownCamAlignTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_DownCamAlignTool.Instance.Show();
                                Frm_DownCamAlignTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_DownCamAlignTool.Instance.btn_runDownCamAlignTool.Focus();
                                RobotDownCamAlignTool robotDownCamAlignTool = (RobotDownCamAlignTool)(L_toolList[i].tool);
                                Frm_DownCamAlignTool.robotDownCamAlignTool = robotDownCamAlignTool;
                                Application.DoEvents();

                                Frm_DownCamAlignTool.Instance.tbx_inputPosX.Text = robotDownCamAlignTool.inputPos.X.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_inputPosY.Text = robotDownCamAlignTool.inputPos.Y.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_inputPosU.Text = robotDownCamAlignTool.inputPos.U.ToString();

                                Frm_DownCamAlignTool.Instance.tbx_caputurePosX.Text = robotDownCamAlignTool.caputurePos.X.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_caputurePosY.Text = robotDownCamAlignTool.caputurePos.Y.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_caputurePosU.Text = robotDownCamAlignTool.caputurePos.U.ToString();

                                Frm_DownCamAlignTool.Instance.tbx_templateFeatureX.Text = robotDownCamAlignTool.templateFeaturePos.X.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_templateFeatureY.Text = robotDownCamAlignTool.templateFeaturePos.Y.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_templateFeatureU.Text = robotDownCamAlignTool.templateFeaturePos.U.ToString();

                                Frm_DownCamAlignTool.Instance.tbx_targetPosX.Text = robotDownCamAlignTool.targetPos.X.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_targetPosY.Text = robotDownCamAlignTool.targetPos.Y.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_targetPosU.Text = robotDownCamAlignTool.targetPos.U.ToString();

                                Frm_DownCamAlignTool.Instance.tbx_touchPosX.Text = robotDownCamAlignTool.touchPos.X.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_touchPosY.Text = robotDownCamAlignTool.touchPos.Y.ToString();
                                Frm_DownCamAlignTool.Instance.tbx_touchPosU.Text = robotDownCamAlignTool.touchPos.U.ToString();
                                Frm_DownCamAlignTool.Instance.cbx_downCamAlignToolEnable.Checked = L_toolList[i].enable;
                                break;
                            #endregion

                            #region ColorToRGB
                            case ToolType.ColorToRGB:
                                Frm_ColorToRGBTool.Instance.Text = "彩图转RGB图 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_ColorToRGBTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_ColorToRGBTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_ShapeMatchTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_ColorToRGBTool.Instance.TopMost = true;
                                Frm_ColorToRGBTool.Instance.jobName = this.jobName;
                                Frm_ColorToRGBTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_ColorToRGBTool.Instance.Show();
                                Frm_ColorToRGBTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_ColorToRGBTool.Instance.btn_runColorToRGBTool.Focus();
                                ColorToRGBTool colorToRGBTool = (ColorToRGBTool)(L_toolList[i].tool);
                                Frm_ColorToRGBTool.colorToRGBTool = colorToRGBTool;
                                colorToRGBTool.jobName = this.jobName;
                                Application.DoEvents();

                                if (colorToRGBTool.inputImage != null)
                                    colorToRGBTool.ShowImage(this.jobName, colorToRGBTool.inputImage);
                                else
                                    colorToRGBTool.ClearWindow(this.jobName);




                                //将对象信息更新到界面

                                break;
                            #endregion

                            #region FindLine
                            case ToolType.FindLine:
                                Frm_FindLineTool.Instance.Text = "找线 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_FindLineTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_FindLineTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_FindLineTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_FindLineTool.Instance.TopMost = true;
                                Frm_FindLineTool.Instance.jobName = this.jobName;
                                Frm_FindLineTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_FindLineTool.Instance.Show();
                                Frm_FindLineTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_FindLineTool.Instance.btn_runFindLineTool.Focus();
                                FindLineTool findLineTool = (FindLineTool)(L_toolList[i].tool);
                                Frm_FindLineTool.findLineTool = findLineTool;
                                Application.DoEvents();

                                if (findLineTool.inputImage != null)
                                    findLineTool.ShowImage(this.jobName, findLineTool.inputImage);
                                else
                                    findLineTool.ClearWindow(this.jobName);

                                findLineTool.Run(jobName, true, true);         //运行一下，使卡尺显示出来

                                Frm_FindLineTool.Instance.tbx_expectLineStartRow.Text = findLineTool.expectLineStartRow.ToString();
                                Frm_FindLineTool.Instance.tbx_expectLineStartCol.Text = findLineTool.expectLineStartCol.ToString();
                                Frm_FindLineTool.Instance.tbx_expectLineEndRow.Text = findLineTool.expectLineEndRow.ToString();
                                Frm_FindLineTool.Instance.tbx_expectLineEndCol.Text = findLineTool.expectLineEndCol.ToString();
                                Frm_FindLineTool.Instance.cbx_edgeSelect.Text = findLineTool.edgeSelect;
                                Frm_FindLineTool.Instance.tbx_minScore.Text = findLineTool.minScore.ToString();
                                Frm_FindLineTool.Instance.cbx_polarity.Text = findLineTool.polarity == "positive" ? "由明到暗" : "由暗到明";
                                Frm_FindLineTool.Instance.tbx_caliperNum.Text = findLineTool.cliperNum.ToString();
                                Frm_FindLineTool.Instance.tbx_caliperLength.Text = findLineTool.length.ToString();
                                Frm_FindLineTool.Instance.tbx_threshold.Text = findLineTool.threshold.ToString();
                                Frm_FindLineTool.Instance.ckb_updateImage.Checked = findLineTool.updateImage;
                                Frm_FindLineTool.Instance.ckb_findLineToolEnable.Checked = L_toolList[i].enable;
                                break;
                            #endregion

                            #region FindCircle
                            case ToolType.FindCircle:
                                Frm_FindCircleTool.Instance.Text = "找圆 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_FindCircleTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_FindCircleTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_FindCircleTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_FindCircleTool.Instance.TopMost = true;
                                Frm_FindCircleTool.Instance.jobName = this.jobName;
                                Frm_FindCircleTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_FindCircleTool.Instance.Show();
                                Frm_FindCircleTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_FindCircleTool.Instance.btn_runFindCircleTool.Focus();
                                FindCircleTool findCircleTool = (FindCircleTool)(L_toolList[i].tool);
                                Frm_FindCircleTool.findCircleTool = findCircleTool;
                                Application.DoEvents();

                                if (findCircleTool.inputImage != null)
                                    findCircleTool.ShowImage(jobName, findCircleTool.inputImage);
                                else
                                    findCircleTool.ClearWindow(jobName);

                                //将对象信息更新到界面
                                Frm_FindCircleTool.Instance.ckb_findCircleToolEnable.Checked = L_toolList[i].enable;

                                Frm_FindCircleTool.Instance.tbx_expectCircelRow.Text = findCircleTool.expectCircleRow.ToString();
                                Frm_FindCircleTool.Instance.tbx_expectCircleCol.Text = findCircleTool.expectCircleCol.ToString();
                                Frm_FindCircleTool.Instance.tbx_expectCircleradius.Text = findCircleTool.expectCircleRadius.ToString();

                                Frm_FindCircleTool.Instance.tbx_ringRadiusLength.Text = findCircleTool.ringRadiusLength.ToString();
                                Frm_FindCircleTool.Instance.tbx_startAngle.Text = findCircleTool.startAngle.ToString();
                                Frm_FindCircleTool.Instance.tbx_endAngle.Text = findCircleTool.endAngle.ToString();
                                Frm_FindCircleTool.Instance.tbx_threshold.Text = findCircleTool.threshold.ToString();
                                Frm_FindCircleTool.Instance.tbx_cliperNum.Text = findCircleTool.cliperNum.ToString();
                                break;
                            #endregion

                            #region CreateROI
                            case ToolType.CreateROI:
                                Frm_CreateROITool.Instance.Text = "创建ROI - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_CreateROITool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_CreateROITool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_CreateROITool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_CreateROITool.Instance.TopMost = true;
                                Frm_CreateROITool.Instance.jobName = this.jobName;
                                Frm_CreateROITool.Instance.toolName = L_toolList[i].toolName;
                                Frm_CreateROITool.Instance.Show();
                                Frm_CreateROITool.Instance.WindowState = FormWindowState.Normal;
                                //////Frm_CreateROITool.Instance.btn_runFindCircleTool.Focus();
                                CreateROITool createROITool = (CreateROITool)(L_toolList[i].tool);
                                Frm_CreateROITool.createROITool = createROITool;
                                Application.DoEvents();

                                //将对象信息更新到界面
                                Frm_CreateROITool.Instance.ckb_createROIToolEnable.Checked = L_toolList[i].enable;
                                Frm_CreateROITool.Instance.tbx_leftTopRow.Text = createROITool.leftTopRow.ToString();
                                Frm_CreateROITool.Instance.tbx_leftTopCol.Text = createROITool.leftTopCol.ToString();
                                Frm_CreateROITool.Instance.tbx_rightDownRow.Text = createROITool.rightDownRow.ToString();
                                Frm_CreateROITool.Instance.tbx_rightDownCol.Text = createROITool.rightDownCol.ToString();
                                break;
                            #endregion

                            #region DistancePL
                            case ToolType.DistancePL:
                                Frm_DistancePLTool.Instance.Text = "点线距离 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_DistancePLTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_DistancePLTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_DistancePLTool.Instance.Width - 2, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_DistancePLTool.Instance.TopMost = true;
                                Frm_DistancePLTool.Instance.jobName = this.jobName;
                                Frm_DistancePLTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_DistancePLTool.Instance.Show();
                                Frm_DistancePLTool.Instance.WindowState = FormWindowState.Normal;
                                //  Frm_DistancePointLineTool.Instance.dd.Focus();
                                DistancePLTool distancePLTool = (DistancePLTool)(L_toolList[i].tool);
                                Application.DoEvents();

                                if (((DistancePLTool)(L_toolList[i].tool)).inputImage != null)
                                    Frm_ImageWindow.Instance.Display_Image(((DistancePLTool)(L_toolList[i].tool)).inputImage);
                                else
                                    HOperatorSet.ClearWindow(Frm_ImageWindow.Instance.WindowHandle);

                                //将对象信息更新到界面
                                Frm_DistancePLTool.Instance.ckb_distancePLToolEnable.Checked = L_toolList[i].enable;

                                break;
                            #endregion

                            #region DistanceSS
                            case ToolType.DistanceSS:
                                Frm_DistanceLLTool.Instance.Text = "线段与线段距离 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_DistanceLLTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_DistanceLLTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_DistanceLLTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_DistanceLLTool.Instance.TopMost = true;
                                Frm_DistanceLLTool.jobName = this.jobName;
                                Frm_DistanceLLTool.toolName = L_toolList[i].toolName;
                                ////// Frm_DistanceSegmentAndSegmentTool.Instance.Show();
                                Frm_DistanceLLTool.Instance.WindowState = FormWindowState.Normal;
                                //////SharpEdit.Form1.Instance.fctb.Focus();
                                DistanceLLTool distanceSSTool = (DistanceLLTool)(L_toolList[i].tool);
                                Application.DoEvents();

                                //将对象信息更新到界面
                                Frm_MessageBox.Instance.MessageBoxShow("\r\n本工具为无窗体工具！");
                                break;
                            #endregion

                            #region LLPoint
                            case ToolType.LLPoint:
                                Frm_LLPointTool.Instance.Text = "线线交点 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_LLPointTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_LLPointTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_DistanceLLTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_LLPointTool.Instance.TopMost = true;
                                //////Frm_LLPointTool.jobName = this.jobName;
                                //////Frm_LLPointTool.toolName = L_toolList[i].toolName;
                                ////// Frm_DistanceSegmentAndSegmentTool.Instance.Show();
                                Frm_LLPointTool.Instance.WindowState = FormWindowState.Normal;
                                //////SharpEdit.Form1.Instance.fctb.Focus();
                                LLPointTool llPointTool = (LLPointTool)(L_toolList[i].tool);
                                Application.DoEvents();

                                //将对象信息更新到界面
                                Frm_MessageBox.Instance.MessageBoxShow("\r\n本工具为无窗体工具！");
                                break;
                            #endregion

                            #region Barcode
                            case ToolType.Barcode:
                                Frm_BarcodeTool.Instance.Text = "条码 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_BarcodeTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_BarcodeTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_BarcodeTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_BarcodeTool.Instance.TopMost = true;
                                Frm_BarcodeTool.Instance.jobName = this.jobName;
                                Frm_BarcodeTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_BarcodeTool.Instance.Show();
                                Frm_BarcodeTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_BarcodeTool.Instance.btn_runFindBarcodeTool.Focus();
                                BarcodeTool barcodeTool = (BarcodeTool)(L_toolList[i].tool);
                                Frm_BarcodeTool.barcodeTool = barcodeTool;
                                Application.DoEvents();

                                break;
                            #endregion

                            #region RegionFeature
                            case ToolType.RegionFeature:
                                Frm_RegionFeatureTool.Instance.Text = "区域特征 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_RegionFeatureTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_RegionFeatureTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_RegionFeatureTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_RegionFeatureTool.Instance.TopMost = true;
                                Frm_RegionFeatureTool.Instance.jobName = this.jobName;
                                Frm_RegionFeatureTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_RegionFeatureTool.Instance.Show();
                                Frm_RegionFeatureTool.Instance.WindowState = FormWindowState.Normal;
                                //////Frm_RegionFeatureTool.Instance.btn_runFindBarcodeTool.Focus();
                                RegionFeatureTool regionFeatureTool = (RegionFeatureTool)(L_toolList[i].tool);
                                Frm_RegionFeatureTool.regionFeatureTool = regionFeatureTool;
                                Application.DoEvents();

                                break;
                            #endregion

                            #region OCR
                            case ToolType.OCR:
                                Frm_OCRTool.Instance.Text = "OCR - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_OCRTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_OCRTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_ShapeMatchTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_OCRTool.Instance.TopMost = true;
                                Frm_OCRTool.Instance.jobName = this.jobName;
                                Frm_OCRTool.Instance.toolName = L_toolList[i].toolName;
                                Frm_OCRTool.Instance.Show();
                                Frm_OCRTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_OCRTool.Instance.btn_runOCRTool.Focus();
                                OCRTool ocrTool = (OCRTool)(L_toolList[i].tool);
                                Frm_OCRTool.ocrTool = ocrTool;
                                ocrTool.jobName = this.jobName;
                                Application.DoEvents();

                                if (ocrTool.inputImage != null)
                                    ocrTool.ShowImage(this.jobName, ocrTool.inputImage);
                                else
                                    ocrTool.ClearWindow(this.jobName);

                                if (ocrTool.searchRegion != null)
                                {
                                    ocrTool.SetColor(this.jobName, "blue");
                                    ocrTool.ShowObj(this.jobName, ocrTool.searchRegion);
                                }

                                Frm_OCRTool.Instance.lbl_threshold.Text = ocrTool.threshold.ToString();
                                Frm_OCRTool.Instance.tkb_threshold.Value = ocrTool.threshold;
                                Frm_OCRTool.Instance.ckb_OCRToolEnable.Checked = L_toolList[i].enable;
                                Frm_OCRTool.Instance.cbx_searchRegionType.Text = ocrTool.searchRegionType.ToString();
                                Frm_OCRTool.Instance.cbx_templateRegionType.Text = ocrTool.templateRegionType.ToString();
                                Frm_OCRTool.Instance.tbx_resultStr.Text = ocrTool.outputStr;
                                Frm_OCRTool.Instance.cbx_charType.SelectedIndex = (ocrTool.charType == CharType.BlackChar ? 0 : 1);
                                Frm_OCRTool.Instance.tbx_dilationSize.Text = ocrTool.dilationSize.ToString();
                                Frm_OCRTool.Instance.tbx_standardCharList.Text = ocrTool.standardCharList;

                                break;
                            #endregion

                            #region Barcode
                            case ToolType.KeyenceSR1000:
                                Frm_KeyenceSR1000Tool.Instance.Text = "基恩士SR1000 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_KeyenceSR1000Tool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_KeyenceSR1000Tool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_KeyenceSR1000Tool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_KeyenceSR1000Tool.Instance.TopMost = true;
                                Frm_KeyenceSR1000Tool.Instance.jobName = this.jobName;
                                Frm_KeyenceSR1000Tool.Instance.toolName = L_toolList[i].toolName;
                                Frm_KeyenceSR1000Tool.Instance.Show();
                                Frm_KeyenceSR1000Tool.Instance.WindowState = FormWindowState.Normal;
                                //////Frm_KeyenceSR1000Tool.Instance.btn_runFindBarcodeTool.Focus();
                                KeyenceSR1000Tool keyenceSR1000Tool = (KeyenceSR1000Tool)(L_toolList[i].tool);
                                Frm_KeyenceSR1000Tool.keyenceSR1000Tool = keyenceSR1000Tool;
                                Application.DoEvents();

                                break;
                            #endregion

                            #region CodeEdit
                            case ToolType.CodeEdit:
                                Frm_CodeEditTool.Instance.Text = "脚本编辑 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_CodeEditTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_CodeEditTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_CodeEditTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_CodeEditTool.Instance.TopMost = true;
                                Frm_CodeEditTool.Instance.Show();
                                Frm_CodeEditTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_CodeEditTool.Instance.tbx_code.Focus();
                                CodeEditTool codeEditTool = (CodeEditTool)(L_toolList[i].tool);
                                Frm_CodeEditTool.codeEditTool = codeEditTool;
                                Application.DoEvents();

                                Frm_CodeEditTool.Instance.tbx_code.Text = ((CodeEditTool)L_toolList[i].tool).sourceCode;
                                break;
                            #endregion

                            #region Label
                            case ToolType.Label:
                                Frm_LabelTool.Instance.Text = "标签 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_LabelTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_LabelTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_LabelTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_LabelTool.Instance.TopMost = true;
                                Frm_LabelTool.Instance.jobName = this.jobName;
                                Frm_LabelTool.Instance.toolName = L_toolList[i].toolName; ;
                                Frm_LabelTool.Instance.Show();
                                Frm_LabelTool.Instance.WindowState = FormWindowState.Normal;
                                Frm_LabelTool.Instance.btn_runLabelTool.Focus();
                                LabelTool labelTool = (LabelTool)(L_toolList[i].tool);
                                Frm_LabelTool.labelTool = labelTool;
                                Application.DoEvents();

                                int itemCount = ((LabelTool)L_toolList[i].tool).L_label.Count;
                                Frm_LabelTool.Instance.dgv_outputItem.Rows.Clear();
                                Frm_LabelTool.Instance.dgv_outputItem2.Rows.Clear();
                                for (int j = 0; j < itemCount; j++)
                                {
                                    if (((LabelTool)L_toolList[i].tool).L_label[j].ValueType == "Value")
                                    {
                                        int index = Frm_LabelTool.Instance.dgv_outputItem.Rows.Add();
                                        Frm_LabelTool.Instance.dgv_outputItem.Rows[index].Cells[0].Value = ((LabelTool)L_toolList[i].tool).L_label[j].OutputItem.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem.Rows[index].Cells[1].Value = ((LabelTool)L_toolList[i].tool).L_label[j].PreAddStr.ToString() == "" ? null : ((LabelTool)L_toolList[i].tool).L_label[j].PreAddStr.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem.Rows[index].Cells[2].Value = ((LabelTool)L_toolList[i].tool).L_label[j].Row.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem.Rows[index].Cells[3].Value = ((LabelTool)L_toolList[i].tool).L_label[j].Col.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem.Rows[index].Cells[4].Value = ((LabelTool)L_toolList[i].tool).L_label[j].DownLimit;
                                        Frm_LabelTool.Instance.dgv_outputItem.Rows[index].Cells[5].Value = ((LabelTool)L_toolList[i].tool).L_label[j].UpLimit;
                                        Frm_LabelTool.Instance.dgv_outputItem.Rows[index].Cells[6].Value = ((LabelTool)L_toolList[i].tool).L_label[j].Incolor.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem.Rows[index].Cells[7].Value = ((LabelTool)L_toolList[i].tool).L_label[j].OutColor.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem.Rows[index].Cells[8].Value = ((LabelTool)L_toolList[i].tool).L_label[j].Size.ToString();
                                    }
                                    else
                                    {
                                        int index = Frm_LabelTool.Instance.dgv_outputItem2.Rows.Add();
                                        Frm_LabelTool.Instance.dgv_outputItem2.Rows[index].Cells[0].Value = ((LabelTool)L_toolList[i].tool).L_label[j].OutputItem.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem2.Rows[index].Cells[1].Value = ((LabelTool)L_toolList[i].tool).L_label[j].PreAddStr.ToString() == "" ? null : ((LabelTool)L_toolList[i].tool).L_label[j].PreAddStr.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem2.Rows[index].Cells[2].Value = ((LabelTool)L_toolList[i].tool).L_label[j].Row.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem2.Rows[index].Cells[3].Value = ((LabelTool)L_toolList[i].tool).L_label[j].Col.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem2.Rows[index].Cells[4].Value = ((LabelTool)L_toolList[i].tool).L_label[j].ExpectValue.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem2.Rows[index].Cells[5].Value = ((LabelTool)L_toolList[i].tool).L_label[j].Incolor.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem2.Rows[index].Cells[6].Value = ((LabelTool)L_toolList[i].tool).L_label[j].OutColor.ToString();
                                        Frm_LabelTool.Instance.dgv_outputItem2.Rows[index].Cells[7].Value = ((LabelTool)L_toolList[i].tool).L_label[j].Size.ToString();

                                    }
                                }

                                Frm_LabelTool.Instance.ckb_labelToolEnable.Checked = ((ToolInfo)L_toolList[i]).enable;
                                break;
                            #endregion

                            #region Output
                            case ToolType.Output:
                                //Frm_OutputBoxTool.Instance.Text = "输出 - " + this.jobName + "." + L_toolList[i].toolName;
                                //Frm_OutputBoxTool.Instance.TopMost = true;
                                //Frm_OutputBoxTool.Instance.jobName = this.jobName;
                                //Frm_OutputBoxTool.Instance.toolName = L_toolList[i].toolName; ;
                                //Frm_OutputBoxTool.Instance.Show();
                                //Frm_OutputBoxTool.Instance.WindowState = FormWindowState.Normal;
                                //Frm_OutputBoxTool.Instance.b.Focus();
                                //OutputTool outputTool = (OutputTool)(L_toolList[i].tool);
                                //Application.DoEvents();

                                //将对象信息更新到界面
                                //Frm_OutputBoxTool.Instance.ckb_outputBoxToolNotRun.Checked = L_toolList[i].enable;
                                Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "\r\nThis tool is a form-free tool" : "\r\n本工具为无窗体工具！");
                                break;
                            #endregion

                            #region Condition
                            case ToolType.Condition:
                                Frm_ConditionTool.Instance.Text = "脚本编辑 - " + this.jobName + "." + L_toolList[i].toolName;
                                Frm_ConditionTool.Instance.StartPosition = FormStartPosition.Manual;
                                Frm_ConditionTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Frm_CodeEditTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                Frm_ConditionTool.Instance.TopMost = true;

                                for(int ii = 0; ii < this.L_toolList.Count; ii++)
                                {
                                    this.L_toolList[ii].Id = ii;
                                }

                                Frm_ConditionTool.Instance.InitData(L_toolList);
                                Frm_ConditionTool.Instance.Show();
                                Frm_ConditionTool.Instance.WindowState = FormWindowState.Normal;
                                //Frm_ConditionTool.Instance.tbx_code.Focus();
                                //CodeEditTool codeEditTool = (CodeEditTool)(L_toolList[i].tool);
                                //Frm_ConditionTool.codeEditTool = codeEditTool;
                                Application.DoEvents();

                               // Frm_ConditionTool.Instance.tbx_code.Text = ((CodeEditTool)L_toolList[i].tool).sourceCode;
                                break;
                                #endregion

                        }
                    }
                }
                Frm_Main.ignore = false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 运行此流程
        /// </summary>
        public List<string> Run()
        {
            try
            {
                jobRunStatu = JobRunStatu.Fail;
                Project.GetJobTree(jobName).ShowNodeToolTips = true;
                Stopwatch jobElapsedTime = new Stopwatch();
                jobElapsedTime.Restart();
                recordElapseTime = 0;
                //////foreach (TreeNode toolNode in Project.GetJobTree(jobName).Nodes)
                //////{
                //////    toolNode.ForeColor = Color.Black;
                //////}

                //开始逐个执行各工具
                jobRunStatu = JobRunStatu.Fail;
                List<string> L_result = new List<string>();
                int toolIndex = -1;

                for (int i = 0; i < L_toolList.Count; i++)
                {
                    toolIndex++;
                    TreeNode treeNode = GetToolNodeByNodeText(L_toolList[i].toolName);
                    inputItemNum = (L_toolList[i]).input.Count;
                    outputItemNum = (L_toolList[i]).output.Count;
                    bool sourceValueIsEmpty = false;      //此变量判断输入源值是否为空，若为空就终止流程执行

                    #region HalconInterface
                    if (L_toolList[i].toolType == ToolType.HalconInterface)
                    {
                        HalconInterfaceTool halconInterfaceTool = (HalconInterfaceTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            halconInterfaceTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = halconInterfaceTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        halconInterfaceTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出图像":
                                case "OutputImage":
                                    L_toolList[i].GetOutput(outputItem).value = halconInterfaceTool.outputImage;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break;
                            }
                        }

                        if (halconInterfaceTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, halconInterfaceTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region SDK_Basler
                    if (L_toolList[i].toolType == ToolType.SDK_Basler)
                    {
                        SDK_BaslerTool SDK_baslerTool = (SDK_BaslerTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            SDK_baslerTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = SDK_baslerTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        SDK_baslerTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出图像":
                                case "OutputImage":
                                    L_toolList[i].GetOutput(outputItem).value = SDK_baslerTool.outputImage;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break;
                            }
                        }

                        if (SDK_baslerTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, SDK_baslerTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region SDK_Cognex
                    if (L_toolList[i].toolType == ToolType.SDK_Congex)
                    {
                        SDK_CongexTool SDK_congexTool = (SDK_CongexTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            SDK_congexTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = SDK_congexTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        SDK_congexTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出图像":
                                case "OutputImage":
                                    L_toolList[i].GetOutput(outputItem).value = SDK_congexTool.outputImage;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break;
                            }
                        }

                        if (SDK_congexTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, SDK_congexTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region SDK_PointGray
                    if (L_toolList[i].toolType == ToolType.SDK_PointGray)
                    {
                        SDK_PointGrayTool SDK_pointGrayTool = (SDK_PointGrayTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            SDK_pointGrayTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = SDK_pointGrayTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        SDK_pointGrayTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出图像":
                                case "OutputImage":
                                    L_toolList[i].GetOutput(outputItem).value = SDK_pointGrayTool.outputImage;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break;
                            }
                        }

                        if (SDK_pointGrayTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, SDK_pointGrayTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region SDK_IMAVision
                    if (L_toolList[i].toolType == ToolType.SDK_IMAVision)
                    {
                        SDK_IMAVisionTool SDK_imaVisionTool = (SDK_IMAVisionTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            SDK_imaVisionTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = SDK_imaVisionTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        SDK_imaVisionTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出图像":
                                case "OutputImage":
                                    L_toolList[i].GetOutput(outputItem).value = SDK_imaVisionTool.outputImage;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break;
                            }
                        }

                        if (SDK_imaVisionTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, SDK_imaVisionTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region SDK_MindVision
                    if (L_toolList[i].toolType == ToolType.SDK_MindVision)
                    {
                        SDK_MindVisionTool SDK_mindVisionTool = (SDK_MindVisionTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            SDK_mindVisionTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = SDK_mindVisionTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        SDK_mindVisionTool.Run(jobName, true, false);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出图像":
                                case "OutputImage":
                                    L_toolList[i].GetOutput(outputItem).value = SDK_mindVisionTool.outputImage;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break;
                            }
                        }

                        if (SDK_mindVisionTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, SDK_mindVisionTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region SDK_HIKVision
                    if (L_toolList[i].toolType == ToolType.SDK_HIKVision)
                    {
                        SDK_HIKVisionTool SDK_hikVisionTool = (SDK_HIKVisionTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            SDK_hikVisionTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = SDK_hikVisionTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        SDK_hikVisionTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出图像":
                                case "OutputImage":
                                    L_toolList[i].GetOutput(outputItem).value = SDK_hikVisionTool.outputImage;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break;
                            }
                        }

                        if (SDK_hikVisionTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, SDK_hikVisionTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region ShapeMatch
                    else if (L_toolList[i].toolType == ToolType.ShapeMatch)
                    {
                        ShapeMatchTool shapeMatchTool = (ShapeMatchTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            shapeMatchTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = shapeMatchTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        shapeMatchTool.ClearLastInput();

                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItemName = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                shapeMatchTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Asign_Input_Image : ToolRunStatu.未指定输入图像);
                                treeNode.ToolTipText = shapeMatchTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, shapeMatchTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }
                            if (inputItemName == "输入图像" || inputItemName == "InputImage")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                shapeMatchTool.inputImage = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (shapeMatchTool.inputImage == null)
                                {
                                    shapeMatchTool.runStatu = ToolRunStatu.未指定输入图像;
                                    treeNode.ToolTipText = shapeMatchTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        shapeMatchTool.Run(jobName, false, false);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "结果个数":
                                case "ResultNum":
                                    L_toolList[i].GetOutput(outputItem).value = shapeMatchTool.matchNum;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = shapeMatchTool.matchNum.ToString();
                                    break; ;
                                case "结果字符串":
                                case "ResultStr":
                                    if (shapeMatchTool.L_matchResult.Count == 0)
                                    {
                                        L_toolList[i].GetOutput(outputItem).value = "0,+0000.000,+0000.000,+0000.000";
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "0,+0000.000,+0000.000,+0000.000";
                                    }
                                    else
                                    {
                                        string result = shapeMatchTool.L_matchResult.Count.ToString();
                                        for (int k = 0; k < shapeMatchTool.L_matchResult.Count; k++)
                                        {
                                            string row = FormatValueTo9Bit(shapeMatchTool.L_matchResult[k].Row);
                                            string col = FormatValueTo9Bit(shapeMatchTool.L_matchResult[k].Col);
                                            string ang = FormatValueTo9Bit(shapeMatchTool.L_matchResult[k].Angle);
                                            result += "," + row + "," + col + "," + ang;
                                        }
                                        L_toolList[i].GetOutput(outputItem).value = result;
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = result;
                                    }
                                    break;
                                case "位姿":
                                case "Pose":
                                    L_toolList[i].GetOutput(outputItem).value = shapeMatchTool.pose;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = shapeMatchTool.pose.X + " | " + shapeMatchTool.pose.Y + " | " + shapeMatchTool.pose.U;
                                    break;
                                case "结果行":
                                case "ResultRow":
                                    if (shapeMatchTool.L_matchResult.Count == 0)
                                    {
                                        L_toolList[i].GetOutput(outputItem).value = "0000.000";
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "0000.000";
                                    }
                                    else
                                    {
                                        L_toolList[i].GetOutput(outputItem).value = shapeMatchTool.resultRow;
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = shapeMatchTool.resultRow.ToString();
                                    }
                                    break;
                                case "结果列":
                                case "ResultCol":
                                    if (shapeMatchTool.L_matchResult.Count == 0)
                                    {
                                        L_toolList[i].GetOutput(outputItem).value = "0000.000";
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "0000.000";
                                    }
                                    else
                                    {
                                        L_toolList[i].GetOutput(outputItem).value = shapeMatchTool.resultCol;
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = shapeMatchTool.resultCol.ToString();
                                    }
                                    break;
                                case "结果角度":
                                case "ResultAngle":
                                    if (shapeMatchTool.L_matchResult.Count == 0)
                                    {
                                        L_toolList[i].GetOutput(outputItem).value = "0000.000";
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "0000.000";
                                    }
                                    else
                                    {
                                        L_toolList[i].GetOutput(outputItem).value = shapeMatchTool.resultAngle;
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = shapeMatchTool.resultAngle.ToString();
                                    }
                                    break;
                            }
                        }

                        if (shapeMatchTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, shapeMatchTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region EyeHandCalibration
                    else if (L_toolList[i].toolType == ToolType.EyeHandCalibration)
                    {
                        EyeHandCalibrationTool eyeHandCalibrationTool = (EyeHandCalibrationTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((EyeHandCalibrationTool)(L_toolList[i].tool)).runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled  : ToolRunStatu.未启用);
                            treeNode.ToolTipText = eyeHandCalibrationTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }

                        //首先把所有的输入都清空
                        eyeHandCalibrationTool.inputStr = null;
                        eyeHandCalibrationTool.inputImage = null;

                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                ((EyeHandCalibrationTool)(L_toolList[i].tool)).runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                                treeNode.ToolTipText = eyeHandCalibrationTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, eyeHandCalibrationTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "输入图像" || inputItem == "InputImage")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                eyeHandCalibrationTool.inputImage = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (eyeHandCalibrationTool.inputImage == null)
                                {
                                    eyeHandCalibrationTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.No_Input_Image : ToolRunStatu.无输入图像);
                                    treeNode.ToolTipText = eyeHandCalibrationTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                            else if (inputItem == "输入字符串" || inputItem == "InputStr")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                eyeHandCalibrationTool.inputStr = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as string;
                                if (eyeHandCalibrationTool.inputStr == null)
                                {
                                    eyeHandCalibrationTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.No_Input_String : ToolRunStatu.无输入字符串);
                                    treeNode.ToolTipText = eyeHandCalibrationTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                            else if (inputItem == "输入点" || inputItem == "InputStr")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                eyeHandCalibrationTool.inputPose = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as PosXYU;
                                if (eyeHandCalibrationTool.inputPose == null)
                                {
                                    eyeHandCalibrationTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.No_Input_String : ToolRunStatu.无输入字符串);
                                    treeNode.ToolTipText = eyeHandCalibrationTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                            else if (inputItem == "输入位置" || inputItem == "InputStr")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                eyeHandCalibrationTool.inputPose = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as PosXYU;
                                if (eyeHandCalibrationTool.inputPose == null)
                                {
                                    eyeHandCalibrationTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.No_Input_String : ToolRunStatu.无输入字符串);
                                    treeNode.ToolTipText = eyeHandCalibrationTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        eyeHandCalibrationTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出图像":
                                case "OutputImage":
                                    L_toolList[i].GetOutput(outputItem).value = eyeHandCalibrationTool.outputImage;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量不支持显示";
                                    break;
                                case "输出字符串":
                                case "ResultStr":
                                    L_toolList[i].GetOutput(outputItem).value = eyeHandCalibrationTool.outputStr;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = eyeHandCalibrationTool.outputStr;
                                    break;
                                case "输出点":
                                case "ResultxxStr":
                                    L_toolList[i].GetOutput(outputItem).value = eyeHandCalibrationTool.outputPose;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = eyeHandCalibrationTool.outputPose.X + " | " + eyeHandCalibrationTool.outputPose.Y + " | " + eyeHandCalibrationTool.outputPose.U;
                                    break;
                            }
                        }

                        if (eyeHandCalibrationTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, eyeHandCalibrationTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region CircleCalibration
                    else if (L_toolList[i].toolType == ToolType.CircleCalibration)
                    {
                        CircleCalibrationTool circleCalibrationTool = (CircleCalibrationTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            circleCalibrationTool.runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = circleCalibrationTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString(); if (inputItem == string.Empty)
                            {
                                circleCalibrationTool.runStatu = ToolRunStatu.Not_Input_Image;
                                treeNode.ToolTipText = circleCalibrationTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, circleCalibrationTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            circleCalibrationTool.inputPose = null;
                            if (inputItem == "输入图像")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                circleCalibrationTool.inputImage = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                            }
                            else if (inputItem == "位姿")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                circleCalibrationTool.inputPose = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as PosXYU;
                            }
                            else if (inputItem == "预期圆中心行" || inputItem == "ExpectCircleCenterX")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                circleCalibrationTool.expectCircleRow = (HTuple)Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value);
                            }
                            else if (inputItem == "预期圆中心列" || inputItem == "ExpectCircleCenterY")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ").Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                circleCalibrationTool.expectCircleCol = (HTuple)Convert.ToDouble((HObject)GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value);
                            }
                            if (inputItem == "预期圆半径" || inputItem == "ExpectCircleRadius")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ").Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                circleCalibrationTool.expectCircleRadius = (HTuple)Convert.ToDouble((HObject)GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value);
                            }
                        }

                        circleCalibrationTool.Run(jobName, true, true);
                        for (int j = 0; j < outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "结果圆圆心行坐标":
                                case "<--Result_CenterX":
                                    L_toolList[i].GetOutput(outputItem).value = circleCalibrationTool.ResultCircleRow.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = circleCalibrationTool.ResultCircleRow.ToString();
                                    break;
                                case "结果圆圆心列坐标":
                                case "<--Result_CenterY":
                                    L_toolList[i].GetOutput(outputItem).value = circleCalibrationTool.ResultCircleCol.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = circleCalibrationTool.ResultCircleCol.ToString();
                                    break;
                                case "结果圆半径":
                                case "<--Result_Radius":
                                    L_toolList[i].GetOutput(outputItem).value = circleCalibrationTool.ResultCircleRadius.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = circleCalibrationTool.ResultCircleRadius.ToString();
                                    break;
                            }
                        }

                        if (circleCalibrationTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, circleCalibrationTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region SubImage
                    else if (L_toolList[i].toolType == ToolType.SubImage)
                    {
                        SubImageTool subImageTool = (SubImageTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((SubImageTool)(L_toolList[i].tool)).runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用;
                            treeNode.ToolTipText = subImageTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                ((SubImageTool)(L_toolList[i].tool)).runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Image : ToolRunStatu.未指定输入图像;
                                treeNode.ToolTipText = subImageTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, subImageTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "输入图像" || inputItem == "InputImage")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                subImageTool.inputImage = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (subImageTool.inputImage == null)
                                {
                                    subImageTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Image : ToolRunStatu.未指定输入图像);
                                    treeNode.ToolTipText = subImageTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        subImageTool.Run(jobName, false, false);
                        for (int j = 0; j < outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出图像":
                                case "OutputImage":
                                    L_toolList[i].GetOutput(outputItem).value = subImageTool.outputImage;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量不支持显示";
                                    break;
                            }
                        }

                        if (subImageTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, subImageTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region BlobAnalyse
                    else if (L_toolList[i].toolType == ToolType.BlobAnalyse)
                    {
                        BlobAnalyseTool blobAnalyseTool = (BlobAnalyseTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((BlobAnalyseTool)(L_toolList[i].tool)).runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = blobAnalyseTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }

                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                ((BlobAnalyseTool)(L_toolList[i].tool)).runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                                treeNode.ToolTipText = blobAnalyseTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, blobAnalyseTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "输入图像" || inputItem == "InputImage")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                blobAnalyseTool.inputImage = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (blobAnalyseTool.inputImage == null)
                                {
                                    blobAnalyseTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Lack_Of_Input_Image : ToolRunStatu.Lack_Of_Input_Image);
                                    treeNode.ToolTipText = blobAnalyseTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                            else if (inputItem == "搜索区域" || inputItem == "SearchRegion")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                blobAnalyseTool.inputSearchRegion = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (blobAnalyseTool.inputSearchRegion == null)
                                {
                                    blobAnalyseTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Lack_Of_Input_Search_Region : ToolRunStatu.缺少输入搜索区域);
                                    treeNode.ToolTipText = blobAnalyseTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        blobAnalyseTool.Run(jobName, false);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItemName = L_toolList[i].output[j].IOName;
                            switch (outputItemName)
                            {
                                case "结果个数":
                                case "ResultCount":
                                    L_toolList[i].GetOutput(outputItemName).value = blobAnalyseTool.L_resultBlob.Count.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItemName).ToolTipText = blobAnalyseTool.L_resultBlob.Count.ToString();
                                    break;
                                case "斑点结果":
                                case "BlobResult":
                                    L_toolList[i].GetOutput(outputItemName).value = blobAnalyseTool.L_resultBlob;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItemName).ToolTipText = "图像变量暂不支持显示";
                                    break;
                                case "结果区域":
                                case "ResultRegion":
                                    L_toolList[i].GetOutput(outputItemName).value = blobAnalyseTool.outputResultRegion;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItemName).ToolTipText = "图像变量暂不支持显示";
                                    break;
                            }
                        }

                        if (blobAnalyseTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, blobAnalyseTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region DownCamAlign
                    else if (L_toolList[i].toolType == ToolType.DownCamAlign)
                    {
                        RobotDownCamAlignTool robotDownCamAlignTool = (RobotDownCamAlignTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            robotDownCamAlignTool.runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = robotDownCamAlignTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                robotDownCamAlignTool.runStatu = ToolRunStatu.Not_Assign_Input_Pos;
                                treeNode.ToolTipText = robotDownCamAlignTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, robotDownCamAlignTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "输入字符串" || inputItem == "InputStr")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                PosXYU posXYU = new PosXYU();
                                string temp = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString();
                                string str = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString().Substring(2, 9);
                                posXYU.X = Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString().Substring(2, 9));
                                posXYU.Y = Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString().Substring(12, 9));
                                posXYU.U = Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString().Substring(22, 9));
                                robotDownCamAlignTool.inputPos = posXYU;
                            }
                            else if (inputItem == "输入点" || inputItem == "InputStr")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                robotDownCamAlignTool.inputPos = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as PosXYU;
                            }
                        }
                        robotDownCamAlignTool.Run(string.Empty, true, true);
                        for (int j = 0; j < outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出点":
                                case "OutputAllAddStr":
                                    L_toolList[i].GetOutput(outputItem).value = robotDownCamAlignTool.targetPos.X + "," + robotDownCamAlignTool.targetPos.Y + "," + robotDownCamAlignTool.targetPos.U;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = robotDownCamAlignTool.targetPos.X + "," + robotDownCamAlignTool.targetPos.Y + "," + robotDownCamAlignTool.targetPos.U;
                                    break;
                                case "格式点":
                                case "OutputAllAddxStr":
                                    L_toolList[i].GetOutput(outputItem).value = robotDownCamAlignTool.targetPos.ToFormatStr();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = robotDownCamAlignTool.targetPos.ToShowTip();
                                    break;
                            }
                        }

                        if (robotDownCamAlignTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, robotDownCamAlignTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region ColorToRGB
                    else if (L_toolList[i].toolType == ToolType.ColorToRGB)
                    {
                        ColorToRGBTool colorToRGBTool = (ColorToRGBTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            colorToRGBTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = colorToRGBTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        colorToRGBTool.ClearLastInput();

                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItemName = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                colorToRGBTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Asign_Input_Image : ToolRunStatu.未指定输入图像);
                                treeNode.ToolTipText = colorToRGBTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, colorToRGBTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }
                            if (inputItemName == "输入图像" || inputItemName == "InputImage")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                colorToRGBTool.inputImage = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (colorToRGBTool.inputImage == null)
                                {
                                    colorToRGBTool.runStatu = ToolRunStatu.未指定输入图像;
                                    treeNode.ToolTipText = colorToRGBTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        colorToRGBTool.Run(jobName, false, false);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "红":
                                case "Red":
                                    L_toolList[i].GetOutput(outputItem).value = colorToRGBTool.outputRed;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break; ;
                                case "绿":
                                case "Green":
                                    L_toolList[i].GetOutput(outputItem).value = colorToRGBTool.outputGreen;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break; ;
                                case "蓝":
                                case "Blue":
                                    L_toolList[i].GetOutput(outputItem).value = colorToRGBTool.outputBlue;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break; ;
                            }
                        }

                        if (colorToRGBTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, colorToRGBTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region FindLine
                    else if (L_toolList[i].toolType == ToolType.FindLine)
                    {
                        FindLineTool findLineTool = (FindLineTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            findLineTool.runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = findLineTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                findLineTool.runStatu = ToolRunStatu.Not_Asign_Input_Source;
                                treeNode.ToolTipText = findLineTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, findLineTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            findLineTool.inputPose = null;
                            if (inputItem == "InputImage" || inputItem == "输入图像")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0]; ;
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                findLineTool.inputImage = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                            }
                            else if (inputItem == "Pose" || inputItem == "位姿")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0]; ;
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                findLineTool.inputPose = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as PosXYU;
                            }
                        }
                        findLineTool.Run(jobName, false, true);
                        for (int j = 0; j < outputItemNum; j++)
                        {
                            string outputItemName = L_toolList[i].output[j].IOName;
                            switch (outputItemName)
                            {
                                case "结果线起点行坐标":
                                case "ResultLineStartX":
                                    L_toolList[i].GetOutput(outputItemName).value = findLineTool.ResultLineStartRow.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItemName).ToolTipText = findLineTool.ResultLineStartRow.ToString();
                                    break;
                                case "结果线起点列坐标":
                                case "ResultLineStartY":
                                    L_toolList[i].GetOutput(outputItemName).value = findLineTool.ResultLineStartCol.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItemName).ToolTipText = findLineTool.ResultLineStartCol.ToString();
                                    break;
                                case "结果线终点行坐标":
                                case "ResultLineEndX":
                                    L_toolList[i].GetOutput(outputItemName).value = findLineTool.ResultLineEndRow.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItemName).ToolTipText = findLineTool.ResultLineEndRow.ToString();
                                    break;
                                case "结果线终点列坐标":
                                case "ResultLineEndY":
                                    L_toolList[i].GetOutput(outputItemName).value = findLineTool.ResultLineEndCol.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItemName).ToolTipText = findLineTool.ResultLineEndCol.ToString();
                                    break;
                                case "结果线段":
                                case "ResultSegment":
                                    L_toolList[i].GetOutput(outputItemName).value = findLineTool.resultLine;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItemName).ToolTipText = findLineTool.ResultLineStartRow.ToString() + " | " + findLineTool.ResultLineStartCol.ToString() + " | " + findLineTool.ResultLineEndRow.ToString() + " | " + findLineTool.ResultLineEndCol.ToString();
                                    break;
                                case "方向":
                                case "ResultSxegment":
                                    L_toolList[i].GetOutput(outputItemName).value = findLineTool.Angle;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItemName).ToolTipText = findLineTool.Angle.ToString();
                                    break;
                            }
                        }

                        if (findLineTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, findLineTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region FindCricle
                    else if (L_toolList[i].toolType == ToolType.FindCircle)
                    {
                        FindCircleTool findCircleTool = (FindCircleTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            findCircleTool.runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = findCircleTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString(); if (inputItem == string.Empty)
                            {
                                findCircleTool.runStatu = ToolRunStatu.Not_Input_Image;
                                treeNode.ToolTipText = findCircleTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, findCircleTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            findCircleTool.inputPose = null;
                            if (inputItem == "输入图像")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                findCircleTool.inputImage = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                            }
                            else if (inputItem == "位姿")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                findCircleTool.inputPose = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as PosXYU;
                            }
                            else if (inputItem == "预期圆中心行" || inputItem == "ExpectCircleCenterX")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                findCircleTool.expectCircleRow = (HTuple)Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value);
                            }
                            else if (inputItem == "预期圆中心列" || inputItem == "ExpectCircleCenterY")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                findCircleTool.expectCircleCol = (HTuple)Convert.ToDouble((HObject)GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value);
                            }
                            if (inputItem == "预期圆半径" || inputItem == "ExpectCircleRadius")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                findCircleTool.expectCircleRadius = (HTuple)Convert.ToDouble((HObject)GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value);
                            }
                        }

                        findCircleTool.Run(jobName, true, true);
                        for (int j = 0; j < outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "结果圆圆心行坐标":
                                case "Result_CenterX":
                                    L_toolList[i].GetOutput(outputItem).value = findCircleTool.ResultCircleRow.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = findCircleTool.ResultCircleRow.ToString();
                                    break;
                                case "结果圆圆心列坐标":
                                case "<--Result_CenterY":
                                    L_toolList[i].GetOutput(outputItem).value = findCircleTool.ResultCircleCol.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = findCircleTool.ResultCircleCol.ToString();
                                    break;
                                case "结果圆半径":
                                case "<--Result_Radius":
                                    L_toolList[i].GetOutput(outputItem).value = findCircleTool.ResultCircleRadius.ToString();
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = findCircleTool.ResultCircleRadius.ToString();
                                    break;
                            }
                        }

                        if (findCircleTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, findCircleTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region CreateROI
                    else if (L_toolList[i].toolType == ToolType.CreateROI)
                    {
                        CreateROITool createROITool = (CreateROITool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            createROITool.runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = createROITool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString(); if (inputItem == string.Empty)
                            {
                                createROITool.runStatu = ToolRunStatu.Not_Assign_Input_Source;
                                treeNode.ToolTipText = createROITool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, createROITool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            //////createROITool.inputPose = null;
                            if (inputItem == "左上点行")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                createROITool.leftTopRow = Convert.ToInt16(Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value));
                            }
                            else if (inputItem == "左上点列")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                createROITool.leftTopCol = Convert.ToInt16(Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value));
                            }
                            else if (inputItem == "右下点行" || inputItem == "ExpectCircleCenterX")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                createROITool.rightDownRow = Convert.ToInt16(Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value));
                            }
                            else if (inputItem == "右下点列" || inputItem == "ExpectCircleCenterY")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                createROITool.rightDownCol = Convert.ToInt16(Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value));
                            }
                            else if (inputItem == "位姿" || inputItem == "ExpectCircleCenterY")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                createROITool.inputPose = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as PosXYU;
                            }
                        }

                        createROITool.Run(jobName, true, true);
                        for (int j = 0; j < outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出ROI":
                                case "<--Result_CenterX":
                                    L_toolList[i].GetOutput(outputItem).value = createROITool.outputROI;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量不支持显示";
                                    break;
                            }
                        }

                        if (createROITool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, createROITool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region CreateLine
                    else if (L_toolList[i].toolType == ToolType.CreateLine)
                    {
                        CreateLineTool createLineTool = (CreateLineTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            createLineTool.runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = createLineTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString(); if (inputItem == string.Empty)
                            {
                                createLineTool.runStatu = ToolRunStatu.Not_Assign_Input_Source;
                                treeNode.ToolTipText = createLineTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, createLineTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            //////createROITool.inputPose = null;
                            if (inputItem == "起点")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                createLineTool.inputPoint1 = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as Point;
                            }
                            else if (inputItem == "终点")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                createLineTool.inputPoint2 = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as Point;
                            }
                        }

                        createLineTool.Run(jobName, true, true);
                        for (int j = 0; j < outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出线":
                                case "<--Result_CenterX":
                                    L_toolList[i].GetOutput(outputItem).value = createLineTool.outputLine;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = createLineTool.outputLine.ToShowTip();
                                    break;
                                case "输出线方向":
                                case "<--Result_CxenterX":
                                    L_toolList[i].GetOutput(outputItem).value = createLineTool.outputLine.Angle;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = createLineTool.outputLine.Angle.ToString();
                                    break;
                            }
                        }

                        if (createLineTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, createLineTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region CreatePosition
                    else if (L_toolList[i].toolType == ToolType.CreatePosition)
                    {
                        CreatePositionTool createPositionTool = (CreatePositionTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            createPositionTool.runStatu = ToolRunStatu.Not_Enabled;
                            treeNode.ToolTipText = createPositionTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString(); if (inputItem == string.Empty)
                            {
                                createPositionTool.runStatu = ToolRunStatu.Not_Assign_Input_Source;
                                treeNode.ToolTipText = createPositionTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, createPositionTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            //////createROITool.inputPose = null;
                            if (inputItem == "输入点")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                createPositionTool.inputPoint = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as Point;
                            }
                            else if (inputItem == "输入方向")
                            {
                                string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                createPositionTool.inputAngle = Convert.ToDouble(GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString());
                            }
                        }

                        createPositionTool.Run(jobName, true, true);
                        for (int j = 0; j < outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出位置":
                                case "<--Result_CenterX":
                                    L_toolList[i].GetOutput(outputItem).value = createPositionTool.outputPos;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = createPositionTool.outputPos.X.ToString() + " | " + createPositionTool.outputPos.Y.ToString() + " | " + createPositionTool.outputPos.U.ToString();
                                    break;
                            }
                        }

                        if (createPositionTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, createPositionTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region DistancePL
                    else if (L_toolList[i].toolType == ToolType.DistancePL)
                    {
                        DistancePLTool distancePLTool = (DistancePLTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            distancePLTool.runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = distancePLTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (inputItem == string.Empty)
                            {
                                ((DistancePLTool)(L_toolList[i].tool)).runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.Not_Assign_Input_Source);
                                treeNode.ToolTipText = distancePLTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                return L_result;
                            }

                            if (inputItem == "输入点" || inputItem == "InputSegment1")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                distancePLTool.inputPoint = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as Point;
                                if (distancePLTool.inputPoint == null)
                                {
                                    distancePLTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                                    treeNode.ToolTipText = distancePLTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, distancePLTool.runStatu.ToString()), Color.Red);
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                            if (inputItem == "输入线段" || inputItem == "InputSegment2")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                distancePLTool.inputLine = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as Line;
                                if (distancePLTool.inputLine == null)
                                {
                                    distancePLTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                                    treeNode.ToolTipText = distancePLTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        distancePLTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "ResultDistance":
                                case "结果距离":
                                    L_toolList[i].GetOutput(outputItem).value = distancePLTool.outputDistance;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = distancePLTool.outputDistance.ToString();
                                    break;
                            }
                        }

                        if (distancePLTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, distancePLTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region DistanceSS
                    else if (L_toolList[i].toolType == ToolType.DistanceSS)
                    {
                        DistanceLLTool distanceSegmentAndSegmentTool = (DistanceLLTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            distanceSegmentAndSegmentTool.runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = distanceSegmentAndSegmentTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (inputItem == string.Empty)
                            {
                                ((DistanceLLTool)(L_toolList[i].tool)).runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.Not_Assign_Input_Source);
                                treeNode.ToolTipText = distanceSegmentAndSegmentTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, distanceSegmentAndSegmentTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "输入线段1" || inputItem == "InputSegment1")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                distanceSegmentAndSegmentTool.line1 = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as Line;
                                if (distanceSegmentAndSegmentTool.line1 == null)
                                {
                                    distanceSegmentAndSegmentTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                                    treeNode.ToolTipText = distanceSegmentAndSegmentTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                            if (inputItem == "输入线段2" || inputItem == "InputSegment2")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                distanceSegmentAndSegmentTool.line2 = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as Line;
                                if (distanceSegmentAndSegmentTool.line2 == null)
                                {
                                    distanceSegmentAndSegmentTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                                    treeNode.ToolTipText = distanceSegmentAndSegmentTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        distanceSegmentAndSegmentTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "ResultDistance":
                                case "结果距离值":
                                    L_toolList[i].GetOutput(outputItem).value = distanceSegmentAndSegmentTool.ResultDistance;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = distanceSegmentAndSegmentTool.ResultDistance.ToString();
                                    break;
                            }
                        }

                        if (distanceSegmentAndSegmentTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, distanceSegmentAndSegmentTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region LLPoint
                    else if (L_toolList[i].toolType == ToolType.LLPoint)
                    {
                        LLPointTool llPointTool = (LLPointTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            llPointTool.runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = llPointTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (inputItem == string.Empty)
                            {
                                ((LLPointTool)(L_toolList[i].tool)).runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.Not_Assign_Input_Source);
                                treeNode.ToolTipText = llPointTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, llPointTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "输入线段1" || inputItem == "InputSegment1")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                llPointTool.line1 = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as Line;
                                if (llPointTool.line1 == null)
                                {
                                    llPointTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                                    treeNode.ToolTipText = llPointTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                            if (inputItem == "输入线段2" || inputItem == "InputSegment2")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                llPointTool.line2 = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as Line;
                                if (llPointTool.line2 == null)
                                {
                                    llPointTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                                    treeNode.ToolTipText = llPointTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        llPointTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "ResultDistance":
                                case "输出点":
                                    L_toolList[i].GetOutput(outputItem).value = llPointTool.ResultDistance;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = llPointTool.ResultDistance.Row + " , " + llPointTool.ResultDistance.Col;
                                    break;
                            }
                        }

                        if (llPointTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, llPointTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region OCR
                    else if (L_toolList[i].toolType == ToolType.OCR)
                    {
                        OCRTool ocrTool = (OCRTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ocrTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = ocrTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        ocrTool.ClearLastInput();

                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItemName = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                ocrTool.runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Asign_Input_Image : ToolRunStatu.未指定输入图像);
                                treeNode.ToolTipText = ocrTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, ocrTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }
                            if (inputItemName == "输入图像" || inputItemName == "InputImage")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                ocrTool.inputImage = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (ocrTool.inputImage == null)
                                {
                                    ocrTool.runStatu = ToolRunStatu.未指定输入图像;
                                    treeNode.ToolTipText = ocrTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        ocrTool.Run(jobName, false, false);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "结果字符串":
                                case "ResultStr":
                                    //////if (ocrTool.L_matchResult.Count == 0)
                                    //////{
                                    //////    L_toolList[i].GetOutput (outputItem ).value = "";
                                    //////    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "空";
                                    //////}
                                    //////else
                                    {

                                        L_toolList[i].GetOutput(outputItem).value = ocrTool.outputStr;
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = ocrTool.outputStr;
                                    }
                                    break;
                            }
                        }

                        if (ocrTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, ocrTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region Barcode
                    else if (L_toolList[i].toolType == ToolType.Barcode)
                    {
                        BarcodeTool barcodeTool = (BarcodeTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((BarcodeTool)(L_toolList[i].tool)).runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = barcodeTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                ((BarcodeTool)(L_toolList[i].tool)).runStatu = ToolRunStatu.Not_Assign_Input_Source;
                                treeNode.ToolTipText = barcodeTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, barcodeTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "InputImage" || inputItem == "输入图像")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                barcodeTool.inputImage = GetToolInfoByToolName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (barcodeTool.inputImage == null)
                                {
                                    barcodeTool.runStatu = ToolRunStatu.Not_Assign_Input_Source;
                                    treeNode.ToolTipText = barcodeTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        barcodeTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "结果字符串":
                                    if (barcodeTool.resultNum == 0)
                                    {
                                        L_toolList[i].GetOutput(outputItem).value = "";
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "无条码";
                                    }
                                    else
                                    {
                                        L_toolList[i].GetOutput(outputItem).value = barcodeTool.outputStr;
                                        GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = barcodeTool.outputStr;
                                    }
                                    break;
                            }
                        }

                        if (barcodeTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, barcodeTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region RegionFeature
                    else if (L_toolList[i].toolType == ToolType.RegionFeature)
                    {
                        RegionFeatureTool regionFeatureTool = (RegionFeatureTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((RegionFeatureTool)(L_toolList[i].tool)).runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Enabled : ToolRunStatu.未启用);
                            treeNode.ToolTipText = regionFeatureTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                ((RegionFeatureTool)(L_toolList[i].tool)).runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                                treeNode.ToolTipText = regionFeatureTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, regionFeatureTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "InputImage" || inputItem == "输入区域")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                regionFeatureTool.inputRegion = GetToolInfoByToolName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (regionFeatureTool.inputRegion == null)
                                {
                                    regionFeatureTool.runStatu = ToolRunStatu.Not_Assign_Input_Source;
                                    treeNode.ToolTipText = regionFeatureTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        regionFeatureTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "圆度":
                                    L_toolList[i].GetOutput(outputItem).value = regionFeatureTool.Roundness;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = regionFeatureTool.Roundness.ToString();
                                    break;
                                case "中心点":
                                    L_toolList[i].GetOutput(outputItem).value = regionFeatureTool.CenterPoint;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = regionFeatureTool.CenterPoint.ToShowTip();
                                    break;
                                case "外接仿矩":
                                    L_toolList[i].GetOutput(outputItem).value = regionFeatureTool.outRectangle2;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = "图形变量暂不支持显示";
                                    break;
                            }
                        }

                        if (regionFeatureTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, regionFeatureTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region RegionOperation
                    else if (L_toolList[i].toolType == ToolType.RegionOperation)
                    {
                        RegionOperationTool regionOperationTool = (RegionOperationTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((BarcodeTool)(L_toolList[i].tool)).runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = regionOperationTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                ((BarcodeTool)(L_toolList[i].tool)).runStatu = ToolRunStatu.Not_Assign_Input_Source;
                                treeNode.ToolTipText = regionOperationTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, regionOperationTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "InputImage" || inputItem == "区域1")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                regionOperationTool.inputRegion1 = GetToolInfoByToolName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (regionOperationTool.inputRegion1 == null)
                                {
                                    regionOperationTool.runStatu = ToolRunStatu.Not_Assign_Input_Source;
                                    treeNode.ToolTipText = regionOperationTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                            else if (inputItem == "InputImage" || inputItem == "区域2")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                regionOperationTool.inputRegion2 = GetToolInfoByToolName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text, sourceToolName).GetOutput(toolItem).value as HObject;
                                if (regionOperationTool.inputRegion2 == null)
                                {
                                    regionOperationTool.runStatu = ToolRunStatu.Not_Assign_Input_Source;
                                    treeNode.ToolTipText = regionOperationTool.runStatu.ToString();
                                    treeNode.ForeColor = Color.Red;
                                    sourceValueIsEmpty = true;
                                    break;
                                }
                            }
                        }
                        if (sourceValueIsEmpty)
                            break;
                        regionOperationTool.Run(jobName, true, true);
                        for (int j = 0; j < this.outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "结果区域":
                                    L_toolList[i].GetOutput(outputItem).value = regionOperationTool.outputRegion;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = regionOperationTool.Roundness.ToString();
                                    break;
                            }
                        }

                        if (regionOperationTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, regionOperationTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region CodeEdit
                    else if (L_toolList[i].toolType == ToolType.CodeEdit)
                    {
                        CodeEditTool codeEditTool = (CodeEditTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((CodeEditTool)(L_toolList[i].tool)).runStatu = ToolRunStatu.Not_Enabled ;
                            treeNode.ToolTipText = codeEditTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                ((ShapeMatchTool)(L_toolList[i].tool)).runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Asign_Input_Image : ToolRunStatu.未指定输入图像);
                                treeNode.ToolTipText = codeEditTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, codeEditTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "输入项1" || inputItem == "InputImage")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " , ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3); ;
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                codeEditTool.Input1 = (GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value).ToString();
                            }

                        }
                        codeEditTool.Run(jobName, true, true);
                        for (int j = 0; j < outputItemNum; j++)
                        {
                            string outputItem = L_toolList[i].output[j].IOName;
                            switch (outputItem)
                            {
                                case "输出项1":
                                    L_toolList[i].GetOutput(outputItem).value = codeEditTool.Output1;
                                    GetToolIONodeByNodeText(L_toolList[i].toolName, "-->" + outputItem).ToolTipText = codeEditTool.Output1;
                                    break;
                            }
                        }

                        if (codeEditTool.runStatu != ToolRunStatu.Succeed)
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, codeEditTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region Label
                    else if (L_toolList[i].toolType == ToolType.Label)
                    {
                        LabelTool labelTool = (LabelTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((LabelTool)(L_toolList[i].tool)).runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Enabled  : ToolRunStatu.未启用;
                            treeNode.ToolTipText = labelTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        labelTool.D_inputItemAndVlaue.Clear();
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string inputItem = L_toolList[i].input[j].IOName;
                            string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString();
                            if (sourceFrom == string.Empty)
                            {
                                ((LabelTool)(L_toolList[i].tool)).runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源;
                                treeNode.ToolTipText = labelTool.runStatu.ToString();
                                treeNode.ForeColor = Color.Red;
                                Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, labelTool.runStatu.ToString()), Color.Red);
                                return L_result;
                            }

                            if (inputItem == "输入项1" || inputItem == "InputItem1")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                labelTool.D_inputItemAndVlaue.Add("InputItem1", GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString());
                            }
                            else if (inputItem == "输入项2" || inputItem == "InputItem2")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                labelTool.D_inputItemAndVlaue.Add("InputItem2", GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString());
                            }
                            else if (inputItem == "输入项3" || inputItem == "InputItem3")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                labelTool.D_inputItemAndVlaue.Add("InputItem3", GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString());
                            }
                            else if (inputItem == "输入项4" || inputItem == "InputItem4")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                labelTool.D_inputItemAndVlaue.Add("InputItem3", GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString());
                            }
                            else if (inputItem == "输入项5" || inputItem == "InputItem5")
                            {
                                string sourceToolName = Regex.Split(sourceFrom, " . ")[0];
                                sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, " . ")[0].Length - 3);
                                string toolItem = Regex.Split(sourceFrom, " . ")[1];
                                labelTool.D_inputItemAndVlaue.Add("InputItem5", GetToolInfoByToolName(jobName, sourceToolName).GetOutput(toolItem).value.ToString());
                            }
                        }

                        labelTool.Run(jobName, true, true);

                        if (labelTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, labelTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region Output
                    else if (L_toolList[i].toolType == ToolType.Output)
                    {
                        ConditionTool outputTool = (ConditionTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((ConditionTool)(L_toolList[i].tool)).runStatu = ToolRunStatu.Not_Enabled;
                            treeNode.ToolTipText = outputTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string sourceFrom = L_toolList[i].input[j].IOName;
                            string sourceToolName = Regex.Split(sourceFrom, " . ")[0].Substring(3);
                            string sourceToolItem = Regex.Split(sourceFrom, " . ")[1].Substring(3);
                            string value = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(sourceToolItem).value.ToString();
                            GetToolIONodeByNodeText(L_toolList[i].toolName, sourceFrom).ToolTipText = value;
                            L_toolList[i].GetInput(sourceFrom).value = value;
                            L_result.Add(value);
                        }
                        outputTool.Run(jobName, true, true);

                        if (outputTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, outputTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion

                    #region Condation
                    else if (L_toolList[i].toolType == ToolType.Condition)
                    {
                        ConditionTool outputTool = (ConditionTool)L_toolList[i].tool;
                        if (!L_toolList[i].enable)
                        {
                            ((ConditionTool)(L_toolList[i].tool)).runStatu = ToolRunStatu.Not_Enabled;
                            treeNode.ToolTipText = outputTool.runStatu.ToString();
                            treeNode.ForeColor = Color.Goldenrod;
                            continue;
                        }
                        for (int j = 0; j < inputItemNum; j++)
                        {
                            string sourceFrom = L_toolList[i].input[j].IOName;
                            string sourceToolName = Regex.Split(sourceFrom, " . ")[0].Substring(3);
                            string sourceToolItem = Regex.Split(sourceFrom, " . ")[1].Substring(3);
                            string value = GetToolInfoByToolName(jobName, sourceToolName).GetOutput(sourceToolItem).value.ToString();
                            GetToolIONodeByNodeText(L_toolList[i].toolName, sourceFrom).ToolTipText = value;
                            L_toolList[i].GetInput(sourceFrom).value = value;
                            L_result.Add(value);
                        }
                        outputTool.Run(jobName, true, true);

                        if (outputTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                        {
                            Frm_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, outputTool.runStatu.ToString()), Color.Red);
                            treeNode.ForeColor = Color.Red;
                            break;
                        }
                        else
                        {
                            treeNode.ForeColor = Color.Green;
                        }
                    }
                    #endregion
                    double elapseTime = jobElapsedTime.ElapsedMilliseconds - recordElapseTime;
                    recordElapseTime = jobElapsedTime.ElapsedMilliseconds;
                    treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n备注：{2}", ((ToolBase)L_toolList[i].tool).runStatu.ToString(), elapseTime, L_toolList[i].toolTipInfo);
                    Application.DoEvents();
                }
                for (int i = toolIndex + 1; i < L_toolList.Count; i++)
                {
                    GetToolNodeByNodeText(L_toolList[i].toolName).ForeColor = Color.Black;
                }

                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        //////HOperatorSet.DumpWindowImage(out jobResultImage, item.Value.hwc_imageWindow.HalconWindow);
                    }
                }

                foreach (Control item in Frm_UserForm.Instance.Controls)
                {
                    if (item.GetType().ToString() == "System.Windows.Forms.PictureBox" && item.Name == imageWindowName)
                    {
                        if (firstDisplayImage)
                        {
                            firstDisplayImage = false;
                            HOperatorSet.OpenWindow(0, 0, item.Size.Width, item.Size.Height, item.Handle, "visible", "", out imageWindow);
                        }
                        Frm_ImageWindow.Instance.Display_Image(jobResultImage, imageWindow);
                    }
                }

                Project.GetJobTree(jobName).SelectedNode = null;
                jobElapsedTime.Stop();
                double time = jobElapsedTime.ElapsedMilliseconds;
                //自动运行状态下结果不显示
                Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "The process was successfully run,Elapsed：" + time + "ms" : "流程运行成功，耗时：" + time + "ms", Color.Green);

                jobRunStatu = JobRunStatu.Succeed;
                Application.DoEvents();
                return L_result;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }

    }
    public enum JobRunStatu
    {
        Succeed,
        Fail,
    }
}




