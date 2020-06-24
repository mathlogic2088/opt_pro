using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    internal partial class Frm_SerialPort : Form
    {
        internal Frm_SerialPort()
        {
            InitializeComponent();
            Init_Language();
        }

        /// <summary>
        /// 通讯用串口类
        /// </summary>
        internal SerialPort serialPort = new SerialPort();
        /// <summary>
        /// 串口号
        /// </summary>
        private string portName = "COM1";
        /// <summary>
        /// 波特率
        /// </summary>
        private int baudRate = 9600;
        /// <summary>
        /// 数据位
        /// </summary>
        private int dataBit = 8;
        /// <summary>
        /// 停止位
        /// </summary>
        private StopBits stopBit = (StopBits)Enum.Parse(typeof(StopBits), "One");
        /// <summary>
        /// 奇偶效验位
        /// </summary>
        private Parity parity = (Parity)Enum.Parse(typeof(Parity), "Odd");
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_SerialPort _instance;
        public static Frm_SerialPort Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_SerialPort();
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
                    this.Text = "Serial";
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    Frm_MessageBox.Instance.MessageBoxShow("\r\n串口未打开，请打开后重试");
                    return;
                }
                string receiveStr = serialPort.ReadExisting();
                if (receiveStr != "")
                {
                    tbx_output.Text += DateTime.Now.ToString("HH:mm:ss") + "<-:  " + receiveStr + Environment.NewLine;

                    Help11 help11 = new Help11();
                    help11.commType = 1;
                    help11.str1 = receiveStr;
                    help11.str2 = "temp";// socket.RemoteEndPoint.ToString();
                    Thread th = new Thread(Frm_Main.Protocol);
                    th.IsBackground = true;
                    th.Start(help11);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        private void OpenPort()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.NewLine = "\r\n";
                    serialPort.RtsEnable = false;
                    serialPort.PortName = portName;
                    serialPort.BaudRate = baudRate;
                    serialPort.DataBits = dataBit;
                    serialPort.StopBits = stopBit;
                    serialPort.Parity = parity;
                    serialPort.Open();
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                    tbx_output.Text = "打开串口成功";
                    lbl_statu.Text = "已打开";
                    lbl_statu.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                lbl_statu.Text = "未打开";
                lbl_statu.ForeColor = Color.Red;
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        private void ClosePort()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    lbl_statu.Text = "未打开";
                    lbl_statu.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }


        private void Frm_Serial_Load(object sender, EventArgs e)
        {
            try
            {
                cbx_portName.Items.Clear();
                cbx_portName.Items.AddRange(SerialPort.GetPortNames());

                cbx_parityBit.Items.Clear();
                foreach (var item in Enum.GetValues(typeof(Parity)))
                {
                    cbx_parityBit.Items.Add(item.ToString());
                }

                cbx_stopBit.Items.Clear();
                foreach (var item in Enum.GetValues(typeof(StopBits)))
                {
                    cbx_stopBit.Items.Add(item.ToString());
                }

                if (cbx_portName.Items.Count > 0)
                    cbx_portName.SelectedIndex = 0;
                if (cbx_parityBit.Items.Count > 0)
                    cbx_parityBit.SelectedIndex = 1;
                if (cbx_stopBit.Items.Count > 1)
                    cbx_stopBit.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void cbx_portName_SelectedIndexChanged(object sender, EventArgs e)
        {
            portName = cbx_portName.Text.Trim();
        }
        private void cbx_baudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RegexJudge.IsInt(cbx_baudRate.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：9600", Color.Red);
                cbx_baudRate.Text = "9600";
                return;
            }
            baudRate = Convert.ToInt32(cbx_baudRate.Text.Trim());
        }
        private void tbx_dataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RegexJudge.IsInt(cbx_baudRate.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：8", Color.Red);
                tbx_dataBit.Text = "8";
                return;
            }
            dataBit = Convert.ToInt32(tbx_dataBit.Text.Trim());
        }
        private void cbx_stopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            stopBit = (StopBits)Enum.Parse(typeof(StopBits), cbx_stopBit.Text);
        }
        private void cbx_parityBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            parity = (Parity)Enum.Parse(typeof(Parity), cbx_parityBit.Text);
        }
        internal void btn_openPort_Click(object sender, EventArgs e)
        {
            OpenPort();
        }
        private void btn_closePort_Click(object sender, EventArgs e)
        {
            ClosePort();
        }
        private void lnk_clear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbx_output.Clear();
        }
        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    Frm_MessageBox.Instance.MessageBoxShow("\r\n串口未打开，请打开后重试");
                    return;
                }
                if (tbx_sendMsg.Text.Trim() == string.Empty)
                {
                    Frm_MessageBox.Instance.MessageBoxShow("\r\n不能发送空字符串");
                    return;
                }
                serialPort.ReadExisting();
                serialPort.WriteLine(tbx_sendMsg.Text.Trim());
                tbx_output.Text += DateTime.Now.ToString("HH:mm:ss") + "->:  " + tbx_sendMsg.Text.Trim() + Environment.NewLine;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_receiveMsg_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
