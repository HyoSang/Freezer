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
            this.StartURL = new System.Windows.Forms.Button();
            this.AddURL_TextBox = new System.Windows.Forms.TextBox();
            this.Chrome = new System.Windows.Forms.Button();
            this.StopURL = new System.Windows.Forms.Button();
            this.URLList = new System.Windows.Forms.ListBox();
            this.URL차단 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddURL_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AddProcess_Button = new System.Windows.Forms.Button();
            this.ProcessList = new System.Windows.Forms.ListBox();
            this.AddProcess_TextBox = new System.Windows.Forms.TextBox();
            this.StartProcess = new System.Windows.Forms.Button();
            this.StopProcess = new System.Windows.Forms.Button();
            this.URL차단.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartURL
            // 
            this.StartURL.Location = new System.Drawing.Point(6, 202);
            this.StartURL.Name = "StartURL";
            this.StartURL.Size = new System.Drawing.Size(95, 38);
            this.StartURL.TabIndex = 1;
            this.StartURL.Text = "금지";
            this.StartURL.UseVisualStyleBackColor = true;
            this.StartURL.Click += new System.EventHandler(this.StartURL_Click);
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
            this.Chrome.Location = new System.Drawing.Point(271, 202);
            this.Chrome.Name = "Chrome";
            this.Chrome.Size = new System.Drawing.Size(95, 38);
            this.Chrome.TabIndex = 4;
            this.Chrome.Text = "크롬실행";
            this.Chrome.UseVisualStyleBackColor = true;
            this.Chrome.Click += new System.EventHandler(this.Chrome_Click);
            // 
            // StopURL
            // 
            this.StopURL.Location = new System.Drawing.Point(144, 202);
            this.StopURL.Name = "StopURL";
            this.StopURL.Size = new System.Drawing.Size(95, 38);
            this.StopURL.TabIndex = 5;
            this.StopURL.Text = "금지종료";
            this.StopURL.UseVisualStyleBackColor = true;
            this.StopURL.Click += new System.EventHandler(this.StopURL_Click);
            // 
            // URLList
            // 
            this.URLList.FormattingEnabled = true;
            this.URLList.ItemHeight = 12;
            this.URLList.Location = new System.Drawing.Point(6, 30);
            this.URLList.Name = "URLList";
            this.URLList.Size = new System.Drawing.Size(352, 124);
            this.URLList.TabIndex = 6;
            // 
            // URL차단
            // 
            this.URL차단.Controls.Add(this.label1);
            this.URL차단.Controls.Add(this.AddURL_Button);
            this.URL차단.Controls.Add(this.URLList);
            this.URL차단.Controls.Add(this.AddURL_TextBox);
            this.URL차단.Controls.Add(this.StartURL);
            this.URL차단.Controls.Add(this.StopURL);
            this.URL차단.Controls.Add(this.Chrome);
            this.URL차단.Location = new System.Drawing.Point(22, 16);
            this.URL차단.Name = "URL차단";
            this.URL차단.Size = new System.Drawing.Size(372, 246);
            this.URL차단.TabIndex = 11;
            this.URL차단.TabStop = false;
            this.URL차단.Text = "브라우저 제어";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "허용 URL";
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
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.AddProcess_Button);
            this.groupBox1.Controls.Add(this.ProcessList);
            this.groupBox1.Controls.Add(this.AddProcess_TextBox);
            this.groupBox1.Controls.Add(this.StartProcess);
            this.groupBox1.Controls.Add(this.StopProcess);
            this.groupBox1.Location = new System.Drawing.Point(22, 268);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 246);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "프로세스 제어";
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
            this.AddProcess_Button.Location = new System.Drawing.Point(263, 160);
            this.AddProcess_Button.Name = "AddProcess_Button";
            this.AddProcess_Button.Size = new System.Drawing.Size(95, 23);
            this.AddProcess_Button.TabIndex = 7;
            this.AddProcess_Button.Text = "프로세스 추가";
            this.AddProcess_Button.UseVisualStyleBackColor = true;
            this.AddProcess_Button.Click += new System.EventHandler(this.AddProcess_Button_Click);
            // 
            // ProcessList
            // 
            this.ProcessList.FormattingEnabled = true;
            this.ProcessList.ItemHeight = 12;
            this.ProcessList.Location = new System.Drawing.Point(6, 30);
            this.ProcessList.Name = "ProcessList";
            this.ProcessList.Size = new System.Drawing.Size(352, 124);
            this.ProcessList.TabIndex = 6;
            // 
            // AddProcess_TextBox
            // 
            this.AddProcess_TextBox.Location = new System.Drawing.Point(6, 160);
            this.AddProcess_TextBox.Name = "AddProcess_TextBox";
            this.AddProcess_TextBox.Size = new System.Drawing.Size(233, 21);
            this.AddProcess_TextBox.TabIndex = 3;
            // 
            // StartProcess
            // 
            this.StartProcess.Location = new System.Drawing.Point(6, 202);
            this.StartProcess.Name = "StartProcess";
            this.StartProcess.Size = new System.Drawing.Size(95, 38);
            this.StartProcess.TabIndex = 1;
            this.StartProcess.Text = "금지";
            this.StartProcess.UseVisualStyleBackColor = true;
            this.StartProcess.Click += new System.EventHandler(this.StartProcess_Click);
            // 
            // StopProcess
            // 
            this.StopProcess.Location = new System.Drawing.Point(144, 202);
            this.StopProcess.Name = "StopProcess";
            this.StopProcess.Size = new System.Drawing.Size(95, 38);
            this.StopProcess.TabIndex = 5;
            this.StopProcess.Text = "금지종료";
            this.StopProcess.UseVisualStyleBackColor = true;
            this.StopProcess.Click += new System.EventHandler(this.StopProcess_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 537);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.URL차단);
            this.Name = "Form1";
            this.Text = "FreezerPC";
            this.URL차단.ResumeLayout(false);
            this.URL차단.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button StartURL;
        private System.Windows.Forms.TextBox AddURL_TextBox;
        private System.Windows.Forms.Button Chrome;
        private System.Windows.Forms.Button StopURL;
        private System.Windows.Forms.ListBox URLList;
        private System.Windows.Forms.GroupBox URL차단;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddURL_Button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddProcess_Button;
        private System.Windows.Forms.ListBox ProcessList;
        private System.Windows.Forms.TextBox AddProcess_TextBox;
        private System.Windows.Forms.Button StartProcess;
        private System.Windows.Forms.Button StopProcess;
    }
}

