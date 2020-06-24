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
    internal partial class Frm_MessageBox : Form
    {
        internal Frm_MessageBox()
        {
            InitializeComponent();
            Init_Language();
        }

        #region 窗体拖动
        private static bool IsDrag = false;
        private int enterX;
        private int enterY;
        private void setForm_MouseDown(object sender, MouseEventArgs e)
        {
            IsDrag = true;
            enterX = e.Location.X;
            enterY = e.Location.Y;
        }
        private void setForm_MouseUp(object sender, MouseEventArgs e)
        {
            IsDrag = false;
            enterX = 0;
            enterY = 0;
        }
        private void setForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDrag)
            {
                Left += e.Location.X - enterX;
                Top += e.Location.Y - enterY;
            }
        }
        #endregion

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_MessageBox _instance;
        public static Frm_MessageBox Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_MessageBox();
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
                    lbl_title.Text = "Tip:";
                    btn_confim.Text = "Confirm";
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 弹框
        /// </summary>
        /// <param name="msg">要显示的信息</param>
        internal void MessageBoxShow(string msg)
        {
            this.lbl_info.Text = msg;
            this.ShowDialog();
        }


        private void btn_confim_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Frm_MessageBox_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

    }
}
