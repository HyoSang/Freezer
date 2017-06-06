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
using System.ServiceModel;






namespace FreezerPC
{
    public partial class MainForm : Form
    {
        List<IWebDriver> driver= new List<IWebDriver>();
        List<int> webProcessID = new List<int>();
        Thread URLT;
        Thread ProcessT;
        Socket socket;
        System.IO.FileStream URLF;
        System.IO.FileStream ProcessF;
        System.IO.FileStream LoginF;
        bool flag = true;
        bool flag2 = true;
        public MainForm(string ID,string Pass)
        {
            InitializeComponent();
            LoginID.Text = ID;
            socket = IO.Socket("http://localhost:8005");
            socket.On("start", (data) =>
            {
                URLT = new Thread(new ThreadStart(URLThread));
                URLT.Start();
                ProcessT = new Thread(new ThreadStart(ProcessThread));
                ProcessT.Start();
                Lock.Icon = FreezerPC.Properties.Resources.object_locked;
                LightChange(URL_red, URL_green, true);
                LightChange(Process_red, Process_green, true);
                try
                {
                    if (URLF==null||!URLF.CanRead) URLF = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\URL.dat", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                    if (ProcessF == null || !ProcessF.CanRead) ProcessF = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\Process.dat", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                    if (LoginF== null || !LoginF.CanRead) LoginF = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\login.dat", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                }catch (System.IO.FileNotFoundException) { flag2 = false; }
                if (flag2)
                {
                    try
                    {

                        System.IO.StreamReader reader = new System.IO.StreamReader(LoginF, System.Text.Encoding.Default);
                        string Text = reader.ReadLine();
                        if (!Text.Equals("T"))
                        {
                            flag = false;
                            reader.Close();
                        }
                        reader.Close();
                        LoginF.Close();
                        
                    }
                    catch (System.IO.FileNotFoundException) { }

                }
                if(LoginF == null || !LoginF.CanWrite)LoginF = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\login.dat", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(LoginF, System.Text.Encoding.Default);
                writer.WriteLine("I");
                writer.WriteLine(ID);
                writer.WriteLine(Pass);
                writer.Close();

            });
            socket.On("end", (data) =>
            {
                if(URLT!=null&&URLT.IsAlive)URLT.Abort();
                if(ProcessT!=null&&ProcessT.IsAlive)ProcessT.Abort();
                if (URLF != null) { URLF.Close();  }
                if (ProcessF != null) { ProcessF.Close(); }
                if (LoginF != null) { LoginF.Close(); }
                Lock.Icon = FreezerPC.Properties.Resources.object_unlocked;
                LightChange(URL_red, URL_green, false);
                LightChange(Process_red, Process_green, false);
                if (flag2)
                {
                    try
                    {
                        if (LoginF == null || !LoginF.CanWrite) LoginF = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\login.dat", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                        System.IO.StreamWriter writer = new System.IO.StreamWriter(LoginF, System.Text.Encoding.Default);
                        if (flag) writer.WriteLine("T");
                        else writer.WriteLine("F");
                        writer.WriteLine(ID);
                        writer.WriteLine(Pass);
                        writer.Close();
                        LoginF.Close();
                    }
                    catch (System.IO.FileNotFoundException) { }
                }
                else
                {
                    System.IO.File.Delete(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\login.dat");
                }
                

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
                System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\URL.dat", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                System.IO.StreamReader reader = new System.IO.StreamReader(fs, System.Text.Encoding.Default);
                while (!reader.EndOfStream)
                {
                    URLList.Items.Add(reader.ReadLine());
                }
                reader.Close();
                fs.Close();
            }
            catch (System.IO.FileNotFoundException) { }


            ProcessList.BeginUpdate();
            ProcessList.View = View.Details;
            ProcessList.Columns.Add("프로세스이름", 120, HorizontalAlignment.Center);
            ProcessList.Columns.Add("설명", 232, HorizontalAlignment.Center);

            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\Process.dat", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                System.IO.StreamReader reader = new System.IO.StreamReader(fs, System.Text.Encoding.Default);
                while (!reader.EndOfStream)
                {
                    ListViewItem lvi = new ListViewItem(reader.ReadLine());
                    lvi.SubItems.Add(reader.ReadLine());
                    ProcessList.Items.Add(lvi);
                }
                reader.Close();
                fs.Close();
            }
            catch (System.IO.FileNotFoundException) { }

            ProcessList.EndUpdate();

            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\login.dat", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                System.IO.StreamReader reader = new System.IO.StreamReader(fs, System.Text.Encoding.Default);
                if(reader.ReadLine()=="I")
                {
                    socket.Emit("Force", ID);
                }
                reader.Close();
                fs.Close();
            }
            catch (System.IO.FileNotFoundException) { }

        }


        private void Chrome_Click(object sender, EventArgs e)
        {
            if (BrowerList.SelectedIndex == 0)
            {
                Program.ManualResstEvent.Reset();
                var service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
                int count = driver.Count;
                service.HideCommandPromptWindow = true;
                driver.Add(new ChromeDriver(service));
                webProcessID.Add(service.ProcessId);
                Program.ManualResstEvent.Set();
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
            System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\URL.dat", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
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
                string[] temp = path.Split('\\');
                string[] temp2 = temp[temp.Length - 1].Split('.');
                AddProcess_TextBox_name.Text = temp2[0];
            }
        }


        void URLThread()
        {
            while (true)
            {
                Program.ManualResstEvent.WaitOne();
                if (driver.Count == 0) continue;
                for (int j = 0; j < driver.Count; j++)
                {
                    try
                    {
                        if (driver[j].WindowHandles.Count >= 2)
                        {
                            Program.ManualResstEvent.Reset();
                            if (Process.GetProcessById(webProcessID[j]).ProcessName.Equals("chromedriver"))
                            {
                                driver[j].SwitchTo().Window(driver[j].WindowHandles[1]);
                                var url = driver[j].Url;
                                var service = ChromeDriverService.CreateDefaultService(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine));
                                int count = driver.Count;
                                service.HideCommandPromptWindow = true;
                                driver.Add(new ChromeDriver(service));
                                webProcessID.Add(service.ProcessId);
                                Program.ManualResstEvent.Set();
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
                Program.ManualResstEvent.WaitOne();
                Process[] pro = Process.GetProcesses();
                Process c_pro = Process.GetCurrentProcess();
                if (pro.Length != 0)
                {
                    int count2 = 0;
                    for (int i = 0; i < pro.Length; i++)
                    {
                        if (pro[i].MainWindowTitle.Equals("")) continue;
                        if (pro[i].MainWindowTitle.Equals("FreezerPC")) continue;
                        if (pro[i].ProcessName.Equals("Taskmgr"))
                        {
                            pro[i].Kill();
                            continue;
                        }
                        if (pro[i].ProcessName.Equals("cmd")) continue;
                        if (pro[i].ProcessName.Equals("chrome"))
                        {
                            if (webProcessID.Count > count2) count2++;
                            else
                            {
                                pro[i].Kill();
                                continue;
                            }
                        }
                        int count = 0;
                        for(int j=0;j<ProcessList.Items.Count;j++)
                        {
                            if (pro[i].ProcessName.Equals("chrome")) break;
                                if (!pro[i].ProcessName.Equals(ProcessList.Items[j].SubItems[0].Text)) count++;
                        }
                        if (pro[i].ProcessName.Equals("chrome")) continue;
                        try
                        {
                            if (count == ProcessList.Items.Count) pro[i].Kill();
                        }
                        catch (Exception) { };
                        
                    }

                    
                }
                
                Thread.Sleep(100);
                
            }
        }

        private void LogOut_Click(object sender, EventArgs e)
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
            try
            {
                if (URLT.IsAlive || ProcessT.IsAlive) MessageBox.Show("프로그램이 동작중이면 종료할 수 없습니다.");
                else
                {
                    socket.Close();
                    Application.ExitThread();
                    Environment.Exit(0);
                    Application.Exit();
                }
            }catch(Exception)
            {
                socket.Close();
                Application.ExitThread();
                Environment.Exit(0);
                Application.Exit();
            };
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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

        delegate void del2(MainForm form);

        public static void close(MainForm form)
        {
            form.Close();
        }

        void closeForm(MainForm form)
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

        private void AddProcess_Click(object sender, EventArgs e)
        {
           string name = AddProcess_TextBox_name.Text;
           string explain = AddProcess_TextBox_exp.Text;
            if(name.Equals("chrome"))
            {
                MessageBox.Show("크롬은 등록 불가능 합니다.");
                return;
            }
            ProcessList.BeginUpdate();
            ListViewItem lvi = new ListViewItem(name);
           lvi.SubItems.Add(explain);
           ProcessList.Items.Add(lvi);
            ProcessList.EndUpdate();
            AddProcess_TextBox_name.Clear();
            AddProcess_TextBox_exp.Clear();
            System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\Process.dat", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
            for (int i = 0; i < ProcessList.Items.Count; i++)
            {
                writer.WriteLine(ProcessList.Items[i].SubItems[0].Text);
                writer.WriteLine(ProcessList.Items[i].SubItems[1].Text);
            }

            writer.Close();
        }

        private void 수정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (URLList.SelectedItem == null) return;
            else
            {
                AddURL_TextBox.Text = URLList.SelectedItem.ToString();
                URLList.Items.Remove(URLList.SelectedItem);
            }
        }

        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (URLList.SelectedItem == null) return;
            else
            {
                URLList.Items.Remove(URLList.SelectedItem);
                System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\URL.dat", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
                for (int i = 0; i < URLList.Items.Count; i++)
                {
                    writer.WriteLine(URLList.Items[i].ToString());
                }

                writer.Close();
            }
        }

        private void 수정ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ProcessList.SelectedItems.Count == 0) return;
            else
            {
                AddProcess_TextBox_name.Text = ProcessList.SelectedItems[0].SubItems[0].Text;
                AddProcess_TextBox_exp.Text = ProcessList.SelectedItems[0].SubItems[1].Text;
                ProcessList.Items.Remove(ProcessList.SelectedItems[0]);
            }
        }

        private void 삭제ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ProcessList.SelectedItems.Count == 0) return;
            else
            {
               ProcessList.Items.Remove(ProcessList.SelectedItems[0]);
                System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\Process.dat", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
                for (int i = 0; i < URLList.Items.Count; i++)
                {
                    writer.WriteLine(ProcessList.Items[i].SubItems[0].Text);
                    writer.WriteLine(ProcessList.Items[i].SubItems[1].Text);
                }

                writer.Close();
            }
        }
}
   


}
