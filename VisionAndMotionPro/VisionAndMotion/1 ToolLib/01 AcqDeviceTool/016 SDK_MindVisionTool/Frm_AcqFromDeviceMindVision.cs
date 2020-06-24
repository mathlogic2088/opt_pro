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
    public partial class Frm_AcqFromDeviceMindVision : Frm_ToolBase
    {
        public Frm_AcqFromDeviceMindVision()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_AcqFromDeviceMindVision _instance;
        public static Frm_AcqFromDeviceMindVision Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_AcqFromDeviceMindVision();
                return _instance;
            }
        }

        /// <summary>
        /// 工具对象
        /// </summary>
        internal static SDK_MindVisionTool SDK_mindVisionTool = new SDK_MindVisionTool();


        private void tkb_exposure_Scroll(object sender, EventArgs e)
        {
            tbx_exposure.Text = tkb_exposure.Value.ToString();
        }
        private void tbx_exposure_TextChanged(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (RegexJudge.IsInt(tbx_exposure.Text.Trim()))
                SDK_mindVisionTool.Set_Exposure(jobName);
            else if (tbx_exposure.Text.Trim() == string.Empty || tbx_exposure.Text.Trim() == "-")
            {
                //不做事
            }
            else
            {
                Frm_Main.Instance.OutputMsg("曝光值不合法，请输入整型值（错误代码：0101）", Color.Red);
            }
        }
        private void btn_saveImage_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.save_Image();
        }
        private void btn_addToStandardImage_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Regist_Images();
        }
        private void btn_RealTime_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Real_Time_Acquistion();
        }
      
        private void cbx_deviceList_TextChanged(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Device_Changed(cbx_deviceList.Text);
        }
        private void btn_runImageAcquistionTool_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Run(jobName,true,false  );
            if (SDK_mindVisionTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(SDK_mindVisionTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(SDK_mindVisionTool.runStatu.ToString(), Color.Green);
        }

        private void ckb_RGBToGray_CheckedChanged(object sender, EventArgs e)
        {
            SDK_mindVisionTool.RGBToGray = ckb_RGBToGray.Checked;
        }

    }
}
