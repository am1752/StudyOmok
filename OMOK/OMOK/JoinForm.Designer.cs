namespace OMOK
{
    partial class JoinForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.Txt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtID = new System.Windows.Forms.TextBox();
            this.CheckButton = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPWCHECK = new System.Windows.Forms.TextBox();
            this.JOIN = new System.Windows.Forms.Button();
            this.CboQ = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(223, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "회원가입";
            // 
            // Txt
            // 
            this.Txt.AutoSize = true;
            this.Txt.Location = new System.Drawing.Point(134, 94);
            this.Txt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Txt.Name = "Txt";
            this.Txt.Size = new System.Drawing.Size(22, 18);
            this.Txt.TabIndex = 1;
            this.Txt.Text = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 177);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "PW";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 252);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "PW CHECK";
            // 
            // TxtID
            // 
            this.TxtID.Location = new System.Drawing.Point(196, 90);
            this.TxtID.Margin = new System.Windows.Forms.Padding(4);
            this.TxtID.Name = "TxtID";
            this.TxtID.Size = new System.Drawing.Size(223, 28);
            this.TxtID.TabIndex = 0;
            this.TxtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // CheckButton
            // 
            this.CheckButton.Location = new System.Drawing.Point(461, 86);
            this.CheckButton.Margin = new System.Windows.Forms.Padding(4);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(107, 34);
            this.CheckButton.TabIndex = 1;
            this.CheckButton.Text = "중복확인";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(196, 172);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '♥';
            this.txtPassword.Size = new System.Drawing.Size(223, 28);
            this.txtPassword.TabIndex = 1;
            // 
            // txtPWCHECK
            // 
            this.txtPWCHECK.Location = new System.Drawing.Point(196, 248);
            this.txtPWCHECK.Margin = new System.Windows.Forms.Padding(4);
            this.txtPWCHECK.Name = "txtPWCHECK";
            this.txtPWCHECK.PasswordChar = '♥';
            this.txtPWCHECK.Size = new System.Drawing.Size(223, 28);
            this.txtPWCHECK.TabIndex = 2;
            // 
            // JOIN
            // 
            this.JOIN.Location = new System.Drawing.Point(269, 517);
            this.JOIN.Margin = new System.Windows.Forms.Padding(4);
            this.JOIN.Name = "JOIN";
            this.JOIN.Size = new System.Drawing.Size(107, 34);
            this.JOIN.TabIndex = 5;
            this.JOIN.Text = "확인";
            this.JOIN.UseVisualStyleBackColor = true;
            this.JOIN.Click += new System.EventHandler(this.JOIN_Click);
            // 
            // CboQ
            // 
            this.CboQ.Items.AddRange(new object[] {
            "",
            "고향은?",
            "좋아하는 나라?",
            "좋아하는 배우?",
            "좋아하는 과목?"});
            this.CboQ.Location = new System.Drawing.Point(185, 320);
            this.CboQ.Name = "CboQ";
            this.CboQ.Size = new System.Drawing.Size(269, 26);
            this.CboQ.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "비번 질문";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 379);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "비번 답";
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(185, 369);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(269, 28);
            this.txtA.TabIndex = 4;
            this.txtA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtA_KeyPress);
            // 
            // JoinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 564);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CboQ);
            this.Controls.Add(this.JOIN);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.txtPWCHECK);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.TxtID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Txt);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "JoinForm";
            this.Text = "JoinForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtID;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtPWCHECK;
        private System.Windows.Forms.Button JOIN;
        private System.Windows.Forms.ComboBox CboQ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtA;
    }
}