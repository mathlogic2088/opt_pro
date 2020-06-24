﻿using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    public partial class Frm_SDK_CognexTool : Frm_ToolBase
    {
        public Frm_SDK_CognexTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_SDK_CognexTool _instance;
        public static Frm_SDK_CognexTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_SDK_CognexTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static SDK_CongexTool SDK_congexTool = new SDK_CongexTool();


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
                Frm_AcqFromDeviceCognex.Instance.TopLevel = false;
                Frm_AcqFromDeviceCognex.Instance.Parent = this.pnl_formBox;
                Frm_AcqFromDeviceCognex.Instance.Dock = DockStyle.Fill;
                Frm_AcqFromDeviceCognex.Instance.Show();
                Frm_AcqFromDeviceCognex.Instance.btn_runSDKCognexTool.Focus();
                SDK_congexTool.imageSourceMode = ImageSourceMode.FormDevice;
            }
            else
            {
                this.tsb_realTimeDisplay.Enabled = false;
                this.btn_fromLocal.BackColor = Color.LimeGreen;
                this.btn_fromDevice.BackColor = Color.LightGray;
                this.pnl_formBox.Controls.Clear();
                Frm_ReadFromLocalCognex.Instance.TopLevel = false;
                Frm_ReadFromLocalCognex.Instance.Parent = this.pnl_formBox;
                Frm_ReadFromLocalCognex.Instance.Dock = DockStyle.Fill;
                Frm_ReadFromLocalCognex.Instance.Show();
                Frm_ReadFromLocalCognex.Instance.btn_runSDKCognexTool.Focus();
                SDK_congexTool.imageSourceMode = ImageSourceMode.FromLocal;
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
            SDK_congexTool.Real_Time_Acquistion();
        }
        private void tsb_reset_Click(object sender, EventArgs e)
        {
            Frm_AcqFromDeviceCognex.Instance.cbx_deviceList.Text = string.Empty;
            Frm_AcqFromDeviceCognex.Instance.tbx_exposure.Text = "0";
            Frm_AcqFromDeviceCognex.Instance.lbl_exposureRange.Text = "0 ~ 0";
            Frm_ReadFromLocalCognex.Instance.tbx_imageDirectory.Text = string.Empty;
            Frm_ReadFromLocalCognex.Instance.tbx_imagePath.Text = string.Empty;
            Frm_ReadFromLocalCognex.Instance.lbl_imageName.Text = string.Empty;
            Frm_ReadFromLocalCognex.Instance.lbl_imageNum.Text = "共0张";
            SDK_congexTool.Reset_Tool();
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
            SDK_congexTool.Regist_Images();
        }
        private void ckb_SDKCognexToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (Frm_Main.ignore)
                return;
            Job.GetToolInfoByToolName(jobName, toolName).enable = ckb_SDKCognexToolEnable.Checked;
        }
        private void tsb_saveImage_Click(object sender, EventArgs e)
        {
            SDK_congexTool.save_Image();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Job.GetJobByName(jobName).Run();
        }
        private void tsb_runTool_Click(object sender, EventArgs e)
        {
            SDK_congexTool.Run(jobName,true,true  );
            if (SDK_congexTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(SDK_congexTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(SDK_congexTool.runStatu.ToString(), Color.Green);
        }

    }
}
