namespace VisionAndMotionPro
{
    partial class Frm_SDK_BaslerTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SDK_BaslerTool));
            this.ckb_SDKBaslerToolEnable = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_runTool = new System.Windows.Forms.ToolStripButton();
            this.tsb_runJob = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_realTimeDisplay = new System.Windows.Forms.ToolStripButton();
            this.tsb_registImage = new System.Windows.Forms.ToolStripButton();
            this.tsb_saveImage = new System.Windows.Forms.ToolStripButton();
            this.tsb_resetTool = new System.Windows.Forms.ToolStripButton();
            this.tsb_help = new System.Windows.Forms.ToolStripButton();
            this.btn_fromLocal = new System.Windows.Forms.Button();
            this.btn_fromDevice = new System.Windows.Forms.Button();
            this.pnl_formBox = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckb_SDKBaslerToolEnable
            // 
            this.ckb_SDKBaslerToolEnable.AutoSize = true;
            this.ckb_SDKBaslerToolEnable.BackColor = System.Drawing.Color.Transparent;
            this.ckb_SDKBaslerToolEnable.Location = new System.Drawing.Point(500, 3);
            this.ckb_SDKBaslerToolEnable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckb_SDKBaslerToolEnable.Name = "ckb_SDKBaslerToolEnable";
            this.ckb_SDKBaslerToolEnable.Size = new System.Drawing.Size(51, 21);
            this.ckb_SDKBaslerToolEnable.TabIndex = 89;
            this.ckb_SDKBaslerToolEnable.Text = "启用";
            this.ckb_SDKBaslerToolEnable.UseVisualStyleBackColor = false;
            this.ckb_SDKBaslerToolEnable.CheckedChanged += new System.EventHandler(this.ckb_SDKBaslerToolEnable_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 17);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_runTool,
            this.tsb_runJob,
            this.toolStripSeparator1,
            this.tsb_realTimeDisplay,
            this.tsb_registImage,
            this.tsb_saveImage,
            this.tsb_resetTool,
            this.tsb_help});
            this.toolStrip1.Location = new System.Drawing.Point(-5, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(573, 25);
            this.toolStrip1.TabIndex = 92;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_runTool
            // 
            this.tsb_runTool.AutoSize = false;
            this.tsb_runTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_runTool.Image = ((System.Drawing.Image)(resources.GetObject("tsb_runTool.Image")));
            this.tsb_runTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_runTool.Name = "tsb_runTool";
            this.tsb_runTool.RightToLeftAutoMirrorImage = true;
            this.tsb_runTool.Size = new System.Drawing.Size(25, 22);
            this.tsb_runTool.Text = "toolStripButton5";
            this.tsb_runTool.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_runTool.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_runTool.ToolTipText = "运行工具";
            this.tsb_runTool.Click += new System.EventHandler(this.tsb_runTool_Click);
            // 
            // tsb_runJob
            // 
            this.tsb_runJob.AutoSize = false;
            this.tsb_runJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_runJob.Image = ((System.Drawing.Image)(resources.GetObject("tsb_runJob.Image")));
            this.tsb_runJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_runJob.Name = "tsb_runJob";
            this.tsb_runJob.Size = new System.Drawing.Size(25, 22);
            this.tsb_runJob.Text = "toolStripButton1";
            this.tsb_runJob.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_runJob.ToolTipText = "运行流程";
            this.tsb_runJob.Click += new System.EventHandler(this.tsb_runJob_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_realTimeDisplay
            // 
            this.tsb_realTimeDisplay.AutoSize = false;
            this.tsb_realTimeDisplay.CheckOnClick = true;
            this.tsb_realTimeDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_realTimeDisplay.Image = ((System.Drawing.Image)(resources.GetObject("tsb_realTimeDisplay.Image")));
            this.tsb_realTimeDisplay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_realTimeDisplay.Name = "tsb_realTimeDisplay";
            this.tsb_realTimeDisplay.RightToLeftAutoMirrorImage = true;
            this.tsb_realTimeDisplay.Size = new System.Drawing.Size(25, 22);
            this.tsb_realTimeDisplay.Text = "toolStripButton6";
            this.tsb_realTimeDisplay.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_realTimeDisplay.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_realTimeDisplay.ToolTipText = "实时显示";
            this.tsb_realTimeDisplay.Click += new System.EventHandler(this.tsb_realTimeDisplay_Click);
            // 
            // tsb_registImage
            // 
            this.tsb_registImage.AutoSize = false;
            this.tsb_registImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_registImage.Image = ((System.Drawing.Image)(resources.GetObject("tsb_registImage.Image")));
            this.tsb_registImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_registImage.Name = "tsb_registImage";
            this.tsb_registImage.RightToLeftAutoMirrorImage = true;
            this.tsb_registImage.Size = new System.Drawing.Size(25, 22);
            this.tsb_registImage.Text = "toolStripButton1";
            this.tsb_registImage.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_registImage.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_registImage.ToolTipText = "注册图像";
            this.tsb_registImage.Click += new System.EventHandler(this.tsb_regiestImage_Click);
            // 
            // tsb_saveImage
            // 
            this.tsb_saveImage.AutoSize = false;
            this.tsb_saveImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_saveImage.Image = ((System.Drawing.Image)(resources.GetObject("tsb_saveImage.Image")));
            this.tsb_saveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_saveImage.Name = "tsb_saveImage";
            this.tsb_saveImage.RightToLeftAutoMirrorImage = true;
            this.tsb_saveImage.Size = new System.Drawing.Size(25, 22);
            this.tsb_saveImage.Text = "toolStripButton2";
            this.tsb_saveImage.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_saveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_saveImage.ToolTipText = "图像另存";
            this.tsb_saveImage.Click += new System.EventHandler(this.tsb_saveImage_Click);
            // 
            // tsb_resetTool
            // 
            this.tsb_resetTool.AutoSize = false;
            this.tsb_resetTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_resetTool.Image = ((System.Drawing.Image)(resources.GetObject("tsb_resetTool.Image")));
            this.tsb_resetTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_resetTool.Name = "tsb_resetTool";
            this.tsb_resetTool.RightToLeftAutoMirrorImage = true;
            this.tsb_resetTool.Size = new System.Drawing.Size(25, 22);
            this.tsb_resetTool.Text = "toolStripButton4";
            this.tsb_resetTool.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_resetTool.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_resetTool.ToolTipText = "复位工具";
            this.tsb_resetTool.Click += new System.EventHandler(this.tsb_reset_Click);
            // 
            // tsb_help
            // 
            this.tsb_help.AutoSize = false;
            this.tsb_help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_help.Image = ((System.Drawing.Image)(resources.GetObject("tsb_help.Image")));
            this.tsb_help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_help.Name = "tsb_help";
            this.tsb_help.RightToLeftAutoMirrorImage = true;
            this.tsb_help.Size = new System.Drawing.Size(25, 22);
            this.tsb_help.Text = "toolStripButton7";
            this.tsb_help.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_help.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_help.ToolTipText = "帮助";
            this.tsb_help.Click += new System.EventHandler(this.tsb_help_Click);
            // 
            // btn_fromLocal
            // 
            this.btn_fromLocal.Location = new System.Drawing.Point(1, 204);
            this.btn_fromLocal.Name = "btn_fromLocal";
            this.btn_fromLocal.Size = new System.Drawing.Size(47, 166);
            this.btn_fromLocal.TabIndex = 91;
            this.btn_fromLocal.Text = "本  地  读  取";
            this.btn_fromLocal.UseVisualStyleBackColor = true;
            this.btn_fromLocal.Click += new System.EventHandler(this.btn_fromLocal_Click);
            // 
            // btn_fromDevice
            // 
            this.btn_fromDevice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_fromDevice.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_fromDevice.Location = new System.Drawing.Point(1, 38);
            this.btn_fromDevice.Name = "btn_fromDevice";
            this.btn_fromDevice.Size = new System.Drawing.Size(47, 166);
            this.btn_fromDevice.TabIndex = 90;
            this.btn_fromDevice.Text = "设  备  采  集";
            this.btn_fromDevice.UseVisualStyleBackColor = true;
            this.btn_fromDevice.Click += new System.EventHandler(this.btn_fromDevice_Click);
            // 
            // pnl_formBox
            // 
            this.pnl_formBox.Location = new System.Drawing.Point(53, 38);
            this.pnl_formBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnl_formBox.Name = "pnl_formBox";
            this.pnl_formBox.Size = new System.Drawing.Size(503, 332);
            this.pnl_formBox.TabIndex = 88;
            // 
            // Frm_SDK_BaslerTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(567, 371);
            this.Controls.Add(this.ckb_SDKBaslerToolEnable);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btn_fromLocal);
            this.Controls.Add(this.btn_fromDevice);
            this.Controls.Add(this.pnl_formBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(583, 410);
            this.MinimumSize = new System.Drawing.Size(583, 410);
            this.Name = "Frm_SDK_BaslerTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDK_巴斯勒";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox ckb_SDKBaslerToolEnable;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_runTool;
        public System.Windows.Forms.ToolStripButton tsb_realTimeDisplay;
        private System.Windows.Forms.ToolStripButton tsb_registImage;
        private System.Windows.Forms.ToolStripButton tsb_saveImage;
        private System.Windows.Forms.ToolStripButton tsb_resetTool;
        private System.Windows.Forms.ToolStripButton tsb_help;
        public System.Windows.Forms.Button btn_fromLocal;
        public System.Windows.Forms.Button btn_fromDevice;
        public System.Windows.Forms.Panel pnl_formBox;
        private System.Windows.Forms.ToolStripButton tsb_runJob;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}