using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class OutputTool : ToolBase 
    {

        /// <summary>
        /// 用于CodeEditTool添加的输出项，用于显示在界面和发送到第三方通讯设备
        /// </summary>
        internal Dictionary<string, string> D_OutputItem = new Dictionary<string, string>();
        /// <summary>
        /// 输出项列表
        /// </summary>
        internal List<string> L_outputItem = new List<string>();


        /// <summary>
        /// 运行工具
        /// </summary>
        public  override void Run(string jobName, bool updateImage, bool b)
        {
            runStatu = (Configuration.language == Language.English ? ToolRunStatu.Not_Succeed : ToolRunStatu.失败);
            //此工具没有要运行的实体
            runStatu = (Configuration.language == Language.English ? ToolRunStatu.Succeed : ToolRunStatu.成功);
        }

    }
}
