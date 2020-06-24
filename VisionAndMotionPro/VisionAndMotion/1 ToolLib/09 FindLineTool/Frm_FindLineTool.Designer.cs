namespace VisionAndMotionPro
{
    partial class Frm_FindLineTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_FindLineTool));
            this.btn_moveCliperRegion = new System.Windows.Forms.Button();
            this.btn_runFindLineTool = new System.Windows.Forms.Button();
            this.ckb_findLineToolEnable = new System.Windows.Forms.CheckBox();
            this.tbx_expectLineEndCol = new System.Windows.Forms.TextBox();
            this.tbx_minScore = new System.Windows.Forms.TextBox();
            this.tbx_expectLineStartRow = new System.Windows.Forms.TextBox();
            this.tbx_expectLineStartCol = new System.Windows.Forms.TextBox();
            this.tbx_expectLineEndRow = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbx_resultEndCol = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbx_resultStartRow = new System.Windows.Forms.TextBox();
            this.tbx_resultStartCol = new System.Windows.Forms.TextBox();
            this.tbx_resultEndRow = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_subMinScore = new System.Windows.Forms.Button();
            this.tbx_minScoreSpan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_addMinScore = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_subEndCol = new System.Windows.Forms.Button();
            this.tbx_endColSpan = new System.Windows.Forms.TextBox();
            this.btn_addEndCol = new System.Windows.Forms.Button();
            this.btn_subEndRow = new System.Windows.Forms.Button();
            this.tbx_endRowSpan = new System.Windows.Forms.TextBox();
            this.btn_addEndRow = new System.Windows.Forms.Button();
            this.btn_addStartRow = new System.Windows.Forms.Button();
            this.btn_subStartCol = new System.Windows.Forms.Button();
            this.tbx_startRowSpan = new System.Windows.Forms.TextBox();
            this.tbx_starColSpan = new System.Windows.Forms.TextBox();
            this.btn_subStartRow = new System.Windows.Forms.Button();
            this.btn_addStartCol = new System.Windows.Forms.Button();
            this.tbx_caliperNum = new System.Windows.Forms.TextBox();
            this.tbx_threshold = new System.Windows.Forms.TextBox();
            this.tbx_caliperLength = new System.Windows.Forms.TextBox();
            this.btn_subThreshold = new System.Windows.Forms.Button();
            this.tbx_thresholdSpan = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_addThreshold = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_subSliperLenght = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.tbx_cliperLengthSpan = new System.Windows.Forms.TextBox();
            this.btn_addSliperLength = new System.Windows.Forms.Button();
            this.btn_subSliperNum = new System.Windows.Forms.Button();
            this.tbx_cliperNumSpan = new System.Windows.Forms.TextBox();
            this.tbx_addSliperNum = new System.Windows.Forms.Button();
            this.cbx_polarity = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_runTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_resetTool = new System.Windows.Forms.ToolStripButton();
            this.tsb_help = new System.Windows.Forms.ToolStripButton();
            this.ckb_updateImage = new System.Windows.Forms.CheckBox();
            this.btn_switchPolarity = new System.Windows.Forms.Button();
            this.cbx_edgeSelect = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_moveCliperRegion
            // 
            this.btn_moveCliperRegion.Location = new System.Drawing.Point(222, 367);
            this.btn_moveCliperRegion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_moveCliperRegion.Name = "btn_moveCliperRegion";
            this.btn_moveCliperRegion.Size = new System.Drawing.Size(76, 26);
            this.btn_moveCliperRegion.TabIndex = 0;
            this.btn_moveCliperRegion.Text = "编辑卡尺";
            this.btn_moveCliperRegion.UseVisualStyleBackColor = true;
            this.btn_moveCliperRegion.Click += new System.EventHandler(this.btn_moveCliperRegion_Click);
            // 
            // btn_runFindLineTool
            // 
            this.btn_runFindLineTool.Location = new System.Drawing.Point(442, 358);
            this.btn_runFindLineTool.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_runFindLineTool.Name = "btn_runFindLineTool";
            this.btn_runFindLineTool.Size = new System.Drawing.Size(98, 45);
            this.btn_runFindLineTool.TabIndex = 1;
            this.btn_runFindLineTool.Text = "运行";
            this.btn_runFindLineTool.UseVisualStyleBackColor = true;
            this.btn_runFindLineTool.Click += new System.EventHandler(this.btn_runFindLineTool_Click);
            // 
            // ckb_findLineToolEnable
            // 
            this.ckb_findLineToolEnable.AutoSize = true;
            this.ckb_findLineToolEnable.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_findLineToolEnable.Location = new System.Drawing.Point(499, 3);
            this.ckb_findLineToolEnable.Name = "ckb_findLineToolEnable";
            this.ckb_findLineToolEnable.Size = new System.Drawing.Size(51, 21);
            this.ckb_findLineToolEnable.TabIndex = 79;
            this.ckb_findLineToolEnable.Text = "启用";
            this.ckb_findLineToolEnable.UseVisualStyleBackColor = true;
            this.ckb_findLineToolEnable.CheckedChanged += new System.EventHandler(this.ckb_findLineToolEnable_CheckedChanged);
            // 
            // tbx_expectLineEndCol
            // 
            this.tbx_expectLineEndCol.Location = new System.Drawing.Point(96, 128);
            this.tbx_expectLineEndCol.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_expectLineEndCol.Name = "tbx_expectLineEndCol";
            this.tbx_expectLineEndCol.Size = new System.Drawing.Size(71, 23);
            this.tbx_expectLineEndCol.TabIndex = 120;
            this.tbx_expectLineEndCol.TextChanged += new System.EventHandler(this.tbx_expectLineEndCol_TextChanged);
            // 
            // tbx_minScore
            // 
            this.tbx_minScore.Location = new System.Drawing.Point(96, 181);
            this.tbx_minScore.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_minScore.Name = "tbx_minScore";
            this.tbx_minScore.Size = new System.Drawing.Size(71, 23);
            this.tbx_minScore.TabIndex = 139;
            this.tbx_minScore.TextChanged += new System.EventHandler(this.tbx_minScore_TextChanged);
            // 
            // tbx_expectLineStartRow
            // 
            this.tbx_expectLineStartRow.Location = new System.Drawing.Point(96, 40);
            this.tbx_expectLineStartRow.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_expectLineStartRow.Name = "tbx_expectLineStartRow";
            this.tbx_expectLineStartRow.Size = new System.Drawing.Size(71, 23);
            this.tbx_expectLineStartRow.TabIndex = 114;
            this.tbx_expectLineStartRow.TextChanged += new System.EventHandler(this.tbx_expectLineStartRow_TextChanged);
            // 
            // tbx_expectLineStartCol
            // 
            this.tbx_expectLineStartCol.Location = new System.Drawing.Point(96, 70);
            this.tbx_expectLineStartCol.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_expectLineStartCol.Name = "tbx_expectLineStartCol";
            this.tbx_expectLineStartCol.Size = new System.Drawing.Size(71, 23);
            this.tbx_expectLineStartCol.TabIndex = 116;
            this.tbx_expectLineStartCol.TextChanged += new System.EventHandler(this.tbx_expectLineStartCol_TextChanged);
            // 
            // tbx_expectLineEndRow
            // 
            this.tbx_expectLineEndRow.Location = new System.Drawing.Point(96, 99);
            this.tbx_expectLineEndRow.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_expectLineEndRow.Name = "tbx_expectLineEndRow";
            this.tbx_expectLineEndRow.Size = new System.Drawing.Size(71, 23);
            this.tbx_expectLineEndRow.TabIndex = 118;
            this.tbx_expectLineEndRow.TextChanged += new System.EventHandler(this.tbx_expectLineEndRow_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbx_resultEndCol);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tbx_resultStartRow);
            this.groupBox2.Controls.Add(this.tbx_resultStartCol);
            this.groupBox2.Controls.Add(this.tbx_resultEndRow);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(339, 40);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(201, 167);
            this.groupBox2.TabIndex = 144;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果线";
            // 
            // tbx_resultEndCol
            // 
            this.tbx_resultEndCol.Location = new System.Drawing.Point(103, 129);
            this.tbx_resultEndCol.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_resultEndCol.Name = "tbx_resultEndCol";
            this.tbx_resultEndCol.Size = new System.Drawing.Size(71, 23);
            this.tbx_resultEndCol.TabIndex = 108;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 132);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 17);
            this.label8.TabIndex = 109;
            this.label8.Text = "终点列坐标：";
            // 
            // tbx_resultStartRow
            // 
            this.tbx_resultStartRow.Location = new System.Drawing.Point(103, 30);
            this.tbx_resultStartRow.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_resultStartRow.Name = "tbx_resultStartRow";
            this.tbx_resultStartRow.Size = new System.Drawing.Size(71, 23);
            this.tbx_resultStartRow.TabIndex = 102;
            // 
            // tbx_resultStartCol
            // 
            this.tbx_resultStartCol.Location = new System.Drawing.Point(103, 63);
            this.tbx_resultStartCol.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_resultStartCol.Name = "tbx_resultStartCol";
            this.tbx_resultStartCol.Size = new System.Drawing.Size(71, 23);
            this.tbx_resultStartCol.TabIndex = 104;
            // 
            // tbx_resultEndRow
            // 
            this.tbx_resultEndRow.Location = new System.Drawing.Point(103, 96);
            this.tbx_resultEndRow.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_resultEndRow.Name = "tbx_resultEndRow";
            this.tbx_resultEndRow.Size = new System.Drawing.Size(71, 23);
            this.tbx_resultEndRow.TabIndex = 106;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 33);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 17);
            this.label7.TabIndex = 103;
            this.label7.Text = "起点行坐标：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 99);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 17);
            this.label10.TabIndex = 107;
            this.label10.Text = "终点行坐标：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 66);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 17);
            this.label9.TabIndex = 105;
            this.label9.Text = "起点列坐标：";
            // 
            // btn_subMinScore
            // 
            this.btn_subMinScore.Location = new System.Drawing.Point(201, 181);
            this.btn_subMinScore.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_subMinScore.Name = "btn_subMinScore";
            this.btn_subMinScore.Size = new System.Drawing.Size(29, 25);
            this.btn_subMinScore.TabIndex = 143;
            this.btn_subMinScore.Text = "-";
            this.btn_subMinScore.UseVisualStyleBackColor = true;
            this.btn_subMinScore.Click += new System.EventHandler(this.btn_subMinScore_Click);
            // 
            // tbx_minScoreSpan
            // 
            this.tbx_minScoreSpan.Location = new System.Drawing.Point(232, 182);
            this.tbx_minScoreSpan.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_minScoreSpan.Name = "tbx_minScoreSpan";
            this.tbx_minScoreSpan.Size = new System.Drawing.Size(35, 23);
            this.tbx_minScoreSpan.TabIndex = 142;
            this.tbx_minScoreSpan.Text = "0.1";
            this.tbx_minScoreSpan.TextChanged += new System.EventHandler(this.tbx_minScoreSpan_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 101);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 119;
            this.label3.Text = "终点行坐标：";
            // 
            // btn_addMinScore
            // 
            this.btn_addMinScore.Location = new System.Drawing.Point(269, 181);
            this.btn_addMinScore.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_addMinScore.Name = "btn_addMinScore";
            this.btn_addMinScore.Size = new System.Drawing.Size(29, 25);
            this.btn_addMinScore.TabIndex = 141;
            this.btn_addMinScore.Text = "+";
            this.btn_addMinScore.UseVisualStyleBackColor = true;
            this.btn_addMinScore.Click += new System.EventHandler(this.btn_addMinScore_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 117;
            this.label2.Text = "起点列坐标：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 131);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 121;
            this.label4.Text = "终点列坐标：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 183);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 140;
            this.label6.Text = "最小分数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 115;
            this.label1.Text = "起点行坐标：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 333);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 123;
            this.label5.Text = "结果选择：";
            // 
            // btn_subEndCol
            // 
            this.btn_subEndCol.Location = new System.Drawing.Point(201, 127);
            this.btn_subEndCol.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_subEndCol.Name = "btn_subEndCol";
            this.btn_subEndCol.Size = new System.Drawing.Size(29, 25);
            this.btn_subEndCol.TabIndex = 135;
            this.btn_subEndCol.Text = "-";
            this.btn_subEndCol.UseVisualStyleBackColor = true;
            this.btn_subEndCol.Click += new System.EventHandler(this.btn_subEndCol_Click);
            // 
            // tbx_endColSpan
            // 
            this.tbx_endColSpan.Location = new System.Drawing.Point(232, 128);
            this.tbx_endColSpan.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_endColSpan.Name = "tbx_endColSpan";
            this.tbx_endColSpan.Size = new System.Drawing.Size(35, 23);
            this.tbx_endColSpan.TabIndex = 134;
            this.tbx_endColSpan.Text = "10";
            this.tbx_endColSpan.TextChanged += new System.EventHandler(this.tbx_endColSpan_TextChanged);
            // 
            // btn_addEndCol
            // 
            this.btn_addEndCol.Location = new System.Drawing.Point(269, 127);
            this.btn_addEndCol.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_addEndCol.Name = "btn_addEndCol";
            this.btn_addEndCol.Size = new System.Drawing.Size(29, 25);
            this.btn_addEndCol.TabIndex = 133;
            this.btn_addEndCol.Text = "+";
            this.btn_addEndCol.UseVisualStyleBackColor = true;
            this.btn_addEndCol.Click += new System.EventHandler(this.btn_addEndCol_Click);
            // 
            // btn_subEndRow
            // 
            this.btn_subEndRow.Location = new System.Drawing.Point(201, 98);
            this.btn_subEndRow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_subEndRow.Name = "btn_subEndRow";
            this.btn_subEndRow.Size = new System.Drawing.Size(29, 25);
            this.btn_subEndRow.TabIndex = 132;
            this.btn_subEndRow.Text = "-";
            this.btn_subEndRow.UseVisualStyleBackColor = true;
            this.btn_subEndRow.Click += new System.EventHandler(this.btn_subEndRow_Click);
            // 
            // tbx_endRowSpan
            // 
            this.tbx_endRowSpan.Location = new System.Drawing.Point(232, 99);
            this.tbx_endRowSpan.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_endRowSpan.Name = "tbx_endRowSpan";
            this.tbx_endRowSpan.Size = new System.Drawing.Size(35, 23);
            this.tbx_endRowSpan.TabIndex = 131;
            this.tbx_endRowSpan.Text = "10";
            this.tbx_endRowSpan.TextChanged += new System.EventHandler(this.tbx_endRowSpan_TextChanged);
            // 
            // btn_addEndRow
            // 
            this.btn_addEndRow.Location = new System.Drawing.Point(269, 98);
            this.btn_addEndRow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_addEndRow.Name = "btn_addEndRow";
            this.btn_addEndRow.Size = new System.Drawing.Size(29, 25);
            this.btn_addEndRow.TabIndex = 130;
            this.btn_addEndRow.Text = "+";
            this.btn_addEndRow.UseVisualStyleBackColor = true;
            this.btn_addEndRow.Click += new System.EventHandler(this.btn_addEndRow_Click);
            // 
            // btn_addStartRow
            // 
            this.btn_addStartRow.Location = new System.Drawing.Point(269, 39);
            this.btn_addStartRow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_addStartRow.Name = "btn_addStartRow";
            this.btn_addStartRow.Size = new System.Drawing.Size(29, 25);
            this.btn_addStartRow.TabIndex = 124;
            this.btn_addStartRow.Text = "+";
            this.btn_addStartRow.UseVisualStyleBackColor = true;
            this.btn_addStartRow.Click += new System.EventHandler(this.btn_addStartRow_Click);
            // 
            // btn_subStartCol
            // 
            this.btn_subStartCol.Location = new System.Drawing.Point(201, 69);
            this.btn_subStartCol.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_subStartCol.Name = "btn_subStartCol";
            this.btn_subStartCol.Size = new System.Drawing.Size(29, 25);
            this.btn_subStartCol.TabIndex = 129;
            this.btn_subStartCol.Text = "-";
            this.btn_subStartCol.UseVisualStyleBackColor = true;
            this.btn_subStartCol.Click += new System.EventHandler(this.btn_subStartCol_Click);
            // 
            // tbx_startRowSpan
            // 
            this.tbx_startRowSpan.Location = new System.Drawing.Point(232, 40);
            this.tbx_startRowSpan.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_startRowSpan.Name = "tbx_startRowSpan";
            this.tbx_startRowSpan.Size = new System.Drawing.Size(35, 23);
            this.tbx_startRowSpan.TabIndex = 125;
            this.tbx_startRowSpan.Text = "10";
            this.tbx_startRowSpan.TextChanged += new System.EventHandler(this.tbx_startRowSpan_TextChanged);
            // 
            // tbx_starColSpan
            // 
            this.tbx_starColSpan.Location = new System.Drawing.Point(232, 70);
            this.tbx_starColSpan.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_starColSpan.Name = "tbx_starColSpan";
            this.tbx_starColSpan.Size = new System.Drawing.Size(35, 23);
            this.tbx_starColSpan.TabIndex = 128;
            this.tbx_starColSpan.Text = "10";
            this.tbx_starColSpan.TextChanged += new System.EventHandler(this.tbx_starColSpan_TextChanged);
            // 
            // btn_subStartRow
            // 
            this.btn_subStartRow.Location = new System.Drawing.Point(201, 39);
            this.btn_subStartRow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_subStartRow.Name = "btn_subStartRow";
            this.btn_subStartRow.Size = new System.Drawing.Size(29, 25);
            this.btn_subStartRow.TabIndex = 126;
            this.btn_subStartRow.Text = "-";
            this.btn_subStartRow.UseVisualStyleBackColor = true;
            this.btn_subStartRow.Click += new System.EventHandler(this.btn_subStartRow_Click);
            // 
            // btn_addStartCol
            // 
            this.btn_addStartCol.Location = new System.Drawing.Point(269, 69);
            this.btn_addStartCol.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_addStartCol.Name = "btn_addStartCol";
            this.btn_addStartCol.Size = new System.Drawing.Size(29, 25);
            this.btn_addStartCol.TabIndex = 127;
            this.btn_addStartCol.Text = "+";
            this.btn_addStartCol.UseVisualStyleBackColor = true;
            this.btn_addStartCol.Click += new System.EventHandler(this.btn_addStartCol_Click);
            // 
            // tbx_caliperNum
            // 
            this.tbx_caliperNum.Location = new System.Drawing.Point(96, 211);
            this.tbx_caliperNum.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_caliperNum.Name = "tbx_caliperNum";
            this.tbx_caliperNum.Size = new System.Drawing.Size(71, 23);
            this.tbx_caliperNum.TabIndex = 147;
            this.tbx_caliperNum.TextChanged += new System.EventHandler(this.tbx_caliperNum_TextChanged);
            // 
            // tbx_threshold
            // 
            this.tbx_threshold.Location = new System.Drawing.Point(96, 271);
            this.tbx_threshold.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_threshold.Name = "tbx_threshold";
            this.tbx_threshold.Size = new System.Drawing.Size(71, 23);
            this.tbx_threshold.TabIndex = 160;
            this.tbx_threshold.TextChanged += new System.EventHandler(this.tbx_threshold_TextChanged);
            // 
            // tbx_caliperLength
            // 
            this.tbx_caliperLength.Location = new System.Drawing.Point(96, 241);
            this.tbx_caliperLength.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_caliperLength.Name = "tbx_caliperLength";
            this.tbx_caliperLength.Size = new System.Drawing.Size(71, 23);
            this.tbx_caliperLength.TabIndex = 149;
            this.tbx_caliperLength.TextChanged += new System.EventHandler(this.tbx_caliperLength_TextChanged);
            // 
            // btn_subThreshold
            // 
            this.btn_subThreshold.Location = new System.Drawing.Point(201, 271);
            this.btn_subThreshold.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_subThreshold.Name = "btn_subThreshold";
            this.btn_subThreshold.Size = new System.Drawing.Size(29, 25);
            this.btn_subThreshold.TabIndex = 164;
            this.btn_subThreshold.Text = "-";
            this.btn_subThreshold.UseVisualStyleBackColor = true;
            this.btn_subThreshold.Click += new System.EventHandler(this.btn_subThreshold_Click);
            // 
            // tbx_thresholdSpan
            // 
            this.tbx_thresholdSpan.Location = new System.Drawing.Point(232, 272);
            this.tbx_thresholdSpan.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_thresholdSpan.Name = "tbx_thresholdSpan";
            this.tbx_thresholdSpan.Size = new System.Drawing.Size(35, 23);
            this.tbx_thresholdSpan.TabIndex = 163;
            this.tbx_thresholdSpan.Text = "10";
            this.tbx_thresholdSpan.TextChanged += new System.EventHandler(this.tbx_thresholdSpan_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 303);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 17);
            this.label11.TabIndex = 146;
            this.label11.Text = "极性：";
            // 
            // btn_addThreshold
            // 
            this.btn_addThreshold.Location = new System.Drawing.Point(269, 271);
            this.btn_addThreshold.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_addThreshold.Name = "btn_addThreshold";
            this.btn_addThreshold.Size = new System.Drawing.Size(29, 25);
            this.btn_addThreshold.TabIndex = 162;
            this.btn_addThreshold.Text = "+";
            this.btn_addThreshold.UseVisualStyleBackColor = true;
            this.btn_addThreshold.Click += new System.EventHandler(this.btn_addThreshold_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 213);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 17);
            this.label12.TabIndex = 148;
            this.label12.Text = "卡尺数：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 273);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 17);
            this.label13.TabIndex = 161;
            this.label13.Text = "阈值：";
            // 
            // btn_subSliperLenght
            // 
            this.btn_subSliperLenght.Location = new System.Drawing.Point(201, 241);
            this.btn_subSliperLenght.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_subSliperLenght.Name = "btn_subSliperLenght";
            this.btn_subSliperLenght.Size = new System.Drawing.Size(29, 25);
            this.btn_subSliperLenght.TabIndex = 159;
            this.btn_subSliperLenght.Text = "-";
            this.btn_subSliperLenght.UseVisualStyleBackColor = true;
            this.btn_subSliperLenght.Click += new System.EventHandler(this.btn_subSliperLenght_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 243);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 17);
            this.label14.TabIndex = 150;
            this.label14.Text = "卡尺宽：";
            // 
            // tbx_cliperLengthSpan
            // 
            this.tbx_cliperLengthSpan.Location = new System.Drawing.Point(232, 242);
            this.tbx_cliperLengthSpan.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_cliperLengthSpan.Name = "tbx_cliperLengthSpan";
            this.tbx_cliperLengthSpan.Size = new System.Drawing.Size(35, 23);
            this.tbx_cliperLengthSpan.TabIndex = 158;
            this.tbx_cliperLengthSpan.Text = "10";
            this.tbx_cliperLengthSpan.TextChanged += new System.EventHandler(this.tbx_cliperLengthSpan_TextChanged);
            // 
            // btn_addSliperLength
            // 
            this.btn_addSliperLength.Location = new System.Drawing.Point(269, 241);
            this.btn_addSliperLength.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_addSliperLength.Name = "btn_addSliperLength";
            this.btn_addSliperLength.Size = new System.Drawing.Size(29, 25);
            this.btn_addSliperLength.TabIndex = 157;
            this.btn_addSliperLength.Text = "+";
            this.btn_addSliperLength.UseVisualStyleBackColor = true;
            this.btn_addSliperLength.Click += new System.EventHandler(this.btn_addSliperLength_Click);
            // 
            // btn_subSliperNum
            // 
            this.btn_subSliperNum.Location = new System.Drawing.Point(201, 211);
            this.btn_subSliperNum.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_subSliperNum.Name = "btn_subSliperNum";
            this.btn_subSliperNum.Size = new System.Drawing.Size(29, 25);
            this.btn_subSliperNum.TabIndex = 156;
            this.btn_subSliperNum.Text = "-";
            this.btn_subSliperNum.UseVisualStyleBackColor = true;
            this.btn_subSliperNum.Click += new System.EventHandler(this.btn_subSliperNum_Click);
            // 
            // tbx_cliperNumSpan
            // 
            this.tbx_cliperNumSpan.Location = new System.Drawing.Point(232, 212);
            this.tbx_cliperNumSpan.Margin = new System.Windows.Forms.Padding(2);
            this.tbx_cliperNumSpan.Name = "tbx_cliperNumSpan";
            this.tbx_cliperNumSpan.Size = new System.Drawing.Size(35, 23);
            this.tbx_cliperNumSpan.TabIndex = 155;
            this.tbx_cliperNumSpan.Text = "5";
            this.tbx_cliperNumSpan.TextChanged += new System.EventHandler(this.tbx_cliperNumSpan_TextChanged);
            // 
            // tbx_addSliperNum
            // 
            this.tbx_addSliperNum.Location = new System.Drawing.Point(269, 211);
            this.tbx_addSliperNum.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbx_addSliperNum.Name = "tbx_addSliperNum";
            this.tbx_addSliperNum.Size = new System.Drawing.Size(29, 25);
            this.tbx_addSliperNum.TabIndex = 154;
            this.tbx_addSliperNum.Text = "+";
            this.tbx_addSliperNum.UseVisualStyleBackColor = true;
            this.tbx_addSliperNum.Click += new System.EventHandler(this.tbx_addSliperNum_Click);
            // 
            // cbx_polarity
            // 
            this.cbx_polarity.FormattingEnabled = true;
            this.cbx_polarity.Items.AddRange(new object[] {
            "从明到暗",
            "从暗到明"});
            this.cbx_polarity.Location = new System.Drawing.Point(96, 301);
            this.cbx_polarity.Name = "cbx_polarity";
            this.cbx_polarity.Size = new System.Drawing.Size(71, 25);
            this.cbx_polarity.TabIndex = 165;
            this.cbx_polarity.Text = "从明到暗";
            this.cbx_polarity.SelectedIndexChanged += new System.EventHandler(this.cbx_polarity_SelectedIndexChanged);
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
            this.toolStrip1.Size = new System.Drawing.Size(574, 25);
            this.toolStrip1.TabIndex = 166;
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
            // ckb_updateImage
            // 
            this.ckb_updateImage.AutoSize = true;
            this.ckb_updateImage.Location = new System.Drawing.Point(339, 220);
            this.ckb_updateImage.Name = "ckb_updateImage";
            this.ckb_updateImage.Size = new System.Drawing.Size(99, 21);
            this.ckb_updateImage.TabIndex = 167;
            this.ckb_updateImage.Text = "刷新输入图像";
            this.ckb_updateImage.UseVisualStyleBackColor = true;
            this.ckb_updateImage.CheckedChanged += new System.EventHandler(this.ckb_updateImage_CheckedChanged);
            // 
            // btn_switchPolarity
            // 
            this.btn_switchPolarity.Location = new System.Drawing.Point(201, 300);
            this.btn_switchPolarity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_switchPolarity.Name = "btn_switchPolarity";
            this.btn_switchPolarity.Size = new System.Drawing.Size(97, 26);
            this.btn_switchPolarity.TabIndex = 168;
            this.btn_switchPolarity.Text = "切换";
            this.btn_switchPolarity.UseVisualStyleBackColor = true;
            this.btn_switchPolarity.Click += new System.EventHandler(this.btn_switchPolarity_Click);
            // 
            // cbx_edgeSelect
            // 
            this.cbx_edgeSelect.FormattingEnabled = true;
            this.cbx_edgeSelect.Items.AddRange(new object[] {
            "all"});
            this.cbx_edgeSelect.Location = new System.Drawing.Point(96, 332);
            this.cbx_edgeSelect.Name = "cbx_edgeSelect";
            this.cbx_edgeSelect.Size = new System.Drawing.Size(71, 25);
            this.cbx_edgeSelect.TabIndex = 169;
            this.cbx_edgeSelect.Text = "all";
            // 
            // Frm_FindLineTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(566, 410);
            this.Controls.Add(this.cbx_edgeSelect);
            this.Controls.Add(this.btn_switchPolarity);
            this.Controls.Add(this.ckb_updateImage);
            this.Controls.Add(this.cbx_polarity);
            this.Controls.Add(this.tbx_caliperNum);
            this.Controls.Add(this.tbx_threshold);
            this.Controls.Add(this.tbx_caliperLength);
            this.Controls.Add(this.btn_subThreshold);
            this.Controls.Add(this.tbx_thresholdSpan);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btn_addThreshold);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btn_subSliperLenght);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbx_cliperLengthSpan);
            this.Controls.Add(this.btn_addSliperLength);
            this.Controls.Add(this.btn_subSliperNum);
            this.Controls.Add(this.tbx_cliperNumSpan);
            this.Controls.Add(this.tbx_addSliperNum);
            this.Controls.Add(this.tbx_expectLineEndCol);
            this.Controls.Add(this.tbx_minScore);
            this.Controls.Add(this.tbx_expectLineStartRow);
            this.Controls.Add(this.tbx_expectLineStartCol);
            this.Controls.Add(this.tbx_expectLineEndRow);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_subMinScore);
            this.Controls.Add(this.tbx_minScoreSpan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_addMinScore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_subEndCol);
            this.Controls.Add(this.tbx_endColSpan);
            this.Controls.Add(this.btn_addEndCol);
            this.Controls.Add(this.btn_subEndRow);
            this.Controls.Add(this.tbx_endRowSpan);
            this.Controls.Add(this.btn_addEndRow);
            this.Controls.Add(this.btn_addStartRow);
            this.Controls.Add(this.btn_subStartCol);
            this.Controls.Add(this.tbx_startRowSpan);
            this.Controls.Add(this.tbx_starColSpan);
            this.Controls.Add(this.btn_subStartRow);
            this.Controls.Add(this.btn_addStartCol);
            this.Controls.Add(this.ckb_findLineToolEnable);
            this.Controls.Add(this.btn_runFindLineTool);
            this.Controls.Add(this.btn_moveCliperRegion);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(582, 449);
            this.MinimumSize = new System.Drawing.Size(582, 449);
            this.Name = "Frm_FindLineTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "找线";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_moveCliperRegion;
        public System.Windows.Forms.CheckBox ckb_findLineToolEnable;
        public System.Windows.Forms.TextBox tbx_expectLineEndCol;
        public System.Windows.Forms.TextBox tbx_minScore;
        public System.Windows.Forms.TextBox tbx_expectLineStartRow;
        public System.Windows.Forms.TextBox tbx_expectLineStartCol;
        public System.Windows.Forms.TextBox tbx_expectLineEndRow;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox tbx_resultEndCol;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox tbx_resultStartRow;
        public System.Windows.Forms.TextBox tbx_resultStartCol;
        public System.Windows.Forms.TextBox tbx_resultEndRow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_subMinScore;
        private System.Windows.Forms.TextBox tbx_minScoreSpan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_addMinScore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_subEndCol;
        private System.Windows.Forms.TextBox tbx_endColSpan;
        private System.Windows.Forms.Button btn_addEndCol;
        private System.Windows.Forms.Button btn_subEndRow;
        private System.Windows.Forms.TextBox tbx_endRowSpan;
        private System.Windows.Forms.Button btn_addEndRow;
        private System.Windows.Forms.Button btn_addStartRow;
        private System.Windows.Forms.Button btn_subStartCol;
        private System.Windows.Forms.TextBox tbx_startRowSpan;
        private System.Windows.Forms.TextBox tbx_starColSpan;
        private System.Windows.Forms.Button btn_subStartRow;
        private System.Windows.Forms.Button btn_addStartCol;
        public System.Windows.Forms.TextBox tbx_caliperNum;
        public System.Windows.Forms.TextBox tbx_threshold;
        public System.Windows.Forms.TextBox tbx_caliperLength;
        private System.Windows.Forms.Button btn_subThreshold;
        private System.Windows.Forms.TextBox tbx_thresholdSpan;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_addThreshold;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_subSliperLenght;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbx_cliperLengthSpan;
        private System.Windows.Forms.Button btn_addSliperLength;
        private System.Windows.Forms.Button btn_subSliperNum;
        private System.Windows.Forms.TextBox tbx_cliperNumSpan;
        private System.Windows.Forms.Button tbx_addSliperNum;
        public System.Windows.Forms.ComboBox cbx_polarity;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_runTool;
        private System.Windows.Forms.ToolStripButton tsb_resetTool;
        private System.Windows.Forms.ToolStripButton tsb_help;
        private System.Windows.Forms.Button btn_switchPolarity;
        public System.Windows.Forms.ComboBox cbx_edgeSelect;
        public System.Windows.Forms.Button btn_runFindLineTool;
        public System.Windows.Forms.CheckBox ckb_updateImage;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}