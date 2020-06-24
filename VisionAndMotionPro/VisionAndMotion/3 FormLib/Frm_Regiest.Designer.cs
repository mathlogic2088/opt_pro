namespace VisionAndMotionPro
{
    partial class Frm_Regiest
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
            this.txt_machineCode = new System.Windows.Forms.TextBox();
            this.lnl_author = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_regiest = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_regiestCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_machineCode
            // 
            this.txt_machineCode.Location = new System.Drawing.Point(80, 57);
            this.txt_machineCode.Name = "txt_machineCode";
            this.txt_machineCode.ReadOnly = true;
            this.txt_machineCode.Size = new System.Drawing.Size(194, 21);
            this.txt_machineCode.TabIndex = 11;
            // 
            // lnl_author
            // 
            this.lnl_author.AutoSize = true;
            this.lnl_author.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lnl_author.Location = new System.Drawing.Point(9, 151);
            this.lnl_author.Name = "lnl_author";
            this.lnl_author.Size = new System.Drawing.Size(45, 10);
            this.lnl_author.TabIndex = 19;
            this.lnl_author.TabStop = true;
            this.lnl_author.Text = "我是作者";
            this.lnl_author.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnl_author_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(5, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(317, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "使用前请联系作者，并使用下面的机器码与作者兑换注册码";
            // 
            // btn_regiest
            // 
            this.btn_regiest.Location = new System.Drawing.Point(201, 110);
            this.btn_regiest.Name = "btn_regiest";
            this.btn_regiest.Size = new System.Drawing.Size(73, 28);
            this.btn_regiest.TabIndex = 15;
            this.btn_regiest.Text = "注册";
            this.btn_regiest.UseVisualStyleBackColor = true;
            this.btn_regiest.Click += new System.EventHandler(this.btn_regiest_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(31, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "机器码：";
            // 
            // txt_regiestCode
            // 
            this.txt_regiestCode.Location = new System.Drawing.Point(80, 81);
            this.txt_regiestCode.Name = "txt_regiestCode";
            this.txt_regiestCode.Size = new System.Drawing.Size(194, 21);
            this.txt_regiestCode.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(31, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "注册码：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 28);
            this.button1.TabIndex = 20;
            this.button1.Text = "退出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Frm_Regiest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.ClientSize = new System.Drawing.Size(327, 170);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_machineCode);
            this.Controls.Add(this.lnl_author);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_regiest);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_regiestCode);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Regiest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "327, 185";
            this.Load += new System.EventHandler(this.Frm_Regiest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_machineCode;
        private System.Windows.Forms.LinkLabel lnl_author;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_regiest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_regiestCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
    }
}