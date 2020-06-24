using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Tool;
using System.Diagnostics;

namespace VisionAndMotionPro
{
    internal partial class Frm_TCPClient : Form
    {
        internal Frm_TCPClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 通讯用Socket
        /// </summary>
        internal Socket socket;
        /// <summary>
        /// 接收消息的线程
        /// </summary>
        private static Thread th_recieve;
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_TCPClient _instance;
        public static Frm_TCPClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_TCPClient();
                return _instance;
            }
        }


        /// <summary>
        /// 信息显示
        /// </summary>
        /// <param name="message">要显示的信息</param>
        internal void ShowMsg(string message)
        {
            string curTime = DateTime.Now.ToString("HH:mm:ss");
            tbx_log.AppendText(curTime + " " + message + "\r\n");
        }
        /// <summary>
        /// 保存IP和Port
        /// </summary>
        private void Save()
        {
            try
            {
                Configuration.remoteIPAsClient  = tbx_ip.Text.Trim();
                Configuration.remotePortAsClient  = Convert.ToInt32(tbx_port.Text.Trim());
                Configuration.Save();
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="msg">要发送的信息</param>
        internal  void Send(string msg)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(msg);
                socket.Send(buffer);
                if (Instance.Visible == true)
                    ShowMsg("-> ：" + tbx_sendMessage.Text.Trim());
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        private void Recieve()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int length = 0;
                    try
                    {
                        length = socket.Receive(buffer);
                    }
                    catch { }
                    string result = Encoding.Default.GetString(buffer, 0, length);
                    if (length > 0)
                    {
                        if (Frm_TCPClient.Instance.Visible)
                            ShowMsg("<- ：" + result);
                        Help11 help11 = new Help11();
                        help11.str1 = result;
                        help11.commType = 0;
                        help11.str2 = socket.RemoteEndPoint.ToString();
                        Frm_Main.Protocol(help11);
                    }
                    else
                    {
                        if (socket != null)
                        {
                            try
                            {
                                socket.Disconnect(false);
                                socket.Close();
                            }
                            catch { }
                        }
                        lbl_connectStatu.Text = "已断开";
                        lbl_connectStatu.ForeColor = Color.Red;
                        btn_connect.Text = "连接";
                        tbx_ip.Enabled = true;
                        tbx_port.Enabled = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 连接服务端
        /// </summary>
        public void Connect()
        {
        Again:
            if (btn_connect.Text == "连接")
            {
                string socketIP = Configuration.remoteIPAsClient;
                int socketPort = Configuration.remotePortAsClient;
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip;
                try
                {
                    ip = IPAddress.Parse(socketIP);
                }
                catch
                {
                    Frm_MessageBox.Instance.MessageBoxShow("\r\nIP地址有误或IP不存在，请检查");
                    return;
                }
                IPEndPoint point = new IPEndPoint(ip, socketPort);
                try
                {
                    socket.Connect(point);
                }
                catch
                {
                    if (MessageBox.Show("连接失败，可能是第三方通讯设备未正常运行，请在第三方通讯设备正常运行后点击确定，或直接点击取消放弃连接。", "提示：", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        goto Again;
                    }
                }
                if (socket.Connected)
                {
                    lbl_connectStatu.Text = "已连接";
                    lbl_connectStatu.ForeColor = Color.Green;
                    th_recieve = new Thread(Recieve);
                    th_recieve.IsBackground = true;
                    th_recieve.Start();
                    btn_connect.Text = "断开";
                    tbx_ip.Enabled = false;
                    tbx_port.Enabled = false;
                }
            }
            else
            {
                if (socket != null)
                {
                    try
                    {
                        socket.Disconnect(false);
                        socket.Close();
                        socket = null;
                    }
                    catch { }
                }
                lbl_connectStatu.Text = "已断开";
                lbl_connectStatu.ForeColor = Color.Red;
                btn_connect.Text = "连接";
                tbx_ip.Enabled = true;
                tbx_port.Enabled = true;
            }
        }


        private void Frm_TCPIP_Load(object sender, EventArgs e)
        {
            tbx_ip.Text = Configuration.remoteIPAsClient  ;
            tbx_port.Text = Configuration.remotePortAsClient  .ToString();
            tbx_ip.TextChanged -= new EventHandler(txt_ip_TextChanged);
            tbx_ip.TextChanged += new EventHandler(txt_ip_TextChanged);
            tbx_port.TextChanged -= new EventHandler(txt_port_TextChanged);
            tbx_port.TextChanged += new EventHandler(txt_port_TextChanged);
        }
        public  void btn_connect_Click(object sender, EventArgs e)
        {
            Connect();
        }
        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                if (socket == null || !socket.Connected)
                {
                    Frm_MessageBox.Instance.MessageBoxShow("未建立连接");
                    return;
                }
                if (tbx_sendMessage.Text.Trim() == "")
                {
                    Frm_MessageBox.Instance.MessageBoxShow("不可发送空字符串");
                    return;
                }
                Send (tbx_sendMessage .Text .Trim ());
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void txt_ip_TextChanged(object sender, EventArgs e)
        {
            Save();
        }
        private void txt_port_TextChanged(object sender, EventArgs e)
        {
            Save();
        }
        private void lal_connectStatu_TextChanged(object sender, EventArgs e)
        {
            if (lbl_connectStatu.Text == (Configuration.language == Language.English ? "Not Connected" : "未连接"))
                lbl_connectStatu.ForeColor = Color.Red;
            else
                lbl_connectStatu.ForeColor = Color.Green;
        }
        private void lnk_clearLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbx_log.Clear();
        }
        private void Frm_TCPClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    }
}
