namespace VisionAndMotionPro
{
    partial class Frm_TCPServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TCPServer));
            this.lbx_connectedNumber = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.断开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.tbx_sendMessage = new System.Windows.Forms.TextBox();
            this.tbx_log = new System.Windows.Forms.TextBox();
            this.tbx_port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_ip = new System.Windows.Forms.TextBox();
            this.btn_listen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbx_connectedMember = new System.Windows.Forms.ComboBox();
            this.lbl_connectStatu = new System.Windows.Forms.Label();
            this.lnk_clearLog = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbx_connectedNumber
            // 
            this.lbx_connectedNumber.ContextMenuStrip = this.contextMenuStrip1;
            this.lbx_connectedNumber.FormattingEnabled = true;
            this.lbx_connectedNumber.ItemHeight = 17;
            this.lbx_connectedNumber.Location = new System.Drawing.Point(19, 167);
            this.lbx_connectedNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbx_connectedNumber.Name = "lbx_connectedNumber";
            this.lbx_connectedNumber.Size = new System.Drawing.Size(162, 157);
            this.lbx_connectedNumber.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.断开ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 断开ToolStripMenuItem
            // 
            this.断开ToolStripMenuItem.Name = "断开ToolStripMenuItem";
            this.断开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.断开ToolStripMenuItem.Text = "断开";
            this.断开ToolStripMenuItem.Click += new System.EventHandler(this.断开ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "已连接列表";
            // 
            // btn_send
            // 
            this.btn_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_send.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_send.Location = new System.Drawing.Point(435, 316);
            this.btn_send.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(82, 29);
            this.btn_send.TabIndex = 18;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // tbx_sendMessage
            // 
            this.tbx_sendMessage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_sendMessage.Location = new System.Drawing.Point(199, 316);
            this.tbx_sendMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_sendMessage.Name = "tbx_sendMessage";
            this.tbx_sendMessage.Size = new System.Drawing.Size(230, 29);
            this.tbx_sendMessage.TabIndex = 17;
            // 
            // tbx_log
            // 
            this.tbx_log.Location = new System.Drawing.Point(199, 48);
            this.tbx_log.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_log.Multiline = true;
            this.tbx_log.Name = "tbx_log";
            this.tbx_log.ReadOnly = true;
            this.tbx_log.Size = new System.Drawing.Size(318, 260);
            this.tbx_log.TabIndex = 16;
            // 
            // tbx_port
            // 
            this.tbx_port.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_port.Location = new System.Drawing.Point(73, 47);
            this.tbx_port.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_port.Name = "tbx_port";
            this.tbx_port.Size = new System.Drawing.Size(108, 23);
            this.tbx_port.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "端口号：";
            // 
            // tbx_ip
            // 
            this.tbx_ip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_ip.Location = new System.Drawing.Point(73, 18);
            this.tbx_ip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_ip.Name = "tbx_ip";
            this.tbx_ip.Size = new System.Drawing.Size(108, 23);
            this.tbx_ip.TabIndex = 13;
            // 
            // btn_listen
            // 
            this.btn_listen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_listen.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_listen.Location = new System.Drawing.Point(17, 88);
            this.btn_listen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_listen.Name = "btn_listen";
            this.btn_listen.Size = new System.Drawing.Size(164, 45);
            this.btn_listen.TabIndex = 12;
            this.btn_listen.Text = "开始监听";
            this.btn_listen.UseVisualStyleBackColor = true;
            this.btn_listen.Click += new System.EventHandler(this.btn_listen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(14, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "IP地址：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "通讯记录";
            // 
            // cbx_connectedMember
            // 
            this.cbx_connectedMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_connectedMember.FormattingEnabled = true;
            this.cbx_connectedMember.Location = new System.Drawing.Point(368, 15);
            this.cbx_connectedMember.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_connectedMember.Name = "cbx_connectedMember";
            this.cbx_connectedMember.Size = new System.Drawing.Size(149, 25);
            this.cbx_connectedMember.TabIndex = 20;
            // 
            // lbl_connectStatu
            // 
            this.lbl_connectStatu.AutoSize = true;
            this.lbl_connectStatu.ForeColor = System.Drawing.Color.Red;
            this.lbl_connectStatu.Location = new System.Drawing.Point(16, 328);
            this.lbl_connectStatu.Name = "lbl_connectStatu";
            this.lbl_connectStatu.Size = new System.Drawing.Size(44, 17);
            this.lbl_connectStatu.TabIndex = 24;
            this.lbl_connectStatu.Text = "未监听";
            // 
            // lnk_clearLog
            // 
            this.lnk_clearLog.AutoSize = true;
            this.lnk_clearLog.Location = new System.Drawing.Point(132, 328);
            this.lnk_clearLog.Name = "lnk_clearLog";
            this.lnk_clearLog.Size = new System.Drawing.Size(56, 17);
            this.lnk_clearLog.TabIndex = 25;
            this.lnk_clearLog.TabStop = true;
            this.lnk_clearLog.Text = "清空记录";
            this.lnk_clearLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_clearLog_LinkClicked);
            // 
            // Frm_TCPServer
            // 
            this.AcceptButton = this.btn_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(535, 363);
            this.Controls.Add(this.lnk_clearLog);
            this.Controls.Add(this.lbl_connectStatu);
            this.Controls.Add(this.cbx_connectedMember);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tbx_sendMessage);
            this.Controls.Add(this.tbx_log);
            this.Controls.Add(this.tbx_port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbx_ip);
            this.Controls.Add(this.btn_listen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbx_connectedNumber);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(551, 402);
            this.MinimumSize = new System.Drawing.Size(551, 402);
            this.Name = "Frm_TCPServer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP服务器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_TCPServer_FormClosing);
            this.Load += new System.EventHandler(this.Frm_TCPServer_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbx_connectedNumber;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox tbx_sendMessage;
        internal System.Windows.Forms.TextBox tbx_log;
        internal System.Windows.Forms.TextBox tbx_port;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox tbx_ip;
        internal System.Windows.Forms.Button btn_listen;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbx_connectedMember;
        private System.Windows.Forms.Label lbl_connectStatu;
        private System.Windows.Forms.LinkLabel lnk_clearLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 断开ToolStripMenuItem;
    }
}