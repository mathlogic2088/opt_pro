using Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VisionAndMotionPro;
using System.Diagnostics;

namespace Start
{
    static class Program
    {
        /// <summary>
        /// 应用程序的/>".'
        /// 主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                System.Threading.Mutex mutex = new System.Threading.Mutex(false, "ThisShouldOnlyRunOnce");
                bool running = true;
                try
                {
                     running = !mutex.WaitOne(0, false);            //这一句有可能会报错，所以要Try起来
                }
                catch { }

                //此处首先读取一次配置，因为程序启动时就需要知道当前语言选择，用于下面的提示信息的语言类型
                Ini ini = new Ini(Application.StartupPath + @"\Config\Configuration.ini");
                string language = ini.IniReadConfig("Language");
                if (language != string.Empty)
                    Configuration.language = (Language)System.Enum.Parse(typeof(Language), language);

                if (running)
                {
                    DialogResult result = MessageBox.Show(Configuration.language == Language.English ? "An instance has already been run (or the old instance has not been completely closed). Do you want to open another instance?" : "已经运行了一个实例（或旧实例尚未完全关闭），是否再开启一个实例？", Configuration.language == Language.English ? "Tip" : "提示", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                        VM.Init();
                }
                else
                {
                    VM.Init();
                }
            }
            catch
            {
                MessageBox.Show(Configuration.language == Language.English ? "Startup failed" : "启动失败（错误代码：0101）");
            }
        }
    }
}
