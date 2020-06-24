using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Tool;

namespace VisionAndMotionPro
{
    internal partial class Frm_InputMessage : Form
    {
        internal Frm_InputMessage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否以密码的形式输入
        /// </summary>
        internal bool passwordChar = false;
        /// <summary>
        /// 信息输入窗体输入的信息
        /// </summary>
        internal static string input = string.Empty;
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_InputMessage _instance;
        public static Frm_InputMessage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_InputMessage();
                return _instance;
            }
        }


        private void btn_confirm_Click(object sender, EventArgs e)
        {
            input = txt_input.Text.Trim();
            this.Close();
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            input = string.Empty;
            this.Close();
        }
        private void Frm_InputMessage_Load(object sender, EventArgs e)
        {
            try
            {
                if (!passwordChar)
                {
                    txt_input.PasswordChar = '\0';
                }
                else
                {
                    txt_input.PasswordChar = '*';
                }
                txt_input.Select();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
