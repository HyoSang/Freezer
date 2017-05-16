using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium.IE;
using System.Diagnostics;

namespace Test1
{
    
    public partial class Form1 : Form
    {
        IWebDriver driver=null;
        Thread URLT;
        Thread ProcessT;
        public Form1()
        {
            InitializeComponent();
        }

        private void StopURL_Click(object sender, EventArgs e)
        {
            URLT.Abort();
        }

        private void StartURL_Click(object sender, EventArgs e)
        {
            URLT = new Thread(new ThreadStart(URLThread));
            URLT.Start();
        }

        private void Chrome_Click(object sender, EventArgs e)
        {
            var service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
            service.HideCommandPromptWindow = true;
            driver = new ChromeDriver(service);
            driver.Url = "http://www.naver.com";
        }

        private void AddURL_Button_Click(object sender, EventArgs e)
        {
            URLList.Items.Add(AddURL_TextBox.Text);
        }

        private void AddProcess_Button_Click(object sender, EventArgs e)
        {
            ProcessList.Items.Add(AddProcess_TextBox.Text);
        }

        private void StartProcess_Click(object sender, EventArgs e)
        {
            
            ProcessT = new Thread(new ThreadStart(ProcessThread));
            ProcessT.Start();
        }

        private void StopProcess_Click(object sender, EventArgs e)
        {
            ProcessT.Abort();
        }


        void URLThread()
        {
            while (true)
            {
                for (int i = 0; i < URLList.Items.Count; i++)
                {
                    if (driver == null) continue;
                    if (driver.Url.Contains(URLList.Items[i].ToString()))
                    {
                        driver.Close();
                        driver = null;
                        break;
                    }
                }
                Thread.Sleep(100);
            }
        }

        void ProcessThread()
        {
            while (true)
            {
                Process[] pro = Process.GetProcesses();
                Process c_pro = Process.GetCurrentProcess();
                if (pro.Length != 0)
                {
                    for (int i = 0; i < pro.Length; i++)
                    {
                        if (pro[i].MainWindowTitle.Equals("")) continue;
                        if (pro[i].MainWindowTitle.Equals("FreezerPC")) continue;
                        int count = 0;
                        for(int j=0;j<ProcessList.Items.Count;j++)
                        {
                            if (!pro[i].ProcessName.Equals(ProcessList.Items[j].ToString())) count++;
                        }
                        if (count == ProcessList.Items.Count) pro[i].Kill();
                        
                    }
                    Thread.Sleep(100);
                }
            }
        }


    }

    


}
