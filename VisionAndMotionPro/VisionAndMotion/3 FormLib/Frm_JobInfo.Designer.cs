namespace VisionAndMotionPro
{
    partial class Frm_JobInfo
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_jobName = new System.Windows.Forms.TextBox();
            this.btn_saveAndExit = new System.Windows.Forms.Button();
            this.btn_deleteCurrentStandardImage = new System.Windows.Forms.Button();
            this.btn_displayCurrentStandImage = new System.Windows.Forms.Button();
            this.cbx_standardImage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_imageWindowList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "流程名：";
            // 
            // tbx_jobName
            // 
            this.tbx_jobName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_jobName.Location = new System.Drawing.Point(77, 14);
            this.tbx_jobName.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_jobName.Name = "tbx_jobName";
            this.tbx_jobName.Size = new System.Drawing.Size(298, 23);
            this.tbx_jobName.TabIndex = 2;
            // 
            // btn_saveAndExit
            // 
            this.btn_saveAndExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_saveAndExit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_saveAndExit.Location = new System.Drawing.Point(290, 225);
            this.btn_saveAndExit.Margin = new System.Windows.Forms.Padding(2);
            this.btn_saveAndExit.Name = "btn_saveAndExit";
            this.btn_saveAndExit.Size = new System.Drawing.Size(85, 30);
            this.btn_saveAndExit.TabIndex = 3;
            this.btn_saveAndExit.Text = "应用并退出";
            this.btn_saveAndExit.UseVisualStyleBackColor = true;
            this.btn_saveAndExit.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_deleteCurrentStandardImage
            // 
            this.btn_deleteCurrentStandardImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_deleteCurrentStandardImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_deleteCurrentStandardImage.Location = new System.Drawing.Point(333, 41);
            this.btn_deleteCurrentStandardImage.Margin = new System.Windows.Forms.Padding(2);
            this.btn_deleteCurrentStandardImage.Name = "btn_deleteCurrentStandardImage";
            this.btn_deleteCurrentStandardImage.Size = new System.Drawing.Size(42, 25);
            this.btn_deleteCurrentStandardImage.TabIndex = 66;
            this.btn_deleteCurrentStandardImage.Text = "移除";
            this.btn_deleteCurrentStandardImage.UseVisualStyleBackColor = true;
            this.btn_deleteCurrentStandardImage.Click += new System.EventHandler(this.btn_deleteStandardImage_Click);
            // 
            // btn_displayCurrentStandImage
            // 
            this.btn_displayCurrentStandImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_displayCurrentStandImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_displayCurrentStandImage.Location = new System.Drawing.Point(288, 41);
            this.btn_displayCurrentStandImage.Margin = new System.Windows.Forms.Padding(2);
            this.btn_displayCurrentStandImage.Name = "btn_displayCurrentStandImage";
            this.btn_displayCurrentStandImage.Size = new System.Drawing.Size(42, 25);
            this.btn_displayCurrentStandImage.TabIndex = 65;
            this.btn_displayCurrentStandImage.Text = "显示";
            this.btn_displayCurrentStandImage.UseVisualStyleBackColor = false;
            this.btn_displayCurrentStandImage.Click += new System.EventHandler(this.btn_displayCurrentStandImage_Click);
            // 
            // cbx_standardImage
            // 
            this.cbx_standardImage.BackColor = System.Drawing.Color.LightGray;
            this.cbx_standardImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_standardImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbx_standardImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_standardImage.FormattingEnabled = true;
            this.cbx_standardImage.Location = new System.Drawing.Point(77, 41);
            this.cbx_standardImage.Margin = new System.Windows.Forms.Padding(2);
            this.cbx_standardImage.Name = "cbx_standardImage";
            this.cbx_standardImage.Size = new System.Drawing.Size(202, 25);
            this.cbx_standardImage.TabIndex = 64;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(17, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 67;
            this.label1.Text = "调试窗口：";
            // 
            // cbx_imageWindowList
            // 
            this.cbx_imageWindowList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_imageWindowList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_imageWindowList.FormattingEnabled = true;
            this.cbx_imageWindowList.Location = new System.Drawing.Point(77, 99);
            this.cbx_imageWindowList.Margin = new System.Windows.Forms.Padding(2);
            this.cbx_imageWindowList.Name = "cbx_imageWindowList";
            this.cbx_imageWindowList.Size = new System.Drawing.Size(185, 25);
            this.cbx_imageWindowList.TabIndex = 68;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(17, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 69;
            this.label3.Text = "标准图像：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(77, 70);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(185, 25);
            this.comboBox1.TabIndex = 71;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(17, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 70;
            this.label4.Text = "生产窗口：";
            // 
            // Frm_JobInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 268);
            this.Controls.Add(this.cbx_imageWindowList);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbx_standardImage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_deleteCurrentStandardImage);
            this.Controls.Add(this.btn_displayCurrentStandImage);
            this.Controls.Add(this.btn_saveAndExit);
            this.Controls.Add(this.tbx_jobName);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_JobInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "当前流程属性";
            this.Load += new System.EventHandler(this.Frm_JobInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbx_jobName;
        private System.Windows.Forms.Button btn_saveAndExit;
        private System.Windows.Forms.Button btn_deleteCurrentStandardImage;
        private System.Windows.Forms.Button btn_displayCurrentStandImage;
        internal System.Windows.Forms.ComboBox cbx_standardImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbx_imageWindowList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox comboBox1;
    }
}