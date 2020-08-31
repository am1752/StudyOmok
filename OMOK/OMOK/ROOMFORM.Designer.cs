namespace OMOK
{
    partial class ROOMFORM
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
            this.GrdRoom = new System.Windows.Forms.DataGridView();
            this.Enter_Button = new System.Windows.Forms.Button();
            this.SeeButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.txtCreate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UpateName = new System.Windows.Forms.Button();
            this.IDst = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GrdRoom)).BeginInit();
            this.SuspendLayout();
            // 
            // GrdRoom
            // 
            this.GrdRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrdRoom.Location = new System.Drawing.Point(12, 6);
            this.GrdRoom.Name = "GrdRoom";
            this.GrdRoom.RowHeadersWidth = 62;
            this.GrdRoom.RowTemplate.Height = 23;
            this.GrdRoom.Size = new System.Drawing.Size(437, 602);
            this.GrdRoom.TabIndex = 0;
            this.GrdRoom.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdRoom_CellClick_1);
            // 
            // Enter_Button
            // 
            this.Enter_Button.Location = new System.Drawing.Point(533, 44);
            this.Enter_Button.Name = "Enter_Button";
            this.Enter_Button.Size = new System.Drawing.Size(75, 23);
            this.Enter_Button.TabIndex = 1;
            this.Enter_Button.Text = "입장";
            this.Enter_Button.UseVisualStyleBackColor = true;
            this.Enter_Button.Click += new System.EventHandler(this.Enter_Button_Click);
            // 
            // SeeButton
            // 
            this.SeeButton.Location = new System.Drawing.Point(533, 87);
            this.SeeButton.Name = "SeeButton";
            this.SeeButton.Size = new System.Drawing.Size(75, 23);
            this.SeeButton.TabIndex = 2;
            this.SeeButton.Text = "관전";
            this.SeeButton.UseVisualStyleBackColor = true;
            this.SeeButton.Click += new System.EventHandler(this.SeeButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(682, 173);
            this.CreateButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 3;
            this.CreateButton.Text = "방만들기";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // txtCreate
            // 
            this.txtCreate.Location = new System.Drawing.Point(503, 177);
            this.txtCreate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCreate.Name = "txtCreate";
            this.txtCreate.Size = new System.Drawing.Size(152, 21);
            this.txtCreate.TabIndex = 4;
            this.txtCreate.TextChanged += new System.EventHandler(this.txtCreate_TextChanged);
            this.txtCreate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(544, 260);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 5;
            // 
            // UpateName
            // 
            this.UpateName.Location = new System.Drawing.Point(533, 136);
            this.UpateName.Name = "UpateName";
            this.UpateName.Size = new System.Drawing.Size(75, 23);
            this.UpateName.TabIndex = 2;
            this.UpateName.Text = "업데이트";
            this.UpateName.UseVisualStyleBackColor = true;
            this.UpateName.Click += new System.EventHandler(this.UpateName_Click);
            // 
            // IDst
            // 
            this.IDst.AutoSize = true;
            this.IDst.Location = new System.Drawing.Point(546, 307);
            this.IDst.Name = "IDst";
            this.IDst.Size = new System.Drawing.Size(0, 12);
            this.IDst.TabIndex = 6;
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(568, 585);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 7;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ROOMFORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 620);
            this.ControlBox = false;
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.IDst);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCreate);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.UpateName);
            this.Controls.Add(this.SeeButton);
            this.Controls.Add(this.Enter_Button);
            this.Controls.Add(this.GrdRoom);
            this.Name = "ROOMFORM";
            this.Text = "ROOMFORM";
            this.Load += new System.EventHandler(this.ROOMFORM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GrdRoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GrdRoom;
        private System.Windows.Forms.Button Enter_Button;
        private System.Windows.Forms.Button SeeButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.TextBox txtCreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UpateName;
        private System.Windows.Forms.Label IDst;
        private System.Windows.Forms.Button ExitButton;
    }
}