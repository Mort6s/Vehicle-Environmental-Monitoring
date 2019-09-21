namespace DataCenter
{
    partial class QueryHistoryForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgNode = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgvNodeHistory = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Temperature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Humidity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PM25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PM10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.gpbNodeInfo = new System.Windows.Forms.GroupBox();
            this.labNodeID = new System.Windows.Forms.Label();
            this.labCarInfo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labNodeNum = new System.Windows.Forms.Label();
            this.dtpChooseDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQueryNode = new System.Windows.Forms.Button();
            this.cboChooseNode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgNode.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodeHistory)).BeginInit();
            this.panel4.SuspendLayout();
            this.gpbNodeInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 465);
            this.panel2.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgNode);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(623, 465);
            this.tabControl1.TabIndex = 0;
            // 
            // tpgNode
            // 
            this.tpgNode.Controls.Add(this.panel5);
            this.tpgNode.Controls.Add(this.panel4);
            this.tpgNode.Controls.Add(this.dtpChooseDate);
            this.tpgNode.Controls.Add(this.label2);
            this.tpgNode.Controls.Add(this.btnQueryNode);
            this.tpgNode.Controls.Add(this.cboChooseNode);
            this.tpgNode.Controls.Add(this.label1);
            this.tpgNode.Controls.Add(this.panel1);
            this.tpgNode.Location = new System.Drawing.Point(4, 22);
            this.tpgNode.Name = "tpgNode";
            this.tpgNode.Padding = new System.Windows.Forms.Padding(3);
            this.tpgNode.Size = new System.Drawing.Size(615, 439);
            this.tpgNode.TabIndex = 0;
            this.tpgNode.Text = "节点";
            this.tpgNode.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dgvNodeHistory);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(133, 59);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(479, 377);
            this.panel5.TabIndex = 7;
            // 
            // dgvNodeHistory
            // 
            this.dgvNodeHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNodeHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNodeHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Longitude,
            this.Latitude,
            this.Temperature,
            this.Humidity,
            this.SO,
            this.PM25,
            this.PM10});
            this.dgvNodeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNodeHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvNodeHistory.Name = "dgvNodeHistory";
            this.dgvNodeHistory.ReadOnly = true;
            this.dgvNodeHistory.RowTemplate.Height = 23;
            this.dgvNodeHistory.Size = new System.Drawing.Size(479, 377);
            this.dgvNodeHistory.TabIndex = 0;
            // 
            // Time
            // 
            this.Time.HeaderText = "时间";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Longitude
            // 
            this.Longitude.HeaderText = "位置经度";
            this.Longitude.Name = "Longitude";
            this.Longitude.ReadOnly = true;
            // 
            // Latitude
            // 
            this.Latitude.HeaderText = "位置纬度";
            this.Latitude.Name = "Latitude";
            this.Latitude.ReadOnly = true;
            // 
            // Temperature
            // 
            this.Temperature.HeaderText = "温度";
            this.Temperature.Name = "Temperature";
            this.Temperature.ReadOnly = true;
            // 
            // Humidity
            // 
            this.Humidity.HeaderText = "湿度";
            this.Humidity.Name = "Humidity";
            this.Humidity.ReadOnly = true;
            // 
            // SO
            // 
            this.SO.HeaderText = "SO浓度";
            this.SO.Name = "SO";
            this.SO.ReadOnly = true;
            // 
            // PM25
            // 
            this.PM25.HeaderText = "PM2.5含量";
            this.PM25.Name = "PM25";
            this.PM25.ReadOnly = true;
            // 
            // PM10
            // 
            this.PM10.HeaderText = "PM10含量";
            this.PM10.Name = "PM10";
            this.PM10.ReadOnly = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.gpbNodeInfo);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(3, 59);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(130, 377);
            this.panel4.TabIndex = 6;
            // 
            // gpbNodeInfo
            // 
            this.gpbNodeInfo.Controls.Add(this.labNodeID);
            this.gpbNodeInfo.Controls.Add(this.labCarInfo);
            this.gpbNodeInfo.Controls.Add(this.label5);
            this.gpbNodeInfo.Controls.Add(this.labNodeNum);
            this.gpbNodeInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.gpbNodeInfo.Location = new System.Drawing.Point(0, 0);
            this.gpbNodeInfo.Name = "gpbNodeInfo";
            this.gpbNodeInfo.Size = new System.Drawing.Size(127, 377);
            this.gpbNodeInfo.TabIndex = 0;
            this.gpbNodeInfo.TabStop = false;
            this.gpbNodeInfo.Text = "节点信息";
            // 
            // labNodeID
            // 
            this.labNodeID.AutoSize = true;
            this.labNodeID.Location = new System.Drawing.Point(63, 34);
            this.labNodeID.Name = "labNodeID";
            this.labNodeID.Size = new System.Drawing.Size(0, 12);
            this.labNodeID.TabIndex = 3;
            // 
            // labCarInfo
            // 
            this.labCarInfo.AutoSize = true;
            this.labCarInfo.Location = new System.Drawing.Point(15, 83);
            this.labCarInfo.Name = "labCarInfo";
            this.labCarInfo.Size = new System.Drawing.Size(0, 12);
            this.labCarInfo.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "车牌号：";
            // 
            // labNodeNum
            // 
            this.labNodeNum.AutoSize = true;
            this.labNodeNum.Location = new System.Drawing.Point(15, 33);
            this.labNodeNum.Name = "labNodeNum";
            this.labNodeNum.Size = new System.Drawing.Size(41, 12);
            this.labNodeNum.TabIndex = 0;
            this.labNodeNum.Text = "编号：";
            // 
            // dtpChooseDate
            // 
            this.dtpChooseDate.Location = new System.Drawing.Point(278, 20);
            this.dtpChooseDate.Name = "dtpChooseDate";
            this.dtpChooseDate.Size = new System.Drawing.Size(122, 21);
            this.dtpChooseDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "选择日期";
            // 
            // btnQueryNode
            // 
            this.btnQueryNode.Location = new System.Drawing.Point(423, 18);
            this.btnQueryNode.Name = "btnQueryNode";
            this.btnQueryNode.Size = new System.Drawing.Size(75, 23);
            this.btnQueryNode.TabIndex = 4;
            this.btnQueryNode.Text = "查询";
            this.btnQueryNode.UseVisualStyleBackColor = true;
            this.btnQueryNode.Click += new System.EventHandler(this.btnQueryNode_Click);
            // 
            // cboChooseNode
            // 
            this.cboChooseNode.FormattingEnabled = true;
            this.cboChooseNode.Location = new System.Drawing.Point(86, 21);
            this.cboChooseNode.Name = "cboChooseNode";
            this.cboChooseNode.Size = new System.Drawing.Size(121, 20);
            this.cboChooseNode.TabIndex = 1;
            this.cboChooseNode.SelectedIndexChanged += new System.EventHandler(this.cboChooseNode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择节点";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 56);
            this.panel1.TabIndex = 5;
            // 
            // QueryHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 465);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(590, 503);
            this.Name = "QueryHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询历史";
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpgNode.ResumeLayout(false);
            this.tpgNode.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodeHistory)).EndInit();
            this.panel4.ResumeLayout(false);
            this.gpbNodeInfo.ResumeLayout(false);
            this.gpbNodeInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgNode;
        private System.Windows.Forms.DateTimePicker dtpChooseDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQueryNode;
        private System.Windows.Forms.ComboBox cboChooseNode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox gpbNodeInfo;
        private System.Windows.Forms.DataGridView dgvNodeHistory;
        private System.Windows.Forms.Label labNodeNum;
        private System.Windows.Forms.Label labCarInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labNodeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Temperature;
        private System.Windows.Forms.DataGridViewTextBoxColumn Humidity;
        private System.Windows.Forms.DataGridViewTextBoxColumn SO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PM25;
        private System.Windows.Forms.DataGridViewTextBoxColumn PM10;
    }
}