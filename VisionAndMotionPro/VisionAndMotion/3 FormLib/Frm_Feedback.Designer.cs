namespace VisionAndMotionPro
{
    partial class Frm_Feedback
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Feedback));
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_feedBackMessage = new System.Windows.Forms.TextBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_emailAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "      感谢您对此软件的建议与反馈，我们将认真汲取并改进，从而更好的服务用户，请在下面的文本框内填写您的反馈信息后提交。";
            // 
            // tbx_feedBackMessage
            // 
            this.tbx_feedBackMessage.BackColor = System.Drawing.Color.White;
            this.tbx_feedBackMessage.Location = new System.Drawing.Point(12, 56);
            this.tbx_feedBackMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_feedBackMessage.Multiline = true;
            this.tbx_feedBackMessage.Name = "tbx_feedBackMessage";
            this.tbx_feedBackMessage.Size = new System.Drawing.Size(372, 81);
            this.tbx_feedBackMessage.TabIndex = 1;
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(291, 197);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(93, 31);
            this.btn_submit.TabIndex = 2;
            this.btn_submit.Text = "提交";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "请输入您的邮箱地址，便于我们回复（可不填）：";
            // 
            // tbx_emailAddress
            // 
            this.tbx_emailAddress.BackColor = System.Drawing.Color.White;
            this.tbx_emailAddress.Location = new System.Drawing.Point(12, 167);
            this.tbx_emailAddress.Name = "tbx_emailAddress";
            this.tbx_emailAddress.Size = new System.Drawing.Size(372, 23);
            this.tbx_emailAddress.TabIndex = 4;
            // 
            // Frm_Feedback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(398, 235);
            this.Controls.Add(this.tbx_emailAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.tbx_feedBackMessage);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(414, 274);
            this.MinimumSize = new System.Drawing.Size(414, 274);
            this.Name = "Frm_Feedback";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "建议和反馈";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_feedBackMessage;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_emailAddress;
    }
}