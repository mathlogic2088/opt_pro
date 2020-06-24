namespace VisionAndMotionPro
{
    partial class Frm_LayoutManage
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_createNewLayout = new System.Windows.Forms.Button();
            this.btn_deleteLayout = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_layoutList = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "自定义布局列表";
            // 
            // btn_createNewLayout
            // 
            this.btn_createNewLayout.Location = new System.Drawing.Point(327, 31);
            this.btn_createNewLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_createNewLayout.Name = "btn_createNewLayout";
            this.btn_createNewLayout.Size = new System.Drawing.Size(120, 40);
            this.btn_createNewLayout.TabIndex = 2;
            this.btn_createNewLayout.Text = "保存当前布局";
            this.btn_createNewLayout.UseVisualStyleBackColor = true;
            this.btn_createNewLayout.Click += new System.EventHandler(this.btn_createNewLayout_Click);
            // 
            // btn_deleteLayout
            // 
            this.btn_deleteLayout.Location = new System.Drawing.Point(327, 80);
            this.btn_deleteLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_deleteLayout.Name = "btn_deleteLayout";
            this.btn_deleteLayout.Size = new System.Drawing.Size(120, 40);
            this.btn_deleteLayout.TabIndex = 3;
            this.btn_deleteLayout.Text = "删除选中布局";
            this.btn_deleteLayout.UseVisualStyleBackColor = true;
            this.btn_deleteLayout.Click += new System.EventHandler(this.btn_deleteLayout_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(17, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(304, 228);
            this.dataGridView1.TabIndex = 4;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "名称";
            this.Column2.Name = "Column2";
            this.Column2.Width = 280;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "选择布局：";
            // 
            // cbx_layoutList
            // 
            this.cbx_layoutList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_layoutList.FormattingEnabled = true;
            this.cbx_layoutList.Location = new System.Drawing.Point(76, 277);
            this.cbx_layoutList.Name = "cbx_layoutList";
            this.cbx_layoutList.Size = new System.Drawing.Size(245, 25);
            this.cbx_layoutList.TabIndex = 6;
            this.cbx_layoutList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Frm_LayoutManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 312);
            this.Controls.Add(this.cbx_layoutList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_deleteLayout);
            this.Controls.Add(this.btn_createNewLayout);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(476, 351);
            this.MinimumSize = new System.Drawing.Size(476, 351);
            this.Name = "Frm_LayoutManage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义布局";
            this.Load += new System.EventHandler(this.Frm_LayoutManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_createNewLayout;
        private System.Windows.Forms.Button btn_deleteLayout;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_layoutList;
    }
}