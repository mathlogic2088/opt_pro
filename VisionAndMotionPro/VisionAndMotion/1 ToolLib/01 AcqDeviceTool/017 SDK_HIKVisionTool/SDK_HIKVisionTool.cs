using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using Basler.Pylon;

namespace VisionAndMotionPro
{
    [Serializable]
    class SDK_HIKVisionTool : ToolBase
    {
        /// <summary>
        /// 曝光时间
        /// </summary>
        internal Int64 exposure = 30;
        /// <summary>
        /// 流程名
        /// </summary>
        internal string jobName = string.Empty;
        /// <summary>
        /// 图像的获取方式
        /// </summary>
        internal ImageSourceMode imageSourceMode = ImageSourceMode.FormDevice;
        /// <summary>
        /// 是否处于实时采集模式
        /// </summary>
        internal bool realTimeMode = false;
        /// <summary>
        /// 相机对象
        /// </summary>
        internal Camera_Basler camera_Basler;
        /// <summary>
        /// 设备描述字符串
        /// </summary>
        internal string deviceDescriptionStr = string.Empty;
        /// <summary>
        /// 实时采集线程
        /// </summary>
        internal static Thread th_acq;
        /// <summary>
        /// 读取文件夹图像模式时每次运行是否自动切换图像
        /// </summary>
        internal bool autoSwitch = true;
        /// <summary>
        /// 是否将彩色图像转化成灰度图像
        /// </summary>
        internal bool RGBToGray = true;
        /// <summary>
        /// 工作模式为读取文件夹图像时，当前图像的名称
        /// </summary>
        internal string currentImageName = string.Empty;
        /// <summary>
        /// 工作模式为读取文件夹图像时，当前显示的图片的索引
        /// </summary>
        internal int currentImageIndex = 0;
        /// <summary>
        /// 文件夹中的图像文件集合
        /// </summary>
        internal List<string> L_imageFile = new List<string>();
        /// <summary>
        /// 单张图像文件路径
        /// </summary>
        internal string imagePath = string.Empty;
        /// <summary>
        /// 巴斯勒相机对象
        /// </summary>
        internal static Camera_Basler Camera_basler;
        /// <summary>
        /// 图像文件夹路径
        /// </summary>
        internal string imageDirectoryPath = string.Empty;
        /// <summary>
        /// 输出图像
        /// </summary>
        internal HObject outputImage;
        /// <summary>
        /// 读取单张图像或批量读取文件夹图像工作模式
        /// </summary>
        internal WorkMode workMode = WorkMode.ReadMultImage;


        /// <summary>
        /// 工作模式切换
        /// </summary>
        internal void Work_Mode_Changed()
        {
            if (Frm_ReadFromLocalBasler.Instance.rdo_readOneImage.Checked)
            {
                Frm_ReadFromLocalBasler.Instance.ckb_autoSwitch.Visible = false;
                Frm_ReadFromLocalBasler.Instance.pnl_multImage.Visible = false;
                Frm_ReadFromLocalBasler.Instance.btn_browseImage.Visible = false;
                workMode = WorkMode.ReadOneImage;
            }
            else
            {
                Frm_ReadFromLocalBasler.Instance.ckb_autoSwitch.Visible = true;
                Frm_ReadFromLocalBasler.Instance.pnl_multImage.Visible = true;
                Frm_ReadFromLocalBasler.Instance.btn_browseImage.Visible = true;
                workMode = WorkMode.ReadMultImage;
            }
        }

        /// <summary>
        /// 图像源模式切换
        /// </summary>
        internal void Work_Mode_Changed(object sender)
        {
            if (((Button)sender).Text == "设  备  采  集")
            {
                Frm_SDK_HIKVisionTool.Instance.tsb_realTimeDisplay.Enabled = true;
                Frm_SDK_HIKVisionTool.Instance.btn_fromDevice.BackColor = Color.LimeGreen;
                Frm_SDK_HIKVisionTool.Instance.btn_fromLocal.BackColor = Color.LightGray;
                Frm_SDK_HIKVisionTool.Instance.pnl_formBox.Controls.Clear();
                Frm_AcqFromDeviceHIKVision.Instance.TopLevel = false;
                Frm_AcqFromDeviceHIKVision.Instance.Parent = Frm_SDK_HIKVisionTool.Instance.pnl_formBox;
                Frm_AcqFromDeviceHIKVision.Instance.Dock = DockStyle.Fill;
                Frm_AcqFromDeviceHIKVision.Instance.Show();
                Frm_AcqFromDeviceHIKVision.Instance.btn_runSDKHIKVisionTool.Focus();
                imageSourceMode = ImageSourceMode.FormDevice;
            }
            else
            {
                Frm_SDK_HIKVisionTool.Instance.tsb_realTimeDisplay.Enabled = false;
                Frm_SDK_HIKVisionTool.Instance.btn_fromLocal.BackColor = Color.LimeGreen;
                Frm_SDK_HIKVisionTool.Instance.btn_fromDevice.BackColor = Color.LightGray;
                Frm_SDK_HIKVisionTool.Instance.pnl_formBox.Controls.Clear();
                Frm_ReadFromLocal6.Instance.TopLevel = false;
                Frm_ReadFromLocal6.Instance.Parent = Frm_SDK_HIKVisionTool.Instance.pnl_formBox;
                Frm_ReadFromLocal6.Instance.Dock = DockStyle.Fill;
                Frm_ReadFromLocal6.Instance.Show();
                Frm_ReadFromLocal6.Instance.btn_runSDKHIKVisionTool.Focus();
                imageSourceMode = ImageSourceMode.FromLocal;
            }
        }

        /// <summary>
        /// 选择图像文件路径
        /// </summary>
        internal void Select_Image_Path()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openImage = new System.Windows.Forms.OpenFileDialog();
                dig_openImage.Title = Configuration.language == Language.English ? "Please select image path" : "请选择图像文件路径";
                dig_openImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_openImage.Filter = Configuration.language == Language.English ? "Image File(*.*)|*.*|Image File(*.png)|*.png|Image File(*.jpg)|*.jpg|Image File(*.tif)|*.tif" : "图像文件(*.*)|*.*|图像文件(*.png)|*.png|图像文件(*.jpg)|*.jpg|图像文件(*.tif)|*.tif";
                if (dig_openImage.ShowDialog() == DialogResult.OK)
                {
                    Frm_ReadFromLocal6.Instance.tbx_imagePath.Text = dig_openImage.FileName;
                    imagePath = dig_openImage.FileName;
                    HObject image = new HObject();
                    try
                    {
                        HOperatorSet.ReadImage(out image, dig_openImage.FileName);
                        if (RGBToGray)
                        {
                            HTuple channel;
                            HOperatorSet.CountChannels(image, out channel);
                            if (channel == 3)
                                HOperatorSet.Rgb1ToGray(image, out image);
                        }
                    }
                    catch
                    {
                        Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "There's a problem with the file or the path is invalid(ErrorCode:1102)" : "图像文件异常或路径不合法（错误代码：0102）", Color.Red);
                        return;
                    }
                    ShowImage(jobName, image);
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 读取图像
        /// </summary>
        internal void Read_Image()
        {
            try
            {
                HObject image = new HObject();
                try
                {
                    HOperatorSet.ReadImage(out image, Frm_ReadFromLocal.Instance.tbx_imagePath.Text);
                }
                catch
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "There's a problem with the file or the path is invalid(ErrorCode:1102)" : "图像文件异常或路径不合法（错误代码：0102）", Color.Red);
                    return;
                }
                ShowImage(jobName, image);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 选择图像文件夹路径
        /// </summary>
        internal void Select_Image_Directory()
        {
            try
            {
                FolderBrowserDialog folderBrowseDialog = new FolderBrowserDialog();
                if (Directory.Exists(imageDirectoryPath))
                    folderBrowseDialog.SelectedPath = imageDirectoryPath;
                folderBrowseDialog.Description = Configuration.language == Language.English ? "Please select image folder" : "请选择图像文件夹路径";
                if (folderBrowseDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    Frm_ReadFromLocal6.Instance.tbx_imageDirectory.Text = folderBrowseDialog.SelectedPath;
                    imageDirectoryPath = folderBrowseDialog.SelectedPath;
                    L_imageFile.Clear();
                    string[] files = Directory.GetFiles(folderBrowseDialog.SelectedPath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        FileInfo fileInfo = new FileInfo(files[i]);
                        if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".bmp" || fileInfo.Extension == ".png" || fileInfo.Extension == ".tif")
                            L_imageFile.Add(files[i]);
                    }
                    if (L_imageFile.Count > 0)
                    {
                        HObject image = new HObject();
                        try
                        {
                            HOperatorSet.ReadImage(out image, L_imageFile[0]);
                            if (RGBToGray)
                            {
                                HTuple channel;
                                HOperatorSet.CountChannels(image, out channel);
                                if (channel > 1)
                                    HOperatorSet.Rgb1ToGray(image, out image);
                            }
                        }
                        catch
                        {
                            Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "There's a problem with the file or the path is invalid(ErrorCode:1102)" : "图像文件异常或路径不合法（错误代码：0102）", Color.Red);
                            return;
                        }
                        currentImageIndex = 0;
                        ShowImage(jobName, image);
                        currentImageName = Path.GetFileName(L_imageFile[0]);
                        Frm_ReadFromLocal6.Instance.lbl_imageName.Text = currentImageName;
                    }
                    Frm_ReadFromLocal6.Instance.lbl_imageNum.Text = "共" + L_imageFile.Count + "张";
                    Frm_Main.Save(Frm_Job.Instance.tbc_jobs.SelectedTab.Text);
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 读取文件夹中上一张图像
        /// </summary>
        internal void Read_Last_Image()
        {
            try
            {
                HObject image = new HObject();
                currentImageIndex = currentImageIndex - 1;
                if (currentImageIndex < 0)
                    currentImageIndex = L_imageFile.Count - 1;
                try
                {
                    HOperatorSet.ReadImage(out image, L_imageFile[currentImageIndex]);
                }
                catch
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "There's a problem with the file or the path is invalid(ErrorCode:1102)" : "图像文件异常或路径不合法（错误代码：0102）", Color.Red);
                    return;
                }
                ShowImage(jobName, image);
                currentImageName = Path.GetFileName(L_imageFile[currentImageIndex]);
                Frm_ReadFromLocal6.Instance.lbl_imageName.Text = currentImageName;
                Frm_ReadFromLocal6.Instance.lbl_imageNum.Text = "共" + L_imageFile.Count + "张";
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 读取文件夹中下一张图像
        /// </summary>
        internal void Read_Next_Image()
        {
            try
            {
                HObject image = new HObject();
                currentImageIndex = currentImageIndex + 1;
                if (currentImageIndex > L_imageFile.Count - 1)
                {
                    currentImageIndex = 0;
                }
                try
                {
                    HOperatorSet.ReadImage(out image, L_imageFile[currentImageIndex]);
                }
                catch
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "There's a problem with the file or the path is invalid(ErrorCode:1102)" : "图像文件异常或路径不合法（错误代码：0102）", Color.Red);
                    return;
                }
                ShowImage(jobName, image);
                currentImageName = Path.GetFileName(L_imageFile[currentImageIndex]);
                Frm_ReadFromLocal6.Instance.lbl_imageName.Text = currentImageName;
                Frm_ReadFromLocal6.Instance.lbl_imageNum.Text = "共" + L_imageFile.Count + "张";
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 添加当前图像到标准图像
        /// </summary>
        internal void Regist_Images()
        {
            try
            {
                //首先判断当前图像是否为空
                if (Frm_ImageWindow.currentImage == null)
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Current iamge is empty,Can not add" : "当前图像为空，不可添加", Color.Red);
                    return;
                }
            //如果不覆盖已存在的图像则重新输入，所以需要跳到这里重新执行
            inputAgain:
                Frm_InputMessage frm_inputMessage = new Frm_InputMessage();
                frm_inputMessage.lal_title.Text = Configuration.language == Language.English ? "Please input name of standard image" : "请输入标准图像名";
                frm_inputMessage.btn_confirm.Text = Configuration.language == Language.English ? "OK" : "确定";
                frm_inputMessage.TopMost = true;
                frm_inputMessage.ShowDialog();
                if (Frm_InputMessage.input == string.Empty)
                {
                    return;
                }
                if (!Job.D_standardImage.ContainsKey(Frm_InputMessage.input + ".tif"))       //此名称不存在
                {
                    string fileName = Application.StartupPath + "\\Config\\Vision\\StandardImage\\" + Frm_InputMessage.input;
                    HOperatorSet.WriteImage(Frm_ImageWindow.currentImage, new HTuple("tiff"), new HTuple(0), new HTuple(fileName));
                    Frm_JobInfo.Instance.cbx_standardImage.Items.Add(Frm_InputMessage.input + ".tif");
                    Frm_SubImageTool.Instance.cbx_standardImage.Items.Add(Frm_InputMessage.input + ".tif");
                    Job.D_standardImage.Add(Frm_InputMessage.input + ".tif", Frm_ImageWindow.currentImage);
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Add to standard images successfully" : "添加到标准图项成功", Color.Green);
                }
                else        //此名称已存在
                {
                    DialogResult result = MessageBox.Show(Configuration.language == Language.English ? "An image with the same name already exists, is it overwritten?" : "已存在同名图像，是否覆盖？", Configuration.language == Language.English ? "Tip:" : "提示：", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        string fileName = Application.StartupPath + "\\Config\\Vision\\StandardImage\\" + Frm_InputMessage.input;
                        Job.D_standardImage.Remove(Frm_InputMessage.input + ".tif");
                        Job.D_standardImage.Add(Frm_InputMessage.input + ".tif", Frm_ImageWindow.currentImage);
                        HOperatorSet.WriteImage(Frm_ImageWindow.currentImage, new HTuple("tiff"), new HTuple(0), new HTuple(fileName));
                        if (!Frm_JobInfo.Instance.cbx_standardImage.Items.Contains(Frm_InputMessage.input + ".tif"))
                        {
                            Frm_JobInfo.Instance.cbx_standardImage.Items.Add(Frm_InputMessage.input + ".tif");
                            Frm_SubImageTool.Instance.cbx_standardImage.Items.Add(Frm_InputMessage.input + ".tif");
                        }
                    }
                    else
                    {
                        goto inputAgain;
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 实时显示
        /// </summary>
        internal void Real_Time()
        {
            try
            {
                while (realTimeMode)
                {
                    Camera_basler.GrabOneImage();

                    //彩色图像转灰度图像
                    if (RGBToGray)
                    {
                        HTuple channel;
                        HOperatorSet.CountChannels(outputImage, out channel);
                        if (channel == 3)
                            HOperatorSet.Rgb1ToGray(outputImage, out outputImage);
                    }
                    ShowImage(jobName, outputImage);
                }
                Thread.Sleep(10);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 开始实时采集
        /// </summary>
        internal void Real_Time_Acquistion()
        {
            try
            {
                if (Camera_basler == null)
                {
                    Frm_SDK_HIKVisionTool.Instance.tsb_realTimeDisplay.CheckState = CheckState.Unchecked;
                    Frm_Main.Instance.OutputMsg("未指定采集设备，不可进行实时采集（错误代码：0103）", Color.Red);
                    return;
                }

                if (!realTimeMode)
                {
                    realTimeMode = true;
                    Frm_SDK_HIKVisionTool.Instance.tsb_realTimeDisplay.CheckState = CheckState.Checked;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_RealTime.Text = "停止实时";
                    Frm_AcqFromDeviceHIKVision.Instance.btn_runSDKHIKVisionTool.Enabled = false;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_saveImage.Enabled = false;
                    Frm_AcqFromDeviceHIKVision.Instance.cbx_deviceList.Enabled = false;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_registImage.Enabled = false;
                    Frm_AcqFromDeviceHIKVision.Instance.tbx_exposure.Enabled = false;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_runSDKHIKVisionTool.BackColor = Color.LightGray;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_saveImage.BackColor = Color.LightGray;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_registImage.BackColor = Color.LightGray;

                    th_acq = new Thread(Real_Time);
                    th_acq.IsBackground = true;
                    th_acq.Start();
                }
                else
                {
                    realTimeMode = false;
                    Frm_SDK_HIKVisionTool.Instance.tsb_realTimeDisplay.CheckState = CheckState.Unchecked;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_RealTime.Text = "实时采集";
                    Frm_AcqFromDeviceHIKVision.Instance.btn_runSDKHIKVisionTool.Enabled = true;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_saveImage.Enabled = true;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_registImage.Enabled = true;
                    Frm_AcqFromDeviceHIKVision.Instance.cbx_deviceList.Enabled = true;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_runSDKHIKVisionTool.BackColor = Color.White;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_saveImage.BackColor = Color.White;
                    Frm_AcqFromDeviceHIKVision.Instance.btn_registImage.BackColor = Color.White;
                    Frm_AcqFromDeviceHIKVision.Instance.tbx_exposure.Enabled = true;
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 采集设备切换
        /// </summary>
        /// <param name="deviceDescriptionStr">设备描述字符串</param>
        internal void Device_Changed(string deviceDescriptionStr)
        {
            try
            {
                if (Frm_AcqFromDeviceHIKVision.Instance.cbx_deviceList.Text != "")
                {
                    for (int i = 0; i < Camera_Basler.L_devices.Count; i++)
                    {
                        if (Camera_Basler.L_devices[i].DeviceInfoStr == deviceDescriptionStr)
                        {
                            Frm_AcqFromDeviceHIKVision.Instance.tkb_exposure.Maximum = Camera_Basler.L_devices[i].MaxExposure;
                            Frm_AcqFromDeviceHIKVision.Instance.tkb_exposure.Minimum = Camera_Basler.L_devices[i].MinExposure;
                            Int32 exposure = camera_Basler.GetExposure();
                            //////HOperatorSet.GetFramegrabberParam(Camera_Basler.L_devices[i].Handle, "exposure", out exposure);
                            Frm_AcqFromDeviceHIKVision.Instance.tbx_exposure.Text = exposure.ToString();
                            Frm_AcqFromDeviceHIKVision.Instance.tkb_exposure.Value = exposure;
                            Frm_AcqFromDeviceHIKVision.Instance.lbl_exposureRange.Text = "曝光范围：" + Camera.L_device[i].MinExposure + " ~ " + Camera.L_device[i].MaxExposure;
                            this.camera_Basler = Camera_Basler.L_devices[i].Camera_basler;
                            this.deviceDescriptionStr = deviceDescriptionStr;
                        }
                        else
                        {
                            this.camera_Basler = null;
                        }
                    }
                    Frm_Main.Save();
                }
                else
                {
                    this.camera_Basler = null;
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 图像另存为
        /// </summary>
        internal void save_Image()
        {
            try
            {
                System.Windows.Forms.SaveFileDialog dig_saveImage = new System.Windows.Forms.SaveFileDialog();
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                dig_saveImage.FileName = DateTime.Now.ToString("yyyy_MM_dd");
                dig_saveImage.Title = Configuration.language == Language.English ? "Please select the image path" : "请选择图像保存路径";
                dig_saveImage.Filter = Configuration.language == Language.English ? "Image File(*.tif)|*.tif|Image File(*.png)|*.png|Image File(*.jpg)|*.jpg|Image File(*.*)|*.*" : "图像文件(*.tif)|*.tif|图像文件(*.png)|*.png|图像文件(*.jpg)|*.jpg|图像文件(*.*)|*.*";
                dig_saveImage.InitialDirectory = path;
                if (dig_saveImage.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dig_saveImage.FileName;
                    try
                    {
                        HOperatorSet.WriteImage(Frm_ImageWindow.currentImage, "tiff", 0, dig_saveImage.FileName);
                    }
                    catch
                    {
                        Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "There's a problem with the file or the path is invalid(ErrorCode:1201)" : "图像文件异常或路径不合法（错误代码：0102）", Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 设置曝光
        /// </summary>
        internal void Set_Exposure(string jobName)
        {
            try
            {
                if (Frm_Main.ignore)
                    return; ;
                if (Camera_basler != null)
                {
                    Int32 exposure = Convert.ToInt32(Frm_AcqFromDeviceHIKVision.Instance.tbx_exposure.Text.Trim());
                    for (int i = 0; i < Camera.L_device.Count; i++)
                    {
                        if (Camera.L_device[i].DeviceDescriptionStr == Frm_AcqFromDeviceHIKVision.Instance.cbx_deviceList.Text.Trim())
                        {
                            if (Camera.L_device[i].MinExposure <= exposure && exposure <= Camera.L_device[i].MaxExposure)
                                Camera.L_device[i].Exposure = exposure;
                            else
                            {
                                Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Not assign valid device" : "曝光设置失败，曝光值不在可设置的曝光范围内（错误代码：0104）", Color.Red);
                                return;
                            }
                        }
                    }
                    if (Frm_AcqFromDeviceHIKVision.Instance.tkb_exposure.Value != exposure)
                        Frm_AcqFromDeviceHIKVision.Instance.tkb_exposure.Value = exposure;
                    camera_Basler.ExposureTime = exposure;
                    Thread.Sleep(100);
                    if (!Frm_Main.ignore)
                        Run( jobName,true ,false );
                }
                else
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Not assign valid device" : "未指定可用设备，曝光设置失败（错误代码：0105）", Color.Red);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 运行工具
        /// </summary>
        public  override void Run(string jobName, bool updateImage, bool b)
        {
            runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败;
            if (imageSourceMode == ImageSourceMode.FormDevice)      //从设备采集图像
            {

                if (camera_Basler == null)
                {
                    runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Acq_Device : ToolRunStatu.未指定采集设备;
                    return;
                }
                //此处Try起来，因为重新启动程序之后相机句柄就变了
                try
                {
                    HOperatorSet.GrabImage(out outputImage, new HTuple(camera_Basler));
                }
                catch
                {
                    camera_Basler = Camera_Basler.Get_Camera(deviceDescriptionStr);
                    HOperatorSet.GrabImage(out outputImage, new HTuple(camera_Basler));
                }

                if (updateImage)
                    HOperatorSet.ClearWindow(Frm_ImageWindow.Instance.WindowHandle);

                //彩色图像转灰度图像
                if (RGBToGray)
                {
                    HTuple channel;
                    HOperatorSet.CountChannels(outputImage, out channel);
                    if (channel > 1)
                        HOperatorSet.Rgb1ToGray(outputImage, out outputImage);
                }
            }
            else
            {
                HObject image = new HObject();
                try
                {
                    if (workMode == WorkMode.ReadOneImage)
                    {
                        if (imagePath == string .Empty )
                        {
                            runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Assign_Path : ToolRunStatu.未指定路径;
                            return;
                        }
                        HOperatorSet.ReadImage(out image, imagePath);
                    }
                    else
                    {
                        if (imageDirectoryPath == string .Empty )
                        {
                            runStatu = ToolRunStatu.Not_Assign_Path;
                            return;
                        }

                        //需要更新一下文件夹下面的图像
                        L_imageFile.Clear();
                        string[] files = new string[] { };
                        try
                        {
                            files = Directory.GetFiles(imageDirectoryPath);
                        }
                        catch
                        {
                            Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "The path is invalid(ErrorCode:1102)" : "路径不合法（错误代码：0106）", Color.Red);
                            return;
                        }
                        for (int i = 0; i < files.Length; i++)
                        {
                            FileInfo fileInfo = new FileInfo(files[i]);
                            if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".bmp" || fileInfo.Extension == ".png" || fileInfo.Extension == ".tif")
                                L_imageFile.Add(files[i]);
                        }
                        Frm_ReadFromLocal6.Instance.lbl_imageNum.Text = "共" + L_imageFile.Count + "张";

                        //if (autoSwitch && !Frm_Main.Instance.btm_autoSwitch.Checked)
                        //{
                        //    currentImageIndex = currentImageIndex + 1;
                        //}
                        if (currentImageIndex > L_imageFile.Count - 1)
                        {
                            currentImageIndex = 0;
                        }
                        if (L_imageFile.Count == 0)
                        {
                            Frm_Main.Instance.OutputMsg("图像路径下无有效图像文件", Color.Green);
                            runStatu = ToolRunStatu.No_Image_In_Folder;
                            return;
                        }
                        HOperatorSet.ReadImage(out image, L_imageFile[currentImageIndex]);
                        currentImageName = Path.GetFileName(L_imageFile[currentImageIndex]);
                        Frm_ReadFromLocal6.Instance.lbl_imageName.Text = currentImageName;
                    }
                }
                catch
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "There's a problem with the file or the path is invalid(ErrorCode:1102)" : "图像文件异常或路径不合法（错误代码：0102）", Color.Red);
                    runStatu = ToolRunStatu.File_Error_Or_Path_Invalid;
                    return;
                }

                //彩色图像转灰度图像
                if (RGBToGray)
                {
                    HTuple channel;
                    HOperatorSet.CountChannels(image, out channel);
                    if (channel > 1)
                        HOperatorSet.Rgb1ToGray(image, out image);
                }

                outputImage = image;
            }
            ShowImage(jobName, outputImage);
            runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
        }

    }
}
