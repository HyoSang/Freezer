using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO.Pipes;
using System.IO;
using IWshRuntimeLibrary;
using System.ServiceModel;
using Microsoft.Win32;


namespace FreezerPCService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        WshShell wsh;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
                String path2 = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\";
                try
                {
                    System.IO.FileStream fs = new System.IO.FileStream(path2 + "FreezerPC.lnk", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                    fs.Close();
                }
                catch (Exception)
                {
                    wsh = new WshShell();
                    IWshRuntimeLibrary.IWshShortcut myShotCut;
                    myShotCut = (IWshRuntimeLibrary.IWshShortcut)wsh.CreateShortcut(path2 + "FreezerPC.lnk");
                    myShotCut.TargetPath = Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine)+"\\FreezerPC.exe";
                    myShotCut.Description = "FreezerPC";
                    myShotCut.Save();
                }
    
        }


        protected override void OnStop()
        {
        }
    }
}
