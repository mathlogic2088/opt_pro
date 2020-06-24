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
    public partial class Frm_SDK_MindVisionTool : Frm_ToolBase 
    {
        public Frm_SDK_MindVisionTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_SDK_MindVisionTool _instance;
        public static Frm_SDK_MindVisionTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_SDK_MindVisionTool();
                return _instance;
            }
        }

        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static SDK_MindVisionTool SDK_mindVisionTool = new SDK_MindVisionTool();


        private void btn_fromDevice_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Work_Mode_Changed(sender);
        }
        private void btn_fromLocal_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Work_Mode_Changed(sender);
        }
        private void tsb_realTimeDisplay_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Real_Time_Acquistion();
        }
        private void tsb_reset_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_ToolHelp.Instance.ShowToolHelp("Halcon采集接口",
                                                "此工具以Halcon的采集接口为基础获取图像，支持从设备采集图像和从本地读取图像两种工作模式，可自如切换。",
                                                "1. 将工具添加到流程；\r\n2. 打开工具，选择图像获取模式(从设备采集或从本地读取)；\r\n3. 从设备列表选定图像采集设备(从设备采集模式)或指定图像路径(从本地读取模式)；",
                                                "无"
                                                );
        }
        private void tsb_regiestImage_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Regist_Images();
        }
        private void tsb_saveImageTo_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.save_Image();
        }
        private void ckb_imageAcquistionToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (Frm_Main.ignore)
                return;
            Job.GetToolInfoByToolName(jobName, toolName).enable = ckb_SDKMindVisionToolEnable.Checked;
        }

        private void tsb_runTool_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Run( jobName,true,true  );
            if (SDK_mindVisionTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(SDK_mindVisionTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(SDK_mindVisionTool.runStatu.ToString(), Color.Green);
        }
      

    }
}
