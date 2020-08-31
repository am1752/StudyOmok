namespace Client
{
    partial class InviteForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.inviteListView = new System.Windows.Forms.ListView();
            this.userListCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.closeButton = new System.Windows.Forms.Button();
            this.inviteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.closeButton);
            this.splitContainer1.Panel2.Controls.Add(this.inviteButton);
            this.splitContainer1.Size = new System.Drawing.Size(258, 338);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 0;
            // 
            // inviteListView
            // 
            this.inviteListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.userListCol});
            this.inviteListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inviteListView.HideSelection = false;
            this.inviteListView.Location = new System.Drawing.Point(0, 0);
            this.inviteListView.Name = "inviteListView";
            this.inviteListView.Size = new System.Drawing.Size(258, 258);
            this.inviteListView.TabIndex = 0;
            this.inviteListView.UseCompatibleStateImageBehavior = false;
            this.inviteListView.View = System.Windows.Forms.View.Details;
            // 
            // userListCol
            // 
            this.userListCol.Text = "아이디";
            this.userListCol.Width = 300;
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.closeButton.Location = new System.Drawing.Point(131, 19);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(92, 34);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "취소";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // inviteButton
            // 
            this.inviteButton.Location = new System.Drawing.Point(33, 19);
            this.inviteButton.Name = "inviteButton";
            this.inviteButton.Size = new System.Drawing.Size(92, 34);
            this.inviteButton.TabIndex = 0;
            this.inviteButton.Text = "초대";
            this.inviteButton.UseVisualStyleBackColor = true;
            this.inviteButton.Click += new System.EventHandler(this.inviteButton_Click);
            // 
            // InviteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 338);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InviteForm";
            this.Text = "InviteForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView inviteListView;
        private System.Windows.Forms.ColumnHeader userListCol;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button inviteButton;
    }
}