using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    internal partial class Frm_ConfirmBox : Form
    {
        internal Frm_ConfirmBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_ConfirmBox _instance;
        public static Frm_ConfirmBox Instance
        {
            get
            {
                if (_instance ==null )
                _instance = new Frm_ConfirmBox();
                return _instance;
            }
        }
        /// <summary>
        /// 选择结果
        /// </summary>
        internal ConfirmBoxResult result = ConfirmBoxResult.Confirm;


        private void btn_cancel_Click(object sender, EventArgs e)
        {
            result = ConfirmBoxResult.Cancel;
            this.Hide();
        }
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            result = ConfirmBoxResult.Confirm;
            this.Hide();
        }
        private void Frm_ConfirmBox_Load(object sender, EventArgs e)
        {
            Frm_ConfirmBox.Instance.TopMost = true;
        }

    }
    internal enum ConfirmBoxResult
    {
        Cancel,
        Confirm,
    }
}
