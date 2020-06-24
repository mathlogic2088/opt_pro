using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basler.Pylon;
using HalconDotNet;
using System.Runtime.InteropServices;
using System.Windows;
using System.Threading;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class Camera_Basler
    {
        public Camera_Basler(Basler.Pylon.Camera camera, string sn)
        {
            cameraSN = sn;
            Open(camera);
        }

        /// <summary>
        /// 相机列表
        /// </summary>
        internal static List<Basler.Pylon.Camera> D_cameras = new List<Basler.Pylon.Camera>();
        /// <summary>
        /// 相机集合
        /// </summary>
        private static List<ICameraInfo> L_cameras = new List<Basler.Pylon.ICameraInfo>();
        /// <summary>
        /// 当前相机对象
        /// </summary>
        internal string cameraSN = string.Empty;
        /// <summary>
        /// Basler相机集合
        /// </summary>
        internal static List<DeviceBasler> L_devices = new List<DeviceBasler>();
        /// <summary>
        /// 接收图像完成
        /// </summary>
        internal event EventHandler RetrieveImageCompleted;
        /// <summary>
        /// 获取图像完成
        /// </summary>
        internal event EventHandler<GenImageCompletedEventArgs> GenImageCompleted;
        /// <summary>
        /// 抓取到的图像
        /// </summary>
        internal HImage Image = null;
        /// <summary>
        /// 第一次运行
        /// </summary>
        internal bool isFirst = true;
        /// <summary>
        /// 相机索引
        /// </summary>
        internal int cameraIndex = 0;


        /// <summary>
        /// 抓取一张图像
        /// </summary>
        /// <param name="sn">相机SN</param>
        /// <returns>抓取到的图像</returns>
        internal HObject GrabOneImage()
        {
            try
            {
                HObject image = new HObject();
                try
                {
                    for (int i = 0; i < D_cameras.Count; i++)
                    {
                        if (D_cameras[i].CameraInfo[CameraInfoKey.SerialNumber] == cameraSN)
                        {
                            D_cameras[i].StreamGrabber.Start();

                            IGrabResult grabResult = D_cameras[i].StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                            using (grabResult)
                            {
                                if (grabResult.GrabSucceeded)
                                {
                                    Console.WriteLine("SizeX: {0}", grabResult.Width);
                                    Console.WriteLine("SizeY: {0}", grabResult.Height);
                                    byte[] buffer = grabResult.PixelData as byte[];
                                    Console.WriteLine("Gray value of first pixel: {0}", buffer[0]);
                                    Console.WriteLine("");
                                    byte[] buffer1 = grabResult.PixelData as byte[];
                                    GCHandle hand = GCHandle.Alloc(buffer1, GCHandleType.Pinned);
                                    IntPtr pr = hand.AddrOfPinnedObject();
                                    HOperatorSet.GenImage1(out image, new HTuple("byte"), grabResult.Width, grabResult.Height, pr);
                                    if (hand.IsAllocated)
                                        hand.Free();
                                }
                                else
                                {
                                    Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                                }
                            }
                            D_cameras[i].StreamGrabber.Stop();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Exception: {0}", ex.Message);
                }
                finally
                {
                    Console.Error.WriteLine("\nPress enter to exit.");
                    Console.ReadLine();
                }

                return image;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return null;
            }
        }
        internal Basler.Pylon.Camera GetCameraBySN(string sn)
        {
            try
            {
                //////foreach (KeyValuePair <string ,Basler.Pylon.Camera > item in D_cameras )
                //////{
                //////    if (item.Key == sn)
                //////        return item.Value;
                //////}
                return new Basler.Pylon.Camera();
            }
            catch
            {
                return null;
            }
        }
        internal Int32 GetExposure()
        {
            try
            {
                return 100;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return 0;
            }
        }
        public void SetExposureTime(long value)
        {
            try
            {
                D_cameras[cameraIndex].Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Off);
                D_cameras[cameraIndex].Parameters[PLCamera.ExposureMode].TrySetValue(PLCamera.ExposureMode.Timed);

                D_cameras[cameraIndex].Parameters[PLUsbCamera.ExposureTime].SetValue((double)value);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 通过相机句柄获取信息字符串字符串
        /// </summary>
        /// <param name="deviceStr"></param>
        /// <returns></returns>
        internal static Camera_Basler Get_Camera(string deviceDescriptionStr)
        {
            for (int i = 0; i < L_devices.Count; i++)
            {
                if (L_devices[i].DeviceInfoStr == deviceDescriptionStr)
                    return L_devices[i].Camera_basler;
            }
            return null;
        }
        /// <summary>
        /// 获取图像完成触发事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnGenImageCompleted(GenImageCompletedEventArgs e)
        {
            if (GenImageCompleted != null)
                GenImageCompleted(this, e);
        }
        internal class GenImageCompletedEventArgs : EventArgs
        {
            public HImage image;

            public GenImageCompletedEventArgs(HImage image)
            {
                this.image = image;
            }
        }
        /// <summary>
        /// 设置为软触发模式
        /// </summary>
        internal void SetSoftwareTriggerMode(Basler.Pylon.Camera camera)
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                    camera.StreamGrabber.Stop();
                camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="camera"></param>
        internal void Open(Basler.Pylon.Camera camera)
        {
            try
            {
                //////camera.Open();
                //////camera.ConnectionLost += OnConnectionLost;
                //////camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                //////SetSoftwareTriggerMode(camera );
                //////camera.StreamGrabber.Start();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 相机丢失触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            if (GetCameraBySN(cameraSN) != null)
            {
                //////GetCameraBySN(cameraSN) = null;
            }
            Frm_MessageBox.Instance.MessageBoxShow("相机连接中断");
        }
        /// <summary>
        /// 设置或获取相机曝光时间
        /// </summary>
        internal Int32 ExposureTime
        {
            get
            {
                if (D_cameras[cameraIndex].Parameters.Contains(PLCamera.ExposureTimeAbs))
                    return (int)D_cameras[cameraIndex].Parameters[PLCamera.ExposureTimeAbs].GetValue();
                else
                    return (int)D_cameras[cameraIndex].Parameters[PLCamera.ExposureTime].GetValue();
            }
            set
            {
                if (D_cameras[cameraIndex].Parameters.Contains(PLCamera.ExposureTimeAbs))
                    D_cameras[cameraIndex].Parameters[PLCamera.ExposureTimeAbs].TrySetValue(value);
                else
                    D_cameras[cameraIndex].Parameters[PLCamera.ExposureTime].TrySetValue(value);
            }
        }
        /// <summary>
        /// 枚举所以的Basler相机
        /// </summary>
        internal static void EnumCamrea()
        {
            try
            {
                L_cameras = CameraFinder.Enumerate();
                foreach (ICameraInfo item in L_cameras)
                {
                    string sn = item[CameraInfoKey.SerialNumber];
                    string temp = item[CameraInfoKey.DeviceIpAddress];
                    string temp2 = item[CameraInfoKey.DeviceMacAddress];
                    string temp1 = item[CameraInfoKey.VendorName];
                    Basler.Pylon.Camera camera = new Basler.Pylon.Camera(sn);
                    D_cameras.Add(camera);
                    camera.CameraOpened += Basler.Pylon.Configuration.AcquireContinuous;
                    camera.Open();
                    camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);

                    DeviceBasler deviceBasler = new DeviceBasler();
                    deviceBasler.Camera_basler = new Camera_Basler(camera, sn);
                    deviceBasler.SN = sn;
                    deviceBasler.Exposure = 111;
                    deviceBasler.MinExposure = 11;
                    deviceBasler.MaxExposure = 1111111;
                    deviceBasler.DeviceInfoStr = string.Format("{0}|{1}|{2}|{3}", temp1, sn, temp2, temp);
                    L_devices.Add(deviceBasler);
                    Frm_AcqFromDeviceBasler.Instance.cbx_deviceList.Items.Add(deviceBasler.DeviceInfoStr);
                }
            }
            catch { }
        }
        /// <summary>
        /// 关闭所有相机
        /// </summary>
        public static void CloseAllCamera()
        {
            try
            {
                foreach (Basler.Pylon.Camera item in D_cameras)
                {
                    item.Close();
                }
            }
            catch { }
        }

    }
    /// <summary>
    /// Basler相机
    /// </summary>
    [Serializable]
    internal class DeviceBasler
    {
        internal Camera_Basler Camera_basler;
        internal string SN;
        internal string DeviceInfoStr;
        internal double Exposure;
        internal int MinExposure;
        internal int MaxExposure;
    }
}
