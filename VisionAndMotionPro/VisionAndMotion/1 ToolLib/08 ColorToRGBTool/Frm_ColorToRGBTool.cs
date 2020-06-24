using HalconDotNet;
using Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    internal partial class Frm_ColorToRGBTool : Frm_ToolBase 
    {
        internal Frm_ColorToRGBTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_ColorToRGBTool _instance;
        public static Frm_ColorToRGBTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ColorToRGBTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static ColorToRGBTool colorToRGBTool = new ColorToRGBTool();


        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_ToolHelp.Instance.ShowToolHelp("形状匹配",
                                                "此工具可创建模板，并在输入图像中搜索模板位置。",
                                                "1. 将工具添加到流程；\r\n2. 指定图像输入；\r\n3. 创建匹配模板；",
                                                "1. 创建模板前必须有正常的图像输入；\r\n2. 输入输出配置完成后需运行一次流程，这样才能使前面工具的输出项传递到本工具的输入项"
                                                );
        }
        private void tsb_resetTool_Click(object sender, EventArgs e)
        {
            colorToRGBTool.ResetTool();
        }
        private void ckb_colorToRGBToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            Job.GetToolInfoByToolName(jobName, toolName).enable = ckb_colorToRGBToolEnable.Checked;
        }
        private void tsb_runTool_Click(object sender, EventArgs e)
        {
            colorToRGBTool.Run(jobName,true ,true );
            if (colorToRGBTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(colorToRGBTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(colorToRGBTool.runStatu.ToString(), Color.Green);
        }
        private void btn_runColorToRGBTool_Click(object sender, EventArgs e)
        {
            colorToRGBTool.Run( jobName,true ,true );
            if (colorToRGBTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(colorToRGBTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(colorToRGBTool.runStatu.ToString(), Color.Green);
        }
       
    }
}
