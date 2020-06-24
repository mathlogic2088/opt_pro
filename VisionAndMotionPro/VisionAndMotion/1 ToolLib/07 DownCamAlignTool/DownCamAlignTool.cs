using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class RobotDownCamAlignTool :ToolBase 
    {

        /// <summary>
        /// 机械手拍照位置坐标
        /// </summary>
        internal PosXYU caputurePos = new PosXYU();
        /// <summary>
        /// 制作模板时特征点坐标
        /// </summary>
        internal PosXYU templateFeaturePos = new PosXYU();
        /// <summary>
        /// 示教位置坐标
        /// </summary>
        internal PosXYU touchPos = new PosXYU();
        /// <summary>
        /// 本次定位输入坐标
        /// </summary>
        internal PosXYU inputPos = new PosXYU();
        /// <summary>
        /// 计算出来的最终放料机械手坐标
        /// </summary>
        internal PosXYU targetPos = new PosXYU();


        /// <summary>
        /// 绕点旋转
        /// </summary>
        /// <param name="curPos">当前被旋转的点</param>
        /// <param name="rotateCenter">旋转中心</param>
        /// <param name="rotateAngle">旋转角度</param>
        /// <returns></returns>
        internal PosXYU Rotate_At(PosXYU curPos, PosXYU rotateCenter, double rotateAngle)
        {
            try
            {
                double rad = rotateAngle * Math.PI / 180;
                var res = new PosXYU()
                {
                    X = rotateCenter.X + (curPos.X - rotateCenter.X) * Math.Cos(rad) - (curPos.Y - rotateCenter.Y) * Math.Sin(rad),
                    Y = rotateCenter.Y + (curPos.X - rotateCenter.X) * Math.Sin(rad) + (curPos.Y - rotateCenter.Y) * Math.Cos(rad),
                    U = curPos.U + rotateAngle
                };
                if (res.U < -180)
                {
                    res.U += 360;
                }
                else if (res.U >= 180)
                {
                    res.U -= 360;
                }
                return res;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return new PosXYU();
            }
        }
        /// <summary>
        /// 运行工具
        /// </summary>
        public  override void Run(string jobName, bool updateImage, bool b)
        {
            try
            {
                runStatu =(Configuration .language ==Language .English ? ToolRunStatu.Not_Succeed:ToolRunStatu.失败 );
                if (inputPos.X ==0&& inputPos.Y ==0&& inputPos.U == 0)
                {
                    runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Input_Pos  : ToolRunStatu.未指定输入坐标点);
                    return;
                }

                //然后将机械手平移和旋转，使本次定位特征机械坐标和创建模板时的机械坐标重合
                //首先旋转使角度重合
                double templateOffsetU = templateFeaturePos.U - inputPos.U;
                double robotPosAfterRotateU = caputurePos.U + templateOffsetU;

                //计算旋转之后本次定位特征点的XY坐标
                PosXYU currentInputPosAfterRotate = Rotate_At(inputPos, caputurePos, templateOffsetU);

                //计算本次经过旋转的定位特征机械坐标和创建模板时的机械坐标的平移量
                double templateOffsetX = templateFeaturePos.X - currentInputPosAfterRotate.X;
                double templateOffsetY = templateFeaturePos.Y - currentInputPosAfterRotate.Y;

                //机械手再平移这些量
                PosXYU robotPosAfterRotateUAndMoveXY = new PosXYU();
                robotPosAfterRotateUAndMoveXY.X = caputurePos.X + templateOffsetX;
                robotPosAfterRotateUAndMoveXY.Y = caputurePos.Y + templateOffsetY;
                robotPosAfterRotateUAndMoveXY.U = robotPosAfterRotateU;

                //如果机械手移动到上述点，则本次定位特征与模板特征重合
                //此处可以做一个检查，暂未添加，待完善

                //计算示教时机械手旋转的角度
                double touchRobotRotateAngle = touchPos.U - caputurePos.U;

                //计算产品旋转这么多角度时机械手应该所处的位置
                PosXYU robotPosAfterSecondRotate = Rotate_At(robotPosAfterRotateUAndMoveXY, templateFeaturePos, touchRobotRotateAngle);

                //计算产品放料时的平移量
                double productTouchMoveX = touchPos.X - caputurePos.X;
                double productTouchMoveY = touchPos.Y - caputurePos.Y;

                //计算产品平移和旋转后的机械手应该所处的坐标，也就是最终的放料坐标
                targetPos.X = Math.Round(robotPosAfterSecondRotate.X + productTouchMoveX, 3);
                targetPos.Y = Math.Round(robotPosAfterSecondRotate.Y + productTouchMoveY, 3);
                targetPos.U = Math.Round(robotPosAfterSecondRotate.U, 3);

                Frm_DownCamAlignTool.Instance.tbx_inputPosX.Text = inputPos.X.ToString();
                Frm_DownCamAlignTool.Instance.tbx_inputPosY.Text = inputPos.Y.ToString();
                Frm_DownCamAlignTool.Instance.tbx_inputPosU.Text = inputPos.U.ToString();

                Frm_DownCamAlignTool.Instance.tbx_targetPosX.Text = targetPos.X.ToString();
                Frm_DownCamAlignTool.Instance.tbx_targetPosY.Text = targetPos.Y.ToString();
                Frm_DownCamAlignTool.Instance.tbx_targetPosU.Text = targetPos.U.ToString();

                runStatu = (Configuration .language ==Language .English ? ToolRunStatu.Succeed:ToolRunStatu .成功);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
