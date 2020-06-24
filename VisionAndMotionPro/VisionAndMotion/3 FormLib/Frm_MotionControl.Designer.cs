namespace VisionAndMotionPro
{
    partial class Frm_MotionControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MotionControl));
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rdo_jog = new System.Windows.Forms.RadioButton();
            this.cbo_moveDistance = new System.Windows.Forms.ComboBox();
            this.cbx_axisName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pic_motorOnOrOff = new System.Windows.Forms.PictureBox();
            this.pic_ASTPStatu = new System.Windows.Forms.PictureBox();
            this.pic_ALMStatu = new System.Windows.Forms.PictureBox();
            this.pic_PELStatu = new System.Windows.Forms.PictureBox();
            this.pic_ORGStatu = new System.Windows.Forms.PictureBox();
            this.pic_NELStatu = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tkb_speed = new System.Windows.Forms.TrackBar();
            this.btn_axisSetting = new System.Windows.Forms.Button();
            this.btn_home = new System.Windows.Forms.Button();
            this.btn_moveBackward = new System.Windows.Forms.Button();
            this.btn_moveForeward = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_speed = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_moveToPointMM = new System.Windows.Forms.Button();
            this.btn_moveToPointPixel = new System.Windows.Forms.Button();
            this.tbx_targetPos = new System.Windows.Forms.TextBox();
            this.btn_stopMove = new System.Windows.Forms.Button();
            this.lnk_motorOnOrOff = new System.Windows.Forms.LinkLabel();
            this.dgv_pointList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_axisInfo = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_appear = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_touch = new System.Windows.Forms.Button();
            this.cbo_moveVel = new System.Windows.Forms.ComboBox();
            this.label145 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_motorOnOrOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ASTPStatu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ALMStatu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_PELStatu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ORGStatu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NELStatu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_speed)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_axisInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(162, 39);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 21);
            this.radioButton1.TabIndex = 25;
            this.radioButton1.Text = "连续";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rdo_jog
            // 
            this.rdo_jog.AutoSize = true;
            this.rdo_jog.Checked = true;
            this.rdo_jog.Location = new System.Drawing.Point(51, 39);
            this.rdo_jog.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.rdo_jog.Name = "rdo_jog";
            this.rdo_jog.Size = new System.Drawing.Size(50, 21);
            this.rdo_jog.TabIndex = 24;
            this.rdo_jog.TabStop = true;
            this.rdo_jog.Text = "点动";
            this.rdo_jog.UseVisualStyleBackColor = true;
            // 
            // cbo_moveDistance
            // 
            this.cbo_moveDistance.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbo_moveDistance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_moveDistance.FormattingEnabled = true;
            this.cbo_moveDistance.Items.AddRange(new object[] {
            "0.001",
            "0.01",
            "0.1",
            "1",
            "2",
            "5",
            "10",
            "20",
            "50",
            "100"});
            this.cbo_moveDistance.Location = new System.Drawing.Point(79, 114);
            this.cbo_moveDistance.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbo_moveDistance.Name = "cbo_moveDistance";
            this.cbo_moveDistance.Size = new System.Drawing.Size(138, 25);
            this.cbo_moveDistance.TabIndex = 2;
            this.cbo_moveDistance.Text = "5";
            // 
            // cbx_axisName
            // 
            this.cbx_axisName.BackColor = System.Drawing.Color.LightGray;
            this.cbx_axisName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_axisName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbx_axisName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_axisName.FormattingEnabled = true;
            this.cbx_axisName.Location = new System.Drawing.Point(79, 77);
            this.cbx_axisName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbx_axisName.Name = "cbx_axisName";
            this.cbx_axisName.Size = new System.Drawing.Size(168, 25);
            this.cbx_axisName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(213, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "ASTP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(176, 272);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "ALM";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(140, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "PEL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(97, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "ORG";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(59, 272);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "NEL";
            // 
            // pic_motorOnOrOff
            // 
            this.pic_motorOnOrOff.Image = ((System.Drawing.Image)(resources.GetObject("pic_motorOnOrOff.Image")));
            this.pic_motorOnOrOff.Location = new System.Drawing.Point(21, 237);
            this.pic_motorOnOrOff.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.pic_motorOnOrOff.Name = "pic_motorOnOrOff";
            this.pic_motorOnOrOff.Size = new System.Drawing.Size(28, 28);
            this.pic_motorOnOrOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_motorOnOrOff.TabIndex = 12;
            this.pic_motorOnOrOff.TabStop = false;
            this.pic_motorOnOrOff.Tag = "Off";
            // 
            // pic_ASTPStatu
            // 
            this.pic_ASTPStatu.Image = ((System.Drawing.Image)(resources.GetObject("pic_ASTPStatu.Image")));
            this.pic_ASTPStatu.Location = new System.Drawing.Point(216, 237);
            this.pic_ASTPStatu.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.pic_ASTPStatu.Name = "pic_ASTPStatu";
            this.pic_ASTPStatu.Size = new System.Drawing.Size(28, 28);
            this.pic_ASTPStatu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_ASTPStatu.TabIndex = 15;
            this.pic_ASTPStatu.TabStop = false;
            // 
            // pic_ALMStatu
            // 
            this.pic_ALMStatu.Image = ((System.Drawing.Image)(resources.GetObject("pic_ALMStatu.Image")));
            this.pic_ALMStatu.Location = new System.Drawing.Point(177, 237);
            this.pic_ALMStatu.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.pic_ALMStatu.Name = "pic_ALMStatu";
            this.pic_ALMStatu.Size = new System.Drawing.Size(28, 28);
            this.pic_ALMStatu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_ALMStatu.TabIndex = 14;
            this.pic_ALMStatu.TabStop = false;
            // 
            // pic_PELStatu
            // 
            this.pic_PELStatu.Image = ((System.Drawing.Image)(resources.GetObject("pic_PELStatu.Image")));
            this.pic_PELStatu.Location = new System.Drawing.Point(138, 237);
            this.pic_PELStatu.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.pic_PELStatu.Name = "pic_PELStatu";
            this.pic_PELStatu.Size = new System.Drawing.Size(28, 28);
            this.pic_PELStatu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_PELStatu.TabIndex = 13;
            this.pic_PELStatu.TabStop = false;
            // 
            // pic_ORGStatu
            // 
            this.pic_ORGStatu.Image = ((System.Drawing.Image)(resources.GetObject("pic_ORGStatu.Image")));
            this.pic_ORGStatu.Location = new System.Drawing.Point(99, 237);
            this.pic_ORGStatu.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.pic_ORGStatu.Name = "pic_ORGStatu";
            this.pic_ORGStatu.Size = new System.Drawing.Size(28, 28);
            this.pic_ORGStatu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_ORGStatu.TabIndex = 12;
            this.pic_ORGStatu.TabStop = false;
            // 
            // pic_NELStatu
            // 
            this.pic_NELStatu.Image = ((System.Drawing.Image)(resources.GetObject("pic_NELStatu.Image")));
            this.pic_NELStatu.Location = new System.Drawing.Point(60, 237);
            this.pic_NELStatu.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.pic_NELStatu.Name = "pic_NELStatu";
            this.pic_NELStatu.Size = new System.Drawing.Size(28, 28);
            this.pic_NELStatu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_NELStatu.TabIndex = 11;
            this.pic_NELStatu.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(18, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "点动距离：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(18, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "轴名称：";
            // 
            // tkb_speed
            // 
            this.tkb_speed.AutoSize = false;
            this.tkb_speed.Location = new System.Drawing.Point(16, 152);
            this.tkb_speed.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.tkb_speed.Maximum = 100;
            this.tkb_speed.Name = "tkb_speed";
            this.tkb_speed.Size = new System.Drawing.Size(186, 38);
            this.tkb_speed.TabIndex = 7;
            this.tkb_speed.Value = 20;
            this.tkb_speed.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // btn_axisSetting
            // 
            this.btn_axisSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_axisSetting.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_axisSetting.Image = ((System.Drawing.Image)(resources.GetObject("btn_axisSetting.Image")));
            this.btn_axisSetting.Location = new System.Drawing.Point(343, 235);
            this.btn_axisSetting.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btn_axisSetting.Name = "btn_axisSetting";
            this.btn_axisSetting.Size = new System.Drawing.Size(69, 52);
            this.btn_axisSetting.TabIndex = 6;
            this.btn_axisSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_axisSetting.UseVisualStyleBackColor = true;
            this.btn_axisSetting.Click += new System.EventHandler(this.btn_axisSetting_Click);
            // 
            // btn_home
            // 
            this.btn_home.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_home.Location = new System.Drawing.Point(271, 235);
            this.btn_home.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btn_home.Name = "btn_home";
            this.btn_home.Size = new System.Drawing.Size(69, 52);
            this.btn_home.TabIndex = 5;
            this.btn_home.Text = "回零";
            this.btn_home.UseVisualStyleBackColor = true;
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // btn_moveBackward
            // 
            this.btn_moveBackward.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_moveBackward.Location = new System.Drawing.Point(271, 103);
            this.btn_moveBackward.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btn_moveBackward.Name = "btn_moveBackward";
            this.btn_moveBackward.Size = new System.Drawing.Size(141, 63);
            this.btn_moveBackward.TabIndex = 4;
            this.btn_moveBackward.Text = "-";
            this.btn_moveBackward.UseVisualStyleBackColor = true;
            this.btn_moveBackward.Click += new System.EventHandler(this.btn_moveBackward_Click);
            // 
            // btn_moveForeward
            // 
            this.btn_moveForeward.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_moveForeward.Location = new System.Drawing.Point(271, 37);
            this.btn_moveForeward.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btn_moveForeward.Name = "btn_moveForeward";
            this.btn_moveForeward.Size = new System.Drawing.Size(141, 63);
            this.btn_moveForeward.TabIndex = 3;
            this.btn_moveForeward.Text = "+";
            this.btn_moveForeward.UseVisualStyleBackColor = true;
            this.btn_moveForeward.Click += new System.EventHandler(this.btn_moveForeward_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(218, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 17);
            this.label10.TabIndex = 22;
            this.label10.Text = "mm";
            // 
            // lbl_speed
            // 
            this.lbl_speed.AutoSize = true;
            this.lbl_speed.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_speed.Location = new System.Drawing.Point(200, 154);
            this.lbl_speed.Name = "lbl_speed";
            this.lbl_speed.Size = new System.Drawing.Size(55, 17);
            this.lbl_speed.TabIndex = 23;
            this.lbl_speed.Text = "20mm/s";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_moveToPointMM);
            this.groupBox1.Controls.Add(this.btn_moveToPointPixel);
            this.groupBox1.Controls.Add(this.tbx_targetPos);
            this.groupBox1.Controls.Add(this.btn_stopMove);
            this.groupBox1.Controls.Add(this.lnk_motorOnOrOff);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.rdo_jog);
            this.groupBox1.Controls.Add(this.cbo_moveDistance);
            this.groupBox1.Controls.Add(this.cbx_axisName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pic_motorOnOrOff);
            this.groupBox1.Controls.Add(this.pic_ASTPStatu);
            this.groupBox1.Controls.Add(this.pic_ALMStatu);
            this.groupBox1.Controls.Add(this.pic_PELStatu);
            this.groupBox1.Controls.Add(this.pic_ORGStatu);
            this.groupBox1.Controls.Add(this.pic_NELStatu);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tkb_speed);
            this.groupBox1.Controls.Add(this.btn_axisSetting);
            this.groupBox1.Controls.Add(this.btn_home);
            this.groupBox1.Controls.Add(this.btn_moveBackward);
            this.groupBox1.Controls.Add(this.btn_moveForeward);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lbl_speed);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(500, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.groupBox1.Size = new System.Drawing.Size(418, 356);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btn_moveToPointMM
            // 
            this.btn_moveToPointMM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_moveToPointMM.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_moveToPointMM.Location = new System.Drawing.Point(322, 319);
            this.btn_moveToPointMM.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btn_moveToPointMM.Name = "btn_moveToPointMM";
            this.btn_moveToPointMM.Size = new System.Drawing.Size(90, 28);
            this.btn_moveToPointMM.TabIndex = 30;
            this.btn_moveToPointMM.Text = "前往点(脉冲)";
            this.btn_moveToPointMM.UseVisualStyleBackColor = true;
            this.btn_moveToPointMM.Click += new System.EventHandler(this.btn_moveToPointMM_Click);
            // 
            // btn_moveToPointPixel
            // 
            this.btn_moveToPointPixel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_moveToPointPixel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_moveToPointPixel.Location = new System.Drawing.Point(231, 319);
            this.btn_moveToPointPixel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btn_moveToPointPixel.Name = "btn_moveToPointPixel";
            this.btn_moveToPointPixel.Size = new System.Drawing.Size(90, 28);
            this.btn_moveToPointPixel.TabIndex = 29;
            this.btn_moveToPointPixel.Text = "前往点(毫米)";
            this.btn_moveToPointPixel.UseVisualStyleBackColor = true;
            this.btn_moveToPointPixel.Click += new System.EventHandler(this.btn_moveToPoint_Click);
            // 
            // tbx_targetPos
            // 
            this.tbx_targetPos.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_targetPos.Location = new System.Drawing.Point(21, 320);
            this.tbx_targetPos.Name = "tbx_targetPos";
            this.tbx_targetPos.Size = new System.Drawing.Size(204, 26);
            this.tbx_targetPos.TabIndex = 28;
            // 
            // btn_stopMove
            // 
            this.btn_stopMove.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_stopMove.Location = new System.Drawing.Point(271, 169);
            this.btn_stopMove.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btn_stopMove.Name = "btn_stopMove";
            this.btn_stopMove.Size = new System.Drawing.Size(141, 63);
            this.btn_stopMove.TabIndex = 27;
            this.btn_stopMove.Text = "停止";
            this.btn_stopMove.UseVisualStyleBackColor = true;
            this.btn_stopMove.Click += new System.EventHandler(this.btn_stopMove_Click);
            // 
            // lnk_motorOnOrOff
            // 
            this.lnk_motorOnOrOff.AutoSize = true;
            this.lnk_motorOnOrOff.Location = new System.Drawing.Point(15, 272);
            this.lnk_motorOnOrOff.Name = "lnk_motorOnOrOff";
            this.lnk_motorOnOrOff.Size = new System.Drawing.Size(43, 17);
            this.lnk_motorOnOrOff.TabIndex = 26;
            this.lnk_motorOnOrOff.TabStop = true;
            this.lnk_motorOnOrOff.Text = "SVON";
            this.lnk_motorOnOrOff.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_motorOnOrOff_LinkClicked);
            // 
            // dgv_pointList
            // 
            this.dgv_pointList.AllowUserToResizeRows = false;
            this.dgv_pointList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pointList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgv_pointList.Location = new System.Drawing.Point(6, 42);
            this.dgv_pointList.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.dgv_pointList.MultiSelect = false;
            this.dgv_pointList.Name = "dgv_pointList";
            this.dgv_pointList.RowHeadersVisible = false;
            this.dgv_pointList.RowTemplate.Height = 23;
            this.dgv_pointList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_pointList.Size = new System.Drawing.Size(489, 318);
            this.dgv_pointList.TabIndex = 5;
            this.dgv_pointList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 65;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dgv_axisInfo
            // 
            this.dgv_axisInfo.AllowUserToAddRows = false;
            this.dgv_axisInfo.AllowUserToDeleteRows = false;
            this.dgv_axisInfo.AllowUserToResizeColumns = false;
            this.dgv_axisInfo.AllowUserToResizeRows = false;
            this.dgv_axisInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_axisInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Column1,
            this.dataGridViewTextBoxColumn5,
            this.Column2,
            this.Column3});
            this.dgv_axisInfo.Location = new System.Drawing.Point(6, 364);
            this.dgv_axisInfo.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.dgv_axisInfo.Name = "dgv_axisInfo";
            this.dgv_axisInfo.ReadOnly = true;
            this.dgv_axisInfo.RowHeadersVisible = false;
            this.dgv_axisInfo.RowTemplate.Height = 23;
            this.dgv_axisInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_axisInfo.Size = new System.Drawing.Size(912, 306);
            this.dgv_axisInfo.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "轴号";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 65;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "轴名称";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 123;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "命令值(脉冲)";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 106;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "命令值(毫米)";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 106;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "编码器(脉冲)";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "编码值(毫米)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // btn_delete
            // 
            this.btn_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_delete.BackgroundImage")));
            this.btn_delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_delete.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_delete.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_delete.Location = new System.Drawing.Point(438, 13);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_delete.Size = new System.Drawing.Size(57, 24);
            this.btn_delete.TabIndex = 27;
            this.btn_delete.Text = "删除";
            this.btn_delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_appear
            // 
            this.btn_appear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_appear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_appear.BackgroundImage")));
            this.btn_appear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_appear.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_appear.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btn_appear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_appear.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_appear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_appear.Location = new System.Drawing.Point(320, 13);
            this.btn_appear.Name = "btn_appear";
            this.btn_appear.Size = new System.Drawing.Size(57, 24);
            this.btn_appear.TabIndex = 25;
            this.btn_appear.Text = "重现";
            this.btn_appear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_appear.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_appear.UseVisualStyleBackColor = true;
            this.btn_appear.Click += new System.EventHandler(this.btn_appear_Click);
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_save.BackgroundImage")));
            this.btn_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_save.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_save.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Location = new System.Drawing.Point(379, 13);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(57, 24);
            this.btn_save.TabIndex = 26;
            this.btn_save.Text = "保存";
            this.btn_save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_touch
            // 
            this.btn_touch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_touch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_touch.BackgroundImage")));
            this.btn_touch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_touch.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_touch.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btn_touch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_touch.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_touch.Location = new System.Drawing.Point(261, 13);
            this.btn_touch.Name = "btn_touch";
            this.btn_touch.Size = new System.Drawing.Size(57, 24);
            this.btn_touch.TabIndex = 24;
            this.btn_touch.Text = " 试教";
            this.btn_touch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btn_touch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_touch.UseVisualStyleBackColor = true;
            this.btn_touch.Click += new System.EventHandler(this.btn_touch_Click);
            // 
            // cbo_moveVel
            // 
            this.cbo_moveVel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_moveVel.FormattingEnabled = true;
            this.cbo_moveVel.Items.AddRange(new object[] {
            "0.001",
            "0.01",
            "0.1",
            "1",
            "5",
            "10",
            "20",
            "50",
            "100"});
            this.cbo_moveVel.Location = new System.Drawing.Point(42, 12);
            this.cbo_moveVel.Name = "cbo_moveVel";
            this.cbo_moveVel.Size = new System.Drawing.Size(76, 25);
            this.cbo_moveVel.TabIndex = 28;
            this.cbo_moveVel.Text = "20";
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label145.Location = new System.Drawing.Point(5, 17);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(44, 17);
            this.label145.TabIndex = 29;
            this.label145.Text = "速度：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(118, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 17);
            this.label12.TabIndex = 26;
            this.label12.Text = "mm/s";
            // 
            // Frm_AxisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 676);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbo_moveVel);
            this.Controls.Add(this.label145);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_appear);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_touch);
            this.Controls.Add(this.dgv_axisInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_pointList);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(798, 558);
            this.Name = "Frm_AxisControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "轴控制";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Axis_FormClosing);
            this.Load += new System.EventHandler(this.Frm_AxisControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_motorOnOrOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ASTPStatu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ALMStatu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_PELStatu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ORGStatu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NELStatu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkb_speed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_axisInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rdo_jog;
        private System.Windows.Forms.ComboBox cbo_moveDistance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pic_motorOnOrOff;
        private System.Windows.Forms.PictureBox pic_ASTPStatu;
        private System.Windows.Forms.PictureBox pic_ALMStatu;
        private System.Windows.Forms.PictureBox pic_PELStatu;
        private System.Windows.Forms.PictureBox pic_ORGStatu;
        private System.Windows.Forms.PictureBox pic_NELStatu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar tkb_speed;
        private System.Windows.Forms.Button btn_axisSetting;
        private System.Windows.Forms.Button btn_home;
        private System.Windows.Forms.Button btn_moveBackward;
        private System.Windows.Forms.Button btn_moveForeward;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_speed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_appear;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ComboBox cbo_moveVel;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.Button btn_touch;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel lnk_motorOnOrOff;
        public System.Windows.Forms.DataGridView dgv_axisInfo;
        public System.Windows.Forms.DataGridView dgv_pointList;
        public System.Windows.Forms.ComboBox cbx_axisName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button btn_stopMove;
        private System.Windows.Forms.Button btn_moveToPointPixel;
        private System.Windows.Forms.TextBox tbx_targetPos;
        private System.Windows.Forms.Button btn_moveToPointMM;
    }
}