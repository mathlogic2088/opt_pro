using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyCapture2Managed;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using HalconDotNet;
using System.Drawing.Imaging;
using System.Threading;

namespace VisionAndMotionPro
{
    internal class Camera_PointGrey
    {

        /// <summary>
        /// 灰点相机对象
        /// </summary>
        private ManagedCamera camera;
        /// <summary>
        /// 灰点相机对象集合
        /// </summary>
        internal static List<DevicePointGray> L_devices = new List<DevicePointGray>();


        /// <summary>
        /// 枚举相机
        /// </summary>
        internal static void EnumCamera()
        {
            try
            {
                ManagedBusManager busMgr = new ManagedBusManager();
                uint num = busMgr.GetNumOfCameras();
                for (int i = 0; i < num; i++)
                {
                    ManagedPGRGuid guid = busMgr.GetCameraFromIndex(0);
                    ManagedCamera camera = new ManagedCamera();
                    camera.Connect(guid);
                    EmbeddedImageInfo embeddedInfo = camera.GetEmbeddedImageInfo();
                    if (embeddedInfo.timestamp.available == true)
                    {
                        embeddedInfo.timestamp.onOff = true;
                    }
                    if (embeddedInfo.exposure.available == true)
                    {
                        embeddedInfo.exposure.onOff = false;
                        embeddedInfo.shutter.onOff = false;
                        embeddedInfo.gain.onOff = false;
                    }
                    camera.SetEmbeddedImageInfo(embeddedInfo);

                    CameraProperty cameraShutter = camera.GetProperty(PropertyType.Shutter);
                    cameraShutter.autoManualMode = false;
                    cameraShutter.absControl = true;

                    CameraProperty cameraGain = camera.GetProperty(PropertyType.Gain);
                    cameraGain.autoManualMode = false;
                    cameraGain.absControl = true;

                    CameraProperty cameraExposure = camera.GetProperty(PropertyType.AutoExposure);
                    cameraExposure.autoManualMode = false;
                    cameraExposure.absControl = true;
                    cameraExposure.absValue = 30;

                    CameraProperty cameraAutoBrightness = camera.GetProperty(PropertyType.Brightness);
                    cameraAutoBrightness.autoManualMode = false;
                    cameraAutoBrightness.absControl = true;

                    //设置采图模式  灰度图或彩色图等
                    VideoMode videoMode = VideoMode.NumberOfVideoModes;
                    FrameRate frameRate = FrameRate.NumberOfFrameRates;
                    camera.GetVideoModeAndFrameRate(ref videoMode, ref frameRate);
                    videoMode = VideoMode.VideoMode1600x1200Y8;

                    camera.SetProperty(cameraShutter);
                    camera.SetProperty(cameraGain);
                    camera.SetProperty(cameraExposure);
                    camera.SetProperty(cameraAutoBrightness);
                    camera.SetVideoModeAndFrameRate(videoMode, frameRate);
                    Thread.Sleep(100);
                    camera.StartCapture();
                    DevicePointGray devicePointGray = new DevicePointGray();
                    devicePointGray.DeviceDescriptionStr = string.Empty;
                    devicePointGray.Exposure = 30;
                    devicePointGray.MinExposure = 10;
                    devicePointGray.MaxExposure = 1000000;
                    devicePointGray.SN = string.Empty;
                    L_devices.Add(devicePointGray);
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 通过相机句柄获取信息字符串字符串
        /// </summary>
        /// <param name="descriptionStr"></param>
        /// <returns></returns>
        public static Camera_PointGrey GetCameraByDescription(string descriptionStr)
        {
            try
            {
                for (int i = 0; i < L_devices.Count; i++)
                {
                    if (L_devices[i].DeviceDescriptionStr == descriptionStr)
                        return L_devices[i].Camera_pointGrey;
                }
                return new Camera_PointGrey();
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
                return new Camera_PointGrey();
            }
        }
        /// <summary>
        /// 抓取一张图像
        /// </summary>
        /// <returns></returns>
        public HObject GrabOneImage()
        {
            try
            {
                ManagedImage rawImage = new ManagedImage();
                camera.RetrieveBuffer(rawImage);
                ManagedImage convertedImage = new ManagedImage();
                rawImage.Convert(FlyCapture2Managed.PixelFormat.PixelFormatBgr, convertedImage);
                System.Drawing.Bitmap bitmap = convertedImage.bitmap;

                HObject image;
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData srcBmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                HOperatorSet.GenImageInterleaved(out image, srcBmpData.Scan0, "bgr", bitmap.Width, bitmap.Height, 0, "byte", 0, 0, 0, 0, -1, 0);
                bitmap.UnlockBits(srcBmpData);
                return image;
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
                return new HObject();
            }
        }
        /// <summary>
        /// 断开相机
        /// </summary>
        public void CloseCamera()
        {
            try
            {
                camera.StopCapture();
                camera.Disconnect();
                camera.Dispose();
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
    /// <summary>
    /// 灰点相机
    /// </summary>
    [Serializable]
    internal class DevicePointGray
    {
        internal Camera_PointGrey Camera_pointGrey;
        internal string SN;
        internal string DeviceDescriptionStr;
        internal double Exposure;
        internal int MinExposure;
        internal int MaxExposure;
    }
}
