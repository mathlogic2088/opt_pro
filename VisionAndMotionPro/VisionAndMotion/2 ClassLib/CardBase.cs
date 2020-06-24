using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VisionAndMotionPro.Properties;

namespace VisionAndMotionPro
{
    /// <summary>
    /// 板卡基类(此类及所有类成员应为非静态，因为一个项目中的板卡数量可能不止一个，所以应为实例类，待后期修改完善)
    /// </summary>
    class CardBase
    {

        /// <summary>
        /// 板卡资源锁
        /// </summary>
        internal static object obj_card = new object();
        /// <summary>
        /// 指示此板卡是否初始化成功
        /// </summary>
        internal static bool initSucceed = false;
        /// <summary>
        /// 正在回零
        /// </summary>
        internal static bool homing = false;
        /// <summary>
        /// 轴集合
        /// </summary>
        protected static List<S_Axis> L_axis = new List<S_Axis>();
        /// <summary>
        /// 输出点集合
        /// </summary>
        protected static List<S_Do> L_do = new List<S_Do>();
        /// <summary>
        /// 输入点集合
        /// </summary>
        protected static List<S_Di> L_di = new List<S_Di>();
        /// <summary>
        /// 表示输入信号的虚拟情况，键表示要虚拟的输入信号，值的第一位为0时表示不虚拟，为1时表示虚拟。值的第二位为0时表示虚拟为高电平，为1时表示虚拟为低电平，为2时表示虚拟为置反
        /// </summary>
        internal static Dictionary<string, string> D_inputSignalVitualStatu = new Dictionary<string, string>();
        /// <summary>
        /// 输出信号的虚拟状态，只在卡为虚拟卡时有意义
        /// </summary>
        internal static Dictionary<string, Level> D_outputSingalVitualStatu = new Dictionary<string, Level>();


        /// <summary>
        /// 绑定轴
        /// </summary>
        /// <param name="actNo">轴映射号</param>
        /// <param name="axisName">轴枚举</param>
        internal static void BindAxis(ushort actNo, object axisName)
        {
            try
            {
                S_Axis axis = new S_Axis();
                axis.actNo = actNo;
                axis.axisName = axisName.ToString();
                L_axis.Add(axis);
                if (!Frm_MotionControl.Instance.cbx_axisName.Items.Contains(axisName.ToString()))
                    Frm_MotionControl.Instance.cbx_axisName.Items.Add(axisName.ToString());
                if (Frm_MotionControl.Instance.cbx_axisName.Items.Count > 0)
                    Frm_MotionControl.Instance.cbx_axisName.SelectedIndex = 0;

                int row = Frm_MotionControl.Instance.dgv_axisInfo.Rows.Add();
                Frm_MotionControl.Instance.dgv_axisInfo.Rows[row].Cells[0].Value = (Frm_MotionControl.Instance.dgv_axisInfo.Rows.Count - 1).ToString();
                Frm_MotionControl.Instance.dgv_axisInfo.Rows[row].Cells[1].Value = axisName.ToString();

                if (!Frm_MotionControl.Instance.dgv_pointList.Columns.Contains(axisName.ToString()))
                    Frm_MotionControl.Instance.dgv_pointList.Columns.Add(axisName.ToString(), axisName.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 绑定通用输入
        /// </summary>
        /// <param name="indexNo">IO编号</param>
        /// <param name="actNo">映射号</param>
        /// <param name="diName">输入枚举</param>
        internal static void BindDi(int indexNo, short actNo, object diName)
        {
            try
            {
                S_Di di = new S_Di();
                di.indexNo = indexNo;
                di.actNo = (short)(actNo);
                di.diName = diName.ToString();
                L_di.Add(di);
                int index = Frm_IO.Instance.dgv_diList.Rows.Add();
                Frm_IO.Instance.dgv_diList.Rows[index].Cells[0].Value = indexNo;
                Frm_IO.Instance.dgv_diList.Rows[index].Cells[1].Value = actNo;
                Frm_IO.Instance.dgv_diList.Rows[index].Cells[2].Value = Resources.Off;
                Frm_IO.Instance.dgv_diList.Rows[index].Cells[2].Tag = "Off";
                Frm_IO.Instance.dgv_diList.Rows[index].Cells[3].Value = diName.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 绑定通用输出
        /// </summary>
        /// <param name="indexNo">IO编号</param>
        /// <param name="actNo">映射号</param>
        /// <param name="doName">输出枚举</param>
        internal static void BindDo(int indexNo, short actNo, object doName)
        {
            try
            {
                S_Do do1 = new S_Do();
                do1.indexNo = indexNo;
                do1.actNo = actNo;
                do1.doName = doName.ToString();
                L_do.Add(do1);
                int index = Frm_IO.Instance.dgv_doList.Rows.Add();
                Frm_IO.Instance.dgv_doList.Rows[index].Cells[0].Value = indexNo;
                Frm_IO.Instance.dgv_doList.Rows[index].Cells[1].Value = actNo;
                Frm_IO.Instance.dgv_doList.Rows[index].Cells[3].Value = Resources.Off;
                Frm_IO.Instance.dgv_doList.Rows[index].Cells[3].Tag = "Off";
                Frm_IO.Instance.dgv_doList.Rows[index].Cells[4].Value = doName.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }
        /// <summary>
        /// 通过轴名称获取轴映射号
        /// </summary>
        /// <param name="axisName">轴枚举</param>
        /// <returns>轴映射号</returns>
        internal static ushort GetAxisIndexByName(object axisName)
        {
            try
            {
                for (int i = 0; i < L_axis.Count; i++)
                {
                    if (L_axis[i].axisName == axisName.ToString())
                        return L_axis[i].actNo;
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return 0;
            }
        }
        /// <summary>
        /// 获取位状态(16位版)
        /// </summary>
        /// <param name="vaule">对象值</param>
        /// <param name="position">第几位</param>
        /// <returns></returns>
        public static bool GetBit16(UInt16 vaule, byte position)
        {
            try
            {
                uint pos = 1;
                pos = pos << position;
                return (vaule & (0xffffffff & pos)) > 1;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return false;
            }
        }
        /// <summary>
        /// 通过输入点枚举获取输入点映射号
        /// </summary>
        /// <param name="diName">输入枚举</param>
        /// <returns>输入映射号</returns>
        protected static short GetDiIndexByName(object diName)
        {
            try
            {
                for (int i = 0; i < L_di.Count; i++)
                {
                    if (L_di[i].diName == diName.ToString())
                        return L_di[i].actNo;
                }
                return -1;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return -1;
            }
        }
        /// <summary>
        /// 通过输出点枚举名获取输出点映射号
        /// </summary>
        /// <param name="doName">输出枚举</param>
        /// <returns>输出映射号</returns>
        protected static short GetDoIndexByName(object doName)
        {
            try
            {
                for (int i = 0; i < L_di.Count; i++)
                {
                    if (L_do[i].doName == doName.ToString())
                        return L_do[i].actNo;
                }
                return -1;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
                return -1;
            }
        }

    }
    internal struct S_Axis
    {
        internal int indexNo;
        internal ushort actNo;
        internal string axisName;
    }
    internal struct S_Di
    {
        internal int indexNo;
        internal short actNo;
        internal string diName;
    }
    internal struct S_Do
    {
        internal int indexNo;
        internal short actNo;
        internal string doName;
    }
    internal enum Level
    {
        High,
        Low,
    }
}
