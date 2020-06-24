using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    internal partial class Frm_BlobAnalyseTool : Frm_ToolBase
    {
        public Frm_BlobAnalyseTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_BlobAnalyseTool _instance;
        public static Frm_BlobAnalyseTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_BlobAnalyseTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static BlobAnalyseTool blobAnalyseTool = new BlobAnalyseTool();


        private void tsb_resetTool_Click(object sender, EventArgs e)
        {
            blobAnalyseTool.ResetTool();
        }
        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_ToolHelp.Instance.ShowToolHelp("Halcon采集接口",
                                                "此工具以Halcon的采集接口为基础获取图像，支持从设备采集图像和从本地读取图像两种工作模式，可自如切换。",
                                                "1. 将工具添加到流程；\r\n2. 打开工具，选择图像获取模式(从设备采集或从本地读取)；\r\n3. 从设备列表选定图像采集设备(从设备采集模式)或指定图像路径(从本地读取模式)；",
                                                "无"
                                                );
        }
        private void ckb_blobAnalyseToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            Job.GetToolInfoByToolName(jobName, toolName).enable = ckb_blobAnalyseToolEnable.Checked;
        }
        public void cbo_blobAnalyseSearchRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Frm_Main.ignore)
                return;
            blobAnalyseTool.Draw_Search_Region(jobName);
        }
        private void btn_SearchRegionDelete_Click(object sender, EventArgs e)
        {
            blobAnalyseTool.Clear_Search_Region(jobName);
        }
        private void ckb_fillHole_CheckedChanged(object sender, EventArgs e)
        {
            blobAnalyseTool.fillHole = ckb_fillHole.Checked;
        }
        private void ckb_displaySearchRegion_CheckedChanged(object sender, EventArgs e)
        {
            blobAnalyseTool.displaySearchRegion = ckb_displaySearchRegion.Checked;
        }
        private void ckb_displayCross_CheckedChanged(object sender, EventArgs e)
        {
            blobAnalyseTool.displayCross = ckb_displayCross.Checked;
        }
        private void tbx_lineWidth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                blobAnalyseTool.marginLineWidth = Convert.ToInt16(tbx_lineWidth.Text.Trim());
            }
            catch
            {
                Frm_Main.Instance.OutputMsg("输入了非法字符，已自动替换为默认值：1",Color.Red );
                tbx_lineWidth.Text = "1";
            }
        }
        private void ckb_showResultRegion_CheckedChanged(object sender, EventArgs e)
        {
            blobAnalyseTool.showResultRegion = ckb_showResultRegion.Checked;
        }
        private void ckb_showOutCircle_CheckedChanged(object sender, EventArgs e)
        {
            blobAnalyseTool.showOutCircle = ckb_showOutCircle.Checked;
        }
        private void rdo_resultRegionFillMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_resultRegionFillMode.Checked)
                blobAnalyseTool.resultRegionDrawMode = FillMode.Fill;
            else
                blobAnalyseTool.resultRegionDrawMode = FillMode.Margin;
        }
        private void rdo_outCircleFillMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_outCircleFillMode.Checked)
                blobAnalyseTool.outCircleDrawMode = FillMode.Fill;
            else
                blobAnalyseTool.outCircleDrawMode = FillMode.Margin;
        }
        private void dgv_blobAnalyseResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            blobAnalyseTool.Click_Result_List(dgv_blobAnalyseResult, e);
        }
        private void lbx_preProcessingItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgv_processingItem.SelectedRows == null)
                    return;
                if (dgv_processingItem.SelectedRows[0].Cells[0].Value.ToString() == "开运算" || dgv_processingItem.SelectedRows[0].Cells[0].Value.ToString() == "闭运算")
                {
                    Frm_ProcessingItemConfig1.Instance.cbx_elementType.Text = blobAnalyseTool.L_prePorcessing[dgv_processingItem.SelectedRows[0].Index].ElementType;
                    Frm_ProcessingItemConfig1.Instance.tbx_elementSize.Text = blobAnalyseTool.L_prePorcessing[dgv_processingItem.SelectedRows[0].Index].ElementSize.ToString();
                    Frm_ProcessingItemConfig1.Instance.ShowDialog();
                }
                else if (dgv_processingItem.SelectedRows[0].Cells[0].Value.ToString() == "腐蚀" || dgv_processingItem.SelectedRows[0].Cells[0].Value.ToString() == "膨胀")
                {
                    Frm_ProcessingItemConfig.Instance.tbx_minArea.Text = blobAnalyseTool.L_prePorcessing[dgv_processingItem.SelectedRows[0].Index].MinArea.ToString();
                    Frm_ProcessingItemConfig.Instance.tbx_maxArea.Text = blobAnalyseTool.L_prePorcessing[dgv_processingItem.SelectedRows[0].Index].MaxArea.ToString();
                    Frm_ProcessingItemConfig.Instance.tbx_elementSize.Text = blobAnalyseTool.L_prePorcessing[dgv_processingItem.SelectedRows[0].Index].ElementSize.ToString();
                    Frm_ProcessingItemConfig.Instance.ShowDialog();
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void dgv_preProcessingItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 && e.RowIndex != -1)
                {
                    Boolean flag = Convert.ToBoolean(((DataGridViewCheckBoxCell)this.dgv_processingItem.Rows[e.RowIndex].Cells[1]).Value);
                    if (flag == false)
                    {
                        ((DataGridViewCheckBoxCell)this.dgv_processingItem.Rows[e.RowIndex].Cells[1]).Value = true;
                        blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].Enable = true;
                    }
                    else
                    {
                        ((DataGridViewCheckBoxCell)this.dgv_processingItem.Rows[e.RowIndex].Cells[1]).Value = false;
                        blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].Enable = false;
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void nud_minThreshold_ValueChanged(object sender, EventArgs e)
        {
            blobAnalyseTool.minThreshold = Convert.ToDouble(nud_minThreshold.Value);
        }
        private void nud_maxThreshold_ValueChanged(object sender, EventArgs e)
        {
            blobAnalyseTool.maxThreshold = Convert.ToDouble(nud_maxThreshold.Value);
        }
        private void cbx_blobAnalyseSearchRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Frm_Main.ignore)
                return;
            blobAnalyseTool.Draw_Search_Region(jobName);
        }
        private void dgv_select_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Frm_Main.ignore)
                return;
            blobAnalyseTool.SaveSelectItem();
        }
        private void tvw_preProcessingItem_DoubleClick(object sender, EventArgs e)
        {
            blobAnalyseTool.AddProcessingItem();
        }
        private void tsm_deletePreprocessingItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_processingItem.SelectedRows[0].Index != -1)
                {
                    blobAnalyseTool.L_prePorcessing.RemoveAt(dgv_processingItem.SelectedRows[0].Index);
                    dgv_processingItem.Rows.RemoveAt(dgv_processingItem.SelectedRows[0].Index);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_runBlobAnalyseTool_Click(object sender, EventArgs e)
        {
            btn_runBlobAnalyseTool.Enabled = false;
            blobAnalyseTool.Run( jobName,true,true);
            if (blobAnalyseTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(blobAnalyseTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(blobAnalyseTool.runStatu.ToString(), Color.Green);
            btn_runBlobAnalyseTool.Enabled = true;
        }
        private void tsb_runOnce_Click(object sender, EventArgs e)
        {
            btn_runBlobAnalyseTool.Enabled = false;
            blobAnalyseTool.Run(jobName,true ,true );
            if (blobAnalyseTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
                Frm_Main.Instance.OutputMsg(blobAnalyseTool.runStatu.ToString(), Color.Red);
            else
                Frm_Main.Instance.OutputMsg(blobAnalyseTool.runStatu.ToString(), Color.Green);
            btn_runBlobAnalyseTool.Enabled = true;
        }

        private void btn_saveResultRegion_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Windows.Forms.SaveFileDialog dig_saveImage = new System.Windows.Forms.SaveFileDialog();
                dig_saveImage.FileName = DateTime.Now.ToString("yyyy_MM_dd");
                dig_saveImage.Title = Configuration.language == Language.English ? "Please select the region saving path" : "请选择区域文件保存路径";
                dig_saveImage.Filter = "Region File|*.hobj";
                dig_saveImage.InitialDirectory = path;
                if (dig_saveImage.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dig_saveImage.FileName;
                    HOperatorSet.WriteRegion(blobAnalyseTool.outputResultRegion, new HTuple(dig_saveImage.FileName));
                    Frm_Main.Instance.OutputMsg("Region saved successfully", Color.Green);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
