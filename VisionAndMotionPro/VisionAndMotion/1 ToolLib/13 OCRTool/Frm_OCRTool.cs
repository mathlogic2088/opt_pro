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
    internal partial class Frm_OCRTool : Frm_ToolBase
    {
        internal Frm_OCRTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_OCRTool _instance;
        public static Frm_OCRTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_OCRTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static OCRTool ocrTool = new OCRTool();


        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_ToolHelp.Instance.ShowToolHelp("Halcon采集接口",
                                                "此工具以Halcon的采集接口为基础获取图像，支持从设备采集图像和从本地读取图像两种工作模式，可自如切换。",
                                                "1. 将工具添加到流程；\r\n2. 打开工具，选择图像获取模式(从设备采集或从本地读取)；\r\n3. 从设备列表选定图像采集设备(从设备采集模式)或指定图像路径(从本地读取模式)；",
                                                "无"
                                                );
        }
        private void tsb_resetTool_Click(object sender, EventArgs e)
        {
            ocrTool.ResetTool();
        }
        private void btn_drawTemplateRegion_Click(object sender, EventArgs e)
        {
            ocrTool.Draw_Template_Region();
        }
        private void btn_deleteTemplateRegion_Click(object sender, EventArgs e)
        {
            ocrTool.ClearTemplateRegion();
        }
        private void tkb_threshold_Scroll(object sender, EventArgs e)
        {
            lbl_threshold.Text = tkb_threshold.Value.ToString();
            ocrTool.threshold =tkb_threshold.Value;
            ocrTool.Train();
        }
        private void cbx_templateRegionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ocrTool.Draw_Template_Region();
        }
        private void ckb_OCRToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            Job.GetToolInfoByToolName(jobName, toolName).enable = ckb_OCRToolEnable.Checked;
        }
        private void btn_trainChar_Click(object sender, EventArgs e)
        {
            ocrTool.charType = (cbx_charType.SelectedIndex == 0 ? CharType.BlackChar : CharType.WhiteChar);
            ocrTool.standardCharList = tbx_standardCharList.Text.Trim();
            ocrTool.threshold = tkb_threshold.Value;
            ocrTool.dilationSize = Convert.ToInt16(tbx_dilationSize.Text.Trim());
            ocrTool.standardImage = ocrTool.inputImage;
            ocrTool.Train();
        }
        private void btn_drawSearchRegion_Click(object sender, EventArgs e)
        {
            ocrTool.Draw_Search_Region();
        }
        private void cbx_searchRegionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ocrTool.Draw_Search_Region();
        }
        private void tsb_runJob_Click(object sender, EventArgs e)
        {
            Job.GetJobByName(jobName).Run();
        }
        private void btn_removeSearchRegion_Click(object sender, EventArgs e)
        {
            ocrTool.clearSearchRegion();
        }
        private void btn_runOCRTool_Click(object sender, EventArgs e)
        {
            ocrTool.Run( jobName,true,false  );
            if (ocrTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(ocrTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(ocrTool.runStatu.ToString(), Color.Green);
        }
        private void tsb_runTool_Click(object sender, EventArgs e)
        {
            ocrTool.Run(jobName,true,false  );
            if (ocrTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(ocrTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(ocrTool.runStatu.ToString(), Color.Green);
        }

    }
}
