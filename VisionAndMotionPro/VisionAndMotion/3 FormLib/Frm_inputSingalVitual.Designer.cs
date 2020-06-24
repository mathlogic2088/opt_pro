namespace VisionAndMotionPro
{
    partial class Frm_inputSingalVitual
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
            this.btn_startVitual = new System.Windows.Forms.Button();
            this.cbx_inputSignal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_vitualType = new System.Windows.Forms.ComboBox();
            this.lbl_vitualTime = new System.Windows.Forms.Label();
            this.tbx_vitualTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_startVitual
            // 
            this.btn_startVitual.Location = new System.Drawing.Point(246, 31);
            this.btn_startVitual.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_startVitual.Name = "btn_startVitual";
            this.btn_startVitual.Size = new System.Drawing.Size(107, 44);
            this.btn_startVitual.TabIndex = 0;
            this.btn_startVitual.Text = "开始虚拟";
            this.btn_startVitual.UseVisualStyleBackColor = true;
            this.btn_startVitual.Click += new System.EventHandler(this.btn_startVitual_Click);
            // 
            // cbx_inputSignal
            // 
            this.cbx_inputSignal.FormattingEnabled = true;
            this.cbx_inputSignal.Location = new System.Drawing.Point(15, 31);
            this.cbx_inputSignal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_inputSignal.Name = "cbx_inputSignal";
            this.cbx_inputSignal.Size = new System.Drawing.Size(210, 25);
            this.cbx_inputSignal.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "待虚拟信号选择";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "虚拟方式";
            // 
            // cbx_vitualType
            // 
            this.cbx_vitualType.FormattingEnabled = true;
            this.cbx_vitualType.Items.AddRange(new object[] {
            "虚拟为低电平",
            "虚拟为高电平",
            "虚拟为高电平，指定时长",
            "虚拟为低电平，指定时长",
            "虚拟置反"});
            this.cbx_vitualType.Location = new System.Drawing.Point(15, 90);
            this.cbx_vitualType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_vitualType.Name = "cbx_vitualType";
            this.cbx_vitualType.Size = new System.Drawing.Size(210, 25);
            this.cbx_vitualType.TabIndex = 3;
            this.cbx_vitualType.SelectedIndexChanged += new System.EventHandler(this.cbx_vitualType_SelectedIndexChanged);
            // 
            // lbl_vitualTime
            // 
            this.lbl_vitualTime.AutoSize = true;
            this.lbl_vitualTime.Location = new System.Drawing.Point(15, 129);
            this.lbl_vitualTime.Name = "lbl_vitualTime";
            this.lbl_vitualTime.Size = new System.Drawing.Size(81, 17);
            this.lbl_vitualTime.TabIndex = 6;
            this.lbl_vitualTime.Text = "虚拟时长(ms)";
            // 
            // tbx_vitualTime
            // 
            this.tbx_vitualTime.Location = new System.Drawing.Point(15, 149);
            this.tbx_vitualTime.Name = "tbx_vitualTime";
            this.tbx_vitualTime.Size = new System.Drawing.Size(210, 23);
            this.tbx_vitualTime.TabIndex = 7;
            this.tbx_vitualTime.Text = "100";
            // 
            // Frm_inputSingalVitual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 190);
            this.Controls.Add(this.tbx_vitualTime);
            this.Controls.Add(this.lbl_vitualTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbx_vitualType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbx_inputSignal);
            this.Controls.Add(this.btn_startVitual);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(381, 229);
            this.MinimumSize = new System.Drawing.Size(381, 229);
            this.Name = "Frm_inputSingalVitual";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入信号虚拟";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_inputSingalVitual_FormClosing);
            this.Load += new System.EventHandler(this.Frm_inputSingalVitual_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_startVitual;
        private System.Windows.Forms.ComboBox cbx_inputSignal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_vitualType;
        private System.Windows.Forms.Label lbl_vitualTime;
        private System.Windows.Forms.TextBox tbx_vitualTime;
    }
}