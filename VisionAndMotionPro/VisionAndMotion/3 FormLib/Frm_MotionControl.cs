using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using VisionAndMotionPro.Properties;

namespace VisionAndMotionPro
{
    public partial class Frm_MotionControl : Form
    {
        public Frm_MotionControl()
        {
            InitializeComponent();
            Init_Language();
            Thread th = new Thread(ShowAxisInfo);
            th.IsBackground = true;
            th.Start();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Frm_MotionControl _instance;
        public static Frm_MotionControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_MotionControl();
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
                    this.Text = "Axis Control";
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 点位运行
        /// </summary>
        /// <param name="obj"></param>
        private void Movement()
        {
            try
            {
                int selectRow = dgv_pointList.SelectedRows[0].Index;
                for (int i = 2; i < dgv_pointList.Columns.Count; i++)
                {
                    string axisName = dgv_pointList.Columns[i].HeaderText.ToString();
                    Int32 targetPos = Convert.ToInt32(dgv_pointList.Rows[selectRow].Cells[i].Value);
                    if (dgv_pointList.Rows[selectRow].Cells[i].Value.ToString() != "NA")
                    {
                        switch (Configuration.cardType)
                        {
                            case CardType.固高_GTS:
                                targetPos = (int)(Convert.ToDouble(dgv_pointList.Rows[selectRow].Cells[i].Value));
                                int axisIndex = Card_Googol.GetAxisIndexByName(axisName);
                                Card_Googol.MoveAbs(axisName, targetPos, (short)(Convert.ToDouble(cbo_moveVel.Text) / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                                break;
                            case CardType.雷塞_DMC2410:
                                axisIndex = Card_LeadShine_DMC2410.GetAxisIndexByName(axisName);
                                Card_LeadShine_DMC2410.MoveAbs(axisName, (int)(targetPos * Axis_Config.Instance.MMPixelRoute[axisIndex]), (int)(Convert.ToDouble(cbo_moveVel.Text) / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                                break;
                            case CardType.雷塞_DMC2210:
                                axisIndex = Card_LeadShineDMC2210.GetAxisIndexByName(axisName);
                                Card_LeadShineDMC2210.MoveAbs(axisName, (int)(targetPos * Axis_Config.Instance.MMPixelRoute[axisIndex]), (int)(Convert.ToDouble(cbo_moveVel.Text) / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 显示各轴信息
        /// </summary>
        private void ShowAxisInfo()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(100);

                    //获取各轴信息
                    for (int i = 0; i < dgv_axisInfo.Rows.Count; i++)
                    {
                        switch (Configuration.cardType)
                        {
                            case CardType.固高_GTS:
                                break;
                            case CardType.雷塞_DMC2210:
                                ushort axisIndex = (ushort)Card_LeadShineDMC2210.GetAxisIndexByName(dgv_axisInfo.Rows[i].Cells[1].Value);

                                double value = Card_LeadShineDMC2210.GetCurPosition(dgv_axisInfo.Rows[i].Cells[1].Value);
                                dgv_axisInfo.Rows[i].Cells[2].Value = value;
                                dgv_axisInfo.Rows[i].Cells[3].Value = value * Axis_Config.Instance.MMPixelRoute[axisIndex];

                                value = Card_LeadShineDMC2210.GetCurEncoder(dgv_axisInfo.Rows[i].Cells[1].Value);
                                dgv_axisInfo.Rows[i].Cells[4].Value = value;
                                dgv_axisInfo.Rows[i].Cells[5].Value = value * Axis_Config.Instance.MMPixelRoute[axisIndex];

                                //获取当前轴专用信号状态
                                ushort axisIndex1 = (ushort)Card_LeadShineDMC2210.GetAxisIndexByName(cbx_axisName);
                                bool b = Card_LeadShineDMC2210.GetMotorStatu(axisIndex1);
                                if (b)
                                    pic_motorOnOrOff.Image = Resources.On;
                                else
                                    pic_motorOnOrOff.Image = Resources.Off;

                                b = Card_LeadShineDMC2210.InNEL(axisIndex1);
                                if (b)
                                    pic_NELStatu.Image = Resources.On;
                                else
                                    pic_NELStatu.Image = Resources.Off;

                                b = Card_LeadShineDMC2210.InHome(axisIndex1);
                                if (b)
                                    pic_ORGStatu.Image = Resources.On;
                                else
                                    pic_ORGStatu.Image = Resources.Off;

                                b = Card_LeadShineDMC2210.InPEL(axisIndex1);
                                if (b)
                                    pic_PELStatu.Image = Resources.On;
                                else
                                    pic_PELStatu.Image = Resources.Off;

                                b = Card_LeadShineDMC2210.GetAlarmStatu(axisIndex1);
                                if (b)
                                    pic_ALMStatu.Image = Resources.On;
                                else
                                    pic_ALMStatu.Image = Resources.Off;
                                break;
                            case CardType.雷塞_DMC2410:
                                axisIndex = (ushort)Card_LeadShine_DMC2410.GetAxisIndexByName(dgv_axisInfo.Rows[i].Cells[1].Value);

                                value = Card_LeadShine_DMC2410.GetCurPosition(dgv_axisInfo.Rows[i].Cells[1].Value);
                                dgv_axisInfo.Rows[i].Cells[2].Value = value;
                                dgv_axisInfo.Rows[i].Cells[3].Value = value * 1.0;

                                value = Card_LeadShine_DMC2410.GetCurEncoder(dgv_axisInfo.Rows[i].Cells[1].Value);
                                dgv_axisInfo.Rows[i].Cells[4].Value = value;
                                dgv_axisInfo.Rows[i].Cells[5].Value = value * 1.0;

                                //获取当前轴专用信号状态
                                axisIndex1 = (ushort)Card_LeadShine_DMC2410.GetAxisIndexByName(cbx_axisName);
                                b = Card_LeadShine_DMC2410.GetMotorStatu(axisIndex1);
                                if (b)
                                    pic_motorOnOrOff.Image = Resources.On;
                                else
                                    pic_motorOnOrOff.Image = Resources.Off;

                                b = Card_LeadShine_DMC2410.InNEL(axisIndex1);
                                if (b)
                                    pic_NELStatu.Image = Resources.On;
                                else
                                    pic_NELStatu.Image = Resources.Off;

                                b = Card_LeadShine_DMC2410.InHome(axisIndex1);
                                if (b)
                                    pic_ORGStatu.Image = Resources.On;
                                else
                                    pic_ORGStatu.Image = Resources.Off;

                                b = Card_LeadShine_DMC2410.InPEL(axisIndex1);
                                if (b)
                                    pic_PELStatu.Image = Resources.On;
                                else
                                    pic_PELStatu.Image = Resources.Off;

                                b = Card_LeadShine_DMC2410.GetAlarmStatu(axisIndex1);
                                if (b)
                                    pic_ALMStatu.Image = Resources.On;
                                else
                                    pic_ALMStatu.Image = Resources.Off;
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }


        private void btn_home_Click(object sender, EventArgs e)
        {
            try
            {
                if (Configuration.cardType == CardType.无)
                {
                    Frm_Main.Instance.OutputMsg("当前项目被认定为无板卡项目，若确实存在板卡，请在[系统]菜单下的[设置]页面中选择对应板卡型号", Color.Red);
                }
                switch (Configuration.cardType)
                {
                    case CardType.固高_GTS:
                        if (!Card_Googol.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可回零");
                            return;
                        }
                        if (cbx_axisName.Text == string.Empty)
                        {
                            Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Please select axis" : "请先选择轴", Color.Red);
                            return;
                        }
                        if (Card_Googol.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        Frm_Main.Instance.OutputMsg("提示：" + cbx_axisName.Text + "轴正在回零中......", Color.Green);
                        string axisName = cbx_axisName.Text;
                        Card_Googol.Home(axisName,
                                         Axis_Config.Instance.回零搜索长度[cbx_axisName.SelectedIndex],
                                         100,
                                         tkb_speed.Value,
                                         Axis_Config.Instance.回零方向[cbx_axisName.SelectedIndex],
                                         Axis_Config.Instance.回退长度[cbx_axisName.SelectedIndex],
                                         1);
                        Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Tip :Home succeed" : "轴回零成功", Color.Green);
                        break;
                    case CardType.雷塞_DMC2210:
                        if (!Card_LeadShineDMC2210.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可回零");
                            return;
                        }
                        if (Card_LeadShineDMC2210.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        if (cbx_axisName.Text == string.Empty)
                        {
                            Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Please select axis" : "请先选择轴", Color.Red);
                            return;
                        }
                        Frm_Main.Instance.OutputMsg("提示：" + cbx_axisName.Text + "轴正在回零中......", Color.Green);
                        ushort axisIndex = (ushort)Card_LeadShineDMC2210.GetAxisIndexByName(cbx_axisName.Text);
                        Card_LeadShineDMC2210.Home(axisIndex,
                                        Axis_Config.Instance.回零速度[axisIndex] / Axis_Config.Instance.MMPixelRoute[axisIndex],
                                        Axis_Config.Instance.回零方向[axisIndex],
                                      (int)(Axis_Config.Instance.回退长度[axisIndex] / Axis_Config.Instance.MMPixelRoute[axisIndex])
                                       );
                        Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Tip :Home succeed" : "轴回零成功", Color.Green);
                        break;
                    case CardType.雷塞_DMC2410:
                        if (!Card_LeadShine_DMC2410.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可回零");
                            return;
                        }
                        if (Card_LeadShine_DMC2410.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        if (cbx_axisName.Text == string.Empty)
                        {
                            Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Please select axis" : "请先选择轴", Color.Red);
                            return;
                        }
                        Frm_Main.Instance.OutputMsg("提示：" + cbx_axisName.Text + "轴正在回零中......", Color.Green);
                        axisIndex = (ushort)Card_LeadShineDMC2210.GetAxisIndexByName(cbx_axisName.Text);
                        Card_LeadShine_DMC2410.Home(axisIndex,
                                      Axis_Config.Instance.回零速度[axisIndex] / Axis_Config.Instance.MMPixelRoute[axisIndex],
                                        Axis_Config.Instance.回零方向[axisIndex],
                                      (int)(Axis_Config.Instance.回退长度[axisIndex] / Axis_Config.Instance.MMPixelRoute[axisIndex]),
                                        1);
                        Frm_Main.Instance.OutputMsg(Configuration.language == Language.English ? "Tip :Home succeed" : "轴回零成功", Color.Green);
                        break;
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_moveForeward_Click(object sender, EventArgs e)
        {
            try
            {
                if (Configuration.cardType == CardType.无)
                {
                    Frm_Main.Instance.OutputMsg("当前项目被认定为无板卡项目，若确实存在板卡，请在[系统]菜单下的[设置]页面中选择对应板卡型号", Color.Red);
                }
                switch (Configuration.cardType)
                {
                    case CardType.固高_GTS:
                        if (!Card_Googol.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可操作");
                            return;
                        }
                        if (Card_Googol.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        if (cbx_axisName.Text == string .Empty )
                        {
                            Frm_Main.Instance.OutputMsg("Tip:Please select axis", Color.Red);
                            return;
                        }
                        if (rdo_jog.Checked)         //点动
                        {
                            double curPos = Card_Googol.GetCurPosition(cbx_axisName.Text);
                            double offset = Convert.ToDouble(cbo_moveDistance.Text) * 1000;
                            double targetPos = curPos + offset;
                            Card_Googol.MoveAbs(cbx_axisName.Text, Convert.ToInt32(targetPos), (short)tkb_speed.Value);
                        }
                        else        //连续
                        {

                        }
                        break;

                    case CardType.雷塞_DMC2210:
                        int axisIndex = Card_LeadShineDMC2210.GetAxisIndexByName(cbx_axisName);

                        if (!Card_LeadShineDMC2210.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可操作");
                            return;
                        }
                        if (Card_LeadShineDMC2210.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        if (cbx_axisName.Text == string .Empty )
                        {
                            Frm_Main.Instance.OutputMsg("Tip:Please select axis", Color.Red);
                            return;
                        }
                        if (rdo_jog.Checked)         //点动
                        {
                            double distance = Convert.ToDouble(cbo_moveDistance.Text.Trim());
                            Card_LeadShineDMC2210.MoveRel(cbx_axisName.Text, (int)distance, (int)(tkb_speed.Value / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        }
                        else        //连续
                        {
                            Card_LeadShineDMC2210.KeepMove(cbx_axisName.Text, 1, (short)(tkb_speed.Value / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        }
                        break;
                    case CardType.雷塞_DMC2410:
                        axisIndex = Card_LeadShine_DMC2410.GetAxisIndexByName(cbx_axisName);
                        if (!Card_LeadShine_DMC2410.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可操作");
                            return;
                        }
                        if (Card_LeadShine_DMC2410.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        if (cbx_axisName.Text == string .Empty )
                        {
                            Frm_Main.Instance.OutputMsg("Tip:Please select axis", Color.Red);
                            return;
                        }
                        if (rdo_jog.Checked)         //点动
                        {
                            double distance = Convert.ToDouble(cbo_moveDistance.Text.Trim());
                            Card_LeadShine_DMC2410.MoveRel(cbx_axisName.Text, (int)(distance / Axis_Config.Instance.MMPixelRoute[axisIndex]), (short)(tkb_speed.Value / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        }
                        else        //连续
                        {
                            Card_LeadShineDMC2210.KeepMove(cbx_axisName.Text, 1, (short)(tkb_speed.Value / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_moveBackward_Click(object sender, EventArgs e)
        {
            try
            {
                if (Configuration.cardType == CardType.无)
                {
                    Frm_Main.Instance.OutputMsg("当前项目被认定为无板卡项目，若确实存在板卡，请在[系统]菜单下的[设置]页面中选择对应板卡型号", Color.Red);
                }
                switch (Configuration.cardType)
                {
                    case CardType.固高_GTS:
                        int axisIndex = Card_Googol.GetAxisIndexByName(cbx_axisName);
                        if (!Card_Googol.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可操作");
                            return;
                        }
                        if (Card_Googol.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        if (cbx_axisName.Text == string .Empty )
                        {
                            Frm_Main.Instance.OutputMsg("Tip:Please select axis", Color.Red);
                            return;
                        }
                        double curPos = Card_Googol.GetCurPosition(cbx_axisName.Text);
                        double offset = Convert.ToDouble(cbo_moveDistance.Text) * 1000;
                        double targetPos = curPos - offset;
                        Card_Googol.MoveAbs(cbx_axisName.Text, Convert.ToInt32(targetPos), (short)(tkb_speed.Value / Axis_Config.Instance.MMPixelRoute[axisIndex]));
                        break;

                    case CardType.雷塞_DMC2210:
                        axisIndex = Card_LeadShineDMC2210.GetAxisIndexByName(cbx_axisName);
                        if (!Card_LeadShineDMC2210.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可操作");
                            return;
                        }
                        if (Card_LeadShineDMC2210.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        if (cbx_axisName.Text == string .Empty )
                        {
                            Frm_Main.Instance.OutputMsg("Tip:Please select axis", Color.Red);
                            return;
                        }
                        if (rdo_jog.Checked)         //点动
                        {
                            double distance = Convert.ToDouble(cbo_moveDistance.Text.Trim());
                            Card_LeadShineDMC2210.MoveRel(cbx_axisName.Text, -(int)distance, (int)(tkb_speed.Value / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        }
                        else
                        {
                            Card_LeadShineDMC2210.KeepMove(cbx_axisName.Text, 0, -(int)(tkb_speed.Value / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        }
                        break;
                    case CardType.雷塞_DMC2410:
                        axisIndex = Card_LeadShine_DMC2410.GetAxisIndexByName(cbx_axisName);
                        if (!Card_LeadShine_DMC2410.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可操作");
                            return;
                        }
                        if (Card_LeadShine_DMC2410.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        if (cbx_axisName.Text == string .Empty )
                        {
                            Frm_Main.Instance.OutputMsg("Tip:Please select axis", Color.Red);
                            return;
                        }
                        if (rdo_jog.Checked)         //点动
                        {
                            double distance = Convert.ToDouble(cbo_moveDistance.Text.Trim());
                            Card_LeadShine_DMC2410.MoveRel(cbx_axisName.Text, -(int)(distance / Axis_Config.Instance.MMPixelRoute[axisIndex]), (int)(tkb_speed.Value / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        }
                        else
                        {
                            Card_LeadShineDMC2210.KeepMove(cbx_axisName.Text, 0, -(int)(tkb_speed.Value / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_touch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_pointList.SelectedRows.Count == 0)
                {
                    Frm_Main.Instance.OutputMsg("请先选中点表中的具体行后删除", Color.Red);
                    return;
                }
                switch (Configuration.cardType)
                {
                    case CardType.固高_GTS:
                        if (!Card_Googol.initSucceed && !Configuration.vitualCard)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可示教");
                            return;
                        }
                        break;

                    case CardType.雷塞_DMC2210:
                        if (!Card_LeadShineDMC2210.initSucceed && !Configuration.vitualCard)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可示教");
                            return;
                        }
                        Frm_ConfirmBox.Instance.lbl_info.Text = Configuration.language == Language.English ? "Are you sure you want to overwrite the point data?" : "确定要覆盖此点？";
                        Frm_ConfirmBox.Instance.ShowDialog();
                        if (Frm_ConfirmBox.Instance.result == ConfirmBoxResult.Cancel)
                        {
                            return;
                        }
                        int selectRow = dgv_pointList.SelectedRows[0].Index;
                        for (int i = 2; i < dgv_pointList.Columns.Count; i++)
                        {
                            double curPos = Card_LeadShineDMC2210.GetCurPosition(dgv_pointList.Columns[i].HeaderText);
                            dgv_pointList.Rows[selectRow].Cells[i].Value = curPos;
                        }
                        break;
                    case CardType.雷塞_DMC2410:
                        if (!Card_LeadShine_DMC2410.initSucceed && !Configuration.vitualCard)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可示教");
                            return;
                        }
                        Frm_ConfirmBox.Instance.lbl_info.Text = Configuration.language == Language.English ? "Are you sure you want to overwrite the point data?" : "确定要覆盖此点？";
                        Frm_ConfirmBox.Instance.ShowDialog();
                        if (Frm_ConfirmBox.Instance.result == ConfirmBoxResult.Cancel)
                        {
                            return;
                        }
                        selectRow = dgv_pointList.SelectedRows[0].Index;
                        for (int i = 2; i < dgv_pointList.Columns.Count; i++)
                        {
                            double curPos = Card_LeadShine_DMC2410.GetCurPosition(dgv_pointList.Columns[i].HeaderText);
                            dgv_pointList.Rows[selectRow].Cells[i].Value = curPos;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_appear_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_pointList.SelectedRows.Count == 0)
                {
                    Frm_Main.Instance.OutputMsg("请先选中点表中的具体行后删除", Color.Red);
                    return;
                }
                switch (Configuration.cardType)
                {
                    case CardType.固高_GTS:
                        if (!Card_Googol.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可重现");
                            return;
                        }
                        if (Card_Googol.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        break;

                    case CardType.雷塞_DMC2210:
                        if (!Card_LeadShineDMC2210.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可重现");
                            return;
                        }
                        if (Card_LeadShineDMC2210.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        break;
                    case CardType.雷塞_DMC2410:
                        if (!Card_LeadShine_DMC2410.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可重现");
                            return;
                        }
                        if (Card_LeadShine_DMC2410.homing)
                        {
                            Frm_Main.Instance.OutputMsg("当前轴正在回零，请回零完成后操作", Color.Red);
                            return;
                        }
                        break;
                }

                if (Convert.ToDouble(cbo_moveVel.Text) < 0 || Convert.ToDouble(cbo_moveVel.Text) > 100)
                {
                    Frm_MessageBox.Instance.MessageBoxShow("\r\n速度输入不合法，已自动变更为默认值：5");
                    cbo_moveVel.Text = "5";
                }
                Movement();
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
                if (File.Exists(Application.StartupPath + "\\Config\\Motion\\Point.xml"))
                {
                    File.Delete(Application.StartupPath + "\\Config\\Motion\\Point.xml");
                }
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(xmlSM);
                XmlElement root = xmlDoc.CreateElement("", "PointList", "");
                xmlDoc.AppendChild(root);
                for (int i = 0; i < dgv_pointList.Rows.Count - 1; i++)
                {
                    string pointName = dgv_pointList.Rows[i].Cells[1].Value.ToString();
                    XmlElement pointElement = xmlDoc.CreateElement(pointName);
                    XmlNode node = root.AppendChild(pointElement);
                    for (int j = 2; j < dgv_pointList.Columns.Count; j++)
                    {
                        string axisName = dgv_pointList.Columns[j].HeaderText;
                        node = pointElement.AppendChild(xmlDoc.CreateElement("", axisName, ""));
                        if (dgv_pointList.Rows[i].Cells[j].Value == null)
                            dgv_pointList.Rows[i].Cells[j].Value = string.Empty;
                        node.InnerText = dgv_pointList.Rows[i].Cells[j].Value.ToString();
                    }
                }
                Application.DoEvents();
                xmlDoc.Save(Application.StartupPath + "\\Config\\Motion\\Point.xml");
                Frm_MessageBox.Instance.MessageBoxShow("\r\n保存成功");
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_pointList.SelectedRows.Count == 0)
                {
                    Frm_Main.Instance.OutputMsg("请先选中点表中的具体行后删除", Color.Red);
                    return;
                }
                int selectRow = dgv_pointList.SelectedRows[0].Index;
                dgv_pointList.Rows.RemoveAt(selectRow);

                //自动保存一下
                if (!File.Exists(Application.StartupPath + "\\Config\\Motion\\Point.xml"))
                {
                    File.Create(Application.StartupPath + "\\Config\\Motion\\Point.xml").Close();
                }
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(xmlSM);
                XmlElement root = xmlDoc.CreateElement("", "Root", "");
                xmlDoc.AppendChild(root);
                for (int i = 0; i < dgv_pointList.Rows.Count - 1; i++)
                {
                    string pointName = dgv_pointList.Rows[i].Cells[1].Value.ToString();
                    XmlElement pointElement = xmlDoc.CreateElement(pointName);
                    root.AppendChild(pointElement);
                    for (int j = 2; j < dgv_pointList.Columns.Count; j++)
                    {
                        string axisName = dgv_pointList.Rows[i].Cells[j].Value.ToString();
                        XmlNode node = pointElement.AppendChild(xmlDoc.CreateElement("", axisName, ""));
                        node.InnerText = dgv_pointList.Rows[i].Cells[j].Value.ToString();
                    }
                }
                Application.DoEvents();
                xmlDoc.Save(Application.StartupPath + "\\Config\\Motion\\Point.xml");
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lbl_speed.Text = tkb_speed.Value + "mm/s";
        }
        private void Frm_Axis_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void Frm_AxisControl_Load(object sender, EventArgs e)
        {
            dgv_pointList.Rows[0].Cells[0].Value = 1;
            this.TopMost = true;
        }
        private void btn_axisSetting_Click(object sender, EventArgs e)
        {
            Frm_AxisSetting.Instance.ShowDialog();
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgv_pointList.Rows[dgv_pointList.Rows.Count - 1].Cells[0].Value = dgv_pointList.Rows.Count;
        }
        private void btn_stopMove_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Configuration.cardType)
                {
                    case CardType.固高_GTS:
                        break;

                    case CardType.雷塞_DMC2210:
                        ushort axisIndex = (ushort)Card_LeadShineDMC2210.GetAxisIndexByName(cbx_axisName.Text);
                        Card_LeadShineDMC2210.DecStop(axisIndex);
                        break;
                    case CardType.雷塞_DMC2410:
                        axisIndex = (ushort)Card_LeadShine_DMC2410.GetAxisIndexByName(cbx_axisName.Text);
                        Card_LeadShine_DMC2410.DecStop(axisIndex);
                        break;
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void lnk_motorOnOrOff_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                switch (Configuration.cardType)
                {
                    case CardType.固高_GTS: if (!Card_Googol.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可操作");
                            return;
                        }
                        if (pic_motorOnOrOff.Image == Resources.On)
                        {
                            Card_Googol.MotorOff(cbx_axisName.Text);
                            pic_motorOnOrOff.Image = Resources.Off;
                        }
                        else
                        {
                            Card_Googol.MotorOn(cbx_axisName.Text);
                            pic_motorOnOrOff.Image = Resources.On;
                        }
                        break;

                    case CardType.雷塞_DMC2210:
                        if (!Card_LeadShineDMC2210.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可操作");
                            return;
                        }
                        if (pic_motorOnOrOff.Tag.ToString() == "On")
                        {
                            pic_motorOnOrOff.Tag = "Off";
                            Card_LeadShineDMC2210.MotorOff(cbx_axisName.Text);
                            pic_motorOnOrOff.Image = Resources.Off;
                        }
                        else
                        {
                            pic_motorOnOrOff.Tag = "On";
                            Card_LeadShineDMC2210.MotorOn(cbx_axisName.Text);
                            pic_motorOnOrOff.Image = Resources.On;
                        }
                        break;
                    case CardType.雷塞_DMC2410:
                        if (!Card_LeadShine_DMC2410.initSucceed)
                        {
                            Frm_MessageBox.Instance.MessageBoxShow("\r\n未识别到相应运动控制卡，不可操作");
                            return;
                        }
                        if (pic_motorOnOrOff.Image == Resources.On)
                        {
                            Card_LeadShine_DMC2410.MotorOff(cbx_axisName.Text);
                            pic_motorOnOrOff.Image = Resources.Off;
                        }
                        else
                        {
                            Card_LeadShine_DMC2410.MotorOn(cbx_axisName.Text);
                            pic_motorOnOrOff.Image = Resources.On;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_moveToPoint_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Configuration.cardType)
                {
                    case CardType.雷塞_DMC2210:
                        int axisIndex = Card_LeadShineDMC2210.GetAxisIndexByName(cbx_axisName.Text);
                        Card_LeadShineDMC2210.MoveAbs(axisIndex, (int)Convert.ToInt16(tbx_targetPos.Text.Trim()), (int)(Convert.ToInt16(cbo_moveVel.Text) / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        break;
                    case CardType.雷塞_DMC2410:
                        axisIndex = Card_LeadShine_DMC2410.GetAxisIndexByName(cbx_axisName.Text);
                        Card_LeadShine_DMC2410.MoveAbs(axisIndex, (int)Convert.ToInt16(tbx_targetPos.Text.Trim()), (int)(Convert.ToInt16(cbo_moveVel.Text) / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        break;
                }
                tbx_targetPos.Clear();
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }
        private void btn_moveToPointMM_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Configuration.cardType)
                {
                    case CardType.雷塞_DMC2210:
                        int axisIndex = Card_LeadShineDMC2210.GetAxisIndexByName(cbx_axisName.Text);
                        Card_LeadShineDMC2210.MoveAbs(axisIndex, (int)(Convert.ToDouble(tbx_targetPos.Text.Trim()) * Axis_Config.Instance.MMPixelRoute[axisIndex]), (int)(Convert.ToDouble(cbo_moveVel.Text) / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        break;
                    case CardType.雷塞_DMC2410:
                        axisIndex = Card_LeadShine_DMC2410.GetAxisIndexByName(cbx_axisName.Text);
                        Card_LeadShine_DMC2410.MoveAbs(axisIndex, (int)(Convert.ToDouble(tbx_targetPos.Text.Trim()) / Axis_Config.Instance.MMPixelRoute[axisIndex]), (int)(Convert.ToDouble(cbo_moveVel.Text) / Axis_Config.Instance.MMPixelRoute[axisIndex]), false);
                        break;
                }
                tbx_targetPos.Clear();
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}

