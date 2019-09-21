namespace DataCenter
{
    partial class NodeReceiveStatusForm
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
            this.gpbSocketSetting = new System.Windows.Forms.GroupBox();
            this.chbShowOriData = new System.Windows.Forms.CheckBox();
            this.btnSocketStop = new System.Windows.Forms.Button();
            this.btnSocketStart = new System.Windows.Forms.Button();
            this.txbListenPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbServerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lsbStatus = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.gpbSocketSetting.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gpbSocketSetting);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 87);
            this.panel1.TabIndex = 0;
            // 
            // gpbSocketSetting
            // 
            this.gpbSocketSetting.Controls.Add(this.chbShowOriData);
            this.gpbSocketSetting.Controls.Add(this.btnSocketStop);
            this.gpbSocketSetting.Controls.Add(this.btnSocketStart);
            this.gpbSocketSetting.Controls.Add(this.txbListenPort);
            this.gpbSocketSetting.Controls.Add(this.label2);
            this.gpbSocketSetting.Controls.Add(this.txbServerIP);
            this.gpbSocketSetting.Controls.Add(this.label1);
            this.gpbSocketSetting.Location = new System.Drawing.Point(9, 12);
            this.gpbSocketSetting.Name = "gpbSocketSetting";
            this.gpbSocketSetting.Size = new System.Drawing.Size(677, 69);
            this.gpbSocketSetting.TabIndex = 0;
            this.gpbSocketSetting.TabStop = false;
            this.gpbSocketSetting.Text = "配置";
            // 
            // chbShowOriData
            // 
            this.chbShowOriData.AutoSize = true;
            this.chbShowOriData.Location = new System.Drawing.Point(406, 28);
            this.chbShowOriData.Name = "chbShowOriData";
            this.chbShowOriData.Size = new System.Drawing.Size(96, 16);
            this.chbShowOriData.TabIndex = 6;
            this.chbShowOriData.Text = "显示原始数据";
            this.chbShowOriData.UseVisualStyleBackColor = true;
            // 
            // btnSocketStop
            // 
            this.btnSocketStop.Location = new System.Drawing.Point(588, 25);
            this.btnSocketStop.Name = "btnSocketStop";
            this.btnSocketStop.Size = new System.Drawing.Size(52, 23);
            this.btnSocketStop.TabIndex = 5;
            this.btnSocketStop.Text = "停止";
            this.btnSocketStop.UseVisualStyleBackColor = true;
            this.btnSocketStop.Click += new System.EventHandler(this.btnSocketStop_Click);
            // 
            // btnSocketStart
            // 
            this.btnSocketStart.Location = new System.Drawing.Point(519, 25);
            this.btnSocketStart.Name = "btnSocketStart";
            this.btnSocketStart.Size = new System.Drawing.Size(56, 23);
            this.btnSocketStart.TabIndex = 4;
            this.btnSocketStart.Text = "开始";
            this.btnSocketStart.UseVisualStyleBackColor = true;
            this.btnSocketStart.Click += new System.EventHandler(this.btnSocketStart_Click);
            // 
            // txbListenPort
            // 
            this.txbListenPort.Location = new System.Drawing.Point(309, 27);
            this.txbListenPort.Name = "txbListenPort";
            this.txbListenPort.Size = new System.Drawing.Size(70, 21);
            this.txbListenPort.TabIndex = 3;
            this.txbListenPort.Text = "12345";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "侦听端口";
            // 
            // txbServerIP
            // 
            this.txbServerIP.Location = new System.Drawing.Point(89, 27);
            this.txbServerIP.Name = "txbServerIP";
            this.txbServerIP.Size = new System.Drawing.Size(133, 21);
            this.txbServerIP.TabIndex = 1;
            this.txbServerIP.Text = "103.229.127.7";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器IP";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lsbStatus);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(701, 371);
            this.panel2.TabIndex = 1;
            // 
            // lsbStatus
            // 
            this.lsbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbStatus.FormattingEnabled = true;
            this.lsbStatus.ItemHeight = 12;
            this.lsbStatus.Location = new System.Drawing.Point(0, 0);
            this.lsbStatus.Name = "lsbStatus";
            this.lsbStatus.Size = new System.Drawing.Size(701, 371);
            this.lsbStatus.TabIndex = 0;
            // 
            // NodeReceiveStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 458);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "NodeReceiveStatusForm";
            this.Text = "实时接收状况";
            this.panel1.ResumeLayout(false);
            this.gpbSocketSetting.ResumeLayout(false);
            this.gpbSocketSetting.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lsbStatus;
        private System.Windows.Forms.GroupBox gpbSocketSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbServerIP;
        private System.Windows.Forms.TextBox txbListenPort;
        private System.Windows.Forms.Button btnSocketStart;
        private System.Windows.Forms.Button btnSocketStop;
        private System.Windows.Forms.CheckBox chbShowOriData;

    }
}