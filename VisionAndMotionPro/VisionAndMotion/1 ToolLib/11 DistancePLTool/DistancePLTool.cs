﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using HalconDotNet;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class DistancePLTool : ToolBase
    {

        /// <summary>
        /// 输入图像
        /// </summary>
        internal HObject inputImage;
        /// <summary>
        /// 流程名
        /// </summary>
        internal string jobName = string.Empty;
        /// <summary>
        /// 输入点
        /// </summary>
        internal Point inputPoint;
        /// <summary>
        /// 输入线
        /// </summary>
        internal Line inputLine;
        /// <summary>
        /// 点线距离
        /// </summary>
        internal double outputDistance = 0;


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
        public override void Run(string jobName, bool updateImage, bool b)
        {
            try
            {
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败;

                runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
