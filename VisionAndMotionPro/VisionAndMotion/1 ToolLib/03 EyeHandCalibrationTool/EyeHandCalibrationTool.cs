using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class EyeHandCalibrationTool : ToolBase
    {

        /// <summary>
        /// 标定数据
        /// </summary>
        internal List<List<string>> L_calibrationData = new List<List<string>>();
        /// <summary>
        /// 输入图像
        /// </summary>
        internal HObject inputImage;
        /// <summary>
        /// 输入点字符串
        /// </summary>
        internal string inputStr = null;
        internal PosXYU  inputPose = new PosXYU();
        /// <summary>
        /// 输出点字符串
        /// </summary>
        internal string outputStr = string.Empty;
        internal PosXYU outputPose = new PosXYU();
        /// <summary>
        /// 输出图像
        /// </summary>
        internal HObject outputImage;
        /// <summary>
        /// 放射变换矩阵
        /// </summary>
        internal HTuple homMat2D;
        /// <summary>
        /// X平移
        /// </summary>
        private HTuple _translateX = 0;
        internal HTuple TranslateX
        {
            get
            {
                _translateX = Math.Round((double)_translateX, 3);
                return _translateX;
            }
            set
            {
                value = Math.Round((double)value, 3);
                _translateX = value;
            }
        }
        /// <summary>
        /// Y平移
        /// </summary>
        private HTuple _translateY = 0;
        internal HTuple TranslateY
        {
            get
            {
                _translateY = Math.Round((double)_translateY, 3);
                return _translateY;
            }
            set
            {
                value = Math.Round((double)value, 3);
                _translateY = value;
            }
        }
        /// <summary>
        /// X缩放
        /// </summary>
        private HTuple _scanX = 1;
        internal HTuple ScanX
        {
            get
            {
                _scanX = Math.Round((double)_scanX, 3);
                return _scanX;
            }
            set
            {
                value = Math.Round((double)value, 3);
                _scanX = value;
            }
        }
        /// <summary>
        /// Y缩放
        /// </summary>
        private HTuple _scanY = 1;
        internal HTuple ScanY
        {
            get
            {
                _scanY = Math.Round((double)_scanY, 3);
                return _scanY;
            }
            set
            {
                value = Math.Round((double)value, 3);
                _scanY = value;
            }
        }
        /// <summary>
        /// 角度旋转
        /// </summary>
        private HTuple _rotation = 0;
        internal HTuple Rotation
        {
            get
            {
                _rotation = Math.Round((double)_rotation, 3);
                return _rotation;
            }
            set
            {
                value = Math.Round((double)value, 3);
                _rotation = value;
            }
        }
        /// <summary>
        /// 轴斜切
        /// </summary>
        private HTuple _theta = 0;
        internal HTuple Theta
        {
            get
            {
                _theta = Math.Round((double)_theta, 3);
                return _theta;
            }
            set
            {
                value = Math.Round((double)value, 3);
                _theta = value;
            }
        }
        /// <summary>
        /// 标定类型 四点标定|九点标定
        /// </summary>
        internal CalibrationType calibrationType = CalibrationType.Four_Point;


        /// <summary>
        /// 格式化Double类型的数据，格式化为指定8位的格式，如-0.12格式化为：-000.120
        /// </summary>
        /// <param name="d"></param>
        internal static string Format_Htuple_To_Str(HTuple htuple)
        {
            try
            {
                return Convert.ToDouble(htuple.ToString()) >= 0 ? "+" + Convert.ToDouble(htuple.ToString()).ToString("0000.000") : Convert.ToDouble(htuple.ToString()).ToString("0000.000");
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// 将工具恢复到初始状态
        /// </summary>
        internal void ResetTool()
        {
            try
            {
                TranslateX = 0;
                TranslateY = 0;
                ScanX = 1;
                ScanY = 1;
                Rotation = 0;
                Theta = 0;
                Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows.Clear();
                Frm_EyeHandCalibrationTool.Instance.tbx_translateX.Text = "0";
                Frm_EyeHandCalibrationTool.Instance.tbx_translateY.Text = "0";
                Frm_EyeHandCalibrationTool.Instance.tbx_scaleX.Text = "1";
                Frm_EyeHandCalibrationTool.Instance.tbx_scaleY.Text = "1";
                Frm_EyeHandCalibrationTool.Instance.tbx_rotation.Text = "0";
                Frm_EyeHandCalibrationTool.Instance.tbx_theta.Text = "0";
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 标定
        /// </summary>
        internal void Calibrate()
        {
            try
            {
                List<double> L_pixelPosX = new List<double>();
                List<double> L_pixelPosY = new List<double>();
                List<double> L_MechanicalPosX = new List<double>();
                List<double> L_MechanicalPosY = new List<double>();
                try
                {
                    for (int i = 0; i < Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows.Count; i++)
                    {
                        L_pixelPosX.Add(Convert.ToDouble(Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows[i].Cells[0].Value.ToString()));
                        L_pixelPosY.Add(Convert.ToDouble(Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows[i].Cells[1].Value.ToString()));
                        L_MechanicalPosX.Add(Convert.ToDouble(Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows[i].Cells[2].Value.ToString()));
                        L_MechanicalPosY.Add(Convert.ToDouble(Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows[i].Cells[3].Value.ToString()));
                    }
                }
                catch
                {
                    Frm_Main.Instance.OutputMsg("标定失败，标定数据不合法（错误代码：0301）", Color.Red);
                    return;
                }

                try
                {
                    if (calibrationType == CalibrationType.Four_Point)
                    {
                        HOperatorSet.VectorToHomMat2d(new HTuple((HTuple)L_pixelPosX[0]).TupleConcat((HTuple)L_pixelPosX[1]).TupleConcat((HTuple)L_pixelPosX[2]).TupleConcat((HTuple)L_pixelPosX[3]),
                                                      new HTuple((HTuple)L_pixelPosY[0]).TupleConcat((HTuple)L_pixelPosY[1]).TupleConcat((HTuple)L_pixelPosY[2]).TupleConcat((HTuple)L_pixelPosY[3]),
                                                      new HTuple((HTuple)L_MechanicalPosX[0]).TupleConcat((HTuple)L_MechanicalPosX[1]).TupleConcat((HTuple)L_MechanicalPosX[2]).TupleConcat((HTuple)L_MechanicalPosX[3]),
                                                      new HTuple((HTuple)L_MechanicalPosY[0]).TupleConcat((HTuple)L_MechanicalPosY[1]).TupleConcat((HTuple)L_MechanicalPosY[2]).TupleConcat((HTuple)L_MechanicalPosY[3]),
                                                      out homMat2D);
                    }
                    else
                    {
                        HOperatorSet.VectorToHomMat2d(new HTuple((HTuple)L_pixelPosX[0]).TupleConcat((HTuple)L_pixelPosX[1]).TupleConcat((HTuple)L_pixelPosX[2]).TupleConcat((HTuple)L_pixelPosX[3]).TupleConcat((HTuple)L_pixelPosX[4]).TupleConcat((HTuple)L_pixelPosX[5]).TupleConcat((HTuple)L_pixelPosX[6]).TupleConcat((HTuple)L_pixelPosX[7]),
                                                          new HTuple((HTuple)L_pixelPosY[0]).TupleConcat((HTuple)L_pixelPosY[1]).TupleConcat((HTuple)L_pixelPosY[2]).TupleConcat((HTuple)L_pixelPosY[3]).TupleConcat((HTuple)L_pixelPosY[4]).TupleConcat((HTuple)L_pixelPosY[5]).TupleConcat((HTuple)L_pixelPosY[6]).TupleConcat((HTuple)L_pixelPosY[7]),
                                                          new HTuple((HTuple)L_MechanicalPosX[0]).TupleConcat((HTuple)L_MechanicalPosX[1]).TupleConcat((HTuple)L_MechanicalPosX[2]).TupleConcat((HTuple)L_MechanicalPosX[3]).TupleConcat((HTuple)L_MechanicalPosX[4]).TupleConcat((HTuple)L_MechanicalPosX[5]).TupleConcat((HTuple)L_MechanicalPosX[6]).TupleConcat((HTuple)L_MechanicalPosX[7]),
                                                          new HTuple((HTuple)L_MechanicalPosY[0]).TupleConcat((HTuple)L_MechanicalPosY[1]).TupleConcat((HTuple)L_MechanicalPosY[2]).TupleConcat((HTuple)L_MechanicalPosY[3]).TupleConcat((HTuple)L_MechanicalPosY[4]).TupleConcat((HTuple)L_MechanicalPosY[5]).TupleConcat((HTuple)L_MechanicalPosY[6]).TupleConcat((HTuple)L_MechanicalPosY[7]),
                                                          out homMat2D);
                    }
                }
                catch
                {
                    Frm_Main.Instance.OutputMsg("标定失败，标定数据无法确定仿射变换关系（错误代码：0302）", Color.Red);
                }

                HOperatorSet.HomMat2dToAffinePar(homMat2D, out _scanX, out _scanY, out _rotation, out _theta, out _translateX, out _translateY);
                Frm_EyeHandCalibrationTool.Instance.tbx_scaleX.Text = (Convert.ToDouble(ScanX.ToString())).ToString("0.000");
                Frm_EyeHandCalibrationTool.Instance.tbx_scaleY.Text = (Convert.ToDouble(ScanY.ToString())).ToString("0.000");
                Frm_EyeHandCalibrationTool.Instance.tbx_rotation.Text = (Convert.ToDouble(Rotation.ToString())).ToString("0.000");
                Frm_EyeHandCalibrationTool.Instance.tbx_theta.Text = (Convert.ToDouble(Theta.ToString())).ToString("0.000");
                Frm_EyeHandCalibrationTool.Instance.tbx_translateX.Text = (Convert.ToDouble(TranslateX.ToString())).ToString("0.000");
                Frm_EyeHandCalibrationTool.Instance.tbx_translateY.Text = (Convert.ToDouble(TranslateY.ToString())).ToString("0.000");

                //保存标定数据
                L_calibrationData.Clear();
                for (int i = 0; i < Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows.Count; i++)
                {
                    List<string> list = new List<string>();
                    for (int j = 0; j < Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows[i].Cells.Count; j++)
                    {
                        list.Add(Math.Round(Convert.ToDouble(Frm_EyeHandCalibrationTool.Instance.dgv_calibrateData.Rows[i].Cells[j].Value), 3).ToString());
                    }
                    L_calibrationData.Add(list);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 工具运行
        /// </summary>
        public override void Run(string jobName, bool updateImage, bool b)
        {
            try
            {
                runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败);
                if (inputImage == null && inputStr == null&&inputPose ==null )
                {
                    runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Source : ToolRunStatu.未指定输入源);
                    return;
                }

                HOperatorSet.HomMat2dIdentity(out homMat2D);
                HOperatorSet.HomMat2dTranslate(homMat2D, TranslateX, TranslateY, out homMat2D);
                HOperatorSet.HomMat2dRotate(homMat2D, (Rotation).TupleRad(), (HTuple)0, (HTuple)0, out homMat2D);
                HOperatorSet.HomMat2dScale(homMat2D, (HTuple)ScanX, (HTuple)ScanY, (HTuple)0, (HTuple)0, out homMat2D);
                HOperatorSet.HomMat2dSlant(homMat2D, (HTuple)Theta, (HTuple)"y", (HTuple)0, (HTuple)0, out homMat2D);

                //对图像进行仿射变换
                if (inputImage != null)
                {
                    HOperatorSet.AffineTransImage(inputImage, out outputImage, homMat2D, (HTuple)"nearest_neighbor", (HTuple)"false");
                    if (updateImage)
                    {
                        HOperatorSet.DispObj(outputImage, GetWindowHandle(jobName));
                    }
                }

                //对点字符串进行放射变换
                if (inputStr != null)
                {
                    //将点字符串分割出来，然后对每一个点进行放射变换，然后再连接成字符串
                    string pointStr = inputStr;
                    int pointCount = Convert.ToInt16(Regex.Split(inputStr, ",")[0]);
                    string resultStr;
                    if (pointCount == 0)
                    {
                        resultStr = "0,+0000.000,+0000.000,+0000.000";
                    }
                    else
                    {
                        resultStr = pointCount.ToString();
                        for (int i = 0; i < pointCount; i++)
                        {
                            double row = Convert.ToDouble(Regex.Split(inputStr, ",")[3 * i + 1]);
                            double col = Convert.ToDouble(Regex.Split(inputStr, ",")[3 * i + 2]);
                            double angle = Convert.ToDouble(Regex.Split(inputStr, ",")[3 * i + 3]);
                            HTuple rowAfterTrans;
                            HTuple colAfterTrans;
                            HTuple angleAfterTrans;
                            HOperatorSet.AffineTransPixel(homMat2D, (HTuple)row, (HTuple)col, out rowAfterTrans, out colAfterTrans);
                            angleAfterTrans = angle;
                            resultStr += "," + Format_Htuple_To_Str(rowAfterTrans) + "," + Format_Htuple_To_Str(colAfterTrans) + "," + Format_Htuple_To_Str(angleAfterTrans);
                        }
                    }
                    outputStr = resultStr;
                }

                //对点字符串进行放射变换
                if (inputPose != null)
                {
                    HTuple rowAfterTrans;
                    HTuple colAfterTrans;
                    HTuple angleAfterTrans;
                    HOperatorSet.AffineTransPixel(homMat2D, (HTuple)inputPose.X , (HTuple)inputPose.Y , out rowAfterTrans, out colAfterTrans);
                    angleAfterTrans = inputPose.U ;
                    outputPose.X  = rowAfterTrans;
                    outputPose.Y  = colAfterTrans;
                    outputPose.U  = angleAfterTrans;
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
