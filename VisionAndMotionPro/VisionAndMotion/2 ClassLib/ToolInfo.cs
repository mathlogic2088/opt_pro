using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionAndMotionPro
{

    /// <summary>
    /// 工具信息类
    /// </summary>
    [Serializable]
    public class ToolInfo
    {
        public static List<int> _id = new List<int>();
        public ToolInfo()
        {
            enable = true;
            toolType = ToolType.None;
            toolName = string.Empty;
            tool = new object();
            input = new List<ToolIO>();
            output = new List<ToolIO>();
        }

        public int Id { get; set; }
        public string Name { get { return this.toolName; } set {; } }

        /// <summary>
        /// 工具是否启用
        /// </summary>
        public bool enable;
        /// <summary>
        /// 工具名称
        /// </summary>
        public string toolName;
        /// <summary>
        /// 工具类型
        /// </summary>
        public ToolType toolType;
        /// <summary>
        /// 工具对象
        /// </summary>
        public object tool;
        /// <summary>
        /// 工具描述信息
        /// </summary>
        public string toolTipInfo = string.Empty;
        /// <summary>
        /// 工具输入字典集合
        /// </summary>
        public List<ToolIO> input;
        /// <summary>
        /// 工具输出字典集合
        /// </summary>
        public List<ToolIO> output;


        /// <summary>
        /// 以IO名获取IO对象
        /// </summary>
        /// <param name="IOName"></param>
        /// <returns></returns>
        public ToolIO GetInput(string IOName)
        {
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].IOName == IOName)
                    return input[i];
            }
            return new ToolIO();
        }
        /// <summary>
        /// 以IO名获取IO对象
        /// </summary>
        /// <param name="IOName"></param>
        /// <returns></returns>
        public ToolIO GetOutput(string IOName)
        {
            for (int i = 0; i < output.Count; i++)
            {
                if (output[i].IOName == IOName)
                    return output[i];
            }
            return new ToolIO();
        }
        /// <summary>
        /// 移除工具输入项
        /// </summary>
        /// <param name="IOName"></param>
        public void RemoveInputIO(string IOName)
        {
            for (int i = 0; i < input .Count ; i++)
            {
                if (input[i].IOName == toolName)
                    input.RemoveAt(i);
            }
        }
        /// <summary>
        /// 移除工具输出项
        /// </summary>
        /// <param name="IOName"></param>
        public void RemoveOutputIO(string IOName)
        {
            for (int i = 0; i < output.Count; i++)
            {
                if (output[i].IOName == toolName)
                    output.RemoveAt(i);
            }
        }

    }
}
