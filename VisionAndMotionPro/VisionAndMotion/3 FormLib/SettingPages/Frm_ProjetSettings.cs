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
    internal partial class Frm_ProjetSettings : Form
    {
        internal Frm_ProjetSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_ProjetSettings _instance;
        public static Frm_ProjetSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ProjetSettings();
                return _instance;
            }
        }

    }
}
