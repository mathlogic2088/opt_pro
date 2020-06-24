using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Management;
using System.Data.Common;
using System.Data.OleDb;
using System.Net.Sockets;
using System.Net;
using System.Drawing.Imaging;
using System.Net.NetworkInformation;
using WeifenLuo.WinFormsUI.Docking;
using CameraHandle = System.Int32;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Resources;
using Microsoft.Win32;
using VisionAndMotionPro.Properties;
using SACTools;
using gts;

using Tool;
using System.Xml;
using HalconDotNet;
using Newtonsoft.Json;
using System.Runtime.InteropServices;


namespace VisionAndMotionPro
{
    internal partial class Frm_Main : Form
    {
        internal Frm_Main()
        {
            deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            //初始化窗体控件
            try
            {
                InitializeComponent();
                dockPanel.DefaultFloatWindowSize = new System.Drawing.Size(260, 500);
                Control.CheckForIllegalCrossThreadCalls = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(""))
                {
                    DialogResult result = MessageBox.Show(Configuration.language == Language.English ? "An image with the same name already exists, is it overwritten?" : "启动失败，本机未安装此程序所依赖的组件，是否立即安装？", Configuration.language == Language.English ? "Tip:" : "提示：", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start(Application.StartupPath + "\\DotNetBarSetupTrial_140015.msi");
                    }
                }
                else
                    Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Failed to start!, suspected that Halcon is missing a usable License file 或者系统平台不正确" : "启动失败，Halcon已过期或者系统平台不正确");
                Process.GetCurrentProcess().Kill();
            }
            Init_Language();
        }

        #region 作者相关

        //************************************************************************************************************************************************************************ 
        //*  Author:                              Kim Li                                                                                                                         *
        //*  Guidance Teacher:                    NoBody                                                                                                                         *
        //*  Write Date:                          2018-05-02                                                                                                                     *
        //*  Apartment:                           TianJi Third Apartment                                                                                                         *
        //*  Welcome Sentence:                    Welcome to browse this source code                                                                                             *
        //*  Notes:                               Please do not modify without author's permission                                                                               *
        //*  Record items:                        ①Nothing                                                                                                                      *
        //*  Version Info and Update Details:     ①Vision Number:20180502 1.0.0.0（Initial Vision）                                                                             *  
        //*                                       Update Details:Nothing                                                                                                         *
        //*  Description:                         This software is based on Halcon as a visual processing software, only for personal learning. Due to personal time relations,  *
        //*                                       there are still many bugs and deficiencies in the software. Later, new versions will be continuously updated to optimize and   *
        //*                                       improve the deficiencies. At the same time, users who have better suggestions or corrections are welcome to inform me. Thank   *
        //*                                       you for using                                                                                                                  *
        //************************************************************************************************************************************************************************

        #endregion

        #region 变量定义

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_Main _instance;
        public static Frm_Main Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_Main();
                return _instance;
            }
        }
        /// <summary>
        /// 程序即将退出
        /// </summary>
        internal static bool willExit = false;
        /// <summary>
        /// 是否启用事件，也就是不执行本次触发的事件
        /// </summary>
        internal static bool ignore = true;
        /// <summary>
        /// 是否停止运行
        /// </summary>
        internal static bool isStopRun = false;
        /// <summary>
        /// 指示是否允许拖动和缩放
        /// </summary>
        public static bool allowScaleAndZoom = false;
        /// <summary>
        /// 图像窗体集合
        /// </summary>
        public Dictionary<string, Frm_ImageWindow> D_imageWindow = new Dictionary<string, Frm_ImageWindow>();
        /// <summary>
        /// 指示是否已按下Ctrl+E组合键
        /// </summary>
        private bool controlE = false;
        /// <summary>
        /// 图像窗口编号
        /// </summary>
        private int imageWindowIndex = 0;
        /// <summary>
        /// 反序列化Dock控件对象
        /// </summary>
        internal DeserializeDockContent deserializeDockContent;
        /// <summary>
        /// 监控线程
        /// </summary>
        public static Thread th_update;
        /// <summary>
        /// 指示是否启用了全屏模式
        /// </summary>
        internal static bool fullScreen = false;
        /// <summary>
        /// 注册码
        /// </summary>
        public string regiestCode;
        /// <summary>
        /// 累计时间
        /// </summary>
        public int elapsedTime = 0;
        /// <summary>
        /// 贪吃蛇游戏进程
        /// </summary>
        internal Process processGreedSnake;
        /// <summary>
        /// 虚拟键盘进程
        /// </summary>
        internal Process processKeyBoard;
        /// <summary>
        /// 配置文件读写对象
        /// </summary>
        private static Ini iniConfig = new Ini(Application.StartupPath + "\\Config.ini");
        /// <summary>
        /// 轴配置窗体对象
        /// </summary>
        private static Frm_AxisSetting frm_axisSetting = new Frm_AxisSetting();

        #endregion

        #region 函数定义

        private void Init_Language()
        {
            try
            {
                if (Configuration.language == Language.English)
                {



                    ////btn_runOnce.ToolTipText = "Run once";
                    ////tsb_runLoop.ToolTipText = "Run loop";

                    tss_permissionInfo.Text = "Current login: not logged in, default is minimum permission";
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 整体保存
        /// </summary>
        internal void SaveAll()
        {
            try
            {
                foreach (TabPage item in Frm_Job.Instance.tbc_jobs.TabPages)
                {
                    //如果本地没有此流程，则可能是临时读取的流程，返回，不保存
                    if (Frm_Job.Instance.tbc_jobs.TabCount > 0 && File.Exists(Application.StartupPath + "\\Config\\Vision\\Job\\" + item.Text + ".job"))
                        Save(item.Text);
                }
                //////Frm_Job.Instance.Dock = DockStyle.Fill;
                //////Frm_Job.Instance.Show();
                //////Frm_Job.Instance.Dock = DockStyle.Fill;
                Frm_Main.Instance.OutputMsg("保存成功", Color.Green);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        private IDockContent GetContentFromPersistString(string persistString)
        {
            try
            {
                if (persistString == typeof(Frm_Job).ToString())
                    return Frm_Job.Instance;
                else if (persistString == typeof(Frm_Tools).ToString())
                    return Frm_Tools.Instance;
                else if (persistString == typeof(Frm_ImageWindow).ToString() && imageWindowIndex == 0)
                {
                    imageWindowIndex++;
                    D_imageWindow.Add(Configuration.language == Language.English ? "Image" : "图像", Frm_ImageWindow.Instance);
                    return Frm_ImageWindow.Instance;
                }
                else if (persistString == typeof(Frm_Output).ToString())
                    return Frm_Output.Instance;
                //else if (persistString == typeof(Frm_Monitor).ToString())
                //    return Frm_Monitor.Instance;
                //else if (persistString == typeof(Frm_IO).ToString())
                //    return Frm_IO.Instance;
                //else if (persistString == typeof(Frm_MotionControl).ToString())
                //    return Frm_Monitor.Instance;
                //else if (persistString == typeof(Frm_Omniselector).ToString())
                //    return Frm_Omniselector.Instance;
                else
                {
                    string[] parsedStrings = persistString.Split(new char[] { ',' });

                    if (parsedStrings[0] != typeof(Frm_ImageWindow).ToString())
                        return null;

                    Frm_ImageWindow dummyDoc = new Frm_ImageWindow();
                    dummyDoc.Text = Configuration.language == Language.English ? "Image" : "图像" + imageWindowIndex;
                    D_imageWindow.Add(Configuration.language == Language.English ? "Image" : "图像" + imageWindowIndex, dummyDoc);

                    imageWindowIndex++;
                    return dummyDoc;
                }
                return Frm_Tools.Instance;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }

        /// <summary>
        /// 创建新流程
        /// </summary>
        internal void Create_New_Job()
        {
            try
            {
            Again:
                Frm_InputMessage.Instance.lal_title.Text = (Configuration.language == Language.English ? "Please input job's name" : "请输入流程名");
                Frm_InputMessage.Instance.btn_confirm.Text = (Configuration.language == Language.English ? "Confirm" : "确定");
                Frm_InputMessage.Instance.passwordChar = false;
                Frm_InputMessage.Instance.txt_input.Clear();
                Frm_InputMessage.Instance.ShowDialog();
                string jobName = Frm_InputMessage.input;
                if (jobName == "")
                    return;

                //检查此名称的流程是否已存在
                if (Job.Job_Exist(jobName))
                {
                    Frm_MessageBox.Instance.MessageBoxShow((Configuration.language == Language.English ? "\r\nA process with this name already exists. The process name cannot be repeated. Please enter again" : "\r\n已存在此名称的流程，流程名不可重复，请重新输入"));
                    goto Again;
                }
                //检查此名称是否含有特殊字符\
                if (jobName.Contains(@"\"))
                {
                    Frm_MessageBox.Instance.MessageBoxShow((Configuration.language == Language.English ? "\r\nA process with this name already exists. The process name cannot be repeated. Please enter again" : "\r\n流程名中不能含有 \\ 等特殊字符 ，请重新输入"));
                    goto Again;
                }
                LogHelper.SaveLog(LogType.Operate, (Configuration.language == Language.English ? "A new process named:" + jobName : "创建了新流程，流程名为：" + jobName));

                Job job = new Job();
                job.jobName = jobName;
                Project.Instance.L_jobList.Add(job);
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

                tvw_job.Dock = DockStyle.Fill;
                tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                Frm_Job.Instance.tbc_jobs.TabPages.Add(jobName);
                Frm_Job.Instance.tbc_jobs.TabPages[Frm_Job.Instance.tbc_jobs.TabPages.Count - 1].Controls.Add(tvw_job);
                Frm_Job.Instance.tbc_jobs.SelectedIndex = Frm_Job.Instance.tbc_jobs.TabCount - 1;
                Application.DoEvents();

                //默认添加ImageAcquistionTool工具
                Frm_Tools.Instance.Add_Tool(Configuration.language == Language.English ? "HalconAcqInterface" : "Halcon采集接口");

                //默认选中第一个工具节点
                tvw_job.SelectedNode = tvw_job.Nodes[0];

                //展开已默认添加的工具的输入输出项
                tvw_job.ExpandAll();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 显示对象
        /// </summary>
        /// <param name="obj">要显示的对象</param>
        internal void Display_Obj(HTuple hw, HObject obj)
        {
            try
            {
                //有开启全屏
                if (fullScreen)
                {
                    HOperatorSet.DispObj(obj, Frm_FullScreen.Instance.windowHandle);
                }
                else
                {
                    HOperatorSet.DispObj(obj, hw);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 开机自启动
        /// </summary>
        /// <param name="isAuto">是否启用</param>
        internal static void Auto_Start(bool isAuto)
        {
            try
            {
                if (isAuto == true)
                {
                    RegistryKey R_local = Registry.LocalMachine;        //RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.SetValue("应用名称", Application.ExecutablePath);
                    R_run.Close();
                    R_local.Close();
                }

                else
                {
                    RegistryKey R_local = Registry.LocalMachine;        //RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.DeleteValue("应用名称", false);
                    R_run.Close();
                    R_local.Close();
                }
            }
            catch (Exception)
            {
                Frm_MessageBox.Instance.MessageBoxShow("\r\n开机程序自启动设置失败，请以管理员权限运行此程序后重新尝试");
            }
        }

        /// <summary>
        /// 信息输出
        /// </summary>
        /// <param name="msg">要输出的信息</param>
        /// <param name="color">背景颜色</param>
        public void OutputMsg(string msg, Color color)
        {
            try
            {
                Frm_Output.Instance.OutputMsg(msg, color);
                elapsedTime = 0;
                lbl_output.Text = DateTime.Now.ToString("HH:mm:ss") + "    " + msg;
                if (color == Color.Red)
                {
                    lbl_output.ForeColor = Color.Black;
                    lbl_output.BackColor = color;
                }
                else
                {
                    lbl_output.ForeColor = color;
                    lbl_output.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 整体保存
        /// </summary>
        internal static string Save(string tabText)
        {
            try
            {
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count == 0)
                {
                    return "";
                }

                string jobName = tabText;
                Job job = Job.GetJobByName(jobName);

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Application.StartupPath + "\\Config\\Vision\\Job\\" + job.jobName + ".job", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, job);
                stream.Close();

                //更新结果下拉框
                Frm_ImageWindow.Instance.Update_Last_Run_Result_Image_List();
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Program saved successfully" : "程序保存成功");
                Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Program saved successfully" : "保存成功", Color.Green);
                return jobName;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return "";
            }
        }
        /// <summary>
        /// 整体保存
        /// </summary>
        internal static string Save()
        {
            try
            {
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count == 0)
                {
                    return "";
                }

                string jobName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
                Job job = Job.GetJobByName(jobName);

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Application.StartupPath + "\\Config\\Vision\\Job\\" + job.jobName + ".job", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, job);
                stream.Close();

                //更新结果下拉框
                Frm_ImageWindow.Instance.Update_Last_Run_Result_Image_List();
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Program saved successfully" : "程序保存成功");
                Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Program saved successfully" : "保存成功", Color.Green);
                return jobName;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return "";
            }
        }

        /// <summary>
        /// 初始化工具提示
        /// </summary>
        public void Init_Tool_Tips()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 10;
            toolTip.ReshowDelay = 10;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(Frm_Job.Instance.pic_createJob, Configuration.language == Language.English ? "Expand job tree" : "新建流程");
            toolTip.SetToolTip(Frm_Job.Instance.pic_expandJobTree, Configuration.language == Language.English ? "Expand job tree" : "展开流程树");
            toolTip.SetToolTip(Frm_Job.Instance.pic_foldJobTree, Configuration.language == Language.English ? "Fold job tree" : "折叠树");
            toolTip.SetToolTip(Frm_Job.Instance.pic_deleteJob, Configuration.language == Language.English ? "Delete job" : "删除当前流程");
            toolTip.SetToolTip(Frm_Job.Instance.pic_jobProperty, Configuration.language == Language.English ? "Home program" : "当前流程属性");
        }

        /// <summary>
        /// 读取图像
        /// </summary>
        internal void Read_Image()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openImage = new System.Windows.Forms.OpenFileDialog();
                dig_openImage.FileName = "";
                dig_openImage.Title = Configuration.language == Language.English ? "Please select image path" : "请选择图像文件";
                dig_openImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_openImage.Filter = Configuration.language == Language.English ? "Image File(*.*)|*.*|Image File(*.png)|*.txt|Image File(*.jpg)|*.jpg|Image File(*.bmp)|*.bmp|Image File(*.tif)|*.tif" : "图像文件(*.tif)|*.tif|图像文件(*.png)|*.txt|图像文件(*.jpg)|*.jpg|图像文件(*.bmp)|*.bmp|图像文件(*.*)|*.*";
                dig_openImage.ShowDialog();
                if (dig_openImage.FileName == "")
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "No image path" : "无图像路径", Color.Green);
                    return;
                }
                HObject image;
                try
                {
                    HOperatorSet.ReadImage(out image, dig_openImage.FileName);
                }
                catch
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Unable to read specified file" : "图像文件异常，无法读取", Color.Red);
                    return;
                }
                Frm_ImageWindow.Instance.Display_Image(image);
                Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Loading Image successfully" : "读取图像成功", Color.Green);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 旋转图像
        /// </summary>
        /// <param name="angle"></param>
        private void Rotate_Image(double angle)
        {
            try
            {
                HObject imageAfterRotate;
                HOperatorSet.RotateImage(Frm_ImageWindow.currentImage, out imageAfterRotate, (HTuple)(angle), "constant");
                Frm_ImageWindow.Instance.Display_Image(imageAfterRotate);
                Frm_Main.Instance.OutputMsg("Image rotatate" + angle + "degree Successly", Color.Green);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 创建新的图像窗体
        /// </summary>
        /// <returns></returns>
        internal Frm_ImageWindow CreateNewImageWindow()
        {
            try
            {
                Frm_ImageWindow dummyDoc = new Frm_ImageWindow();

                int count = 1;
                string text = Configuration.language == Language.English ? "Image" : "图像" + count.ToString();
                while (FindImageWindow(text) != null)
                {
                    count++;
                    text = Configuration.language == Language.English ? "Image" : "图像" + count.ToString();
                }
                dummyDoc.Text = text;
                return dummyDoc;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return new Frm_ImageWindow();
            }
        }

        /// <summary>
        /// 在图像中显示字符串
        /// </summary>
        internal void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem, HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {
            try
            {
                HTuple hv_M = null, hv_N = null, hv_Red = null;
                HTuple hv_Green = null, hv_Blue = null, hv_RowI1Part = null;
                HTuple hv_ColumnI1Part = null, hv_RowI2Part = null, hv_ColumnI2Part = null;
                HTuple hv_RowIWin = null, hv_ColumnIWin = null, hv_WidthWin = null;
                HTuple hv_HeightWin = null, hv_I = null, hv_RowI = new HTuple();
                HTuple hv_ColumnI = new HTuple(), hv_StringI = new HTuple();
                HTuple hv_MaxAscent = new HTuple(), hv_MaxDescent = new HTuple();
                HTuple hv_MaxWidth = new HTuple(), hv_MaxHeight = new HTuple();
                HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRowI = new HTuple();
                HTuple hv_FactorColumnI = new HTuple(), hv_UseShadow = new HTuple();
                HTuple hv_ShadowColor = new HTuple(), hv_Exception = new HTuple();
                HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
                HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
                HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
                HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
                HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
                HTuple hv_CurrentColor = new HTuple();
                HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
                HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
                HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
                HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
                HTuple hv_String_COPY_INP_TMP = hv_String.Clone();
                if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                {
                    hv_Color_COPY_INP_TMP = "";
                }
                if ((int)(new HTuple(hv_Box_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                {
                    hv_Box_COPY_INP_TMP = "false";
                }
                hv_M = (new HTuple(hv_Row_COPY_INP_TMP.TupleLength())) * (new HTuple(hv_Column_COPY_INP_TMP.TupleLength()
                    ));
                hv_N = new HTuple(hv_Row_COPY_INP_TMP.TupleLength());
                if ((int)((new HTuple(hv_M.TupleEqual(0))).TupleOr(new HTuple(hv_String_COPY_INP_TMP.TupleEqual(
                    new HTuple())))) != 0)
                {
                    return;
                }
                if ((int)(new HTuple(hv_M.TupleNotEqual(1))) != 0)
                {
                    if ((int)(new HTuple((new HTuple(hv_Row_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        hv_N = new HTuple(hv_Column_COPY_INP_TMP.TupleLength());
                        HOperatorSet.TupleGenConst(hv_N, hv_Row_COPY_INP_TMP, out hv_Row_COPY_INP_TMP);
                    }
                    else if ((int)(new HTuple((new HTuple(hv_Column_COPY_INP_TMP.TupleLength()
                        )).TupleEqual(1))) != 0)
                    {
                        HOperatorSet.TupleGenConst(hv_N, hv_Column_COPY_INP_TMP, out hv_Column_COPY_INP_TMP);
                    }
                    else if ((int)(new HTuple((new HTuple(hv_Column_COPY_INP_TMP.TupleLength()
                        )).TupleNotEqual(new HTuple(hv_Row_COPY_INP_TMP.TupleLength())))) != 0)
                    {
                        throw new HalconException("Number of elements in Row and Column does not match.");
                    }
                    if ((int)(new HTuple((new HTuple(hv_String_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        HOperatorSet.TupleGenConst(hv_N, hv_String_COPY_INP_TMP, out hv_String_COPY_INP_TMP);
                    }
                    else if ((int)(new HTuple((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                        )).TupleNotEqual(hv_N))) != 0)
                    {
                        throw new HalconException("Number of elements in Strings does not match number of positions.");
                    }
                }
                HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
                HOperatorSet.GetPart(hv_WindowHandle, out hv_RowI1Part, out hv_ColumnI1Part,
                    out hv_RowI2Part, out hv_ColumnI2Part);
                HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowIWin, out hv_ColumnIWin,
                    out hv_WidthWin, out hv_HeightWin);
                HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
                HTuple end_val89 = hv_N - 1;
                HTuple step_val89 = 1;
                for (hv_I = 0; hv_I.Continue(end_val89, step_val89); hv_I = hv_I.TupleAdd(step_val89))
                {
                    hv_RowI = hv_Row_COPY_INP_TMP.TupleSelect(hv_I);
                    hv_ColumnI = hv_Column_COPY_INP_TMP.TupleSelect(hv_I);
                    if ((int)(new HTuple(hv_N.TupleEqual(1))) != 0)
                    {
                        hv_StringI = hv_String_COPY_INP_TMP.Clone();
                    }
                    else
                    {
                        hv_StringI = hv_String_COPY_INP_TMP.TupleSelect(hv_I);
                    }
                    if ((int)(new HTuple(hv_RowI.TupleEqual(-1))) != 0)
                    {
                        hv_RowI = 12;
                    }
                    if ((int)(new HTuple(hv_ColumnI.TupleEqual(-1))) != 0)
                    {
                        hv_ColumnI = 12;
                    }
                    hv_StringI = ((("" + hv_StringI) + "")).TupleSplit("\n");
                    HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                        out hv_MaxWidth, out hv_MaxHeight);
                    if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
                    {
                        hv_R1 = hv_RowI.Clone();
                        hv_C1 = hv_ColumnI.Clone();
                    }
                    else
                    {
                        hv_FactorRowI = (1.0 * hv_HeightWin) / ((hv_RowI2Part - hv_RowI1Part) + 1);
                        hv_FactorColumnI = (1.0 * hv_WidthWin) / ((hv_ColumnI2Part - hv_ColumnI1Part) + 1);
                        hv_R1 = (((hv_RowI - hv_RowI1Part) + 0.5) * hv_FactorRowI) - 0.5;
                        hv_C1 = (((hv_ColumnI - hv_ColumnI1Part) + 0.5) * hv_FactorColumnI) - 0.5;
                    }
                    hv_UseShadow = 1;
                    hv_ShadowColor = "gray";
                    if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
                    {
                        if (hv_Box_COPY_INP_TMP == null)
                            hv_Box_COPY_INP_TMP = new HTuple();
                        hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                        hv_ShadowColor = "#f28d26";
                    }
                    if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                        1))) != 0)
                    {
                        if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                        {
                        }
                        else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                            "false"))) != 0)
                        {
                            hv_UseShadow = 0;
                        }
                        else
                        {
                            hv_ShadowColor = hv_Box_COPY_INP_TMP.TupleSelect(1);
                            try
                            {
                                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                                    1));
                            }
                            catch (HalconException HDevExpDefaultException1)
                            {
                                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                                hv_Exception = new HTuple("Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)");
                                throw new HalconException(hv_Exception);
                            }
                        }
                    }
                    if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
                    {
                        try
                        {
                            HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                                0));
                        }
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            hv_Exception = new HTuple("Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)");
                            throw new HalconException(hv_Exception);
                        }
                        hv_StringI = (" " + hv_StringI) + " ";
                        hv_Width = new HTuple();
                        for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_StringI.TupleLength()
                            )) - 1); hv_Index = (int)hv_Index + 1)
                        {
                            HOperatorSet.GetStringExtents(hv_WindowHandle, hv_StringI.TupleSelect(hv_Index),
                                out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                            hv_Width = hv_Width.TupleConcat(hv_W);
                        }
                        hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_StringI.TupleLength()));
                        hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                        hv_R2 = hv_R1 + hv_FrameHeight;
                        hv_C2 = hv_C1 + hv_FrameWidth;
                        HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                        HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                        HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                        if ((int)(hv_UseShadow) != 0)
                        {
                            HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1,
                                hv_C2 + 1);
                        }
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                        HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                        HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
                    }
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_StringI.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        if ((int)(new HTuple(hv_N.TupleEqual(1))) != 0)
                        {
                            hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                                )));
                        }
                        else
                        {
                            hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_I % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                                )));
                        }
                        if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                            "auto")))) != 0)
                        {
                            try
                            {
                                HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                            }
                            catch (HalconException HDevExpDefaultException1)
                            {
                                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                                hv_Exception = ((("Wrong value of control parameter Color[" + (hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                                    )))) + "] == '") + hv_CurrentColor) + "' (must be a valid color string)";
                                throw new HalconException(hv_Exception);
                            }
                        }
                        else
                        {
                            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                        }
                        hv_RowI = hv_R1 + (hv_MaxHeight * hv_Index);
                        HOperatorSet.SetTposition(hv_WindowHandle, hv_RowI, hv_C1);
                        HOperatorSet.WriteString(hv_WindowHandle, hv_StringI.TupleSelect(hv_Index));
                    }
                }
                HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                HOperatorSet.SetPart(hv_WindowHandle, hv_RowI1Part, hv_ColumnI1Part, hv_RowI2Part,
                    hv_ColumnI2Part);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 显示模板匹配结果
        /// </summary>
        internal void dev_display_shape_Match_results(HTuple hv_ModelID, HTuple hv_Color, HTuple hv_Row, HTuple hv_Column, HTuple hv_Angle, HTuple hv_ScaleR, HTuple hv_ScaleC, HTuple hv_Model)
        {
            try
            {
                HObject ho_ModelContours = null, ho_ContoursAffinTrans = null;
                HTuple hv_NumMatches = null, hv_Index = new HTuple();
                HTuple hv_Match = new HTuple(), hv_HomMat2DIdentity = new HTuple();
                HTuple hv_HomMat2DScale = new HTuple(), hv_HomMat2DRotate = new HTuple();
                HTuple hv_HomMat2DTranslate = new HTuple();
                HTuple hv_Model_COPY_INP_TMP = hv_Model.Clone();
                HTuple hv_ScaleC_COPY_INP_TMP = hv_ScaleC.Clone();
                HTuple hv_ScaleR_COPY_INP_TMP = hv_ScaleR.Clone();
                HOperatorSet.GenEmptyObj(out ho_ModelContours);
                HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans);
                hv_NumMatches = new HTuple(hv_Row.TupleLength());
                if ((int)(new HTuple(hv_NumMatches.TupleGreater(0))) != 0)
                {
                    if ((int)(new HTuple((new HTuple(hv_ScaleR_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleR_COPY_INP_TMP, out hv_ScaleR_COPY_INP_TMP);
                    }
                    if ((int)(new HTuple((new HTuple(hv_ScaleC_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleC_COPY_INP_TMP, out hv_ScaleC_COPY_INP_TMP);
                    }
                    if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength())).TupleEqual(
                        0))) != 0)
                    {
                        HOperatorSet.TupleGenConst(hv_NumMatches, 0, out hv_Model_COPY_INP_TMP);
                    }
                    else if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength()
                        )).TupleEqual(1))) != 0)
                    {
                        HOperatorSet.TupleGenConst(hv_NumMatches, hv_Model_COPY_INP_TMP, out hv_Model_COPY_INP_TMP);
                    }
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ModelID.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        ho_ModelContours.Dispose();
                        HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelID.TupleSelect(
                            hv_Index), 1);
                        HOperatorSet.SetColor(Frm_Main.fullScreen ? Frm_FullScreen.Instance.windowHandle : Frm_ImageWindow.Instance.WindowHandle, hv_Color.TupleSelect(
                            hv_Index % (new HTuple(hv_Color.TupleLength()))));
                        HTuple end_val18 = hv_NumMatches - 1;
                        HTuple step_val18 = 1;
                        for (hv_Match = 0; hv_Match.Continue(end_val18, step_val18); hv_Match = hv_Match.TupleAdd(step_val18))
                        {
                            if ((int)(new HTuple(hv_Index.TupleEqual(hv_Model_COPY_INP_TMP.TupleSelect(
                                hv_Match)))) != 0)
                            {
                                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                                HOperatorSet.HomMat2dScale(hv_HomMat2DIdentity, hv_ScaleR_COPY_INP_TMP.TupleSelect(
                                    hv_Match), hv_ScaleC_COPY_INP_TMP.TupleSelect(hv_Match), 0, 0, out hv_HomMat2DScale);
                                HOperatorSet.HomMat2dRotate(hv_HomMat2DScale, hv_Angle.TupleSelect(hv_Match),
                                    0, 0, out hv_HomMat2DRotate);
                                HOperatorSet.HomMat2dTranslate(hv_HomMat2DRotate, hv_Row.TupleSelect(
                                    hv_Match), hv_Column.TupleSelect(hv_Match), out hv_HomMat2DTranslate);
                                ho_ContoursAffinTrans.Dispose();
                                HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans,
                                    hv_HomMat2DTranslate);
                                //HOperatorSet.DispObj(ho_ContoursAffinTrans, Frm_ImageWindow.Instance.dip_displayImage.HalconWindow);
                                Display_Obj(Frm_ImageWindow.Instance.WindowHandle, ho_ContoursAffinTrans);
                            }
                        }
                    }
                }
                ho_ModelContours.Dispose();
                ho_ContoursAffinTrans.Dispose();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 把一个目录下的文件拷贝的目标目录下
        /// </summary>
        /// <param name="sourceFolder">源目录</param>
        /// <param name="targerFolder">目标目录</param>
        /// <param name="removePrefix">移除文件名部分路径</param>
        internal static void CopyFiles(string sourceFolder, string targerFolder, string removePrefix = "")
        {
            try
            {
                if (string.IsNullOrEmpty(removePrefix))
                {
                    removePrefix = sourceFolder;
                }
                if (!Directory.Exists(targerFolder))
                {
                    Directory.CreateDirectory(targerFolder);
                }
                DirectoryInfo directory = new DirectoryInfo(sourceFolder);
                //获取目录下的文件
                FileInfo[] files = directory.GetFiles();
                foreach (FileInfo item in files)
                {
                    if (item.Name == "Thumbs.db")
                    {
                        continue;
                    }
                    string tempPath = item.FullName.Replace(removePrefix, string.Empty);
                    tempPath = targerFolder + tempPath;
                    FileInfo fileInfo = new FileInfo(tempPath);
                    if (!fileInfo.Directory.Exists)
                    {
                        fileInfo.Directory.Create();
                    }
                    File.Delete(tempPath);
                    item.CopyTo(tempPath, true);
                }
                //获取目录下的子目录
                DirectoryInfo[] directors = directory.GetDirectories();
                foreach (var item in directors)
                {
                    CopyFiles(item.FullName, targerFolder, removePrefix);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 获取图像窗体
        /// </summary>
        /// <param name="text">窗体名</param>
        /// <returns></returns>
        private IDockContent FindImageWindow(string text)
        {
            try
            {
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    foreach (Form form in MdiChildren)
                        if (form.Text == text)
                            return form as IDockContent;
                    return null;
                }
                else
                {
                    foreach (IDockContent content in dockPanel.Documents)
                        if (content.DockHandler.TabText == text)
                            return content;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }

        #endregion

        #region 相关事件
        private void tss_permissionInfo_Click(object sender, EventArgs e)
        {
            Frm_Login.Instance.ShowDialog();
        }
        private void btn_startRun_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Operator))
                return;
            Machine.StartRun();
        }
        private void Frm_Main_Resize(object sender, EventArgs e)
        {
            lbl_output.Size = new Size(statusStrip1.Size.Width - lbl_runStatu.Size.Width - tss_curTime.Size.Width - tss_permissionInfo.Size.Width - 15, lbl_output.Size.Height);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if ((keyData & Keys.Control) == Keys.Control)
                {
                    if ((keyData & Keys.M) == Keys.M)
                    {
                        btn_changeMode_Click(null, null);
                    }
                    if ((keyData & Keys.W) == Keys.W)
                    {
                        controlE = true;
                        return true;
                    }
                    else if ((keyData & Keys.X) == Keys.X && controlE)
                    {
                        if (Frm_Tools.Instance.DockState == DockState.Hidden || Frm_Tools.Instance.DockState == DockState.Unknown)
                        {
                            Frm_Tools.Instance.Show(dockPanel, DockState.DockLeft);
                        }
                    }
                    else if ((keyData & Keys.S) == Keys.S && controlE)
                    {
                        Frm_Job.Instance.Show(dockPanel, DockState.DockRight);
                    }
                    else if ((keyData & Keys.E) == Keys.E && controlE)
                    {
                        Frm_ImageWindow.Instance.Show(dockPanel, DockState.DockTop);
                    }
                }
                controlE = false;
                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return false;
            }
        }
        public void cbo_standardImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HObject image;
                image = Job.D_standardImage[Frm_JobInfo.Instance.cbx_standardImage.Text];
                Frm_ImageWindow.Instance.Display_Image(image);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Opacity = 0;

                try
                {
                    switch (Configuration.cardType)
                    {
                        case CardType.雷塞_DMC2210:
                            csDmc2210.Dmc2210.d2210_board_close();
                            break;
                        case CardType.雷塞_DMC2410:
                            csDmc2410.Dmc2410.d2410_board_close();
                            break;
                    }
                }
                catch { }
                try
                {
                    Camera_Basler.CloseAllCamera();
                }
                catch { }       //此处要Try起来，不然没有安装Phyon的电脑这里会异常
                Frm_CloseTip frm_closeTip = new Frm_CloseTip();
                frm_closeTip.Show();

                //清除图像窗体里面的ROI
                Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.resetWindowImage();
                Frm_ImageWindow.Instance.hwc_imageWindow.ClearWindow();
                Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow._hWndControl.roiManager.reset();
                Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow._hWndControl.roiManager.ROIList.Clear();

                Application.DoEvents();
                this.ShowInTaskbar = false;
                //关闭已连接的Socket
                if (Frm_TCPClient.Instance.socket != null && Frm_TCPClient.Instance.socket.Connected)
                {
                    Frm_TCPClient.Instance.socket.Disconnect(false);
                }
                if (Frm_TCPServer.Instance.commSkt != null && Frm_TCPServer.Instance.commSkt.Connected)
                {
                    Frm_TCPServer.Instance.commSkt.Disconnect(false);
                }
                //保存配置信息
                Configuration.Save();

                //要返回到作业编辑界面以下，否则会有一些修改过得参数不能保存
                if (Configuration.saveWhenExit)
                    SaveAll();

                //////if (Frm_TCPClient.Instance.socket != null && Frm_TCPClient.Instance.socket.Connected)
                //////{
                //////    Frm_TCPClient.Instance.socket.Disconnect(false);
                //////    Frm_TCPClient.Instance.socket.Close();
                //////}
                try
                {
                    Camera.Close_All_Camera();
                }
                catch { }
                //自我备份
                try
                {
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    if (Directory.Exists(@"C:\Kim\" + date))
                    {
                        Directory.Delete(@"C:\Kim\" + date, true);
                        Directory.CreateDirectory(@"C:\Kim\" + date);
                    }
                    CopyFiles(Application.StartupPath, @"C:\Kim\" + date);
                }
                catch { }
                LogHelper.SaveLog(LogType.Operate, "程序关闭\r\n");
                frm_closeTip.Close();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_Main_Shown(object sender, EventArgs e)
        {
            try
            {
                Frm_Welcome.Instance.Hide();
                string localRegiestCode = iniConfig.IniReadValue("Regiest", "RegiestCode");

                //////string needLogin = iniConfig.IniReadValue("Login", "NeedPassword");
                //////if (localRegiestCode != regiestCode)
                //////{
                //////    Frm_Regiest.Instance.ShowDialog();
                //////}
                Application.DoEvents();
                //  Thread.Sleep(1000);
                this.Opacity = 1;

                if (Configuration.autoLockAfterStart)
                {
                    Frm_Lock frm_lock = new Frm_Lock();
                    frm_lock.ShowDialog();
                }

                if (Configuration.showProductionFormAfterStart)
                    Machine.SwitchToProductForm();
                else
                    Machine.SwitchToDebugForm();
                if (Frm_Job.Instance.tbc_jobs.TabCount > 0)
                    Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text).DrawLine();
                lbl_output.Size = new Size(statusStrip1.Size.Width - lbl_runStatu.Size.Width - tss_curTime.Size.Width - tss_permissionInfo.Size.Width - 15, lbl_output.Size.Height);
                //////Frm_Login frm_login = new Frm_Login();
                //////frm_login.ShowDialog();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }

        }
        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Frm_Main.willExit = true;
                if (processGreedSnake != null && !processGreedSnake.HasExited)
                    processGreedSnake.Kill();
                if (processKeyBoard != null && !processKeyBoard.HasExited)
                    processKeyBoard.Kill();

                string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
                dockPanel.SaveAsXml(configFile);

                //////if (经典布局1ToolStripMenuItem.Checked)
                //////    Configuration.layoutFilePath = Configuration.layoutFilePath = Application.StartupPath + "\\Resources\\Layout\\" + "ClassicalLayout1.config";
                //////else if (经典布局2ToolStripMenuItem.Checked)
                //////    Configuration.layoutFilePath = Configuration.layoutFilePath = Application.StartupPath + "\\Resources\\Layout\\" + "ClassicalLayout2.config";
                //////else if (经典布局3ToolStripMenuItem.Checked)
                //////    Configuration.layoutFilePath = Configuration.layoutFilePath = Application.StartupPath + "\\Resources\\Layout\\" + "ClassicalLayout3.config";
                //////else
                //////    Configuration.layoutFilePath = configFile;
                Configuration.mainFormWidth = this.Size.Width;
                Configuration.mainFormHeight = this.Size.Height;


                Frm_Job.Instance.Hide();

            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (File.Exists(Configuration.layoutFilePath))
                        Frm_Main.Instance.dockPanel.LoadFromXml(Configuration.layoutFilePath, Frm_Main.Instance.deserializeDockContent);
                }
                catch { }

                ToolTip toolTip = new ToolTip();
                toolTip.AutoPopDelay = 5000;
                toolTip.InitialDelay = 10;
                toolTip.ReshowDelay = 10;
                toolTip.ShowAlways = true;
                //////toolTip.SetToolTip(btn_startRun, Configuration.language == Language.English ? "Start" : "开始运行");
                //////toolTip.SetToolTip(btn_stopRun, Configuration.language == Language.English ? "Stop" : "停止运行");
                //////toolTip.SetToolTip(btn_allHome, Configuration.language == Language.English ? "Home" : "整体复位");
                //////toolTip.SetToolTip(btn_changeMode, Configuration.language == Language.English ? "Product/debug" : "生产/调试切换");
                //////toolTip.SetToolTip(btn_changeUser, Configuration.language == Language.English ? "Switch user" : "切换用户");
                //////toolTip.SetToolTip(btn_exit, Configuration.language == Language.English ? "Exit" : "退出");

                //////tsm_lockLayout.Checked = Configuration.lockLayout;
                dockPanel.AllowEndUserDocking = !Configuration.lockLayout;


                if (Configuration.layoutFilePath.Contains("ClassicalLayout1"))
                    checkBoxItem1.Checked = true;
                else if (Configuration.layoutFilePath.Contains("ClassicalLayout2"))
                    checkBoxItem2.Checked = true;

                Frm_JobInfo.Instance.comboBox1.Items.AddRange(D_imageWindow.Keys.ToArray());

                //  if (Frm_ImageWindow.Instance.DockState == DockState.Hidden)
                Frm_ImageWindow.Instance.Show(dockPanel);

            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_changeMode_Click(object sender, EventArgs e)
        {
            if (Machine.productionMode)
            {
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Switch to debug page" : "切换到调试页面");
                Frm_Login.Instance.ShowDialog();
            }
            else
            {
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Switch to production page" : "切换到生产页面");
                Machine.SwitchToProductForm();
            }
        }
        private void buttonItem14_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Frm_Main_SizeChanged(object sender, EventArgs e)
        {
            pic_statu.Location = new System.Drawing.Point(this.Width - 80, pic_statu.Location.Y);
        }
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Machine.productionMode)             //生产模式不让收缩菜单栏
                    return;
                if (buttonItem2.Text == "^")
                {
                    buttonItem2.Text = "口";
                    ribbonControl1.Height = 32;
                    tableLayoutPanel1.Height = 32;
                }
                else
                {
                    buttonItem2.Text = "^";
                    ribbonControl1.Height = 145;
                    tableLayoutPanel1.Height = 145;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem52_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Developer))
                return;
            Frm_SerialPort.Instance.ShowDialog();
        }
        private void buttonItem53_Click(object sender, EventArgs e)
        {
            Frm_PLCComm frm_PLCComm = new Frm_PLCComm();
            frm_PLCComm.Show();
        }
        private void buttonItem54_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void buttonItem55_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;

                Frm_communicateConfig.Instance.WindowState = FormWindowState.Normal;
                Frm_communicateConfig.Instance.ShowDialog();
                Frm_communicateConfig.Instance.TopMost = true;
                Frm_communicateConfig.Instance.TopMost = false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem56_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openImage = new System.Windows.Forms.OpenFileDialog();
                dig_openImage.FileName = "";
                dig_openImage.Title = (Configuration.language == Language.English ? "Please select a job file" : "请选择流程文件");
                dig_openImage.InitialDirectory = Application.StartupPath + "\\Resources\\Sample";
                dig_openImage.Filter = (Configuration.language == Language.English ? "job file(*.job)|*.job" : "流程文件(*.job)|*.job");
                if (dig_openImage.ShowDialog() == DialogResult.OK)
                {
                    if (Configuration.L_recentlyOpendFile.Contains(dig_openImage.FileName))
                        Configuration.L_recentlyOpendFile.Remove(dig_openImage.FileName);
                    Configuration.L_recentlyOpendFile.Insert(0, dig_openImage.FileName);
                    if (Configuration.L_recentlyOpendFile.Count >= 5)
                        Configuration.L_recentlyOpendFile.RemoveRange(5, Configuration.L_recentlyOpendFile.Count - 5);

                    Job job = Job.LoadJob(dig_openImage.FileName);
                    Frm_Job.Instance.tbc_jobs.SelectedIndex = Frm_Job.Instance.tbc_jobs.TabCount - 1;
                    Application.DoEvents();
                    Frm_ImageWindow.Instance.Update_Last_Run_Result_Image_List();

                    //  job .Draw_Line(null ,null );   //此处不能画线，否则就会出现添加第二个示例流程时报错的问题
                    switch (job.jobName)
                    {
                        case "尺寸测量":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\Measurement";
                            break;
                        case "斑点分析":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\Blob";
                            break;
                        case "机械手下视觉定位":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\Battary";
                            break;
                        case "机械手下视觉抓取定点放置":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\Battary";
                            break;
                        case "模板匹配":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\Match";
                            break;
                        case "条码读取":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\Barcode";
                            break;
                        case "位置跟随":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\Battary";
                            break;
                        case "焊点检测":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\Weld";
                            break;
                        case "OCR":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\OCR";
                            break;
                        case "记号检测":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\DefectDetection";
                            break;
                        case "圆度检测":
                            ((HalconInterfaceTool)(Job.GetToolByToolName(job.jobName, "Halcon采集接口"))).imageDirectoryPath = Application.StartupPath + "\\Resources\\Image\\RoundnessDetection";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem59_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\Resources\\Demo");
        }
        private void buttonItem57_Click_2(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\Resources\\Help.html");
        }
        private void buttonItem58_Click_2(object sender, EventArgs e)
        {
            Frm_Feedback.Instance.ShowDialog();
        }
        private void buttonItem62_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void buttonItem63_Click(object sender, EventArgs e)
        {
            Frm_Version.Instance.ShowDialog();
        }
        private void buttonItem17_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Create_New_Job();
        }
        private void buttonItem26_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void buttonItem60_Click_1(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Windows.Forms.SaveFileDialog dig_saveImage = new System.Windows.Forms.SaveFileDialog();
                dig_saveImage.FileName = DateTime.Now.ToString("yyyy_MM_dd");
                dig_saveImage.Title = Configuration.language == Language.English ? "Please select the image saving path" : "请选择图像保存路径";
                dig_saveImage.Filter = "Image File|*.tif|Image File(*.*)|*.*|Image File(*.png)|*.txt|Image File(*.jpg)|*.jpg|Image File(*.bmp)|*.bmp";
                dig_saveImage.InitialDirectory = path;
                if (dig_saveImage.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dig_saveImage.FileName;
                    HObject image;
                    HOperatorSet.DumpWindowImage(out image, Frm_ImageWindow.Instance.WindowHandle);
                    HOperatorSet.WriteImage(image, "tiff", 0, dig_saveImage.FileName);
                    Frm_Main.Instance.OutputMsg("Image saved successfully", Color.Green);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (RegexJudge.IsInt(tbx_percentageOfMovementSpeed.Text.Trim()))
                Configuration.autoRunVelRoute = Convert.ToInt16(tbx_percentageOfMovementSpeed.Text.Trim());
            else if (tbx_percentageOfMovementSpeed.Text.Trim() == string.Empty || tbx_percentageOfMovementSpeed.Text.Trim() == "-")
            {
                //不做事
            }
            else
            {
                Frm_Main.Instance.OutputMsg("曝光值不合法，请输入整型值（错误代码：0101）", Color.Red);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;

            //////// if (homing)
            //////// {
            //////     Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
            //////     return;
            ////// }

            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void buttonItem38_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                if (Frm_Job.Instance.tbc_jobs.TabPages.Count < 1)
                    return;
                Frm_ConfirmBox.Instance.lbl_info.Text = Configuration.language == Language.English ? "Are you sure you want to delete current job?" : "确定要删除当前流程吗？";
                Frm_ConfirmBox.Instance.ShowDialog();
                if (Frm_ConfirmBox.Instance.result == ConfirmBoxResult.Cancel)
                {
                    return;
                }
                string jobName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
                Job.RemoveJobByName(jobName);
                for (int i = 0; i < Frm_Job.Instance.tbc_jobs.TabPages.Count; i++)
                {
                    if (Frm_Job.Instance.tbc_jobs.TabPages[i].Text == jobName)
                    {
                        Frm_Job.Instance.tbc_jobs.TabPages.RemoveAt(i);
                    }
                }
                if (File.Exists(Application.StartupPath + "\\Config\\Vision\\Job\\" + jobName + ".job"))
                    File.Delete(Application.StartupPath + "\\Config\\Vision\\Job\\" + jobName + ".job");
                Frm_Main.Instance.OutputMsg("流程删除成功", Color.Green);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem36_Click(object sender, EventArgs e)
        {
            Job.CloneCurJob();
        }
        private void buttonItem64_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openImage = new System.Windows.Forms.OpenFileDialog();
                dig_openImage.FileName = string.Empty;
                dig_openImage.Title = Configuration.language == Language.English ? "Please select image path" : "请选择图像文件";
                dig_openImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_openImage.Filter = Configuration.language == Language.English ? "Image File(*.*)|*.*|Image File(*.png)|*.txt|Image File(*.jpg)|*.jpg|Image File(*.bmp)|*.bmp|Image File(*.tif)|*.tif" : "图像文件(*.tif)|*.tif|图像文件(*.png)|*.txt|图像文件(*.jpg)|*.jpg|图像文件(*.bmp)|*.bmp|图像文件(*.*)|*.*";
                if (dig_openImage.ShowDialog() == DialogResult.OK)
                {
                    HObject image;
                    try
                    {
                        HOperatorSet.ReadImage(out image, dig_openImage.FileName);
                    }
                    catch
                    {
                        Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Unable to read specified file" : "图像文件异常，无法读取", Color.Red);
                        return;
                    }
                    Frm_ImageWindow.Instance.Display_Image(image);
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Loading Image successfully" : "读取图像成功", Color.Green);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem37_Click_1(object sender, EventArgs e)
        {
            SaveAll();
        }
        private void buttonItem69_Click(object sender, EventArgs e)
        {
            Project.ExportProject();
        }
        private void buttonItem71_Click(object sender, EventArgs e)
        {
            Project.InportProject();
        }
        private void buttonItem65_Click(object sender, EventArgs e)
        {
            SaveAll();
        }
        private void buttonItem67_Click(object sender, EventArgs e)
        {
            Project.InportProject();
        }
        private void buttonItem68_Click(object sender, EventArgs e)
        {
            Project.ExportProject();
        }
        private void buttonItem61_Click(object sender, EventArgs e)
        {

        }
        private void buttonItem66_Click(object sender, EventArgs e)
        {

        }
        private void buttonItem72_Click(object sender, EventArgs e)
        {

        }
        private void buttonItem73_Click(object sender, EventArgs e)
        {
            if (Machine.productionMode)
            {
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Switch to debug page" : "切换到调试页面");
                Frm_Login.Instance.ShowDialog();
            }
            else
            {
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Switch to production page" : "切换到生产页面");
                Frm_Main.Instance.buttonItem776.Image = Properties.Resources.Debug;
                Frm_Main.Instance.buttonItem776.Text = "调试";
                Machine.SwitchToProductForm();
            }
        }
        private void buttonItem74_Click(object sender, EventArgs e)
        {

        }
        private void buttonItem75_Click(object sender, EventArgs e)
        {

        }
        private void buttonItem76_Click(object sender, EventArgs e)
        {
            if (Machine.productionMode)
            {
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Switch to debug page" : "切换到调试页面");
                Frm_Login.Instance.ShowDialog();
            }
        }
        private void buttonItem70_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                if (Frm_Job.Instance.tbc_jobs.TabPages.Count < 1)
                    return;
                Frm_ConfirmBox.Instance.lbl_info.Text = Configuration.language == Language.English ? "Are you sure you want to delete current job?" : "确定要删除当前项目吗？";
                Frm_ConfirmBox.Instance.ShowDialog();
                if (Frm_ConfirmBox.Instance.result == ConfirmBoxResult.Cancel)
                {
                    return;
                }
                foreach (TabPage tabPage in Frm_Job.Instance.tbc_jobs.TabPages)
                {
                    string jobName = tabPage.Text;
                    Job.RemoveJobByName(jobName);
                    for (int j = 0; j < Frm_Job.Instance.tbc_jobs.TabPages.Count; j++)
                    {
                        if (Frm_Job.Instance.tbc_jobs.TabPages[j].Text == jobName)
                        {
                            Frm_Job.Instance.tbc_jobs.TabPages.RemoveAt(j);
                        }
                    }
                    if (File.Exists(Application.StartupPath + "\\Config\\Vision\\Job\\" + jobName + ".job"))
                        File.Delete(Application.StartupPath + "\\Config\\Vision\\Job\\" + jobName + ".job");
                }
                Frm_Main.Instance.OutputMsg("项目删除成功", Color.Green);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem46_Click_4(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;

                if (Frm_Job.Instance.tbc_jobs.TabPages.Count == 0)
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "No jobs to run" : "没有可运行的流程", Color.Green);
                    return;
                }
                Frm_Main.Instance.btn_runOnce.Enabled = false;
                Frm_Job.Instance.btn_runLoop.Enabled = false;
                Frm_Job.Instance.btn_runOnce.Enabled = false;
                Application.DoEvents();
                Thread.Sleep(50);
                if (Frm_Job.Instance.btn_runLoop.Text == (Configuration.language == Language.English ? "Run Loop" : "连续运行"))
                {
                    Frm_Main.isStopRun = false;
                    Frm_Job.Instance.th_runJob = new Thread(Frm_Job.Instance.RealTimeRun);
                    Frm_Job.Instance.th_runJob.IsBackground = true;
                    Frm_Job.Instance.th_runJob.Start();
                    Frm_Main.Instance.btn_runLoop.Text = "停止运行";
                    Frm_Job.Instance.btn_runLoop.Text = Configuration.language == Language.English ? "Run Loop" : "停止运行";
                    Frm_Main.Instance.btn_runOnce.Enabled = false;
                }
                else
                {
                    Frm_Main.isStopRun = true;
                    Thread.Sleep(20);
                    Frm_Main.Instance.btn_runLoop.Text = "连续运行";
                    Frm_Job.Instance.btn_runLoop.Text = Configuration.language == Language.English ? "Run Loop" : "连续运行";
                    Frm_Main.Instance.btn_runOnce.Enabled = true;
                    Frm_Job.Instance.btn_runOnce.Enabled = true;
                }
                Frm_Job.Instance.btn_runLoop.Enabled = true;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem46_Click_5(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;

            Frm_TCPServer.Instance.WindowState = FormWindowState.Normal;
            Frm_TCPServer.Instance.Show();
            Frm_TCPServer.Instance.TopMost = true;
        }
        private void buttonItem50_Click_1(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;

            Frm_TCPClient.Instance.WindowState = FormWindowState.Normal;
            Frm_TCPClient.Instance.Show();
            Frm_TCPClient.Instance.TopMost = true;
        }
        private void buttonItem51_Click_1(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Developer))
                return;
            Frm_SerialPort.Instance.ShowDialog();
        }
        private void buttonItem77_Click(object sender, EventArgs e)
        {
            Frm_PLCComm frm_PLCComm = new Frm_PLCComm();
            frm_PLCComm.Show();
        }
        private void buttonItem13_Click_1(object sender, EventArgs e)
        {
            SaveAll();
            this.Close();
        }
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Create_New_Job();
        }
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openImage = new System.Windows.Forms.OpenFileDialog();
                dig_openImage.FileName = "";
                dig_openImage.Title = (Configuration.language == Language.English ? "Please select a job file" : "请选择流程文件");
                dig_openImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_openImage.Filter = (Configuration.language == Language.English ? "job file(*.job)|*.job" : "流程文件(*.job)|*.job");
                if (dig_openImage.ShowDialog() == DialogResult.OK)
                {
                    Configuration.L_recentlyOpendFile.Insert(0, dig_openImage.FileName);
                    if (Configuration.L_recentlyOpendFile.Count >= 5)
                        Configuration.L_recentlyOpendFile.RemoveRange(5, Configuration.L_recentlyOpendFile.Count - 5);
                    VM.LoadJob(dig_openImage.FileName);
                }
                Frm_Job.Instance.tbc_jobs.SelectedIndex = Frm_Job.Instance.tbc_jobs.TabCount - 1;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void buttonItem7_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void buttonItem8_Click(object sender, EventArgs e)
        {
            SaveAll();
        }
        private void buttonItem9_Click(object sender, EventArgs e)
        {
            Job.LoadJob(((DevComponents.DotNetBar.ButtonItem)sender).Tag.ToString());
        }
        private void buttonItem10_Click(object sender, EventArgs e)
        {
            Job.LoadJob(((DevComponents.DotNetBar.ButtonItem)sender).Tag.ToString());
        }
        private void buttonItem11_Click(object sender, EventArgs e)
        {
            Job.LoadJob(((DevComponents.DotNetBar.ButtonItem)sender).Tag.ToString());
        }
        private void buttonItem12_Click(object sender, EventArgs e)
        {
            Job.LoadJob(((DevComponents.DotNetBar.ButtonItem)sender).Tag.ToString());
        }
        private void buttonItem18_Click(object sender, EventArgs e)
        {
            Job.LoadJob(((DevComponents.DotNetBar.ButtonItem)sender).Tag.ToString());
        }
        private void buttonItem28_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count == 0)
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "No jobs to run" : "没有可运行的流程", Color.Green);
                    return;
                }
                Frm_Job.Instance.btn_runOnce.Enabled = false;
                string jobName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
                Job job = Job.GetJobByName(jobName);
                job.Run();
                Frm_Job.Instance.btn_runOnce.Enabled = true;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem15_Click(object sender, EventArgs e)
        {
            Frm_Omniselector.Instance.Show(dockPanel, DockState.DockLeft);
        }
        private void buttonItem19_Click(object sender, EventArgs e)
        {
            Machine.SwitchToProductForm();
        }
        private void buttonItem20_Click(object sender, EventArgs e)
        {
            if (Frm_Job.Instance.DockState == DockState.Hidden)
                Frm_Job.Instance.Show(dockPanel, DockState.DockRight);
        }
        private void buttonItem21_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                Frm_ImageWindow dummyDoc = CreateNewImageWindow();
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    dummyDoc.MdiParent = this;
                    dummyDoc.Show();
                }
                else
                    dummyDoc.Show(dockPanel);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem23_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Frm_Setting.Instance.ShowDialog();
        }
        private void buttonItem24_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;

            Frm_MotionControl.Instance.Show();
        }
        private void buttonItem25_Click(object sender, EventArgs e)
        {
            Frm_Tools.Instance.Show(dockPanel, DockState.DockLeft);
        }
        private void buttonItem27_Click(object sender, EventArgs e)
        {
            if (Frm_Output.Instance.DockState == DockState.Hidden || Frm_Output.Instance.DockState == DockState.Unknown)
                Frm_Output.Instance.Show(dockPanel, DockState.DockBottom);
            else
                Frm_Output.Instance.Activate();
        }
        private void buttonItem31_Click(object sender, EventArgs e)
        {
            if (Frm_IO.Instance.DockState == DockState.Hidden || Frm_IO.Instance.DockState == DockState.Unknown)
                Frm_IO.Instance.Show(dockPanel, DockState.DockBottomAutoHide);
            else
                Frm_IO.Instance.Activate();
        }
        private void buttonItem32_Click(object sender, EventArgs e)
        {
            if (Frm_Monitor.Instance.DockState == DockState.Hidden || Frm_Monitor.Instance.DockState == DockState.Unknown)
            {
                Frm_Monitor.Instance.Show(dockPanel, DockState.DockBottomAutoHide);
            }
            else
                Frm_Monitor.Instance.Activate();
        }
        private void buttonItem33_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void buttonItem35_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;

                System.Windows.Forms.OpenFileDialog dig_openImage = new System.Windows.Forms.OpenFileDialog();
                dig_openImage.FileName = "";
                dig_openImage.Title = (Configuration.language == Language.English ? "Please select a job file" : "请选择流程文件");
                dig_openImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_openImage.Filter = (Configuration.language == Language.English ? "job file(*.job)|*.job" : "流程文件(*.job)|*.job");
                dig_openImage.ShowDialog();
                if (dig_openImage.FileName == string.Empty)
                {
                    return;
                }
                try
                {
                    Job job = VM.LoadJob(dig_openImage.FileName);
                    File.Copy(dig_openImage.FileName, Application.StartupPath + "\\Config\\Vision\\Job\\" + job.jobName + ".job");
                }
                catch { }
                Frm_Job.Instance.tbc_jobs.SelectedIndex = Frm_Job.Instance.tbc_jobs.TabCount - 1;
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "A new process was imported" : "导入了新流程");
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem34_Click(object sender, EventArgs e)
        {
            try
            {
                if (Frm_Job.Instance.tbc_jobs.TabCount > 0)
                {
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.FileName = Frm_Job.Instance.tbc_jobs.SelectedTab.Text;
                    saveFileDialog.Filter = (Configuration.language == Language.English ? "job file(*.job)|*.job" : "流程文件(*.job)|*.job");
                    saveFileDialog.Title = (Configuration.language == Language.English ? "Export path selection" : "导出路径选择");
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); ;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(saveFileDialog.FileName))
                            File.Delete(saveFileDialog.FileName);
                        File.Copy(Application.StartupPath + "\\Config\\Vision\\Job\\" + Frm_Job.Instance.tbc_jobs.SelectedTab.Text + ".job", saveFileDialog.FileName);
                    }
                }
                else
                {
                    Frm_Main.Instance.OutputMsg("当前无流程可导出", Color.Red);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem39_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Frm_LayoutManage.Instance.ShowDialog();
        }
        private void buttonItem40_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;

            dockPanel.AllowEndUserDocking = !dockPanel.AllowEndUserDocking;
            if (dockPanel.AllowEndUserDocking)
            {
                buttonItem40.Text = "已解锁";
                buttonItem40.Image = Properties.Resources.UnLock;
            }
            else
            {
                buttonItem40.Text = "已锁定";
                buttonItem40.Image = Properties.Resources.Lock;

            }
            LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Interface lock enabled" : "界面锁定启用");
        }
        private void buttonItem41_Click(object sender, EventArgs e)
        {
            try
            {
                Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = null;
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));

                HTuple row, column;
                HOperatorSet.DrawPoint(Frm_ImageWindow.Instance.WindowHandle, out row, out column);
                HOperatorSet.SetLineWidth(Frm_ImageWindow.Instance.WindowHandle, new HTuple(2));
                HOperatorSet.DispCross(Frm_ImageWindow.Instance.WindowHandle, row, column, 40, 0);
                Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;

                Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = null;
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                HTuple row1, column1;
                HOperatorSet.DrawPoint(Frm_ImageWindow.Instance.WindowHandle, out row1, out column1);
                HOperatorSet.SetLineWidth(Frm_ImageWindow.Instance.WindowHandle, new HTuple(2));
                HOperatorSet.DispCross(Frm_ImageWindow.Instance.WindowHandle, row1, column1, 40, 0);
                Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;

                HOperatorSet.DispArrow(Frm_ImageWindow.Instance.WindowHandle, row, column, row1, column1, new HTuple(10));
                HTuple distance;
                HOperatorSet.DistancePp(row, column, row1, column1, out distance);
                Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "\r\nPixel distance" : "\r\n像素距离：" + ((double)distance).ToString("0.000"));
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem42_Click(object sender, EventArgs e)
        {
            processKeyBoard = System.Diagnostics.Process.Start("osk.exe");
        }
        private void buttonItem43_Click(object sender, EventArgs e)
        {
            try
            {
                //屏幕宽
                int iWidth = Screen.PrimaryScreen.Bounds.Width;
                //屏幕高
                int iHeight = Screen.PrimaryScreen.Bounds.Height;
                //按照屏幕宽高创建位图
                Image img = new Bitmap(iWidth, iHeight);
                //从一个继承自Image类的对象中创建Graphics对象
                Graphics gc = Graphics.FromImage(img);
                //抓屏并拷贝到myimage里
                gc.CopyFromScreen(new System.Drawing.Point(0, 0), new System.Drawing.Point(0, 0), new Size(iWidth, iHeight));
                //this.BackgroundImage = img;

                //保存
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Windows.Forms.SaveFileDialog dig_saveImage = new System.Windows.Forms.SaveFileDialog();
                dig_saveImage.Title = (Configuration.language == Language.English ? "Please select the image saving path" : "请选择图像保存路径");
                dig_saveImage.Filter = "Image File|*.tif|Image File(*.png)|*.png|Image File(*.jpg)|*.jpg|Image File(*.bmp)|*.bmp";
                dig_saveImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_saveImage.FileName = DateTime.Now.ToString("yyyy_MM_dd");
                if (dig_saveImage.ShowDialog() == DialogResult.OK)
                    img.Save(dig_saveImage.FileName);       //保存位图
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem44_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Frm_inputSingalVitual.Instance.Show();
        }
        private void buttonItem45_Click(object sender, EventArgs e)
        {
            Frm_Lock frm_lock = new Frm_Lock();
            buttonItem45.Checked = true;
            frm_lock.ShowDialog();
        }
        private void buttonItem29_Click_1(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Open the Settings page" : "打开设置页面");
            Frm_Setting.Instance.ShowDialog();
        }
        private void buttonItem47_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                DialogResult result = MessageBox.Show(Configuration.language == Language.English ? "Are you sure you want to reset all the setting and config?" : "    确定要将程序恢复到初始状态吗？这将丢失所有配置信息及相关设置，使程序恢复到安装完毕时的初始状态！", Configuration.language == Language.English ? "Tip:" : "提示：", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }

                //删除所有配置文件
                if (Directory.Exists(Application.StartupPath + "\\Config"))
                    Directory.Delete(Application.StartupPath + "\\Config", true);
                Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "\r\nReset succeeded! (Effective after restart, the program will shut down automatically)" : "\r\n重置成功!（重启后生效，程序将自动关闭）");
                this.Close();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void buttonItem48_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\Log");
        }
        private void buttonItem49_Click(object sender, EventArgs e)
        {
            processGreedSnake = Process.Start(Application.StartupPath + "\\我的贪吃蛇.exe");
        }
        private void checkBoxItem1_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            if (checkBoxItem1.Checked)
            {
                checkBoxItem2.Checked = false;
                Configuration.layoutFilePath = Application.StartupPath + "\\Resources\\Layout\\" + "ClassicalLayout1.config";
            }
            else
                Configuration.layoutFilePath = Application.StartupPath + "\\dockPanel.config";
        }
        private void checkBoxItem2_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            if (checkBoxItem2.Checked)
            {
                checkBoxItem1.Checked = false;
                Configuration.layoutFilePath = Application.StartupPath + "\\Resources\\Layout\\" + "ClassicalLayout2.config";
            }
            else
                Configuration.layoutFilePath = Application.StartupPath + "\\dockPanel.config";
        }

        #endregion

        #region 通讯协议

        /// <summary>
        /// 协议
        /// </summary>
        /// <param name="command"></param>
        internal static void Protocol(object help1)//(string command, string sender)
        {
            try
            {
                Help11 help = (Help11)help1;
                //通讯本地记录

                LogHelper.SaveLog(LogType.Comm, "接收到：" + help.str1);

                bool b = false;     //用于指示协议中是否包含此指令
                for (int i = 0; i < Configuration.L_communicationItemList.Count; i++)
                {
                    string receiveStr = Configuration.L_communicationItemList[i].ReceivedCommand;
                    if (help.commType == 1)
                        help.str1 = help.str1.Substring(0, help.str1.Length - 2);
                    string jobName = Configuration.L_communicationItemList[i].JobName;
                    if (help.str1 == receiveStr)
                    {
                        b = true;
                        //执行相应的流程
                        string outputItem = Configuration.L_communicationItemList[i].OutputItem;
                        Job job = Job.GetJobByName(jobName);
                        if (job == null)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("未找到名为\"" + jobName + "\"的流程，请检查");
                            return;
                        }

                        //寻找OutputBoxTool
                        ToolInfo outputBox = new ToolInfo();
                        for (int j = 0; j < job.L_toolList.Count; j++)
                        {
                            if (job.L_toolList[j].toolType == ToolType.Output)
                                outputBox = (ToolInfo)job.L_toolList[j];
                        }

                        job.Run();
                        string result = outputBox.GetInput ("<--" + outputItem).value .ToString();

                        int index = Frm_UserForm.Instance.dataGridView1.Rows.Add();


                        Frm_UserForm.Instance.dataGridView1.Rows[index].Cells[0].Value = DateTime.Now.ToString("HH:mm:ss");
                        Frm_UserForm.Instance.dataGridView1.Rows[index].Cells[1].Value = result;
                        if (Frm_UserForm.Instance.dataGridView1.Rows.Count > 100)
                            Frm_UserForm.Instance.dataGridView1.Rows.RemoveAt(Frm_UserForm.Instance.dataGridView1.Rows.Count - 1);


                        string addStr = Configuration.L_communicationItemList[i].PrefixStr;
                        if (Configuration.communicationType == CommunicationType.Internet_Client)
                        {
                            Frm_TCPClient.Instance.Send(result);
                        }
                        else if (Configuration.communicationType == CommunicationType.Internet_Sever)
                        {
                            Frm_TCPServer.Instance.Send(result, help.str2);
                        }
                        else if (Configuration.communicationType == CommunicationType.SerialPort)
                        {
                            Frm_SerialPort.Instance.serialPort.ReadExisting();
                            Frm_SerialPort.Instance.serialPort.Write(result);
                        }
                        Frm_UserForm.Instance.OutputMsg("已发送：" + result);


                        LogHelper.SaveLog(LogType.Comm, "已发送：" + result);

                        Application.DoEvents();
                    }
                }
                if (!b)
                {
                    Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Receiving the non-negotiated agreement content sent by the remote terminal：" : "接收到远程端发来的未商议协议内容：" + help.str1);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        #endregion

        #region  窗体缩放
        private const int WM_NCHITTEST = 0x0084; //鼠标在窗体客户区（除标题栏和边框以外的部分）时发送的信息
        const int HTLEFT = 10;  //左变
        const int HTRIGHT = 11;  //右边
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;  //左上
        const int HTTOPRIGHT = 14; //右上
        const int HTBOTTOM = 15;  //下
        const int HTBOTTOMLEFT = 0x10;  //左下
        const int HTBOTTOMRIGHT = 17;  //右下
        System.Drawing.Point vPoint = System.Drawing.Point.Empty;
        //自定义边框拉伸
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
                switch (m.Msg)
                {
                    case WM_NCHITTEST:
                        vPoint = new System.Drawing.Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        if (vPoint.X <= 5)
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)HTTOPLEFT;  //左上
                            else if (vPoint.Y >= this.ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMLEFT; //左下
                            else
                                m.Result = (IntPtr)HTLEFT;  //左边
                        else if (vPoint.X >= this.ClientSize.Width - 5)
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)HTTOPRIGHT;  //右上
                            else if (vPoint.Y >= this.ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMRIGHT;  //右下
                            else
                                m.Result = (IntPtr)HTRIGHT;  //右
                        else if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOP;  //上
                        else if (vPoint.Y >= this.ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOM; //下

                        else
                        {
                            base.WndProc(ref m);//如果去掉这一行代码,窗体将失去MouseMove..等事件
                            System.Drawing.Point lpint = new System.Drawing.Point((int)m.LParam);//可以得到鼠标坐标,这样就可以决定怎么处理这个消息了,是移动窗体,还是缩放,以及向哪向的缩放

                            m.Result = (IntPtr)0x2;//托动HTCAPTION=2 <0x2>
                        }
                        break;
                }
            }
            catch { }
        }
        #endregion

        private void buttonItem73_Click_1(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Operator))
                return;
            Machine.StartRun();
        }

        private void buttonItem78_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Operator))
                return;
            Machine.StopRun();
        }

        private void buttonItem79_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Operator))
                return;
            Machine.Home();
        }

        private void buttonItem80_Click(object sender, EventArgs e)
        {
            Frm_Login.Instance.ShowDialog();
        }

        private void buttonItem776_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem82_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonItem776_Click_1(object sender, EventArgs e)
        {
            if (Machine.productionMode)
            {
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Switch to debug page" : "切换到调试页面");
                Frm_Login.Instance.ShowDialog();
            }
            else
            {
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Switch to production page" : "切换到生产页面");
                Frm_Main.Instance.buttonItem776.Image = Properties.Resources.Debug;
                Frm_Main.Instance.buttonItem776.Text = "调试页面";
                Machine.SwitchToProductForm();

                //生产模式不让收缩菜单栏
                buttonItem2.Text = "^";
                ribbonControl1.Height = 145;
                tableLayoutPanel1.Height = 145;
            }
        }

        private void buttonItem61_Click_1(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Windows.Forms.SaveFileDialog dig_saveImage = new System.Windows.Forms.SaveFileDialog();
                dig_saveImage.FileName = DateTime.Now.ToString("yyyy_MM_dd");
                dig_saveImage.Title = Configuration.language == Language.English ? "Please select the image saving path" : "请选择图像保存路径";
                dig_saveImage.Filter = "Image File|*.tif|Image File(*.*)|*.*|Image File(*.png)|*.txt|Image File(*.jpg)|*.jpg|Image File(*.bmp)|*.bmp";
                dig_saveImage.InitialDirectory = path;
                if (dig_saveImage.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dig_saveImage.FileName;
                    HOperatorSet.WriteImage(Frm_ImageWindow.currentImage, "tiff", 0, dig_saveImage.FileName);
                    Frm_Main.Instance.OutputMsg("Image saved successfully", Color.Green);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        private void tim_recordTime_Tick(object sender, EventArgs e)
        {
            if (Machine.runStatu == MachineRunStatu.Running)
            {
                Machine.runTime += DateTime.Now - Machine.oldTime;
                Machine.oldTime = DateTime.Now;
                lbl_runTime.Text = Machine.runTime.Days.ToString("00") + "天" + Machine.runTime.Hours.ToString("00") + "时" + Machine.runTime.Minutes.ToString("00") + "分" + Machine.runTime.Seconds.ToString("00") + "秒";
            }
            else if (Machine.runStatu == MachineRunStatu.Alarm)
            {
                Machine.alarmTime += DateTime.Now - Machine.oldTime;
                Machine.oldTime = DateTime.Now;
                lbl_alarmTime.Text = Machine.alarmTime.Days.ToString("00") + "天" + Machine.alarmTime.Hours.ToString("00") + "时" + Machine.alarmTime.Minutes.ToString("00") + "分" + Machine.alarmTime.Seconds.ToString("00") + "秒";
            }
            else
            {
                Machine.waitTime+=DateTime.Now - Machine.oldTime;
                Machine.oldTime = DateTime.Now;
                lbl_waitTime.Text = Machine.waitTime.Days.ToString("00") + "天" + Machine.waitTime.Hours.ToString("00") + "时" + Machine.waitTime.Minutes.ToString("00") + "分" + Machine.waitTime.Seconds.ToString("00") + "秒";
            }
        }

        private void ribbonTabItem2_Click(object sender, EventArgs e)
        {

        }
    }
}
