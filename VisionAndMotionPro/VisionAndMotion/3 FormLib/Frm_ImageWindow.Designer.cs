namespace VisionAndMotionPro
{
    partial class Frm_ImageWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ImageWindow));
            this.cbx_toolRunResultImageList = new System.Windows.Forms.ComboBox();
            this.cnt_rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.运行流程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像适应窗体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.区域填充方式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.填充ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.轮廓ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绘制ROIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.圆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.矩形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仿射矩形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.椭圆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任意形状ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标记点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除静态图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除原始图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全屏显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hwc_imageWindow = new ChoiceTech.Halcon.Control.HWindow_Final();
            this.cnt_rightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbx_toolRunResultImageList
            // 
            this.cbx_toolRunResultImageList.BackColor = System.Drawing.Color.LightGray;
            this.cbx_toolRunResultImageList.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbx_toolRunResultImageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_toolRunResultImageList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbx_toolRunResultImageList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_toolRunResultImageList.FormattingEnabled = true;
            this.cbx_toolRunResultImageList.Location = new System.Drawing.Point(0, 0);
            this.cbx_toolRunResultImageList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_toolRunResultImageList.Name = "cbx_toolRunResultImageList";
            this.cbx_toolRunResultImageList.Size = new System.Drawing.Size(632, 25);
            this.cbx_toolRunResultImageList.TabIndex = 65;
            this.cbx_toolRunResultImageList.TabStop = false;
            this.cbx_toolRunResultImageList.SelectedIndexChanged += new System.EventHandler(this.cbx_toolRunResultImageList_SelectedIndexChanged);
            this.cbx_toolRunResultImageList.SizeChanged += new System.EventHandler(this.cbx_toolRunResultImageList_SizeChanged);
            // 
            // cnt_rightClickMenu
            // 
            this.cnt_rightClickMenu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cnt_rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.运行流程ToolStripMenuItem,
            this.图像适应窗体ToolStripMenuItem,
            this.区域填充方式ToolStripMenuItem,
            this.绘制ROIToolStripMenuItem,
            this.打开图片ToolStripMenuItem,
            this.清除静态图像ToolStripMenuItem,
            this.清除原始图像ToolStripMenuItem,
            this.全屏显示ToolStripMenuItem,
            this.图像另存为ToolStripMenuItem});
            this.cnt_rightClickMenu.Name = "cnt_rightClickMenu";
            this.cnt_rightClickMenu.Size = new System.Drawing.Size(149, 202);
            // 
            // 运行流程ToolStripMenuItem
            // 
            this.运行流程ToolStripMenuItem.Name = "运行流程ToolStripMenuItem";
            this.运行流程ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.运行流程ToolStripMenuItem.Text = "运行流程";
            this.运行流程ToolStripMenuItem.Click += new System.EventHandler(this.运行流程ToolStripMenuItem_Click);
            // 
            // 图像适应窗体ToolStripMenuItem
            // 
            this.图像适应窗体ToolStripMenuItem.Name = "图像适应窗体ToolStripMenuItem";
            this.图像适应窗体ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.图像适应窗体ToolStripMenuItem.Text = "图像适应窗体";
            this.图像适应窗体ToolStripMenuItem.Click += new System.EventHandler(this.图像适应窗体ToolStripMenuItem_Click);
            // 
            // 区域填充方式ToolStripMenuItem
            // 
            this.区域填充方式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.填充ToolStripMenuItem,
            this.轮廓ToolStripMenuItem});
            this.区域填充方式ToolStripMenuItem.Name = "区域填充方式ToolStripMenuItem";
            this.区域填充方式ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.区域填充方式ToolStripMenuItem.Text = "填充模式";
            // 
            // 填充ToolStripMenuItem
            // 
            this.填充ToolStripMenuItem.Name = "填充ToolStripMenuItem";
            this.填充ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.填充ToolStripMenuItem.Text = "填充";
            this.填充ToolStripMenuItem.Click += new System.EventHandler(this.填充ToolStripMenuItem_Click);
            // 
            // 轮廓ToolStripMenuItem
            // 
            this.轮廓ToolStripMenuItem.Name = "轮廓ToolStripMenuItem";
            this.轮廓ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.轮廓ToolStripMenuItem.Text = "轮廓";
            this.轮廓ToolStripMenuItem.Click += new System.EventHandler(this.轮廓ToolStripMenuItem_Click);
            // 
            // 绘制ROIToolStripMenuItem
            // 
            this.绘制ROIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.圆ToolStripMenuItem,
            this.矩形ToolStripMenuItem,
            this.仿射矩形ToolStripMenuItem,
            this.椭圆ToolStripMenuItem,
            this.任意形状ToolStripMenuItem,
            this.线ToolStripMenuItem,
            this.标记点ToolStripMenuItem});
            this.绘制ROIToolStripMenuItem.Name = "绘制ROIToolStripMenuItem";
            this.绘制ROIToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.绘制ROIToolStripMenuItem.Text = "绘制ROI";
            // 
            // 圆ToolStripMenuItem
            // 
            this.圆ToolStripMenuItem.Name = "圆ToolStripMenuItem";
            this.圆ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.圆ToolStripMenuItem.Text = "圆";
            this.圆ToolStripMenuItem.Click += new System.EventHandler(this.圆ToolStripMenuItem_Click);
            // 
            // 矩形ToolStripMenuItem
            // 
            this.矩形ToolStripMenuItem.Name = "矩形ToolStripMenuItem";
            this.矩形ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.矩形ToolStripMenuItem.Text = "矩形";
            this.矩形ToolStripMenuItem.Click += new System.EventHandler(this.矩形ToolStripMenuItem_Click);
            // 
            // 仿射矩形ToolStripMenuItem
            // 
            this.仿射矩形ToolStripMenuItem.Name = "仿射矩形ToolStripMenuItem";
            this.仿射矩形ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.仿射矩形ToolStripMenuItem.Text = "仿射矩形";
            this.仿射矩形ToolStripMenuItem.Click += new System.EventHandler(this.放射矩形ToolStripMenuItem_Click);
            // 
            // 椭圆ToolStripMenuItem
            // 
            this.椭圆ToolStripMenuItem.Name = "椭圆ToolStripMenuItem";
            this.椭圆ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.椭圆ToolStripMenuItem.Text = "椭圆";
            this.椭圆ToolStripMenuItem.Click += new System.EventHandler(this.椭圆ToolStripMenuItem_Click);
            // 
            // 任意形状ToolStripMenuItem
            // 
            this.任意形状ToolStripMenuItem.Name = "任意形状ToolStripMenuItem";
            this.任意形状ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.任意形状ToolStripMenuItem.Text = "任意形状";
            this.任意形状ToolStripMenuItem.Click += new System.EventHandler(this.任意形状ToolStripMenuItem_Click);
            // 
            // 线ToolStripMenuItem
            // 
            this.线ToolStripMenuItem.Name = "线ToolStripMenuItem";
            this.线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.线ToolStripMenuItem.Text = "线";
            this.线ToolStripMenuItem.Click += new System.EventHandler(this.线ToolStripMenuItem_Click);
            // 
            // 标记点ToolStripMenuItem
            // 
            this.标记点ToolStripMenuItem.Name = "标记点ToolStripMenuItem";
            this.标记点ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.标记点ToolStripMenuItem.Text = "标记点";
            this.标记点ToolStripMenuItem.Click += new System.EventHandler(this.标记点ToolStripMenuItem_Click);
            // 
            // 打开图片ToolStripMenuItem
            // 
            this.打开图片ToolStripMenuItem.Name = "打开图片ToolStripMenuItem";
            this.打开图片ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.打开图片ToolStripMenuItem.Text = "打开图像";
            this.打开图片ToolStripMenuItem.Click += new System.EventHandler(this.打开图片ToolStripMenuItem_Click);
            // 
            // 清除静态图像ToolStripMenuItem
            // 
            this.清除静态图像ToolStripMenuItem.Name = "清除静态图像ToolStripMenuItem";
            this.清除静态图像ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清除静态图像ToolStripMenuItem.Text = "清除静态图像";
            this.清除静态图像ToolStripMenuItem.Click += new System.EventHandler(this.清除静态图像ToolStripMenuItem_Click);
            // 
            // 清除原始图像ToolStripMenuItem
            // 
            this.清除原始图像ToolStripMenuItem.Name = "清除原始图像ToolStripMenuItem";
            this.清除原始图像ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清除原始图像ToolStripMenuItem.Text = "清除原始图像";
            this.清除原始图像ToolStripMenuItem.Click += new System.EventHandler(this.清除原始图像ToolStripMenuItem_Click);
            // 
            // 全屏显示ToolStripMenuItem
            // 
            this.全屏显示ToolStripMenuItem.Name = "全屏显示ToolStripMenuItem";
            this.全屏显示ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.全屏显示ToolStripMenuItem.Text = "全屏显示";
            this.全屏显示ToolStripMenuItem.Click += new System.EventHandler(this.fullScreenDisplayToolStripMenuItem_Click);
            // 
            // 图像另存为ToolStripMenuItem
            // 
            this.图像另存为ToolStripMenuItem.Name = "图像另存为ToolStripMenuItem";
            this.图像另存为ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.图像另存为ToolStripMenuItem.Text = "图像另存为";
            this.图像另存为ToolStripMenuItem.Click += new System.EventHandler(this.图像另存为ToolStripMenuItem_Click);
            // 
            // hwc_imageWindow
            // 
            this.hwc_imageWindow.BackColor = System.Drawing.Color.Transparent;
            this.hwc_imageWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hwc_imageWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hwc_imageWindow.DrawModel = false;
            this.hwc_imageWindow.EditModel = true;
            this.hwc_imageWindow.Image = null;
            this.hwc_imageWindow.Location = new System.Drawing.Point(0, 25);
            this.hwc_imageWindow.Name = "hwc_imageWindow";
            this.hwc_imageWindow.Size = new System.Drawing.Size(632, 459);
            this.hwc_imageWindow.TabIndex = 70;
            // 
            // Frm_ImageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 484);
            this.Controls.Add(this.hwc_imageWindow);
            this.Controls.Add(this.cbx_toolRunResultImageList);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_ImageWindow";
            this.ShowIcon = false;
            this.Text = "图像";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_ImageWindow_FormClosed);
            this.Load += new System.EventHandler(this.Frm_ImageWindow_Load);
            this.cnt_rightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cbx_toolRunResultImageList;
        internal System.Windows.Forms.ContextMenuStrip cnt_rightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem 运行流程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像适应窗体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 区域填充方式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 填充ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 轮廓ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绘制ROIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 圆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 矩形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 仿射矩形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 椭圆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 任意形状ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标记点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除静态图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除原始图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全屏显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像另存为ToolStripMenuItem;
        public ChoiceTech.Halcon.Control.HWindow_Final hwc_imageWindow;




    }
}