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
    public partial class Frm_ProcessingItemConfig : Form
    {
        public Frm_ProcessingItemConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_ProcessingItemConfig _instance;
        public static Frm_ProcessingItemConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_ProcessingItemConfig();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static BlobAnalyseTool blobAnalyseTool = new BlobAnalyseTool();


        private void Frm_ProcessingItemConfig_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            tbx_minArea.Text = blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].MinArea.ToString();
            tbx_maxArea.Text = blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].MaxArea.ToString();
            tbx_elementSize.Text = blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].ElementSize.ToString();
        }
        private void btn_applyAndExit_Click(object sender, EventArgs e)
        {
            blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].MinArea = Convert.ToInt32(tbx_minArea.Text);
            blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].MaxArea = Convert.ToInt32(tbx_maxArea.Text);
            blobAnalyseTool.L_prePorcessing[Frm_BlobAnalyseTool.Instance.dgv_processingItem.SelectedRows[0].Index].ElementSize = Convert.ToInt32(tbx_elementSize.Text);
            this.Close();
        }

    }
}
