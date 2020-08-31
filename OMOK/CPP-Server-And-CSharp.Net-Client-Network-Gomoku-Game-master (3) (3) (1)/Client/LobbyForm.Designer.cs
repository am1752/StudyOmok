namespace Client
{
    partial class LobbyForm
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
            this.roomListView = new System.Windows.Forms.ListView();
            this.roomNumCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roomNameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roomPeopleCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roomObserveCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.peopleListView = new System.Windows.Forms.ListView();
            this.idCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chatInputTextBox = new System.Windows.Forms.TextBox();
            this.chatTextBox = new System.Windows.Forms.TextBox();
            this.createRoomButton = new System.Windows.Forms.Button();
            this.roomTitleTextBox = new System.Windows.Forms.TextBox();
            this.roomTitleLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.inviteButton = new System.Windows.Forms.Button();
            this.inviteTimePrg = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // roomListView
            // 
            this.roomListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.roomNumCol,
            this.roomNameCol,
            this.roomPeopleCol,
            this.roomObserveCol});
            this.roomListView.FullRowSelect = true;
            this.roomListView.HideSelection = false;
            this.roomListView.Location = new System.Drawing.Point(12, 12);
            this.roomListView.MultiSelect = false;
            this.roomListView.Name = "roomListView";
            this.roomListView.Size = new System.Drawing.Size(426, 209);
            this.roomListView.TabIndex = 0;
            this.roomListView.UseCompatibleStateImageBehavior = false;
            this.roomListView.View = System.Windows.Forms.View.Details;
            this.roomListView.DoubleClick += new System.EventHandler(this.roomListView_DoubleClick);
            // 
            // roomNumCol
            // 
            this.roomNumCol.Text = "방번호";
            // 
            // roomNameCol
            // 
            this.roomNameCol.Text = "방제목";
            this.roomNameCol.Width = 250;
            // 
            // roomPeopleCol
            // 
            this.roomPeopleCol.Text = "사람수";
            this.roomPeopleCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // roomObserveCol
            // 
            this.roomObserveCol.Text = "관전";
            // 
            // peopleListView
            // 
            this.peopleListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idCol});
            this.peopleListView.FullRowSelect = true;
            this.peopleListView.HideSelection = false;
            this.peopleListView.Location = new System.Drawing.Point(444, 12);
            this.peopleListView.MultiSelect = false;
            this.peopleListView.Name = "peopleListView";
            this.peopleListView.Size = new System.Drawing.Size(227, 209);
            this.peopleListView.TabIndex = 1;
            this.peopleListView.UseCompatibleStateImageBehavior = false;
            this.peopleListView.View = System.Windows.Forms.View.Details;
            // 
            // idCol
            // 
            this.idCol.Text = "아이디";
            this.idCol.Width = 150;
            // 
            // chatInputTextBox
            // 
            this.chatInputTextBox.Location = new System.Drawing.Point(12, 373);
            this.chatInputTextBox.Name = "chatInputTextBox";
            this.chatInputTextBox.Size = new System.Drawing.Size(426, 21);
            this.chatInputTextBox.TabIndex = 2;
            this.chatInputTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chatInputTextBox_KeyPress);
            // 
            // chatTextBox
            // 
            this.chatTextBox.Location = new System.Drawing.Point(12, 227);
            this.chatTextBox.Multiline = true;
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ReadOnly = true;
            this.chatTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatTextBox.Size = new System.Drawing.Size(426, 140);
            this.chatTextBox.TabIndex = 2;
            // 
            // createRoomButton
            // 
            this.createRoomButton.Location = new System.Drawing.Point(446, 260);
            this.createRoomButton.Name = "createRoomButton";
            this.createRoomButton.Size = new System.Drawing.Size(100, 43);
            this.createRoomButton.TabIndex = 3;
            this.createRoomButton.Text = "방생성";
            this.createRoomButton.UseVisualStyleBackColor = true;
            this.createRoomButton.Click += new System.EventHandler(this.createRoomButton_Click);
            // 
            // roomTitleTextBox
            // 
            this.roomTitleTextBox.Location = new System.Drawing.Point(491, 227);
            this.roomTitleTextBox.Name = "roomTitleTextBox";
            this.roomTitleTextBox.Size = new System.Drawing.Size(161, 21);
            this.roomTitleTextBox.TabIndex = 4;
            // 
            // roomTitleLabel
            // 
            this.roomTitleLabel.AutoSize = true;
            this.roomTitleLabel.Location = new System.Drawing.Point(444, 230);
            this.roomTitleLabel.Name = "roomTitleLabel";
            this.roomTitleLabel.Size = new System.Drawing.Size(41, 12);
            this.roomTitleLabel.TabIndex = 5;
            this.roomTitleLabel.Text = "방제목";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(552, 260);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(100, 43);
            this.refreshButton.TabIndex = 6;
            this.refreshButton.Text = "방목록갱신";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // inviteButton
            // 
            this.inviteButton.Location = new System.Drawing.Point(446, 310);
            this.inviteButton.Name = "inviteButton";
            this.inviteButton.Size = new System.Drawing.Size(206, 57);
            this.inviteButton.TabIndex = 7;
            this.inviteButton.Text = "초대";
            this.inviteButton.UseVisualStyleBackColor = true;
            this.inviteButton.Click += new System.EventHandler(this.inviteButton_Click);
            // 
            // inviteTimePrg
            // 
            this.inviteTimePrg.Location = new System.Drawing.Point(446, 371);
            this.inviteTimePrg.Maximum = 10;
            this.inviteTimePrg.Name = "inviteTimePrg";
            this.inviteTimePrg.Size = new System.Drawing.Size(206, 23);
            this.inviteTimePrg.TabIndex = 8;
            this.inviteTimePrg.Value = 10;
            // 
            // LobbyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 419);
            this.Controls.Add(this.inviteTimePrg);
            this.Controls.Add(this.inviteButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.roomTitleLabel);
            this.Controls.Add(this.roomTitleTextBox);
            this.Controls.Add(this.createRoomButton);
            this.Controls.Add(this.chatTextBox);
            this.Controls.Add(this.chatInputTextBox);
            this.Controls.Add(this.peopleListView);
            this.Controls.Add(this.roomListView);
            this.Name = "LobbyForm";
            this.Text = "LobbyForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LobbyForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView roomListView;
        private System.Windows.Forms.ColumnHeader roomNameCol;
        private System.Windows.Forms.ColumnHeader roomPeopleCol;
        private System.Windows.Forms.ListView peopleListView;
        private System.Windows.Forms.ColumnHeader idCol;
        private System.Windows.Forms.TextBox chatInputTextBox;
        private System.Windows.Forms.TextBox chatTextBox;
        private System.Windows.Forms.Button createRoomButton;
        private System.Windows.Forms.TextBox roomTitleTextBox;
        private System.Windows.Forms.Label roomTitleLabel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ColumnHeader roomNumCol;
        private System.Windows.Forms.ColumnHeader roomObserveCol;
        private System.Windows.Forms.Button inviteButton;
        private System.Windows.Forms.ProgressBar inviteTimePrg;
    }
}