using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    internal partial class Frm_EyeHandCalibrationTool : Frm_ToolBase
    {
        internal Frm_EyeHandCalibrationTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_EyeHandCalibrationTool _instance;
        public static Frm_EyeHandCalibrationTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_EyeHandCalibrationTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static EyeHandCalibrationTool eyeHandCalibrationTool = new EyeHandCalibrationTool();


        private void btn_calibrate_Click(object sender, EventArgs e)
        {
            eyeHandCalibrationTool.Calibrate();
        }
        private void cbo_calibrationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_calibrationType.Text == "四点标定")
            {
                dgv_calibrateData.Rows.Clear();
                dgv_calibrateData.Rows.Add(4);
                eyeHandCalibrationTool.calibrationType = CalibrationType.Four_Point;
            }
            else if (cbo_calibrationType.Text == "九点标定")
            {
                dgv_calibrateData.Rows.Clear();
                dgv_calibrateData.Rows.Add(9);
                eyeHandCalibrationTool.calibrationType = CalibrationType.Nine_Point;
            }
        }
        private void tsb_runOnce_Click(object sender, EventArgs e)
        {
            eyeHandCalibrationTool.Run( jobName,true ,true );
        }
        private void tsb_resetTool_Click(object sender, EventArgs e)
        {
            eyeHandCalibrationTool.ResetTool();
        }
        private void tsb_help_Click(object sender, EventArgs e)
        {
            Frm_ToolHelp.Instance.ShowToolHelp("Halcon采集接口",
                                              "此工具以Halcon的采集接口为基础获取图像，支持从设备采集图像和从本地读取图像两种工作模式，可自如切换。",
                                              "1. 将工具添加到流程；\r\n2. 打开工具，选择图像获取模式(从设备采集或从本地读取)；\r\n3. 从设备列表选定图像采集设备(从设备采集模式)或指定图像路径(从本地读取模式)；",
                                              "无"
                                              );
        }
        private void btn_readCalibrationData_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openImage = new System.Windows.Forms.OpenFileDialog();
                dig_openImage.FileName = string.Empty;
                dig_openImage.Title = (Configuration.language == Language.English ? "Please select a form file" : "请选择表格文件");
                dig_openImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_openImage.Filter = (Configuration.language == Language.English ? "Image File(*.*)|*.*|Image File(*.png)|*.txt|Image File(*.jpg)|*.jpg|Image File(*.bmp)|*.bmp|Image File(*.tif)|*.tif" : "标定文件(*.*)|*.*|CSV文件(*.csv)|*.csv|XLS文件(*.xls)|*.xls");
                if (dig_openImage.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = File.ReadAllLines(dig_openImage.FileName, Encoding.Default);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] data = Regex.Split(lines[i], ",");
                        for (int j = 0; j < data.Length; j++)
                        {
                            dgv_calibrateData.Rows[i].Cells[j].Value = data[j];
                            if (j == 3)            //只导入前四列
                                break;
                        }
                        if (i == (cbo_calibrationType.SelectedIndex == 0 ? 3 : 8))           //若标定类型为四点标定，则导入前四行，若为九点标定，则导入前九行
                            break;
                    }
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Loading Image successfully" : "标定文件导入成功", Color.Green);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_writeCalibrationData_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Windows.Forms.SaveFileDialog dig_saveImage = new System.Windows.Forms.SaveFileDialog();
                dig_saveImage.FileName = "Data " + DateTime.Now.ToString("yyyy_MM_dd");
                dig_saveImage.Title = Configuration.language == Language.English ? "Please select the image saving path" : "请选择导出路径";
                dig_saveImage.Filter = "CSV文件(*.csv)|*.csv|XLS文件(*.xls)|*.xls";
                dig_saveImage.InitialDirectory = path;
                if (dig_saveImage.ShowDialog() == DialogResult.OK)
                {
                    File.Create(dig_saveImage.FileName).Close();
                    string data = string.Empty;
                    for (int i = 0; i < dgv_calibrateData.Rows.Count; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            data += dgv_calibrateData.Rows[i].Cells[j].Value;
                            if (j < 3)
                                data += ",";
                        }
                        data += Environment.NewLine;
                    }
                    File.AppendAllText(dig_saveImage.FileName, data, Encoding.Default);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void ckb_eyeHandCalibrationToolEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (Frm_Main.ignore)
                return;
            Job.GetToolInfoByToolName(jobName, toolName).enable = ckb_eyeHandCalibrationToolEnable.Checked;
        }

    }
}
