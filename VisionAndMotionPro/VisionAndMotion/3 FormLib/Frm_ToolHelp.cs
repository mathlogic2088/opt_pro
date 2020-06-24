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
    public partial class Frm_ToolHelp : Form
    {
        public Frm_ToolHelp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_ToolHelp _instance;
        public static Frm_ToolHelp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ToolHelp();
                return _instance;
            }
        }


        internal void ShowToolHelp(string toolType, string toolFunc, string useStep,string attentionItem)
        {
            this.tbx_toolType.Text = toolType;
            this.tbx_toolFunc.Text = toolFunc;
            this.tbx_useStep.Text = useStep;
            this.tbx_attentionItem.Text = attentionItem;
            this.TopMost = true;
            this.ShowDialog();
        }

    }
}
