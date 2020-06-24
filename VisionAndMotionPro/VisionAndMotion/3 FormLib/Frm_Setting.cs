using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tool;

namespace VisionAndMotionPro
{
    internal partial class Frm_Setting : Form
    {
        internal Frm_Setting()
        {
            InitializeComponent();
            Init_Language();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_Setting _instance;
        public static Frm_Setting Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_Setting();
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
                    this.Text = "Axis Control";
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }


        private void Frm_Setting_Load(object sender, EventArgs e)
        {
            try
            {
                tvw_setting.SelectedNode = tvw_setting.Nodes[0];

                Frm_GeneralSettings.Instance.tbx_companyName.Text = Configuration.CompanyName;
                Frm_GeneralSettings.Instance.cbo_lanuage.Text = Configuration.language == Language.English ? "English" : "简体中文";

                Frm_ProjetSettings.Instance.tbx_programTitle.Text = Configuration.ProgramTitle;
                Frm_ProjetSettings.Instance.cbx_cardType.Text = Configuration.cardType.ToString();
                Frm_ProjetSettings.Instance.ckb_vitualCard.Checked = Configuration.vitualCard;

                Frm_RunSettings.Instance.tbx_autoRunVel.Text = Configuration.autoRunVel.ToString();
                Frm_RunSettings.Instance.tbx_jobsRunPouseTime.Text = Configuration.timeBetweenJobRun.ToString();

                Frm_StartSetting.Instance.ckb_autoConnect.Checked = Configuration.autoConnectAfterStart;
                Frm_StartSetting.Instance.ckb_switchedToAutoRunMode.Checked = Configuration.switchedToAutoMode;
                Frm_StartSetting.Instance.ckb_autoStartAfterStartup.Checked = Configuration.autoStart;
                Frm_StartSetting.Instance.ckb_autoBackup.Checked = Configuration.autoBackupProgram;
                Frm_StartSetting.Instance.ckb_autoLock.Checked = Configuration.autoLockAfterStart;
                Frm_StartSetting.Instance.ckb_maxSizeAfterStart.Checked = Configuration.maxSizeAfterStart;
                Frm_StartSetting.Instance.ckb_showMainForm.Checked = Configuration.showProductionFormAfterStart;
                Frm_StartSetting.Instance.ckb_allowResizeFormSize.Checked = Configuration.allowResizeForm;
                Frm_StartSetting.Instance.ckb_hideFuncPart.Checked = Configuration.hideFuncPart;
                Frm_StartSetting.Instance.ckb_saveWhileExit.Checked = Configuration.saveWhenExit;

                Frm_UserManager.Instance.ckb_loginFree.Checked = Configuration.enablePermissionControl;
                Frm_UserManager.Instance.ckb_enablePermissionControl.Checked = Configuration.enablePermissionControl;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_saveSetting_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Configuration.autoBackupProgram = Frm_StartSetting.Instance.ckb_autoBackup.Checked;
                Configuration.autoConnectAfterStart = Frm_StartSetting.Instance.ckb_autoConnect.Checked;
                Configuration.autoRunVel = (short)Convert.ToInt32(Frm_RunSettings.Instance.tbx_autoRunVel.Text);
                Configuration.ProgramTitle = Frm_ProjetSettings.Instance.tbx_programTitle.Text.Trim();
                Configuration.timeBetweenJobRun = Convert.ToInt32(Frm_RunSettings.Instance.tbx_jobsRunPouseTime.Text);
                Configuration.switchedToAutoMode = Frm_StartSetting.Instance.ckb_switchedToAutoRunMode.Checked;
                Configuration.language = Frm_GeneralSettings.Instance.cbo_lanuage.SelectedIndex == 0 ? Language.Chinese : Language.English;
                Configuration.cardType = Frm_ProjetSettings.Instance.cbx_cardType.Text == string.Empty ? CardType.无 : (CardType)Enum.Parse(typeof(CardType), Frm_ProjetSettings.Instance.cbx_cardType.Text);
                Configuration.autoConnectAfterStart = Frm_StartSetting.Instance.ckb_autoConnect.Checked;
                Configuration.CompanyName = Frm_GeneralSettings.Instance.tbx_companyName.Text.Trim();
                Configuration.showProductionFormAfterStart = Frm_StartSetting.Instance.ckb_showMainForm.Checked;
                Configuration.enablePermissionControl = Frm_UserManager.Instance.ckb_loginFree.Checked;
                Configuration.autoLockAfterStart = Frm_StartSetting.Instance.ckb_autoLock.Checked;
                Configuration.maxSizeAfterStart = Frm_StartSetting.Instance.ckb_maxSizeAfterStart.Checked;
                Configuration.enablePermissionControl = Frm_UserManager.Instance.ckb_enablePermissionControl.Checked;
                if (Frm_StartSetting.Instance.ckb_autoStartAfterStartup.Checked != Configuration.autoStartAfterStartup)
                {
                    if (Frm_StartSetting.Instance.ckb_autoStartAfterStartup.Checked)
                        Frm_Main.Auto_Start(!Configuration.autoStartAfterStartup);
                    else
                        Frm_Main.Auto_Start(!Configuration.autoStartAfterStartup);
                }
                Configuration.autoStartAfterStartup = Frm_StartSetting.Instance.ckb_autoStartAfterStartup.Checked;
                Configuration.allowResizeForm = Frm_StartSetting.Instance.ckb_allowResizeFormSize.Checked;
                Configuration.vitualCard = Frm_ProjetSettings.Instance.ckb_vitualCard.Checked;
                Configuration.hideFuncPart = Frm_StartSetting.Instance.ckb_hideFuncPart.Checked;
                Configuration.saveWhenExit = Frm_StartSetting.Instance.ckb_saveWhileExit.Checked;

                Configuration.Save();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void tvw_setting_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode node = tvw_setting.SelectedNode;
                switch (node.Text)
                {
                    case "启动":
                        pnl_window.Controls.Clear();
                        Frm_StartSetting.Instance.TopLevel = false;
                        Frm_StartSetting.Instance.Parent = pnl_window;
                        Frm_StartSetting.Instance.Show();
                        break;
                    case "项目":
                        pnl_window.Controls.Clear();
                        Frm_ProjetSettings.Instance.TopLevel = false;
                        Frm_ProjetSettings.Instance.Parent = pnl_window;
                        Frm_ProjetSettings.Instance.Show();
                        break;
                    case "运行":
                        pnl_window.Controls.Clear();
                        Frm_RunSettings.Instance.TopLevel = false;
                        Frm_RunSettings.Instance.Parent = pnl_window;
                        Frm_RunSettings.Instance.Show();
                        break;
                    case "常规":
                        pnl_window.Controls.Clear();
                        Frm_GeneralSettings.Instance.TopLevel = false;
                        Frm_GeneralSettings.Instance.Parent = pnl_window;
                        Frm_GeneralSettings.Instance.Show();
                        break;
                    case "用户管理":
                        pnl_window.Controls.Clear();
                        Frm_UserManager.Instance.TopLevel = false;
                        Frm_UserManager.Instance.Parent = pnl_window;
                        Frm_UserManager.Instance.Show();
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
