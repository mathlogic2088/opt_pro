using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VisionAndMotionPro
{
    [Serializable]
    public class ToolBase
    {

        internal ToolRunStatu runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Run : ToolRunStatu.未运行);
        /// <summary>
        /// 工具运行
        /// </summary>
        public  virtual void Run(string s, bool b,bool b1=true )
        {
        }
        /// <summary>
        /// 显示图像
        /// </summary>
        internal void ShowImage(string jobName, HObject image)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        Frm_ImageWindow.Instance.Display_Image(image, item.Value.hwc_imageWindow.HWindowHalconID);
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 设置线的样式
        /// </summary>
        internal void SetLineStyle(string jobName, int style)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        if (style == 0)
                            HOperatorSet.SetLineStyle(item.Value.hwc_imageWindow.HWindowHalconID, new HTuple());
                        else
                            HOperatorSet.SetLineStyle(item.Value.hwc_imageWindow.HWindowHalconID, new HTuple(style));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 通过流程名获取窗体句柄
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        internal HTuple GetWindowHandle(string jobName)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        return item.Value.hwc_imageWindow.HWindowHalconID;
                    }
                }
                Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "The process was successfully run,Elapsed：" : "此流程所绑定的窗体不存在，已自动更换为默认图像窗体" , Color.Red );
                Job.GetJobByName(jobName).debugImageWindow = Frm_ImageWindow.Instance.Text;
                return Frm_ImageWindow.Instance .WindowHandle ;
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }
        /// <summary>
        /// 显示对象
        /// </summary>
        internal void ShowObj(string jobName, HObject obj)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        HOperatorSet.DispObj(obj, item.Value.hwc_imageWindow.HWindowHalconID);
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 清除图像
        /// </summary>
        internal void ClearWindow(string jobName)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        HOperatorSet.ClearWindow(item.Value.hwc_imageWindow.HWindowHalconID);
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 开始绘制模式
        /// </summary>
        internal void Start_Draw_Mode(string jobName)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        item.Value.hwc_imageWindow.ContextMenuStrip = null;
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 结束绘制模式
        /// </summary>
        internal void End_Draw_Mode(string jobName)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        item.Value.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 设置线宽
        /// </summary>
        internal void SetLineWidth(string jobName, int width)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        HOperatorSet.SetLineWidth(item.Value.hwc_imageWindow.HWindowHalconID, new HTuple(width));
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 设置填充模式
        /// </summary>
        internal void SetDraw(string jobName, string drawStyle)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        HOperatorSet.SetDraw(item.Value.hwc_imageWindow.HWindowHalconID, new HTuple(drawStyle));
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 设置显示区域
        /// </summary>
        internal void SetPart(string jobName, double row, double col, double row1, double col1)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        HOperatorSet.SetPart(item.Value.hwc_imageWindow.HWindowHalconID, row, col, row1, col1);
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 设置颜色
        /// </summary>
        internal void SetColor(string jobName, string color)
        {
            try
            {
                foreach (KeyValuePair<string, Frm_ImageWindow> item in Frm_Main.Instance.D_imageWindow)
                {
                    if (item.Key == Job.GetJobByName(jobName).debugImageWindow)
                    {
                        HOperatorSet.SetColor(item.Value.hwc_imageWindow.HWindowHalconID, new HTuple(color));
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
    /// <summary>
    /// 工具运行状态
    /// </summary>
    public enum ToolRunStatu
    {
        Not_Run,
        Not_Enabled,
        No_Input_Image,
        Not_Input_Image,
        Character_Untrained,
        Not_Assign_Image_Template,
        Not_Assign_Input_Image,
        Not_Assign_Input_Source,
        Not_Assign_Input_Pos,
        Not_Asign_Input_Source,
        Lack_Of_Input_Image,
        Lack_Of_Input_Search_Region,
        Not_Assign_Path,
        Not_Asign_Input_Image,
        Input_Image_Cannot_Be_Converted,
        Not_Create_Template,
        No_Image_In_Folder,
        File_Error_Or_Path_Invalid,
        Not_Assign_Acq_Device,
        Not_Succeed,
        Succeed,
        No_Input_String,
        未运行,
        未启用,
        缺少输入搜索区域,
        未指定路径,
        无输入图像,
        未创建模板,
        未训练字符,
        无输入字符串,
        未指定输入图像,
        未指定图像模板,
        缺少输入图像,
        未指定输入坐标点,
        未指定输入源,
        输入图像不能被转化,
        文件夹内无图像,
        图像文件异常或路径不合法,
        未指定采集设备,
        失败,
        成功,
    }

}
