using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    internal partial class Frm_GeneralSettings : Form
    {
        internal Frm_GeneralSettings()
        {
            InitializeComponent();
            Init_Language();
        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_GeneralSettings _instance;
        public static Frm_GeneralSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_GeneralSettings();
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
                    cbo_lanuage.Items.Clear();
                    cbo_lanuage.Items.Add("Simplified Chinese");
                    cbo_lanuage.Items.Add("English");
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
