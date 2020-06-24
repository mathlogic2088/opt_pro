namespace VisionAndMotionPro
{
    partial class Frm_EyeHandCalibrationTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EyeHandCalibrationTool));
            this.btn_calibrate = new System.Windows.Forms.Button();
            this.cbo_calibrationType = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbx_scaleY = new System.Windows.Forms.TextBox();
            this.tbx_theta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbx_translateX = new System.Windows.Forms.TextBox();
            this.tbx_rotation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbx_translateY = new System.Windows.Forms.TextBox();
            this.tbx_scaleX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_writeCalibrationData = new System.Windows.Forms.Button();
            this.btn_readCalibrationData = new System.Windows.Forms.Button();
            this.dgv_calibrateData = new System.Windows.Forms.DataGridView();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label54 = new System.Windows.Forms.Label();
            this.ckb_eyeHandCalibrationToolEnable = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_runTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_resetTool = new System.Windows.Forms.ToolStripButton();
            this.tsb_help = new System.Windows.Forms.ToolStripButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_calibrateData)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_calibrate
            // 
            this.btn_calibrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_calibrate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_calibrate.Location = new System.Drawing.Point(439, 304);
            this.btn_calibrate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_calibrate.Name = "btn_calibrate";
            this.btn_calibrate.Size = new System.Drawing.Size(98, 45);
            this.btn_calibrate.TabIndex = 22;
            this.btn_calibrate.Text = "标定";
            this.btn_calibrate.UseVisualStyleBackColor = true;
            this.btn_calibrate.Click += new System.EventHandler(this.btn_calibrate_Click);
            // 
            // cbo_calibrationType
            // 
            this.cbo_calibrationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_calibrationType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_calibrationType.FormattingEnabled = true;
            this.cbo_calibrationType.Items.AddRange(new object[] {
            "四点标定",
            "九点标定"});
            this.cbo_calibrationType.Location = new System.Drawing.Point(70, 43);
            this.cbo_calibrationType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbo_calibrationType.Name = "cbo_calibrationType";
            this.cbo_calibrationType.Size = new System.Drawing.Size(251, 25);
            this.cbo_calibrationType.TabIndex = 20;
            this.cbo_calibrationType.SelectedIndexChanged += new System.EventHandler(this.cbo_calibrationType_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.tbx_scaleY);
            this.groupBox2.Controls.Add(this.tbx_theta);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbx_translateX);
            this.groupBox2.Controls.Add(this.tbx_rotation);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbx_translateY);
            this.groupBox2.Controls.Add(this.tbx_scaleX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(333, 43);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(204, 216);
            this.groupBox2.TabIndex = 85;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标定结果";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(165, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 89;
            this.label7.Text = "度";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(165, 148);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 17);
            this.label12.TabIndex = 88;
            this.label12.Text = "度";
            // 
            // tbx_scaleY
            // 
            this.tbx_scaleY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_scaleY.Location = new System.Drawing.Point(74, 115);
            this.tbx_scaleY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_scaleY.Name = "tbx_scaleY";
            this.tbx_scaleY.ReadOnly = true;
            this.tbx_scaleY.Size = new System.Drawing.Size(88, 23);
            this.tbx_scaleY.TabIndex = 78;
            // 
            // tbx_theta
            // 
            this.tbx_theta.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_theta.Location = new System.Drawing.Point(74, 175);
            this.tbx_theta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_theta.Name = "tbx_theta";
            this.tbx_theta.ReadOnly = true;
            this.tbx_theta.Size = new System.Drawing.Size(88, 23);
            this.tbx_theta.TabIndex = 84;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(19, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 73;
            this.label1.Text = "X平移：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(19, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 83;
            this.label6.Text = "轴斜切：";
            // 
            // tbx_translateX
            // 
            this.tbx_translateX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_translateX.Location = new System.Drawing.Point(74, 25);
            this.tbx_translateX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_translateX.Name = "tbx_translateX";
            this.tbx_translateX.ReadOnly = true;
            this.tbx_translateX.Size = new System.Drawing.Size(88, 23);
            this.tbx_translateX.TabIndex = 74;
            // 
            // tbx_rotation
            // 
            this.tbx_rotation.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_rotation.Location = new System.Drawing.Point(74, 145);
            this.tbx_rotation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_rotation.Name = "tbx_rotation";
            this.tbx_rotation.ReadOnly = true;
            this.tbx_rotation.Size = new System.Drawing.Size(88, 23);
            this.tbx_rotation.TabIndex = 82;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(19, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 75;
            this.label2.Text = "Y平移：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(19, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 17);
            this.label5.TabIndex = 81;
            this.label5.Text = "Y缩放：";
            // 
            // tbx_translateY
            // 
            this.tbx_translateY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_translateY.Location = new System.Drawing.Point(74, 55);
            this.tbx_translateY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_translateY.Name = "tbx_translateY";
            this.tbx_translateY.ReadOnly = true;
            this.tbx_translateY.Size = new System.Drawing.Size(88, 23);
            this.tbx_translateY.TabIndex = 76;
            // 
            // tbx_scaleX
            // 
            this.tbx_scaleX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_scaleX.Location = new System.Drawing.Point(74, 85);
            this.tbx_scaleX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_scaleX.Name = "tbx_scaleX";
            this.tbx_scaleX.ReadOnly = true;
            this.tbx_scaleX.Size = new System.Drawing.Size(88, 23);
            this.tbx_scaleX.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(19, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 77;
            this.label3.Text = "X缩放：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(19, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 79;
            this.label4.Text = "旋转：";
            // 
            // btn_writeCalibrationData
            // 
            this.btn_writeCalibrationData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_writeCalibrationData.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_writeCalibrationData.Location = new System.Drawing.Point(109, 323);
            this.btn_writeCalibrationData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_writeCalibrationData.Name = "btn_writeCalibrationData";
            this.btn_writeCalibrationData.Size = new System.Drawing.Size(92, 26);
            this.btn_writeCalibrationData.TabIndex = 71;
            this.btn_writeCalibrationData.Text = "导出标定数据";
            this.btn_writeCalibrationData.UseVisualStyleBackColor = true;
            this.btn_writeCalibrationData.Click += new System.EventHandler(this.btn_writeCalibrationData_Click);
            // 
            // btn_readCalibrationData
            // 
            this.btn_readCalibrationData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_readCalibrationData.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_readCalibrationData.Location = new System.Drawing.Point(11, 323);
            this.btn_readCalibrationData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_readCalibrationData.Name = "btn_readCalibrationData";
            this.btn_readCalibrationData.Size = new System.Drawing.Size(92, 26);
            this.btn_readCalibrationData.TabIndex = 72;
            this.btn_readCalibrationData.Text = "导入标定数据";
            this.btn_readCalibrationData.UseVisualStyleBackColor = true;
            this.btn_readCalibrationData.Click += new System.EventHandler(this.btn_readCalibrationData_Click);
            // 
            // dgv_calibrateData
            // 
            this.dgv_calibrateData.AllowUserToAddRows = false;
            this.dgv_calibrateData.AllowUserToDeleteRows = false;
            this.dgv_calibrateData.AllowUserToResizeColumns = false;
            this.dgv_calibrateData.AllowUserToResizeRows = false;
            this.dgv_calibrateData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_calibrateData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column15,
            this.Column16,
            this.Column13,
            this.Column14});
            this.dgv_calibrateData.Location = new System.Drawing.Point(11, 79);
            this.dgv_calibrateData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_calibrateData.Name = "dgv_calibrateData";
            this.dgv_calibrateData.RowHeadersVisible = false;
            this.dgv_calibrateData.RowTemplate.Height = 23;
            this.dgv_calibrateData.Size = new System.Drawing.Size(310, 234);
            this.dgv_calibrateData.TabIndex = 16;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "像素X";
            this.Column15.Name = "Column15";
            this.Column15.Width = 77;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "像素Y";
            this.Column16.Name = "Column16";
            this.Column16.Width = 77;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "机械X";
            this.Column13.Name = "Column13";
            this.Column13.Width = 77;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "机械Y";
            this.Column14.Name = "Column14";
            this.Column14.Width = 76;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label54.Location = new System.Drawing.Point(9, 46);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(68, 17);
            this.label54.TabIndex = 19;
            this.label54.Text = "标定类型：";
            // 
            // ckb_eyeHandCalibrationToolEnable
            // 
            this.ckb_eyeHandCalibrationToolEnable.AutoSize = true;
            this.ckb_eyeHandCalibrationToolEnable.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_eyeHandCalibrationToolEnable.Location = new System.Drawing.Point(482, 3);
            this.ckb_eyeHandCalibrationToolEnable.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.ckb_eyeHandCalibrationToolEnable.Name = "ckb_eyeHandCalibrationToolEnable";
            this.ckb_eyeHandCalibrationToolEnable.Size = new System.Drawing.Size(51, 21);
            this.ckb_eyeHandCalibrationToolEnable.TabIndex = 70;
            this.ckb_eyeHandCalibrationToolEnable.Text = "启用";
            this.ckb_eyeHandCalibrationToolEnable.UseVisualStyleBackColor = true;
            this.ckb_eyeHandCalibrationToolEnable.CheckedChanged += new System.EventHandler(this.ckb_eyeHandCalibrationToolEnable_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 17);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_runTool,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.tsb_resetTool,
            this.tsb_help});
            this.toolStrip1.Location = new System.Drawing.Point(-5, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(557, 25);
            this.toolStrip1.TabIndex = 88;
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
            this.tsb_runTool.Click += new System.EventHandler(this.tsb_runOnce_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.RightToLeftAutoMirrorImage = true;
            this.toolStripButton1.Size = new System.Drawing.Size(25, 22);
            this.toolStripButton1.Text = "toolStripButton5";
            this.toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButton1.ToolTipText = "运行流程";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            this.tsb_resetTool.Click += new System.EventHandler(this.tsb_resetTool_Click);
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
            // Frm_EyeHandCalibrationTool
            // 
            this.AcceptButton = this.btn_calibrate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(549, 361);
            this.Controls.Add(this.cbo_calibrationType);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_writeCalibrationData);
            this.Controls.Add(this.btn_readCalibrationData);
            this.Controls.Add(this.dgv_calibrateData);
            this.Controls.Add(this.label54);
            this.Controls.Add(this.ckb_eyeHandCalibrationToolEnable);
            this.Controls.Add(this.btn_calibrate);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(565, 400);
            this.MinimumSize = new System.Drawing.Size(565, 400);
            this.Name = "Frm_EyeHandCalibrationTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手眼标定";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_calibrateData)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Button btn_calibrate;
        internal System.Windows.Forms.CheckBox ckb_eyeHandCalibrationToolEnable;
        private System.Windows.Forms.Button btn_readCalibrationData;
        private System.Windows.Forms.Button btn_writeCalibrationData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox tbx_scaleY;
        internal System.Windows.Forms.TextBox tbx_theta;
        internal System.Windows.Forms.TextBox tbx_translateX;
        internal System.Windows.Forms.TextBox tbx_rotation;
        internal System.Windows.Forms.TextBox tbx_translateY;
        internal System.Windows.Forms.TextBox tbx_scaleX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.ComboBox cbo_calibrationType;
        internal System.Windows.Forms.DataGridView dgv_calibrateData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_runTool;
        private System.Windows.Forms.ToolStripButton tsb_resetTool;
        private System.Windows.Forms.ToolStripButton tsb_help;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}