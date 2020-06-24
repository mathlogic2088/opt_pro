using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionAndMotionPro
{
    public partial class Frm_Omniselector : DockContent
    {
        public Frm_Omniselector()
        {
            InitializeComponent();
            Init_Language();
        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_Omniselector _instance;
        public static Frm_Omniselector Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_Omniselector();
                return _instance;
            }
        }


        /// <summary>
        /// 初始化语言
        /// </summary>
        private void Init_Language()
        {
            try
            {
                if (Configuration.language == Language.English)
                {
                    this.Text = "Omniselector";
                    treeView1.Nodes.Clear();
                    treeView1.Nodes.Add("Job Editor");
                    treeView1.Nodes.Add("Image Window");
                    treeView1.Nodes.Add("Toolbox");
                    treeView1.Nodes.Add("Setting");
                    treeView1.Nodes.Add("Value Monitor");
                    treeView1.Nodes.Add("Comm Monitor");
                    treeView1.Nodes.Add("IO Monitor");
                    treeView1.Nodes.Add("Axis Control");
                    treeView1.Nodes.Add("Output");
                    TreeNode node = treeView1.Nodes.Add("Ethernet");
                    {
                        node.Nodes.Add("Client");
                        node.Nodes.Add("Sever");
                    }
                    treeView1.Nodes.Add("Serial");
                    node = treeView1.Nodes.Add("Comm Config");
                    treeView1.Nodes.Add("PLC Comm");
                    {
                        node.Nodes.Add("Omron");
                        node.Nodes.Add("XiMenzi");
                        node.Nodes.Add("SanLing");
                        node.Nodes.Add("SongXia");
                    }
                    treeView1.Nodes.Add("Virtual Keyboard");
                    treeView1.Nodes.Add("Game");
                    treeView1.Nodes.Add("Feedback");
                    treeView1.Nodes.Add("Activation");
                    treeView1.Nodes.Add("About");
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }


        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            switch (treeView1.SelectedNode.Text)
            {
                case "Job Edixxtor":
                case "生产页面":
                    Machine.SwitchToProductForm();
                    break;

                case "Job Editor":
                case "流程编辑器":
                    if (Frm_Job.Instance.DockState == DockState.Hidden || Frm_Job.Instance.DockState == DockState.Unknown)
                        Frm_Job.Instance.Show(Frm_Main.Instance.dockPanel, DockState.DockRight);
                    break;

                case "Image Window":
                case "图像窗口":
                    try
                    {
                        if (!Permission.CheckPermission(PermissionLevel.Developer))
                            return;

                        Frm_ImageWindow dummyDoc = Frm_Main.Instance.CreateNewImageWindow();
                        if (Frm_Main.Instance.dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                        {
                            dummyDoc.MdiParent = this;
                            dummyDoc.Show();
                        }
                        else
                            dummyDoc.Show(Frm_Main.Instance.dockPanel);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.SaveErrorInfo(ex);
                    }
                    break;

                case "Toolbox":
                case "工具箱":
                    Frm_Tools.Instance.Show(Frm_Main.Instance.dockPanel, DockState.DockLeft);
                    break;

                case "Setting":
                case "设置":
                    if (!Permission.CheckPermission(PermissionLevel.Admin))
                        return;
                    Frm_Setting.Instance.ShowDialog();
                    break;

                case "Value Monitor":
                case "值监控器":
                    if (Frm_Monitor.Instance.DockState == DockState.Hidden || Frm_Monitor.Instance.DockState == DockState.Unknown)
                        Frm_Monitor.Instance.Show(Frm_Main.Instance.dockPanel, DockState.DockBottomAutoHide);
                    else
                        Frm_Monitor.Instance.Activate();
                    break;

                case "Comm Monitor":
                case "通讯监控":
                    Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
                    break;

                case "IO Monitor":
                case "IO监控":
                    if (Frm_IO.Instance.DockState == DockState.Hidden || Frm_IO.Instance.DockState == DockState.Unknown)
                        Frm_IO.Instance.Show(Frm_Main.Instance.dockPanel, DockState.DockBottomAutoHide);
                    else
                        Frm_IO.Instance.Activate();
                    break;

                case "Output":
                case "输出":
                    if (Frm_Output.Instance.DockState == DockState.Hidden || Frm_Output.Instance.DockState == DockState.Unknown)
                        Frm_Output.Instance.Show(Frm_Main.Instance.dockPanel, DockState.DockBottomAutoHide);
                    else
                        Frm_Output.Instance.Activate();
                    break;

                case "Axis Control":
                case "运动控制":
                    if (!Permission.CheckPermission(PermissionLevel.Admin))
                        return;
                    Frm_MotionControl.Instance.Show();
                    Frm_MotionControl.Instance.WindowState = FormWindowState.Normal;
                    break;

                case "Client":
                case "客户端":
                    if (!Permission.CheckPermission(PermissionLevel.Admin))
                        return;
                    Frm_TCPClient.Instance.WindowState = FormWindowState.Normal;
                    Frm_TCPClient.Instance.Show();
                    Frm_TCPClient.Instance.TopMost = true;
                    break;

                case "Sever":
                case "服务端":
                    if (!Permission.CheckPermission(PermissionLevel.Admin))
                        return;
                    Frm_TCPServer.Instance.WindowState = FormWindowState.Normal;
                    Frm_TCPServer.Instance.Show();
                    Frm_TCPServer.Instance.TopMost = true;
                    break;

                case "Serial":
                case "串口":
                    if (!Permission.CheckPermission(PermissionLevel.Admin))
                        return;
                    Frm_SerialPort.Instance.ShowDialog();
                    break;

                case "CommConfig":
                case "通讯配置":
                    if (!Permission.CheckPermission(PermissionLevel.Admin))
                        return;
                    Frm_communicateConfig.Instance.WindowState = FormWindowState.Normal;
                    Frm_communicateConfig.Instance.ShowDialog();
                    Frm_communicateConfig.Instance.TopMost = true;
                    Frm_communicateConfig.Instance.TopMost = false;
                    break;

                case "Ormon":
                case "PLC通讯":
                    Frm_PLCComm frm_PLCComm = new Frm_PLCComm();
                    frm_PLCComm.Show();
                    break;

                case "Virtual Keyboard":
                case "软键盘":
                    Frm_Main.Instance.processKeyBoard = System.Diagnostics.Process.Start("osk.exe");
                    break;

                case "Snake":
                case "贪吃蛇":
                    Frm_Main.Instance.processGreedSnake = Process.Start(Application.StartupPath + "\\我的贪吃蛇.exe");
                    break;

                case "Feedback":
                case "反馈":
                    Frm_Feedback.Instance.ShowDialog();
                    break;

                case "dd":
                case "激活":
                    Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
                    break;

                case "About":
                case "关于":
                    Frm_Version.Instance.ShowDialog();
                    break;
            }
        }
        private void Frm_Omniselector_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void Frm_Omniselector_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

    }
}
