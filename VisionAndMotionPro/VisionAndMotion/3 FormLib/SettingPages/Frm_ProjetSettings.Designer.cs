namespace VisionAndMotionPro
{
    partial class Frm_ProjetSettings
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
            this.cbx_cardType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_programTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckb_vitualCard = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbx_cardType
            // 
            this.cbx_cardType.BackColor = System.Drawing.Color.LightGray;
            this.cbx_cardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_cardType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbx_cardType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_cardType.FormattingEnabled = true;
            this.cbx_cardType.Items.AddRange(new object[] {
            "无",
            "固高_GTS",
            "雷塞_DMC2210",
            "雷塞_DMC2410",
            "雷赛_IOC0640"});
            this.cbx_cardType.Location = new System.Drawing.Point(78, 39);
            this.cbx_cardType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_cardType.Name = "cbx_cardType";
            this.cbx_cardType.Size = new System.Drawing.Size(234, 25);
            this.cbx_cardType.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(15, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "板卡型号：";
            // 
            // tbx_programTitle
            // 
            this.tbx_programTitle.Location = new System.Drawing.Point(78, 13);
            this.tbx_programTitle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_programTitle.Name = "tbx_programTitle";
            this.tbx_programTitle.Size = new System.Drawing.Size(325, 23);
            this.tbx_programTitle.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "项目名称：";
            // 
            // ckb_vitualCard
            // 
            this.ckb_vitualCard.AutoSize = true;
            this.ckb_vitualCard.Location = new System.Drawing.Point(341, 43);
            this.ckb_vitualCard.Name = "ckb_vitualCard";
            this.ckb_vitualCard.Size = new System.Drawing.Size(63, 21);
            this.ckb_vitualCard.TabIndex = 9;
            this.ckb_vitualCard.Text = "虚拟卡";
            this.ckb_vitualCard.UseVisualStyleBackColor = true;
            // 
            // Frm_ProjetSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 318);
            this.Controls.Add(this.ckb_vitualCard);
            this.Controls.Add(this.tbx_programTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbx_cardType);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_ProjetSettings";
            this.Text = "Frm_ProjetSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cbx_cardType;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox tbx_programTitle;
        internal System.Windows.Forms.Label label3;
        public System.Windows.Forms.CheckBox ckb_vitualCard;
    }
}