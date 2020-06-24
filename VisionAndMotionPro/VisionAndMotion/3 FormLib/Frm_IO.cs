using csDmc2210;
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
    public partial class Frm_IO : DockContent
    {
        public Frm_IO()
        {
            InitializeComponent();
            Init_Language();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_IO _instance;
        public static Frm_IO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_IO();
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
                    this.Text = "IO Monitor";
                    label1.Text = "Input";
                    label2.Text = "Output";
                    dataGridViewTextBoxColumn1.HeaderText = "Index";
                    dataGridViewTextBoxColumn2.HeaderText = "ActNum";
                    dataGridViewImageColumn1.HeaderText = "Statu";
                    dataGridViewTextBoxColumn3.HeaderText = "Description";

                    dataGridViewTextBoxColumn4.HeaderText = "Index";
                    dataGridViewTextBoxColumn5.HeaderText = "ActNum";
                    dataGridViewButtonColumn1.HeaderText = "Operation";
                    dataGridViewButtonColumn1.Text = "Reverse";
                    dataGridViewImageColumn2.HeaderText = "Statu";
                    dataGridViewTextBoxColumn6.HeaderText = "Description";
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }


        private void dgv_doList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                if (dgv_doList.SelectedRows[0].Index != -1)
                {
                    if (Configuration.cardType == CardType.固高_GTS)
                    {
                        string doName = dgv_doList.SelectedRows[0].Cells[4].Value.ToString();
                        if (Card_Googol.GetDoSts(doName) == Level.Low)
                            Card_Googol.SetDo(doName, Level.High);
                        else
                            Card_Googol.SetDo(doName, Level.Low);
                    }
                    else if (Configuration.cardType == CardType.雷赛_IOC0640)
                    {
                        string doName = dgv_doList.SelectedRows[0].Cells[4].Value.ToString();
                        if (Card_Googol.GetDoSts(doName) == Level.Low)
                            Card_Googol.SetDo(doName, Level.High);
                        else
                            Card_Googol.SetDo(doName, Level.Low);
                    }
                    else if (Configuration.cardType == CardType.雷塞_DMC2210)
                    {
                        string doName = dgv_doList.SelectedRows[0].Cells[4].Value.ToString();
                        if (Card_LeadShineDMC2210.GetDoSts(doName) == Level.Low)
                            Card_LeadShineDMC2210.SetDo(doName, Level.High);
                        else
                            Card_LeadShineDMC2210.SetDo(doName, Level.Low);
                    }
                    else if (Configuration.cardType == CardType.雷塞_DMC2410)
                    {
                        string doName = dgv_doList.SelectedRows[0].Cells[4].Value.ToString();
                        if (Card_LeadShine_DMC2410.GetDoSts(doName) == Level.Low)
                            Card_LeadShine_DMC2410.SetDo(doName, Level.High);
                        else
                            Card_LeadShine_DMC2410.SetDo(doName, Level.Low);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
