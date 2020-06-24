using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Tool;

namespace VisionAndMotionPro
{
    internal partial class Frm_Login : Form
    {
        internal Frm_Login()
        {
            InitializeComponent();
            Init_Language();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_Login _instance;
        public static Frm_Login Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_Login();
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
                    this.Text = "Login";
                    cbx_user.Items.Clear();
                    cbx_user.Items.AddRange(new string[] { "Developer", "Admin", "Operator", "Password-free Login" });
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 延迟界面不卡死函数，慎用，会大量消耗CPU
        /// </summary>
        /// <param name="pinterval">时长</param>
        private void Delay(double interval)
        {
            try
            {
                DateTime time = DateTime.Now;
                double span = interval * 10000;
                while ((DateTime.Now.Ticks - time.Ticks) < span)
                {
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }


        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                //去注释后可以免登录
                //////this.Hide();
                //////Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;
                //////Permission.CurrentPermission = PermissionLevel.Developer;
                //////Machine.SwitchToDebugForm();

                if (cbx_user.Text == (Configuration.language == Language.English ? "Developer" : "开发人员"))
                {
                    if (Method.GetMD5(tbx_password.Text.Trim()) == Configuration.developerPassword)
                    {
                        this.Hide();
                        Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;
                        Permission.CurrentPermission = PermissionLevel.Developer;
                        LogHelper.SaveLog(LogType.Operate, "用户登录成功，当前用户：Developer");
                        Machine.SwitchToDebugForm();
                    }
                    else
                    {
                        tbx_password.Clear();
                        tbx_password.Focus();
                        lbl_tip.ForeColor = Color.Red;
                        lbl_tip.Text = "提示：作者密码输入错误，请重新输入";
                        Delay(5000);
                        lbl_tip.ForeColor = Color.Black;
                        Application.DoEvents();
                        lbl_tip.Text = "";
                    }
                }
                else if (cbx_user.Text == (Configuration.language == Language.English ? "Admin" : "管理员"))
                {
                    string currentMD5 = Tool.Method.GetMD5(tbx_password.Text.Trim());
                    if (currentMD5 == Configuration.adminPassword)
                    {
                        this.Hide();
                        Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;
                        LogHelper.SaveLog(LogType.Operate, "用户登录成功，当前用户：Admin");
                        Permission.CurrentPermission = PermissionLevel.Admin;
                        Machine.SwitchToDebugForm();
                    }
                    else
                    {
                        tbx_password.Clear();
                        tbx_password.Focus();
                        lbl_tip.ForeColor = Color.Red;
                        lbl_tip.Text = Configuration.language == Language.English ? "Tips : Administrator password is wrong, please enter again" : "管理员密码输入有误，请重新输入";
                        Delay(5000);
                        lbl_tip.ForeColor = Color.Black;
                        lbl_tip.Text = "";
                    }
                }
                else if (cbx_user.Text == (Configuration.language == Language.English ? "Admin" : "操作员"))
                {
                    string currentMD5 = Tool.Method.GetMD5(tbx_password.Text.Trim());
                    if (currentMD5 == Configuration.adminPassword)
                    {
                        this.Hide();
                        Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;
                        Permission.CurrentPermission = PermissionLevel.Operator;
                        LogHelper.SaveLog(LogType.Operate, "用户登录成功，当前用户：Operator");
                        Machine.SwitchToDebugForm();
                    }
                    else
                    {
                        tbx_password.Clear();
                        tbx_password.Focus();
                        lbl_tip.ForeColor = Color.Red;
                        lbl_tip.Text = Configuration.language == Language.English ? "Tips : Administrator password is wrong, please enter again" : "管理员密码输入有误，请重新输入";
                        Delay(5000);
                        lbl_tip.ForeColor = Color.Black;
                        lbl_tip.Text = "";
                    }
                }
                else
                {
                    this.Hide();
                    Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;
                    Permission.CurrentPermission = PermissionLevel.NoPermission;
                    Machine.SwitchToDebugForm();
                }
                tbx_password.Clear();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void cbx_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbx_password.Focus();
        }
        private void Frm_Login_Shown(object sender, EventArgs e)
        {
            cbx_user.SelectedIndex = 0;
            tbx_password.Select();
        }
        private void btn_logout_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Frm_ImageWindow.Instance.hwc_imageWindow.ContextMenuStrip = Frm_ImageWindow.Instance.cnt_rightClickMenu;
                Permission.CurrentPermission = PermissionLevel.NoPermission;
                Machine.SwitchToDebugForm();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            this.lbl_companyName.Text = Configuration.CompanyName;
        }

    }
}
