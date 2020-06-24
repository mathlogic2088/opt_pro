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
    public partial class Frm_InitItemStatu : Form
    {
        public Frm_InitItemStatu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_InitItemStatu _instance;
        internal static Frm_InitItemStatu Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_InitItemStatu();
                return _instance;
            }
        }


        //更新进度
        internal void UpdateStep(int percentValue, string stepMsg, bool succeed)
        {
            try
            {
                Frm_Welcome.Instance .bar_step.Value = percentValue;
                Frm_Welcome.Instance .lbl_step.Text = stepMsg + "......";
                int index = dataGridView1.Rows.Add();

                string temp = stepMsg + (succeed ? "成功" : "失败");
                string temp1 = stepMsg + (succeed ? "成功" : "失败");
                for (int i = 0; i < 26 - temp.Length; i++)
                {
                    temp1 = temp1 + "  ";
                }
                temp1 = temp1 + (succeed ? "√" : "×");
                dataGridView1.Rows[index].Cells[0].Value = temp1;

                dataGridView1.Rows[index].Cells[0].Style.ForeColor = (succeed == true ? Color.Green : Color.Red);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Main.Instance.ShowDialog();
        }

    }
}
