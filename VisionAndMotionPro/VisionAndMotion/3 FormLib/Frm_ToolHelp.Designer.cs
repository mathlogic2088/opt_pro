namespace VisionAndMotionPro
{
    partial class Frm_ToolHelp
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
            this.tbx_toolType = new System.Windows.Forms.TextBox();
            this.tbx_toolFunc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_useStep = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_attentionItem = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "工具类型：";
            // 
            // tbx_toolType
            // 
            this.tbx_toolType.Location = new System.Drawing.Point(74, 18);
            this.tbx_toolType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_toolType.Name = "tbx_toolType";
            this.tbx_toolType.ReadOnly = true;
            this.tbx_toolType.Size = new System.Drawing.Size(491, 23);
            this.tbx_toolType.TabIndex = 1;
            // 
            // tbx_toolFunc
            // 
            this.tbx_toolFunc.Location = new System.Drawing.Point(74, 56);
            this.tbx_toolFunc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_toolFunc.Multiline = true;
            this.tbx_toolFunc.Name = "tbx_toolFunc";
            this.tbx_toolFunc.ReadOnly = true;
            this.tbx_toolFunc.Size = new System.Drawing.Size(491, 59);
            this.tbx_toolFunc.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "工具用途：";
            // 
            // tbx_useStep
            // 
            this.tbx_useStep.Location = new System.Drawing.Point(74, 130);
            this.tbx_useStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_useStep.Multiline = true;
            this.tbx_useStep.Name = "tbx_useStep";
            this.tbx_useStep.ReadOnly = true;
            this.tbx_useStep.Size = new System.Drawing.Size(491, 126);
            this.tbx_useStep.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "使用步骤：";
            // 
            // tbx_attentionItem
            // 
            this.tbx_attentionItem.Location = new System.Drawing.Point(74, 272);
            this.tbx_attentionItem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_attentionItem.Multiline = true;
            this.tbx_attentionItem.Name = "tbx_attentionItem";
            this.tbx_attentionItem.ReadOnly = true;
            this.tbx_attentionItem.Size = new System.Drawing.Size(491, 59);
            this.tbx_attentionItem.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "注意事项：";
            // 
            // Frm_ToolHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 339);
            this.Controls.Add(this.tbx_attentionItem);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbx_useStep);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbx_toolFunc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbx_toolType);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(593, 378);
            this.MinimumSize = new System.Drawing.Size(593, 378);
            this.Name = "Frm_ToolHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工具帮助";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_toolType;
        private System.Windows.Forms.TextBox tbx_toolFunc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_useStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_attentionItem;
        private System.Windows.Forms.Label label4;
    }
}