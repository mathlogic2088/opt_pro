using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionAndMotionPro
{
    public partial class Frm_UserForm : Form 
    {
        public Frm_UserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_UserForm _instance;
        public static Frm_UserForm Instance
        {
            get
            {
                if (_instance == null||_instance .IsDisposed )
                    _instance = new Frm_UserForm();
                return _instance;
            }
        }


        /// <summary>
        /// 信息输出
        /// </summary>
        /// <param name="msg">要输出的信息</param>
        internal void OutputMsg(string msg)
        {
            try
            {
                tbx_output.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + msg + "\r\n");
                if (tbx_output.Lines.Count() > 100)
                    tbx_output.Clear();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex );
            }
        }


        private void Frm_UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void Frm_UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Machine.productionMode)
            {
                LogHelper.SaveLog(LogType.Operate, Configuration.language == Language.English ? "Switch to debug page" : "切换到调试页面");
                Frm_Login.Instance.ShowDialog();
            }
        }

    }
}
