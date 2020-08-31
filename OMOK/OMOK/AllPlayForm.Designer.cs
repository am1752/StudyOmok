namespace OMOK
{
    partial class AllPlayForm
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
            this.status = new System.Windows.Forms.Label();
            this.playbutton = new System.Windows.Forms.Button();
            this.BoardPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(560, 134);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 12);
            this.status.TabIndex = 5;
            // 
            // playbutton
            // 
            this.playbutton.Location = new System.Drawing.Point(611, 68);
            this.playbutton.Name = "playbutton";
            this.playbutton.Size = new System.Drawing.Size(94, 43);
            this.playbutton.TabIndex = 4;
            this.playbutton.Text = "게임시작";
            this.playbutton.UseVisualStyleBackColor = true;
            this.playbutton.Click += new System.EventHandler(this.playbutton_Click);
            // 
            // BoardPicture
            // 
            this.BoardPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(216)))));
            this.BoardPicture.Location = new System.Drawing.Point(40, 30);
            this.BoardPicture.Name = "BoardPicture";
            this.BoardPicture.Size = new System.Drawing.Size(500, 500);
            this.BoardPicture.TabIndex = 3;
            this.BoardPicture.TabStop = false;
            this.BoardPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardPicture_Paint);
            this.BoardPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoardPicture_MouseDown);
            // 
            // AllPlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 561);
            this.Controls.Add(this.status);
            this.Controls.Add(this.playbutton);
            this.Controls.Add(this.BoardPicture);
            this.Name = "AllPlayForm";
            this.Text = "AllPlayForm";
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button playbutton;
        private System.Windows.Forms.PictureBox BoardPicture;
    }
}