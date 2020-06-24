using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionAndMotionPro._1_ToolLib._25_ConditionTool
{
    internal partial class Frm_ConditionTool : Frm_ToolBase
    {
        internal Frm_ConditionTool()
        {
            InitializeComponent();
            this.cbx_sucess_change.SelectedIndex = 1;
            this.cbx_fail_change.SelectedIndex = 1;
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_ConditionTool _instance;
        public static Frm_ConditionTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ConditionTool();
                return _instance;
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        public string GetResult()
        {
            var ret = true;
            var retString = string.Empty;
            //run input1 
            if(ret)
            {
                if( 0 == this.cbx_sucess_change.SelectedIndex)
                {
                    //跳转
                    retString = this.cbx_sucess_item.SelectedValue.ToString();
                }
            }
            else
            {
                if(0 == this.cbx_fail_change.SelectedIndex)
                {
                    retString = this.cbx_fail_item.SelectedValue.ToString();
                }
            }

            return retString;
        }

        private void ChangeDataBinding(ComboBox cbx, List<ToolInfo> l_toolList)
        {
            cbx.DataSource = null;

            cbx.Items.Clear();

            cbx.DataSource = l_toolList;

            cbx.DisplayMember = "Name";
            cbx.ValueMember = "Id";
        }
        internal void InitData(List<ToolInfo> l_toolList)
        {            
            this.ChangeDataBinding(this.cbx_sucess_item, l_toolList);
            this.ChangeDataBinding(this.cbx_fail_item, new List<ToolInfo>(l_toolList));
        }
    }
}
