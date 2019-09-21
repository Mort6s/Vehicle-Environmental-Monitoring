namespace DataCenter
{
    partial class AdminTypeInfoForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgAdd = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAddType = new System.Windows.Forms.Button();
            this.txbAddUnitPrice = new System.Windows.Forms.TextBox();
            this.txbAddTypeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpgUpdate = new System.Windows.Forms.TabPage();
            this.btnDeleteType = new System.Windows.Forms.Button();
            this.btnUpdateType = new System.Windows.Forms.Button();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbUnitPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txbTypeName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tpgAdd.SuspendLayout();
            this.tpgUpdate.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgAdd);
            this.tabControl1.Controls.Add(this.tpgUpdate);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(340, 215);
            this.tabControl1.TabIndex = 0;
            // 
            // tpgAdd
            // 
            this.tpgAdd.Controls.Add(this.btnClear);
            this.tpgAdd.Controls.Add(this.btnAddType);
            this.tpgAdd.Controls.Add(this.txbAddUnitPrice);
            this.tpgAdd.Controls.Add(this.txbAddTypeName);
            this.tpgAdd.Controls.Add(this.label2);
            this.tpgAdd.Controls.Add(this.label1);
            this.tpgAdd.Location = new System.Drawing.Point(4, 22);
            this.tpgAdd.Name = "tpgAdd";
            this.tpgAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tpgAdd.Size = new System.Drawing.Size(332, 189);
            this.tpgAdd.TabIndex = 0;
            this.tpgAdd.Text = "添加";
            this.tpgAdd.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(159, 117);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAddType
            // 
            this.btnAddType.Location = new System.Drawing.Point(66, 117);
            this.btnAddType.Name = "btnAddType";
            this.btnAddType.Size = new System.Drawing.Size(75, 23);
            this.btnAddType.TabIndex = 4;
            this.btnAddType.Text = "添加";
            this.btnAddType.UseVisualStyleBackColor = true;
            this.btnAddType.Click += new System.EventHandler(this.btnAddType_Click);
            // 
            // txbAddUnitPrice
            // 
            this.txbAddUnitPrice.Location = new System.Drawing.Point(112, 71);
            this.txbAddUnitPrice.Name = "txbAddUnitPrice";
            this.txbAddUnitPrice.Size = new System.Drawing.Size(135, 21);
            this.txbAddUnitPrice.TabIndex = 3;
            // 
            // txbAddTypeName
            // 
            this.txbAddTypeName.Location = new System.Drawing.Point(112, 30);
            this.txbAddTypeName.Name = "txbAddTypeName";
            this.txbAddTypeName.Size = new System.Drawing.Size(135, 21);
            this.txbAddTypeName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "单位：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "传感器名称：";
            // 
            // tpgUpdate
            // 
            this.tpgUpdate.Controls.Add(this.btnDeleteType);
            this.tpgUpdate.Controls.Add(this.btnUpdateType);
            this.tpgUpdate.Controls.Add(this.cboType);
            this.tpgUpdate.Controls.Add(this.groupBox1);
            this.tpgUpdate.Controls.Add(this.label3);
            this.tpgUpdate.Location = new System.Drawing.Point(4, 22);
            this.tpgUpdate.Name = "tpgUpdate";
            this.tpgUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tpgUpdate.Size = new System.Drawing.Size(332, 189);
            this.tpgUpdate.TabIndex = 1;
            this.tpgUpdate.Text = "更新";
            this.tpgUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDeleteType
            // 
            this.btnDeleteType.Location = new System.Drawing.Point(270, 18);
            this.btnDeleteType.Name = "btnDeleteType";
            this.btnDeleteType.Size = new System.Drawing.Size(53, 23);
            this.btnDeleteType.TabIndex = 4;
            this.btnDeleteType.Text = "删除";
            this.btnDeleteType.UseVisualStyleBackColor = true;
            this.btnDeleteType.Click += new System.EventHandler(this.btnDeleteType_Click);
            // 
            // btnUpdateType
            // 
            this.btnUpdateType.Location = new System.Drawing.Point(212, 18);
            this.btnUpdateType.Name = "btnUpdateType";
            this.btnUpdateType.Size = new System.Drawing.Size(53, 23);
            this.btnUpdateType.TabIndex = 3;
            this.btnUpdateType.Text = "更新";
            this.btnUpdateType.UseVisualStyleBackColor = true;
            this.btnUpdateType.Click += new System.EventHandler(this.btnUpdateType_Click);
            // 
            // cboType
            // 
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(84, 20);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(121, 20);
            this.cboType.TabIndex = 2;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txbUnitPrice);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txbTypeName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(6, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 133);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "详细信息";
            // 
            // txbUnitPrice
            // 
            this.txbUnitPrice.Location = new System.Drawing.Point(117, 77);
            this.txbUnitPrice.Name = "txbUnitPrice";
            this.txbUnitPrice.Size = new System.Drawing.Size(139, 21);
            this.txbUnitPrice.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "传感器单位：";
            // 
            // txbTypeName
            // 
            this.txbTypeName.Location = new System.Drawing.Point(117, 36);
            this.txbTypeName.Name = "txbTypeName";
            this.txbTypeName.Size = new System.Drawing.Size(137, 21);
            this.txbTypeName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "传感器名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "选择传感器：";
            // 
            // AdminTypeInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 215);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AdminTypeInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "管理传感器";
            this.tabControl1.ResumeLayout(false);
            this.tpgAdd.ResumeLayout(false);
            this.tpgAdd.PerformLayout();
            this.tpgUpdate.ResumeLayout(false);
            this.tpgUpdate.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgAdd;
        private System.Windows.Forms.TabPage tpgUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAddType;
        private System.Windows.Forms.TextBox txbAddUnitPrice;
        private System.Windows.Forms.TextBox txbAddTypeName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Button btnDeleteType;
        private System.Windows.Forms.Button btnUpdateType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbTypeName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbUnitPrice;
    }
}