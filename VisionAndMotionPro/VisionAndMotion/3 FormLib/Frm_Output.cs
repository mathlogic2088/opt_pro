using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionAndMotionPro
{
    public partial class Frm_Output : DockContent
    {
        public Frm_Output()
        {
            InitializeComponent();
            Init_Language();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_Output _instance;
        public static Frm_Output Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new Frm_Output();
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
                    this.Text = "Output";
                    listView1.Columns[0].Text = "Time";
                    listView1.Columns[1].Text = "Info";
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">信息内容</param>
        /// <param name="color">颜色显示</param>
        public void OutputMsg(string msg, Color color)
        {
            try
            {
                listView1.Columns[1].Width = listView1.Width - listView1.Columns[0].Width-10 ;
                ListViewItem item = new ListViewItem();
                item.Text = DateTime.Now.ToString("HH:mm:ss");
                item.SubItems.Add(msg);
                item.ForeColor = color;
                listView1.Items.Insert(0, item);
                if (listView1.Items.Count > 100)
                    listView1.Items.RemoveAt(100);
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }


        private void Frm_Output_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

    }
}
