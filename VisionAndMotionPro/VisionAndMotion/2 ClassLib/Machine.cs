using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using VisionAndMotionPro.Properties;
using System.IO;
using HalconDotNet;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace VisionAndMotionPro
{
    internal class Machine
    {

        /// <summary>
        /// 表示程序启动时是否初始化成功
        /// </summary>
        internal static bool initSucceed = true;
        /// <summary>
        /// 设备运行状态
        /// </summary>
        internal static MachineRunStatu runStatu = MachineRunStatu.WaitReset;
        /// <summary>
        /// 是否处于前台生产状态，分前台生产状态和后台调试状态
        /// </summary>
        internal static bool productionMode = true;
        /// <summary>
        /// 记录旧的时间
        /// </summary>
        internal static DateTime oldTime = DateTime.Now;
        /// <summary>
        /// 总生产时间
        /// </summary>
        internal static TimeSpan runTime;
        /// <summary>
        /// 总待机时间
        /// </summary>
        internal static TimeSpan waitTime;
        /// <summary>
        /// 总报警时间
        /// </summary>
        internal static TimeSpan alarmTime;
        /// <summary>
        /// 资源锁
        /// </summary>
        internal static object lock_resources = new object();
        /// <summary>
        /// 是否已经显示
        /// </summary>
        internal static bool hasShown = false;
        internal static bool loading = true;

        /// <summary>
        /// 初始化
        /// </summary>
        internal static void Init()
        {
            try
            {
                Frm_InitItemStatu.Instance.UpdateStep(5, Configuration.language == Language.English ? "Init" : "初始化", true);
                Configuration.Read();

                Frm_Welcome.Instance.lbl_companyName.Text = Configuration.CompanyName;
                //Frm_Main.Instance.lbl_title.Text = "Vision & Motion Pro " + "  V " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " _ " + Configuration.ProgramTitle;
                //Frm_Main.Instance.ribbonControl1.TitleText = Configuration.CompanyName;

                Frm_Main.Instance.Init_Tool_Tips();
                Application.DoEvents();
                Frm_Main.Instance.Opacity = 0;

                //添加最近打开过的文件列表
                for (int i = 0; i < Configuration.L_recentlyOpendFile.Count; i++)
                {
                    switch (i)
                    {
                        //case 0:
                        //    if (Configuration.L_recentlyOpendFile[0] != string.Empty)
                        //    {
                        //        //Frm_Main.Instance.buttonItem9.Text = "&1. " + Path.GetFileName(Configuration.L_recentlyOpendFile[i]);
                        //        //Frm_Main.Instance.buttonItem9.Tag = Configuration.L_recentlyOpendFile[i];
                        //    }
                        //    else
                        //        //Frm_Main.Instance.buttonItem9.Visible = false;
                        //    break;
                        //case 1:
                        //    if (Configuration.L_recentlyOpendFile[1] != string.Empty)
                        //    {
                        //        //Frm_Main.Instance.buttonItem10.Text = "&2. " + Path.GetFileName(Configuration.L_recentlyOpendFile[i]);
                        //        //Frm_Main.Instance.buttonItem10.Tag = Configuration.L_recentlyOpendFile[i];
                        //    }
                        //    else
                        //        //Frm_Main.Instance.buttonItem10.Visible = false;
                        //    break;
                        //case 2:
                        //    if (Configuration.L_recentlyOpendFile[2] != string.Empty)
                        //    {
                        //        //Frm_Main.Instance.buttonItem11.Text = "&3. " + Path.GetFileName(Configuration.L_recentlyOpendFile[i]);
                        //        //Frm_Main.Instance.buttonItem11.Tag = Configuration.L_recentlyOpendFile[i];
                        //    }
                        //    else
                        //        //Frm_Main.Instance.buttonItem11.Visible = false;
                        //    break;
                        //case 3:
                        //    if (Configuration.L_recentlyOpendFile[3] != string.Empty)
                        //    {
                        //        //Frm_Main.Instance.buttonItem12.Text = "&4. " + Path.GetFileName(Configuration.L_recentlyOpendFile[i]);
                        //        //Frm_Main.Instance.buttonItem12.Tag = Configuration.L_recentlyOpendFile[i];
                        //    }
                        //    else
                        //        //Frm_Main.Instance.buttonItem12.Visible = false;
                        //    break;
                        //case 4:
                        //    if (Configuration.L_recentlyOpendFile[4] != string.Empty)
                        //    {
                        //        //Frm_Main.Instance.buttonItem18.Text = "&5. " + Path.GetFileName(Configuration.L_recentlyOpendFile[i]);
                        //        //Frm_Main.Instance.buttonItem18.Tag = Configuration.L_recentlyOpendFile[i];
                        //    }
                        //    else
                        //        //Frm_Main.Instance.buttonItem18.Visible = false;
                        //    break;
                    }
                }

                //程序自我备份
                Frm_InitItemStatu.Instance.UpdateStep(10, Configuration.language == Language.English ? "Program backup" : "程序备份", true);
                if (Configuration.autoBackupProgram)
                {
                    try
                    {
                        if (Application.StartupPath.Substring(0, 6) == @"C:\Kim")       //我们不允许在备份的文件夹下启动
                        {
                            MessageBox.Show("Do not run this backup program directly, please copy to another path and run again");
                            Process.GetCurrentProcess().Kill();
                        }
                        string date = DateTime.Now.ToString("yyyy-MM-dd");
                        if (Directory.Exists(@"C:\Kim\" + date))
                        {
                            Directory.Delete(@"C:\Kim\" + date, true);
                            Directory.CreateDirectory(@"C:\Kim\" + date);
                        }
                        Frm_Main.CopyFiles(Application.StartupPath, @"C:\Kim\" + date);

                        //清理30天以前的备份
                        DateTime now = DateTime.Now;
                        string[] fileList = Directory.GetDirectories(@"C:\Kim");
                        for (int i = 0; i < fileList.Length; i++)
                        {
                            DirectoryInfo dir = new System.IO.DirectoryInfo(fileList[i]);
                            DateTime dt = dir.CreationTime;
                            if ((now - dt).Days > 30)
                            {
                                File.Delete(fileList[i]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.SaveErrorInfo(ex);
                    }
                }
                //return;
                //Frm_Welcome.Instance.Hide();
                //VM.ShowWindow();


                //初始化配置文件
                Frm_InitItemStatu.Instance.UpdateStep(15, Configuration.language == Language.English ? "Initialization profile" : "初始化配置文件", true);
                Init_Config_Dirctory();

                //反序列化轴配置对象类
                if (File.Exists(Application.StartupPath + "\\Config\\Motion\\AxisPar.cfg"))
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(Application.StartupPath + "\\Config\\Motion\\AxisPar.cfg", FileMode.Open, FileAccess.Read, FileShare.None);
                    Axis_Config.Instance = (Axis_Config)formatter.Deserialize(stream);
                    stream.Close();
                }

                //加载点表信息
                Frm_InitItemStatu.Instance.UpdateStep(30, Configuration.language == Language.English ? "Loading point table information" : "加载点表信息", true);
                if (File.Exists(Application.StartupPath + "\\Config\\Motion\\Point.xml"))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Application.StartupPath + "\\Config\\Motion\\Point.xml");
                    XmlNode rootNode = xmlDoc.SelectSingleNode("PointList");
                    XmlNodeList Pointnodes = rootNode.ChildNodes;
                    for (int i = 0; i < Pointnodes.Count; i++)
                    {
                        int index = Frm_MotionControl.Instance.dgv_pointList.Rows.Add();
                        Frm_MotionControl.Instance.dgv_pointList.Rows[index].Cells[0].Value = Frm_MotionControl.Instance.dgv_pointList.Rows.Count - 1;
                        Frm_MotionControl.Instance.dgv_pointList.Rows[index].Cells[1].Value = Pointnodes[i].Name;
                        XmlNodeList AxisNodes = Pointnodes[i].ChildNodes;
                        Application.DoEvents();
                        for (int j = 0; j < AxisNodes.Count; j++)
                        {
                            string axisName = AxisNodes[j].Name;
                            if (!Frm_MotionControl.Instance.dgv_pointList.Columns.Contains(axisName))
                                Frm_MotionControl.Instance.dgv_pointList.Columns.Add(axisName, axisName);
                            string pos = AxisNodes[j].InnerText;
                            Frm_MotionControl.Instance.dgv_pointList.Rows[index].Cells[j + 2].Value = pos;
                        }
                    }
                }

                //对RibbonControl进行强签名
                try
                {
                    if (Directory.Exists(@"C:\Windows\assembly"))
                    {
                        if (!File.Exists(@"C:\Windows\assembly\DevComponents.DotNetBar2.dll"))
                            File.Copy(Application.StartupPath + "\\DevComponents.DotNetBar2.dll", @"C:\Windows\assembly\DevComponents.DotNetBar2.dll");
                    }
                }
                catch { }

                //通讯连接
                Frm_InitItemStatu.Instance.UpdateStep(40, Configuration.language == Language.English ? "Try Tencent connection" : "尝试通讯连接", true);
                if (Configuration.autoConnectAfterStart)
                {
                    if (Configuration.communicationType == CommunicationType.Internet_Client)
                        Frm_TCPClient.Instance.Connect();
                    else if (Configuration.communicationType == CommunicationType.Internet_Sever)
                        Frm_TCPServer.Instance.Listen();
                    else if (Configuration.communicationType == CommunicationType.SerialPort)
                        Frm_SerialPort.Instance.btn_openPort_Click(null, null);
                }

                //枚举网络中的相机 
                Frm_InitItemStatu.Instance.UpdateStep(45, Configuration.language == Language.English ? "Enumerate cameras in the network" : "枚举网络中的相机", true);
                Camera.Search_All_Camera();       //枚举网络中的所有的相机
                try
                {
                    Camera_Basler.EnumCamrea();
                }
                catch { }
                try
                {
                    Camera_PointGrey.EnumCamera();
                }
                catch { }
                try
                {
                    //////Camera_Cognex.EnumCamrea();       //运行这个函数需要主机安装VisionPro，所以此处暂时注释掉，请在主机安装了VisionPro之后解除注释即可
                }
                catch { }

                //加载标准图像
                Frm_InitItemStatu.Instance.UpdateStep(50, Configuration.language == Language.English ? "Load standard image" : "加载标准图像", true);
                Frm_JobInfo.Instance.Load_Standard_Image();


                //////Frm_Welcome.Instance.Hide();
                //////VM.ShowWindow();

                //初始化板卡
                Frm_InitItemStatu.Instance.UpdateStep(55, Configuration.language == Language.English ? "Initialize board" : "初始化板卡", true);
                if (Configuration.cardType == CardType.固高_GTS)
                    Card_Googol.Init();
                else if (Configuration.cardType == CardType.雷赛_IOC0640)
                    Card_IOC0640.Init();
                else if (Configuration.cardType == CardType.雷塞_DMC2410)
                    Card_LeadShine_DMC2410.Init();
                else if (Configuration.cardType == CardType.雷塞_DMC2210)
                    Card_LeadShineDMC2210.Init();

                //开启实时刷新线程
                Frm_Main.th_update = new Thread(UpdateAll);
                Frm_Main.th_update.IsBackground = true;
                Frm_Main.th_update.Start();

                try
                {
                    HOperatorSet.SetDraw(Frm_ImageWindow.Instance.WindowHandle, new HTuple("margin"));
                }
                catch
                {
                    Frm_MessageBox.Instance.MessageBoxShow("启动异常，启动后程序将不能正常运行(错误代码：002)\r\n可能原因：\r\n1、本机未安装Halcon\r\n2、所安装的Halcon不是17.12版");
                }

                //初始化并运行自动流程
                MainTask.Init();
                MainTask.AutoRun();

                Frm_InitItemStatu.Instance.UpdateStep(70, Configuration.language == Language.English ? "Initialize form" : "初始化窗体", true);
                //显示生产界面
                if (Configuration.showProductionFormAfterStart)
                {
                    Machine.SwitchToProductForm();
                }

                Frm_InitItemStatu.Instance.UpdateStep(80, Configuration.language == Language.English ? "Check registration status" : "检查注册状态", true);
                //  Frm_Main .Instance .  machineCode = "";
                Frm_Main.Instance.regiestCode = Regiest.Get_RNum(Regiest.Get_MNum());

                Frm_JobInfo.Instance.cbx_standardImage.SelectedIndexChanged += new EventHandler(Frm_Main.Instance.cbo_standardImage_SelectedIndexChanged);



                ////Frm_Welcome.Instance.Hide();
                ////VM.ShowWindow();

                ////// //获取开机后运行模式
                ////// if (Configuration.SwitchedToAuto)
                //////Machine.runStatu = MachineRunStatu.Running;
                ////// else
                //////     Machine.runStatu = MachineRunStatu.Stop ;

                //初始化界面布局
                Frm_InitItemStatu.Instance.UpdateStep(85, Configuration.language == Language.English ? "Initialize interface layout" : "初始化界面布局", true);
                //////try
                //////{
                //////    if (File.Exists(Configuration.layoutFilePath))
                //////        Frm_Main.Instance.dockPanel.LoadFromXml(Configuration.layoutFilePath, Frm_Main.Instance.deserializeDockContent);
                //////}
                //////catch { }
                if (Configuration.maxSizeAfterStart)
                    Frm_Main.Instance.WindowState = FormWindowState.Maximized;

                if (Configuration.hideFuncPart)
                {
                    foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                    {
                        item.Value.cbx_toolRunResultImageList.Dock = DockStyle.None;
                        item.Value.cbx_toolRunResultImageList.Size = new Size(0, 0);
                    }
                }

                //加载流程
                Job.Init_Icon_List();
                Frm_InitItemStatu.Instance.UpdateStep(90, Configuration.language == Language.English ? "Load process file" : "加载流程文件", true);
                string[] files = Directory.GetFiles(Application.StartupPath + "\\Config\\Vision\\Job");
                //按照创建时间排序，以达到先创建的显示在前面的效果
                string temp;
                for (int i = 0; i < files.Length - 1; i++)
                {
                    for (int j = 0; j < files.Length - 1 - i; j++)
                    {
                        DirectoryInfo dir = new System.IO.DirectoryInfo(files[j]);
                        DateTime DT = dir.CreationTime;
                        DirectoryInfo dir1 = new System.IO.DirectoryInfo(files[j + 1]);
                        DateTime DT1 = dir1.CreationTime;
                        if (DT > DT1)
                        {
                            temp = files[j];
                            files[j] = files[j + 1];
                            files[j + 1] = temp;
                        }
                    }
                }


                ////Frm_Welcome.Instance.Hide();
                ////VM.ShowWindow();

                //加载所有流程
                for (int i = 0; i < files.Length; i++)
                {
                    Job job = Job.LoadJob(files[i]);
                    if (job != null)          //如果安装的时Halcon2010，此处的job对象就会为空
                        job.Run();
                }



                //更新最后一次结果图像列表
                Frm_ImageWindow.Instance.Update_Last_Run_Result_Image_List();
                Frm_Main.Instance.tbx_percentageOfMovementSpeed.Text = Configuration.autoRunVelRoute.ToString();

                //Frm_Main.Instance.ribbonTabItem3.Select();
                //Frm_Main.Instance.ribbonTabItem1.Select();



                Frm_Welcome.Instance.bar_step.Value = 100;
                Application.DoEvents();
                Thread.Sleep(500);
                if (Configuration.enablePermissionControl)
                    Frm_Main.Instance.tss_permissionInfo.Text = "当前登录：未登录，默认为最低权限";
                else
                    Frm_Main.Instance.tss_permissionInfo.Text = "当前登录：未登录";
                if (!Configuration.allowResizeForm)
                {
                    Frm_Main.Instance.MinimumSize = Frm_Main.Instance.Size;
                    Frm_Main.Instance.MaximumSize = Frm_Main.Instance.Size;
                }
                Frm_InitItemStatu.Instance.dataGridView1.FirstDisplayedScrollingRowIndex = Frm_InitItemStatu.Instance.dataGridView1.Rows.Count - 1;
                //Frm_Main.Instance.ribbonTabItem1.Select();
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Startup successful" : "程序启动");
                if (Machine.initSucceed)
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Startup successful" : "启动成功", Color.Green);
                    Frm_Welcome.Instance.lbl_step.Text = Configuration.language == Language.English ? "Startup successful" : "启动成功";
                    //////Frm_Welcome.Instance.Hide  ();
                    ////// VM.ShowWindow();
                }
                else
                {
                    Frm_Main.Instance.OutputMsg("启动出错", Color.Red);
                    Frm_Welcome.Instance.lbl_step.Text = "                  启动出错";
                    Frm_Welcome.Instance.lbl_step.ForeColor = Color.Red;
                    Frm_Welcome.Instance.Height = 356;
                }
                loading = false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 实时更新线程
        /// </summary>
        private static void UpdateAll()
        {
            try
            {
                lock (lock_resources)
                {
                    while (true)
                    {
                        if (Frm_Main.willExit)
                        {
                            break;
                        }

                        //更新程序运行状态
                        Update_Run_Status();

                        //实时更新运动控制卡轴与IO状态
                        if (Configuration.cardType == CardType.固高_GTS)
                        {
                            if (Card_Googol.initSucceed || Configuration.vitualCard)
                            {
                                //更新轴位置
                                for (int i = 0; i < Frm_MotionControl.Instance.dgv_axisInfo.Rows.Count; i++)
                                {
                                    double curPos = Card_Googol.GetCurPosition(Frm_MotionControl.Instance.dgv_axisInfo.Rows[i].Cells[1].Value);
                                    Frm_MotionControl.Instance.dgv_axisInfo.Rows[i].Cells[2].Value = curPos.ToString("0.000");
                                }

                                //更新各IO状态
                                for (int i = 0; i < Frm_IO.Instance.dgv_diList.Rows.Count; i++)
                                {
                                    string diName = Frm_IO.Instance.dgv_diList.Rows[i].Cells[3].Value.ToString();
                                    Level level = Card_Googol.GetDiSts(diName);
                                    if (level == Level.High)
                                    {
                                        if (Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag.ToString() != "On")             //此处加一个条件判断，意在防止因重复显示图像导致的闪图问题
                                        {
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = Resources.On;
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag = "On";
                                        }
                                    }
                                    else
                                    {
                                        if (Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag.ToString() != "Off")
                                        {
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = Resources.Off;
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag = "Off";
                                        }
                                    }
                                }
                                for (int i = 0; i < Frm_IO.Instance.dgv_doList.Rows.Count; i++)
                                {
                                    string doName = Frm_IO.Instance.dgv_doList.Rows[i].Cells[4].Value.ToString();
                                    Level level = Card_Googol.GetDoSts(doName);
                                    if (level == Level.High)
                                    {
                                        if (Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag.ToString() != "On")
                                        {
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Value = Resources.On;
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag = "On";
                                        }
                                    }
                                    else
                                    {
                                        if (Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag.ToString() != "Off")
                                        {
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Value = Resources.Off;
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag = "Off";
                                        }
                                    }
                                }
                            }
                        }
                        else if (Configuration.cardType == CardType.雷赛_IOC0640)
                        {
                            if (Card_Googol.initSucceed || Configuration.vitualCard)
                            {
                                //雷赛IOC0640为IO卡，不存在轴，故只更新各IO状态
                                for (int i = 0; i < Frm_IO.Instance.dgv_diList.Rows.Count; i++)
                                {
                                    string diName = Frm_IO.Instance.dgv_diList.Rows[i].Cells[3].Value.ToString();
                                    Level level = Card_IOC0640.GetDiSts(diName);
                                    if (level == Level.High)
                                    {
                                        if (Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag.ToString() != "On")
                                        {
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = Resources.On;
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = "On";
                                        }
                                    }
                                    else
                                    {
                                        if (Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag.ToString() != "Off")
                                        {
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = Resources.Off;
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag = "Off";
                                        }
                                    }
                                }
                                for (int i = 0; i < Frm_IO.Instance.dgv_doList.Rows.Count; i++)
                                {
                                    string doName = Frm_IO.Instance.dgv_doList.Rows[i].Cells[4].Value.ToString();
                                    Level level = Card_IOC0640.GetDoSts(doName);
                                    if (level == Level.High)
                                    {
                                        if (Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag.ToString() != "On")
                                        {
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Value = Resources.On;
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag = "On";
                                        }
                                    }
                                    else
                                    {
                                        if (Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag.ToString() != "Off")
                                        {
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Value = Resources.Off;
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag = "Off";
                                        }
                                    }
                                }
                            }
                        }
                        else if (Configuration.cardType == CardType.雷塞_DMC2210)
                        {
                            if (Card_Googol.initSucceed || Configuration.vitualCard)
                            {
                                for (int i = 0; i < Frm_IO.Instance.dgv_diList.Rows.Count; i++)
                                {
                                    string diName = Frm_IO.Instance.dgv_diList.Rows[i].Cells[3].Value.ToString();
                                    Level level = Card_LeadShineDMC2210.GetDiSts(diName);
                                    if (level == Level.High)
                                    {
                                        if (Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag.ToString() != "On")
                                        {
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = Resources.On;
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = "On";
                                        }
                                    }
                                    else
                                    {
                                        if (Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag.ToString() != "Off")
                                        {
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = Resources.Off;
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag = "Off";
                                        }
                                    }
                                }
                                for (int i = 0; i < Frm_IO.Instance.dgv_doList.Rows.Count; i++)
                                {
                                    string doName = Frm_IO.Instance.dgv_doList.Rows[i].Cells[4].Value.ToString();
                                    Level level = Card_LeadShineDMC2210.GetDoSts(doName);
                                    if (level == Level.High)
                                    {
                                        if (Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag.ToString() != "On")
                                        {
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Value = Resources.On;
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag = "On";
                                        }
                                    }
                                    else
                                    {
                                        if (Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag.ToString() != "Off")
                                        {
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Value = Resources.Off;
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag = "Off";
                                        }
                                    }
                                }
                            }
                        }
                        else if (Configuration.cardType == CardType.雷塞_DMC2410)
                        {
                            if (Card_Googol.initSucceed || Configuration.vitualCard)
                            {
                                for (int i = 0; i < Frm_IO.Instance.dgv_diList.Rows.Count; i++)
                                {
                                    string diName = Frm_IO.Instance.dgv_diList.Rows[i].Cells[3].Value.ToString();
                                    Level level = Card_LeadShine_DMC2410.GetDiSts(diName);
                                    if (level == Level.High)
                                    {
                                        if (Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag.ToString() != "On")
                                        {
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = Resources.On;
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = "On";
                                        }
                                    }
                                    else
                                    {
                                        if (Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag.ToString() != "Off")
                                        {
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Value = Resources.Off;
                                            Frm_IO.Instance.dgv_diList.Rows[i].Cells[2].Tag = "Off";
                                        }
                                    }
                                }
                                for (int i = 0; i < Frm_IO.Instance.dgv_doList.Rows.Count; i++)
                                {
                                    string doName = Frm_IO.Instance.dgv_doList.Rows[i].Cells[4].Value.ToString();
                                    Level level = Card_LeadShine_DMC2410.GetDoSts(doName);
                                    if (level == Level.High)
                                    {
                                        if (Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag.ToString() != "On")
                                        {
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Value = Resources.On;
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag = "On";
                                        }
                                    }
                                    else
                                    {
                                        if (Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag.ToString() != "Off")
                                        {
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Value = Resources.Off;
                                            Frm_IO.Instance.dgv_doList.Rows[i].Cells[3].Tag = "Off";
                                        }
                                    }
                                }
                            }
                        }

                        //检测启动按钮
                        if (Card_Googol.initSucceed)
                        {
                            Level statu = Card_Googol.GetDiSts(Di.启动信号);
                            if (statu == Level.High)
                            {
                                StartRun();
                            }
                        }

                        //监控停止按钮
                        if (Card_Googol.initSucceed)
                        {
                            Level statu = Card_Googol.GetDiSts(Di.停止信号);
                            if (statu == Level.High)
                            {
                                StopRun();
                            }
                        }

                        //监控复位按钮
                        if (Card_Googol.initSucceed)
                        {
                            Level statu = Card_Googol.GetDiSts(Di.停止信号);
                            if (statu == Level.High)
                            {
                                Home();
                            }
                        }

                        //监控急停按钮
                        if (Card_Googol.initSucceed)
                        {
                            Level statu = Card_Googol.GetDiSts(Di.急停信号);
                            if (statu == Level.High)
                            {
                                //急停
                            }
                        }

                        //状态栏提示信息定时清空
                        Frm_Main.Instance.elapsedTime++;
                        if (Frm_Main.Instance.elapsedTime >= 800)
                        {
                            Frm_Main.Instance.lbl_output.ForeColor = Color.Black;
                            Frm_Main.Instance.lbl_output.BackColor = Color.White;
                            Frm_Main.Instance.lbl_output.Text = "";
                            Frm_Main.Instance.elapsedTime = 0;
                        }

                        Frm_Main.Instance.tss_curTime.Text = DateTime.Now.ToString();
                        Frm_Main.Instance.lbl_output.Size = new Size(Frm_Main.Instance.statusStrip1.Size.Width - Frm_Main.Instance.lbl_runStatu.Size.Width - Frm_Main.Instance.tss_curTime.Size.Width - Frm_Main.Instance.tss_permissionInfo.Size.Width - 15, Frm_Main.Instance.lbl_output.Size.Height);
                        Thread.Sleep(10);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 设备整体复位
        /// </summary>
        public static void Home()
        {
            if (runStatu == MachineRunStatu.Running)
            {
                Frm_ConfirmBox.Instance.lbl_info.Text = Configuration.language == Language.English ? "Are you sure you want to delete current job?" : "设备运行中，确定要整体复位吗？";
                Frm_ConfirmBox.Instance.ShowDialog();
                if (Frm_ConfirmBox.Instance.result == ConfirmBoxResult.Confirm)
                {
                    Frm_Main.Instance.OutputMsg("设备整体复位成功", Color.Green);
                    LogHelper.SaveLog(LogType.Operate, "程序复位");
                    runStatu = MachineRunStatu.WaitRun;
                }
            }
            else
            {
                Frm_Main.Instance.OutputMsg("设备整体复位成功", Color.Green);
                runStatu = MachineRunStatu.WaitRun;
            }
        }
        /// <summary>
        /// 开始自动运行
        /// </summary>
        internal static void StartRun()
        {
            if (runStatu == MachineRunStatu.Homing)
            {
                Frm_Main.Instance.OutputMsg("设备复位中，请复位完成后开始", Color.Red);
                return;
            }
            else if (runStatu == MachineRunStatu.WaitReset)
            {
                Frm_Main.Instance.OutputMsg("设备未复位，请复位成后开始", Color.Red);
                return;
            }
            else
            {
                Frm_Main.Instance.OutputMsg("设备开始运行成功", Color.Green);
                LogHelper.SaveLog(LogType.Operate, "开始自动运行");
                runStatu = MachineRunStatu.Running;
            }
        }
        /// <summary>
        /// 停止自动运行
        /// </summary>
        internal static void StopRun()
        {
            if (runStatu == MachineRunStatu.Running)
            {
                Frm_Main.Instance.OutputMsg("设备停止运行成功", Color.Red);
                LogHelper.SaveLog(LogType.Operate, "停止自动运行");
                runStatu = MachineRunStatu.Stop;
                return;
            }
        }
        /// <summary>
        /// 检查各配置文件
        /// </summary>
        private static void Init_Config_Dirctory()
        {
            try
            {
                //主配置文件文件夹
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Config"))
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Config");

                //主配置文件ini
                if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\Config.ini"))
                    File.Create(System.Windows.Forms.Application.StartupPath + "\\Config.ini").Close();

                //标准图像文件夹
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Config\\Vision\\StandardImage"))
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Config\\Vision\\StandardImage");

                //错误信息保存文件夹
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Log\\Error"))
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Log\\Error");

                //创建运控控制文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Config\\Motion"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Config\\Motion");

                //作业文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Config\\Vision\\Job"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Config\\Vision\\Job");

                //资源文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Resources"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Resources");

                //通讯记录文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Log\\Comm"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Log\\Comm");

                //操作记录文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Log\\Comm"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Log\\Operate");
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private static  object obj = new object();
        /// <summary>
        /// 更新运行状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void Update_Run_Status()
        {
            try
            {
                lock (obj)
                {
                    if (runStatu == MachineRunStatu.WaitReset)
                    {
                        Frm_Main.Instance.lbl_runStatu.Text = Configuration.language == Language.English ? "WaitHome......" : "等待复位......              ";
                            Frm_Main.Instance.pic_statu.Image = (Image)Resources.YellowLight.Clone();
                        Frm_Main.Instance.lbl_runStatu.BackColor = Color.Yellow;
                        ThreeColorLamp.SetYellow();
                    }
                    else if (runStatu == MachineRunStatu.Homing)
                    {
                        Frm_Main.Instance.lbl_runStatu.Text = Configuration.language == Language.English ? "Homing......" : "复位中......               ";
                        Frm_Main.Instance.pic_statu.Image = (Image)Resources.YellowLight.Clone();
                        Frm_Main.Instance.lbl_runStatu.BackColor = Color.Yellow;
                        ThreeColorLamp.SetYellow();
                    }
                    else if (runStatu == MachineRunStatu.WaitRun)
                    {
                        Frm_Main.Instance.lbl_runStatu.Text = Configuration.language == Language.English ? "WaitRun......" : "等待运行......               ";
                        Frm_Main.Instance.pic_statu.Image = (Image)Resources.YellowLight.Clone();
                        Frm_Main.Instance.lbl_runStatu.BackColor = Color.Yellow;
                        ThreeColorLamp.SetYellow();
                    }
                    else if (runStatu == MachineRunStatu.Running)
                    {
                        Frm_Main.Instance.lbl_runStatu.Text = Configuration.language == Language.English ? "Runing......" : "运行中......               ";
                        Frm_Main.Instance.pic_statu.Image = (Image)Resources.GreenLight.Clone();
                        Frm_Main.Instance.lbl_runStatu.BackColor = Color.Lime;
                        ThreeColorLamp.SetGreen();
                    }
                    else if (runStatu == MachineRunStatu.Stop)
                    {
                        Frm_Main.Instance.lbl_runStatu.Text = Configuration.language == Language.English ? "Stop......" : "暂停中......               ";
                        Frm_Main.Instance.pic_statu.Image = (Image)Resources.YellowLight.Clone();
                        Frm_Main.Instance.lbl_runStatu.BackColor = Color.Yellow;
                        ThreeColorLamp.SetYellow();
                    }
                    else if (runStatu == MachineRunStatu.Alarm)
                    {
                        Frm_Main.Instance.lbl_runStatu.Text = Configuration.language == Language.English ? "Alarm......" : "报警中......               ";
                        if (hasShown)
                        {
                            hasShown = false;
                            Frm_Main.Instance.pic_statu.Image = null;
                            Thread.Sleep(400);
                        }
                        else
                        {
                            hasShown = true;
                            Frm_Main.Instance.pic_statu.Image = (Image)Resources.RedLight.Clone();
                            Thread.Sleep(400);
                        }
                        Frm_Main.Instance.lbl_runStatu.BackColor = Color.Red;
                        ThreeColorLamp.SetRed();
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// 切换到后台调试状态
        /// </summary>
        internal static void SwitchToDebugForm()
        {
            Frm_Main.Instance.tableLayoutPanel1.Height = 145;
            Frm_Main.Instance.statusStrip1.Height = 26;
            Frm_Main.Instance.pnl_productFormBox.Visible = false;
            productionMode = false;
            //Frm_Main.Instance.buttonItem776.Image = Properties.Resources.Product1;
            //Frm_Main.Instance.buttonItem776.Text = "生产页面";
        }
        /// <summary>
        /// 切换到前台生产状态
        /// </summary>
        internal static void SwitchToProductForm()
        {
            Frm_Main.Instance.statusStrip1.AutoSize = false;
            Frm_Main.Instance.statusStrip1.Height = 0;
            Frm_Main.Instance.tableLayoutPanel1 .Height = 32;
            Frm_Main.Instance.pnl_productFormBox.Visible = true;
            Frm_UserForm.Instance.TopLevel = false;
            Frm_Main.Instance.pnl_productFormBox.Controls.Add(Frm_UserForm.Instance);
            Frm_UserForm.Instance.Parent = Frm_Main.Instance.pnl_productFormBox;
            Frm_UserForm.Instance.Show();
            Frm_UserForm.Instance.Dock = DockStyle.Fill;
            productionMode = true;
        }

    }
    internal enum MachineRunStatu
    {
        WaitReset,
        Stop,
        Homing,
        WaitRun,
        Running,
        Alarm,
    }
}
