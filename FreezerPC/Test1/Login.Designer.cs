namespace FreezerPC
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.IDtextBox = new System.Windows.Forms.TextBox();
            this.PasstextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Login_button = new System.Windows.Forms.Button();
            this.checkBox_AutoLogin = new System.Windows.Forms.CheckBox();
            this.checkBox_saveData = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // IDtextBox
            // 
            this.IDtextBox.Location = new System.Drawing.Point(96, 19);
            this.IDtextBox.Name = "IDtextBox";
            this.IDtextBox.Size = new System.Drawing.Size(156, 21);
            this.IDtextBox.TabIndex = 0;
            // 
            // PasstextBox
            // 
            this.PasstextBox.Location = new System.Drawing.Point(96, 46);
            this.PasstextBox.Name = "PasstextBox";
            this.PasstextBox.PasswordChar = '*';
            this.PasstextBox.Size = new System.Drawing.Size(156, 21);
            this.PasstextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // Login_button
            // 
            this.Login_button.Location = new System.Drawing.Point(177, 102);
            this.Login_button.Name = "Login_button";
            this.Login_button.Size = new System.Drawing.Size(75, 23);
            this.Login_button.TabIndex = 4;
            this.Login_button.Text = "로그인";
            this.Login_button.UseVisualStyleBackColor = true;
            this.Login_button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // checkBox_AutoLogin
            // 
            this.checkBox_AutoLogin.AutoSize = true;
            this.checkBox_AutoLogin.Location = new System.Drawing.Point(168, 80);
            this.checkBox_AutoLogin.Name = "checkBox_AutoLogin";
            this.checkBox_AutoLogin.Size = new System.Drawing.Size(84, 16);
            this.checkBox_AutoLogin.TabIndex = 5;
            this.checkBox_AutoLogin.Text = "자동로그인";
            this.checkBox_AutoLogin.UseVisualStyleBackColor = true;
            // 
            // checkBox_saveData
            // 
            this.checkBox_saveData.AutoSize = true;
            this.checkBox_saveData.Location = new System.Drawing.Point(82, 80);
            this.checkBox_saveData.Name = "checkBox_saveData";
            this.checkBox_saveData.Size = new System.Drawing.Size(76, 16);
            this.checkBox_saveData.TabIndex = 6;
            this.checkBox_saveData.Text = "정보 기억";
            this.checkBox_saveData.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 145);
            this.Controls.Add(this.checkBox_saveData);
            this.Controls.Add(this.checkBox_AutoLogin);
            this.Controls.Add(this.Login_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PasstextBox);
            this.Controls.Add(this.IDtextBox);
            this.Name = "Login";
            this.Text = "FreezerPC";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IDtextBox;
        private System.Windows.Forms.TextBox PasstextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Login_button;
        private System.Windows.Forms.CheckBox checkBox_AutoLogin;
        private System.Windows.Forms.CheckBox checkBox_saveData;
    }
}