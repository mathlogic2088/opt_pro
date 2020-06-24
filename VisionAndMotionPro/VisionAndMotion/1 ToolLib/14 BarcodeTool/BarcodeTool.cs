using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class BarcodeTool : ToolBase
    {

        /// <summary>
        /// 条码搜索区域图片
        /// </summary>
        internal HObject searchRegionImage;
        /// <summary>
        /// 解析出来的条码个数
        /// </summary>
        internal int resultNum = 0;
        /// <summary>
        /// 要查找的条码数量
        /// </summary>
        internal int findNum = 1;
        /// <summary>
        /// 最小对比度
        /// </summary>
        internal double minContrast = 0.5;
        /// <summary>
        /// 在图像上显示条码
        /// </summary>
        internal bool showResultStr = true;
        /// <summary>
        /// 显示条码方向
        /// </summary>
        internal bool showArrow = true;
        /// <summary>
        /// 解析出来的条码
        /// </summary>
        internal string outputStr = string.Empty;
        /// <summary>
        /// 条码搜索区域
        /// </summary>
        internal HObject SearchRegion;
        /// <summary>
        /// 输入图像
        /// </summary>
        internal HObject inputImage;
        /// <summary>
        /// 用于存放查找结果和结果对应的区域
        /// </summary>
        internal Dictionary<string, HObject> D_stringAndRegion = new Dictionary<string, HObject>();
        /// <summary>
        /// 工具运行结果
        /// </summary>
        internal List<RunResult> L_result = new List<RunResult>();


        /// <summary>
        /// 结果列表点击事件
        /// </summary>
        /// <param name="e"></param>
        internal void Click_Result_Dgv(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                Frm_ImageWindow.Instance.set_display_font(Frm_ImageWindow.Instance.WindowHandle, 20, "mono", "true", "false");
                HOperatorSet.ClearWindow(Frm_ImageWindow.Instance.WindowHandle);
                HOperatorSet.DispObj(Frm_ImageWindow.currentImage, Frm_ImageWindow.Instance.WindowHandle);
                int rowIndex = e.RowIndex;
                HObject region = D_stringAndRegion[(e.RowIndex + 1).ToString()];
                HOperatorSet.SetLineWidth(Frm_ImageWindow.Instance.WindowHandle, new HTuple(2));
                HOperatorSet.DispObj(region, Frm_ImageWindow.Instance.WindowHandle);
                string resultStr = Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[rowIndex].Cells[2].Value.ToString();
                Frm_Main.Instance.disp_message(Frm_ImageWindow.Instance.WindowHandle, resultStr, "image", 12, 12, "green", "true");
                double row = Convert.ToDouble(Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[rowIndex].Cells[5].Value);
                double column = Convert.ToDouble(Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[rowIndex].Cells[6].Value);
                double angle = Convert.ToDouble(Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[rowIndex].Cells[3].Value);
                double drow, dcolumn;
                if (Frm_BarcodeTool.Instance.rdo_showArrow.Checked)
                {
                    if (0 <= angle && angle <= 90)
                    {
                        drow = Math.Abs(100 * Math.Sin(((HTuple)angle).TupleRad()));
                        dcolumn = Math.Abs(100 * Math.Cos(((HTuple)angle).TupleRad()));
                        HOperatorSet.DispArrow(Frm_ImageWindow.Instance.WindowHandle, row, column, row - drow, column + dcolumn, 2.0);

                    }
                    else if (angle > 90 && angle <= 180)
                    {
                        drow = 100 * Math.Sin(((HTuple)(180 - angle)).TupleRad());
                        dcolumn = 100 * Math.Cos(((HTuple)(180 - angle)).TupleRad());
                        HOperatorSet.DispArrow(Frm_ImageWindow.Instance.WindowHandle, row, column, row - drow, column - dcolumn, 2.0);
                    }
                    else if (angle < 0 && angle >= -90)
                    {
                        drow = 100 * Math.Sin(((HTuple)Math.Abs(angle)).TupleRad());
                        dcolumn = 100 * Math.Cos(((HTuple)Math.Abs(angle)).TupleRad());
                        HOperatorSet.DispArrow(Frm_ImageWindow.Instance.WindowHandle, row, column, row + drow, column + dcolumn, 2.0);
                    }
                    else if (angle < -90 && angle >= -180)
                    {
                        drow = Math.Abs(100 * Math.Sin(((HTuple)angle + 180).TupleRad()));
                        dcolumn = Math.Abs(100 * Math.Cos(((HTuple)angle + 180).TupleRad()));
                        HOperatorSet.DispArrow(Frm_ImageWindow.Instance.WindowHandle, row, column, row + drow, column - dcolumn, 2.0);
                    }
                }
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("blue"));
                if (SearchRegion != null)
                    HOperatorSet.DispObj(SearchRegion, Frm_ImageWindow.Instance.WindowHandle);
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="jobName">流程名</param>
        public override void Run(string jobName, bool updateImage, bool b)
        {
            try
            {
                runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败);
                if (inputImage == null)
                {
                    runStatu = ToolRunStatu.未指定输入图像;
                    return;
                }
                if (updateImage)
                    ShowImage(jobName ,inputImage);
                HTuple handleID;
                HOperatorSet.CreateBarCodeModel(new HTuple(), new HTuple(), out handleID);

                HObject region;
                HTuple resultStr;
                HTuple type;
                HTuple angle;
                HOperatorSet.FindBarCode(inputImage, out region, handleID, new HTuple("auto"), out resultStr);
                HOperatorSet.GetBarCodeResult(handleID, "all", "decoded_types", out type);
                HOperatorSet.GetBarCodeResult(handleID, "all", "orientation", out angle);

                L_result.Clear();
                for (int i = 0; i < resultStr.Length; i++)
                {
                    RunResult result = new RunResult();
                    result.ResultString = resultStr[i];
                    HOperatorSet.SelectObj(region, out  result.Region, i + 1);
                    HTuple area, row, column;
                    HOperatorSet.AreaCenter(result.Region, out  area, out  row, out  column);
                    result.Row = row;
                    result.Col = column;
                    result.BarcodeType = type;
                    HTuple angle1;
                    HOperatorSet.TupleSelect(angle, i, out angle1);
                    result.Angle = angle1;
                    L_result.Add(result);
                }

                HOperatorSet.ClearBarCodeModel(handleID);

                //显示
                SetColor(jobName, "green");
                SetDraw(jobName, "margin");
                D_stringAndRegion.Clear();
                Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows.Clear();
                for (int i = 0; i < L_result.Count; i++)
                {
                    Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows.Add();
                    Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[i].Cells[0].Value = L_result[i].ResultString;
                    Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[i].Cells[1].Value = L_result[i].ResultString.Length;
                    Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[i].Cells[2].Value = L_result[i].BarcodeType.ToString();
                    Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[i].Cells[3].Value = L_result[i].Row.ToString("0.00");
                    Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[i].Cells[4].Value = L_result[i].Col.ToString("0.00");
                    Frm_BarcodeTool.Instance.dgv_barcordFindResult.Rows[i].Cells[5].Value = L_result[i].Angle.ToString("0.00");

                    D_stringAndRegion.Add((i + 1).ToString(), L_result[i].Region);

                    //显示
                    Frm_ImageWindow.Instance.hwc_imageWindow.DispObj(L_result [i].Region ,"blue");
                    double drow = 0;
                    double dcolumn = 0;
                    if (0 <= L_result[i].Angle && L_result[i].Angle <= 90)
                    {
                        dcolumn = Math.Abs(100 * Math.Cos(((HTuple)L_result[i].Angle).TupleRad()));
                        drow = Math.Abs(100 * Math.Sin(((HTuple)L_result[i].Angle).TupleRad()));
                        HOperatorSet.DispArrow(Frm_ImageWindow.Instance.WindowHandle, L_result[i].Row, L_result[i].Col, L_result[i].Row - drow, L_result[i].Col + dcolumn, 1.0);
                    }
                    else if (L_result[i].Angle > 90 && L_result[i].Angle <= 180)
                    {
                        dcolumn = 100 * Math.Cos(((HTuple)(180 - L_result[i].Angle)).TupleRad());
                        drow = 100 * Math.Sin(((HTuple)(180 - L_result[i].Angle)).TupleRad());
                        HOperatorSet.DispArrow(Frm_ImageWindow.Instance.WindowHandle, L_result[i].Row, L_result[i].Col, L_result[i].Row - drow, L_result[i].Col - dcolumn, 1.0);
                    }
                    else if (L_result[i].Angle < 0 && L_result[i].Angle >= -90)
                    {
                        dcolumn = 100 * Math.Cos(((HTuple)Math.Abs(L_result[i].Angle)).TupleRad());
                        drow = 100 * Math.Sin(((HTuple)Math.Abs(L_result[i].Angle)).TupleRad());
                        HOperatorSet.DispArrow(Frm_ImageWindow.Instance.WindowHandle, L_result[i].Row, L_result[i].Col, L_result[i].Row + drow, L_result[i].Col + dcolumn, 1.0);
                    }
                    else if (L_result[i].Angle < -90 && L_result[i].Angle >= -180)
                    {
                        dcolumn = Math.Abs(100 * Math.Cos(((HTuple)L_result[i].Angle + 180).TupleRad()));
                        drow = Math.Abs(100 * Math.Sin(((HTuple)L_result[i].Angle + 180).TupleRad()));
                        HOperatorSet.DispArrow(Frm_ImageWindow.Instance.WindowHandle, L_result[i].Row, L_result[i].Col, L_result[i].Row + drow, L_result[i].Col - dcolumn, 1.0);
                    }
                    Frm_Main.Instance.disp_message(Frm_ImageWindow.Instance.WindowHandle, (HTuple)L_result[i].ResultString, "image", (HTuple)L_result[i].Row + 5, (HTuple)L_result[i].Col, "green", "true");
                }
                Frm_BarcodeTool.Instance.lbl_resultCount.Text = L_result.Count.ToString();
                if (SearchRegion != null)
                {
                    SetColor(jobName, "blue");
                    HOperatorSet.DispObj(SearchRegion, Frm_ImageWindow.Instance.WindowHandle);
                }

                if (L_result.Count > 0)
                {
                    outputStr = L_result[0].ResultString;
                }
                resultNum = L_result.Count;
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
