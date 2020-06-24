namespace VisionAndMotionPro
{
    partial class Frm_Setting
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("常规");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("启动");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("项目");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("环境");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("用户管理");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("安全", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("运行");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Setting));
            this.tvw_setting = new System.Windows.Forms.TreeView();
            this.pnl_window = new System.Windows.Forms.Panel();
            this.btn_saveSetting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvw_setting
            // 
            this.tvw_setting.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvw_setting.Indent = 20;
            this.tvw_setting.ItemHeight = 20;
            this.tvw_setting.Location = new System.Drawing.Point(10, 9);
            this.tvw_setting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvw_setting.Name = "tvw_setting";
            treeNode1.Name = "节点0";
            treeNode1.Text = "常规";
            treeNode2.Name = "节点4";
            treeNode2.Text = "启动";
            treeNode3.Name = "节点0";
            treeNode3.Text = "项目";
            treeNode4.Name = "节点1";
            treeNode4.Text = "环境";
            treeNode5.Name = "节点1";
            treeNode5.Text = "用户管理";
            treeNode6.Name = "节点0";
            treeNode6.Text = "安全";
            treeNode7.Name = "节点0";
            treeNode7.Text = "运行";
            this.tvw_setting.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode6,
            treeNode7});
            this.tvw_setting.Size = new System.Drawing.Size(168, 475);
            this.tvw_setting.TabIndex = 19;
            this.tvw_setting.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvw_setting_AfterSelect);
            // 
            // pnl_window
            // 
            this.pnl_window.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_window.Location = new System.Drawing.Point(184, 9);
            this.pnl_window.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnl_window.Name = "pnl_window";
            this.pnl_window.Size = new System.Drawing.Size(520, 427);
            this.pnl_window.TabIndex = 20;
            // 
            // btn_saveSetting
            // 
            this.btn_saveSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_saveSetting.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_saveSetting.Location = new System.Drawing.Point(604, 444);
            this.btn_saveSetting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_saveSetting.Name = "btn_saveSetting";
            this.btn_saveSetting.Size = new System.Drawing.Size(100, 40);
            this.btn_saveSetting.TabIndex = 5;
            this.btn_saveSetting.Text = "保存";
            this.btn_saveSetting.UseVisualStyleBackColor = true;
            this.btn_saveSetting.Click += new System.EventHandler(this.btn_saveSetting_Click);
            // 
            // Frm_Setting
            // 
            this.AcceptButton = this.btn_saveSetting;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(716, 494);
            this.Controls.Add(this.btn_saveSetting);
            this.Controls.Add(this.pnl_window);
            this.Controls.Add(this.tvw_setting);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(732, 533);
            this.MinimumSize = new System.Drawing.Size(732, 533);
            this.Name = "Frm_Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.Frm_Setting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TreeView tvw_setting;
        private System.Windows.Forms.Panel pnl_window;
        internal System.Windows.Forms.Button btn_saveSetting;


    }
}