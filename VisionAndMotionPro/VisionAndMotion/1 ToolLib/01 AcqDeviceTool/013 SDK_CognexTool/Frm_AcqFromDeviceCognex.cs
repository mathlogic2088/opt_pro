﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    public partial class Frm_AcqFromDeviceCognex : Frm_ToolBase
    {
        public Frm_AcqFromDeviceCognex()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_AcqFromDeviceCognex _instance;
        public static Frm_AcqFromDeviceCognex Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_AcqFromDeviceCognex();
                return _instance;
            }
        }
        /// <summary>
        /// 工具对象
        /// </summary>
        internal static SDK_CongexTool SDK_congexTool = new SDK_CongexTool();


        private void tkb_exposure_Scroll(object sender, EventArgs e)
        {
            tbx_exposure.Text = tkb_exposure.Value.ToString();
        }
        private void tbx_exposure_TextChanged(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (RegexJudge.IsInt(tbx_exposure.Text.Trim()))
                SDK_congexTool.Set_Exposure(jobName);
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
            SDK_congexTool.save_Image();
        }
        private void btn_RealTime_Click(object sender, EventArgs e)
        {
            SDK_congexTool.Real_Time_Acquistion();
        }
        private void cbx_deviceList_TextChanged(object sender, EventArgs e)
        {
            SDK_congexTool.Device_Changed(cbx_deviceList.Text);
        }
        private void ckb_RGBToGray_CheckedChanged(object sender, EventArgs e)
        {
            SDK_congexTool.RGBToGray = ckb_RGBToGray.Checked;
        }
        private void btn_registImage_Click(object sender, EventArgs e)
        {
            SDK_congexTool.Regist_Images();
        }
        private void btn_runImageAcquistionTool_Click(object sender, EventArgs e)
        {
            SDK_congexTool.Run(jobName,true,true  );
            if (SDK_congexTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(SDK_congexTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(SDK_congexTool.runStatu.ToString(), Color.Green);
        }

    }
}
