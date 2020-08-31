namespace OMOK
{
    partial class MultiPlayForm
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
            this.BoardPicture = new System.Windows.Forms.PictureBox();
            this.playbutton = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.Label();
            this.status1 = new System.Windows.Forms.Label();
            this.Exitbutton = new System.Windows.Forms.Button();
            this.IDst = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // BoardPicture
            // 
            this.BoardPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(216)))));
            this.BoardPicture.Location = new System.Drawing.Point(12, 12);
            this.BoardPicture.Name = "BoardPicture";
            this.BoardPicture.Size = new System.Drawing.Size(500, 500);
            this.BoardPicture.TabIndex = 1;
            this.BoardPicture.TabStop = false;
            this.BoardPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardPicture_Paint);
            this.BoardPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoardPicture_MouseDown);
            // 
            // playbutton
            // 
            this.playbutton.Location = new System.Drawing.Point(580, 155);
            this.playbutton.Name = "playbutton";
            this.playbutton.Size = new System.Drawing.Size(92, 35);
            this.playbutton.TabIndex = 2;
            this.playbutton.Text = "시작하기";
            this.playbutton.UseVisualStyleBackColor = true;
            this.playbutton.Click += new System.EventHandler(this.playbutton_Click);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(537, 12);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 12);
            this.status.TabIndex = 4;
            // 
            // status1
            // 
            this.status1.AutoSize = true;
            this.status1.Location = new System.Drawing.Point(548, 271);
            this.status1.Name = "status1";
            this.status1.Size = new System.Drawing.Size(0, 12);
            this.status1.TabIndex = 5;
            // 
            // Exitbutton
            // 
            this.Exitbutton.Location = new System.Drawing.Point(580, 207);
            this.Exitbutton.Name = "Exitbutton";
            this.Exitbutton.Size = new System.Drawing.Size(92, 35);
            this.Exitbutton.TabIndex = 2;
            this.Exitbutton.Text = "나가기";
            this.Exitbutton.UseVisualStyleBackColor = true;
            this.Exitbutton.Click += new System.EventHandler(this.Exitbutton_Click);
            // 
            // IDst
            // 
            this.IDst.AutoSize = true;
            this.IDst.Location = new System.Drawing.Point(592, 51);
            this.IDst.Name = "IDst";
            this.IDst.Size = new System.Drawing.Size(0, 12);
            this.IDst.TabIndex = 6;
            // 
            // MultiPlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 561);
            this.ControlBox = false;
            this.Controls.Add(this.IDst);
            this.Controls.Add(this.status1);
            this.Controls.Add(this.status);
            this.Controls.Add(this.Exitbutton);
            this.Controls.Add(this.playbutton);
            this.Controls.Add(this.BoardPicture);
            this.Name = "MultiPlayForm";
            this.Text = "MultiPlayForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MultiPlayForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BoardPicture;
        private System.Windows.Forms.Button playbutton;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label status1;
        private System.Windows.Forms.Button Exitbutton;
        private System.Windows.Forms.Label IDst;
    }
}