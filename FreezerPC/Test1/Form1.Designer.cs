namespace Test1
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.AddURL_TextBox = new System.Windows.Forms.TextBox();
            this.Chrome = new System.Windows.Forms.Button();
            this.URLList = new System.Windows.Forms.ListBox();
            this.URL차단 = new System.Windows.Forms.GroupBox();
            this.BrowerList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddURL_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProcessList = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.AddProcess_TextBox_exp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AddProcess_Button = new System.Windows.Forms.Button();
            this.AddProcess_TextBox_name = new System.Windows.Forms.TextBox();
            this.Logout = new System.Windows.Forms.Button();
            this.unLock = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.시작ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Lock = new System.Windows.Forms.NotifyIcon(this.components);
            this.server_red = new System.Windows.Forms.PictureBox();
            this.server_green = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.URL_green = new System.Windows.Forms.PictureBox();
            this.URL_red = new System.Windows.Forms.PictureBox();
            this.Process_red = new System.Windows.Forms.PictureBox();
            this.Process_green = new System.Windows.Forms.PictureBox();
            this.LoginID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.수정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.수정ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.삭제ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.URL차단.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.server_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.URL_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.URL_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Process_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Process_green)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddURL_TextBox
            // 
            this.AddURL_TextBox.Location = new System.Drawing.Point(6, 160);
            this.AddURL_TextBox.Name = "AddURL_TextBox";
            this.AddURL_TextBox.Size = new System.Drawing.Size(233, 21);
            this.AddURL_TextBox.TabIndex = 3;
            // 
            // Chrome
            // 
            this.Chrome.Location = new System.Drawing.Point(263, 196);
            this.Chrome.Name = "Chrome";
            this.Chrome.Size = new System.Drawing.Size(95, 38);
            this.Chrome.TabIndex = 4;
            this.Chrome.Text = "브라우저 실행";
            this.Chrome.UseVisualStyleBackColor = true;
            this.Chrome.Click += new System.EventHandler(this.Chrome_Click);
            // 
            // URLList
            // 
            this.URLList.ContextMenuStrip = this.contextMenuStrip2;
            this.URLList.FormattingEnabled = true;
            this.URLList.ItemHeight = 12;
            this.URLList.Location = new System.Drawing.Point(6, 30);
            this.URLList.Name = "URLList";
            this.URLList.Size = new System.Drawing.Size(352, 124);
            this.URLList.TabIndex = 6;
            // 
            // URL차단
            // 
            this.URL차단.Controls.Add(this.BrowerList);
            this.URL차단.Controls.Add(this.label1);
            this.URL차단.Controls.Add(this.AddURL_Button);
            this.URL차단.Controls.Add(this.URLList);
            this.URL차단.Controls.Add(this.AddURL_TextBox);
            this.URL차단.Controls.Add(this.Chrome);
            this.URL차단.Location = new System.Drawing.Point(22, 16);
            this.URL차단.Name = "URL차단";
            this.URL차단.Size = new System.Drawing.Size(372, 246);
            this.URL차단.TabIndex = 11;
            this.URL차단.TabStop = false;
            this.URL차단.Text = "브라우저 제어";
            // 
            // BrowerList
            // 
            this.BrowerList.FormattingEnabled = true;
            this.BrowerList.Location = new System.Drawing.Point(90, 206);
            this.BrowerList.Name = "BrowerList";
            this.BrowerList.Size = new System.Drawing.Size(149, 20);
            this.BrowerList.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "차단 URL";
            // 
            // AddURL_Button
            // 
            this.AddURL_Button.Location = new System.Drawing.Point(283, 160);
            this.AddURL_Button.Name = "AddURL_Button";
            this.AddURL_Button.Size = new System.Drawing.Size(75, 23);
            this.AddURL_Button.TabIndex = 7;
            this.AddURL_Button.Text = "URL추가";
            this.AddURL_Button.UseVisualStyleBackColor = true;
            this.AddURL_Button.Click += new System.EventHandler(this.AddURL_Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ProcessList);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.AddProcess_TextBox_exp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.AddProcess_Button);
            this.groupBox1.Controls.Add(this.AddProcess_TextBox_name);
            this.groupBox1.Location = new System.Drawing.Point(22, 268);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 209);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "프로세스 제어";
            // 
            // ProcessList
            // 
            this.ProcessList.ContextMenuStrip = this.contextMenuStrip3;
            this.ProcessList.LabelWrap = false;
            this.ProcessList.Location = new System.Drawing.Point(6, 30);
            this.ProcessList.MultiSelect = false;
            this.ProcessList.Name = "ProcessList";
            this.ProcessList.Scrollable = false;
            this.ProcessList.Size = new System.Drawing.Size(352, 121);
            this.ProcessList.TabIndex = 30;
            this.ProcessList.UseCompatibleStateImageBehavior = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(271, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "프로세스 등록";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "설명";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 27;
            this.label7.Text = "경로";
            // 
            // AddProcess_TextBox_exp
            // 
            this.AddProcess_TextBox_exp.Location = new System.Drawing.Point(58, 182);
            this.AddProcess_TextBox_exp.Name = "AddProcess_TextBox_exp";
            this.AddProcess_TextBox_exp.Size = new System.Drawing.Size(210, 21);
            this.AddProcess_TextBox_exp.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "허용 프로세스";
            // 
            // AddProcess_Button
            // 
            this.AddProcess_Button.Location = new System.Drawing.Point(272, 157);
            this.AddProcess_Button.Name = "AddProcess_Button";
            this.AddProcess_Button.Size = new System.Drawing.Size(63, 23);
            this.AddProcess_Button.TabIndex = 7;
            this.AddProcess_Button.Text = "찾아보기";
            this.AddProcess_Button.UseVisualStyleBackColor = true;
            this.AddProcess_Button.Click += new System.EventHandler(this.AddProcess_Button_Click);
            // 
            // AddProcess_TextBox_name
            // 
            this.AddProcess_TextBox_name.Location = new System.Drawing.Point(58, 158);
            this.AddProcess_TextBox_name.Name = "AddProcess_TextBox_name";
            this.AddProcess_TextBox_name.ReadOnly = true;
            this.AddProcess_TextBox_name.Size = new System.Drawing.Size(210, 21);
            this.AddProcess_TextBox_name.TabIndex = 3;
            // 
            // Logout
            // 
            this.Logout.Location = new System.Drawing.Point(319, 529);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(75, 23);
            this.Logout.TabIndex = 13;
            this.Logout.Text = "로그아웃";
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.button1_Click);
            // 
            // unLock
            // 
            this.unLock.ContextMenuStrip = this.contextMenuStrip1;
            this.unLock.Icon = ((System.Drawing.Icon)(resources.GetObject("unLock.Icon")));
            this.unLock.Text = "FreezerPC";
            this.unLock.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.시작ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 48);
            // 
            // 시작ToolStripMenuItem
            // 
            this.시작ToolStripMenuItem.Name = "시작ToolStripMenuItem";
            this.시작ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.시작ToolStripMenuItem.Text = "시작";
            this.시작ToolStripMenuItem.Click += new System.EventHandler(this.시작ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // Lock
            // 
            this.Lock.ContextMenuStrip = this.contextMenuStrip1;
            this.Lock.Icon = ((System.Drawing.Icon)(resources.GetObject("Lock.Icon")));
            this.Lock.Text = "FreezerPC";
            // 
            // server_red
            // 
            this.server_red.Image = global::Test1.Properties.Resources.user_busy;
            this.server_red.Location = new System.Drawing.Point(28, 495);
            this.server_red.Name = "server_red";
            this.server_red.Size = new System.Drawing.Size(54, 50);
            this.server_red.TabIndex = 14;
            this.server_red.TabStop = false;
            this.server_red.Visible = false;
            // 
            // server_green
            // 
            this.server_green.Image = global::Test1.Properties.Resources.user_available;
            this.server_green.Location = new System.Drawing.Point(28, 495);
            this.server_green.Name = "server_green";
            this.server_green.Size = new System.Drawing.Size(54, 50);
            this.server_green.TabIndex = 15;
            this.server_green.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 480);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "서버연결";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 480);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "URL제어";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 480);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "프로세스제어";
            // 
            // URL_green
            // 
            this.URL_green.Image = global::Test1.Properties.Resources.user_available;
            this.URL_green.Location = new System.Drawing.Point(114, 495);
            this.URL_green.Name = "URL_green";
            this.URL_green.Size = new System.Drawing.Size(54, 50);
            this.URL_green.TabIndex = 20;
            this.URL_green.TabStop = false;
            this.URL_green.Visible = false;
            // 
            // URL_red
            // 
            this.URL_red.Image = global::Test1.Properties.Resources.user_busy;
            this.URL_red.Location = new System.Drawing.Point(114, 495);
            this.URL_red.Name = "URL_red";
            this.URL_red.Size = new System.Drawing.Size(54, 50);
            this.URL_red.TabIndex = 19;
            this.URL_red.TabStop = false;
            // 
            // Process_red
            // 
            this.Process_red.Image = global::Test1.Properties.Resources.user_busy;
            this.Process_red.Location = new System.Drawing.Point(200, 495);
            this.Process_red.Name = "Process_red";
            this.Process_red.Size = new System.Drawing.Size(54, 50);
            this.Process_red.TabIndex = 21;
            this.Process_red.TabStop = false;
            // 
            // Process_green
            // 
            this.Process_green.Image = global::Test1.Properties.Resources.user_available;
            this.Process_green.Location = new System.Drawing.Point(200, 495);
            this.Process_green.Name = "Process_green";
            this.Process_green.Size = new System.Drawing.Size(54, 50);
            this.Process_green.TabIndex = 23;
            this.Process_green.TabStop = false;
            this.Process_green.Visible = false;
            // 
            // LoginID
            // 
            this.LoginID.Location = new System.Drawing.Point(293, 502);
            this.LoginID.Name = "LoginID";
            this.LoginID.ReadOnly = true;
            this.LoginID.Size = new System.Drawing.Size(100, 21);
            this.LoginID.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(291, 485);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "로그인ID";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(288, 556);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 16);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "자동로그인 취소";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.수정ToolStripMenuItem,
            this.삭제ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(99, 48);
            // 
            // 수정ToolStripMenuItem
            // 
            this.수정ToolStripMenuItem.Name = "수정ToolStripMenuItem";
            this.수정ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.수정ToolStripMenuItem.Text = "수정";
            this.수정ToolStripMenuItem.Click += new System.EventHandler(this.수정ToolStripMenuItem_Click);
            // 
            // 삭제ToolStripMenuItem
            // 
            this.삭제ToolStripMenuItem.Name = "삭제ToolStripMenuItem";
            this.삭제ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.삭제ToolStripMenuItem.Text = "삭제";
            this.삭제ToolStripMenuItem.Click += new System.EventHandler(this.삭제ToolStripMenuItem_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.수정ToolStripMenuItem1,
            this.삭제ToolStripMenuItem1});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(153, 70);
            // 
            // 수정ToolStripMenuItem1
            // 
            this.수정ToolStripMenuItem1.Name = "수정ToolStripMenuItem1";
            this.수정ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.수정ToolStripMenuItem1.Text = "수정";
            this.수정ToolStripMenuItem1.Click += new System.EventHandler(this.수정ToolStripMenuItem1_Click);
            // 
            // 삭제ToolStripMenuItem1
            // 
            this.삭제ToolStripMenuItem1.Name = "삭제ToolStripMenuItem1";
            this.삭제ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.삭제ToolStripMenuItem1.Text = "삭제";
            this.삭제ToolStripMenuItem1.Click += new System.EventHandler(this.삭제ToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 579);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LoginID);
            this.Controls.Add(this.Process_red);
            this.Controls.Add(this.URL_red);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.URL차단);
            this.Controls.Add(this.server_red);
            this.Controls.Add(this.server_green);
            this.Controls.Add(this.URL_green);
            this.Controls.Add(this.Process_green);
            this.Name = "Form1";
            this.Text = "FreezerPC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.URL차단.ResumeLayout(false);
            this.URL차단.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.server_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.URL_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.URL_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Process_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Process_green)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox AddURL_TextBox;
        private System.Windows.Forms.Button Chrome;
        private System.Windows.Forms.ListBox URLList;
        private System.Windows.Forms.GroupBox URL차단;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddURL_Button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddProcess_Button;
        private System.Windows.Forms.TextBox AddProcess_TextBox_name;
        private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.NotifyIcon unLock;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 시작ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon Lock;
        private System.Windows.Forms.ComboBox BrowerList;
        private System.Windows.Forms.PictureBox server_red;
        private System.Windows.Forms.PictureBox server_green;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox URL_green;
        private System.Windows.Forms.PictureBox URL_red;
        private System.Windows.Forms.PictureBox Process_red;
        private System.Windows.Forms.PictureBox Process_green;
        private System.Windows.Forms.TextBox LoginID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox AddProcess_TextBox_exp;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 수정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 삭제ToolStripMenuItem;
        private System.Windows.Forms.ListView ProcessList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem 수정ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 삭제ToolStripMenuItem1;
    }
}

