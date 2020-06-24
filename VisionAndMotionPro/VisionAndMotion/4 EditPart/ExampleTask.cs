using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using gts;
using csIOC0640;
using System.Windows;
using System.IO;
using Tool;
using System.Drawing;

namespace VisionAndMotionPro
{
    /// <summary>
    /// 示例主逻辑类，用户按照此类编写自己具体项目的主逻辑类
    /// </summary>
    public static class MainTask
    {

        /// <summary>
        /// 自动运行线程
        /// </summary>
        private static Thread th_autoRun;


        /// <summary>
        /// 绑定轴和IO
        /// </summary>
        public static void Init()
        {
            Card_LeadShineDMC2210.BindDi(1, 0, Di.PLC请求拍照信号);
            Card_LeadShineDMC2210.BindDi(2, 1, Di.启动信号);
            Card_LeadShineDMC2210.BindDi(3, 2, Di.停止信号);
            Card_LeadShineDMC2210.BindDi(4, 3, Di.复位信号);

            Card_LeadShineDMC2210.BindDo(1, 0, Do.拍照完成信号);
            Card_LeadShineDMC2210.BindDo(2, 1, Do.OK信号);
            Card_LeadShineDMC2210.BindDo(3, 2, Do.NG信号);
            Card_LeadShineDMC2210.BindDo(4, 3, Do.程序已开启信号);
            Card_LeadShineDMC2210.BindDo(5, 4, Do.光源1);
            Card_LeadShineDMC2210.BindDo(6, 5, Do.光源2);
            Card_LeadShineDMC2210.BindDo(7, 6, Do.光源3);
            Card_LeadShineDMC2210.BindDo(8, 7, Do.光源4);
            Card_LeadShineDMC2210.BindDo(9, 8, Do.三色灯_红);
            Card_LeadShineDMC2210.BindDo(10, 9, Do.三色灯_黄);
            Card_LeadShineDMC2210.BindDo(11, 10, Do.三色灯_绿);
            Card_LeadShineDMC2210.BindDo(12, 11, Do.三色灯_蜂鸣);

            Card_LeadShineDMC2210.BindAxis(0, "X轴");
            Card_LeadShineDMC2210.BindAxis(1, "Y轴");
        }
        /// <summary>
        /// 自动运行开始
        /// </summary>
        public static void AutoRun()
        {
            th_autoRun = new Thread(MainLogic);
            th_autoRun.IsBackground = true;
            th_autoRun.Start();
        }
        /// <summary>
        /// 主逻辑
        /// </summary>
        public static void MainLogic()
        {
            try
            {
                while (true)
                {
                    if (Machine.runStatu != MachineRunStatu.Running)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    return;     //临时注释
                    #region 主逻辑
                    Level level = Card_LeadShineDMC2210.GetDiSts(Di.PLC请求拍照信号);           //PLC是否给出要求拍照信号
                    if (level == Level.Low )
                    {
                        Frm_Output.Instance.OutputMsg("接收到PLC请求视觉处理信号", Color.Black);
                        Card_LeadShineDMC2210.SetDo(Do.OK信号, Level.Low);
                        Card_LeadShineDMC2210.SetDo(Do.NG信号, Level.Low);
                        Card_LeadShineDMC2210.SetDo(Do.光源1, Level.High);
                        Card_LeadShineDMC2210.SetDo(Do.光源2, Level.High);
                        Card_LeadShineDMC2210.SetDo(Do.光源3, Level.High);
                        Card_LeadShineDMC2210.SetDo(Do.光源4, Level.High);

                        //将轴移动到Home点
                        Card_LeadShineDMC2210.MoveToPoint("Photo");

                        //运行视觉流程
                        Job.GetJobByName("模板匹配").Run();
                        Frm_Output.Instance.OutputMsg("视觉处理完毕", Color.Black);

                        //获取视觉流程执行结果
                        string result = VM.GetJob("模板匹配").GetOutputItemValue("<--形状匹配 . -->结果字符串");
                        Frm_Output.Instance.OutputMsg("视觉处理结果：" + result, Color.Black);

                        //将轴移动到Home点
                        Card_LeadShineDMC2210.MoveToPoint("Home");

                        Card_LeadShineDMC2210.SetDo(Do.光源1, Level.Low);
                        Card_LeadShineDMC2210.SetDo(Do.光源2, Level.Low);
                        Card_LeadShineDMC2210.SetDo(Do.光源3, Level.Low);
                        Card_LeadShineDMC2210.SetDo(Do.光源4, Level.Low);

                        if (result == "1")
                            Card_LeadShineDMC2210.SetDo(Do.OK信号, Level.High);
                        else
                        {
                            Card_LeadShineDMC2210.SetDo(Do.NG信号, Level.High);
                            Frm_Output.Instance.OutputMsg("为匹配到模板", Color.Red);
                            Machine.runStatu = MachineRunStatu.Alarm;
                        }
                        Frm_Output.Instance.OutputMsg("已反馈给PLC：" + result + "信号", Color.Black);
                    }
                    #endregion

                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
    public enum Di
    {
        PLC请求拍照信号,
        启动信号,
        停止信号,
        复位信号,
        急停信号,
    }
    public enum Do
    {
        三色灯_红,
        三色灯_黄,
        三色灯_绿,
        三色灯_蜂鸣,
        拍照完成信号,
        OK信号,
        NG信号,
        程序已开启信号,
        光源1,
        光源2,
        光源3,
        光源4,
    }
}
