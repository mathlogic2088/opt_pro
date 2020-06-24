namespace VisionAndMotionPro
{
    partial class Frm_communicateConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_communicateConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_communcationType = new System.Windows.Forms.ComboBox();
            this.dgv_communicationConfig = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_deleteItem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_communicationConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "通讯方式：";
            // 
            // cbx_communcationType
            // 
            this.cbx_communcationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_communcationType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbx_communcationType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_communcationType.FormattingEnabled = true;
            this.cbx_communcationType.Items.AddRange(new object[] {
            "无",
            "以太网客户端",
            "以太网服务端",
            "串口",
            "IO信号"});
            this.cbx_communcationType.Location = new System.Drawing.Point(81, 11);
            this.cbx_communcationType.Margin = new System.Windows.Forms.Padding(2);
            this.cbx_communcationType.Name = "cbx_communcationType";
            this.cbx_communcationType.Size = new System.Drawing.Size(155, 25);
            this.cbx_communcationType.TabIndex = 1;
            // 
            // dgv_communicationConfig
            // 
            this.dgv_communicationConfig.AllowUserToDeleteRows = false;
            this.dgv_communicationConfig.AllowUserToResizeRows = false;
            this.dgv_communicationConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_communicationConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column7,
            this.Column5,
            this.Column6});
            this.dgv_communicationConfig.Location = new System.Drawing.Point(9, 47);
            this.dgv_communicationConfig.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_communicationConfig.Name = "dgv_communicationConfig";
            this.dgv_communicationConfig.RowHeadersVisible = false;
            this.dgv_communicationConfig.RowTemplate.Height = 23;
            this.dgv_communicationConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_communicationConfig.Size = new System.Drawing.Size(733, 443);
            this.dgv_communicationConfig.TabIndex = 13;
            this.dgv_communicationConfig.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_communicationConfig_EditingControlShowing);
            this.dgv_communicationConfig.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_communicationConfig_RowsAdded);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "接收指令";
            this.Column2.Name = "Column2";
            // 
            // Column7
            // 
            this.Column7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column7.HeaderText = "流程";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column7.Width = 150;
            // 
            // Column5
            // 
            this.Column5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column5.HeaderText = "返回项";
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column5.Width = 300;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "前缀";
            this.Column6.Name = "Column6";
            this.Column6.Width = 120;
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Location = new System.Drawing.Point(564, 8);
            this.btn_save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(87, 28);
            this.btn_save.TabIndex = 15;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_deleteItem
            // 
            this.btn_deleteItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_deleteItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_deleteItem.Location = new System.Drawing.Point(655, 8);
            this.btn_deleteItem.Margin = new System.Windows.Forms.Padding(2);
            this.btn_deleteItem.Name = "btn_deleteItem";
            this.btn_deleteItem.Size = new System.Drawing.Size(87, 28);
            this.btn_deleteItem.TabIndex = 17;
            this.btn_deleteItem.Text = "删除项";
            this.btn_deleteItem.UseVisualStyleBackColor = true;
            this.btn_deleteItem.Click += new System.EventHandler(this.btn_deleteItem_Click);
            // 
            // Frm_communicateConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(751, 498);
            this.Controls.Add(this.btn_deleteItem);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.dgv_communicationConfig);
            this.Controls.Add(this.cbx_communcationType);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(767, 537);
            this.MinimumSize = new System.Drawing.Size(767, 537);
            this.Name = "Frm_communicateConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通讯配置";
            this.Load += new System.EventHandler(this.Frm_communicateConfig_Load);
            this.Shown += new System.EventHandler(this.Frm_communicateConfig_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_communicationConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView dgv_communicationConfig;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox cbx_communcationType;
        internal System.Windows.Forms.Button btn_save;
        internal System.Windows.Forms.Button btn_deleteItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column7;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}