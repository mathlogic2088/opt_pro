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
    internal partial class Frm_FindLineTool : Frm_ToolBase
    {
        internal Frm_FindLineTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_FindLineTool _instance;
        internal static Frm_FindLineTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_FindLineTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static FindLineTool findLineTool = new FindLineTool();


        private void btn_addStartRow_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_startRowSpan.Text.Trim());
            findLineTool.expectLineStartRow += value;
            tbx_expectLineStartRow.Text = findLineTool.expectLineStartRow.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void btn_subStartCol_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_starColSpan.Text.Trim());
            findLineTool.expectLineStartCol -= value;
            tbx_expectLineStartCol.Text = findLineTool.expectLineStartCol.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void btn_addStartCol_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_starColSpan.Text.Trim());
            findLineTool.expectLineStartCol += value;
            tbx_expectLineStartCol.Text = findLineTool.expectLineStartCol.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void btn_subEndRow_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_endRowSpan.Text.Trim());
            findLineTool.expectLineEndRow -= value;
            tbx_expectLineEndRow.Text = findLineTool.expectLineEndRow.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void btn_addEndRow_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_endRowSpan.Text.Trim());
            findLineTool.expectLineEndRow += value;
            tbx_expectLineEndRow.Text = findLineTool.expectLineEndRow.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void btn_subEndCol_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_endColSpan.Text.Trim());
            findLineTool.expectLineEndCol -= value;
            tbx_expectLineEndCol.Text = findLineTool.expectLineEndCol.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void btn_addEndCol_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_endColSpan.Text.Trim());
            findLineTool.expectLineEndCol += value;
            tbx_expectLineEndCol.Text = findLineTool.expectLineEndCol.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void btn_subMinScore_Click(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(tbx_minScoreSpan.Text.Trim());
            findLineTool.minScore -= value;
            tbx_minScore.Text = findLineTool.minScore.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void btn_subSliperNum_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_cliperNumSpan.Text.Trim());
            findLineTool.cliperNum -= value;
            tbx_caliperNum.Text = findLineTool.cliperNum.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void tbx_addSliperNum_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_cliperNumSpan.Text.Trim());
            findLineTool.cliperNum += value;
            tbx_caliperNum.Text = findLineTool.cliperNum.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void btn_subThreshold_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_thresholdSpan.Text.Trim());
            findLineTool.threshold -= value;
            tbx_threshold.Text = findLineTool.threshold.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true,true );
        }
        private void btn_addThreshold_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_thresholdSpan.Text.Trim());
            findLineTool.threshold += value;
            tbx_threshold.Text = findLineTool.threshold.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
        }
        private void cbx_polarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            findLineTool.polarity = cbx_polarity.SelectedIndex == 0 ? "positive" : "negative";
        }
        private void btn_switchPolarity_Click(object sender, EventArgs e)
        {
            if (cbx_polarity.SelectedIndex == 0)
                cbx_polarity.SelectedIndex = 1;
            else cbx_polarity.SelectedIndex = 0;
        }
        private void tsb_runOnce_Click(object sender, EventArgs e)
        {
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName,true ,true );
            if (findLineTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(findLineTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(findLineTool.runStatu.ToString(), Color.Green);
        }
        private void tsb_resetTool_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Not yet developed, please wait!" : "\r\n尚未开发，敬请期待！");
        }
        private void ckb_findLineToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            (Job.GetToolInfoByToolName(jobName, toolName)).enable = ckb_findLineToolEnable.Checked;
        }
        private void tbx_expectLineStartRow_TextChanged(object sender, EventArgs e)
        {
            try
            {
                findLineTool.expectLineStartRow = Convert.ToDouble(tbx_expectLineStartRow.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：200", Color.Red);
                tbx_expectLineStartRow.Text = "200";
            }
        }
        private void tbx_expectLineStartCol_TextChanged(object sender, EventArgs e)
        {
            try
            { 
            findLineTool.expectLineStartCol = Convert.ToDouble(tbx_expectLineStartCol.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：200", Color.Red);
                tbx_expectLineStartCol.Text = "200";
            }
        }
        private void tbx_expectLineEndRow_TextChanged(object sender, EventArgs e)
        {
            try
            { 
            findLineTool.expectLineEndRow = Convert.ToDouble(tbx_expectLineEndRow.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：200", Color.Red);
                tbx_expectLineEndRow.Text = "200";
            }
        }
        private void tbx_expectLineEndCol_TextChanged(object sender, EventArgs e)
        {
            try
            { 
            findLineTool.expectLineEndCol = Convert.ToDouble(tbx_expectLineEndCol.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：600", Color.Red);
                tbx_expectLineEndCol.Text = "600";
            }
        }
        private void btn_moveCliperRegion_Click(object sender, EventArgs e)
        {
            findLineTool.DrawExpectLine(jobName);
        }
        private void tbx_threshold_TextChanged(object sender, EventArgs e)
        {
            try
            { 
            findLineTool.threshold = Convert.ToInt16(tbx_threshold.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：30", Color.Red);
                tbx_threshold.Text = "30";
            }
        }
        private void tbx_caliperLength_TextChanged(object sender, EventArgs e)
        {
            try
            { 
            findLineTool.length = Convert.ToInt16(tbx_caliperLength.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：80", Color.Red);
                tbx_caliperLength.Text = "80";
            }
        }
        private void tbx_caliperNum_TextChanged(object sender, EventArgs e)
        {
            try
            { 
            findLineTool.cliperNum = Convert.ToInt16(tbx_caliperNum.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：20", Color.Red);
                tbx_caliperNum.Text = "20";
            }
        }
        private void tbx_minScore_TextChanged(object sender, EventArgs e)
        {
            try
            {
                findLineTool.minScore = Convert.ToDouble(tbx_minScore.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0.5", Color.Red);
                tbx_minScore.Text = "0.5";
            }
        }
        private void ckb_updateImage_CheckedChanged(object sender, EventArgs e)
        {
            findLineTool.updateImage = ckb_updateImage.Checked;
        }
        private void btn_addMinScore_Click(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(tbx_minScoreSpan.Text.Trim());
            findLineTool.minScore += value;
            tbx_minScore.Text = findLineTool.minScore.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName, true, true);
        }
        private void btn_addSliperLength_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_cliperLengthSpan.Text.Trim());
            findLineTool.length += value;
            tbx_caliperLength.Text = findLineTool.length.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName, true, true);
        }
        private void btn_subSliperLenght_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_cliperLengthSpan.Text.Trim());
            findLineTool.length -= value;
            tbx_minScore.Text = findLineTool.minScore.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName, true, true);
        }
        private void tbx_startRowSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_startRowSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_startRowSpan.Text = "10";
            }
        }
        private void tbx_starColSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_starColSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_starColSpan.Text = "10";
            }
        }
        private void tbx_endRowSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_endRowSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_endRowSpan.Text = "10";
            }
        }
        private void tbx_endColSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_endColSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_endColSpan.Text = "10";
            }
        }
        private void tbx_minScoreSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_minScoreSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：0.1", Color.Red);
                tbx_minScoreSpan.Text = "0.1";
            }
        }
        private void tbx_cliperNumSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_cliperNumSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：5", Color.Red);
                tbx_cliperNumSpan.Text = "5";
            }
        }
        private void tbx_cliperLengthSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_cliperLengthSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_cliperLengthSpan.Text = "10";
            }
        }
        private void tbx_thresholdSpan_TextChanged(object sender, EventArgs e)
        {
            if (!RegexJudge.IsInt(tbx_thresholdSpan.Text.Trim()))
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：10", Color.Red);
                tbx_expectLineStartRow.Text = "10";
            }
        }
        private void btn_runFindLineTool_Click(object sender, EventArgs e)
        {
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName, true, true);
            if (findLineTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(findLineTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(findLineTool.runStatu.ToString(), Color.Green);
        }
        private void btn_subStartRow_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt16(tbx_startRowSpan.Text.Trim());
            findLineTool.expectLineStartRow -= value;
            tbx_expectLineStartRow.Text = findLineTool.expectLineStartRow.ToString();
            findLineTool.UpdateImage(jobName);
            findLineTool.Run(jobName, true, true);
        }

    }
}
