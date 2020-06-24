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
    public partial class Frm_ReadFromLocalMindVision : Frm_ToolBase
    {
        public Frm_ReadFromLocalMindVision()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_ReadFromLocalMindVision _instance;
        public static Frm_ReadFromLocalMindVision Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ReadFromLocalMindVision();
                return _instance;
            }
        }

        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static SDK_MindVisionTool SDK_mindVisionTool = new SDK_MindVisionTool();


        private void btn_lastImage_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Read_Last_Image();
        }
        private void btn_nextImage_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Read_Next_Image();
        }
     
        private void btn_selectImagePath_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Select_Image_Path();
        }
        private void btn_readImage_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Read_Image();
        }
        private void ckb_colorToGray_CheckedChanged(object sender, EventArgs e)
        {
            SDK_mindVisionTool.RGBToGray = this.ckb_RGBToGray.Checked;
        }
        private void ckb_autoSwitch_CheckedChanged(object sender, EventArgs e)
        {
            SDK_mindVisionTool.autoSwitch = this.ckb_autoSwitch.Checked;
        }
        private void btn_selectImageDirectory_Click_1(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Select_Image_Directory();
        }
        private void rdo_readMultImage_CheckedChanged(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Work_Mode_Changed();
        }
        internal virtual void btn_runImageAcquistionTool_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Run( jobName,true ,true );
            if (SDK_mindVisionTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(SDK_mindVisionTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(SDK_mindVisionTool.runStatu.ToString(), Color.Green);
        }
        private void tbx_imageDirectory_TextChanged(object sender, EventArgs e)
        {
            SDK_mindVisionTool.imageDirectoryPath = tbx_imageDirectory.Text.Trim();
        }
        private void tbx_imagePath_TextChanged(object sender, EventArgs e)
        {
            SDK_mindVisionTool.imagePath = tbx_imagePath.Text.Trim();
        }
        private void btn_browseImage_Click(object sender, EventArgs e)
        {
            Process.Start(SDK_mindVisionTool.imageDirectoryPath);
            Frm_HalconInterfaceTool.Instance.TopMost = false;
        }
        private void btn_selectImageDirectory_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Select_Image_Directory();
        }
        private void btn_registImage_Click(object sender, EventArgs e)
        {
            SDK_mindVisionTool.Regist_Images();
        }
        private void ckb_RGBToGray_CheckedChanged(object sender, EventArgs e)
        {
            SDK_mindVisionTool.RGBToGray = ckb_RGBToGray.Checked;
        }

    }
}
