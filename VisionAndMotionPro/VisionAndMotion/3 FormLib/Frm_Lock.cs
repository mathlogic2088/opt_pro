using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tool;

namespace VisionAndMotionPro
{
    internal partial class Frm_Lock : Form
    {
        public Frm_Lock()
        {
            InitializeComponent();
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
        /// X向自动增加
        /// </summary>
        private bool Xadd = true;
        /// <summary>
        /// Y向自动增加
        /// </summary>
        private bool Yadd = true;
        /// <summary>
        /// 窗体是否移动
        /// </summary>
        private bool isMove = true;
        /// <summary>
        /// 一段时间不操作则自动恢复飘动
        /// </summary>
        private int waitTime = 0;


        private void Frm_Lock_Click(object sender, EventArgs e)
        {
            if (isMove)
            {
                isMove = false;
                this.Opacity = 1;
                this.tbx_password.Focus();
            }
            else
            {
                isMove = true;
                this.Opacity = 0.4;
            }

        }
        private void Frm_Lock_Load(object sender, EventArgs e)
        {
            tbx_password.Select();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            tbx_password.Focus();
            Application.DoEvents();
            if (!isMove)
            {
                waitTime++;
                if (waitTime > 5000)
                {
                    isMove = true;
                    waitTime = 0;
                }
                return;
            }

            if (Xadd)
                this.Location = new System.Drawing.Point(this.Location.X + 1, this.Location.Y);
            if (Yadd)
                this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + 1);
            if (!Xadd)
                this.Location = new System.Drawing.Point(this.Location.X - 1, this.Location.Y);
            if (!Yadd)
                this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y - 1);

            if (this.Location.X >= Frm_Main.Instance.Location.X + Frm_Main.Instance.Width - this.Width - 10)
                Xadd = false;
            if (this.Location.Y >= Frm_Main.Instance.Location.Y + Frm_Main.Instance.Height - this.Height - 10)
                Yadd = false;
            if (this.Location.X <= Frm_Main.Instance.Location.X + 10)
                Xadd = true;
            if (this.Location.Y <= Frm_Main.Instance.Location.Y)
                Yadd = true;

            Application.DoEvents();
        }
        private void tbx_password_TextChanged(object sender, EventArgs e)
        {
            if (isMove)
            {
                isMove = false;
                this.Opacity = 1;
                this.tbx_password.Focus();
            }
        }
        private void btn_unlock_Click(object sender, EventArgs e)
        {
            try
            {
                if (Method.GetMD5(tbx_password.Text.Trim()) == Configuration.adminPassword || Method.GetMD5(tbx_password.Text.Trim()) == Configuration.developerPassword)
                {
                    this.Close();
                    Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;
                    //Frm_Main.Instance.buttonItem45.Checked = false;
                }
                else
                {
                    tbx_password.Clear();
                    label1.Text = "密码错误！";
                    label1.ForeColor = Color.Red;
                    Application.DoEvents();
                    Thread.Sleep(1000);
                    label1.Text = "锁定中......";
                    label1.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void tbx_password_Click(object sender, EventArgs e)
        {
            if (isMove)
            {
                isMove = false;
                this.Opacity = 1;
                this.tbx_password.Focus();
            }
        }

    }
}
