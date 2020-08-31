namespace Client
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
            this.boardPicture = new System.Windows.Forms.PictureBox();
            this.status = new System.Windows.Forms.Label();
            this.readyButton = new System.Windows.Forms.Button();
            this.chatTextBox = new System.Windows.Forms.TextBox();
            this.chatInputTextBox = new System.Windows.Forms.TextBox();
            this.roomNumLabel = new System.Windows.Forms.Label();
            this.inviteButton = new System.Windows.Forms.Button();
            this.invitePanel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.inviteListView = new System.Windows.Forms.ListView();
            this.userListCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.inviteCloseButton = new System.Windows.Forms.Button();
            this.sInviteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.boardPicture)).BeginInit();
            this.invitePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // boardPicture
            // 
            this.boardPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(216)))));
            this.boardPicture.Location = new System.Drawing.Point(12, 12);
            this.boardPicture.Name = "boardPicture";
            this.boardPicture.Size = new System.Drawing.Size(500, 500);
            this.boardPicture.TabIndex = 0;
            this.boardPicture.TabStop = false;
            this.boardPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPicture_Paint);
            this.boardPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.boardPicture_MouseDown);
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(535, 79);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(272, 15);
            this.status.TabIndex = 4;
            this.status.Text = "다른 유저를 기다리는중.....";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // readyButton
            // 
            this.readyButton.Location = new System.Drawing.Point(537, 12);
            this.readyButton.Name = "readyButton";
            this.readyButton.Size = new System.Drawing.Size(139, 46);
            this.readyButton.TabIndex = 5;
            this.readyButton.Text = "준비";
            this.readyButton.UseVisualStyleBackColor = true;
            this.readyButton.Click += new System.EventHandler(this.readyButton_Click);
            // 
            // chatTextBox
            // 
            this.chatTextBox.Location = new System.Drawing.Point(518, 208);
            this.chatTextBox.Multiline = true;
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ReadOnly = true;
            this.chatTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatTextBox.Size = new System.Drawing.Size(199, 277);
            this.chatTextBox.TabIndex = 6;
            // 
            // chatInputTextBox
            // 
            this.chatInputTextBox.Location = new System.Drawing.Point(518, 492);
            this.chatInputTextBox.Name = "chatInputTextBox";
            this.chatInputTextBox.Size = new System.Drawing.Size(199, 21);
            this.chatInputTextBox.TabIndex = 7;
            this.chatInputTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chatInputTextBox_KeyPress);
            // 
            // roomNumLabel
            // 
            this.roomNumLabel.AutoSize = true;
            this.roomNumLabel.Location = new System.Drawing.Point(518, 193);
            this.roomNumLabel.Name = "roomNumLabel";
            this.roomNumLabel.Size = new System.Drawing.Size(41, 12);
            this.roomNumLabel.TabIndex = 8;
            this.roomNumLabel.Text = "방번호";
            // 
            // inviteButton
            // 
            this.inviteButton.Location = new System.Drawing.Point(723, 490);
            this.inviteButton.Name = "inviteButton";
            this.inviteButton.Size = new System.Drawing.Size(129, 23);
            this.inviteButton.TabIndex = 9;
            this.inviteButton.Text = "초대";
            this.inviteButton.UseVisualStyleBackColor = true;
            this.inviteButton.Click += new System.EventHandler(this.inviteButton_Click);
            // 
            // invitePanel
            // 
            this.invitePanel.Controls.Add(this.splitContainer1);
            this.invitePanel.Location = new System.Drawing.Point(646, 201);
            this.invitePanel.Name = "invitePanel";
            this.invitePanel.Size = new System.Drawing.Size(206, 283);
            this.invitePanel.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.inviteListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.inviteCloseButton);
            this.splitContainer1.Panel2.Controls.Add(this.sInviteButton);
            this.splitContainer1.Size = new System.Drawing.Size(206, 283);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 0;
            // 
            // inviteListView
            // 
            this.inviteListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.userListCol});
            this.inviteListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inviteListView.FullRowSelect = true;
            this.inviteListView.HideSelection = false;
            this.inviteListView.Location = new System.Drawing.Point(0, 0);
            this.inviteListView.Name = "inviteListView";
            this.inviteListView.Size = new System.Drawing.Size(206, 228);
            this.inviteListView.TabIndex = 0;
            this.inviteListView.UseCompatibleStateImageBehavior = false;
            this.inviteListView.View = System.Windows.Forms.View.Details;
            this.inviteListView.DoubleClick += new System.EventHandler(this.inviteListView_DoubleClick);
            // 
            // userListCol
            // 
            this.userListCol.Text = "아이디";
            this.userListCol.Width = 200;
            // 
            // inviteCloseButton
            // 
            this.inviteCloseButton.Location = new System.Drawing.Point(105, 2);
            this.inviteCloseButton.Name = "inviteCloseButton";
            this.inviteCloseButton.Size = new System.Drawing.Size(96, 42);
            this.inviteCloseButton.TabIndex = 0;
            this.inviteCloseButton.Text = "취소";
            this.inviteCloseButton.UseVisualStyleBackColor = true;
            this.inviteCloseButton.Click += new System.EventHandler(this.inviteCloseButton_Click);
            // 
            // sInviteButton
            // 
            this.sInviteButton.Location = new System.Drawing.Point(3, 2);
            this.sInviteButton.Name = "sInviteButton";
            this.sInviteButton.Size = new System.Drawing.Size(96, 42);
            this.sInviteButton.TabIndex = 0;
            this.sInviteButton.Text = "초대";
            this.sInviteButton.UseVisualStyleBackColor = true;
            this.sInviteButton.Click += new System.EventHandler(this.sInviteButton_Click);
            // 
            // MultiPlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 541);
            this.Controls.Add(this.invitePanel);
            this.Controls.Add(this.inviteButton);
            this.Controls.Add(this.roomNumLabel);
            this.Controls.Add(this.chatInputTextBox);
            this.Controls.Add(this.chatTextBox);
            this.Controls.Add(this.readyButton);
            this.Controls.Add(this.status);
            this.Controls.Add(this.boardPicture);
            this.Name = "MultiPlayForm";
            this.Text = "MultiPlayForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MultiPlayForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.boardPicture)).EndInit();
            this.invitePanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox boardPicture;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button readyButton;
        private System.Windows.Forms.TextBox chatTextBox;
        private System.Windows.Forms.TextBox chatInputTextBox;
        private System.Windows.Forms.Label roomNumLabel;
        private System.Windows.Forms.Button inviteButton;
        private System.Windows.Forms.Panel invitePanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView inviteListView;
        private System.Windows.Forms.Button inviteCloseButton;
        private System.Windows.Forms.Button sInviteButton;
        private System.Windows.Forms.ColumnHeader userListCol;
    }
}