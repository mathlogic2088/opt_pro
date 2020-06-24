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
    public partial class Frm_ProcessingItemConfig1 : Form
    {
        public Frm_ProcessingItemConfig1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_ProcessingItemConfig1 _instance;
        public static Frm_ProcessingItemConfig1 Instance
        {
            get
            {
                _instance = new Frm_ProcessingItemConfig1();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static BlobAnalyseTool blobAnalyseTool = new BlobAnalyseTool();


        private void btn_saveAndExit_Click(object sender, EventArgs e)
        {
            blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].ElementType =cbx_elementType.Text;
            blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].ElementSize = Convert.ToInt16(tbx_elementSize.Text);
            this.Close();
        }
        private void Frm_ProcessingItemConfig1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            cbx_elementType.SelectedIndex = 0;
           cbx_elementType.Text = blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].ElementType;
            tbx_elementSize.Text = blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].ElementSize.ToString();
        }

    }
}
