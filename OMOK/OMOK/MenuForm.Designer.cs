namespace OMOK
{
    partial class MenuForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.singlePlayButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.multiPlayButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // singlePlayButton
            // 
            this.singlePlayButton.Location = new System.Drawing.Point(130, 32);
            this.singlePlayButton.Name = "singlePlayButton";
            this.singlePlayButton.Size = new System.Drawing.Size(135, 42);
            this.singlePlayButton.TabIndex = 0;
            this.singlePlayButton.Text = "혼자하기";
            this.singlePlayButton.UseVisualStyleBackColor = true;
            this.singlePlayButton.Click += new System.EventHandler(this.singlePlayButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(130, 276);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(135, 38);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "종료하기";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // multiPlayButton
            // 
            this.multiPlayButton.Location = new System.Drawing.Point(130, 90);
            this.multiPlayButton.Name = "multiPlayButton";
            this.multiPlayButton.Size = new System.Drawing.Size(135, 42);
            this.multiPlayButton.TabIndex = 1;
            this.multiPlayButton.Text = "함께하기";
            this.multiPlayButton.UseVisualStyleBackColor = true;
            this.multiPlayButton.Click += new System.EventHandler(this.multiPlayButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(130, 207);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(135, 42);
            this.SearchButton.TabIndex = 3;
            this.SearchButton.Text = "전적검색";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 42);
            this.button1.TabIndex = 2;
            this.button1.Text = "컴퓨터와 싸우기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AlphaPlayButton_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.multiPlayButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.singlePlayButton);
            this.Name = "MenuForm";
            this.Text = "오목 Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MenuForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button singlePlayButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button multiPlayButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button button1;
    }
}

