using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using HalconDotNet;
using ViewWindow.Model;
using System.Reflection;
using System.ComponentModel;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class ShapeMatchTool : ToolBase
    {
        internal ShapeMatchTool()
        {
            //HOperatorSet.GenEmptyRegion(out _searchRegion);
        }

        /// <summary>
        /// 查找数量
        /// </summary>
        internal int matchNum;
        /// <summary>
        /// 做模板时的标准图像
        /// </summary>
        internal HObject standardImage;
        /// <summary>
        /// 是否显示中心十字架
        /// </summary>
        internal bool showCross = true;
        /// <summary>
        /// 是否显示特征
        /// </summary>
        internal bool showFeature = true;
        /// <summary>
        /// 模板句柄
        /// </summary>
        internal HTuple modelID = -1;
        /// <summary>
        /// 模板区域
        /// </summary>
        internal HObject templateRegion;
        /// <summary>
        /// 搜索区域
        /// </summary>
        private HObject _searchRegion;

        internal HObject SearchRegion
        {
            get
            {
                if (regions != null && regions.Count > 0)
                    _searchRegion = regions[0].getRegion();
                return _searchRegion;
            }
            set { _searchRegion = value; }
        }
        /// <summary>
        /// 搜索区域图像
        /// </summary>
        internal HObject reducedImage;
        /// <summary>
        /// 搜索区域点数据，如Rectangle1的左上点行列坐标和右下点行列坐标，需要存储下来，在重绘区域时用
        /// </summary>
        internal List<double> searchRegionPoint = new List<double>();
        /// <summary>
        /// 第一个匹配结果的位姿
        /// </summary>
        internal PosXYU pose = new PosXYU();
        /// <summary>
        /// 模板匹配结果
        /// </summary>
        internal List<MatchResult> L_matchResult = new List<MatchResult>();
        /// <summary>
        /// 第一个匹配结果行坐标
        /// </summary>
        internal double resultRow = 0;
        /// <summary>
        /// 第一个匹配结果列坐标
        /// </summary>
        internal double resultCol = 0;
        /// <summary>
        /// 第一个匹配结果角度
        /// </summary>
        internal double resultAngle = 0;
        /// <summary>
        /// 搜索区域的中心点行坐标和列坐标
        /// </summary>
        internal HTuple searchRegionCenterRow = 0, searchRegionCenterColumn = 0;
        /// <summary>
        /// 搜索区域类型
        /// </summary>
        internal RegionType searchRegionType = RegionType.None;
        /// <summary>
        /// 最小匹配分数
        /// </summary>
        internal double minScore = 0.5;
        /// <summary>
        /// 匹配个数
        /// </summary>
        internal int expectMatchNum = 1;
        /// <summary>
        /// 匹配到的结果数量
        /// </summary>
        internal int matchResultNum = 0;
        /// <summary>
        /// 起始角度
        /// </summary>
        internal int startAngle = -180;
        /// <summary>
        /// 角度范围
        /// </summary>
        internal int angleRange = 360;
        /// <summary>
        /// 角度步长
        /// </summary>
        internal double angleStep = 1;
        /// <summary>
        /// 对比度
        /// </summary>
        internal double contrast = 30;
        /// <summary>
        /// 极性
        /// </summary>
        internal string polarity = "use_polarity";
        /// <summary>
        /// 模板是否已创建
        /// </summary>
        internal bool isCreated = false;
        /// <summary>
        /// 输入图像
        /// </summary>
        internal HObject inputImage;
        /// <summary>
        /// 工具运行状态
        /// </summary>
        internal ToolRunStatu runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Run : ToolRunStatu.未运行);
        /// <summary>
        /// 流程名
        /// </summary>
        internal string jobName = string.Empty;
        internal List<ViewWindow.Model.ROI> regions = new List<ROI>();


        /// <summary>
        /// 工具恢复到初始状态
        /// </summary>
        internal void ResetTool()
        {
            try
            {
                templateRegion = null;
                SearchRegion = null;
                templateRegion = null;
                standardImage = null;
                reducedImage = null;
                isCreated = false;
                searchRegionType = RegionType.None;
                ClearWindow(jobName);
                ShowImage(jobName, inputImage);
                if (isCreated)
                    HOperatorSet.ClearShapeModel(modelID);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 清空上次运行的所有输入
        /// </summary>
        internal void ClearLastInput()
        {
            try
            {
                inputImage = null;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 显示模板
        /// </summary>
        internal void ShowTemplate()
        {
            try
            {
                HObject contour;
                HOperatorSet.GetShapeModelContours(out contour, modelID, new HTuple(1));
                HTuple area, row, col;
                HOperatorSet.AreaCenter(templateRegion, out area, out row, out col);
                HTuple homMat2D;
                HOperatorSet.HomMat2dIdentity(out homMat2D);
                HOperatorSet.HomMat2dTranslate(homMat2D, row, col, out homMat2D);
                HOperatorSet.AffineTransContourXld(contour, out contour, homMat2D);
                Frm_ImageWindow.Instance.hwc_imageWindow.DispObj(contour ,"orange");
                Frm_ImageWindow.Instance.hwc_imageWindow.DispObj(templateRegion ,"green");
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 创建并显示模板
        /// </summary>
        private void Show_Template()
        {
            if (Create_Template() == 0)
            {
                HObject contour;
                HOperatorSet.GetShapeModelContours(out contour, modelID, (HTuple)1);
                HTuple area2, row2, column2;
                HOperatorSet.AreaCenter(templateRegion, out area2, out row2, out column2);
                HTuple homMat2D;
                HOperatorSet.HomMat2dIdentity(out homMat2D);
                HOperatorSet.HomMat2dTranslate(homMat2D, row2, column2, out homMat2D);
                HOperatorSet.AffineTransContourXld(contour, out contour, homMat2D);
                SetColor(jobName, "orange");
                ShowObj(jobName, contour);
                End_Draw_Mode(jobName);
                //////Frm_Main.Instance.tsb_dragMode.CheckState = CheckState.Unchecked;
                standardImage = inputImage;
                Contrast_Changed();

                HTuple row5, col5, row6, col6;
                HOperatorSet.SmallestRectangle1(templateRegion, out row5, out col5, out row6, out col6);
                HObject outRectangle1;
                HOperatorSet.GenRectangle1(out outRectangle1, row5 - 20, col5 - 20, row6 + 20, col6 + 20);
                HObject imageReduced;
                HOperatorSet.ReduceDomain(standardImage, outRectangle1, out imageReduced);
                HOperatorSet.SetPart(Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow, row5 - 20, col5 - 20, row6 + 20, col6 + 20);
                HOperatorSet.DispObj(imageReduced, Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow);
                HOperatorSet.SetDraw(Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow, new HTuple("margin"));
                HOperatorSet.SetColor(Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow, new HTuple("green"));
                HOperatorSet.DispObj(templateRegion, Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow);
                HOperatorSet.SetColor(Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow, new HTuple("orange"));
                HOperatorSet.DispObj(contour, Frm_ShapeMatchTool.Instance.hwc_template.HalconWindow);
            }
        }
        /// <summary>
        /// 绘制矩形模板
        /// </summary>
        internal void Draw_Template_Rectangle1()
        {
            try
            {
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = true;
                //////Frm_Main.Instance.tsb_dragMode.CheckState = CheckState.Checked;
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionRectangle1.BackColor = Color.LightGreen;
                Frm_ImageWindow.Instance.hwc_imageWindow.Focus();
                HOperatorSet.SetDraw(GetWindowHandle(jobName), "margin");
                Start_Draw_Mode(jobName);
                HTuple row, column, row1, column1;
                if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    SetColor(jobName, "green");
                    SetLineStyle(jobName, 0);
                }
                else
                {
                    SetColor(jobName, "yellow");
                    SetLineStyle(jobName, 4);
                }
                HOperatorSet.DrawRectangle1(GetWindowHandle(jobName), out row, out column, out row1, out column1);
                HObject rectangle1;
                HOperatorSet.GenRectangle1(out rectangle1, row, column, row1, column1);
                ShowObj(jobName, rectangle1);
                if (templateRegion == null)
                {
                    templateRegion = rectangle1;
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    HOperatorSet.Union2(templateRegion, rectangle1, out templateRegion);
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionSub.Checked)
                {
                    HOperatorSet.Difference(templateRegion, rectangle1, out templateRegion);
                }
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionRectangle1.BackColor = Color.Transparent;
                Show_Template();
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 绘制仿射矩形模板
        /// </summary>
        internal void Draw_Template_Rectangle2()
        {
            try
            {
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = true;
                //////Frm_Main.Instance.tsb_dragMode.CheckState = CheckState.Checked;
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionRectangle2.BackColor = Color.LightGreen;
                Frm_ImageWindow.Instance.hwc_imageWindow.Focus();
                HOperatorSet.SetDraw(GetWindowHandle(jobName), new HTuple("margin"));
                Start_Draw_Mode(jobName);
                HTuple row, column, angle, lenght1, length2;
                if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    SetColor(jobName, "green");
                    SetLineStyle(jobName, 0);
                }
                else
                {
                    SetColor(jobName, "yellow");
                    SetLineStyle(jobName, 4);
                }
                HOperatorSet.DrawRectangle2(GetWindowHandle(jobName), out row, out column, out angle, out lenght1, out length2);
                HObject rectangle2;
                HOperatorSet.GenRectangle2(out rectangle2, row, column, angle, lenght1, length2);
                ShowObj(jobName, rectangle2);
                if (templateRegion == null)
                {
                    templateRegion = rectangle2;
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    HOperatorSet.Union2(templateRegion, rectangle2, out    templateRegion);
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionSub.Checked)
                {
                    HOperatorSet.Difference(templateRegion, rectangle2, out    templateRegion);
                }
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionRectangle2.BackColor = Color.Transparent;
                Show_Template();
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 绘制圆形模板
        /// </summary>
        internal void Draw_Template_Circle()
        {
            try
            {
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = true;
                //////Frm_Main.Instance.tsb_dragMode.CheckState = CheckState.Checked;
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionCircle.BackColor = Color.LightGreen;
                Frm_ImageWindow.Instance.hwc_imageWindow.Focus();
                HOperatorSet.SetDraw(GetWindowHandle(jobName), new HTuple("margin"));
                Start_Draw_Mode(jobName);
                HTuple row, column, radius;
                if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    SetColor(jobName, "green");
                    SetLineStyle(jobName, 0);
                }
                else
                {
                    SetColor(jobName, "yellow");
                    SetLineStyle(jobName, 4);
                }
                HOperatorSet.DrawCircle(GetWindowHandle(jobName), out row, out column, out radius);
                HObject circle;
                HOperatorSet.GenCircle(out circle, row, column, radius);
                ShowObj(jobName, circle);
                if (templateRegion == null)
                {
                    templateRegion = circle;
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    HOperatorSet.Union2(templateRegion, circle, out   templateRegion);
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionSub.Checked)
                {
                    HOperatorSet.Difference(templateRegion, circle, out   templateRegion);
                }
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionCircle.BackColor = Color.Transparent;
                Show_Template();
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 绘制椭圆形模板
        /// </summary>
        internal void Draw_Template_Ellipse()
        {
            try
            {
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = true;
                //////Frm_Main.Instance.tsb_dragMode.CheckState = CheckState.Checked;
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionEllipse.BackColor = Color.LightGreen;
                Frm_ImageWindow.Instance.hwc_imageWindow.Focus();
                HOperatorSet.SetDraw(GetWindowHandle(jobName), new HTuple("margin"));
                Start_Draw_Mode(jobName);
                HTuple row, column, angle, length1, length2;
                if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    SetColor(jobName, "green");
                    SetLineStyle(jobName, 0);
                }
                else
                {
                    SetColor(jobName, "yellow");
                    SetLineStyle(jobName, 4);
                }
                HOperatorSet.DrawEllipse(GetWindowHandle(jobName), out row, out column, out angle, out length1, out length2);
                HObject ellipse;
                HOperatorSet.GenEllipse(out ellipse, row, column, angle, length1, length2);
                ShowObj(jobName, ellipse);
                if (templateRegion == null)
                {
                    templateRegion = ellipse;
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    HOperatorSet.Union2(templateRegion, ellipse, out     templateRegion);
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionSub.Checked)
                {
                    HOperatorSet.Difference(templateRegion, ellipse, out   templateRegion);
                }
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionEllipse.BackColor = Color.Transparent;
                Show_Template();
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 绘制任意形状模板
        /// </summary>
        internal void Draw_Template_Any()
        {
            try
            {
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = true;
                //////Frm_Main.Instance.tsb_dragMode.CheckState = CheckState.Checked;
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionAny.BackColor = Color.LightGreen;
                Frm_ImageWindow.Instance.hwc_imageWindow.Focus();
                HOperatorSet.SetDraw(GetWindowHandle(jobName), new HTuple("margin"));
                Start_Draw_Mode(jobName);
                HObject region;
                if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    SetColor(jobName, "green");
                    SetLineStyle(jobName, 0);
                }
                else
                {
                    SetColor(jobName, "yellow");
                    SetLineStyle(jobName, 4);
                }

                HOperatorSet.DrawRegion(out region, GetWindowHandle(jobName));
                ShowObj(jobName, region);
                if (templateRegion == null)
                {
                    templateRegion = region;
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionAdd.Checked)
                {
                    HOperatorSet.Union2(templateRegion, region, out    templateRegion);
                }
                else if (Frm_ShapeMatchTool.Instance.rdo_templateRegionSub.Checked)
                {
                    HOperatorSet.Difference(templateRegion, region, out     templateRegion);
                }
                Frm_ShapeMatchTool.Instance.btn_drawTemplateRegionAny.BackColor = Color.Transparent;
                Show_Template();
                Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = false;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 绘制搜索区域
        /// </summary>
        internal void Draw_Search_Region()
        {
            try
            {
                if (Frm_Main.ignore)
                    return;
                if (Frm_ShapeMatchTool.Instance.cbx_shapeMatchSearchRegion.Text == "")
                    return;
                Frm_ShapeMatchTool.Instance.btn_drawShapeMatchSearchRegion.Enabled = false;
                HOperatorSet.SetLineStyle(GetWindowHandle(jobName), new HTuple());
                //////Frm_Main.Instance.tsb_dragMode.CheckState = CheckState.Checked;
                Frm_ShapeMatchTool.Instance.btn_drawShapeMatchSearchRegion.BackColor = Color.Green;
                Frm_ImageWindow.Instance.hwc_imageWindow.Focus();
                HOperatorSet.SetColor(GetWindowHandle(jobName), new HTuple("blue"));
                HOperatorSet.SetDraw(GetWindowHandle(jobName), "margin");
                Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = null;
                if (!Frm_ShapeMatchTool.Instance.ckb_union.Checked)
                {
                    HOperatorSet.ClearWindow(GetWindowHandle(jobName));
                    Frm_ImageWindow.Instance.hwc_imageWindow.HobjectToHimage(Frm_ImageWindow.currentImage);
                    // HOperatorSet.DispObj(Frm_ImageWindow.currentImage, GetWindowHandle(jobName));
                }
                switch (Frm_ShapeMatchTool.Instance.cbx_shapeMatchSearchRegion.Text)
                {
                    case "矩形":
                    case "Rectangle1":


                        this.regions.Clear();
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.genRect1(200.0, 200.0, 600.0, 800.0, ref this.regions);


                        searchRegionType = RegionType.Rectangle1;
                        break;
                    case "仿射矩形":
                    case "Rectangle2":
                        this.regions.Clear();
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.genRect2 (400.0, 500.0,0, 300.0, 200.0, ref this.regions);
                     
                        searchRegionType = RegionType.Rectangle2;
                        break;
                    case "圆":
                    case "Circle":
                      
                             this.regions.Clear();
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.genCircle  (400.0, 500.0, 200.0, ref this.regions);
                        searchRegionType = RegionType.Circle;
                        break;
                    case "椭圆":
                    case "Ellipse":
                        HTuple row4, column4, angle4, length4, length5;
                        if (searchRegionType == RegionType.Ellipse && !Frm_ShapeMatchTool.Instance.ckb_union.Checked)
                            HOperatorSet.DrawEllipseMod(GetWindowHandle(jobName), searchRegionPoint[0], searchRegionPoint[1], searchRegionPoint[2], searchRegionPoint[3], searchRegionPoint[4], out row4, out column4, out angle4, out length4, out length5);
                        else
                            HOperatorSet.DrawEllipse(GetWindowHandle(jobName), out row4, out column4, out angle4, out length4, out length5);
                        searchRegionPoint.Clear();
                        searchRegionPoint.Add((double)row4);
                        searchRegionPoint.Add((double)column4);
                        searchRegionPoint.Add((double)angle4);
                        searchRegionPoint.Add((double)length4);
                        searchRegionPoint.Add((double)length5);

                        HObject ellipse;
                        HOperatorSet.GenEllipse(out ellipse, row4, column4, angle4, length4, length5);
                        HOperatorSet.DispObj(ellipse, GetWindowHandle(jobName));
                        if (Frm_ShapeMatchTool.Instance.ckb_union.Checked && SearchRegion != null)
                            HOperatorSet.Union2(SearchRegion, ellipse, out   _searchRegion);
                        else
                            SearchRegion = ellipse;
                        searchRegionType = RegionType.Ellipse;
                        break;
                    case "任意":
                    case "Any":
                        HObject polygon;
                        HOperatorSet.DrawRegion(out polygon, GetWindowHandle(jobName));
                        HOperatorSet.DispObj(polygon, GetWindowHandle(jobName));
                        if (SearchRegion == null)
                        {
                            SearchRegion = polygon;
                        }
                        else
                        {
                            HOperatorSet.Union2(SearchRegion, polygon, out   _searchRegion);
                        }
                        break;
                }
                Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;
                HTuple area;
                HOperatorSet.AreaCenter(SearchRegion, out area, out   searchRegionCenterRow, out    searchRegionCenterColumn);
                HOperatorSet.ReduceDomain(Frm_ImageWindow.currentImage, SearchRegion, out reducedImage);
                Frm_ImageWindow.Instance.hwc_imageWindow.Select();
                Frm_ShapeMatchTool.Instance.btn_drawShapeMatchSearchRegion.BackColor = Color.White;
                Frm_ShapeMatchTool.Instance.btn_drawShapeMatchSearchRegion.Enabled = true;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 对比度改变
        /// </summary>
        internal void Contrast_Changed()
        {
            try
            {
                if (!Frm_ShapeMatchTool.Instance.ckb_autoContrast.Checked)
                    Frm_ShapeMatchTool.Instance.lbl_contastValue.Text = Frm_ShapeMatchTool.Instance.tkb_contrast.Value.ToString();
                contrast = Convert.ToDouble(Frm_ShapeMatchTool.Instance.tkb_contrast.Value);
                if (Create_Template() != 0)
                    return;
                ShowImage(jobName, inputImage);
                HObject countour;
                HOperatorSet.GetShapeModelContours(out countour, modelID, (HTuple)1);
                HTuple area, row, column;
                HOperatorSet.AreaCenter(templateRegion, out area, out row, out column);
                HTuple homMat2D;
                HOperatorSet.HomMat2dIdentity(out homMat2D);
                HOperatorSet.HomMat2dTranslate(homMat2D, row, column, out homMat2D);
                HOperatorSet.AffineTransContourXld(countour, out countour, homMat2D);
                HOperatorSet.SetLineStyle(GetWindowHandle(jobName), new HTuple());
                HOperatorSet.SetColor(GetWindowHandle(jobName), new HTuple("orange"));
                HOperatorSet.DispObj(countour, GetWindowHandle(jobName));
                HOperatorSet.SetColor(GetWindowHandle(jobName), new HTuple("green"));
                HOperatorSet.DispObj(templateRegion, GetWindowHandle(jobName));
                HOperatorSet.SetColor(GetWindowHandle(jobName), new HTuple("blue"));
                if (SearchRegion != null)
                {
                    ShowObj(jobName, SearchRegion);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 单击结果dgv控件，查看每一个匹配结果
        /// </summary>
        internal void Click_Result_Dgv(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                Frm_ImageWindow.Instance.Display_Image(Frm_ImageWindow.currentImage);
                int rowIndex = e.RowIndex;
                string index = Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows[rowIndex].Cells[0].Value.ToString();
                double row = Convert.ToDouble(Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows[rowIndex].Cells[2].Value.ToString());
                double column = Convert.ToDouble(Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows[rowIndex].Cells[3].Value.ToString());
                double temp = Convert.ToDouble(Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows[rowIndex].Cells[4].Value.ToString());
                temp = Math.Round(temp, 0);
                HTuple angle = ((HTuple)(temp)).TupleRad();
                Frm_Main.Instance.dev_display_shape_Match_results(modelID, "orange", row, column, angle, 1, 1, 0);

                //显示区域
                HTuple homMat2D;
                HOperatorSet.HomMat2dIdentity(out homMat2D);
                HTuple area1, row1, column1;
                HOperatorSet.AreaCenter(templateRegion, out area1, out row1, out column1);
                HOperatorSet.HomMat2dTranslate(homMat2D, (HTuple)(-row1), (HTuple)(-column1), out homMat2D);
                HOperatorSet.HomMat2dRotate(homMat2D, angle, (HTuple)0, (HTuple)0, out homMat2D);
                HObject rectangle1AfterTrans;
                HOperatorSet.HomMat2dTranslate(homMat2D, row, column, out homMat2D);
                HOperatorSet.AffineTransRegion(templateRegion, out rectangle1AfterTrans, homMat2D, "nearest_neighbor");
                HOperatorSet.SetColor(GetWindowHandle(jobName), new HTuple("green"));
                HOperatorSet.DispObj(rectangle1AfterTrans, GetWindowHandle(jobName));

                //显示中心十字架
                HOperatorSet.SetColor(GetWindowHandle(jobName), new HTuple("blue"));
                HOperatorSet.SetLineWidth(GetWindowHandle(jobName), new HTuple(1));
                HOperatorSet.DispCross(GetWindowHandle(jobName), (HTuple)row, (HTuple)column, 20, angle);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 用标准图像创建模板
        /// </summary>
        /// <returns>结果状态返回值：0表示成功 1表示无输入图像 -1表示未知异常 2表示特征过少</returns>
        internal int Create_Template_With_Standard_Image()
        {
            try
            {
                HObject template;
                HOperatorSet.ReduceDomain(standardImage, templateRegion, out template);
                try
                {
                    HOperatorSet.CreateShapeModel(template,
                                                 (HTuple)"auto",
                                                 (HTuple)startAngle,
                                                 (HTuple)angleRange,
                                                (HTuple)("auto"),
                                                 (HTuple)"auto",
                                                 (HTuple)polarity,
                                                 Frm_ShapeMatchTool.Instance.ckb_autoContrast.Checked ? (HTuple)"auto" : (HTuple)contrast,
                                                 (HTuple)"auto",
                                                 out modelID);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("#8510:"))      //特征过少，Halcon报错编号8510
                    {
                        MessageBox.Show("特征过少，无法完成训练（错误代码：0201）");
                        return 2;
                    }
                }
                isCreated = true;
                return 0;       //此处返回0可能会有问题，应该返回modelID，待确认
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return -1;
            }
        }
        /// <summary>
        /// 创建模板
        /// </summary>
        /// <returns>结果状态返回值：0表示成功 1表示无输入图像 -1表示未知异常 2表示特征过少</returns>
        internal int Create_Template()
        {
            try
            {
                HObject template;
                if (inputImage == null)
                {
                    runStatu = ToolRunStatu.未指定输入图像;
                    return 1;
                }
                HOperatorSet.ReduceDomain(inputImage, templateRegion, out template);
                try
                {
                    HOperatorSet.CreateShapeModel(template,
                                                 (HTuple)"auto",
                                                 (HTuple)startAngle,
                                                 (HTuple)angleRange,
                                                (HTuple)("auto"),
                                                 (HTuple)"auto",
                                                 (HTuple)polarity,
                                                 Frm_ShapeMatchTool.Instance.ckb_autoContrast.Checked ? (HTuple)"auto" : (HTuple)contrast,
                                                 (HTuple)"auto",
                                                 out modelID);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("#8510:"))      //特征过少，Halcon报错编号8510
                    {
                        MessageBox.Show("特征过少，无法完成训练（错误代码：0201）");
                        return 2;
                    }
                }
                isCreated = true;
                standardImage = inputImage;
                return 0;       //此处返回0可能会有问题，应该返回modelID，待确认
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return -1;
            }
        }
        /// <summary>
        /// 清除搜索区域
        /// </summary>
        internal void Clear_Search_Region()
        {
            try
            {
                SearchRegion = null;
                searchRegionType = RegionType.None;
                ShowImage(jobName, inputImage);
                if (templateRegion != null)
                {
                    SetColor(jobName, "green");
                    ShowObj(jobName, templateRegion);
                }
                Frm_ShapeMatchTool.Instance.cbx_shapeMatchSearchRegion.Text = "";

            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 注册haclon窗体的鼠标弹起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hwindow_MouseUp(object sender, MouseEventArgs e)
        {
            int index;

            List<double> data;
            ViewWindow.Model.ROI roi = Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.smallestActiveROI(out data, out index);

            if (index > -1)
            {
                string name = roi.GetType().Name;

                this.regions[index] = roi;
            }
        }
        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="updateImage">是否刷新图像</param>
        public  override void Run(string jobName, bool updateImage, bool b)
        {
            try
            {
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败;
                if (inputImage == null)
                {
                    runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Create_Template : ToolRunStatu.未指定输入图像;
                    return;
                }

                if (!isCreated)
                {
                    runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Create_Template : ToolRunStatu.未创建模板;
                    return;
                }

                HObject image;
                if (searchRegionType == RegionType.None)
                {
                    image = inputImage;
                }
                else
                {
                    HTuple area;
                    HOperatorSet.AreaCenter(SearchRegion, out area, out    searchRegionCenterRow, out   searchRegionCenterColumn);
                    HOperatorSet.ReduceDomain(inputImage, SearchRegion, out   reducedImage);
                    image = reducedImage;
                }

                List<MatchResult> L_resultList = new List<MatchResult>();
                HTuple rows, cols, angles, scores;
                Stopwatch sw = new Stopwatch();
                sw.Start();

                //此处Catch中重新查找，是因为开启程序第一次查找时会报错，这是需要重新创建模板，然后重新查找即可
                try
                {

                    HOperatorSet.FindShapeModel(image,
                                             (HTuple)modelID,
                                            ((HTuple)Convert.ToDouble(startAngle)).TupleRad(),
                                               (HTuple)new HTuple(Convert.ToDouble(angleRange)).TupleRad(),
                                               (HTuple)Convert.ToDouble(minScore),
                                                (HTuple)Convert.ToInt16(expectMatchNum),
                                               (HTuple)0.5,
                                                (HTuple)"least_squares",
                                               (HTuple)0,
                                               (HTuple)0.9,
                                                out rows,
                                                out cols,
                                                out angles,
                                                out scores);
                }
                catch
                {
                    Create_Template_With_Standard_Image();
                    HOperatorSet.FindShapeModel(image,
                                            (HTuple)modelID,
                                           ((HTuple)Convert.ToDouble(startAngle)).TupleRad(),
                                              (HTuple)new HTuple(Convert.ToDouble(angleRange)).TupleRad(),
                                              (HTuple)Convert.ToDouble(minScore),
                                               (HTuple)Convert.ToInt16(expectMatchNum),
                                              (HTuple)0.5,
                                               (HTuple)"least_squares",
                                              (HTuple)0,
                                              (HTuple)0.9,
                                               out rows,
                                               out cols,
                                               out angles,
                                               out scores);
                }
                sw.Stop();
                double time = sw.ElapsedMilliseconds;

                if ((int)(rows.TupleLength()) > 0)
                {
                    for (int i = 0; i < (int)rows.TupleLength(); i++)
                    {
                        MatchResult result = new MatchResult();
                        result.Socre = Math.Round((double)scores[i], 3);
                        result.Row = Math.Round((double)rows[i], 3);
                        result.Col = Math.Round((double)cols[i], 3);
                        result.Angle = Math.Round((double)((HTuple)angles[i]), 3);
                        L_resultList.Add(result);
                    }
                }

                //以下代码对结果依据分数进行排序
                MatchResult temp;
                for (int i = 0; i < L_resultList.Count - 1; i++)
                {
                    for (int j = i + 1; j < L_resultList.Count; j++)
                    {
                        if (L_resultList[i].Socre < L_resultList[j].Socre)
                        {
                            temp = L_resultList[i];
                            L_resultList[i] = L_resultList[j];
                            L_resultList[j] = temp;
                        }
                    }
                }
                L_matchResult = L_resultList;

                //重新显示图像，静态图形消失
                if (updateImage)
                {
                    ShowImage(jobName, inputImage);
                }

                //显示匹配特征
                if (showFeature)
                {
                    HObject countor;
                    HOperatorSet.SetColor(GetWindowHandle(jobName), new HTuple("orange"));
                    HOperatorSet.SetDraw(GetWindowHandle(jobName), new HTuple("margin"));
                    HOperatorSet.GetShapeModelContours(out countor, modelID, new HTuple(1));
                    for (int i = 0; i < L_matchResult.Count; i++)
                    {
                        HTuple homMat2D;
                        HOperatorSet.HomMat2dIdentity(out homMat2D);
                        HOperatorSet.HomMat2dTranslate(homMat2D, L_matchResult[i].Row, L_matchResult[i].Col, out homMat2D);
                        HOperatorSet.HomMat2dRotate(homMat2D, (HTuple)L_matchResult[i].Angle, (HTuple)L_matchResult[i].Row, (HTuple)L_matchResult[i].Col, out homMat2D);
                        HObject countorAfterTrans;
                        HOperatorSet.AffineTransContourXld(countor, out countorAfterTrans, homMat2D);
                        //HOperatorSet.DispObj(countorAfterTrans, GetWindowHandle(jobName));
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayHobject(countorAfterTrans, "orange");
                    }
                }

                //显示结果
                Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows.Clear();
                for (int i = 0; i < L_matchResult.Count; i++)
                {
                    int index = Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows.Add();
                    Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows[index].Cells[0].Value = i + 1;
                    Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows[index].Cells[1].Value = L_matchResult[i].Socre.ToString();
                    Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows[index].Cells[2].Value = L_matchResult[i].Row.ToString();
                    Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows[index].Cells[3].Value = L_matchResult[i].Col.ToString();
                    Frm_ShapeMatchTool.Instance.dgv_matchResult.Rows[index].Cells[4].Value = L_matchResult[i].Angle.ToString();

                    HTuple homMat2D;
                    HOperatorSet.HomMat2dIdentity(out homMat2D);
                    HTuple area, row, column;
                    HOperatorSet.AreaCenter(templateRegion, out area, out row, out column);
                    HOperatorSet.HomMat2dTranslate(homMat2D, (HTuple)(-row), (HTuple)(-column), out homMat2D);
                    double roation = Convert.ToDouble(L_matchResult[i].Angle.ToString("0.000"));
                    roation = Math.Round(roation, 0);
                    HTuple angle = (HTuple)roation;
                    HOperatorSet.HomMat2dRotate(homMat2D, angle, (HTuple)0, (HTuple)0, out homMat2D);
                    HObject rectangle1AfterTrans;
                    HOperatorSet.HomMat2dTranslate(homMat2D, L_matchResult[i].Row, L_matchResult[i].Col, out homMat2D);
                    HOperatorSet.AffineTransRegion(templateRegion, out rectangle1AfterTrans, homMat2D, "nearest_neighbor");
                    HOperatorSet.SetColor(GetWindowHandle(jobName), new HTuple("green"));
                    HOperatorSet.DispObj(rectangle1AfterTrans, GetWindowHandle(jobName));
                    // ShowObj(jobName, rectangle1AfterTrans);
                    Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayHobject(rectangle1AfterTrans, "green");

                    //显示中心十字架
                    if (showCross)
                    {
                        SetColor(jobName, "blue");
                        HOperatorSet.AffineTransPoint2d(homMat2D, row, column, out row, out column);
                        HOperatorSet.SetLineWidth(GetWindowHandle(jobName), new HTuple(1));
                        //HOperatorSet.DispCross(Frm_Main.fullScreen ? Frm_FullScreen.Instance.windowHandle : GetWindowHandle(jobName), row, column, new HTuple(20), angle);
                        HObject cross;
                        HOperatorSet.GenCrossContourXld(out cross, row, column, new HTuple(20), angle);
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayHobject(cross, "blue");
                    }
                }
                HOperatorSet.SetColor(GetWindowHandle(jobName), new HTuple("blue"));
                if (SearchRegion != null)
                {
                    if (b)
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayROI(regions);
                    else
                    {
                        ClearEvent(Frm_ImageWindow.Instance.hwc_imageWindow.hWindowControl, "MouseUp");
                        Frm_ImageWindow.Instance.hwc_imageWindow.hWindowControl.MouseUp += Hwindow_MouseUp;
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayHobject(regions[0].getRegion(), "blue");
                    }
                }

                //Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.selectROI(0);
                // ShowObj(jobName, SearchRegion);

                matchNum = L_resultList.Count;
                if (L_resultList.Count > 0)
                {
                    resultRow = L_resultList[0].Row;
                    resultCol = L_resultList[0].Col;
                    resultAngle = L_resultList[0].Angle;
                    pose.X  = resultRow;
                    pose.Y  = resultCol;
                    pose.U  = resultAngle;
                }
                else
                {
                    resultRow = 0;
                    resultCol = 0;
                    resultAngle = 0;
                    pose.X  = 0;
                    pose.Y  = 0;
                    pose.U  = 0;
                }
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 删除指定控件的指定事件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventname"></param>
        public void ClearEvent(System.Windows.Forms.Control control, string eventname)
        {
            if (control == null) return;
            if (string.IsNullOrEmpty(eventname)) return;

            BindingFlags mPropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
            BindingFlags mFieldFlags = BindingFlags.Static | BindingFlags.NonPublic;
            Type controlType = typeof(System.Windows.Forms.Control);
            PropertyInfo propertyInfo = controlType.GetProperty("Events", mPropertyFlags);
            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(control, null);
            FieldInfo fieldInfo = (typeof(System.Windows.Forms.Control)).GetField("Event" + eventname, mFieldFlags);
            Delegate d = eventHandlerList[fieldInfo.GetValue(control)];

            if (d == null) return;
            EventInfo eventInfo = controlType.GetEvent(eventname);

            foreach (Delegate dx in d.GetInvocationList())
                eventInfo.RemoveEventHandler(control, dx);

        }
    }
}
