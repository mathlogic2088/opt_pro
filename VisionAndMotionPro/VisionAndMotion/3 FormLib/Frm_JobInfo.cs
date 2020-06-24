using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HalconDotNet;

namespace VisionAndMotionPro
{
    public partial class Frm_JobInfo : Form
    {
        public Frm_JobInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_JobInfo _instance;
        public static Frm_JobInfo Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_JobInfo();
                return _instance;
            }
        }


        /// <summary>
        /// 初始化语言
        /// </summary>
        private void Init_Language()
        {
            try
            {
                if (Configuration.language == Language.English)
                {
                    this.Text = "Job Info";
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 加载标准图片
        /// </summary>
        internal void Load_Standard_Image()
        {
            try
            {
                string[] files = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\Config\\Vision\\StandardImage", "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".bmp") || s.EndsWith(".tif")).ToArray();
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count > 0)
                    Job.D_standardImage.Clear();
                Frm_JobInfo.Instance.cbx_standardImage.Items.Clear();
                Frm_SubImageTool.Instance.cbx_standardImage.Items.Clear();
                for (int i = 0; i < files.Length; i++)
                {
                    string ImageName = Path.GetFileName(files[i]);
                    HObject image;
                    Application.DoEvents();
                    try
                    {
                        HOperatorSet.ReadImage(out image, files[i]);
                    }
                    catch
                    {
                        continue;
                    }
                   // if (Frm_Job.Instance.tbc_jobs.TabPages.Count > 0)
                        Job.D_standardImage.Add(ImageName, image);
                    Frm_JobInfo.Instance.cbx_standardImage.Items.Add(ImageName);
                    Frm_SubImageTool.Instance.cbx_standardImage.Items.Add(ImageName);
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                //修改流程名
                if (tbx_jobName.Text.Trim() != Frm_Job.Instance.tbc_jobs.SelectedTab.Text)
                {
                    Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text).jobName = tbx_jobName.Text.Trim();
                    Frm_Main.Save();
                }

                Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text).imageWindowName = cbx_imageWindowList.SelectedItem.ToString();
                Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text).debugImageWindow = comboBox1.SelectedItem.ToString();
                this.Close();
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_deleteStandardImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbx_standardImage.Text == string .Empty )
                    return;
                string fileName = Application.StartupPath + "\\Config\\Vision\\StandardImage\\" + cbx_standardImage.Text;
                File.Delete(fileName);
                Job.D_standardImage.Remove(cbx_standardImage.Text.Trim());
                Frm_SubImageTool.Instance.cbx_standardImage.Items.Remove(cbx_standardImage.Text);
                cbx_standardImage.Items.RemoveAt(cbx_standardImage.SelectedIndex);
                if (cbx_standardImage.Items.Count > 0)
                {
                    cbx_standardImage.SelectedIndex = 0;
                    HObject image = Job.D_standardImage[cbx_standardImage.Text];
                    Frm_ImageWindow.Instance.Display_Image(image);
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_displayCurrentStandImage_Click(object sender, EventArgs e)
        {
            try
            {
                HObject image;
                image = Job.D_standardImage[Frm_JobInfo.Instance.cbx_standardImage.Text];
                Frm_ImageWindow.Instance.Display_Image(image);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_JobInfo_Load(object sender, EventArgs e)
        {
            try
            {
                Load_Standard_Image();
           
                //获取编辑界面所有的窗体控件
                cbx_imageWindowList.Items.Clear();
                if (!cbx_imageWindowList.Items.Contains("不绑定"))
                    cbx_imageWindowList.Items.Add("不绑定");
                if (!comboBox1.Items.Contains("不绑定"))
                    comboBox1.Items.Add("不绑定");

                if (cbx_standardImage.Items.Count > 0)
                    cbx_standardImage.SelectedIndex = 0;
                if (cbx_imageWindowList.Items.Count > 0)
                    cbx_imageWindowList.SelectedIndex = 0;

                foreach (Control item in Frm_UserForm.Instance.Controls)
                {
                    if (item.GetType().ToString() == "System.Windows.Forms.PictureBox")
                    {
                        cbx_imageWindowList.Items.Add(item.Name.ToString());
                    }
                }
                cbx_imageWindowList.Text = Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text).imageWindowName;
                if (cbx_imageWindowList.Text == "")
                {
                    cbx_imageWindowList.SelectedIndex = 0;
                }
                comboBox1.Text = Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text).debugImageWindow;
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
