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
    public  partial class Frm_ToolBase : Form
    {
        internal Frm_ToolBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前工具所属的流程
        /// </summary>
        internal  string jobName = string.Empty;
        /// <summary>
        /// 当前工具名
        /// </summary>
        internal  string toolName = string.Empty;


        private void Frm_ToolBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    }
}
