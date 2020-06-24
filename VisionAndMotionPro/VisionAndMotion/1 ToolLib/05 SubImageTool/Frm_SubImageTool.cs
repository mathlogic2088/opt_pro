using HalconDotNet;
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
    internal partial class Frm_SubImageTool : Frm_ToolBase
    {
        internal Frm_SubImageTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_SubImageTool _instance;
        public static Frm_SubImageTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_SubImageTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static SubImageTool subImageTool = new SubImageTool();


        private void btn_runImageSubTool_Click(object sender, EventArgs e)
        {
            btn_runImageSubTool.Enabled = false;
            subImageTool.Run( jobName,true ,true );
            if (subImageTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(subImageTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(subImageTool.runStatu.ToString(), Color.Green);
            btn_runImageSubTool.Enabled = true;
        }
        private void cbo_templateImageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            subImageTool.standardImageName = cbx_standardImage.Text;
        }
        private void ckb_subImageToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            (Job.GetToolInfoByToolName(jobName, toolName)).enable = ckb_subImageToolEnable.Checked;
        }
        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_ToolHelp.Instance.ShowToolHelp("Halcon采集接口",
                                              "此工具以Halcon的采集接口为基础获取图像，支持从设备采集图像和从本地读取图像两种工作模式，可自如切换。",
                                              "1. 将工具添加到流程；\r\n2. 打开工具，选择图像获取模式(从设备采集或从本地读取)；\r\n3. 从设备列表选定图像采集设备(从设备采集模式)或指定图像路径(从本地读取模式)；",
                                              "无"
                                              );
        }
        private void tsb_runOnce_Click(object sender, EventArgs e)
        {
            btn_runImageSubTool.Enabled = false;
            subImageTool.Run( jobName,true,true  );
            if (subImageTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(subImageTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(subImageTool.runStatu.ToString(), Color.Green);
            btn_runImageSubTool.Enabled = true;
        }
        private void tsb_resetTool_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void btn_subResultImage_Click(object sender, EventArgs e)
        {
            subImageTool.ShowImage(jobName ,subImageTool .outputImage );
        }

    }
}
