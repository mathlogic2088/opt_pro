namespace VisionAndMotionPro
{
    partial class Frm_TCPClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TCPClient));
            this.label1 = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.tbx_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_port = new System.Windows.Forms.TextBox();
            this.lbl_connectStatu = new System.Windows.Forms.Label();
            this.tbx_log = new System.Windows.Forms.TextBox();
            this.tbx_sendMessage = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lnk_clearLog = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(323, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器IP地址：";
            // 
            // btn_connect
            // 
            this.btn_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_connect.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_connect.Location = new System.Drawing.Point(441, 98);
            this.btn_connect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(92, 37);
            this.btn_connect.TabIndex = 1;
            this.btn_connect.Text = "连接";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // tbx_ip
            // 
            this.tbx_ip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_ip.Location = new System.Drawing.Point(406, 36);
            this.tbx_ip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_ip.Name = "tbx_ip";
            this.tbx_ip.Size = new System.Drawing.Size(126, 23);
            this.tbx_ip.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(323, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "服务器端口号：";
            // 
            // tbx_port
            // 
            this.tbx_port.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_port.Location = new System.Drawing.Point(406, 62);
            this.tbx_port.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_port.Name = "tbx_port";
            this.tbx_port.Size = new System.Drawing.Size(126, 23);
            this.tbx_port.TabIndex = 4;
            // 
            // lbl_connectStatu
            // 
            this.lbl_connectStatu.AutoSize = true;
            this.lbl_connectStatu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_connectStatu.Location = new System.Drawing.Point(20, 314);
            this.lbl_connectStatu.Name = "lbl_connectStatu";
            this.lbl_connectStatu.Size = new System.Drawing.Size(44, 17);
            this.lbl_connectStatu.TabIndex = 5;
            this.lbl_connectStatu.Text = "未连接";
            this.lbl_connectStatu.TextChanged += new System.EventHandler(this.lal_connectStatu_TextChanged);
            // 
            // tbx_log
            // 
            this.tbx_log.Location = new System.Drawing.Point(23, 36);
            this.tbx_log.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_log.Multiline = true;
            this.tbx_log.Name = "tbx_log";
            this.tbx_log.ReadOnly = true;
            this.tbx_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbx_log.Size = new System.Drawing.Size(285, 274);
            this.tbx_log.TabIndex = 8;
            // 
            // tbx_sendMessage
            // 
            this.tbx_sendMessage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_sendMessage.Location = new System.Drawing.Point(327, 147);
            this.tbx_sendMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_sendMessage.Multiline = true;
            this.tbx_sendMessage.Name = "tbx_sendMessage";
            this.tbx_sendMessage.Size = new System.Drawing.Size(206, 120);
            this.tbx_sendMessage.TabIndex = 9;
            // 
            // btn_send
            // 
            this.btn_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_send.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_send.Location = new System.Drawing.Point(441, 275);
            this.btn_send.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(92, 35);
            this.btn_send.TabIndex = 10;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(20, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "通讯记录：";
            // 
            // lnk_clearLog
            // 
            this.lnk_clearLog.AutoSize = true;
            this.lnk_clearLog.Location = new System.Drawing.Point(252, 314);
            this.lnk_clearLog.Name = "lnk_clearLog";
            this.lnk_clearLog.Size = new System.Drawing.Size(56, 17);
            this.lnk_clearLog.TabIndex = 12;
            this.lnk_clearLog.TabStop = true;
            this.lnk_clearLog.Text = "清空记录";
            this.lnk_clearLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_clearLog_LinkClicked);
            // 
            // Frm_TCPClient
            // 
            this.AcceptButton = this.btn_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(553, 341);
            this.Controls.Add(this.lnk_clearLog);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tbx_sendMessage);
            this.Controls.Add(this.tbx_log);
            this.Controls.Add(this.lbl_connectStatu);
            this.Controls.Add(this.tbx_port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbx_ip);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(569, 380);
            this.MinimumSize = new System.Drawing.Size(569, 380);
            this.Name = "Frm_TCPClient";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_TCPClient_FormClosing);
            this.Load += new System.EventHandler(this.Frm_TCPIP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbx_sendMessage;
        internal System.Windows.Forms.TextBox tbx_ip;
        internal System.Windows.Forms.TextBox tbx_port;
        internal System.Windows.Forms.TextBox tbx_log;
        internal System.Windows.Forms.Label lbl_connectStatu;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btn_connect;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button btn_send;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel lnk_clearLog;
    }
}