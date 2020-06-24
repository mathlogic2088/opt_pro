using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    internal partial class Frm_Welcome : Form
    {
        internal Frm_Welcome()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
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
        private static Frm_Welcome _instance;
        internal static Frm_Welcome Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_Welcome();
                return _instance;
            }
        }


        private void Frm_Welcome_Load(object sender, EventArgs e)
        {
            lbl_version.Text = "V " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            bar_step.Maximum = 100;
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

    }
}
