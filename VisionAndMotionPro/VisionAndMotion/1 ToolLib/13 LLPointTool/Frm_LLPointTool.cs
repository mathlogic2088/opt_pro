﻿using HalconDotNet;
using Tool;
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
    internal partial class Frm_LLPointTool : Frm_ToolBase
    {
        internal Frm_LLPointTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_LLPointTool _instance;
        public static Frm_LLPointTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_LLPointTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static LLPointTool llPointTool = new LLPointTool();


        internal void btn_drawShapeMatchSearchRegion_Click(object sender, EventArgs e)
        {
            //////shapeMatchTool.Draw_Search_Region();
        }
        private void btn_deleteShapeMatchSearchRegion_Click(object sender, EventArgs e)
        {
            //////shapeMatchTool.Clear_Search_Region();
        }
        private void dgv_matchResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //////shapeMatchTool.Click_Result_Dgv(e);
        }
        private void tkb_contrast_Scroll(object sender, EventArgs e)
        {
            //////shapeMatchTool.Contrast_Changed();
        }
   
        private void cbo_shapeMatchSearchRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //////shapeMatchTool.Draw_Search_Region();
        }
        private void btn_displayStandardImage_Click(object sender, EventArgs e)
        {
            //////HOperatorSet.DispObj(shapeMatchTool.standardImage, Frm_ImageWindow.Instance.WindowHandle);
        }
        private void btn_displayTemplateContour_Click(object sender, EventArgs e)
        {
            //////shapeMatchTool.ShowTemplate();
        }
        private void ckb_shapeMatchToolNotRun_CheckedChanged(object sender, EventArgs e)
        {
            Job.GetToolInfoByToolName(jobName, toolName).enable = ckb_distancePLToolEnable.Checked;
        }
        private void btn_drawTemplateRegionRectangle1_Click(object sender, EventArgs e)
        {
            //////shapeMatchTool.Draw_Template_Rectangle1();
        }
        private void btn_drawTemplateRegionRectangle2_Click(object sender, EventArgs e)
        {
            //////////shapeMatchTool.Draw_Template_Rectangle2();
        }
        private void btn_drawTemplateRegionCircle_Click(object sender, EventArgs e)
        {
            //////shapeMatchTool.Draw_Template_Circle();
        }
        private void btn_drawTemplateRegionEllipse_Click(object sender, EventArgs e)
        {
            //////shapeMatchTool.Draw_Template_Ellipse();
        }
        private void btn_drawTemplateRegionAny_Click(object sender, EventArgs e)
        {
            //////shapeMatchTool.Draw_Template_Any();
        }
        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_ToolHelp.Instance.ShowToolHelp("形状匹配",
                                                "此工具可创建模板，并在输入图像中搜索模板位置。",
                                                "1. 将工具添加到流程；\r\n2. 指定图像输入；\r\n3. 创建匹配模板；",
                                                "1. 创建模板前必须有正常的图像输入；\r\n2. 输入输出配置完成后需运行一次流程，这样才能使前面工具的输出项传递到本工具的输入项"
                                                );
        }
        private void tsb_resetTool_Click(object sender, EventArgs e)
        {
            //////shapeMatchTool.ResetTool();
        }
      
        private void btn_runShapeMatchTool_Click(object sender, EventArgs e)
        {
            //////btn_runDistancePLTool.Enabled = false;
            //////shapeMatchTool.Run(true, jobName);
            //////if (shapeMatchTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
            //////    Frm_Main.Instance.OutputMsg(shapeMatchTool.runStatu.ToString(), Color.Red);
            //////else
            //////    Frm_Main.Instance.OutputMsg(shapeMatchTool.runStatu.ToString(), Color.Green);
            //////btn_runDistancePLTool.Enabled = true;
        }
        private void tsb_runOnce_Click(object sender, EventArgs e)
        {
            //////btn_runDistancePLTool.Enabled = false;
            //////shapeMatchTool.Run(true, jobName);
            //////if (shapeMatchTool.runStatu != (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功))
            //////    Frm_Main.Instance.OutputMsg(shapeMatchTool.runStatu.ToString(), Color.Red);
            //////else
            //////    Frm_Main.Instance.OutputMsg(shapeMatchTool.runStatu.ToString(), Color.Green);
            //////btn_runDistancePLTool.Enabled = true;
        }

    }
}
