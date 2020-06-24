using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using Tool;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace VisionAndMotionPro
{
    /// <summary>
    /// 配置类
    /// </summary>
    [Serializable]
    public static class Configuration
    {

        /// <summary>
        /// Ini读写对象
        /// </summary>
        private static Ini ini = new Ini(Application.StartupPath + "\\Config\\Configuration.ini");
        /// <summary>
        /// 图像窗体隐藏顶部的工具图像列表和底部的功能部分，只显示图像
        /// </summary>
        public static bool hideFuncPart = false;
        /// <summary>
        /// 程序退出时是否保存一次
        /// </summary>
        public static bool saveWhenExit = true;
        /// <summary>
        /// 当前界面布局文件存储路径
        /// </summary>
        public static string layoutFilePath = Configuration.layoutFilePath = Application.StartupPath + "\\Resources\\Layout\\" + "ClassicalLayout1.config";
        /// <summary>
        /// 是否虚拟当前的运动控制卡，便于在非现场调试时使用
        /// </summary>
        public static bool vitualCard = false;
        /// <summary>
        /// 是否锁定窗口布局
        /// </summary>
        public static bool lockLayout = true;
        /// <summary>
        /// 是否启用权限管控
        /// </summary>
        public static bool enablePermissionControl = false;
        /// <summary>
        /// 是否允许改变主窗体大小
        /// </summary>
        public static bool allowResizeForm = true;
        /// <summary>
        /// 通讯方式
        /// </summary>
        public static CommunicationType communicationType = CommunicationType.None;
        /// <summary>
        /// 主窗体高
        /// </summary>
        public static Int32 mainFormHeight = 80;
        /// <summary>
        /// 主窗体宽
        /// </summary>
        public static Int32 mainFormWidth = 100;
        /// <summary>
        /// 程序启动后是否处于生产界面
        /// </summary>
        public static bool showProductionFormAfterStart = false;
        /// <summary>
        /// 程序开启后是否自动最大化
        /// </summary>
        public static bool maxSizeAfterStart = true;
        /// <summary>
        /// 公司名称
        /// </summary>
        private static string _companyName = "VM Pro 让开发变得更简单";
        public static string CompanyName
        {
            get { return Configuration._companyName; }
            set
            {
                Configuration._companyName = value;
                //Frm_Main.Instance.ribbonControl1.TitleText = Configuration._companyName;
                //Frm_Main.Instance.ribbonControl1.Refresh();
            }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        private static string programTitle = (Configuration.language == Language.English ? "Unnamed" : "未命名");
        public static string ProgramTitle
        {
            get { return Configuration.programTitle; }
            set
            {
                Configuration.programTitle = value;
                //Frm_Main.Instance.lbl_title.Text = "Vision & Motion Pro " + "  V " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " _ " + Configuration.ProgramTitle;
                //Frm_Main.Instance.ribbonControl1.Refresh();
            }
        }
        /// <summary>
        /// 是否每次启动程序和关闭程序时自动备份程序
        /// </summary>
        public static bool autoBackupProgram = false;
        /// <summary>
        /// 程序启动后自动锁定
        /// </summary>
        public static bool autoLockAfterStart = false;
        /// <summary>
        /// 管理员密码
        /// </summary>
        public static string adminPassword = "e3afed0047b08059d0fada10f400c1e5";        //默认密码为Admin
        /// <summary>
        /// 开发者密码
        /// </summary>
        public static string developerPassword = "95fc6be00264a94959afb8d8ec6704fc";        //密码默认为likang
        /// <summary>
        /// 当前使用的板卡类型
        /// </summary>
        public static CardType cardType = CardType.无;
        /// <summary>
        /// 语言
        /// </summary>
        public static Language language = Language.Chinese;
        /// <summary>
        /// 开机自启动
        /// </summary>
        public static bool autoStart = false;
        /// <summary>
        /// 程序启动以后自动进行通讯连接
        /// </summary>
        public static bool autoConnectAfterStart = false;
        /// <summary>
        /// 充当客户端时对方IP
        /// </summary>
        public static string remoteIPAsClient = "192.168.0.1";
        /// <summary>
        /// 充当客户端时对方端口号
        /// </summary>
        public static Int32 remotePortAsClient = 10004;
        /// <summary>
        /// 充当服务端时本地IP
        /// </summary>
        public static string localIPAsSever = "192.168.0.1";
        /// <summary>
        /// 充当服务端时本地端口号
        /// </summary>
        public static Int32 localPortAsSever = 10004;
        /// <summary>
        /// 自动运行速度
        /// </summary>
        public static short autoRunVel = 20;
        /// <summary>
        /// 自动运行时速度百分比
        /// </summary>
        public static double autoRunVelRoute = 100;
        /// <summary>
        /// 作业自动运行间隔时间
        /// </summary>
        public static int timeBetweenJobRun = 0;
        /// <summary>
        /// 开机自动运行状态
        /// </summary>
        public static bool switchedToAutoMode = true;
        /// <summary>
        /// 开机后程序自启动
        /// </summary>
        public static bool autoStartAfterStartup = false;
        /// <summary>
        /// 通讯配置项
        /// </summary>
        internal static List<CommunicationItem> L_communicationItemList = new List<CommunicationItem>();
        /// <summary>
        /// 近期打开过的文件
        /// </summary>
        public static List<string> L_recentlyOpendFile = new List<string>();


        /// <summary>
        /// 从本地读取配置项
        /// </summary>
        public static void Read(bool tip = true)
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\Config\\Configuration.ini"))
                {
                    autoBackupProgram = Convert.ToBoolean(ini.IniReadConfig("autoBackup"));
                    autoConnectAfterStart = Convert.ToBoolean(ini.IniReadConfig("AutoConnectAfterStart"));
                    autoRunVel = Convert.ToInt16(ini.IniReadConfig("AutoRunVel"));
                    programTitle = ini.IniReadConfig("ProgramTitle");
                    timeBetweenJobRun = Convert.ToInt16(ini.IniReadConfig("TimeBetweenJobRun"));
                    switchedToAutoMode = Convert.ToBoolean(ini.IniReadConfig("SwitchedToAuto"));
                    language = (Language)Enum.Parse(typeof(Language), ini.IniReadConfig("Language"));
                    cardType = (CardType)Enum.Parse(typeof(CardType), ini.IniReadConfig("CardType"));
                    autoConnectAfterStart = Convert.ToBoolean(ini.IniReadConfig("AutoConnectAfterStart"));
                    CompanyName = ini.IniReadConfig("CompanyName");
                    localIPAsSever = ini.IniReadConfig("LocalIPAsSever");
                    localPortAsSever = Convert.ToInt32(ini.IniReadConfig("LocalPortAsSever"));
                    remoteIPAsClient = ini.IniReadConfig("RemoteIPAsClient");
                    remotePortAsClient = Convert.ToInt32(ini.IniReadConfig("RemotePortAsClient"));
                    showProductionFormAfterStart = Convert.ToBoolean(ini.IniReadConfig("ShowMainForm"));
                    enablePermissionControl = Convert.ToBoolean(ini.IniReadConfig("LoginFree"));
                    communicationType = (CommunicationType)Enum.Parse(typeof(CommunicationType), ini.IniReadConfig("CommuniationMode"));
                    adminPassword = ini.IniReadConfig("AdminPassword");
                    developerPassword = ini.IniReadConfig("DeveloperPassword");
                    autoLockAfterStart = Convert.ToBoolean(ini.IniReadConfig("AutoLock"));
                    mainFormWidth = Convert.ToInt32(ini.IniReadConfig("FormWidth"));
                    mainFormHeight = Convert.ToInt32(ini.IniReadConfig("FormHeight"));
                    maxSizeAfterStart = Convert.ToBoolean(ini.IniReadConfig("MaxSizeAfterStart"));
                    allowResizeForm = Convert.ToBoolean(ini.IniReadConfig("AllowResizeForm"));
                    enablePermissionControl = Convert.ToBoolean(ini.IniReadConfig("EnablePermissionControl"));
                    layoutFilePath = ini.IniReadConfig("CurrentLayout");
                    lockLayout = Convert.ToBoolean(ini.IniReadConfig("LockLayout"));
                    vitualCard = Convert.ToBoolean(ini.IniReadConfig("IsVitualCard"));
                    hideFuncPart = Convert.ToBoolean(ini.IniReadConfig("HideFuncPart"));
                    saveWhenExit = Convert.ToBoolean(ini.IniReadConfig("SaveWhenExit"));
                    autoRunVelRoute = Convert.ToDouble(ini.IniReadConfig("AutoRunVelRoute"));

                    L_recentlyOpendFile.Clear();
                    for (int i = 1; i < 6; i++)
                    {
                        L_recentlyOpendFile.Add(ini.IniReadConfig("RecentlyOpendFile" + i));
                    }

                    string data = ini.IniReadConfig("CommConfigList");
                    L_communicationItemList.Clear();
                    if (data != "")
                    {
                        string[] item = Regex.Split(data, ";");
                        for (int i = 0; i < item.Length; i++)
                        {
                            string[] temp = Regex.Split(item[i], ",");
                            CommunicationItem commConfigItem = new CommunicationItem();
                            commConfigItem.ReceivedCommand = temp[0];
                            commConfigItem.JobName = temp[1];
                            commConfigItem.OutputItem = temp[2];
                            commConfigItem.PrefixStr = temp[3];
                            L_communicationItemList.Add(commConfigItem);
                        }
                    }
                }
                else
                {
                    if (tip)
                        Frm_MessageBox.Instance.MessageBoxShow("\r\n配置文件不存在，将以初始默认参数启动");
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 保存所有配置项
        /// </summary>
        internal static void Save()
        {
            try
            {
                ini.IniWriteConfig("autoBackup", autoBackupProgram.ToString());
                ini.IniWriteConfig("AutoConnectAfterStart", autoConnectAfterStart.ToString());
                ini.IniWriteConfig("AutoRunVel", autoRunVel.ToString());
                ini.IniWriteConfig("ProgramTitle", programTitle);
                ini.IniWriteConfig("TimeBetweenJobRun", timeBetweenJobRun.ToString());
                ini.IniWriteConfig("SwitchedToAuto", switchedToAutoMode.ToString());
                ini.IniWriteConfig("Language", language.ToString());
                ini.IniWriteConfig("CardType", cardType.ToString());
                ini.IniWriteConfig("AutoConnectAfterStart", autoConnectAfterStart.ToString());
                ini.IniWriteConfig("CompanyName", CompanyName);
                ini.IniWriteConfig("LocalIPAsSever", localIPAsSever);
                ini.IniWriteConfig("LocalPortAsSever", localPortAsSever.ToString());
                ini.IniWriteConfig("RemoteIPAsClient", remoteIPAsClient);
                ini.IniWriteConfig("RemotePortAsClient", remotePortAsClient.ToString());
                ini.IniWriteConfig("ShowMainForm", showProductionFormAfterStart.ToString());
                ini.IniWriteConfig("LoginFree", enablePermissionControl.ToString());
                ini.IniWriteConfig("CommuniationMode", communicationType.ToString());
                ini.IniWriteConfig("AdminPassword", adminPassword);
                ini.IniWriteConfig("DeveloperPassword", developerPassword);
                ini.IniWriteConfig("AutoLock", autoLockAfterStart.ToString());
                ini.IniWriteConfig("FormWidth", mainFormWidth.ToString());
                ini.IniWriteConfig("FormHeight", mainFormHeight.ToString());
                ini.IniWriteConfig("MaxSizeAfterStart", maxSizeAfterStart.ToString());
                ini.IniWriteConfig("AllowResizeForm", allowResizeForm.ToString());
                ini.IniWriteConfig("CurrentLayout", layoutFilePath);
                ini.IniWriteConfig("LockLayout", lockLayout.ToString());
                ini.IniWriteConfig("IsVitualCard", vitualCard.ToString());
                ini.IniWriteConfig("EnablePermissionControl", enablePermissionControl.ToString());
                ini.IniWriteConfig("HideFuncPart", hideFuncPart.ToString());
                ini.IniWriteConfig("SaveWhenExit", saveWhenExit.ToString());
                ini.IniWriteConfig("AutoRunVelRoute", autoRunVelRoute.ToString());

                for (int i = 0; i < L_recentlyOpendFile.Count; i++)
                {
                    ini.IniWriteConfig("RecentlyOpendFile" + (i + 1), L_recentlyOpendFile[i]);
                }

                //保存通讯配置项
                string data = string.Empty;
                for (int i = 0; i < L_communicationItemList.Count; i++)
                {
                    data += L_communicationItemList[i].ReceivedCommand + "," + L_communicationItemList[i].JobName + "," + L_communicationItemList[i].OutputItem + "," + L_communicationItemList[i].PrefixStr;
                    if (i != L_communicationItemList.Count - 1)
                        data += ";";
                }
                ini.IniWriteConfig("CommConfigList", data);
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
