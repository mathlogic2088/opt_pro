using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using VersionMethods;
using System.Threading;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class FindCircleTool : ToolBase
    {

        /// <summary>
        /// 输入位姿
        /// </summary>
        internal PosXYU inputPose = new PosXYU();
        /// <summary>
        /// 期望圆圆心行坐标
        /// </summary>
        internal HTuple expectCircleRow = 300;
        /// <summary>
        /// 期望圆圆心列坐标
        /// </summary>
        internal HTuple expectCircleCol = 300;
        /// <summary>
        /// 期望圆半径
        /// </summary>
        internal HTuple expectCircleRadius = 200;
        /// <summary>
        /// 查找到圆的圆心行坐标
        /// </summary>
        private double _resultCircleRow = 0;
        internal double ResultCircleRow
        {
            get
            {
                return Math.Round(_resultCircleRow, 3);
            }
            set { _resultCircleRow = value; }
        }
        /// <summary>
        /// 查找到的圆的圆心列坐标
        /// </summary>
        private double _resultCircleCol = 0;
        internal double ResultCircleCol
        {
            get
            {
                return Math.Round(_resultCircleCol, 3);
            }
            set { _resultCircleCol = value; }
        }
        /// <summary>
        /// 查找到的圆的半径
        /// </summary>
        private double resultCircleRadius = 0;
        internal double ResultCircleRadius
        {
            get
            {
                return Math.Round(resultCircleRadius, 3);
            }
        }
        /// <summary>
        /// 起始角度
        /// </summary>
        internal double startAngle = 10;
        /// <summary>
        /// 结束角度
        /// </summary>
        internal double endAngle = 360;
        /// <summary>
        /// 运行工具时是否刷新输入图像
        /// </summary>
        internal bool updateImage = false;
        /// <summary>
        /// 圆环径向长度
        /// </summary>
        internal double ringRadiusLength = 80;
        /// <summary>
        /// 边阈值
        /// </summary>
        internal int threshold = 30;
        /// <summary>
        /// 卡尺
        /// </summary>
        internal HObject contours;
        /// <summary>
        /// 找边极性，从明到暗或从暗到明
        /// </summary>
        internal string polarity = "negative";
        /// <summary>
        /// 卡尺数量
        /// </summary>
        internal int cliperNum = 20;
        /// <summary>
        /// 输入图像
        /// </summary>
        internal HObject inputImage;
        /// <summary>
        /// 新的跟随姿态变化后的预期圆信息
        /// </summary>
        HTuple newExpecCircleRow = new HTuple(200), newExpectCircleCol = new HTuple(200), newExpectCircleRadius = new HTuple(200);
        /// <summary>
        /// 制作模板时的输入位姿
        /// </summary>
        internal PosXYU templatePose = new PosXYU();


        /// <summary>
        /// 刷新图像
        /// </summary>
        /// <param name="jobName">流程名</param>
        internal void UpdateImage(string jobName)
        {
            try
            {
                ClearWindow(jobName);
                ShowImage(jobName, inputImage);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 绘制预期圆
        /// </summary>
        /// <param name="jobName">流程名</param>
        public void DrawExpectCircle(string jobName)
        {
            try
            {
                UpdateImage(jobName);
                Frm_ImageWindow.Instance.hwc_imageWindow.Focus();
                Start_Draw_Mode(jobName);
                HOperatorSet.DrawCircleMod(Frm_ImageWindow.Instance.WindowHandle, newExpecCircleRow, newExpectCircleCol, newExpectCircleRadius, out expectCircleRow, out expectCircleCol, out expectCircleRadius);
                End_Draw_Mode(jobName);
                if (inputPose != null)
                {
                    templatePose.X  = inputPose.X ;
                    templatePose.Y  = inputPose.Y ;
                    templatePose.U  = inputPose.U ;
                }

                Frm_FindCircleTool.Instance.tbx_expectCircelRow.Text = expectCircleRow.TupleString("0.3f");
                Frm_FindCircleTool.Instance.tbx_expectCircleCol.Text = expectCircleCol.TupleString("0.3f");
                Frm_FindCircleTool.Instance.tbx_expectCircleradius.Text = expectCircleRadius.TupleString("0.3f");
                Run(jobName, false , false );
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 运行工具
        /// </summary>
        public override void Run(string jobName, bool updateImage, bool b)
        {
            try
            {
                runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败);
                if (inputImage == null)
                {
                    runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Input_Image : ToolRunStatu.无输入图像);
                    return;
                }
                if (updateImage)
                {
                    ClearWindow(jobName);
                    ShowImage(jobName, inputImage);
                }

                if (inputPose != null)
                {
                    HTuple Row = inputPose.X  - templatePose.X ;
                    HTuple Col = inputPose.Y  - templatePose.Y ;
                    HTuple angle = inputPose.U  - templatePose.U ;

                    HTuple _homMat2D;
                    HOperatorSet.HomMat2dIdentity(out _homMat2D);
                    HOperatorSet.HomMat2dRotate(_homMat2D, ((HTuple)(angle)).TupleRad(), (HTuple)templatePose.X , (HTuple)templatePose.Y , out _homMat2D);
                    HOperatorSet.HomMat2dTranslate(_homMat2D, (HTuple)(Row), (HTuple)(Col), out _homMat2D);

                    //对预期线的起始点做放射变换
                    HOperatorSet.AffineTransPixel(_homMat2D, (HTuple)expectCircleRow, (HTuple)expectCircleCol, out newExpecCircleRow, out newExpectCircleCol);
                }
                else
                {
                    newExpecCircleRow = expectCircleRow;
                    newExpectCircleCol = expectCircleCol;
                    newExpectCircleRadius = expectCircleRadius;
                }

                HTuple handleID;
                HOperatorSet.CreateMetrologyModel(out handleID);
                HTuple width, height;
                HOperatorSet.GetImageSize(inputImage, out width, out height);
                HOperatorSet.SetMetrologyModelImageSize(handleID, width[0], height[0]);
                HTuple index;
                HOperatorSet.AddMetrologyObjectCircleMeasure(handleID, newExpecCircleRow, newExpectCircleCol, expectCircleRadius, new HTuple(ringRadiusLength / 2), new HTuple(5), new HTuple(1), new HTuple(30), new HTuple(), new HTuple(), out index);

                //参数在这里设置
                HOperatorSet.SetMetrologyObjectParam(handleID, new HTuple("all"), new HTuple("measure_transition"), new HTuple(polarity));
                HOperatorSet.SetMetrologyObjectParam(handleID, new HTuple("all"), new HTuple("num_measures"), new HTuple(cliperNum));
                HOperatorSet.SetMetrologyObjectParam(handleID, new HTuple("all"), new HTuple("measure_length1"), new HTuple(ringRadiusLength));
                HOperatorSet.SetMetrologyObjectParam(handleID, new HTuple("all"), new HTuple("measure_threshold"), new HTuple(threshold));
                //////HOperatorSet.SetMetrologyObjectParam(handleID, new HTuple("all"), new HTuple("measure_select"), new HTuple(edgeSelect));
                //////HOperatorSet.SetMetrologyObjectParam(handleID, new HTuple("all"), new HTuple("min_score"), new HTuple(minScore));
                HOperatorSet.ApplyMetrologyModel(inputImage, handleID);

                //显示所有卡尺
                HTuple row, col;
                HOperatorSet.GetMetrologyObjectMeasures(out contours, handleID, new HTuple("all"), new HTuple("all"), out row, out col);
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("blue"));
                HOperatorSet.DispObj(contours, Frm_ImageWindow.Instance.WindowHandle);

                //显示指示找线方向的箭头
                HOperatorSet.DispArrow(Frm_ImageWindow.Instance.hwc_imageWindow.HWindowHalconID, newExpecCircleRow, newExpectCircleCol, newExpecCircleRow + 100, newExpectCircleCol + 100, 5.0);

                //把点显示出来
                HObject cross;
                HOperatorSet.GenCrossContourXld(out cross, row, col, new HTuple(20), new HTuple(0));
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("orange"));
                HOperatorSet.DispObj(cross, Frm_ImageWindow.Instance.WindowHandle);

                HTuple parameter;
                HObject circle;
                HOperatorSet.GetMetrologyObjectResult(handleID, new HTuple("all"), new HTuple("all"), new HTuple("result_type"), new HTuple("all_param"), out parameter);
                HOperatorSet.GetMetrologyObjectResultContour(out circle, handleID, new HTuple("all"), new HTuple("all"), new HTuple(1.5));

                if (parameter.Length >= 3)
                {
                    ResultCircleRow = parameter[0];
                    ResultCircleCol = parameter[1];
                 this .   resultCircleRadius = parameter[2];
                    Frm_FindCircleTool.Instance.tbx_resultCircleRow.Text = ResultCircleRow.ToString("0.000");
                    Frm_FindCircleTool.Instance.tbx_resultCircleCol.Text = ResultCircleCol.ToString("0.000");
                    Frm_FindCircleTool.Instance.tbx_resultCircleRadius.Text = resultCircleRadius.ToString("0.000");

                    //显示找到的圆
                    HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                    HOperatorSet.DispObj(circle, Frm_ImageWindow.Instance.WindowHandle);
                    HOperatorSet.ClearMetrologyModel(handleID);
                }
                runStatu = (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
