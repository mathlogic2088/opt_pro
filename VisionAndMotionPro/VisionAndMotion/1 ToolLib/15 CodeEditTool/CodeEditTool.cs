using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.IO;

namespace VisionAndMotionPro
{
    [Serializable]
    internal class CodeEditTool : ToolBase
    {
        internal CodeEditTool()
        {
            sourceCode =
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApplication
{
    public class TestClass
    {
        static void Main(string[] args)
        {
            string Output1,Output2,Output3;
            //Add code here

            

            Console.WriteLine(Output1);
        }
    }
}";
        }
        /// <summary>
        /// 源码
        /// </summary>
        internal string sourceCode = string.Empty;
        /// <summary>
        /// 输入项1
        /// </summary>
        internal string Input1 = string.Empty;
        /// <summary>
        /// 输入项2
        /// </summary>
        internal string Input2 = string.Empty;
        /// <summary>
        /// 输入项3
        /// </summary>
        internal string Input3 = string.Empty;
        /// <summary>
        /// 输入项4
        /// </summary>
        internal string Input4 = string.Empty;
        /// <summary>
        /// 输入项5
        /// </summary>
        internal string Input5 = string.Empty;
        /// <summary>
        /// 输出项1
        /// </summary>
        internal string Output1 = string.Empty;
        /// <summary>
        /// 输出项2
        /// </summary>
        internal string Output2 = string.Empty;
        /// <summary>
        /// 输出项3
        /// </summary>
        internal string Output3 = string.Empty;
        /// <summary>
        /// 编译结果
        /// </summary>
        internal string compileResult = string.Empty;


        /// <summary>
        /// 运行工具
        /// </summary>
        public override void Run(string jobName, bool updateImage, bool b)
        {
            try
            {
                runStatu = ToolRunStatu.Not_Succeed;

                //解析输入
                string sourceCodeAfter = sourceCode.Replace("Input1", "\"" + Input1 + "\"").Replace("Input2", Input2).Replace("Input3", Input3).Replace("Input4", Input4).Replace("Input5", Input5);

                new Runner().CompileAndRun(sourceCodeAfter, out compileResult);
                compileResult = compileResult.Substring(0, compileResult.Length - 2);
                Output1 = compileResult;
                runStatu = ToolRunStatu.Succeed;
            }
            catch (Exception ex)
            {
                LogHelper.SaveErrorInfo(ex);
            }
        }

    }

}
