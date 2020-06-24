using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class BlobAnalyseTool : ToolBase
    {
        internal BlobAnalyseTool()
        {
            //默认添加一个面积筛选
            SelectItem select = new SelectItem();
            select.SelectType = "area";
            select.AreaDownLimit = 10;
            select.AreaUpLimit = 100000000;
            L_select.Add(select);
        }

        /// <summary>
        /// 轮廓线宽
        /// </summary>
        internal int marginLineWidth = 1;
        /// <summary>
        /// 是否显示搜索区域
        /// </summary>
        internal bool displaySearchRegion = true;
        /// <summary>
        /// 是否显示中心十字架
        /// </summary>
        internal bool displayCross = true;
        /// <summary>
        /// 显示外接圆的填充模式
        /// </summary>
        internal FillMode outCircleDrawMode = FillMode.Margin;
        /// <summary>
        /// 搜索区域对应的图像
        /// </summary>
        private HObject searchRegionImage;
        /// <summary>
        /// 是否显示结果区域
        /// </summary>
        internal bool showResultRegion = true;
        /// <summary>
        /// 所有筛选方式集合
        /// </summary>
        internal List<SelectItem> L_select = new List<SelectItem>();
        /// <summary>
        /// 区域处理操作集合
        /// </summary>
        internal List<PreProcessing> L_prePorcessing = new List<PreProcessing>();
        /// <summary>
        /// 是否显示斑点外接圆
        /// </summary>
        internal bool showOutCircle = false;
        /// <summary>
        /// 结果区域输出
        /// </summary>
        internal HObject outputResultRegion;
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
        /// 搜索区域输入
        /// </summary>
        internal HObject inputSearchRegion;
        /// <summary>
        /// 输入图像
        /// </summary>
        internal HObject inputImage;
        /// <summary>
        /// 结果斑点集合
        /// </summary>
        internal List<BlobResult> L_resultBlob = new List<BlobResult>();
        /// <summary>
        /// 是否填充空洞
        /// </summary>
        internal bool fillHole = true;
        /// <summary>
        /// 搜索区域类型
        /// </summary>
        internal RegionType searchRegionType = RegionType.None;
        /// <summary>
        /// 结果区域填充模式
        /// </summary>
        internal FillMode resultRegionDrawMode = FillMode.Margin ;
        /// <summary>
        /// 阈值下限
        /// </summary>
        internal double minThreshold = 200;
        /// <summary>
        /// 阈值上限
        /// </summary>
        internal double maxThreshold = 255;


        /// <summary>
        /// 清除搜索区域
        /// </summary>
        /// <param name="jobName">流程名</param>
        internal void Clear_Search_Region(string jobName)
        {
            try
            {
                Frm_BlobAnalyseTool.Instance.cbx_blobAnalyseSearchRegion.Text = string.Empty;
                SearchRegion = null;
                searchRegionType = RegionType.None;
                ShowImage(jobName, inputImage);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 复位工具到初始状态
        /// </summary>
        internal void ResetTool()
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 保存筛选项
        /// </summary>
        internal void SaveSelectItem()
        {
            try
            {
                L_select.Clear();
                for (int i = 0; i < Frm_BlobAnalyseTool.Instance.dgv_select.Rows.Count; i++)
                {
                    if (Frm_BlobAnalyseTool.Instance.dgv_select.Rows[i].Cells[0].Value != null)
                    {
                        SelectItem select = new SelectItem();
                        select.SelectType = Frm_BlobAnalyseTool.Instance.dgv_select.Rows[i].Cells[0].Value.ToString();
                        try
                        {
                            select.AreaDownLimit = Convert.ToInt32(Frm_BlobAnalyseTool.Instance.dgv_select.Rows[i].Cells[1].Value);
                            select.AreaUpLimit = Convert.ToInt32(Frm_BlobAnalyseTool.Instance.dgv_select.Rows[i].Cells[2].Value);
                        }
                        catch
                        {
                            Frm_Output.Instance.OutputMsg("输入了非法字符，保存失败（错误代码：0501）", Color.Red);
                        }
                        L_select.Add(select);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 添加处理项
        /// </summary>
        internal void AddProcessingItem()
        {
            try
            {
                switch (Frm_BlobAnalyseTool.Instance.tvw_preProcessingItem.SelectedNode.Text)
                {
                    //////if (preProcessingItem == "开运算")
                    //////{
                    //////    PreProcessing prePorcessing = new PreProcessing();
                    //////    prePorcessing.PreProcessingType = "开运算";
                    //////    prePorcessing.ElementType = "circle";
                    //////    prePorcessing.ElementSize = 3;
                    //////    prePorcessing.Enable = true;
                    //////    blobAnalyseTool.L_prePorcessing.Add(prePorcessing);
                    //////    int index = dgv_processingItem.Rows.Add();
                    //////    dgv_processingItem.Rows[index].Cells[0].Value = "开运算";
                    //////    ((DataGridViewCheckBoxCell)this.dgv_processingItem.Rows[index].Cells[1]).Value = true;
                    //////}
                    //////else if (preProcessingItem == "闭运算")
                    //////{
                    //////    PreProcessing prePorcessing = new PreProcessing();
                    //////    prePorcessing.PreProcessingType = "闭运算";
                    //////    prePorcessing.ElementType = "circle";
                    //////    prePorcessing.ElementSize = 3;
                    //////    prePorcessing.Enable = true;
                    //////    blobAnalyseTool.L_prePorcessing.Add(prePorcessing);
                    //////    int index = dgv_processingItem.Rows.Add();
                    //////    ((DataGridViewCheckBoxCell)this.dgv_processingItem.Rows[index].Cells[1]).Value = true;
                    //////    dgv_processingItem.Rows[index].Cells[0].Value = "闭运算";
                    //////}
                    case "填充":
                        PreProcessing prePorcessing = new PreProcessing();
                        prePorcessing.PreProcessingType = "填充";
                        prePorcessing.ElementType = "";
                        prePorcessing.ElementSize = 0;
                        prePorcessing.Enable = true;
                        L_prePorcessing.Add(prePorcessing);
                        int index = Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows.Add();
                        ((DataGridViewCheckBoxCell)Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[1]).Value = true;
                        Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[0].Value = "填充";
                        break;
                    case "腐蚀":
                        prePorcessing = new PreProcessing();
                        prePorcessing.PreProcessingType = "腐蚀";
                        prePorcessing.ElementType = "";
                        prePorcessing.ElementSize = 1;
                        prePorcessing.Enable = true;
                        prePorcessing.MinArea = 1;
                        prePorcessing.MaxArea = 10000;
                        L_prePorcessing.Add(prePorcessing);
                        index = Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows.Add();
                        ((DataGridViewCheckBoxCell)Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[1]).Value = true;
                        Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[0].Value = "腐蚀";
                        break;
                    case "膨胀":
                        prePorcessing = new PreProcessing();
                        prePorcessing.PreProcessingType = "膨胀";
                        prePorcessing.ElementType = "";
                        prePorcessing.Enable = true;
                        prePorcessing.ElementSize = 1;
                        prePorcessing.MinArea = 1;
                        prePorcessing.MaxArea = 10000;
                        L_prePorcessing.Add(prePorcessing);
                        index = Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows.Add();
                        Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[0].Value = "膨胀";
                        ((DataGridViewCheckBoxCell)Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[1]).Value = true;
                        break;
                    default:
                        Frm_MessageBox.Instance.MessageBoxShow("\r\n尚未开发，敬请期待！");
                        break;
                }
                Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows[Frm_BlobAnalyseTool.Instance.dgv_processingItem.Rows.Count - 1].Selected = true;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 点击结果列表
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="e"></param>
        internal void Click_Result_List(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    for (int i = 0; i < L_resultBlob.Count; i++)
                    {
                        if (L_resultBlob[i].Area.ToString() == dgv.Rows[e.RowIndex].Cells[1].Value.ToString()
                            && L_resultBlob[i].Row.ToString() == dgv.Rows[e.RowIndex].Cells[2].Value.ToString()
                            && L_resultBlob[i].Col.ToString() == dgv.Rows[e.RowIndex].Cells[3].Value.ToString())
                        {
                            Frm_ImageWindow.Instance.Display_Image(Frm_ImageWindow.currentImage);

                            //显示搜索区域
                            if (searchRegionType != RegionType.None)
                            {
                                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("blue"));
                                HOperatorSet.SetDraw(Frm_ImageWindow.Instance.WindowHandle, new HTuple("margin"));
                                Frm_Main.Instance.Display_Obj(Frm_ImageWindow.Instance.WindowHandle, (HObject)SearchRegion);
                            }

                            //显示结果区域
                            if (showResultRegion)
                            {
                                if (resultRegionDrawMode == FillMode.Fill)
                                    HOperatorSet.SetDraw(Frm_ImageWindow.Instance.WindowHandle, new HTuple("fill"));
                                else
                                    HOperatorSet.SetDraw(Frm_ImageWindow.Instance.WindowHandle, new HTuple("margin"));

                                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                                Frm_Main.Instance.Display_Obj(Frm_ImageWindow.Instance.WindowHandle, L_resultBlob[i].region);
                            }

                            //显示外接圆
                            if (showOutCircle)
                            {
                                if (outCircleDrawMode == FillMode.Fill)
                                    HOperatorSet.SetDraw(Frm_ImageWindow.Instance.WindowHandle, new HTuple("fill"));
                                else
                                    HOperatorSet.SetDraw(Frm_ImageWindow.Instance.WindowHandle, new HTuple("margin"));
                                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                                HOperatorSet.DispCircle(Frm_ImageWindow.Instance.WindowHandle, L_resultBlob[i].Row, L_resultBlob[i].Col, L_resultBlob[i].CircumcircleRadius);
                            }

                            //显示中心十字架
                            if (displayCross)
                            {
                                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("blue"));
                                HOperatorSet.DispCross(Frm_ImageWindow.Instance.WindowHandle, new HTuple(L_resultBlob[i].Row), new HTuple(L_resultBlob[i].Col), new HTuple(20), new HTuple(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        internal List<ViewWindow.Model.ROI> regions = new List<ViewWindow.Model.ROI>();
        /// <summary>
        /// 绘制搜索区域
        /// </summary>
        internal void Draw_Search_Region(string jobName)
        {
            try
            {
                if (Frm_BlobAnalyseTool.Instance.cbx_blobAnalyseSearchRegion.Text == string.Empty)
                    return;
                Frm_BlobAnalyseTool.Instance.btn_drawBlobAnalyseRegion.Enabled = false;
                SetLineStyle(jobName, 0);
                //////Frm_Main.Instance.tsb_dragMode.CheckState = CheckState.Unchecked;
                Frm_BlobAnalyseTool.Instance.btn_drawBlobAnalyseRegion.BackColor = Color.Green;
                Frm_ImageWindow.Instance.hwc_imageWindow.Select();
                SetColor(jobName, "blue");
                SetDraw(jobName, "margin");
                Start_Draw_Mode(jobName);
                if (inputImage != null)
                {
                    Frm_ImageWindow.Instance.hwc_imageWindow.ClearWindow();
                    Frm_ImageWindow.Instance.Display_Image(inputImage, Frm_ImageWindow.Instance.hwc_imageWindow.HWindowHalconID);
                }
                switch (Frm_BlobAnalyseTool.Instance.cbx_blobAnalyseSearchRegion.Text)
                {
                    case "矩形":
                    case "Rectangle1":


                        if (this.regions.Count == 0)
                            Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.genRect1(200.0, 200.0, 600.0, 800.0, ref this.regions);
                        else
                            Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayROI(this .regions );

                        searchRegionType = RegionType.Rectangle1;
                        break;
                    case "仿射矩形":
                    case "Rectangle2":
                        if (this.regions.Count == 0)
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.genRect2(400.0, 500.0, 0, 300.0, 200.0, ref this.regions);
                        else
                            Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayROI(this.regions);
                        searchRegionType = RegionType.Rectangle2;
                        break;
                    case "圆":
                    case "Circle":

                        this.regions.Clear();
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.genCircle(400.0, 500.0, 200.0, ref this.regions);
                        searchRegionType = RegionType.Circle;
                        break;
                    case "Ellipse":
                        HTuple row4, column4, angle4, length4, length5;
                        HOperatorSet.DrawEllipse(Frm_ImageWindow.Instance.WindowHandle, out row4, out column4, out angle4, out length4, out length5);
                        HObject ellipse;
                        HOperatorSet.GenEllipse(out ellipse, row4, column4, angle4, length4, length5);
                        HOperatorSet.DispObj(ellipse, Frm_ImageWindow.Instance.WindowHandle);
                        if (SearchRegion == null)
                        {
                            SearchRegion = ellipse;
                        }
                        else
                        {
                            HObject temp;
                            HOperatorSet.Union2((HObject)SearchRegion, ellipse, out  temp);
                            SearchRegion = temp;
                        }
                        searchRegionType = RegionType.Ellipse;
                        break;
                    case "Any":
                        HObject polygon;
                        HOperatorSet.DrawRegion(out polygon, Frm_ImageWindow.Instance.WindowHandle);
                        HOperatorSet.DispObj(polygon, Frm_ImageWindow.Instance.WindowHandle);
                        if (SearchRegion == null)
                        {
                            SearchRegion = polygon;
                        }
                        else
                        {
                            HObject temp;
                            HOperatorSet.Union2((HObject)SearchRegion, polygon, out  temp);
                            SearchRegion = temp;
                        }
                        searchRegionType = RegionType.Any;
                        break;
                    default:
                        HOperatorSet.ClearWindow(Frm_ImageWindow.Instance.WindowHandle);
                        HOperatorSet.DispObj(Frm_ImageWindow.currentImage, Frm_ImageWindow.Instance.WindowHandle);
                        SearchRegion = null;
                        break;
                }
                End_Draw_Mode(jobName);
                Frm_BlobAnalyseTool.Instance.btn_drawBlobAnalyseRegion.BackColor = Color.White;
                Frm_BlobAnalyseTool.Instance.btn_drawBlobAnalyseRegion.Enabled = true;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 运行工具
        /// </summary>
        public override void Run(string jobName, bool updateImage, bool edit = false)
        {
            try
            {
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败;

                //判断是否有图像输入
                if (inputImage == null)
                {
                    runStatu = Configuration.language == Language.English ? ToolRunStatu.Lack_Of_Input_Image : ToolRunStatu.缺少输入图像;
                    return;
                }

                //如果搜索区域是外部输入，判断区域是否有为空
                if (searchRegionType == RegionType.InputRegion)
                {
                    if (inputSearchRegion == null)
                    {
                        runStatu = Configuration.language == Language.English ? ToolRunStatu.Lack_Of_Input_Search_Region : ToolRunStatu.缺少输入搜索区域;
                        return;
                    }
                    SearchRegion = inputSearchRegion;
                }

                if (updateImage)
                {
                    ClearWindow(jobName);
                    ShowObj(jobName, inputImage);
                }

                //截取出搜索区域图像
                if (searchRegionType != RegionType.None)
                {
                    HOperatorSet.ReduceDomain((HObject)inputImage, (HObject)SearchRegion, out   searchRegionImage);
                }

                //开始阈值分割
                HObject resultRegion;
                if (searchRegionType != RegionType.None)
                    HOperatorSet.Threshold(searchRegionImage, out resultRegion, (HTuple)minThreshold, (HTuple)maxThreshold);
                else
                    HOperatorSet.Threshold(inputImage, out resultRegion, (HTuple)minThreshold, (HTuple)maxThreshold);
                HOperatorSet.Connection(resultRegion, out resultRegion);

                //此处进行预处理
                for (int i = 0; i < L_prePorcessing.Count; i++)
                {
                    if (L_prePorcessing[i].Enable == false)
                        continue;
                    switch (L_prePorcessing[i].PreProcessingType)
                    {
                        case "开运算":
                            HOperatorSet.OpeningCircle(resultRegion, out resultRegion, new HTuple(L_prePorcessing[i].ElementSize));
                            break;
                        case "闭运算":
                            HOperatorSet.ClosingCircle(resultRegion, out resultRegion, new HTuple(L_prePorcessing[i].ElementSize));
                            break;
                        case "填充":
                            HOperatorSet.FillUp(resultRegion, out resultRegion);
                            break;
                        case "腐蚀":      //此处对面积在一定范围内的斑点进行腐蚀
                            HTuple count = 0;
                            HOperatorSet.CountObj(resultRegion, out count);
                            HObject tempRegion;
                            HOperatorSet.GenEmptyRegion(out tempRegion);
                            for (int j = 0; j < count; j++)
                            {
                                HObject region;
                                HOperatorSet.SelectObj(resultRegion, out region, new HTuple(j + 1));
                                HTuple area4, row4, col4;
                                HOperatorSet.AreaCenter(region, out area4, out row4, out col4);
                                if ((int)area4 > L_prePorcessing[i].MinArea)
                                {
                                    HOperatorSet.ErosionCircle(region, out region, new HTuple(L_prePorcessing[i].ElementSize));
                                }
                                HOperatorSet.Union2(tempRegion, region, out tempRegion);
                            }
                            resultRegion = tempRegion;
                            HOperatorSet.Connection(resultRegion, out resultRegion);
                            break;
                        case "膨胀":
                            HOperatorSet.CountObj(resultRegion, out count);
                            HOperatorSet.GenEmptyRegion(out tempRegion);
                            for (int j = 0; j < count; j++)
                            {
                                HObject region;
                                HOperatorSet.SelectObj(resultRegion, out region, new HTuple(j + 1));
                                HTuple area4, row4, col4;
                                HOperatorSet.AreaCenter(region, out area4, out row4, out col4);
                                if ((int)area4 < L_prePorcessing[i].MaxArea && (int)area4 > L_prePorcessing[i].MinArea)
                                {
                                    HOperatorSet.DilationCircle(region, out region, new HTuple(L_prePorcessing[i].ElementSize));
                                }
                                HOperatorSet.Union2(tempRegion, region, out tempRegion);
                            }
                            resultRegion = tempRegion;
                            HOperatorSet.Connection(resultRegion, out resultRegion);
                            break;
                    }
                }

                //特征筛选
                for (int i = 0; i < L_select.Count; i++)
                {
                    HOperatorSet.SelectShape(resultRegion, out resultRegion, (HTuple)L_select[i].SelectType, (HTuple)"and", new HTuple(L_select[i].AreaDownLimit), new HTuple(L_select[i].AreaUpLimit));
                }

                //空洞填充
                if (fillHole)
                {
                    HOperatorSet.FillUp(resultRegion, out resultRegion);
                    HOperatorSet.Connection(resultRegion, out resultRegion);
                }

                //显示搜索区域
                if (searchRegionType != RegionType.None && displaySearchRegion)
                {
                    SetDraw(jobName, "margin");
                    if (Frm_BarcodeTool .Instance.Visible )
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayROI(regions);
                    else
                    {
                        ClearEvent(Frm_ImageWindow.Instance.hwc_imageWindow.hWindowControl, "MouseUp");
                        Frm_ImageWindow.Instance.hwc_imageWindow.hWindowControl.MouseUp += Hwindow_MouseUp;
                        Frm_ImageWindow.Instance.hwc_imageWindow.viewWindow.displayHobject(regions[0].getRegion(), "blue");
                    }
                }

                if (showResultRegion)
                {
                    if (resultRegionDrawMode == FillMode.Fill)
                        SetDraw(jobName, "fill");
                    else
                        SetDraw(jobName, "margin");
                    SetColor(jobName, "green");
                    SetLineWidth(jobName, marginLineWidth);
                   // ShowObj(jobName, resultRegion);
                    Frm_ImageWindow.Instance.hwc_imageWindow.DispObj(resultRegion, "green");
                }

                outputResultRegion = resultRegion;
                HOperatorSet.Union1(outputResultRegion, out outputResultRegion);
                HOperatorSet.Connection(outputResultRegion, out outputResultRegion);

                HTuple resultCount = 0;
                HOperatorSet.CountObj(resultRegion, out resultCount);


                SetLineWidth(jobName, 1);
                L_resultBlob.Clear();
                for (int i = 0; i < resultCount; i++)
                {
                    HObject region;
                    HOperatorSet.SelectObj(resultRegion, out region, new HTuple(i + 1));

                    //显示外接圆
                    if (showOutCircle)
                    {
                        if (outCircleDrawMode == FillMode.Fill)
                            SetDraw(jobName, "fill");
                        else
                            SetDraw(jobName, "margin");
                        SetColor(jobName, "green");
                        HTuple row3, col3, radius3;
                        HOperatorSet.SmallestCircle(region, out row3, out col3, out radius3);
                        HObject circle;
                        HOperatorSet.GenCircle(out circle, row3, col3, radius3);
                        ShowObj(jobName, circle);
                    }

                    HTuple row1, column1, area1;
                    HOperatorSet.AreaCenter(region, out area1, out row1, out column1);
                    HTuple row2, col2, radius2;
                    HOperatorSet.SmallestCircle(region, out row2, out col2, out radius2);
                    BlobResult blobResult = new BlobResult();

                    blobResult.Row = Math.Round(Convert.ToDouble(row1.ToString()), 3);
                    blobResult.Col = Math.Round(Convert.ToDouble(column1.ToString()), 3);
                    blobResult.Area = Math.Round(Convert.ToDouble(area1.ToString()), 3);
                    blobResult.CircumcircleRadius = Math.Round(Convert.ToDouble(radius2.ToString()), 3);
                    blobResult.region = region;
                    L_resultBlob.Add(blobResult);

                    //显示十字架
                    if (displayCross)
                    {
                        SetColor(jobName, "blue");
                        HObject cross;
                        HOperatorSet.GenCrossContourXld(out cross, row1, column1, new HTuple(20), new HTuple(0));
                        ShowObj(jobName, cross);
                    }
                }

                //排序
                BlobResult temp1;
                for (int i = 0; i < L_resultBlob.Count - 1; i++)
                {
                    for (int j = i + 1; j < L_resultBlob.Count; j++)
                    {
                        if (L_resultBlob[i].Area < L_resultBlob[j].Area)
                        {
                            temp1 = L_resultBlob[i];
                            L_resultBlob[i] = L_resultBlob[j];
                            L_resultBlob[j] = temp1;
                        }
                    }
                }

                //显示结果
                Frm_BlobAnalyseTool.Instance.dgv_blobAnalyseResult.Rows.Clear();
                for (int i = 0; i < L_resultBlob.Count; i++)
                {
                    int index = Frm_BlobAnalyseTool.Instance.dgv_blobAnalyseResult.Rows.Add();
                    Frm_BlobAnalyseTool.Instance.dgv_blobAnalyseResult.Rows[index].Cells[0].Value = i + 1;
                    Frm_BlobAnalyseTool.Instance.dgv_blobAnalyseResult.Rows[index].Cells[1].Value = L_resultBlob[i].Area;
                    Frm_BlobAnalyseTool.Instance.dgv_blobAnalyseResult.Rows[index].Cells[2].Value = L_resultBlob[i].Row;
                    Frm_BlobAnalyseTool.Instance.dgv_blobAnalyseResult.Rows[index].Cells[3].Value = L_resultBlob[i].Col;
                    Frm_BlobAnalyseTool.Instance.dgv_blobAnalyseResult.Rows[index].Cells[4].Value = L_resultBlob[i].CircumcircleRadius;
                }

                runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
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
