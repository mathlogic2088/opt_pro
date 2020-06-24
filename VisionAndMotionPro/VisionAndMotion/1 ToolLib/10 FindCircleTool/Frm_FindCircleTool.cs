using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VersionMethods;

namespace VisionAndMotionPro
{
    internal partial class Frm_FindCircleTool : Frm_ToolBase 
    {
        public Frm_FindCircleTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_FindCircleTool _instance;
        internal static Frm_FindCircleTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_FindCircleTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static FindCircleTool findCircleTool = new FindCircleTool();


        private void tsb_resetTool_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void btn_moveCliperRegion_Click(object sender, EventArgs e)
        {
            findCircleTool.DrawExpectCircle(jobName);
        }
        private void btn_subExpectCircleRow_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_expectCircelRowSpan.Text.Trim()); 
            findCircleTool.expectCircleRow  -= value;
            tbx_expectCircelRow.Text = findCircleTool.expectCircleRow.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_addExpectCircleRow_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_expectCircelRowSpan.Text.Trim());
            findCircleTool.expectCircleRow += value;
            tbx_expectCircelRow.Text = findCircleTool.expectCircleRow.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_subExpectCircleCol_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_expectCircleColSpan.Text.Trim());
            findCircleTool.expectCircleCol -= value;
            tbx_expectCircleCol.Text = findCircleTool.expectCircleCol.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_addExpectCircleCol_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_expectCircleColSpan.Text.Trim());
            findCircleTool.expectCircleCol += value;
            tbx_expectCircleCol.Text = findCircleTool.expectCircleCol.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_subExpectCircleRadius_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_expectCircleRadiusSapn.Text.Trim());
            findCircleTool.expectCircleRadius  -= value;
            tbx_expectCircleradius.Text = findCircleTool.expectCircleRadius.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_addExpectCirlceRadius_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_expectCircleRadiusSapn.Text.Trim());
            findCircleTool.expectCircleRadius += value;
            tbx_expectCircleradius.Text = findCircleTool.expectCircleRadius.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName,true ,true );
        }
        private void btn_subExpectRingRadiusLength_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_ringRadiusLengthSpan.Text.Trim()); 
            findCircleTool.ringRadiusLength  -= value;
            tbx_ringRadiusLength.Text = findCircleTool.ringRadiusLength.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_addExpectCircleRingRadiusLength_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_ringRadiusLengthSpan.Text.Trim());
            findCircleTool.ringRadiusLength += value;
            tbx_ringRadiusLength.Text = findCircleTool.ringRadiusLength.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_subExpectCircleStartAngle_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_startAngleSpan.Text.Trim());
            findCircleTool.startAngle  -= value;
            tbx_startAngle.Text = findCircleTool.startAngle.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_addExpectCircleStartAngle_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_startAngleSpan.Text.Trim());
            findCircleTool.startAngle += value;
            tbx_startAngle.Text = findCircleTool.startAngle.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_subExpectCircleEndAngle_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_endAngleSpan.Text.Trim());
            findCircleTool.endAngle  -= value;
            tbx_endAngle.Text = findCircleTool.endAngle.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_addExpectCircleEndAngle_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_endAngleSpan.Text.Trim());
            findCircleTool.endAngle += value;
            tbx_endAngle.Text = findCircleTool.endAngle.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_subCliperNum_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_cliperNumSpan.Text.Trim());
            findCircleTool.cliperNum  -= value;
            tbx_cliperNum.Text = findCircleTool.cliperNum.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_addCliperNum_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_cliperNumSpan.Text.Trim());
            findCircleTool.cliperNum += value;
            tbx_cliperNum.Text = findCircleTool.cliperNum.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_switchPolarity_Click(object sender, EventArgs e)
        {
            if (cbx_polarity.SelectedIndex == 0)
                cbx_polarity.SelectedIndex = 1;
            else cbx_polarity.SelectedIndex = 0;
        }
        private void cbx_polarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            findCircleTool.polarity = cbx_polarity.SelectedIndex == 0 ? "positive" : "negative";
        }
        private void btn_subThreshold_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_thresholdSpan.Text.Trim());
            findCircleTool.threshold  -= value;
            tbx_threshold.Text = findCircleTool.threshold.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void btn_addThreshold_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_thresholdSpan.Text.Trim());
            findCircleTool.threshold += value;
            tbx_threshold.Text = findCircleTool.threshold.ToString();
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
        }
        private void ckb_findCircleToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            (Job.GetToolInfoByToolName(jobName, toolName)).enable = ckb_findCircleToolEnable.Checked;
        }
        private void tbx_expectCircelRow_TextChanged(object sender, EventArgs e)
        {
            try
            { 
            findCircleTool.expectCircleRow = Convert.ToDouble(tbx_expectCircelRow .Text .Trim ());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：300", Color.Red);
                tbx_expectCircelRow.Text = "300";
            }
        }
        private void tbx_expectCircleCol_TextChanged(object sender, EventArgs e)
        {
            try
            {
                findCircleTool.expectCircleCol = Convert.ToDouble(tbx_expectCircleCol.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：300", Color.Red);
                tbx_expectCircleCol.Text = "300";
            }
        }
        private void tbx_expectCircleradius_TextChanged(object sender, EventArgs e)
        {
            try
            {
                findCircleTool.expectCircleRadius = Convert.ToDouble(tbx_expectCircleradius.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：200", Color.Red);
                tbx_expectCircleradius.Text = "200";
            }
        }
        private void tbx_startAngle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                findCircleTool.startAngle = Convert.ToDouble(tbx_startAngle.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_startAngle.Text = "10";
            }
        }
        private void tbx_endAngle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                findCircleTool.endAngle = Convert.ToDouble(tbx_endAngle.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：360", Color.Red);
                tbx_endAngle.Text = "360";
            }
        }
        private void tbx_threshold_TextChanged(object sender, EventArgs e)
        {
            try
            {
                findCircleTool.threshold = Convert.ToInt16 (tbx_threshold.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：30", Color.Red);
                tbx_threshold.Text = "30";
            }
        }
        private void tbx_ringRadiusLength_TextChanged(object sender, EventArgs e)
        {
            try
            {
                findCircleTool.ringRadiusLength = Convert.ToInt16(tbx_ringRadiusLength.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：80", Color.Red);
                tbx_ringRadiusLength.Text = "80";
            }
        }
        private void tbx_cliperNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                findCircleTool.cliperNum = Convert.ToInt16(tbx_cliperNum.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：20", Color.Red);
                tbx_cliperNum.Text = "20";
            }
        }
        private void tbx_expectCircelRowSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_expectCircelRowSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_expectCircelRowSpan.Text = "10";
            }
        }
        private void tbx_expectCircleColSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_expectCircleColSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_expectCircleColSpan.Text = "10";
            }
        }
        private void tbx_expectCircleRadiusSapn_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_expectCircleRadiusSapn.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_expectCircleRadiusSapn.Text = "10";
            }
        }
        private void tbx_startAngleSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_startAngleSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_startAngleSpan.Text = "10";
            }
        }
        private void tbx_endAngleSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_endAngleSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_endAngleSpan.Text = "10";
            }
        }
        private void tbx_thresholdSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_thresholdSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_thresholdSpan.Text = "10";
            }
        }
        private void tbx_ringRadiusLengthSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_ringRadiusLengthSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_ringRadiusLengthSpan.Text = "10";
            }
        }
        private void tbx_cliperNumSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_cliperNumSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_cliperNumSpan.Text = "10";
            }
        }
        internal void btn_runFindCircleTool_Click(object sender, EventArgs e)
        {
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
            if (findCircleTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(findCircleTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(findCircleTool.runStatu.ToString(), Color.Green);
        }
        private void tsb_runOnce_Click(object sender, EventArgs e)
        {
            findCircleTool.UpdateImage(jobName);
            findCircleTool.Run(jobName, true, true);
            if (findCircleTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(findCircleTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(findCircleTool.runStatu.ToString(), Color.Green);
        }

    }
}
