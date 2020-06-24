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
    public partial class Frm_AcqFromDeviceBasler : Frm_ToolBase
    {
        public Frm_AcqFromDeviceBasler()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_AcqFromDeviceBasler _instance;
        public static Frm_AcqFromDeviceBasler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_AcqFromDeviceBasler();
                return _instance;
            }
        }
        /// <summary>
        /// 工具对象
        /// </summary>
        internal static SDK_BaslerTool SDK_baslerTool = new SDK_BaslerTool();


        private void tkb_exposure_Scroll(object sender, EventArgs e)
        {
            tbx_exposure.Text = tkb_exposure.Value.ToString();

        }
        private void tbx_exposure_TextChanged(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (RegexJudge.IsInt(tbx_exposure.Text.Trim()))
            {
                SDK_baslerTool.exposure = Convert.ToInt32(tbx_exposure.Text.Trim());
                SDK_baslerTool.Set_Exposure(jobName);
            }
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
            SDK_baslerTool.save_Image();
        }
        private void btn_RealTime_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Real_Time_Acquistion();
        }
        private void cbx_deviceList_TextChanged(object sender, EventArgs e)
        {
            SDK_baslerTool.Device_Changed(cbx_deviceList.Text);
        }
        private void btn_registImage_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Regist_Images();
        }
        private void ckb_RGBToGray_CheckedChanged(object sender, EventArgs e)
        {
            SDK_baslerTool.RGBToGray = ckb_RGBToGray.Checked;
        }
        private void btn_runSDKBaslerTool_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Run(jobName,true,true  );
            if (SDK_baslerTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(SDK_baslerTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(SDK_baslerTool.runStatu.ToString(), Color.Green);
        }

    }
}
