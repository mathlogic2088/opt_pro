namespace VisionAndMotionPro._1_ToolLib._25_ConditionTool
{
    partial class Frm_ConditionTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ConditionTool));
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ckb_createROIToolEnable = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_runTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsb_resetTool = new System.Windows.Forms.ToolStripButton();
            this.tsb_help = new System.Windows.Forms.ToolStripButton();
            this.btn_runDistancePLTool = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_sucess_change = new System.Windows.Forms.ComboBox();
            this.cbx_sucess_item = new System.Windows.Forms.ComboBox();
            this.cbx_fail_change = new System.Windows.Forms.ComboBox();
            this.cbx_fail_item = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 105;
            this.label1.Text = "输入值：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ckb_createROIToolEnable
            // 
            this.ckb_createROIToolEnable.AutoSize = true;
            this.ckb_createROIToolEnable.BackColor = System.Drawing.Color.Transparent;
            this.ckb_createROIToolEnable.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_createROIToolEnable.Location = new System.Drawing.Point(521, 3);
            this.ckb_createROIToolEnable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckb_createROIToolEnable.Name = "ckb_createROIToolEnable";
            this.ckb_createROIToolEnable.Size = new System.Drawing.Size(51, 21);
            this.ckb_createROIToolEnable.TabIndex = 103;
            this.ckb_createROIToolEnable.Text = "启用";
            this.ckb_createROIToolEnable.UseVisualStyleBackColor = false;
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
            this.toolStrip1.Location = new System.Drawing.Point(-6, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(596, 25);
            this.toolStrip1.TabIndex = 104;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_runTool
            // 
            this.tsb_runTool.AutoSize = false;
            this.tsb_runTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_runTool.Image = ((System.Drawing.Image)(resources.GetObject("tsb_runTool.Image")));
            this.tsb_runTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_runTool.Name = "tsb_runTool";
            this.tsb_runTool.Size = new System.Drawing.Size(25, 22);
            this.tsb_runTool.Text = "toolStripButton5";
            this.tsb_runTool.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_runTool.ToolTipText = "运行工具";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(25, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButton1.ToolTipText = "运行流程";
            // 
            // tsb_resetTool
            // 
            this.tsb_resetTool.AutoSize = false;
            this.tsb_resetTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_resetTool.Image = ((System.Drawing.Image)(resources.GetObject("tsb_resetTool.Image")));
            this.tsb_resetTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_resetTool.Name = "tsb_resetTool";
            this.tsb_resetTool.Size = new System.Drawing.Size(25, 22);
            this.tsb_resetTool.Text = "toolStripButton4";
            this.tsb_resetTool.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_resetTool.ToolTipText = "复位工具";
            // 
            // tsb_help
            // 
            this.tsb_help.AutoSize = false;
            this.tsb_help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_help.Image = ((System.Drawing.Image)(resources.GetObject("tsb_help.Image")));
            this.tsb_help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_help.Name = "tsb_help";
            this.tsb_help.Size = new System.Drawing.Size(25, 22);
            this.tsb_help.Text = "toolStripButton7";
            this.tsb_help.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_help.ToolTipText = "帮助";
            // 
            // btn_runDistancePLTool
            // 
            this.btn_runDistancePLTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_runDistancePLTool.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_runDistancePLTool.Location = new System.Drawing.Point(596, 180);
            this.btn_runDistancePLTool.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_runDistancePLTool.Name = "btn_runDistancePLTool";
            this.btn_runDistancePLTool.Size = new System.Drawing.Size(98, 45);
            this.btn_runDistancePLTool.TabIndex = 102;
            this.btn_runDistancePLTool.Text = "运行";
            this.btn_runDistancePLTool.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(363, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(159, 20);
            this.textBox1.TabIndex = 108;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "大于",
            "等于",
            "小于",
            "大于等于",
            "小于等于"});
            this.comboBox1.Location = new System.Drawing.Point(226, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(108, 21);
            this.comboBox1.TabIndex = 109;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "表达式",
            "变量",
            "字符串",
            "逻辑真",
            "逻辑假"});
            this.comboBox2.Location = new System.Drawing.Point(573, 44);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 110;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "输入值1",
            "输入值2",
            "输入值3",
            "输入值4"});
            this.comboBox3.Location = new System.Drawing.Point(66, 45);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(135, 21);
            this.comboBox3.TabIndex = 111;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 112;
            this.label2.Text = "成功：";
            // 
            // cbx_sucess_change
            // 
            this.cbx_sucess_change.FormattingEnabled = true;
            this.cbx_sucess_change.Items.AddRange(new object[] {
            "跳转",
            "不跳转"});
            this.cbx_sucess_change.Location = new System.Drawing.Point(66, 88);
            this.cbx_sucess_change.Name = "cbx_sucess_change";
            this.cbx_sucess_change.Size = new System.Drawing.Size(121, 21);
            this.cbx_sucess_change.TabIndex = 113;
            // 
            // cbx_sucess_item
            // 
            this.cbx_sucess_item.FormattingEnabled = true;
            this.cbx_sucess_item.Location = new System.Drawing.Point(226, 88);
            this.cbx_sucess_item.Name = "cbx_sucess_item";
            this.cbx_sucess_item.Size = new System.Drawing.Size(121, 21);
            this.cbx_sucess_item.TabIndex = 114;
            // 
            // cbx_fail_change
            // 
            this.cbx_fail_change.FormattingEnabled = true;
            this.cbx_fail_change.Items.AddRange(new object[] {
            "跳转",
            "不跳转"});
            this.cbx_fail_change.Location = new System.Drawing.Point(66, 137);
            this.cbx_fail_change.Name = "cbx_fail_change";
            this.cbx_fail_change.Size = new System.Drawing.Size(121, 21);
            this.cbx_fail_change.TabIndex = 115;
            // 
            // cbx_fail_item
            // 
            this.cbx_fail_item.FormattingEnabled = true;
            this.cbx_fail_item.Location = new System.Drawing.Point(226, 140);
            this.cbx_fail_item.Name = "cbx_fail_item";
            this.cbx_fail_item.Size = new System.Drawing.Size(121, 21);
            this.cbx_fail_item.TabIndex = 116;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 117;
            this.label3.Text = "失败：";
            // 
            // Frm_ConditionTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 269);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbx_fail_item);
            this.Controls.Add(this.cbx_fail_change);
            this.Controls.Add(this.cbx_sucess_item);
            this.Controls.Add(this.cbx_sucess_change);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckb_createROIToolEnable);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btn_runDistancePLTool);
            this.Name = "Frm_ConditionTool";
            this.Text = "Frm_COnditionTool";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsb_help;
        private System.Windows.Forms.ToolStripButton tsb_resetTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsb_runTool;
        public System.Windows.Forms.CheckBox ckb_createROIToolEnable;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.Button btn_runDistancePLTool;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_sucess_change;
        private System.Windows.Forms.ComboBox cbx_sucess_item;
        private System.Windows.Forms.ComboBox cbx_fail_change;
        private System.Windows.Forms.ComboBox cbx_fail_item;
        private System.Windows.Forms.Label label3;
    }
}