using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quobject.SocketIoClientDotNet.Client;
using System.Threading;


namespace Test1
{
    
    static class Program
    {
        public static ApplicationContext ac = new ApplicationContext();
        public static ManualResetEvent ManualResstEvent = new ManualResetEvent(true);
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ac.MainForm = new Login();
            Application.Run(ac);
        }
    }
    
}
