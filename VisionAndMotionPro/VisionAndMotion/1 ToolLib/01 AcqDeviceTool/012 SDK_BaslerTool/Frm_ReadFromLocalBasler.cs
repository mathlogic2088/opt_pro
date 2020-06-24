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
    public partial class Frm_ReadFromLocalBasler : Frm_ToolBase
    {
        public Frm_ReadFromLocalBasler()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_ReadFromLocalBasler _instance;
        public static Frm_ReadFromLocalBasler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ReadFromLocalBasler();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static SDK_BaslerTool SDK_baslerTool = new SDK_BaslerTool();


        private void btn_lastImage_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Read_Last_Image();
        }
        private void btn_nextImage_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Read_Next_Image();
        }
        private void btn_selectImagePath_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            SDK_baslerTool.Select_Image_Path(out path);
            this.tbx_imagePath.Text = path;
        }
        private void btn_readImage_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Read_Image(tbx_imagePath.Text.Trim());
        }
        private void ckb_autoSwitch_CheckedChanged(object sender, EventArgs e)
        {
            SDK_baslerTool.autoSwitch = this.ckb_autoSwitch.Checked;
        }
        private void btn_selectImageDirectory_Click(object sender, EventArgs e)
        {
            string directoryPath = string.Empty;
            SDK_baslerTool.Select_Image_Directory(out directoryPath);
            tbx_imageDirectory.Text = directoryPath;
        }
        private void rdo_readMultImage_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_readOneImage.Checked)
            {
                this.ckb_autoSwitch.Visible = false;
                this.pnl_multImage.Visible = false;
                this.btn_browseImage.Visible = false;
                SDK_baslerTool.workMode = WorkMode.ReadOneImage;
            }
            else
            {
                this.ckb_autoSwitch.Visible = true;
                this.pnl_multImage.Visible = true;
                this.btn_browseImage.Visible = true;
                SDK_baslerTool.workMode = WorkMode.ReadMultImage;
            }
        }
        private void tbx_imageDirectory_TextChanged(object sender, EventArgs e)
        {
            SDK_baslerTool.imageDirectoryPath = tbx_imageDirectory.Text.Trim();
        }
        private void btn_registImage_Click(object sender, EventArgs e)
        {
            SDK_baslerTool.Regist_Images();
        }
        private void ckb_RGBToGray_CheckedChanged(object sender, EventArgs e)
        {
            SDK_baslerTool.RGBToGray = this.ckb_RGBToGray.Checked;
        }
        private void tbx_imagePath_TextChanged(object sender, EventArgs e)
        {
            SDK_baslerTool.imagePath = tbx_imagePath.Text.Trim();
        }
        private void btn_browseImage_Click(object sender, EventArgs e)
        {
            Process.Start(SDK_baslerTool.imageDirectoryPath);
            Frm_SDK_BaslerTool.Instance.TopMost = false;
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
