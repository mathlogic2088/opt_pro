using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tool;

namespace VisionAndMotionPro
{
    internal partial class Frm_TCPServer : Form
    {
        public Frm_TCPServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 监听线程
        /// </summary>
        private Thread th_listen;
        /// <summary>
        /// 监听Socket
        /// </summary>
        private Socket listenSkt;
        /// <summary>
        /// 用于通讯的Socket
        /// </summary>
        internal Socket commSkt;
        /// <summary>
        /// 用于存放已连接的socket
        /// </summary>
        Dictionary<string, Socket> L_connectedSocket = new Dictionary<string, Socket>();
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_TCPServer _instance;
        public static Frm_TCPServer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_TCPServer();
                return _instance;
            }
        }


        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="message">要显示的信息内容</param>
        internal void ShowMsg(string message)
        {
            string curTime = DateTime.Now.ToString("HH:mm:ss");
            tbx_log.AppendText(curTime + " " + message + "\r\n");
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg">要发送的信息</param>
        /// <param name="sender">消息发送者</param>
        internal void Send(string msg, string sender)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(msg);
                L_connectedSocket[sender].Send(buffer);
                if (Instance.Visible == true)
                    ShowMsg("-> ：" + tbx_sendMessage.Text.Trim());
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 消息接收
        /// </summary>
        /// <param name="obj">通讯用的Socket</param>
        private void Recieve(object obj)
        {
            try
            {
                Socket socket = obj as Socket;
                byte[] buffer = new byte[1024];
                while (true)
                {
                    Frm_UserForm.Instance.tbx_output.AppendText(Environment.NewLine);
                    Frm_UserForm.Instance.OutputMsg("等待远程命令中......");
                    int length = socket.Receive(buffer);
                    if (length == 0)     //表示另一方已断开
                    {
                        cbx_connectedMember.Items.Remove(socket.RemoteEndPoint.ToString());
                        lbx_connectedNumber.Items.Remove(socket.RemoteEndPoint.ToString());
                        if (cbx_connectedMember.Items.Count == 0)
                            cbx_connectedMember.Text = "";
                        return;
                    }
                    string result = Encoding.Default.GetString(buffer, 0, length);
                    Frm_UserForm.Instance.OutputMsg("已收到：" + result);
                    if (socket.RemoteEndPoint.ToString() == cbx_connectedMember.Text)
                        ShowMsg(" <-：" + result);

                    Help11 help11= new Help11();
                    help11.str1 = result;
                    help11.commType = 0;
                    help11.str2 = socket.RemoteEndPoint.ToString();
                    Thread th = new Thread(Frm_Main.Protocol);
                    th.IsBackground = true;
                    th.Start(help11 );
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
                return;
            }
        }
        /// <summary>
        /// 监听
        /// </summary>
        /// <param name="obj">监听用的Socket</param>
        private void WaitConnect(object obj)
        {
            try
            {
                Socket listenSocket = obj as Socket;
                while (true)
                {
                    commSkt = listenSocket.Accept();
                    L_connectedSocket.Add(commSkt.RemoteEndPoint.ToString(), commSkt);
                    cbx_connectedMember.Items.Add(commSkt.RemoteEndPoint.ToString());
                    lbx_connectedNumber.Items.Add(commSkt.RemoteEndPoint.ToString());
                    if (cbx_connectedMember.Items.Count == 1)
                        cbx_connectedMember.SelectedIndex = 0;
                    Thread th = new Thread(Recieve);
                    th.IsBackground = true;
                    th.Start(commSkt);
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 保存IP和Port
        /// </summary>
        private void Save()
        {
            try
            {
                Configuration.localIPAsSever   = tbx_ip.Text.Trim();
                Configuration.localPortAsSever   = Convert.ToInt32(tbx_port.Text.Trim());
                Configuration.Save();
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        public void Listen()
        {
            try
            {
                if (btn_listen.Text == "开始监听")
                {
                    listenSkt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress ip = IPAddress.Parse(Configuration.localIPAsSever );
                    IPEndPoint point = new IPEndPoint(ip, Configuration.localPortAsSever );
                    try
                    {
                        listenSkt.Bind(point);
                    }
                    catch
                    {
                        this.TopMost = false;
                        Frm_MessageBox.Instance.TopMost = true;
                        Frm_MessageBox.Instance.MessageBoxShow("\r\n服务端在监听时出错！(错误代码：001)\r\n\r\n可能原因：所监听的IP地址有误");
                        this.TopMost = true;
                        return;
                    }
                    listenSkt.Listen(10);
                    th_listen = new Thread(WaitConnect);
                    th_listen.IsBackground = true;
                    th_listen.Start(listenSkt);
                    btn_listen.Text = "停止监听";
                    lbl_connectStatu.Text = "监听中......";
                    lbl_connectStatu.ForeColor = Color.Green;
                    tbx_ip.Enabled = false;
                    tbx_port.Enabled = false;
                }
                else
                {
                    tbx_ip.Enabled = true;
                    tbx_port.Enabled = true;
                    btn_listen.Text = "开始监听";
                    lbl_connectStatu.Text = "未监听";
                    lbl_connectStatu.ForeColor = Color.Red;
                    th_listen.Suspend();
                    th_listen.Abort();
                    listenSkt.Dispose();
                    listenSkt.Close();
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }


        internal void btn_listen_Click(object sender, EventArgs e)
        {
            Listen();
        }
        private void txt_ip_TextChanged(object sender, EventArgs e)
        {
            Save();
        }
        private void txt_port_TextChanged(object sender, EventArgs e)
        {
            Save();
        }
        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbx_connectedMember.SelectedIndex < 0)
                {
                    Frm_MessageBox.Instance.MessageBoxShow("未指定已连接客户端");
                    return;
                }
                if (tbx_sendMessage.Text.Trim() == "")
                {
                    Frm_MessageBox.Instance.MessageBoxShow("不可发送空字符串");
                    return;
                }
                byte[] buffer = Encoding.Default.GetBytes(tbx_sendMessage.Text);
                L_connectedSocket[cbx_connectedMember.SelectedItem.ToString()].Send(buffer);
                ShowMsg(" ->：" + tbx_sendMessage.Text);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_TCPServer_Load(object sender, EventArgs e)
        {
            tbx_ip.Text = Configuration.localIPAsSever ;
            tbx_port.Text = Configuration.localPortAsSever .ToString();
            tbx_ip.TextChanged -= new EventHandler(txt_ip_TextChanged);
            tbx_ip.TextChanged += new EventHandler(txt_ip_TextChanged);
            tbx_port.TextChanged -= new EventHandler(txt_port_TextChanged);
            tbx_port.TextChanged += new EventHandler(txt_port_TextChanged);
        }
        private void lnk_clearLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbx_log.Clear();
        }
        private void 断开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                L_connectedSocket[lbx_connectedNumber.SelectedItem.ToString()].Disconnect(false);
                L_connectedSocket[lbx_connectedNumber.SelectedItem.ToString()].Close();
                cbx_connectedMember.Items.RemoveAt(lbx_connectedNumber.SelectedIndex);
                lbx_connectedNumber.Items.RemoveAt(lbx_connectedNumber.SelectedIndex);
                if (cbx_connectedMember.Items.Count > 0)
                    cbx_connectedMember.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_TCPServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    }
    public struct Help11
    {
        public string str1;
        public string str2;
        public int commType;  //0表示网口通讯1表示串口通讯
    }
}
