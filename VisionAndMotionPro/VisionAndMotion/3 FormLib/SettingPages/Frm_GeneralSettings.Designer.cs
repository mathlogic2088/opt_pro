namespace VisionAndMotionPro
{
    partial class Frm_GeneralSettings
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_lanuage = new System.Windows.Forms.ComboBox();
            this.tbx_companyName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "语言：";
            // 
            // cbo_lanuage
            // 
            this.cbo_lanuage.BackColor = System.Drawing.Color.LightGray;
            this.cbo_lanuage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_lanuage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_lanuage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_lanuage.FormattingEnabled = true;
            this.cbo_lanuage.Items.AddRange(new object[] {
            "简体中文",
            "英文"});
            this.cbo_lanuage.Location = new System.Drawing.Point(78, 8);
            this.cbo_lanuage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbo_lanuage.Name = "cbo_lanuage";
            this.cbo_lanuage.Size = new System.Drawing.Size(165, 25);
            this.cbo_lanuage.TabIndex = 8;
            // 
            // tbx_companyName
            // 
            this.tbx_companyName.Location = new System.Drawing.Point(78, 37);
            this.tbx_companyName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_companyName.Name = "tbx_companyName";
            this.tbx_companyName.Size = new System.Drawing.Size(320, 23);
            this.tbx_companyName.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "公司名称：";
            // 
            // Frm_GeneralSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 318);
            this.Controls.Add(this.tbx_companyName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbo_lanuage);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_GeneralSettings";
            this.Text = "Frm_GeneralSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox cbo_lanuage;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox tbx_companyName;
        internal System.Windows.Forms.Label label3;
    }
}