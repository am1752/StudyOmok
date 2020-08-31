namespace OMOK
{
    partial class FindForm
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
            this.txtA = new System.Windows.Forms.TextBox();
            this.PWA = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CboQ = new System.Windows.Forms.ComboBox();
            this.TxtID = new System.Windows.Forms.TextBox();
            this.Txt = new System.Windows.Forms.Label();
            this.CheckButton = new System.Windows.Forms.Button();
            this.Find = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(131, 116);
            this.txtA.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(189, 21);
            this.txtA.TabIndex = 2;
            this.txtA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtA_KeyPress);
            // 
            // PWA
            // 
            this.PWA.AutoSize = true;
            this.PWA.Location = new System.Drawing.Point(21, 123);
            this.PWA.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PWA.Name = "PWA";
            this.PWA.Size = new System.Drawing.Size(45, 12);
            this.PWA.TabIndex = 11;
            this.PWA.Text = "비번 답";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "비번 질문";
            // 
            // CboQ
            // 
            this.CboQ.Items.AddRange(new object[] {
            "",
            "고향은?",
            "좋아하는 나라?",
            "좋아하는 배우?",
            "좋아하는 과목?"});
            this.CboQ.Location = new System.Drawing.Point(131, 83);
            this.CboQ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CboQ.Name = "CboQ";
            this.CboQ.Size = new System.Drawing.Size(189, 20);
            this.CboQ.TabIndex = 1;
            // 
            // TxtID
            // 
            this.TxtID.Location = new System.Drawing.Point(131, 50);
            this.TxtID.Name = "TxtID";
            this.TxtID.Size = new System.Drawing.Size(189, 21);
            this.TxtID.TabIndex = 0;
            this.TxtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtID_KeyPress);
            // 
            // Txt
            // 
            this.Txt.AutoSize = true;
            this.Txt.Location = new System.Drawing.Point(53, 53);
            this.Txt.Name = "Txt";
            this.Txt.Size = new System.Drawing.Size(16, 12);
            this.Txt.TabIndex = 13;
            this.Txt.Text = "ID";
            // 
            // CheckButton
            // 
            this.CheckButton.Location = new System.Drawing.Point(333, 50);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(80, 23);
            this.CheckButton.TabIndex = 14;
            this.CheckButton.Text = "확인";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // Find
            // 
            this.Find.Location = new System.Drawing.Point(157, 159);
            this.Find.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(79, 27);
            this.Find.TabIndex = 15;
            this.Find.Text = "찾기";
            this.Find.UseVisualStyleBackColor = true;
            this.Find.Click += new System.EventHandler(this.Find_Click);
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 197);
            this.Controls.Add(this.Find);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.TxtID);
            this.Controls.Add(this.Txt);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.PWA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CboQ);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FindForm";
            this.Text = "FindForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FindForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.Label PWA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CboQ;
        private System.Windows.Forms.TextBox TxtID;
        private System.Windows.Forms.Label Txt;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.Button Find;
    }
}