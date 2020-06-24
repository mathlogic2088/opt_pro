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
    public partial class Frm_DistanceLLTool : Form
    {
        public Frm_DistanceLLTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前工具所在的流程
        /// </summary>
        internal static string jobName = string.Empty;
        /// <summary>
        /// 当前工具名
        /// </summary>
        internal static string toolName = string.Empty;
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_DistanceLLTool _instance;
        public static Frm_DistanceLLTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_DistanceLLTool();
                return _instance;
            }
        }

    }
}
