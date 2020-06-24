namespace VisionAndMotionPro
{
    partial class Frm_AlarmWindow
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lal_tipContent = new System.Windows.Forms.Label();
            this.btn_checkAgain = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ignore = new System.Windows.Forms.Button();
            this.btn_confirmNG = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lal_tipContent);
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(47, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 212);
            this.panel1.TabIndex = 0;
            // 
            // lal_tipContent
            // 
            this.lal_tipContent.AutoSize = true;
            this.lal_tipContent.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lal_tipContent.Location = new System.Drawing.Point(16, 14);
            this.lal_tipContent.Name = "lal_tipContent";
            this.lal_tipContent.Size = new System.Drawing.Size(0, 16);
            this.lal_tipContent.TabIndex = 0;
            // 
            // btn_checkAgain
            // 
            this.btn_checkAgain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_checkAgain.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_checkAgain.Location = new System.Drawing.Point(275, 281);
            this.btn_checkAgain.Name = "btn_checkAgain";
            this.btn_checkAgain.Size = new System.Drawing.Size(104, 29);
            this.btn_checkAgain.TabIndex = 1;
            this.btn_checkAgain.Text = "重新检查";
            this.btn_checkAgain.UseVisualStyleBackColor = true;
            this.btn_checkAgain.Click += new System.EventHandler(this.btn_checkAgain_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(43, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "报警中......";
            // 
            // btn_ignore
            // 
            this.btn_ignore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ignore.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ignore.Location = new System.Drawing.Point(47, 281);
            this.btn_ignore.Name = "btn_ignore";
            this.btn_ignore.Size = new System.Drawing.Size(104, 29);
            this.btn_ignore.TabIndex = 3;
            this.btn_ignore.Text = "忽略";
            this.btn_ignore.UseVisualStyleBackColor = true;
            this.btn_ignore.Click += new System.EventHandler(this.btn_ignore_Click);
            // 
            // btn_confirmNG
            // 
            this.btn_confirmNG.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_confirmNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirmNG.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_confirmNG.Location = new System.Drawing.Point(504, 281);
            this.btn_confirmNG.Name = "btn_confirmNG";
            this.btn_confirmNG.Size = new System.Drawing.Size(104, 29);
            this.btn_confirmNG.TabIndex = 4;
            this.btn_confirmNG.Text = "确认NG";
            this.btn_confirmNG.UseVisualStyleBackColor = true;
            this.btn_confirmNG.Click += new System.EventHandler(this.btn_confirmNG_Click);
            // 
            // Frm_AlarmWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.CancelButton = this.btn_confirmNG;
            this.ClientSize = new System.Drawing.Size(655, 322);
            this.Controls.Add(this.btn_confirmNG);
            this.Controls.Add(this.btn_ignore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_checkAgain);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_AlarmWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_AlarmWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_AlarmWindow_FormClosing);
            this.Load += new System.EventHandler(this.Frm_AlarmWindow_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.setForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.setForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.setForm_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lal_tipContent;
        private System.Windows.Forms.Button btn_checkAgain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ignore;
        private System.Windows.Forms.Button btn_confirmNG;
    }
}