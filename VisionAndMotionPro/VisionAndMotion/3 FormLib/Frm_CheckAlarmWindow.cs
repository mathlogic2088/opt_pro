using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace VisionAndMotionPro
{
    internal partial class Frm_AlarmWindow : Form
    {
        internal Frm_AlarmWindow()
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
        /// 报警忽略或重新检测
        /// </summary>
        internal static CheckAlarmWindowResult alarmIgnoreOrCheckAgain = CheckAlarmWindowResult.ConfirmNG;
        /// <summary>
        /// 闪烁线程
        /// </summary>
        private Thread th_shine;
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_AlarmWindow _instance;
        public static Frm_AlarmWindow Instance
        {
            get
            {
                _instance = new Frm_AlarmWindow();
                return _instance;
            }
        }


        /// <summary>
        /// 闪烁
        /// </summary>
        internal void Light()
        {
            while (true)
            {
                if (this.BackColor == Color.Red)
                    this.BackColor = Color.Black;
                else
                    this.BackColor = Color.Red;
                Thread.Sleep(500);
            }
        }


        private void Frm_AlarmWindow_Load(object sender, EventArgs e)
        {
            th_shine = new Thread(Light);
            th_shine.IsBackground = true;
            th_shine.Start();
        }
        private void btn_ignore_Click(object sender, EventArgs e)
        {
            Frm_AlarmWindow.alarmIgnoreOrCheckAgain = CheckAlarmWindowResult.Ignore; 
            this.Close();
        }
        private void btn_checkAgain_Click(object sender, EventArgs e)
        {
            Frm_AlarmWindow.alarmIgnoreOrCheckAgain = CheckAlarmWindowResult.Check_Again;
            this.Close();
        }
        private void btn_confirmNG_Click(object sender, EventArgs e)
        {
            Frm_AlarmWindow.alarmIgnoreOrCheckAgain = CheckAlarmWindowResult.ConfirmNG;
            this.Close();
        }
        private void Frm_AlarmWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            th_shine.Abort();
        }

    }
    internal enum CheckAlarmWindowResult
    {
        Ignore,
        Check_Again,
        ConfirmNG,
    }
}
