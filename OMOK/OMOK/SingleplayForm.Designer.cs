namespace OMOK
{
    partial class SingleplayForm
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
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // BoardPicture
            // 
            this.BoardPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(216)))));
            this.BoardPicture.Location = new System.Drawing.Point(13, 13);
            this.BoardPicture.Name = "BoardPicture";
            this.BoardPicture.Size = new System.Drawing.Size(500, 500);
            this.BoardPicture.TabIndex = 0;
            this.BoardPicture.TabStop = false;
            this.BoardPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardPicture_Paint);
            this.BoardPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoardPicture_MouseDown);
            // 
            // playbutton
            // 
            this.playbutton.Location = new System.Drawing.Point(584, 51);
            this.playbutton.Name = "playbutton";
            this.playbutton.Size = new System.Drawing.Size(94, 43);
            this.playbutton.TabIndex = 1;
            this.playbutton.Text = "게임시작";
            this.playbutton.UseVisualStyleBackColor = true;
            this.playbutton.Click += new System.EventHandler(this.playbutton_Click);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(533, 117);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 12);
            this.status.TabIndex = 2;
            // 
            // SingleplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 561);
            this.Controls.Add(this.status);
            this.Controls.Add(this.playbutton);
            this.Controls.Add(this.BoardPicture);
            this.Name = "SingleplayForm";
            this.Text = "SinglePlayForm";
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BoardPicture;
        private System.Windows.Forms.Button playbutton;
        private System.Windows.Forms.Label status;
    }
}