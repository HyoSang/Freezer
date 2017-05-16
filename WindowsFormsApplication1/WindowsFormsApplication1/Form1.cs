using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace WindowsFormsApplication1
{
   
    public partial class Form1 : Form
    {
        ThreadStart ts;
        Thread t;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] pro = Process.GetProcesses();
            for (int i = 0; i < pro.Length; i++)
            {
                
                listBox1.Items.Add(pro[i].ProcessName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process[] pro = Process.GetProcessesByName(listBox1.SelectedItem.ToString());
            pro[0].Kill();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ts = new ThreadStart(threadbody);
            t = new Thread(ts);
            t.Start();
        }

        void threadbody()
        {
            while (true)
            {
                Process[] pro = Process.GetProcesses();
                if (pro.Length != 0)
                {
                    for (int i = 0; i < pro.Length; i++)
                    {
                        if (pro[i].MainWindowTitle.Equals("")) continue;
                        if (textBox1.Text != pro[i].ProcessName)pro[i].Kill();
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            t.Abort();
        }
    }
}
