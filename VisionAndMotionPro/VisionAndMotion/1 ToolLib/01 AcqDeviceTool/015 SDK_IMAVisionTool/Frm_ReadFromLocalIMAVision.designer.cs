namespace VisionAndMotionPro
{
    partial class Frm_ReadFromLocalIMAVision
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
            this.btn_browseImage = new System.Windows.Forms.Button();
            this.btn_selectImagePath = new System.Windows.Forms.Button();
            this.btn_readImage = new System.Windows.Forms.Button();
            this.ckb_autoSwitch = new System.Windows.Forms.CheckBox();
            this.gpb_oneImage = new System.Windows.Forms.GroupBox();
            this.tbx_imagePath = new System.Windows.Forms.TextBox();
            this.btn_selectImageDirectory = new System.Windows.Forms.Button();
            this.lbl_imageNum = new System.Windows.Forms.Label();
            this.lbl_imageName = new System.Windows.Forms.Label();
            this.pnl_multImage = new System.Windows.Forms.Panel();
            this.gpb_multImage = new System.Windows.Forms.GroupBox();
            this.btn_lastImage = new System.Windows.Forms.Button();
            this.btn_nextImage = new System.Windows.Forms.Button();
            this.tbx_imageDirectory = new System.Windows.Forms.TextBox();
            this.btn_runIMAVisionTool = new System.Windows.Forms.Button();
            this.btn_registImage = new System.Windows.Forms.Button();
            this.rdo_readOneImage = new System.Windows.Forms.RadioButton();
            this.rdo_readMultImage = new System.Windows.Forms.RadioButton();
            this.ckb_RGBToGray = new System.Windows.Forms.CheckBox();
            this.gpb_oneImage.SuspendLayout();
            this.pnl_multImage.SuspendLayout();
            this.gpb_multImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_browseImage
            // 
            this.btn_browseImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_browseImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_browseImage.Location = new System.Drawing.Point(108, 266);
            this.btn_browseImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_browseImage.Name = "btn_browseImage";
            this.btn_browseImage.Size = new System.Drawing.Size(76, 26);
            this.btn_browseImage.TabIndex = 90;
            this.btn_browseImage.Text = "查看图像";
            this.btn_browseImage.UseVisualStyleBackColor = true;
            // 
            // btn_selectImagePath
            // 
            this.btn_selectImagePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_selectImagePath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_selectImagePath.Location = new System.Drawing.Point(18, 99);
            this.btn_selectImagePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_selectImagePath.Name = "btn_selectImagePath";
            this.btn_selectImagePath.Size = new System.Drawing.Size(98, 32);
            this.btn_selectImagePath.TabIndex = 2;
            this.btn_selectImagePath.Text = "选择路径";
            this.btn_selectImagePath.UseVisualStyleBackColor = true;
            this.btn_selectImagePath.Click += new System.EventHandler(this.btn_selectImagePath_Click);
            // 
            // btn_readImage
            // 
            this.btn_readImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_readImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_readImage.Location = new System.Drawing.Point(339, 99);
            this.btn_readImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_readImage.Name = "btn_readImage";
            this.btn_readImage.Size = new System.Drawing.Size(69, 32);
            this.btn_readImage.TabIndex = 3;
            this.btn_readImage.Text = "加载";
            this.btn_readImage.UseVisualStyleBackColor = true;
            this.btn_readImage.Click += new System.EventHandler(this.btn_readImage_Click);
            // 
            // ckb_autoSwitch
            // 
            this.ckb_autoSwitch.AutoSize = true;
            this.ckb_autoSwitch.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_autoSwitch.Location = new System.Drawing.Point(26, 231);
            this.ckb_autoSwitch.Margin = new System.Windows.Forms.Padding(2);
            this.ckb_autoSwitch.Name = "ckb_autoSwitch";
            this.ckb_autoSwitch.Size = new System.Drawing.Size(99, 21);
            this.ckb_autoSwitch.TabIndex = 82;
            this.ckb_autoSwitch.Text = "图像自动切换";
            this.ckb_autoSwitch.UseVisualStyleBackColor = true;
            this.ckb_autoSwitch.CheckedChanged += new System.EventHandler(this.ckb_autoSwitch_CheckedChanged);
            // 
            // gpb_oneImage
            // 
            this.gpb_oneImage.Controls.Add(this.btn_selectImagePath);
            this.gpb_oneImage.Controls.Add(this.btn_readImage);
            this.gpb_oneImage.Controls.Add(this.tbx_imagePath);
            this.gpb_oneImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpb_oneImage.Location = new System.Drawing.Point(26, 43);
            this.gpb_oneImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gpb_oneImage.Name = "gpb_oneImage";
            this.gpb_oneImage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gpb_oneImage.Size = new System.Drawing.Size(451, 154);
            this.gpb_oneImage.TabIndex = 83;
            this.gpb_oneImage.TabStop = false;
            this.gpb_oneImage.Text = "图像路径";
            // 
            // tbx_imagePath
            // 
            this.tbx_imagePath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_imagePath.Location = new System.Drawing.Point(18, 28);
            this.tbx_imagePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_imagePath.Name = "tbx_imagePath";
            this.tbx_imagePath.Size = new System.Drawing.Size(415, 23);
            this.tbx_imagePath.TabIndex = 1;
            // 
            // btn_selectImageDirectory
            // 
            this.btn_selectImageDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_selectImageDirectory.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_selectImageDirectory.Location = new System.Drawing.Point(18, 99);
            this.btn_selectImageDirectory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_selectImageDirectory.Name = "btn_selectImageDirectory";
            this.btn_selectImageDirectory.Size = new System.Drawing.Size(98, 32);
            this.btn_selectImageDirectory.TabIndex = 63;
            this.btn_selectImageDirectory.Text = "选择路径";
            this.btn_selectImageDirectory.UseVisualStyleBackColor = true;
            this.btn_selectImageDirectory.Click += new System.EventHandler(this.btn_selectImageDirectory_Click);
            // 
            // lbl_imageNum
            // 
            this.lbl_imageNum.AutoSize = true;
            this.lbl_imageNum.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_imageNum.Location = new System.Drawing.Point(386, 70);
            this.lbl_imageNum.Name = "lbl_imageNum";
            this.lbl_imageNum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_imageNum.Size = new System.Drawing.Size(39, 17);
            this.lbl_imageNum.TabIndex = 62;
            this.lbl_imageNum.Text = "共0张";
            this.lbl_imageNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_imageName
            // 
            this.lbl_imageName.AutoSize = true;
            this.lbl_imageName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_imageName.Location = new System.Drawing.Point(15, 70);
            this.lbl_imageName.Name = "lbl_imageName";
            this.lbl_imageName.Size = new System.Drawing.Size(0, 17);
            this.lbl_imageName.TabIndex = 61;
            // 
            // pnl_multImage
            // 
            this.pnl_multImage.Controls.Add(this.gpb_multImage);
            this.pnl_multImage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_multImage.Location = new System.Drawing.Point(11, 41);
            this.pnl_multImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnl_multImage.Name = "pnl_multImage";
            this.pnl_multImage.Size = new System.Drawing.Size(480, 164);
            this.pnl_multImage.TabIndex = 87;
            this.pnl_multImage.Visible = false;
            // 
            // gpb_multImage
            // 
            this.gpb_multImage.Controls.Add(this.btn_selectImageDirectory);
            this.gpb_multImage.Controls.Add(this.lbl_imageNum);
            this.gpb_multImage.Controls.Add(this.lbl_imageName);
            this.gpb_multImage.Controls.Add(this.btn_lastImage);
            this.gpb_multImage.Controls.Add(this.btn_nextImage);
            this.gpb_multImage.Controls.Add(this.tbx_imageDirectory);
            this.gpb_multImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpb_multImage.Location = new System.Drawing.Point(15, 2);
            this.gpb_multImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gpb_multImage.Name = "gpb_multImage";
            this.gpb_multImage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gpb_multImage.Size = new System.Drawing.Size(451, 154);
            this.gpb_multImage.TabIndex = 15;
            this.gpb_multImage.TabStop = false;
            this.gpb_multImage.Text = "图像文件夹路径";
            // 
            // btn_lastImage
            // 
            this.btn_lastImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_lastImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_lastImage.Location = new System.Drawing.Point(339, 99);
            this.btn_lastImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_lastImage.Name = "btn_lastImage";
            this.btn_lastImage.Size = new System.Drawing.Size(33, 32);
            this.btn_lastImage.TabIndex = 9;
            this.btn_lastImage.TabStop = false;
            this.btn_lastImage.Text = "<<";
            this.btn_lastImage.UseVisualStyleBackColor = true;
            this.btn_lastImage.Click += new System.EventHandler(this.btn_lastImage_Click);
            // 
            // btn_nextImage
            // 
            this.btn_nextImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_nextImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_nextImage.Location = new System.Drawing.Point(375, 99);
            this.btn_nextImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_nextImage.Name = "btn_nextImage";
            this.btn_nextImage.Size = new System.Drawing.Size(33, 32);
            this.btn_nextImage.TabIndex = 8;
            this.btn_nextImage.TabStop = false;
            this.btn_nextImage.Text = ">>";
            this.btn_nextImage.UseVisualStyleBackColor = true;
            this.btn_nextImage.Click += new System.EventHandler(this.btn_nextImage_Click);
            // 
            // tbx_imageDirectory
            // 
            this.tbx_imageDirectory.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_imageDirectory.Location = new System.Drawing.Point(18, 28);
            this.tbx_imageDirectory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_imageDirectory.Multiline = true;
            this.tbx_imageDirectory.Name = "tbx_imageDirectory";
            this.tbx_imageDirectory.Size = new System.Drawing.Size(415, 38);
            this.tbx_imageDirectory.TabIndex = 5;
            // 
            // btn_runIMAVisionTool
            // 
            this.btn_runIMAVisionTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_runIMAVisionTool.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_runIMAVisionTool.Location = new System.Drawing.Point(388, 273);
            this.btn_runIMAVisionTool.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_runIMAVisionTool.Name = "btn_runIMAVisionTool";
            this.btn_runIMAVisionTool.Size = new System.Drawing.Size(98, 45);
            this.btn_runIMAVisionTool.TabIndex = 88;
            this.btn_runIMAVisionTool.TabStop = false;
            this.btn_runIMAVisionTool.Text = "运行";
            this.btn_runIMAVisionTool.UseVisualStyleBackColor = true;
            this.btn_runIMAVisionTool.Click += new System.EventHandler(this.btn_runIMAVisionTool_Click);
            // 
            // btn_registImage
            // 
            this.btn_registImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_registImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_registImage.Location = new System.Drawing.Point(26, 266);
            this.btn_registImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_registImage.Name = "btn_registImage";
            this.btn_registImage.Size = new System.Drawing.Size(76, 26);
            this.btn_registImage.TabIndex = 84;
            this.btn_registImage.Text = "注册图像";
            this.btn_registImage.UseVisualStyleBackColor = true;
            this.btn_registImage.Click += new System.EventHandler(this.btn_registImage_Click);
            // 
            // rdo_readOneImage
            // 
            this.rdo_readOneImage.AutoSize = true;
            this.rdo_readOneImage.Checked = true;
            this.rdo_readOneImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdo_readOneImage.Location = new System.Drawing.Point(319, 14);
            this.rdo_readOneImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdo_readOneImage.Name = "rdo_readOneImage";
            this.rdo_readOneImage.Size = new System.Drawing.Size(74, 21);
            this.rdo_readOneImage.TabIndex = 86;
            this.rdo_readOneImage.TabStop = true;
            this.rdo_readOneImage.Text = "单张图像";
            this.rdo_readOneImage.UseVisualStyleBackColor = true;
            // 
            // rdo_readMultImage
            // 
            this.rdo_readMultImage.AutoSize = true;
            this.rdo_readMultImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdo_readMultImage.Location = new System.Drawing.Point(93, 14);
            this.rdo_readMultImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdo_readMultImage.Name = "rdo_readMultImage";
            this.rdo_readMultImage.Size = new System.Drawing.Size(74, 21);
            this.rdo_readMultImage.TabIndex = 85;
            this.rdo_readMultImage.Text = "多张图像";
            this.rdo_readMultImage.UseVisualStyleBackColor = true;
            this.rdo_readMultImage.CheckedChanged += new System.EventHandler(this.rdo_readMultImage_CheckedChanged);
            // 
            // ckb_RGBToGray
            // 
            this.ckb_RGBToGray.AutoSize = true;
            this.ckb_RGBToGray.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_RGBToGray.Location = new System.Drawing.Point(26, 206);
            this.ckb_RGBToGray.Margin = new System.Windows.Forms.Padding(2);
            this.ckb_RGBToGray.Name = "ckb_RGBToGray";
            this.ckb_RGBToGray.Size = new System.Drawing.Size(99, 21);
            this.ckb_RGBToGray.TabIndex = 89;
            this.ckb_RGBToGray.Text = "彩图转灰度图";
            this.ckb_RGBToGray.UseVisualStyleBackColor = true;
            this.ckb_RGBToGray.CheckedChanged += new System.EventHandler(this.ckb_RGBToGray_CheckedChanged);
            // 
            // Frm_ReadFromLocalIMAVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(503, 332);
            this.Controls.Add(this.btn_browseImage);
            this.Controls.Add(this.ckb_autoSwitch);
            this.Controls.Add(this.pnl_multImage);
            this.Controls.Add(this.btn_runIMAVisionTool);
            this.Controls.Add(this.btn_registImage);
            this.Controls.Add(this.rdo_readOneImage);
            this.Controls.Add(this.rdo_readMultImage);
            this.Controls.Add(this.ckb_RGBToGray);
            this.Controls.Add(this.gpb_oneImage);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_ReadFromLocalIMAVision";
            this.gpb_oneImage.ResumeLayout(false);
            this.gpb_oneImage.PerformLayout();
            this.pnl_multImage.ResumeLayout(false);
            this.gpb_multImage.ResumeLayout(false);
            this.gpb_multImage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btn_browseImage;
        public System.Windows.Forms.Button btn_selectImagePath;
        internal System.Windows.Forms.Button btn_readImage;
        public System.Windows.Forms.CheckBox ckb_autoSwitch;
        public System.Windows.Forms.GroupBox gpb_oneImage;
        internal System.Windows.Forms.TextBox tbx_imagePath;
        public System.Windows.Forms.Button btn_selectImageDirectory;
        public System.Windows.Forms.Label lbl_imageNum;
        public System.Windows.Forms.Label lbl_imageName;
        internal System.Windows.Forms.Panel pnl_multImage;
        public System.Windows.Forms.GroupBox gpb_multImage;
        internal System.Windows.Forms.Button btn_lastImage;
        internal System.Windows.Forms.Button btn_nextImage;
        internal System.Windows.Forms.TextBox tbx_imageDirectory;
        public System.Windows.Forms.Button btn_runIMAVisionTool;
        public System.Windows.Forms.Button btn_registImage;
        public System.Windows.Forms.RadioButton rdo_readOneImage;
        public System.Windows.Forms.RadioButton rdo_readMultImage;
        public System.Windows.Forms.CheckBox ckb_RGBToGray;
    }
}