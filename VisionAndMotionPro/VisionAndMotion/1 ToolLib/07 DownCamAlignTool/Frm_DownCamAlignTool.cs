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
    internal partial class Frm_DownCamAlignTool : Frm_ToolBase
    {
        internal Frm_DownCamAlignTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_DownCamAlignTool _instance;
        internal static Frm_DownCamAlignTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_DownCamAlignTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static RobotDownCamAlignTool robotDownCamAlignTool = new RobotDownCamAlignTool();


        private void tsb_resetTool_Click(object sender, EventArgs e)
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
        private void cbx_downCamAlignToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            Job.GetToolInfoByToolName(jobName, toolName).enable =cbx_downCamAlignToolEnable.Checked;
        }
        private void tbx_caputurePosX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                robotDownCamAlignTool.caputurePos.X = Convert.ToDouble(tbx_caputurePosX.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0", Color.Red);
                tbx_caputurePosX.Text = "0";
            }
        }
        private void tbx_caputurePosY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                robotDownCamAlignTool.caputurePos.Y = Convert.ToDouble(tbx_caputurePosY.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0", Color.Red);
                tbx_caputurePosY.Text = "0";
            }
        }
        private void tbx_caputurePosU_TextChanged(object sender, EventArgs e)
        {
            try
            {
                robotDownCamAlignTool.caputurePos.U = Convert.ToDouble(tbx_caputurePosU.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0", Color.Red);
                tbx_caputurePosU.Text = "0";
            }
        }
        private void tbx_templateFeatureX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                robotDownCamAlignTool.templateFeaturePos.X = Convert.ToDouble(tbx_templateFeatureX.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0", Color.Red);
                tbx_templateFeatureX.Text = "0";
            }
        }
        private void tbx_templateFeatureY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                robotDownCamAlignTool.templateFeaturePos.Y = Convert.ToDouble(tbx_templateFeatureY.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0", Color.Red);
                tbx_templateFeatureY.Text = "0";
            }
        }
        private void tbx_templateFeatureU_TextChanged(object sender, EventArgs e)
        {
            try
            {
                robotDownCamAlignTool.templateFeaturePos.U = Convert.ToDouble(tbx_templateFeatureU.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0", Color.Red);
                tbx_templateFeatureU.Text = "0";
            }
        }
        private void tbx_touchPosX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                robotDownCamAlignTool.touchPos.X = Convert.ToDouble(tbx_touchPosX.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0", Color.Red);
                tbx_touchPosX.Text = "0";
            }
        }
        private void tbx_touchPosY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                robotDownCamAlignTool.touchPos.Y = Convert.ToDouble(tbx_touchPosY.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0", Color.Red);
                tbx_touchPosY.Text = "0";
            }
        }
        private void tbx_touchPosU_TextChanged(object sender, EventArgs e)
        {
            try
            {
                robotDownCamAlignTool.touchPos.U = Convert.ToDouble(tbx_touchPosU.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0", Color.Red);
                tbx_touchPosY.Text = "0";
            }
        }
        private void tsb_runOnce_Click(object sender, EventArgs e)
        {
            tsb_runTool.Enabled = false;
            robotDownCamAlignTool.Run(null ,true ,true );
            if (robotDownCamAlignTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(robotDownCamAlignTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(robotDownCamAlignTool.runStatu.ToString(), Color.Green);
            tsb_runTool.Enabled = true;
        }
        private void btn_runDownCamAlignTool_Click(object sender, EventArgs e)
        {
            btn_runDownCamAlignTool.Enabled = false;
            robotDownCamAlignTool.Run(string .Empty ,true ,true );
            if (robotDownCamAlignTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(robotDownCamAlignTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(robotDownCamAlignTool.runStatu.ToString(), Color.Green);
            btn_runDownCamAlignTool.Enabled = true;
        }

    }
}
