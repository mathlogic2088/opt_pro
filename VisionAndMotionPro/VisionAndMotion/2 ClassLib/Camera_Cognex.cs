using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cognex.VisionPro;
using System.Windows;
using HalconDotNet;
using System.Drawing;
using System.Drawing.Imaging;

namespace VisionAndMotionPro
{
    [Serializable]
    class Camera_Cognex
    {
        public Camera_Cognex(string sn)
        {
            cameraSN = sn;
        }

        /// <summary>
        /// 相机SN
        /// </summary>
        internal string cameraSN = string.Empty;
        /// <summary>
        /// 相机SN和相机对象集合
        /// </summary>
        internal static Dictionary<string, ICogAcqFifo> D_SNAndCamera = new Dictionary<string, ICogAcqFifo>();
        /// <summary>
        /// 相机对象相关信息
        /// </summary>
        internal static List<DeviceCognex> L_devices = new List<DeviceCognex>();
        /// <summary>
        /// 图像抓取次数
        /// </summary>
        private int acquiredNum = 0;
        /// <summary>
        /// 相机曝光时间
        /// </summary>
        private double exposureTime = 0;
        public double ExposureTime
        {
            get
            {
                exposureTime = D_SNAndCamera[cameraSN].OwnedExposureParams.Exposure;
                return exposureTime;
            }
            set
            {
                exposureTime = value;
                D_SNAndCamera[cameraSN].OwnedExposureParams.Exposure = value;
            }
        }


        /// <summary>
        /// 获取一张图像
        /// </summary>
        /// <returns></returns>
        internal HObject GrabOneImage()
        {
            try
            {
                int trigNums = 0;
                ICogImage cogImage = D_SNAndCamera[cameraSN].Acquire(out trigNums);
                acquiredNum++;
                if (acquiredNum > 4)
                {
                    GC.Collect();
                    acquiredNum = 0;
                }

                //将CogImage转换成Hobject类型
                Bitmap bmp = cogImage.ToBitmap();
                HObject image;
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData srcBmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                HOperatorSet.GenImageInterleaved(out image, srcBmpData.Scan0, "bgr", bmp.Width, bmp.Height, 0, "byte", 0, 0, 0, 0, -1, 0);
                bmp.UnlockBits(srcBmpData);
                return image;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("非正常取相"))           //偶尔会采图失败，报错非正常取相
                {
                    LogHelper.SaveErrorInfo(ex);
                }
                LogHelper.SaveErrorInfo(ex);
            }
            return new HObject();
        }
        /// <summary>
        /// 枚举所以的Basler相机
        /// </summary>
        internal static void EnumCamrea()
        {
            try
            {
                CogFrameGrabbers fs = new CogFrameGrabbers();
                for (int i = 0; i < fs.Count; i++)
                {
                    ICogFrameGrabber cogFrameGrabber = fs[i];
                    try
                    {
                        ICogAcqFifo IacqFifo = cogFrameGrabber.CreateAcqFifo(cogFrameGrabber.AvailableVideoFormats[0], CogAcqFifoPixelFormatConstants.Format8Grey, 0, true);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("所需的硬件已由另一个进程在使用"))
                        {
                            MessageBox.Show("相机被占用！(错误代码：004)");
                        }
                    }
                    string sn = "xxx";
                    string temp = "xxx";
                    string temp2 = "xxx";
                    string temp1 = "xxx";
                    DeviceCognex deviceCognex = new DeviceCognex();
                    deviceCognex.SN = sn;
                    deviceCognex.Exposure = 111;
                    deviceCognex.MinExposure = 11;
                    deviceCognex.MaxExposure = 1111111;
                    deviceCognex.DeviceDescriptionStr = string.Format("{0}|{1}|{2}|{3}", temp1, sn, temp2, temp);
                    L_devices.Add(deviceCognex);
                    Frm_AcqFromDeviceBasler.Instance.cbx_deviceList.Items.Add(deviceCognex.DeviceDescriptionStr);

                }
            }
            catch { }
        }
        /// <summary>
        /// 通过相机句柄获取信息字符串字符串
        /// </summary>
        /// <param name="deviceStr"></param>
        /// <returns></returns>
        internal static Camera_Cognex Get_Camera(string deviceDescriptionStr)
        {
            for (int i = 0; i < L_devices.Count; i++)
            {
                if (L_devices[i].DeviceDescriptionStr == deviceDescriptionStr)
                    return L_devices[i].camera;
            }
            return null;
        }
        /// <summary>
        /// 关闭所有相机
        /// </summary>
        public static void CloseAllCamera()
        {
            try
            {
                //////for (int i = 0; i < L_devices.Count; i++)
                //////{
                //////    L_devices[i].camera.Disconnect(true);
                //////}
            }
            catch { }
        }

    }
    internal class DeviceCognex
    {
        internal Camera_Cognex camera;
        internal string SN;
        internal string DeviceDescriptionStr;
        internal double Exposure;
        internal int MinExposure;
        internal int MaxExposure;
    }
}
