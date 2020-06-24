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
    internal class CreateLineTool : ToolBase
    {

        /// <summary>
        /// 流程名
        /// </summary>
        internal string jobName = string.Empty;
        internal HObject localRegion;
        internal bool fromLocal = false;

        /// <summary>
        /// 输入的第一条线段
        /// </summary>
        internal Point inputPoint1=new Point () ;
        /// <summary>
        /// 输入的第二条线段
        /// </summary>
        internal Point  inputPoint2=new Point ();
        internal Line outputLine = new Line();
        internal Point inputPoint;
        internal double  inputAngle;
        internal int rightDownRow;
        internal int rightDownCol;
        internal bool LeftTopRowUseConst = true;
        internal bool LeftTopColUseConst = true;
        internal bool RightDownRowUseConst = true;
        internal bool RightDownColUseConst = true;
        internal int leftTopRowConstValue;
        internal int leftTopColConstValue;
        internal int rightDownRowConstValue;
        internal int rightDownColConstValue;
        internal HObject outputROI;
        internal PosXYU outputPos = new PosXYU();
        internal PosXYU inputPose = new PosXYU();
        internal PosXYU templatePose = new PosXYU();
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

                _resultDistance.Row = Math.Round(_resultDistance.Row, 3);
                _resultDistance.Col = Math.Round(_resultDistance.Col, 3);



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
                outputLine = new Line();
                outputLine.StartPoint = inputPoint1;
                outputLine.EndPoint  = inputPoint2;
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
