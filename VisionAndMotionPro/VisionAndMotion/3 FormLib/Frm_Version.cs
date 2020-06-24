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
    internal partial class Frm_Version : Form
    {
        internal Frm_Version()
        {
            InitializeComponent();
            Init_Language();
        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_Version _instance;
        internal  static Frm_Version Instance
        {
            get
            {
                if (_instance == null||_instance.IsDisposed )
                    _instance = new Frm_Version();
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
                    this.Text = "About";
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }


        private void Frm_Version_Load(object sender, EventArgs e)
        {
            lbl_version.Text = "V "+ System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

    }
}
