namespace VisionAndMotionPro
{
    partial class Frm_BlobAnalyseTool
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("开运算");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("闭运算");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("膨胀");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("腐蚀");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("填充");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BlobAnalyseTool));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_select = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbx_blobAnalyseSearchRegion = new System.Windows.Forms.ComboBox();
            this.nud_minThreshold = new System.Windows.Forms.NumericUpDown();
            this.label124 = new System.Windows.Forms.Label();
            this.nud_maxThreshold = new System.Windows.Forms.NumericUpDown();
            this.btn_runBlobAnalyseTool = new System.Windows.Forms.Button();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.btn_drawBlobAnalyseRegion = new System.Windows.Forms.Button();
            this.btn_SearchRegionDelete = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgv_processingItem = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_deletePreprocessingItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tvw_preProcessingItem = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_saveResultRegion = new System.Windows.Forms.Button();
            this.tbx_lineWidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ckb_displaySearchRegion = new System.Windows.Forms.CheckBox();
            this.ckb_displayCross = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdo_resultRegionMarginMode = new System.Windows.Forms.RadioButton();
            this.rdo_resultRegionFillMode = new System.Windows.Forms.RadioButton();
            this.ckb_showResultRegion = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdo_outCircleMarginMode = new System.Windows.Forms.RadioButton();
            this.rdo_outCircleFillMode = new System.Windows.Forms.RadioButton();
            this.ckb_showOutCircle = new System.Windows.Forms.CheckBox();
            this.ckb_fillHole = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgv_blobAnalyseResult = new System.Windows.Forms.DataGridView();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ckb_blobAnalyseToolEnable = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_runTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_resetTool = new System.Windows.Forms.ToolStripButton();
            this.tsb_help = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_select)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_minThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_maxThreshold)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_processingItem)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_blobAnalyseResult)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(596, 376);
            this.tabControl1.TabIndex = 95;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_select);
            this.tabPage1.Controls.Add(this.cbx_blobAnalyseSearchRegion);
            this.tabPage1.Controls.Add(this.nud_minThreshold);
            this.tabPage1.Controls.Add(this.label124);
            this.tabPage1.Controls.Add(this.nud_maxThreshold);
            this.tabPage1.Controls.Add(this.btn_runBlobAnalyseTool);
            this.tabPage1.Controls.Add(this.label77);
            this.tabPage1.Controls.Add(this.label78);
            this.tabPage1.Controls.Add(this.btn_drawBlobAnalyseRegion);
            this.tabPage1.Controls.Add(this.btn_SearchRegionDelete);
            this.tabPage1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(588, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "主页面";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_select
            // 
            this.dgv_select.AllowUserToOrderColumns = true;
            this.dgv_select.AllowUserToResizeColumns = false;
            this.dgv_select.AllowUserToResizeRows = false;
            this.dgv_select.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_select.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column10,
            this.Column5});
            this.dgv_select.Location = new System.Drawing.Point(27, 62);
            this.dgv_select.Name = "dgv_select";
            this.dgv_select.RowHeadersVisible = false;
            this.dgv_select.RowTemplate.Height = 23;
            this.dgv_select.Size = new System.Drawing.Size(359, 266);
            this.dgv_select.TabIndex = 97;
            this.dgv_select.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_select_CellValueChanged);
            // 
            // Column4
            // 
            this.Column4.FillWeight = 140F;
            this.Column4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column4.HeaderText = "筛选类型";
            this.Column4.Items.AddRange(new object[] {
            "",
            "area",
            "row",
            "column"});
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column4.Width = 106;
            // 
            // Column10
            // 
            this.Column10.FillWeight = 120F;
            this.Column10.HeaderText = "下限";
            this.Column10.Name = "Column10";
            this.Column10.Width = 125;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 120F;
            this.Column5.HeaderText = "上限";
            this.Column5.Name = "Column5";
            this.Column5.Width = 125;
            // 
            // cbx_blobAnalyseSearchRegion
            // 
            this.cbx_blobAnalyseSearchRegion.BackColor = System.Drawing.Color.LightGray;
            this.cbx_blobAnalyseSearchRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_blobAnalyseSearchRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbx_blobAnalyseSearchRegion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_blobAnalyseSearchRegion.FormattingEnabled = true;
            this.cbx_blobAnalyseSearchRegion.Items.AddRange(new object[] {
            "",
            "Rectangle1",
            "Rectangle2",
            "Circle",
            "InputRegion"});
            this.cbx_blobAnalyseSearchRegion.Location = new System.Drawing.Point(87, 22);
            this.cbx_blobAnalyseSearchRegion.Name = "cbx_blobAnalyseSearchRegion";
            this.cbx_blobAnalyseSearchRegion.Size = new System.Drawing.Size(195, 25);
            this.cbx_blobAnalyseSearchRegion.TabIndex = 89;
            this.cbx_blobAnalyseSearchRegion.SelectedIndexChanged += new System.EventHandler(this.cbx_blobAnalyseSearchRegion_SelectedIndexChanged);
            // 
            // nud_minThreshold
            // 
            this.nud_minThreshold.Location = new System.Drawing.Point(467, 90);
            this.nud_minThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nud_minThreshold.Name = "nud_minThreshold";
            this.nud_minThreshold.Size = new System.Drawing.Size(78, 23);
            this.nud_minThreshold.TabIndex = 1;
            this.nud_minThreshold.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nud_minThreshold.ValueChanged += new System.EventHandler(this.nud_minThreshold_ValueChanged);
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label124.Location = new System.Drawing.Point(24, 25);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(68, 17);
            this.label124.TabIndex = 86;
            this.label124.Text = "搜索区域：";
            // 
            // nud_maxThreshold
            // 
            this.nud_maxThreshold.Location = new System.Drawing.Point(467, 122);
            this.nud_maxThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nud_maxThreshold.Name = "nud_maxThreshold";
            this.nud_maxThreshold.Size = new System.Drawing.Size(78, 23);
            this.nud_maxThreshold.TabIndex = 3;
            this.nud_maxThreshold.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nud_maxThreshold.ValueChanged += new System.EventHandler(this.nud_maxThreshold_ValueChanged);
            // 
            // btn_runBlobAnalyseTool
            // 
            this.btn_runBlobAnalyseTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_runBlobAnalyseTool.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_runBlobAnalyseTool.Location = new System.Drawing.Point(447, 283);
            this.btn_runBlobAnalyseTool.Name = "btn_runBlobAnalyseTool";
            this.btn_runBlobAnalyseTool.Size = new System.Drawing.Size(98, 45);
            this.btn_runBlobAnalyseTool.TabIndex = 92;
            this.btn_runBlobAnalyseTool.Text = "运行";
            this.btn_runBlobAnalyseTool.UseVisualStyleBackColor = true;
            this.btn_runBlobAnalyseTool.Click += new System.EventHandler(this.btn_runBlobAnalyseTool_Click);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(392, 92);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(68, 17);
            this.label77.TabIndex = 0;
            this.label77.Text = "阈值下限：";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(392, 124);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(68, 17);
            this.label78.TabIndex = 2;
            this.label78.Text = "阈值上限：";
            // 
            // btn_drawBlobAnalyseRegion
            // 
            this.btn_drawBlobAnalyseRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_drawBlobAnalyseRegion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_drawBlobAnalyseRegion.Location = new System.Drawing.Point(288, 22);
            this.btn_drawBlobAnalyseRegion.Name = "btn_drawBlobAnalyseRegion";
            this.btn_drawBlobAnalyseRegion.Size = new System.Drawing.Size(49, 25);
            this.btn_drawBlobAnalyseRegion.TabIndex = 87;
            this.btn_drawBlobAnalyseRegion.Text = "绘制";
            this.btn_drawBlobAnalyseRegion.UseVisualStyleBackColor = true;
            this.btn_drawBlobAnalyseRegion.Click += new System.EventHandler(this.cbo_blobAnalyseSearchRegion_SelectedIndexChanged);
            // 
            // btn_SearchRegionDelete
            // 
            this.btn_SearchRegionDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SearchRegionDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SearchRegionDelete.Location = new System.Drawing.Point(340, 22);
            this.btn_SearchRegionDelete.Name = "btn_SearchRegionDelete";
            this.btn_SearchRegionDelete.Size = new System.Drawing.Size(46, 25);
            this.btn_SearchRegionDelete.TabIndex = 88;
            this.btn_SearchRegionDelete.Text = "删除";
            this.btn_SearchRegionDelete.UseVisualStyleBackColor = true;
            this.btn_SearchRegionDelete.Click += new System.EventHandler(this.btn_SearchRegionDelete_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgv_processingItem);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.tvw_preProcessingItem);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(588, 346);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "区域处理";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgv_processingItem
            // 
            this.dgv_processingItem.AllowUserToAddRows = false;
            this.dgv_processingItem.AllowUserToResizeRows = false;
            this.dgv_processingItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_processingItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column7});
            this.dgv_processingItem.ContextMenuStrip = this.contextMenuStrip2;
            this.dgv_processingItem.Location = new System.Drawing.Point(225, 37);
            this.dgv_processingItem.MultiSelect = false;
            this.dgv_processingItem.Name = "dgv_processingItem";
            this.dgv_processingItem.ReadOnly = true;
            this.dgv_processingItem.RowHeadersVisible = false;
            this.dgv_processingItem.RowTemplate.Height = 23;
            this.dgv_processingItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_processingItem.Size = new System.Drawing.Size(353, 298);
            this.dgv_processingItem.TabIndex = 94;
            this.dgv_processingItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_preProcessingItem_CellContentClick);
            this.dgv_processingItem.DoubleClick += new System.EventHandler(this.lbx_preProcessingItem_DoubleClick);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "处理项";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 270;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "    启用";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column7.Width = 80;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_deletePreprocessingItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(101, 26);
            // 
            // tsm_deletePreprocessingItem
            // 
            this.tsm_deletePreprocessingItem.Name = "tsm_deletePreprocessingItem";
            this.tsm_deletePreprocessingItem.Size = new System.Drawing.Size(100, 22);
            this.tsm_deletePreprocessingItem.Text = "删除";
            this.tsm_deletePreprocessingItem.Click += new System.EventHandler(this.tsm_deletePreprocessingItem_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 17);
            this.label9.TabIndex = 11;
            this.label9.Text = "处理项列表";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "已添加处理项列表";
            // 
            // tvw_preProcessingItem
            // 
            this.tvw_preProcessingItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvw_preProcessingItem.Location = new System.Drawing.Point(8, 37);
            this.tvw_preProcessingItem.Margin = new System.Windows.Forms.Padding(5);
            this.tvw_preProcessingItem.Name = "tvw_preProcessingItem";
            treeNode1.Name = "节点0";
            treeNode1.Text = "开运算";
            treeNode2.Name = "节点1";
            treeNode2.Text = "闭运算";
            treeNode3.Name = "节点3";
            treeNode3.Text = "膨胀";
            treeNode4.Name = "节点2";
            treeNode4.Text = "腐蚀";
            treeNode5.Name = "节点0";
            treeNode5.Text = "填充";
            this.tvw_preProcessingItem.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.tvw_preProcessingItem.Size = new System.Drawing.Size(203, 178);
            this.tvw_preProcessingItem.TabIndex = 3;
            this.tvw_preProcessingItem.DoubleClick += new System.EventHandler(this.tvw_preProcessingItem_DoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_saveResultRegion);
            this.tabPage2.Controls.Add(this.tbx_lineWidth);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.ckb_displaySearchRegion);
            this.tabPage2.Controls.Add(this.ckb_displayCross);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.ckb_fillHole);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(588, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "显示与日志";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_saveResultRegion
            // 
            this.btn_saveResultRegion.Location = new System.Drawing.Point(264, 23);
            this.btn_saveResultRegion.Name = "btn_saveResultRegion";
            this.btn_saveResultRegion.Size = new System.Drawing.Size(98, 23);
            this.btn_saveResultRegion.TabIndex = 107;
            this.btn_saveResultRegion.Text = "结果区域另存";
            this.btn_saveResultRegion.UseVisualStyleBackColor = true;
            this.btn_saveResultRegion.Click += new System.EventHandler(this.btn_saveResultRegion_Click);
            // 
            // tbx_lineWidth
            // 
            this.tbx_lineWidth.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_lineWidth.Location = new System.Drawing.Point(89, 103);
            this.tbx_lineWidth.Name = "tbx_lineWidth";
            this.tbx_lineWidth.Size = new System.Drawing.Size(74, 23);
            this.tbx_lineWidth.TabIndex = 104;
            this.tbx_lineWidth.TextChanged += new System.EventHandler(this.tbx_lineWidth_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(14, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 103;
            this.label5.Text = "轮廓线宽：";
            // 
            // ckb_displaySearchRegion
            // 
            this.ckb_displaySearchRegion.AutoSize = true;
            this.ckb_displaySearchRegion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_displaySearchRegion.Location = new System.Drawing.Point(18, 50);
            this.ckb_displaySearchRegion.Name = "ckb_displaySearchRegion";
            this.ckb_displaySearchRegion.Size = new System.Drawing.Size(99, 21);
            this.ckb_displaySearchRegion.TabIndex = 105;
            this.ckb_displaySearchRegion.Text = "显示搜索区域";
            this.ckb_displaySearchRegion.UseVisualStyleBackColor = true;
            this.ckb_displaySearchRegion.CheckedChanged += new System.EventHandler(this.ckb_displaySearchRegion_CheckedChanged);
            // 
            // ckb_displayCross
            // 
            this.ckb_displayCross.AutoSize = true;
            this.ckb_displayCross.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_displayCross.Location = new System.Drawing.Point(18, 76);
            this.ckb_displayCross.Name = "ckb_displayCross";
            this.ckb_displayCross.Size = new System.Drawing.Size(111, 21);
            this.ckb_displayCross.TabIndex = 104;
            this.ckb_displayCross.Text = "显示中心十字架";
            this.ckb_displayCross.UseVisualStyleBackColor = true;
            this.ckb_displayCross.CheckedChanged += new System.EventHandler(this.ckb_displayCross_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdo_resultRegionMarginMode);
            this.groupBox3.Controls.Add(this.rdo_resultRegionFillMode);
            this.groupBox3.Controls.Add(this.ckb_showResultRegion);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(18, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 86);
            this.groupBox3.TabIndex = 103;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "结果区域";
            // 
            // rdo_resultRegionMarginMode
            // 
            this.rdo_resultRegionMarginMode.AutoSize = true;
            this.rdo_resultRegionMarginMode.Location = new System.Drawing.Point(146, 52);
            this.rdo_resultRegionMarginMode.Name = "rdo_resultRegionMarginMode";
            this.rdo_resultRegionMarginMode.Size = new System.Drawing.Size(50, 21);
            this.rdo_resultRegionMarginMode.TabIndex = 102;
            this.rdo_resultRegionMarginMode.TabStop = true;
            this.rdo_resultRegionMarginMode.Text = "轮廓";
            this.rdo_resultRegionMarginMode.UseVisualStyleBackColor = true;
            // 
            // rdo_resultRegionFillMode
            // 
            this.rdo_resultRegionFillMode.AutoSize = true;
            this.rdo_resultRegionFillMode.Location = new System.Drawing.Point(40, 52);
            this.rdo_resultRegionFillMode.Name = "rdo_resultRegionFillMode";
            this.rdo_resultRegionFillMode.Size = new System.Drawing.Size(50, 21);
            this.rdo_resultRegionFillMode.TabIndex = 101;
            this.rdo_resultRegionFillMode.TabStop = true;
            this.rdo_resultRegionFillMode.Text = "填充";
            this.rdo_resultRegionFillMode.UseVisualStyleBackColor = true;
            this.rdo_resultRegionFillMode.CheckedChanged += new System.EventHandler(this.rdo_resultRegionFillMode_CheckedChanged);
            // 
            // ckb_showResultRegion
            // 
            this.ckb_showResultRegion.AutoSize = true;
            this.ckb_showResultRegion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_showResultRegion.Location = new System.Drawing.Point(40, 23);
            this.ckb_showResultRegion.Name = "ckb_showResultRegion";
            this.ckb_showResultRegion.Size = new System.Drawing.Size(99, 21);
            this.ckb_showResultRegion.TabIndex = 100;
            this.ckb_showResultRegion.Text = "显示结果区域";
            this.ckb_showResultRegion.UseVisualStyleBackColor = true;
            this.ckb_showResultRegion.CheckedChanged += new System.EventHandler(this.ckb_showResultRegion_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdo_outCircleMarginMode);
            this.groupBox2.Controls.Add(this.rdo_outCircleFillMode);
            this.groupBox2.Controls.Add(this.ckb_showOutCircle);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(18, 231);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 84);
            this.groupBox2.TabIndex = 100;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果区域外接圆";
            // 
            // rdo_outCircleMarginMode
            // 
            this.rdo_outCircleMarginMode.AutoSize = true;
            this.rdo_outCircleMarginMode.Location = new System.Drawing.Point(146, 51);
            this.rdo_outCircleMarginMode.Name = "rdo_outCircleMarginMode";
            this.rdo_outCircleMarginMode.Size = new System.Drawing.Size(50, 21);
            this.rdo_outCircleMarginMode.TabIndex = 102;
            this.rdo_outCircleMarginMode.TabStop = true;
            this.rdo_outCircleMarginMode.Text = "轮廓";
            this.rdo_outCircleMarginMode.UseVisualStyleBackColor = true;
            // 
            // rdo_outCircleFillMode
            // 
            this.rdo_outCircleFillMode.AutoSize = true;
            this.rdo_outCircleFillMode.Location = new System.Drawing.Point(40, 51);
            this.rdo_outCircleFillMode.Name = "rdo_outCircleFillMode";
            this.rdo_outCircleFillMode.Size = new System.Drawing.Size(50, 21);
            this.rdo_outCircleFillMode.TabIndex = 101;
            this.rdo_outCircleFillMode.TabStop = true;
            this.rdo_outCircleFillMode.Text = "填充";
            this.rdo_outCircleFillMode.UseVisualStyleBackColor = true;
            this.rdo_outCircleFillMode.CheckedChanged += new System.EventHandler(this.rdo_outCircleFillMode_CheckedChanged);
            // 
            // ckb_showOutCircle
            // 
            this.ckb_showOutCircle.AutoSize = true;
            this.ckb_showOutCircle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_showOutCircle.Location = new System.Drawing.Point(40, 25);
            this.ckb_showOutCircle.Name = "ckb_showOutCircle";
            this.ckb_showOutCircle.Size = new System.Drawing.Size(87, 21);
            this.ckb_showOutCircle.TabIndex = 100;
            this.ckb_showOutCircle.Text = "显示外接圆";
            this.ckb_showOutCircle.UseVisualStyleBackColor = true;
            this.ckb_showOutCircle.CheckedChanged += new System.EventHandler(this.ckb_showOutCircle_CheckedChanged);
            // 
            // ckb_fillHole
            // 
            this.ckb_fillHole.AutoSize = true;
            this.ckb_fillHole.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_fillHole.Location = new System.Drawing.Point(18, 23);
            this.ckb_fillHole.Name = "ckb_fillHole";
            this.ckb_fillHole.Size = new System.Drawing.Size(75, 21);
            this.ckb_fillHole.TabIndex = 11;
            this.ckb_fillHole.Text = "孔洞填充";
            this.ckb_fillHole.UseVisualStyleBackColor = true;
            this.ckb_fillHole.CheckedChanged += new System.EventHandler(this.ckb_fillHole_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgv_blobAnalyseResult);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(588, 346);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "结果";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgv_blobAnalyseResult
            // 
            this.dgv_blobAnalyseResult.AllowUserToAddRows = false;
            this.dgv_blobAnalyseResult.AllowUserToResizeRows = false;
            this.dgv_blobAnalyseResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_blobAnalyseResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column18,
            this.Column17,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgv_blobAnalyseResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_blobAnalyseResult.Location = new System.Drawing.Point(0, 0);
            this.dgv_blobAnalyseResult.Name = "dgv_blobAnalyseResult";
            this.dgv_blobAnalyseResult.ReadOnly = true;
            this.dgv_blobAnalyseResult.RowHeadersVisible = false;
            this.dgv_blobAnalyseResult.RowTemplate.Height = 23;
            this.dgv_blobAnalyseResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_blobAnalyseResult.Size = new System.Drawing.Size(588, 346);
            this.dgv_blobAnalyseResult.TabIndex = 93;
            this.dgv_blobAnalyseResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_blobAnalyseResult_CellClick);
            // 
            // Column18
            // 
            this.Column18.HeaderText = "编号";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Width = 68;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "面积";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 85;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "行";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 88;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "列";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 88;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "外圆半径";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 102;
            // 
            // ckb_blobAnalyseToolEnable
            // 
            this.ckb_blobAnalyseToolEnable.AutoSize = true;
            this.ckb_blobAnalyseToolEnable.BackColor = System.Drawing.Color.Transparent;
            this.ckb_blobAnalyseToolEnable.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_blobAnalyseToolEnable.Location = new System.Drawing.Point(528, 4);
            this.ckb_blobAnalyseToolEnable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ckb_blobAnalyseToolEnable.Name = "ckb_blobAnalyseToolEnable";
            this.ckb_blobAnalyseToolEnable.Size = new System.Drawing.Size(51, 21);
            this.ckb_blobAnalyseToolEnable.TabIndex = 69;
            this.ckb_blobAnalyseToolEnable.Text = "启用";
            this.ckb_blobAnalyseToolEnable.UseVisualStyleBackColor = false;
            this.ckb_blobAnalyseToolEnable.CheckedChanged += new System.EventHandler(this.ckb_blobAnalyseToolEnable_CheckedChanged);
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
            this.toolStrip1.Location = new System.Drawing.Point(-5, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(600, 25);
            this.toolStrip1.TabIndex = 96;
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
            this.tsb_help.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsb_help.ToolTipText = "帮助";
            this.tsb_help.Click += new System.EventHandler(this.tsb_help_Click);
            // 
            // Frm_BlobAnalyseTool
            // 
            this.AcceptButton = this.btn_runBlobAnalyseTool;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(594, 413);
            this.Controls.Add(this.ckb_blobAnalyseToolEnable);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximumSize = new System.Drawing.Size(610, 452);
            this.MinimumSize = new System.Drawing.Size(610, 452);
            this.Name = "Frm_BlobAnalyseTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "斑点分析";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_select)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_minThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_maxThreshold)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_processingItem)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_blobAnalyseResult)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox ckb_blobAnalyseToolEnable;
        private System.Windows.Forms.Button btn_SearchRegionDelete;
        private System.Windows.Forms.Label label124;
        internal System.Windows.Forms.CheckBox ckb_fillHole;
        private System.Windows.Forms.Label label77;
        internal System.Windows.Forms.NumericUpDown nud_minThreshold;
        private System.Windows.Forms.Label label78;
        internal System.Windows.Forms.NumericUpDown nud_maxThreshold;
        public System.Windows.Forms.Button btn_runBlobAnalyseTool;
        public System.Windows.Forms.ComboBox cbx_blobAnalyseSearchRegion;
        public System.Windows.Forms.Button btn_drawBlobAnalyseRegion;
        public System.Windows.Forms.DataGridView dgv_blobAnalyseResult;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.DataGridView dgv_select;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.CheckBox ckb_showResultRegion;
        internal System.Windows.Forms.CheckBox ckb_showOutCircle;
        public System.Windows.Forms.RadioButton rdo_resultRegionFillMode;
        public System.Windows.Forms.RadioButton rdo_outCircleMarginMode;
        public System.Windows.Forms.RadioButton rdo_outCircleFillMode;
        public System.Windows.Forms.RadioButton rdo_resultRegionMarginMode;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsm_deletePreprocessingItem;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TreeView tvw_preProcessingItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        internal System.Windows.Forms.CheckBox ckb_displayCross;
        internal System.Windows.Forms.CheckBox ckb_displaySearchRegion;
        public System.Windows.Forms.TextBox tbx_lineWidth;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DataGridView dgv_processingItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_runTool;
        private System.Windows.Forms.ToolStripButton tsb_resetTool;
        private System.Windows.Forms.ToolStripButton tsb_help;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btn_saveResultRegion;
    }
}