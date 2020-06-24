namespace VisionAndMotionPro
{
    partial class Frm_SerialPort
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbx_parityBit = new System.Windows.Forms.ComboBox();
            this.cbx_stopBit = new System.Windows.Forms.ComboBox();
            this.tbx_dataBit = new System.Windows.Forms.ComboBox();
            this.cbx_baudRate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_portName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_openPort = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_statu = new System.Windows.Forms.Label();
            this.tbx_output = new System.Windows.Forms.TextBox();
            this.tbx_sendMsg = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lnk_clear = new System.Windows.Forms.LinkLabel();
            this.btn_closePort = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbx_parityBit);
            this.groupBox1.Controls.Add(this.cbx_stopBit);
            this.groupBox1.Controls.Add(this.tbx_dataBit);
            this.groupBox1.Controls.Add(this.cbx_baudRate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbx_portName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(186, 202);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            // 
            // cbx_parityBit
            // 
            this.cbx_parityBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_parityBit.FormattingEnabled = true;
            this.cbx_parityBit.Location = new System.Drawing.Point(67, 159);
            this.cbx_parityBit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_parityBit.Name = "cbx_parityBit";
            this.cbx_parityBit.Size = new System.Drawing.Size(99, 25);
            this.cbx_parityBit.TabIndex = 13;
            this.cbx_parityBit.SelectedIndexChanged += new System.EventHandler(this.cbx_parityBit_SelectedIndexChanged);
            // 
            // cbx_stopBit
            // 
            this.cbx_stopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_stopBit.FormattingEnabled = true;
            this.cbx_stopBit.Location = new System.Drawing.Point(67, 127);
            this.cbx_stopBit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_stopBit.Name = "cbx_stopBit";
            this.cbx_stopBit.Size = new System.Drawing.Size(99, 25);
            this.cbx_stopBit.TabIndex = 12;
            this.cbx_stopBit.SelectedIndexChanged += new System.EventHandler(this.cbx_stopBit_SelectedIndexChanged);
            // 
            // tbx_dataBit
            // 
            this.tbx_dataBit.FormattingEnabled = true;
            this.tbx_dataBit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.tbx_dataBit.Location = new System.Drawing.Point(67, 95);
            this.tbx_dataBit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_dataBit.Name = "tbx_dataBit";
            this.tbx_dataBit.Size = new System.Drawing.Size(99, 25);
            this.tbx_dataBit.TabIndex = 11;
            this.tbx_dataBit.Text = "8";
            this.tbx_dataBit.SelectedIndexChanged += new System.EventHandler(this.tbx_dataBit_SelectedIndexChanged);
            // 
            // cbx_baudRate
            // 
            this.cbx_baudRate.FormattingEnabled = true;
            this.cbx_baudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "10004",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbx_baudRate.Location = new System.Drawing.Point(67, 63);
            this.cbx_baudRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_baudRate.Name = "cbx_baudRate";
            this.cbx_baudRate.Size = new System.Drawing.Size(99, 25);
            this.cbx_baudRate.TabIndex = 10;
            this.cbx_baudRate.Text = "9600";
            this.cbx_baudRate.SelectedIndexChanged += new System.EventHandler(this.cbx_baudRate_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "效验位：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率：";
            // 
            // cbx_portName
            // 
            this.cbx_portName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_portName.FormattingEnabled = true;
            this.cbx_portName.Location = new System.Drawing.Point(67, 31);
            this.cbx_portName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_portName.Name = "cbx_portName";
            this.cbx_portName.Size = new System.Drawing.Size(99, 25);
            this.cbx_portName.TabIndex = 1;
            this.cbx_portName.SelectedIndexChanged += new System.EventHandler(this.cbx_portName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口：";
            // 
            // btn_openPort
            // 
            this.btn_openPort.Location = new System.Drawing.Point(8, 221);
            this.btn_openPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_openPort.Name = "btn_openPort";
            this.btn_openPort.Size = new System.Drawing.Size(87, 33);
            this.btn_openPort.TabIndex = 1;
            this.btn_openPort.Text = "打开串口";
            this.btn_openPort.UseVisualStyleBackColor = true;
            this.btn_openPort.Click += new System.EventHandler(this.btn_openPort_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "状态：";
            // 
            // lbl_statu
            // 
            this.lbl_statu.AutoSize = true;
            this.lbl_statu.ForeColor = System.Drawing.Color.Red;
            this.lbl_statu.Location = new System.Drawing.Point(37, 286);
            this.lbl_statu.Name = "lbl_statu";
            this.lbl_statu.Size = new System.Drawing.Size(44, 17);
            this.lbl_statu.TabIndex = 11;
            this.lbl_statu.Text = "未打开";
            // 
            // tbx_output
            // 
            this.tbx_output.Location = new System.Drawing.Point(209, 38);
            this.tbx_output.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_output.Multiline = true;
            this.tbx_output.Name = "tbx_output";
            this.tbx_output.Size = new System.Drawing.Size(303, 237);
            this.tbx_output.TabIndex = 12;
            // 
            // tbx_sendMsg
            // 
            this.tbx_sendMsg.Location = new System.Drawing.Point(209, 283);
            this.tbx_sendMsg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_sendMsg.Name = "tbx_sendMsg";
            this.tbx_sendMsg.Size = new System.Drawing.Size(229, 23);
            this.tbx_sendMsg.TabIndex = 13;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(444, 283);
            this.btn_send.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(68, 23);
            this.btn_send.TabIndex = 14;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(206, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "通讯记录";
            // 
            // lnk_clear
            // 
            this.lnk_clear.AutoSize = true;
            this.lnk_clear.Location = new System.Drawing.Point(162, 286);
            this.lnk_clear.Name = "lnk_clear";
            this.lnk_clear.Size = new System.Drawing.Size(32, 17);
            this.lnk_clear.TabIndex = 17;
            this.lnk_clear.TabStop = true;
            this.lnk_clear.Text = "清空";
            this.lnk_clear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_clear_LinkClicked);
            // 
            // btn_closePort
            // 
            this.btn_closePort.Location = new System.Drawing.Point(107, 221);
            this.btn_closePort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_closePort.Name = "btn_closePort";
            this.btn_closePort.Size = new System.Drawing.Size(87, 33);
            this.btn_closePort.TabIndex = 18;
            this.btn_closePort.Text = "关闭串口";
            this.btn_closePort.UseVisualStyleBackColor = true;
            this.btn_closePort.Click += new System.EventHandler(this.btn_closePort_Click);
            // 
            // Frm_SerialPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 316);
            this.Controls.Add(this.btn_closePort);
            this.Controls.Add(this.lnk_clear);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tbx_sendMsg);
            this.Controls.Add(this.tbx_output);
            this.Controls.Add(this.lbl_statu);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_openPort);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(536, 355);
            this.MinimumSize = new System.Drawing.Size(536, 355);
            this.Name = "Frm_SerialPort";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "串口";
            this.Load += new System.EventHandler(this.Frm_Serial_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbx_baudRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_portName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_openPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_statu;
        private System.Windows.Forms.TextBox tbx_output;
        private System.Windows.Forms.TextBox tbx_sendMsg;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel lnk_clear;
        private System.Windows.Forms.ComboBox cbx_parityBit;
        private System.Windows.Forms.ComboBox cbx_stopBit;
        private System.Windows.Forms.ComboBox tbx_dataBit;
        private System.Windows.Forms.Button btn_closePort;

    }
}