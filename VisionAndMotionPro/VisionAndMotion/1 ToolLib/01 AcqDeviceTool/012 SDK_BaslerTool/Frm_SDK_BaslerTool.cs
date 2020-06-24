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
    public partial class Frm_SDK_BaslerTool : Frm_ToolBase
    {
        public Frm_SDK_BaslerTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_SDK_BaslerTool _instance;
        public static Frm_SDK_BaslerTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_SDK_BaslerTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static SDK_BaslerTool SDK_baslerTool = new SDK_BaslerTool();


        /// <summary>
        /// 图像源模式切换
        /// </summary>
        /// <param name="sender"></param>
        private void Image_Source_Mode_Changed(object sender)
        {
            if (((Button)sender).Text == "设  备  采  集")
            {
                this.tsb_realTimeDisplay.Enabled = true;
                this.btn_fromDevice.BackColor = Color.LimeGreen;
                this.btn_fromLocal.BackColor = Color.LightGray;
                this.pnl_formBox.Controls.Clear();
                Frm_AcqFromDeviceBasler.Instance.TopLevel = false;
                Frm_AcqFromDeviceBasler.Instance.Parent = this.pnl_formBox;
                Frm_AcqFromDeviceBasler.Instance.Dock = DockStyle.Fill;
                Frm_AcqFromDeviceBasler.Instance.Show();
                Frm_AcqFromDeviceBasler.Instance.btn_runSDKBaslerTool.Focus();
                SDK_baslerTool.imageSourceMode = ImageSourceMode.FormDevice;
            }
            else
            {
                this.tsb_realTimeDisplay.Enabled = false;
                this.btn_fromLocal.BackColor = Color.LimeGreen;
                this.btn_fromDevice.BackColor = Color.LightGray;
                this.pnl_formBox.Controls.Clear();
                Frm_ReadFromLocalBasler.Instance.TopLevel = false;
                Frm_ReadFromLocalBasler.Instance.Parent = this.pnl_formBox;
                Frm_ReadFromLocalBasler.Instance.Dock = DockStyle.Fill;
                Frm_ReadFromLocalBasler.Instance.Show();
                Frm_ReadFromLocalBasler.Instance.btn_runSDKBaslerTool.Focus();
                SDK_baslerTool.imageSourceMode = ImageSourceMode.FromLocal;
            }
        }


        private void btn_fromDevice_Click(object sender, EventArgs e)
        {
            Image_Source_Mode_Changed(sender);
        }
        private void btn_fromLocal_Click(object sender, EventArgs e)
        {
            Image_Source_Mode_Changed(sender);
        }
        private void tsb_realTimeDisplay_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Real_Time_Acquistion();
        }
        private void tsb_reset_Click(object sender, EventArgs e)
        {
            Frm_AcqFromDeviceBasler.Instance.cbx_deviceList.Text = string.Empty;
            Frm_AcqFromDeviceBasler.Instance.tbx_exposure.Text = "0";
            Frm_AcqFromDeviceBasler.Instance.lbl_exposureRange.Text = "0 ~ 0";
            Frm_ReadFromLocalBasler.Instance.tbx_imageDirectory.Text = string.Empty;
            Frm_ReadFromLocalBasler.Instance.tbx_imagePath.Text = string.Empty;
            Frm_ReadFromLocalBasler.Instance.lbl_imageName.Text = string.Empty;
            Frm_ReadFromLocalBasler.Instance.lbl_imageNum.Text = "共0张";
            SDK_baslerTool.Reset_Tool();
        }
        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_ToolHelp.Instance.ShowToolHelp("SDK_巴斯勒",
                                               "此工具以巴斯勒相机的SDK为基础获取图像，支持从设备采集图像和从本地读取图像两种工作模式，可自如切换。",
                                               "1. 将工具添加到流程；\r\n2. 打开工具，选择图像获取模式(从设备采集或从本地读取)；\r\n3. 从设备列表选定图像采集设备(从设备采集模式)或指定图像路径(从本地读取模式)；",
                                               "无"
                                               );
        }
        private void tsb_regiestImage_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Regist_Images();
        }
        private void ckb_SDKBaslerToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (Frm_Main.ignore)
                return;
            Job.GetToolInfoByToolName(jobName, toolName).enable = ckb_SDKBaslerToolEnable.Checked;
        }
        private void tsb_runJob_Click(object sender, EventArgs e)
        {
            Job.GetJobByName(jobName).Run();
        }
        private void tsb_saveImage_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.save_Image();
        }
        private void tsb_runTool_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Run(jobName,true,true  );
            if (SDK_baslerTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(SDK_baslerTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(SDK_baslerTool.runStatu.ToString(), Color.Green);
        }

    }
}
