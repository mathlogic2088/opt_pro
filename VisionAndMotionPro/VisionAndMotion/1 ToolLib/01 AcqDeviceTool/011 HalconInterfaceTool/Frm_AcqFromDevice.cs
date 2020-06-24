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
    internal partial class Frm_AcqFromDevice : Frm_ToolBase
    {
        internal Frm_AcqFromDevice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_AcqFromDevice _instance;
        internal static Frm_AcqFromDevice Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_AcqFromDevice();
                return _instance;
            }
        }
        /// <summary>
        /// 工具对象
        /// </summary>
        internal static HalconInterfaceTool halconInterfaceTool = new HalconInterfaceTool();


        private void tkb_exposure_Scroll(object sender, EventArgs e)
        {
            tbx_exposure.Text = tkb_exposure.Value.ToString();
        }
        private void tbx_exposure_TextChanged(object sender, EventArgs e)
        {
            Application.DoEvents();         //此处先刷新显示，否则如果采图时间较长，曝光时间显示会延迟
            if (RegexJudge.IsInt(tbx_exposure.Text.Trim()))
                halconInterfaceTool.Set_Exposure();
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
            halconInterfaceTool.save_Image();
        }
        private void btn_RealTime_Click(object sender, EventArgs e)
        {
           halconInterfaceTool . Real_Time_Acquistion();
        }
        private void cbx_deviceList_TextChanged(object sender, EventArgs e)
        {
            halconInterfaceTool.Device_Changed(cbx_deviceList.Text);
        }
        private void ckb_RGBToGray_CheckedChanged(object sender, EventArgs e)
        {
            halconInterfaceTool.RGBToGray = ckb_RGBToGray.Checked;
        }
        private void btn_registImage_Click(object sender, EventArgs e)
        {
            halconInterfaceTool.Regist_Image();
        }
        private void btn_runHalconInterfaceTool_Click(object sender, EventArgs e)
        {
            halconInterfaceTool.Run(jobName ,true,true );
            if (halconInterfaceTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(halconInterfaceTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(halconInterfaceTool.runStatu.ToString(), Color.Green);
        }
     
    }
}
