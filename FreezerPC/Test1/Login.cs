using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using System.Threading;

namespace FreezerPC
{
   
    public partial class Login : Form
    {
        Socket socket;
        bool flag = false;
        public Login()
        {
            InitializeComponent();
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(Environment.CurrentDirectory + "\\FreezerPC.lnk", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                fs.Close();
            }
            catch (Exception)
            {
                Environment.SetEnvironmentVariable("FreezerPC", Environment.CurrentDirectory, EnvironmentVariableTarget.User);
            }
            
                try
                {
                System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) +"\\login.dat", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                    System.IO.StreamReader reader = new System.IO.StreamReader(fs, System.Text.Encoding.Default);
                    checkBox_saveData.Checked = true;
                    string Text = reader.ReadLine();
                    if (Text.Equals("T")|| Text.Equals("I"))
                    {
                        if (Text.Equals("I")) flag = true;
                        checkBox_AutoLogin.Checked = true;
                        IDtextBox.Text = reader.ReadLine();
                        PasstextBox.Text = reader.ReadLine();
                        reader.Close();
                        Login_Button_Click(Login_button, new EventArgs());
                    }
                    else
                    {
                        checkBox_AutoLogin.Checked = false;
                        IDtextBox.Text = reader.ReadLine();
                        PasstextBox.Text = reader.ReadLine();
                        reader.Close();
                    }
                    

                }
                catch (System.IO.FileNotFoundException) { }
                
            
        }
        public Login(bool token)
        {
            InitializeComponent();
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\login.dat", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                System.IO.StreamReader reader = new System.IO.StreamReader(fs, System.Text.Encoding.Default);
                checkBox_saveData.Checked = true;
                string Text = reader.ReadLine();
                if (Text.Equals("T"))
                {
                    checkBox_AutoLogin.Checked = true;
                    IDtextBox.Text = reader.ReadLine();
                    PasstextBox.Text = reader.ReadLine();
                    reader.Close();
                }
                else
                {
                    checkBox_AutoLogin.Checked = false;
                    IDtextBox.Text = reader.ReadLine();
                    PasstextBox.Text = reader.ReadLine();
                    reader.Close();
                }


            }
            catch (System.IO.FileNotFoundException) { }
        }

        class User
        {
          public string id { get; set;  }
          public string pass { get; set; }
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            User JSON = new User();
            JSON.id = IDtextBox.Text;
            JSON.pass = PasstextBox.Text;
            socket = IO.Socket("http://localhost:8005");
            socket.Emit("LoginReq",JsonConvert.SerializeObject(JSON));
            socket.On("LoginRes", (data) =>
            {
                string message = (string)data;
                if (message == "success")
                {
                    
                    socket.Close();
                    Program.ac.MainForm = new MainForm(IDtextBox.Text,PasstextBox.Text);
                    closeForm(this);
                    Application.Run(Program.ac);
                    
                }
                else
                {
                    socket.Close();
                    MessageBox.Show("ID나 패스워드가 틀렸습니다.");
                    IDtextBox.Clear();
                    PasstextBox.Clear();
                }
                
                
            });
            if (checkBox_saveData.Checked == true)
            {
                System.IO.FileStream fs = new System.IO.FileStream(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\login.dat", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
                if(checkBox_AutoLogin.Checked == true)
                {
                    if(flag) writer.WriteLine("I");
                    else writer.WriteLine("T");
                    writer.WriteLine(IDtextBox.Text);
                    writer.WriteLine(PasstextBox.Text);
                    writer.Close();
                }
                else
                {
                    writer.WriteLine("F");
                    writer.WriteLine(IDtextBox.Text);
                    writer.WriteLine(PasstextBox.Text);
                    writer.Close();
                }
                writer.Close();
            }
            else
            {
                if(System.IO.File.Exists(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\login.dat"))
                {
                    System.IO.File.Delete(Environment.GetEnvironmentVariable("FreezerPC", EnvironmentVariableTarget.Machine) + "\\login.dat");
                }
            }
        }
        delegate void Test(Login lg);

        public static void delegateMethod(Login lg)
        {
            lg.Close();
        }
        
        void closeForm(Login lg)
        {
            if(this.InvokeRequired==true)
            {
                Test handler = delegateMethod;
                this.Invoke(handler,new object[]{ lg });
            }
            else
            {
                this.Close();
            }
        }
    }
}
