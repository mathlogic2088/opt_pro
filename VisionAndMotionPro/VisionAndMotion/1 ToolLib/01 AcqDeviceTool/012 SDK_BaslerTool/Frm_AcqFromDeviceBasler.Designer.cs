namespace VisionAndMotionPro
{
    partial class Frm_AcqFromDeviceBasler
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
            this.lbl_exposureRange = new System.Windows.Forms.Label();
            this.ckb_RGBToGray = new System.Windows.Forms.CheckBox();
            this.btn_RealTime = new System.Windows.Forms.Button();
            this.cbx_deviceList = new System.Windows.Forms.ComboBox();
            this.btn_runSDKBaslerTool = new System.Windows.Forms.Button();
            this.label148 = new System.Windows.Forms.Label();
            this.tbx_exposure = new System.Windows.Forms.TextBox();
            this.btn_registImage = new System.Windows.Forms.Button();
            this.label147 = new System.Windows.Forms.Label();
            this.btn_saveImage = new System.Windows.Forms.Button();
            this.tkb_exposure = new System.Windows.Forms.TrackBar();
            this.label123 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_exposure)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_exposureRange
            // 
            this.lbl_exposureRange.AutoSize = true;
            this.lbl_exposureRange.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_exposureRange.Location = new System.Drawing.Point(356, 77);
            this.lbl_exposureRange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_exposureRange.Name = "lbl_exposureRange";
            this.lbl_exposureRange.Size = new System.Drawing.Size(99, 17);
            this.lbl_exposureRange.TabIndex = 104;
            this.lbl_exposureRange.Text = "曝光范围：0 ~ 0";
            // 
            // ckb_RGBToGray
            // 
            this.ckb_RGBToGray.AutoSize = true;
            this.ckb_RGBToGray.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_RGBToGray.Location = new System.Drawing.Point(15, 135);
            this.ckb_RGBToGray.Margin = new System.Windows.Forms.Padding(2);
            this.ckb_RGBToGray.Name = "ckb_RGBToGray";
            this.ckb_RGBToGray.Size = new System.Drawing.Size(99, 21);
            this.ckb_RGBToGray.TabIndex = 103;
            this.ckb_RGBToGray.Text = "彩图转灰度图";
            this.ckb_RGBToGray.UseVisualStyleBackColor = true;
            this.ckb_RGBToGray.CheckedChanged += new System.EventHandler(this.ckb_RGBToGray_CheckedChanged);
            // 
            // btn_RealTime
            // 
            this.btn_RealTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RealTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_RealTime.Location = new System.Drawing.Point(95, 171);
            this.btn_RealTime.Margin = new System.Windows.Forms.Padding(2);
            this.btn_RealTime.Name = "btn_RealTime";
            this.btn_RealTime.Size = new System.Drawing.Size(76, 26);
            this.btn_RealTime.TabIndex = 100;
            this.btn_RealTime.Text = "实时显示";
            this.btn_RealTime.UseVisualStyleBackColor = true;
            this.btn_RealTime.Click += new System.EventHandler(this.btn_RealTime_Click);
            // 
            // cbx_deviceList
            // 
            this.cbx_deviceList.BackColor = System.Drawing.Color.LightGray;
            this.cbx_deviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_deviceList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbx_deviceList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_deviceList.FormattingEnabled = true;
            this.cbx_deviceList.Items.AddRange(new object[] {
            ""});
            this.cbx_deviceList.Location = new System.Drawing.Point(18, 32);
            this.cbx_deviceList.Margin = new System.Windows.Forms.Padding(2);
            this.cbx_deviceList.Name = "cbx_deviceList";
            this.cbx_deviceList.Size = new System.Drawing.Size(470, 25);
            this.cbx_deviceList.TabIndex = 93;
            this.cbx_deviceList.TextChanged += new System.EventHandler(this.cbx_deviceList_TextChanged);
            // 
            // btn_runSDKBaslerTool
            // 
            this.btn_runSDKBaslerTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_runSDKBaslerTool.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_runSDKBaslerTool.Location = new System.Drawing.Point(390, 274);
            this.btn_runSDKBaslerTool.Margin = new System.Windows.Forms.Padding(2);
            this.btn_runSDKBaslerTool.Name = "btn_runSDKBaslerTool";
            this.btn_runSDKBaslerTool.Size = new System.Drawing.Size(98, 45);
            this.btn_runSDKBaslerTool.TabIndex = 99;
            this.btn_runSDKBaslerTool.Text = "运行";
            this.btn_runSDKBaslerTool.UseVisualStyleBackColor = true;
            this.btn_runSDKBaslerTool.Click += new System.EventHandler(this.btn_runSDKBaslerTool_Click);
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label148.Location = new System.Drawing.Point(139, 78);
            this.label148.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(25, 17);
            this.label148.TabIndex = 97;
            this.label148.Text = "ms";
            // 
            // tbx_exposure
            // 
            this.tbx_exposure.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_exposure.Location = new System.Drawing.Point(78, 74);
            this.tbx_exposure.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_exposure.Name = "tbx_exposure";
            this.tbx_exposure.Size = new System.Drawing.Size(61, 23);
            this.tbx_exposure.TabIndex = 96;
            this.tbx_exposure.Text = "0";
            this.tbx_exposure.TextChanged += new System.EventHandler(this.tbx_exposure_TextChanged);
            // 
            // btn_registImage
            // 
            this.btn_registImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_registImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_registImage.Location = new System.Drawing.Point(15, 171);
            this.btn_registImage.Margin = new System.Windows.Forms.Padding(2);
            this.btn_registImage.Name = "btn_registImage";
            this.btn_registImage.Size = new System.Drawing.Size(76, 26);
            this.btn_registImage.TabIndex = 102;
            this.btn_registImage.Text = "注册图像";
            this.btn_registImage.UseVisualStyleBackColor = true;
            this.btn_registImage.Click += new System.EventHandler(this.btn_registImage_Click);
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label147.Location = new System.Drawing.Point(15, 77);
            this.label147.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(68, 17);
            this.label147.TabIndex = 94;
            this.label147.Text = "曝光时间：";
            // 
            // btn_saveImage
            // 
            this.btn_saveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_saveImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_saveImage.Location = new System.Drawing.Point(175, 171);
            this.btn_saveImage.Margin = new System.Windows.Forms.Padding(2);
            this.btn_saveImage.Name = "btn_saveImage";
            this.btn_saveImage.Size = new System.Drawing.Size(76, 26);
            this.btn_saveImage.TabIndex = 101;
            this.btn_saveImage.Text = "图像另存";
            this.btn_saveImage.UseVisualStyleBackColor = true;
            this.btn_saveImage.Click += new System.EventHandler(this.btn_saveImage_Click);
            // 
            // tkb_exposure
            // 
            this.tkb_exposure.AutoSize = false;
            this.tkb_exposure.Location = new System.Drawing.Point(15, 99);
            this.tkb_exposure.Margin = new System.Windows.Forms.Padding(2);
            this.tkb_exposure.Maximum = 100;
            this.tkb_exposure.Name = "tkb_exposure";
            this.tkb_exposure.Size = new System.Drawing.Size(473, 19);
            this.tkb_exposure.TabIndex = 95;
            this.tkb_exposure.Scroll += new System.EventHandler(this.tkb_exposure_Scroll);
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label123.Location = new System.Drawing.Point(15, 13);
            this.label123.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(56, 17);
            this.label123.TabIndex = 98;
            this.label123.Text = "设备列表";
            // 
            // Frm_AcqFromDeviceBasler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(503, 332);
            this.Controls.Add(this.lbl_exposureRange);
            this.Controls.Add(this.ckb_RGBToGray);
            this.Controls.Add(this.btn_RealTime);
            this.Controls.Add(this.cbx_deviceList);
            this.Controls.Add(this.btn_runSDKBaslerTool);
            this.Controls.Add(this.label148);
            this.Controls.Add(this.tbx_exposure);
            this.Controls.Add(this.btn_registImage);
            this.Controls.Add(this.label147);
            this.Controls.Add(this.btn_saveImage);
            this.Controls.Add(this.tkb_exposure);
            this.Controls.Add(this.label123);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_AcqFromDeviceBasler";
            this.Text = "Frm_AcquistionFromDevice";
            ((System.ComponentModel.ISupportInitialize)(this.tkb_exposure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_exposureRange;
        public System.Windows.Forms.CheckBox ckb_RGBToGray;
        internal System.Windows.Forms.Button btn_RealTime;
        internal System.Windows.Forms.ComboBox cbx_deviceList;
        internal System.Windows.Forms.Button btn_runSDKBaslerTool;
        private System.Windows.Forms.Label label148;
        internal System.Windows.Forms.TextBox tbx_exposure;
        internal System.Windows.Forms.Button btn_registImage;
        private System.Windows.Forms.Label label147;
        internal System.Windows.Forms.Button btn_saveImage;
        internal System.Windows.Forms.TrackBar tkb_exposure;
        private System.Windows.Forms.Label label123;
    }
}