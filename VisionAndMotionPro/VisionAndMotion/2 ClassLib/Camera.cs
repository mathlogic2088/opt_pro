using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VisionAndMotionPro
{
    /// <summary>
    /// 相机类
    /// </summary>
    internal class Camera
    {

        /// <summary>
        /// 相机对象集合
        /// </summary>
        internal static List<AcquistionDevice> L_device = new List<AcquistionDevice>();


        /// <summary>
        /// 通过Halcon图像采集接口枚举网络中所有的相机
        /// </summary>
        internal static void Search_All_Camera()
        {
            try
            {
                string[] interfaceTypeArray = new string[] { "DirectShow", "GigEVision2" };
                //遍历所有接口
                for (int i = 0; i < interfaceTypeArray.Length; i++)
                {
                    List<string> L_deviceStr = new List<string>();
                    HTuple info, infoValue = new HTuple();

                    {
                        try
                        {
                            HOperatorSet.InfoFramegrabber(new HTuple(interfaceTypeArray[i]), new HTuple("device"), out info, out infoValue);
                        }
                        catch
                        {
                            continue;
                        }
                        //////if (infoValue.ToString() == "\"device:default\"")       //表示没有搜索到采集设备
                        //////    continue;
                        for (int j = 0; j < infoValue.Length; j++)
                        {

                            HTuple temp = infoValue.TupleSelect(j);

                            //初始化相机
                            HTuple handle;
                            try
                            {
                                HOperatorSet.OpenFramegrabber(new HTuple(interfaceTypeArray[i]),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple("progressive"),
                                                              new HTuple(-1),
                                                              new HTuple("default"),
                                                              new HTuple(-1),
                                                              new HTuple("false"),
                                                              new HTuple("default"),
                                                              temp,
                                                              new HTuple(0),
                                                              new HTuple(-1),
                                                              out handle
                                                              );
                            }
                            catch
                            {
                                continue;
                            }
                            //获取曝光范围
                            HTuple range;
                            // HOperatorSet.GetFramegrabberParam(handle, new HTuple("exposure_range"), out range);
                            // string range1 = range.ToString().TrimStart('[').TrimEnd(']');
                            //////string min = Regex.Split(range1, ",")[0];
                            //////string max = Regex.Split(range1, ",")[1];
                            string min = "10";
                            string max = "1000";
                            HTuple exposure = 100;
                            // HOperatorSet.GetFramegrabberParam(handle, new HTuple("exposure"), out exposure);

                            AcquistionDevice camera = new AcquistionDevice();
                            camera.MinExposure = Convert.ToInt16(min);
                            camera.MaxExposure = Convert.ToInt16(max);
                            camera.DeviceStr = temp;
                            camera.Handle = handle;
                            camera.Exposure = exposure;
                            camera.InterfaceType = interfaceTypeArray[i];
                            camera.DeviceDescriptionStr = camera.InterfaceType + " | " + camera.DeviceStr;
                            Camera.L_device.Add(camera);
                            Frm_AcqFromDevice.Instance.cbx_deviceList.Items.Add(camera.DeviceDescriptionStr);


                            //////HTuple element;
                            //////HOperatorSet.TupleSelect(infoValue, new HTuple(j), out element);
                            //////string temp = element.ToString();
                            //////int startIndex = temp.IndexOf(":");
                            //////int endIndex = temp.IndexOf("|");
                            //////string deviceStr = temp.Substring(startIndex + 1, (endIndex - startIndex) - 2);
                            //////L_deviceStr.Add(deviceStr);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 通过设备描述字符串获取最小曝光
        /// </summary>
        /// <param name="deviceDescriptionStr"></param>
        /// <returns></returns>
        internal static int Get_Min_Exposure(string deviceDescriptionStr)
        {
            for (int i = 0; i < L_device.Count; i++)
            {
                if (L_device[i].DeviceDescriptionStr == deviceDescriptionStr)
                    return L_device[i].MinExposure;
            }
            return -1;
        }
        /// <summary>
        /// 通过相机句柄获取相机描述字符串
        /// </summary>
        /// <param name="deviceStr"></param>
        /// <returns></returns>
        internal static HTuple Get_Handle(string deviceDescriptionStr)
        {
            for (int i = 0; i < Camera.L_device.Count; i++)
            {
                if (Camera.L_device[i].DeviceDescriptionStr == deviceDescriptionStr)
                    return Camera.L_device[i].Handle;
            }
            return null;
        }
        /// <summary>
        /// 通过设备描述字符串获取最大曝光
        /// </summary>
        /// <param name="deviceDescriptionStr"></param>
        /// <returns></returns>
        internal static int Get_Max_Exposure(string deviceDescriptionStr)
        {
            for (int i = 0; i < L_device.Count; i++)
            {
                if (L_device[i].DeviceDescriptionStr == deviceDescriptionStr)
                    return L_device[i].MaxExposure;
            }
            return -1;
        }
        /// <summary>
        /// 关闭所有相机
        /// </summary>
        internal static void Close_All_Camera()
        {
            for (int i = 0; i < Camera.L_device.Count; i++)
            {
                HOperatorSet.CloseFramegrabber(L_device[i].Handle);
            }
        }

    }
}
