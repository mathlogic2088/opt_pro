using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class ColorToRGBTool : ToolBase
    {

        /// <summary>
        /// 工具运行状态
        /// </summary>
        internal ToolRunStatu runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Run : ToolRunStatu.未运行);
        /// <summary>
        /// 输入图像
        /// </summary>
        internal HObject inputImage;
        /// <summary>
        /// 流程名
        /// </summary>
        internal string jobName = string.Empty;
        /// <summary>
        /// 转化后的红色通道图像
        /// </summary>
        internal HObject outputRed;
        /// <summary>
        /// 转化后的绿色通道图像
        /// </summary>
        internal HObject outputGreen;
        /// <summary>
        /// 转化后的蓝色通道图像
        /// </summary>
        internal HObject outputBlue;


        /// <summary>
        /// 工具恢复到初始状态
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
        /// 清空上次运行的所有输入
        /// </summary>
        internal void ClearLastInput()
        {
            try
            {
                inputImage = null;
                outputRed = null;
                outputGreen = null;
                outputBlue = null;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="updateImage">是否刷新图像</param>
        /// <param name="jobName">流程名</param>
        public override void Run(string jobName, bool updateImage, bool b)
        {
            try
            {
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败;
                if (inputImage == null)
                {
                    runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Asign_Input_Image : ToolRunStatu.未指定输入图像);
                    return;
                }
                HTuple channelNum;
                HOperatorSet.CountChannels(inputImage, out channelNum);
                if (channelNum != 3)
                {
                    runStatu = (Configuration.language == Language.English ? ToolRunStatu.Input_Image_Cannot_Be_Converted : ToolRunStatu.输入图像不能被转化);
                    return;
                }
                HOperatorSet.Decompose3(inputImage, out outputRed, out  outputGreen, out  outputBlue);
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
