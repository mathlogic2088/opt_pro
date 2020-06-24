using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class LabelTool:ToolBase 
    {
        /// <summary>
        /// 输入项和值
        /// </summary>
        internal Dictionary<string, string> D_inputItemAndVlaue = new Dictionary<string, string>();
        /// <summary>
        /// 标签项集合
        /// </summary>
        internal List<Label> L_label = new List<Label>();


        /// <summary>
        /// 保存数据
        /// </summary>
        internal void SaveData2()
        {
            try
            {
                if (Frm_Main.ignore)
                    return;
                L_label.Clear();
                for (int i = 0; i < Frm_LabelTool.Instance.dgv_outputItem.Rows.Count - 1; i++)
                {
                    if (Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[2].Value != null
                    && Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[3].Value != null
                    && Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[4].Value != null
                    && Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[5].Value != null
                    && Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[6].Value != null
                    && Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[7].Value != null)
                    {
                        if (Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[0].Value == null)
                            continue;
                        Label label = new Label();
                        label.OutputItem = Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[0].Value.ToString();
                        label.PreAddStr = Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[1].Value == null ? "" : Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[1].Value.ToString();
                        label.Row = Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[2].Value.ToString();
                        label.Col = Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[3].Value.ToString();
                        label.Incolor = Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[6].Value.ToString();
                        label.Size = Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[8].Value.ToString();
                        label.DownLimit = Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[4].Value.ToString();
                        label.UpLimit = Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[5].Value.ToString();
                        label.OutColor = Frm_LabelTool.Instance.dgv_outputItem.Rows[i].Cells[7].Value.ToString();
                        label.ValueType = "Value";      //表示可以比较大小的值类型
                        L_label.Add(label);
                    }
                }

                for (int i = 0; i < Frm_LabelTool.Instance.dgv_outputItem2.Rows.Count - 1; i++)
                {
                    if (Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[0].Value != null
                  && Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[2].Value != null
                   && Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[3].Value != null
                   && Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[4].Value != null
                   && Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[5].Value != null
                   && Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[6].Value != null
                   && Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[7].Value != null)
                    {
                        if (Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[0].Value == null)
                            continue;
                        Label label = new Label();
                        label.OutputItem = Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[0].Value.ToString();
                        label.PreAddStr = Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[1].Value == null ? "" : Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[1].Value.ToString();
                        label.Row = Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[2].Value.ToString();
                        label.Col = Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[3].Value.ToString();
                        label.Incolor = Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[5].Value.ToString();
                        label.Size = Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[7].Value.ToString();
                        label.OutColor = Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[6].Value.ToString();
                        label.ExpectValue = Frm_LabelTool.Instance.dgv_outputItem2.Rows[i].Cells[4].Value.ToString();
                        label.ValueType = "Str";      //表示字符串类型
                        L_label.Add(label);
                    }
                }
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

        /// <summary>
        /// 运行工具
        /// </summary>
        public override void Run(string jobName, bool updateImage, bool b)
        {
            try
            {
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败;
                for (int i = 0; i < L_label.Count; i++)
                {
                    Frm_ImageWindow.Instance.set_display_font(GetWindowHandle(jobName), Convert.ToInt16(L_label[i].Size), "nomo", "true", "false");
                   
                    if (L_label[i].ValueType == "Value")        //数值类
                    {
                        double value = Convert.ToDouble(D_inputItemAndVlaue[L_label[i].OutputItem]);
                        double downLimit = Convert.ToDouble(L_label[i].DownLimit);
                        double upLimit = Convert.ToDouble(L_label[i].UpLimit);
                        if (value >= downLimit && value <= upLimit)
                        {
                            Frm_Main.Instance.disp_message(GetWindowHandle(jobName),
                                                         L_label[i].PreAddStr + D_inputItemAndVlaue[L_label[i].OutputItem],
                                                         new HTuple("image"),
                                                         new HTuple(Convert.ToInt32(L_label[i].Row)),
                                                         new HTuple(Convert.ToInt32(L_label[i].Col)),
                                                         new HTuple(L_label[i].Incolor),
                                                         new HTuple("false"));
                        }
                        else
                        {
                            Frm_Main.Instance.disp_message(GetWindowHandle(jobName),
                                                         L_label[i].PreAddStr + D_inputItemAndVlaue[L_label[i].OutputItem],
                                                         new HTuple("image"),
                                                         new HTuple(Convert.ToInt32(L_label[i].Row)),
                                                         new HTuple(Convert.ToInt32(L_label[i].Col)),
                                                         new HTuple(L_label[i].OutColor),
                                                         new HTuple("false"));
                        }
                    }
                    else
                    {
                        if (L_label[i].ExpectValue == D_inputItemAndVlaue[L_label[i].OutputItem])
                        {
                            Frm_Main.Instance.disp_message(GetWindowHandle(jobName),
                                                         L_label[i].PreAddStr + D_inputItemAndVlaue[L_label[i].OutputItem],
                                                         new HTuple("image"),
                                                         new HTuple(Convert.ToInt32(L_label[i].Row)),
                                                         new HTuple(Convert.ToInt32(L_label[i].Col)),
                                                         new HTuple(L_label[i].Incolor),
                                                         new HTuple("false"));
                        }
                        else
                        {
                            Frm_Main.Instance.disp_message(GetWindowHandle(jobName),
                                                         L_label[i].PreAddStr + D_inputItemAndVlaue[L_label[i].OutputItem],
                                                         new HTuple("image"),
                                                         new HTuple(Convert.ToInt32(L_label[i].Row)),
                                                         new HTuple(Convert.ToInt32(L_label[i].Col)),
                                                         new HTuple(L_label[i].OutColor),
                                                         new HTuple("false"));

                        }
                    }
                }
                runStatu = Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功;
            }
            catch (Exception ex)
            {
               LogHelper.SaveErrorInfo(ex);
            }
        }

    }
}
