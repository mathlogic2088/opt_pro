using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    public partial class Frm_LayoutManage : Form
    {
        public Frm_LayoutManage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_LayoutManage _instance;
        public static Frm_LayoutManage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_LayoutManage();
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
                    this.Text = "Layout Manage";
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }


        private void btn_createNewLayout_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 11; i++)
                {
                    if (!File.Exists(Application.StartupPath + "\\Resources\\Layout\\Personal" + i + ".config"))
                    {
                        Frm_Main.Instance.dockPanel.SaveAsXml(Application.StartupPath + "\\Resources\\Layout\\Personal" + i + ".config");
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = index + 1;
                        dataGridView1.Rows[index].Cells[0].Value = "Personal" + i + ".config";
                        cbx_layoutList.Items.Add("Personal" + i + ".config");
                        return;
                    }
                }
                Frm_MessageBox.Instance.MessageBoxShow("自定义布局最多只能添加10个，请删除后再添加");
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_LayoutManage_Load(object sender, EventArgs e)
        {
            try
            {
                string[] files = Directory.GetFiles(Application.StartupPath + "\\Resources\\Layout");
                dataGridView1.Rows.Clear();
                cbx_layoutList.Items.Clear();
                cbx_layoutList.Items.Add("DockPanel.config");
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = "DockPanel.config";
                for (int i = 0; i < files.Length; i++)
                {
                    string fileName = Path.GetFileName(files[i]);
                     index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = fileName;
                    cbx_layoutList.Items.Add(fileName);
                }
                string selectedLayout = Path.GetFileName(Configuration.layoutFilePath);
                cbx_layoutList.Text = selectedLayout;
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_deleteLayout_Click(object sender, EventArgs e)
        {
            try
            {
                string layoutName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string layoutPath = Application.StartupPath + "\\Resources\\Layout\\" + layoutName;
                if (layoutName != "ClassicalLayout1.config" && layoutName != "ClassicalLayout2.config" && layoutName != "DockPanel.config")
                {
                    if (File.Exists(layoutPath))
                        File.Delete(layoutPath);
                    cbx_layoutList.Items.Remove(dataGridView1.SelectedRows[0].Cells[0].Value .ToString ());
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                else
                {
                    Frm_MessageBox.Instance.MessageBoxShow("\r\n此布局文件为系统经典布局，不可删除");
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_layoutList.Text == "DockPanel.config")
                Configuration.layoutFilePath = Application.StartupPath + "\\" + cbx_layoutList.Text;
            else
                Configuration.layoutFilePath = Application.StartupPath + "\\Resources\\Layout\\" + cbx_layoutList.Text;
        }

    }
}
