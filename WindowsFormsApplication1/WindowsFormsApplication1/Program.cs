using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication1
{
    
    static class Program
    {
       
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 1&& File.Exists(args[0]))
                Application.Run(new Form1(args[0]));
            else if (args.Length == 2 && File.Exists(args[1]))
                Application.Run(new Form1(args[0],args[1]));
            else Application.Run(new Form1());
          
        }
    }
}
