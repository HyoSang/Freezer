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
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;



namespace Test1
{
    
    public partial class Form1 : Form
    {
        List<IWebDriver> driver= new List<IWebDriver>();
        List<int> webProcessID = new List<int>();
        Thread URLT;
        Thread ProcessT;
        Socket socket;
        

        public Form1(string ID)
        {
            InitializeComponent();
            LoginID.Text = ID;
            socket = IO.Socket("http://localhost:8005");
            socket.On("start", (data) =>
            {
                URLT = new Thread(new ThreadStart(URLThread));
                URLT.Start();
                Lock.Visible = true;
                unLock.Visible = false;
                LightChange(URL_red, URL_green, true);

            });
            socket.On("end", (data) =>
            {
                URLT.Abort();
                Lock.Visible = false;
                unLock.Visible = true;
                LightChange(URL_red, URL_green, false);
            });
            socket.On("disconnect", (data) =>
            {
                LightChange(server_red, server_green, false);
            });
            socket.On("connect", (data) =>
            {
                LightChange(server_red, server_green, true);
                socket.Emit("setSocket", LoginID.Text);
            });

            BrowerList.Items.Add("Chrome");
            BrowerList.SelectedIndex = 0;

            try
            {
                System.IO.FileStream fs = new System.IO.FileStream("URL.dat", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                System.IO.StreamReader reader = new System.IO.StreamReader(fs, System.Text.Encoding.Default);
                while (!reader.EndOfStream)
                {
                    URLList.Items.Add(reader.ReadLine());
                }
            }
            catch (System.IO.FileNotFoundException) { }

        }

        private void StopURL_Click(object sender, EventArgs e)
        {
            URLT.Abort();
            Lock.Visible = false;
            unLock.Visible = true;
            
        }

        private void StartURL_Click(object sender, EventArgs e)
        {
            
        }

        private void Chrome_Click(object sender, EventArgs e)
        {
            if (BrowerList.SelectedIndex == 0)
            {
                var service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
                int count = driver.Count;
                service.HideCommandPromptWindow = true;
                driver.Add(new ChromeDriver(service));
                webProcessID.Add(service.ProcessId);
                driver[count].Url = "http://www.naver.com";
                                                
            }
            else
            {
                MessageBox.Show("올바른 브라우저를 선택하세요.");
            }
        }

        private void AddURL_Button_Click(object sender, EventArgs e)
        {
            URLList.Items.Add(AddURL_TextBox.Text);
            AddURL_TextBox.Clear();
            System.IO.FileStream fs = new System.IO.FileStream("URL.dat", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
            for(int i=0;i<URLList.Items.Count;i++)
            {
                writer.WriteLine(URLList.Items[i].ToString());
            }

            writer.Close();
        }
        
        private void AddProcess_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "실행 파일(*.exe)|*.exe";
            fd.Title = "등록할 실행파일을 선택 해 주세요.";
            DialogResult ret = STAShowDialog(fd);
            if (ret == DialogResult.OK)
            {
                String path = fd.FileName;
                AddProcess_TextBox.Text = path;
            }
        }

        private void StartProcess_Click(object sender, EventArgs e)
        {
            
            ProcessT = new Thread(new ThreadStart(ProcessThread));
            ProcessT.Start();
            Lock.Visible = true;
            unLock.Visible = false;
            

        }

        private void StopProcess_Click(object sender, EventArgs e)
        {
            ProcessT.Abort();
            Lock.Visible = false;
            unLock.Visible = true;
        }


        void URLThread()
        {
            while (true)
            {
                if (driver.Count == 0) continue;
                for (int j = 0; j < driver.Count; j++)
                {
                    try
                    {
                        if (driver[j].WindowHandles.Count >= 2)
                        {
                            if (Process.GetProcessById(webProcessID[j]).ProcessName.Equals("chromedriver"))
                            {
                                driver[j].SwitchTo().Window(driver[j].WindowHandles[1]);
                                var url = driver[j].Url;
                                var service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
                                int count = driver.Count;
                                service.HideCommandPromptWindow = true;
                                driver.Add(new ChromeDriver(service));
                                webProcessID.Add(service.ProcessId);
                                driver[count].Url = url;
                                driver[j].Close();
                                driver[j].SwitchTo().Window(driver[j].WindowHandles[0]);
                                driver[count].SwitchTo().Window(driver[count].CurrentWindowHandle);
                            }
                         }
                    }
                    catch (NullReferenceException) { }
                        
                        for (int i = 0; i < URLList.Items.Count; i++)
                        {
                            try
                            {
                                if (driver[j].Url.Contains(URLList.Items[i].ToString()))
                                {
                                    driver[j].Quit();
                                    driver.RemoveAt(j);
                                    webProcessID.RemoveAt(j);
                                    j--;
                                }
                            }
                            catch (NoSuchWindowException)
                            {
                                driver.RemoveAt(j);
                                Process.GetProcessById(webProcessID[j]).Kill();
                                webProcessID.RemoveAt(j);
                            }
                            catch (NullReferenceException)
                            {
                                driver.RemoveAt(j);
                                Process.GetProcessById(webProcessID[j]).Kill();
                                webProcessID.RemoveAt(j);
                            }
                            catch (InvalidOperationException)
                            {
                                driver.RemoveAt(j);
                                Process.GetProcessById(webProcessID[j]).Kill();
                                webProcessID.RemoveAt(j);
                            }
                            catch (WebDriverException)
                            {
                                driver.RemoveAt(j);
                                Process.GetProcessById(webProcessID[j]).Kill();
                                webProcessID.RemoveAt(j);
                            }

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

        private void button1_Click(object sender, EventArgs e)
        {
            socket.Close();
            Lock.Visible = false;
            unLock.Visible = false;
            if(checkBox1.Checked == true) Program.ac.MainForm = new Login(false);
            else Program.ac.MainForm = new Login();
            
            closeForm(this);
            Program.ac.MainForm.ShowDialog();
        }


        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socket.Close();
            Application.ExitThread();
            Environment.Exit(0);
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void 시작ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        delegate void del(PictureBox Light, bool token);

        public static void LightOnOff(PictureBox Light,bool token)
        {
            Light.Visible = token;
        }

        void LightChange(PictureBox red, PictureBox green,bool token)
        {
            if (red.InvokeRequired == true&&green.InvokeRequired==true)
            {
                if(token)
                {
                    red.Invoke(new del(LightOnOff), new object[] { red, false });
                    green.Invoke(new del(LightOnOff), new object[] { green, true });
                }
                else
                {
                    red.Invoke(new del(LightOnOff), new object[] { red, true });
                    green.Invoke(new del(LightOnOff), new object[] { green, false });
                }
            }
            else
            {
                if (token)
                {
                    red.Visible = false;
                    green.Visible = true;
                }
                else
                {
                    red.Visible = true;
                    green.Visible = false;
                }
            }
        }

        delegate void del2(Form1 form);

        public static void close(Form1 form)
        {
            form.Close();
        }

        void closeForm(Form1 form)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new del2(close), form);
            }
            else this.Close();

        }

        public class DialogState
        {
            public DialogResult result;
            public FileDialog dialog;
 

            public void ThreadProcShowDialog()
            {
                result = dialog.ShowDialog();
            }
        }

        private DialogResult STAShowDialog(FileDialog dialog)
        {
            DialogState state = new DialogState();
            state.dialog = dialog;
            System.Threading.Thread t = new System.Threading.Thread(state.ThreadProcShowDialog);
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            t.Join();
            return state.result;
        }

    }

    


}
