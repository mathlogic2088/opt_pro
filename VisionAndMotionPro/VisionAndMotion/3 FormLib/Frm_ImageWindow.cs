﻿using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ViewWindow.Model;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionAndMotionPro
{
    public partial class Frm_ImageWindow : DockContent
    {
        public Frm_ImageWindow()
        {
            InitializeComponent();

            Init_Language();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_ImageWindow _instance;
        public static Frm_ImageWindow Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ImageWindow();
                return _instance;
            }
        }
        /// <summary>
        /// 窗体中当前显示的图像
        /// </summary>
        internal static HObject currentImage;
        /// <summary>
        /// 图像控件句柄
        /// </summary>
        public HTuple WindowHandle
        {
            get
            {
                return this.hwc_imageWindow.HWindowHalconID;
            }
        }
        /// <summary>
        /// 是否处于全屏状态
        /// </summary>
        public bool fullScreen = false;
        /// <summary>
        /// 锁
        /// </summary>
        private object obj = new object();


        /// <summary>
        /// 初始化语言
        /// </summary>
        private void Init_Language()
        {
            try
            {
                if (Configuration.language == Language.English)
                {
                    this.Text = "Image Window";
                    运行流程ToolStripMenuItem.Text = "Run Job";
                    图像适应窗体ToolStripMenuItem.Text = "Image Adapt Window";
                    区域填充方式ToolStripMenuItem.Text = "Draw Mode";
                    填充ToolStripMenuItem.Text = "Fill";
                    轮廓ToolStripMenuItem.Text = "Margin";
                    绘制ROIToolStripMenuItem.Text = "Draw ROI";
                    圆ToolStripMenuItem.Text = "Circle";
                    矩形ToolStripMenuItem.Text = "Rectangle1";
                    仿射矩形ToolStripMenuItem.Text = "Rectangle2";
                    椭圆ToolStripMenuItem.Text = "Ellipse";
                    任意形状ToolStripMenuItem.Text = "Any";
                    线ToolStripMenuItem.Text = "Line";
                    标记点ToolStripMenuItem.Text = "Mark";
                    打开图片ToolStripMenuItem.Text = "Open Image";
                    清除静态图像ToolStripMenuItem.Text = "Clear Statc Iamge";
                    清除原始图像ToolStripMenuItem.Text = "Clear Orgion Image";
                    全屏显示ToolStripMenuItem.Text = "Full Screen Display";
                    图像另存为ToolStripMenuItem.Text = "Save Image ";
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 更新最后一次运行下拉框
        /// </summary>
        internal void Update_Last_Run_Result_Image_List()
        {
            try
            {
                Frm_ImageWindow.Instance.cbx_toolRunResultImageList.Items.Clear();
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count < 1)
                    return;
                Job job = Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text);
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    if (job.L_toolList[i].toolType != ToolType.Output)
                    {
                        Frm_ImageWindow.Instance.cbx_toolRunResultImageList.Items.Add(job.L_toolList[i].toolName);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 显示图像
        /// </summary>
        /// <param name="image"></param>
        internal void Display_Image(HObject image, HTuple windowHandle)
        {
            try
            {
                lock (obj)
                {
                    HTuple width, height;
                    HOperatorSet.GetImageSize(image, out height, out width);
                    if (height.Length > 1)
                    {
                        height = height[0];
                        width = width[0];
                    }

                    //有开启全屏
                    if (fullScreen)
                    {
                        HOperatorSet.SetPart(Frm_FullScreen.Instance.windowHandle, 0, 0, width - 1, height - 1);
                        HOperatorSet.DispObj(image, Frm_FullScreen.Instance.windowHandle);

                        //此处根据图像分辨率大小设置显示字体的大小，只是为了好看而已
                        if (width < 1000)
                        {
                            set_display_font(Frm_FullScreen.Instance.windowHandle, new HTuple(12), "nomo", "false", "false");
                        }
                        else if (width < 2000)
                        {
                            set_display_font(Frm_FullScreen.Instance.windowHandle, new HTuple(12), "nomo", "false", "false");
                        }
                        else if (width < 3000)
                        {
                            set_display_font(Frm_FullScreen.Instance.windowHandle, new HTuple(15), "nomo", "false", "false");
                        }
                        else
                        {
                            set_display_font(Frm_FullScreen.Instance.windowHandle, new HTuple(18), "nomo", "false", "false");
                        }
                        disp_message(Frm_FullScreen.Instance.windowHandle, "ESC 退出全屏模式", "image", 2, 2, "green", "false");
                    }
                    else
                    {
                        // HOperatorSet.SetPart(windowHandle, 0, 0, new HTuple(width - 1), new HTuple(height - 1));
                        //HOperatorSet.DispObj(image, windowHandle);
                        hwc_imageWindow.HobjectToHimage(image);
                    }
                    currentImage = image;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 显示图像
        /// </summary>
        /// <param name="image"></param>
        internal void Display_Image(HObject image)
        {
            try
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out height, out width);
                if (height.Length > 1)
                {
                    height = height[0];
                    width = width[0];
                }

                //有开启全屏
                if (fullScreen)
                {
                    HOperatorSet.SetPart(Frm_FullScreen.Instance.windowHandle, 0, 0, width - 1, height - 1);
                    HOperatorSet.DispObj(image, Frm_FullScreen.Instance.windowHandle);

                    //此处根据图像分辨率大小设置显示字体的大小，只是为了好看而已
                    if (width < 1000)
                    {
                        set_display_font(Frm_FullScreen.Instance.windowHandle, new HTuple(12), "nomo", "false", "false");
                    }
                    else if (width < 2000)
                    {
                        set_display_font(Frm_FullScreen.Instance.windowHandle, new HTuple(12), "nomo", "false", "false");
                    }
                    else if (width < 3000)
                    {
                        set_display_font(Frm_FullScreen.Instance.windowHandle, new HTuple(15), "nomo", "false", "false");
                    }
                    else
                    {
                        set_display_font(Frm_FullScreen.Instance.windowHandle, new HTuple(18), "nomo", "false", "false");
                    }
                    disp_message(Frm_FullScreen.Instance.windowHandle, "ESC 退出全屏模式", "image", 2, 2, "green", "false");
                }
                else
                {
                    HOperatorSet.SetPart(Frm_ImageWindow.Instance.WindowHandle, 0, 0, width - 1, height - 1);
                    try
                    {
                        // HOperatorSet.SetPart(Frm_ImageWindow.Instance.WindowHandle, row1, col1, row2, col2);
                        HOperatorSet.DispObj(image, Frm_ImageWindow.Instance.WindowHandle);
                    }
                    catch { }
                }

                currentImage = image;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 在图像中显示字符串
        /// </summary>
        internal void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem, HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {
            try
            {
                HTuple hv_M = null, hv_N = null, hv_Red = null;
                HTuple hv_Green = null, hv_Blue = null, hv_RowI1Part = null;
                HTuple hv_ColumnI1Part = null, hv_RowI2Part = null, hv_ColumnI2Part = null;
                HTuple hv_RowIWin = null, hv_ColumnIWin = null, hv_WidthWin = null;
                HTuple hv_HeightWin = null, hv_I = null, hv_RowI = new HTuple();
                HTuple hv_ColumnI = new HTuple(), hv_StringI = new HTuple();
                HTuple hv_MaxAscent = new HTuple(), hv_MaxDescent = new HTuple();
                HTuple hv_MaxWidth = new HTuple(), hv_MaxHeight = new HTuple();
                HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRowI = new HTuple();
                HTuple hv_FactorColumnI = new HTuple(), hv_UseShadow = new HTuple();
                HTuple hv_ShadowColor = new HTuple(), hv_Exception = new HTuple();
                HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
                HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
                HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
                HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
                HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
                HTuple hv_CurrentColor = new HTuple();
                HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
                HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
                HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
                HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
                HTuple hv_String_COPY_INP_TMP = hv_String.Clone();
                if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                {
                    hv_Color_COPY_INP_TMP = "";
                }
                if ((int)(new HTuple(hv_Box_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                {
                    hv_Box_COPY_INP_TMP = "false";
                }
                hv_M = (new HTuple(hv_Row_COPY_INP_TMP.TupleLength())) * (new HTuple(hv_Column_COPY_INP_TMP.TupleLength()
                    ));
                hv_N = new HTuple(hv_Row_COPY_INP_TMP.TupleLength());
                if ((int)((new HTuple(hv_M.TupleEqual(0))).TupleOr(new HTuple(hv_String_COPY_INP_TMP.TupleEqual(
                    new HTuple())))) != 0)
                {
                    return;
                }
                if ((int)(new HTuple(hv_M.TupleNotEqual(1))) != 0)
                {
                    if ((int)(new HTuple((new HTuple(hv_Row_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        hv_N = new HTuple(hv_Column_COPY_INP_TMP.TupleLength());
                        HOperatorSet.TupleGenConst(hv_N, hv_Row_COPY_INP_TMP, out hv_Row_COPY_INP_TMP);
                    }
                    else if ((int)(new HTuple((new HTuple(hv_Column_COPY_INP_TMP.TupleLength()
                        )).TupleEqual(1))) != 0)
                    {
                        HOperatorSet.TupleGenConst(hv_N, hv_Column_COPY_INP_TMP, out hv_Column_COPY_INP_TMP);
                    }
                    else if ((int)(new HTuple((new HTuple(hv_Column_COPY_INP_TMP.TupleLength()
                        )).TupleNotEqual(new HTuple(hv_Row_COPY_INP_TMP.TupleLength())))) != 0)
                    {
                        throw new HalconException("Number of elements in Row and Column does not match.");
                    }
                    if ((int)(new HTuple((new HTuple(hv_String_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        HOperatorSet.TupleGenConst(hv_N, hv_String_COPY_INP_TMP, out hv_String_COPY_INP_TMP);
                    }
                    else if ((int)(new HTuple((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                        )).TupleNotEqual(hv_N))) != 0)
                    {
                        throw new HalconException("Number of elements in Strings does not match number of positions.");
                    }
                }
                HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
                HOperatorSet.GetPart(hv_WindowHandle, out hv_RowI1Part, out hv_ColumnI1Part,
                    out hv_RowI2Part, out hv_ColumnI2Part);
                HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowIWin, out hv_ColumnIWin,
                    out hv_WidthWin, out hv_HeightWin);
                HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
                HTuple end_val89 = hv_N - 1;
                HTuple step_val89 = 1;
                for (hv_I = 0; hv_I.Continue(end_val89, step_val89); hv_I = hv_I.TupleAdd(step_val89))
                {
                    hv_RowI = hv_Row_COPY_INP_TMP.TupleSelect(hv_I);
                    hv_ColumnI = hv_Column_COPY_INP_TMP.TupleSelect(hv_I);
                    if ((int)(new HTuple(hv_N.TupleEqual(1))) != 0)
                    {
                        hv_StringI = hv_String_COPY_INP_TMP.Clone();
                    }
                    else
                    {
                        hv_StringI = hv_String_COPY_INP_TMP.TupleSelect(hv_I);
                    }
                    if ((int)(new HTuple(hv_RowI.TupleEqual(-1))) != 0)
                    {
                        hv_RowI = 12;
                    }
                    if ((int)(new HTuple(hv_ColumnI.TupleEqual(-1))) != 0)
                    {
                        hv_ColumnI = 12;
                    }
                    hv_StringI = ((("" + hv_StringI) + "")).TupleSplit("\n");
                    HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                        out hv_MaxWidth, out hv_MaxHeight);
                    if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
                    {
                        hv_R1 = hv_RowI.Clone();
                        hv_C1 = hv_ColumnI.Clone();
                    }
                    else
                    {
                        hv_FactorRowI = (1.0 * hv_HeightWin) / ((hv_RowI2Part - hv_RowI1Part) + 1);
                        hv_FactorColumnI = (1.0 * hv_WidthWin) / ((hv_ColumnI2Part - hv_ColumnI1Part) + 1);
                        hv_R1 = (((hv_RowI - hv_RowI1Part) + 0.5) * hv_FactorRowI) - 0.5;
                        hv_C1 = (((hv_ColumnI - hv_ColumnI1Part) + 0.5) * hv_FactorColumnI) - 0.5;
                    }
                    hv_UseShadow = 1;
                    hv_ShadowColor = "gray";
                    if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
                    {
                        if (hv_Box_COPY_INP_TMP == null)
                            hv_Box_COPY_INP_TMP = new HTuple();
                        hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                        hv_ShadowColor = "#f28d26";
                    }
                    if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                        1))) != 0)
                    {
                        if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                        {
                        }
                        else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                            "false"))) != 0)
                        {
                            hv_UseShadow = 0;
                        }
                        else
                        {
                            hv_ShadowColor = hv_Box_COPY_INP_TMP.TupleSelect(1);
                            try
                            {
                                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                                    1));
                            }
                            catch (HalconException HDevExpDefaultException1)
                            {
                                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                                hv_Exception = new HTuple("Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)");
                                throw new HalconException(hv_Exception);
                            }
                        }
                    }
                    if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
                    {
                        try
                        {
                            HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                                0));
                        }
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            hv_Exception = new HTuple("Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)");
                            throw new HalconException(hv_Exception);
                        }
                        hv_StringI = (" " + hv_StringI) + " ";
                        hv_Width = new HTuple();
                        for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_StringI.TupleLength()
                            )) - 1); hv_Index = (int)hv_Index + 1)
                        {
                            HOperatorSet.GetStringExtents(hv_WindowHandle, hv_StringI.TupleSelect(hv_Index),
                                out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                            hv_Width = hv_Width.TupleConcat(hv_W);
                        }
                        hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_StringI.TupleLength()));
                        hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                        hv_R2 = hv_R1 + hv_FrameHeight;
                        hv_C2 = hv_C1 + hv_FrameWidth;
                        HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                        HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                        HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                        if ((int)(hv_UseShadow) != 0)
                        {
                            HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1,
                                hv_C2 + 1);
                        }
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                        HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                        HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
                    }
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_StringI.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        if ((int)(new HTuple(hv_N.TupleEqual(1))) != 0)
                        {
                            hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                                )));
                        }
                        else
                        {
                            hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_I % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                                )));
                        }
                        if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                            "auto")))) != 0)
                        {
                            try
                            {
                                HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                            }
                            catch (HalconException HDevExpDefaultException1)
                            {
                                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                                hv_Exception = ((("Wrong value of control parameter Color[" + (hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                                    )))) + "] == '") + hv_CurrentColor) + "' (must be a valid color string)";
                                throw new HalconException(hv_Exception);
                            }
                        }
                        else
                        {
                            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                        }
                        hv_RowI = hv_R1 + (hv_MaxHeight * hv_Index);
                        HOperatorSet.SetTposition(hv_WindowHandle, hv_RowI, hv_C1);
                        HOperatorSet.WriteString(hv_WindowHandle, hv_StringI.TupleSelect(hv_Index));
                    }
                }
                HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                HOperatorSet.SetPart(hv_WindowHandle, hv_RowI1Part, hv_ColumnI1Part, hv_RowI2Part,
                    hv_ColumnI2Part);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 设置显示字体
        /// </summary>
        internal void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font, HTuple hv_Bold, HTuple hv_Slant)
        {
            try
            {
                HTuple hv_OS = null, hv_BufferWindowHandle = new HTuple();
                HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
                HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
                HTuple hv_Scale = new HTuple(), hv_Exception = new HTuple();
                HTuple hv_SubFamily = new HTuple(), hv_Fonts = new HTuple();
                HTuple hv_SystemFonts = new HTuple(), hv_Guess = new HTuple();
                HTuple hv_I = new HTuple(), hv_Index = new HTuple(), hv_AllowedFontSizes = new HTuple();
                HTuple hv_Distances = new HTuple(), hv_Indices = new HTuple();
                HTuple hv_FontSelRegexp = new HTuple(), hv_FontsCourier = new HTuple();
                HTuple hv_Bold_COPY_INP_TMP = hv_Bold.Clone();
                HTuple hv_Font_COPY_INP_TMP = hv_Font.Clone();
                HTuple hv_Size_COPY_INP_TMP = hv_Size.Clone();
                HTuple hv_Slant_COPY_INP_TMP = hv_Slant.Clone();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
                {
                    hv_Size_COPY_INP_TMP = 16;
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    try
                    {
                        HOperatorSet.OpenWindow(0, 0, 256, 256, 0, "buffer", "", out hv_BufferWindowHandle);
                        HOperatorSet.SetFont(hv_BufferWindowHandle, "-Consolas-16-*-0-*-*-1-");
                        HOperatorSet.GetStringExtents(hv_BufferWindowHandle, "test_string", out hv_Ascent,
                            out hv_Descent, out hv_Width, out hv_Height);
                        hv_Scale = 110.0 / hv_Width;
                        hv_Size_COPY_INP_TMP = ((hv_Size_COPY_INP_TMP * hv_Scale)).TupleInt();
                        HOperatorSet.CloseWindow(hv_BufferWindowHandle);
                    }
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    }
                    if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))).TupleOr(
                        new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Courier New";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Consolas";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Arial";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "Times New Roman";
                    }
                    if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = 1;
                    }
                    else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = 0;
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Bold";
                        throw new HalconException(hv_Exception);
                    }
                    if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = 1;
                    }
                    else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = 0;
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Slant";
                        throw new HalconException(hv_Exception);
                    }
                    try
                    {
                        HOperatorSet.SetFont(hv_WindowHandle, ((((((("-" + hv_Font_COPY_INP_TMP) + "-") + hv_Size_COPY_INP_TMP) + "-*-") + hv_Slant_COPY_INP_TMP) + "-*-*-") + hv_Bold_COPY_INP_TMP) + "-");
                    }
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    }
                }
                else if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Dar"))) != 0)
                {
                    hv_SubFamily = 0;
                    if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_SubFamily = hv_SubFamily.TupleBor(1);
                    }
                    else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                    {
                        hv_Exception = "Wrong value of control parameter Slant";
                        throw new HalconException(hv_Exception);
                    }
                    if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_SubFamily = hv_SubFamily.TupleBor(2);
                    }
                    else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                    {
                        hv_Exception = "Wrong value of control parameter Bold";
                        throw new HalconException(hv_Exception);
                    }
                    if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                    {
                        hv_Fonts = new HTuple();
                        hv_Fonts[0] = "Menlo-Regular";
                        hv_Fonts[1] = "Menlo-Italic";
                        hv_Fonts[2] = "Menlo-Bold";
                        hv_Fonts[3] = "Menlo-BoldItalic";
                    }
                    else if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))).TupleOr(
                        new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                    {
                        hv_Fonts = new HTuple();
                        hv_Fonts[0] = "CourierNewPSMT";
                        hv_Fonts[1] = "CourierNewPS-ItalicMT";
                        hv_Fonts[2] = "CourierNewPS-BoldMT";
                        hv_Fonts[3] = "CourierNewPS-BoldItalicMT";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                    {
                        hv_Fonts = new HTuple();
                        hv_Fonts[0] = "ArialMT";
                        hv_Fonts[1] = "Arial-ItalicMT";
                        hv_Fonts[2] = "Arial-BoldMT";
                        hv_Fonts[3] = "Arial-BoldItalicMT";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                    {
                        hv_Fonts = new HTuple();
                        hv_Fonts[0] = "TimesNewRomanPSMT";
                        hv_Fonts[1] = "TimesNewRomanPS-ItalicMT";
                        hv_Fonts[2] = "TimesNewRomanPS-BoldMT";
                        hv_Fonts[3] = "TimesNewRomanPS-BoldItalicMT";
                    }
                    else
                    {
                        HOperatorSet.QueryFont(hv_WindowHandle, out hv_SystemFonts);
                        hv_Fonts = new HTuple();
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Guess = new HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP);
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Regular");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "MT");
                        for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HTuple();
                                hv_Fonts[0] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                        hv_Guess = new HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Italic");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-ItalicMT");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Oblique");
                        for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HTuple();
                                hv_Fonts[1] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                        hv_Guess = new HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Bold");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldMT");
                        for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HTuple();
                                hv_Fonts[2] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                        hv_Guess = new HTuple();
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalic");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalicMT");
                        hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldOblique");
                        for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                        {
                            HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                            if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                            {
                                if (hv_Fonts == null)
                                    hv_Fonts = new HTuple();
                                hv_Fonts[3] = hv_Guess.TupleSelect(hv_I);
                                break;
                            }
                        }
                    }
                    hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(hv_SubFamily);
                    try
                    {
                        HOperatorSet.SetFont(hv_WindowHandle, (hv_Font_COPY_INP_TMP + "-") + hv_Size_COPY_INP_TMP);
                    }
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    }
                }
                else
                {
                    hv_Size_COPY_INP_TMP = hv_Size_COPY_INP_TMP * 1.25;
                    hv_AllowedFontSizes = new HTuple();
                    hv_AllowedFontSizes[0] = 11;
                    hv_AllowedFontSizes[1] = 14;
                    hv_AllowedFontSizes[2] = 17;
                    hv_AllowedFontSizes[3] = 20;
                    hv_AllowedFontSizes[4] = 25;
                    hv_AllowedFontSizes[5] = 34;
                    if ((int)(new HTuple(((hv_AllowedFontSizes.TupleFind(hv_Size_COPY_INP_TMP))).TupleEqual(
                        -1))) != 0)
                    {
                        hv_Distances = ((hv_AllowedFontSizes - hv_Size_COPY_INP_TMP)).TupleAbs();
                        HOperatorSet.TupleSortIndex(hv_Distances, out hv_Indices);
                        hv_Size_COPY_INP_TMP = hv_AllowedFontSizes.TupleSelect(hv_Indices.TupleSelect(
                            0));
                    }
                    if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))).TupleOr(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(
                        "Courier")))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "courier";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "helvetica";
                    }
                    else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = "times";
                    }
                    if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = "bold";
                    }
                    else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Bold_COPY_INP_TMP = "medium";
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Bold";
                        throw new HalconException(hv_Exception);
                    }
                    if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                    {
                        if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("times"))) != 0)
                        {
                            hv_Slant_COPY_INP_TMP = "i";
                        }
                        else
                        {
                            hv_Slant_COPY_INP_TMP = "o";
                        }
                    }
                    else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = "r";
                    }
                    else
                    {
                        hv_Exception = "Wrong value of control parameter Slant";
                        throw new HalconException(hv_Exception);
                    }
                    try
                    {
                        HOperatorSet.SetFont(hv_WindowHandle, ((((((("-adobe-" + hv_Font_COPY_INP_TMP) + "-") + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                    }
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        if ((int)((new HTuple(((hv_OS.TupleSubstr(0, 4))).TupleEqual("Linux"))).TupleAnd(
                            new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                        {
                            HOperatorSet.QueryFont(hv_WindowHandle, out hv_Fonts);
                            hv_FontSelRegexp = (("^-[^-]*-[^-]*[Cc]ourier[^-]*-" + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP;
                            hv_FontsCourier = ((hv_Fonts.TupleRegexpSelect(hv_FontSelRegexp))).TupleRegexpMatch(
                                hv_FontSelRegexp);
                            if ((int)(new HTuple((new HTuple(hv_FontsCourier.TupleLength())).TupleEqual(
                                0))) != 0)
                            {
                                hv_Exception = "Wrong font name";
                            }
                            else
                            {
                                try
                                {
                                    HOperatorSet.SetFont(hv_WindowHandle, (((hv_FontsCourier.TupleSelect(
                                        0)) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                                }
                                catch (HalconException HDevExpDefaultException2)
                                {
                                    HDevExpDefaultException2.ToHTuple(out hv_Exception);
                                }
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
        private void Frm_ImageWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        private void cbx_toolRunResultImageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Job job = Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text);
                ToolInfo toolInfo = Job.GetToolInfoByToolName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text, Frm_ImageWindow.Instance.cbx_toolRunResultImageList.Text);


                ((ToolBase)toolInfo.tool).Run(job.jobName, true, true);


            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void tsb_allowDrag_Click(object sender, EventArgs e)
        {
            try
            {
                Frm_ImageWindow.Instance.hwc_imageWindow.Focus();
                Frm_ImageWindow.Instance.hwc_imageWindow.Select();

                //////if (Frm_Main.Instance .tsb_dragMode.Checked)
                //////{
                //////  Frm_Main .  allowScaleAndZoom = true;
                //////    Frm_ImageWindow.Instance.tsb_allowDrag.Checked = true;
                //////}
                //////else
                //////{
                //////    Frm_Main.allowScaleAndZoom = false;
                //////    if (viewController != null)
                //////        viewController.setViewState(HWndCtrl.MODE_VIEW_NONE);
                //////    Frm_ImageWindow.Instance.tsb_allowDrag.Checked = false;
                //////}

                //////if (!Frm_Main.Instance .tsb_dragMode.Checked)
                {
                    if (Frm_ImageWindow.currentImage != null)
                        Frm_ImageWindow.Instance.Display_Image(Frm_ImageWindow.currentImage);
                }
                Frm_ImageWindow.Instance.hwc_imageWindow.Focus();
                Frm_ImageWindow.Instance.hwc_imageWindow.Select();
                if (Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel)
                    Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = false;
                else
                    Frm_ImageWindow.Instance.hwc_imageWindow.DrawModel = true;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void tsb_fullScreen_Click(object sender, EventArgs e)
        {
            Frm_FullScreen.Instance.Show();
            Frm_Main.fullScreen = true;
            Frm_Main.Instance.disp_message(Frm_FullScreen.Instance.windowHandle, "ESC 退出全屏模式", "image", 2, 2, "green", "false");
        }
        private void 运行流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                for (int i = 0; i < Project.Instance.L_jobList.Count; i++)
                {
                    if (Project.Instance.L_jobList[i].debugImageWindow == this.Text)
                    {
                        Project.Instance.L_jobList[i].Run();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 图像适应窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Frm_ImageWindow.currentImage != null)
                Frm_ImageWindow.Instance.Display_Image(Frm_ImageWindow.currentImage);
        }
        private void 填充ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOperatorSet.SetDraw(Frm_ImageWindow.Instance.WindowHandle, "fill");
            Frm_Main.Instance.OutputMsg("Filling mode changed to fill mode", Color.Green);
        }
        private void 轮廓ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOperatorSet.SetDraw(Frm_ImageWindow.Instance.WindowHandle, "margin");
            Frm_Main.Instance.OutputMsg("Filling mode changed to margin mode", Color.Green);
        }
        private void 圆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                this.hwc_imageWindow.ContextMenuStrip = null;
                HTuple row, column, radius;
                HOperatorSet.DrawCircle(Frm_ImageWindow.Instance.WindowHandle, out row, out column, out radius);
                HObject circle;
                HOperatorSet.GenCircle(out circle, row, column, radius);
                HOperatorSet.DispObj(circle, Frm_ImageWindow.Instance.WindowHandle);
                this.hwc_imageWindow.ContextMenuStrip = cnt_rightClickMenu;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 矩形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                this.hwc_imageWindow.ContextMenuStrip = null;
                HTuple row, column, row1, column1;
                HOperatorSet.DrawRectangle1(Frm_ImageWindow.Instance.WindowHandle, out row, out column, out row1, out  column1);
                HObject rectangle1;
                HOperatorSet.GenRectangle1(out rectangle1, row, column, row1, column1);
                HOperatorSet.DispObj(rectangle1, Frm_ImageWindow.Instance.WindowHandle);
                this.hwc_imageWindow.ContextMenuStrip = cnt_rightClickMenu;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 放射矩形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                this.hwc_imageWindow.ContextMenuStrip = null;
                HTuple row, column, angle, width, height;
                HOperatorSet.DrawRectangle2(Frm_ImageWindow.Instance.WindowHandle, out row, out column, out angle, out width, out  height);
                HObject rectangle2;
                HOperatorSet.GenRectangle2(out rectangle2, row, column, angle, width, height);
                HOperatorSet.DispObj(rectangle2, Frm_ImageWindow.Instance.WindowHandle);
                this.hwc_imageWindow.ContextMenuStrip = cnt_rightClickMenu;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 任意形状ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                this.hwc_imageWindow.ContextMenuStrip = null;
                HObject region;
                HOperatorSet.DrawRegion(out region, Frm_ImageWindow.Instance.WindowHandle);
                HOperatorSet.DispObj(region, Frm_ImageWindow.Instance.WindowHandle);
                this.hwc_imageWindow.ContextMenuStrip = cnt_rightClickMenu;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 椭圆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                this.hwc_imageWindow.ContextMenuStrip = null;
                HTuple row, column, angle, radius1, radius2;
                HOperatorSet.DrawEllipse(Frm_ImageWindow.Instance.WindowHandle, out row, out  column, out  angle, out  radius1, out  radius2);
                HObject ellipse;
                HOperatorSet.GenEllipse(out  ellipse, row, column, angle, radius1, radius2);
                HOperatorSet.DispObj(ellipse, Frm_ImageWindow.Instance.WindowHandle);
                this.hwc_imageWindow.ContextMenuStrip = cnt_rightClickMenu;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                this.hwc_imageWindow.ContextMenuStrip = null;
                HTuple row, column, row1, column1;
                HOperatorSet.DrawLine(Frm_ImageWindow.Instance.WindowHandle, out row, out  column, out  row1, out  column1);
                HObject line;
                HOperatorSet.GenRegionLine(out line, row, column, row1, column1);
                HOperatorSet.DispObj(line, Frm_ImageWindow.Instance.WindowHandle);
                this.hwc_imageWindow.ContextMenuStrip = cnt_rightClickMenu;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 区域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                this.hwc_imageWindow.ContextMenuStrip = null;
                HObject region;
                HOperatorSet.DrawRegion(out region, Frm_ImageWindow.Instance.WindowHandle);
                HOperatorSet.DispObj(region, Frm_ImageWindow.Instance.WindowHandle);
                this.hwc_imageWindow.ContextMenuStrip = cnt_rightClickMenu;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 标记点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.hwc_imageWindow.ContextMenuStrip = null;
                HOperatorSet.SetColor(Frm_ImageWindow.Instance.WindowHandle, new HTuple("green"));
                HTuple row, column;
                HOperatorSet.DrawPoint(Frm_ImageWindow.Instance.WindowHandle, out row, out column);
                HOperatorSet.SetLineWidth(Frm_ImageWindow.Instance.WindowHandle, new HTuple(2));
                HOperatorSet.DispCross(Frm_ImageWindow.Instance.WindowHandle, row, column, 20, 0);
                this.hwc_imageWindow.ContextMenuStrip = cnt_rightClickMenu;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void 清除静态图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ImageWindow.Instance.Display_Image(Frm_ImageWindow.currentImage);
        }
        private void 清除原始图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOperatorSet.ClearWindow(Frm_ImageWindow.Instance.WindowHandle);
        }
        private void fullScreenDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_FullScreen.Instance.Show();
            fullScreen = true;
            disp_message(Frm_FullScreen.Instance.windowHandle, "ESC 退出全屏模式", "image", 2, 2, "green", "false");
        }
        private void 打开图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Main.Instance.Read_Image();
        }
        private void 图像另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                SaveFileDialog dig_saveImage = new SaveFileDialog();
                dig_saveImage.FileName = DateTime.Now.ToString("yyyy_MM_dd");
                dig_saveImage.Title = "请选择图像保存路径";
                dig_saveImage.Filter = "Image File|*.tif|Image File(*.*)|*.*|Image File(*.png)|*.txt|Image File(*.jpg)|*.jpg|Image File(*.bmp)|*.bmp";
                dig_saveImage.InitialDirectory = path;
                dig_saveImage.ShowDialog();
                if (dig_saveImage.FileName == string.Empty)
                {
                    return;
                }
                string fileName = dig_saveImage.FileName;
                HObject image;
                HOperatorSet.DumpWindowImage(out image, Frm_ImageWindow.Instance.WindowHandle);
                HOperatorSet.WriteImage(image, "tiff", 0, dig_saveImage.FileName);
                Frm_Main.Instance.OutputMsg("Image saved successfully", Color.Green);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void cbx_toolRunResultImageList_SizeChanged(object sender, EventArgs e)
        {
            cbx_toolRunResultImageList.Refresh();
        }

        private void Frm_ImageWindow_Load(object sender, EventArgs e)
        {

        }

    }
}
