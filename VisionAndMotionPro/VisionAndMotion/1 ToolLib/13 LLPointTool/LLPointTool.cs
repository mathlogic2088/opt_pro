using System;
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
    internal class LLPointTool : ToolBase
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
        /// 输入的第一条线段
        /// </summary>
        internal Line line1;
        /// <summary>
        /// 输入的第二条线段
        /// </summary>
        internal Line line2;
        /// <summary>
        /// 计算出来的线与线之间的距离值
        /// </summary>
        private Point _resultDistance;
        internal Point ResultDistance
        {
            get
            {
                if (_resultDistance == null)
                    _resultDistance = new Point();

                _resultDistance.Row  = Math.Round(_resultDistance.Row , 3);
                _resultDistance.Col  = Math.Round(_resultDistance.Col , 3);

                return _resultDistance;
            }
            set { _resultDistance = value; }
        }


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
                HTuple row1, col1, temp;
                HOperatorSet.IntersectionLines(line1.StartPoint.Row,
                    line1.StartPoint.Col,
                    line1.EndPoint.Row,
                    line1.EndPoint.Col,
                    line2.StartPoint.Row,
                    line2.StartPoint.Col,
                    line2.EndPoint.Row,
                    line2.EndPoint.Col,
                    out row1,
                    out col1,
                    out temp);
                ResultDistance.Row = row1;
                ResultDistance.Col = col1;
                HObject cross;
                HOperatorSet.GenCrossContourXld(out cross, row1, col1, new HTuple(30), new HTuple(0));
              // ShowObj(jobName  ,cross);
               Frm_ImageWindow.Instance.hwc_imageWindow.DispObj(cross ,"green");
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
