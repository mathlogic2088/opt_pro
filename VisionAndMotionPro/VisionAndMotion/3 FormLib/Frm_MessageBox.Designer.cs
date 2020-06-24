namespace VisionAndMotionPro
{
    partial class Frm_MessageBox
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
            this.btn_confim = new System.Windows.Forms.Button();
            this.lbl_title = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_confim
            // 
            this.btn_confim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confim.Location = new System.Drawing.Point(316, 117);
            this.btn_confim.Name = "btn_confim";
            this.btn_confim.Size = new System.Drawing.Size(69, 26);
            this.btn_confim.TabIndex = 0;
            this.btn_confim.Text = "确定";
            this.btn_confim.UseVisualStyleBackColor = true;
            this.btn_confim.Click += new System.EventHandler(this.btn_confim_Click);
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_title.Location = new System.Drawing.Point(6, 3);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(58, 22);
            this.lbl_title.TabIndex = 1;
            this.lbl_title.Text = "提示：";
            // 
            // lbl_info
            // 
            this.lbl_info.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_info.Location = new System.Drawing.Point(18, 32);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(356, 82);
            this.lbl_info.TabIndex = 2;
            this.lbl_info.Text = "lal_info";
            // 
            // Frm_MessageBox
            // 
            this.AcceptButton = this.btn_confim;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(393, 148);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.btn_confim);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_MessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_MessageBox";
            this.Load += new System.EventHandler(this.Frm_MessageBox_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.setForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.setForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.setForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbl_info;
        internal System.Windows.Forms.Button btn_confim;
        internal System.Windows.Forms.Label lbl_title;
    }
}