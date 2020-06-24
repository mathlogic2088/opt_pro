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
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionAndMotionPro
{
    internal partial class Frm_Job : DockContent
    {
        internal Frm_Job()
        {
            InitializeComponent();
            Init_Language();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_Job _instance;
        public static Frm_Job Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_Job();
                return _instance;
            }
        }
        /// <summary>
        /// 运行流程线程
        /// </summary>
        internal Thread th_runJob;


        /// <summary>
        /// 初始化语言
        /// </summary>
        private void Init_Language()
        {
            try
            {
                if (Configuration.language == Language.English)
                {
                    this.Text = "Job Editor";
                    btn_runOnce.Text = "Run Once";
                    btn_runLoop.Text = "Run Loop";
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 作业实时运行
        /// </summary>
        internal void RealTimeRun()
        {
            try
            {
                while (!Frm_Main.isStopRun)
                {
                    Job.RunCurJob();
                    Thread.Sleep(Convert.ToInt16(Configuration.timeBetweenJobRun));
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
   

        internal void pic_deleteJob_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                if (tbc_jobs.TabPages.Count < 1)
                    return;
                Frm_ConfirmBox.Instance.lbl_info.Text = Configuration.language == Language.English ? "Are you sure you want to delete current job?" : "确定要删除当前流程吗？";
                Frm_ConfirmBox.Instance.ShowDialog();
                if (Frm_ConfirmBox.Instance.result == ConfirmBoxResult.Cancel)
                {
                    return;
                }
                string jobName = tbc_jobs.SelectedTab.Text;
                Job.RemoveJobByName(jobName);
                for (int i = 0; i < tbc_jobs.TabPages.Count; i++)
                {
                    if (tbc_jobs.TabPages[i].Text == jobName)
                    {
                        tbc_jobs.TabPages.RemoveAt(i);
                    }
                }
                if (File.Exists(Application.StartupPath + "\\Config\\Vision\\Job\\" + jobName + ".job"))
                    File.Delete(Application.StartupPath + "\\Config\\Vision\\Job\\" + jobName + ".job");
                Frm_Main.Instance.OutputMsg("流程删除成功", Color.Green);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void pic_expandJobTree_Click(object sender, EventArgs e)
        {
            if (tbc_jobs.TabPages.Count < 1)
                return;
            string jobName = tbc_jobs.SelectedTab.Text;
            Job job = Job.GetJobByName(jobName);
            Project .GetJobTree(jobName).ExpandAll();
        }
        private void pic_foldJobTree_Click(object sender, EventArgs e)
        {
            try
            {
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count < 1)
                    return;
                string jobName = tbc_jobs.SelectedTab.Text;
                Job job = Job.GetJobByName(jobName);
                Project .GetJobTree(jobName).CollapseAll();
                job.DrawLine();
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_runJob_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Job.RunCurJob();
            if (Frm_Job.Instance.tbc_jobs.TabPages.Count > 0)
            {
                Job.GetJobByName(Frm_Job.Instance.tbc_jobs.SelectedTab.Text).DrawLine();
            }
        }
        private void btn_jobLoopRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                //Frm_Main.Instance.btn_runOnce.Enabled = false;
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count == 0)
                {
                    Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "No jobs to run" : "没有可运行的流程", Color.Green);
                    return;
                }
                btn_runLoop.Enabled = false;
                btn_runOnce.Enabled = false;
                Application.DoEvents();
                Thread.Sleep(50);
                if (btn_runLoop.Text == (Configuration.language == Language.English ? "Run Loop" : "连续运行"))
                {
                    Frm_Main.isStopRun = false;
                    th_runJob = new Thread(RealTimeRun);
                    th_runJob.IsBackground = true;
                    th_runJob.Start();
                    btn_runLoop.Text = Configuration.language == Language.English ? "Run Loop" : "停止运行";
                    //Frm_Main.Instance.btn_runOnce .Text = "停止运行";
                    //Frm_Main.Instance.btn_runOnce.Enabled = false;
                    //Frm_Main.Instance.btn_runOnce.Enabled = false;
                }
                else
                {
                    Frm_Main.isStopRun = true;
                    Thread.Sleep(20);
                    btn_runLoop.Text = Configuration.language == Language.English ? "Run Loop" : "连续运行";
                    //Frm_Main.Instance.btn_runOnce.Enabled = true;
                    //btn_runOnce.Enabled = true;
                }
                btn_runLoop.Enabled = true;
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        internal void pic_jobInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                if (Frm_Job.Instance.tbc_jobs.TabPages.Count == 0)
                {
                    Frm_Main.Instance.OutputMsg("当前无可用流程，不可打开流程信息页面", Color.Green);
                    return;
                }
                Frm_JobInfo.Instance.tbx_jobName.Text = tbc_jobs.SelectedTab.Text;
                Frm_JobInfo.Instance.ShowDialog();
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_Job_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        private void Frm_Job_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void pic_createJob_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Frm_Main.Instance.Create_New_Job();
        }
        private void tbc_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frm_ImageWindow.Instance.Update_Last_Run_Result_Image_List();
            Frm_Monitor.Instance.dgv_monitor.Rows.Clear();
        }

    }
}
