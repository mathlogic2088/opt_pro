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
    internal partial class Frm_LabelTool : Frm_ToolBase 
    {
        public Frm_LabelTool()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_LabelTool _instance;
        public static Frm_LabelTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_LabelTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static LabelTool labelTool = new LabelTool();


        private void dgv_outputItem2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            labelTool.SaveData2();
        }
        private void btn_runLabelTool_Click(object sender, EventArgs e)
        {
            labelTool.Run(jobName,true ,true );
        }
        private void tsb_runOnce_Click(object sender, EventArgs e)
        {
            labelTool.Run(jobName,true ,true );
        }
        private void tsb_resetTool_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void ckb_labelToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            (Job.GetToolInfoByToolName(jobName, toolName)).enable = ckb_labelToolEnable.Checked;
        }

    }
}
