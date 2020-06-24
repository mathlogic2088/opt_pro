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
    public partial class Frm_ReadFromLocalIMAVision : Frm_ToolBase
    {
        public Frm_ReadFromLocalIMAVision()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_ReadFromLocalIMAVision _instance;
        public static Frm_ReadFromLocalIMAVision Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ReadFromLocalIMAVision();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static SDK_IMAVisionTool SDK_imaVisionTool = new SDK_IMAVisionTool();


        private void btn_lastImage_Click(object sender, EventArgs e)
        {
            SDK_imaVisionTool.Read_Last_Image();
        }
        private void btn_nextImage_Click(object sender, EventArgs e)
        {
            SDK_imaVisionTool.Read_Next_Image();
        }
        private void btn_selectImagePath_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            SDK_imaVisionTool.Select_Image_Path(out path);
            this.tbx_imagePath.Text = path;
        }
        private void btn_readImage_Click(object sender, EventArgs e)
        {
            SDK_imaVisionTool.Read_Image();
        }
        private void ckb_autoSwitch_CheckedChanged(object sender, EventArgs e)
        {
            SDK_imaVisionTool.autoSwitch = this.ckb_autoSwitch.Checked;
        }
        private void btn_selectImageDirectory_Click(object sender, EventArgs e)
        {
            string directoryPath = string.Empty;
            SDK_imaVisionTool.Select_Image_Directory(out directoryPath);
            tbx_imageDirectory.Text = directoryPath;
        }
        private void rdo_readMultImage_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_readOneImage.Checked)
            {
                this.ckb_autoSwitch.Visible = false;
                this.pnl_multImage.Visible = false;
                this.btn_browseImage.Visible = false;
                SDK_imaVisionTool.workMode = WorkMode.ReadOneImage;
            }
            else
            {
                this.ckb_autoSwitch.Visible = true;
                this.pnl_multImage.Visible = true;
                this.btn_browseImage.Visible = true;
                SDK_imaVisionTool.workMode = WorkMode.ReadMultImage;
            }
        }
        private void btn_registImage_Click(object sender, EventArgs e)
        {
            SDK_imaVisionTool.Regist_Images();
        }
        private void ckb_RGBToGray_CheckedChanged(object sender, EventArgs e)
        {
            SDK_imaVisionTool.RGBToGray = ckb_RGBToGray.Checked;
        }
        private void btn_runIMAVisionTool_Click(object sender, EventArgs e)
        {
            SDK_imaVisionTool.Run(jobName, true, true);
            if (SDK_imaVisionTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(SDK_imaVisionTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(SDK_imaVisionTool.runStatu.ToString(), Color.Green);
        }

    }
}
