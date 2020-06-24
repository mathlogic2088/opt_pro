using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HalconDotNet;
using Tool;

namespace VisionAndMotionPro
{
    internal partial class Frm_communicateConfig : Form
    {
        internal Frm_communicateConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Frm_communicateConfig _instance;
        public static Frm_communicateConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_communicateConfig();
                return _instance;
            }
        }


        private void dgv_communicationConfig_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                //判断相应的列
                if (dgv.CurrentCell.GetType().Name == "DataGridViewComboBoxCell" && dgv.CurrentCell.RowIndex != -1)
                {
                    //给这个DataGridViewComboBoxCell加上下拉事件
                    (e.Control as ComboBox).SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgv_communicationConfig.CurrentCell.ColumnIndex == 2)
                {
                    DataGridViewComboBoxCell jobNameCell = (DataGridViewComboBoxCell)(this.dgv_communicationConfig.Rows[dgv_communicationConfig.CurrentCell.RowIndex].Cells[dgv_communicationConfig.CurrentCell.ColumnIndex]);
                    string jobName = (jobNameCell.EditedFormattedValue.ToString());
                    DataGridViewComboBoxCell outputItemCell = (DataGridViewComboBoxCell)(this.dgv_communicationConfig.Rows[dgv_communicationConfig.CurrentCell.RowIndex].Cells[dgv_communicationConfig.CurrentCell.ColumnIndex + 1]);
                    for (int i = 0; i < Job.GetJobByName(jobName).L_toolList.Count; i++)
                    {
                        if (Job.GetJobByName(jobName).L_toolList[i].toolType == ToolType.Output)
                        {
                            int resultCount = Job.GetJobByName(jobName).L_toolList[i].input.Count;
                            outputItemCell.Items.Clear();
                            for (int j = 0; j < resultCount; j++)
                            {
                                outputItemCell.Items.Add(Job.GetJobByName(jobName).L_toolList[i].input [j].IOName .Substring(3));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void dgv_communicationConfig_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                dgv_communicationConfig.Rows[dgv_communicationConfig.Rows.Count - 1].Cells[0].Value = (dgv_communicationConfig.Rows.Count);
                DataGridViewComboBoxCell jobCell = (DataGridViewComboBoxCell)(this.dgv_communicationConfig.Rows[dgv_communicationConfig.Rows.Count - 1].Cells[2]);
                for (int i = 0; i < Frm_Job.Instance.tbc_jobs.TabPages.Count; i++)
                {
                    if (!jobCell.Items.Contains(Frm_Job.Instance.tbc_jobs.TabPages[i].Text))
                        jobCell.Items.Add(Frm_Job.Instance.tbc_jobs.TabPages[i].Text);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration.L_communicationItemList.Clear();
                for (int i = 0; i < dgv_communicationConfig.Rows.Count - 1; i++)
                {
                    DataGridViewTextBoxCell receiveStrCell = (DataGridViewTextBoxCell)(this.dgv_communicationConfig.Rows[i].Cells[1]);
                    string receiveStr = (receiveStrCell.EditedFormattedValue.ToString());

                    DataGridViewComboBoxCell jobNameCell = (DataGridViewComboBoxCell)(this.dgv_communicationConfig.Rows[i].Cells[2]);
                    string jobName = (jobNameCell.EditedFormattedValue.ToString());

                    DataGridViewComboBoxCell outputItemCell = (DataGridViewComboBoxCell)(this.dgv_communicationConfig.Rows[i].Cells[3]);
                    string outputItem = (outputItemCell.EditedFormattedValue.ToString());

                    DataGridViewTextBoxCell addValueCell = (DataGridViewTextBoxCell)(this.dgv_communicationConfig.Rows[i].Cells[4]);
                    string addValue = (addValueCell.EditedFormattedValue.ToString());

                    CommunicationItem commConfig = new CommunicationItem();
                    commConfig.ReceivedCommand = receiveStr;
                    commConfig.JobName = jobName;
                    commConfig.OutputItem = outputItem;
                    commConfig.PrefixStr = addValue;
                    Configuration.L_communicationItemList.Add(commConfig);
                }
                switch (cbx_communcationType.SelectedIndex)
                {
                    case 0:
                        Configuration.communicationType = CommunicationType.None;
                        break;
                    case 1:
                        Configuration.communicationType = CommunicationType.Internet_Client;
                        break;
                    case 2:
                        Configuration.communicationType = CommunicationType.Internet_Sever;
                        break;
                    case 3:
                        Configuration.communicationType = CommunicationType.SerialPort;
                        break;
                    case 4:
                        Configuration.communicationType = CommunicationType.IO;
                        break;
                }
                Frm_MessageBox.Instance.MessageBoxShow(Configuration.language == Language.English ? "Save successfully" : "\r\n保存成功");
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_deleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_communicationConfig.CurrentRow == null || dgv_communicationConfig.Rows.Count == 0)
                    return;
                string receivedStr = dgv_communicationConfig.CurrentRow.Cells[1].Value.ToString();
                for (int i = 0; i < Configuration.L_communicationItemList.Count; i++)
                {
                    if (Configuration.L_communicationItemList[i].ReceivedCommand == receivedStr)
                    {
                        Configuration.L_communicationItemList.RemoveAt(i);
                    }
                }
                dgv_communicationConfig.Rows.RemoveAt(dgv_communicationConfig.CurrentRow.Index);
                //序号重新排序
                for (int i = 0; i < dgv_communicationConfig.Rows.Count; i++)
                {
                    dgv_communicationConfig.Rows[i].Cells[0].Value = i + 1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_communicateConfig_Shown(object sender, EventArgs e)
        {
            try
            {
                if (dgv_communicationConfig.Rows.Count == 1)
                {
                    dgv_communicationConfig.Rows[dgv_communicationConfig.Rows.Count - 1].Cells[0].Value = (dgv_communicationConfig.Rows.Count);
                    DataGridViewComboBoxCell jobCell = (DataGridViewComboBoxCell)(this.dgv_communicationConfig.Rows[dgv_communicationConfig.Rows.Count - 1].Cells[2]);
                    for (int i = 0; i < Frm_Job.Instance.tbc_jobs.TabPages.Count; i++)
                    {
                        if (!jobCell.Items.Contains(Frm_Job.Instance.tbc_jobs.TabPages[i].Text))
                            jobCell.Items.Add(Frm_Job.Instance.tbc_jobs.TabPages[i].Text);
                    }
                }
                if (Configuration.L_communicationItemList.Count <= 0)
                    return;
                dgv_communicationConfig.Rows.Clear();
                dgv_communicationConfig.Rows.Add(Configuration.L_communicationItemList.Count);
                for (int i = 0; i < Configuration.L_communicationItemList.Count; i++)
                {
                    string receiveStr = Configuration.L_communicationItemList[i].ReceivedCommand;
                    string jobName = Configuration.L_communicationItemList[i].JobName;
                    string outputItem = Configuration.L_communicationItemList[i].OutputItem;
                    string addValue = Configuration.L_communicationItemList[i].PrefixStr;
                    this.dgv_communicationConfig.Rows[i].Cells[0].Value = i + 1;
                    DataGridViewTextBoxCell receiveStrCell = (DataGridViewTextBoxCell)(this.dgv_communicationConfig.Rows[i].Cells[1]);
                    receiveStrCell.Value = receiveStr;
                    DataGridViewComboBoxCell jobNameCell = (DataGridViewComboBoxCell)(this.dgv_communicationConfig.Rows[i].Cells[2]);
                    if (!jobNameCell.Items.Contains(jobName))
                    {
                        jobNameCell.Items.Add(jobName);
                    }
                    jobNameCell.Value = jobName;
                    DataGridViewComboBoxCell outputItemCell = (DataGridViewComboBoxCell)(this.dgv_communicationConfig.Rows[i].Cells[3]);
                    outputItemCell.Items.Add(outputItem);
                    outputItemCell.Value = outputItem;
                    DataGridViewTextBoxCell addStrCell = (DataGridViewTextBoxCell)(this.dgv_communicationConfig.Rows[i].Cells[4]);
                    addStrCell.Value = addValue.Substring(0, addValue.Length);
                }
                for (int i = 0; i < this.dgv_communicationConfig.Rows.Count; i++)
                {
                    DataGridViewComboBoxCell jobCell = (DataGridViewComboBoxCell)(this.dgv_communicationConfig.Rows[i].Cells[2]);
                    for (int j = 0; j < Frm_Job.Instance.tbc_jobs.TabPages.Count; j++)
                    {
                        if (!jobCell.Items.Contains(Frm_Job.Instance.tbc_jobs.TabPages[j].Text))
                            jobCell.Items.Add(Frm_Job.Instance.tbc_jobs.TabPages[j].Text);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        private void Frm_communicateConfig_Load(object sender, EventArgs e)
        {
            cbx_communcationType.SelectedIndex = Convert.ToInt16(Configuration.communicationType);
        }

    }
}
