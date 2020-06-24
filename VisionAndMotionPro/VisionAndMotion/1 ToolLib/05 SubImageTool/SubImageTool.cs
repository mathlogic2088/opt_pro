﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Tool;
using HalconDotNet;
using VisionAndMotionPro;
using System.Text.RegularExpressions;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class SubImageTool:ToolBase 
    {

        /// <summary>
        /// 输入图像
        /// </summary>
        internal HObject inputImage;
        /// <summary>
        /// 输出图像
        /// </summary>
        internal HObject outputImage;
        /// <summary>
        /// 模板图像名称
        /// </summary>
        internal string standardImageName = string .Empty ;


        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="checkAgain">检测失败，重新检测</param>
        /// <param name="updateImage">是否更新输入图像</param>
        public  override void Run( string jobName,bool updateImage,bool b)
        {
            try
            {
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败;
                if (standardImageName == string.Empty)
                {
                    runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Image_Template : ToolRunStatu.未指定图像模板;
                    return;
                }
                HObject standardImage = Job.D_standardImage[standardImageName];

                HTuple channelCount;
                HOperatorSet.CountChannels(standardImage, out channelCount);
                if (channelCount > 1)
                    HOperatorSet.Rgb1ToGray(standardImage, out standardImage);
                HOperatorSet.CountChannels(inputImage, out channelCount);
                if (channelCount > 1)
                    HOperatorSet.Rgb1ToGray(inputImage, out inputImage);

                try
                {
                    HOperatorSet.AbsDiffImage(inputImage, standardImage, out outputImage, new HTuple(1));
                }
                catch
                {
                    Frm_Main.Instance.OutputMsg("减图像失败，可能是两幅图像的大小不同所致（错误代码：0401）", Color.Red);
                    return;
                }
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
