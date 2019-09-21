namespace DataCenter
{
    partial class AdminNodeInfoForm
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
            this.tpgUpdate = new System.Windows.Forms.TabPage();
            this.cboNode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpbNodeDetail = new System.Windows.Forms.GroupBox();
            this.btnUpdateNode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txbCarInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbInCharge = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbPhoneNum = new System.Windows.Forms.TextBox();
            this.btnDeleteNode = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txbAddCarInfo = new System.Windows.Forms.TextBox();
            this.txbAddInCharge = new System.Windows.Forms.TextBox();
            this.txbAddPhoneNum = new System.Windows.Forms.TextBox();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpgAdd.SuspendLayout();
            this.tpgUpdate.SuspendLayout();
            this.gpbNodeDetail.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(358, 264);
            this.tabControl1.TabIndex = 0;
            // 
            // tpgAdd
            // 
            this.tpgAdd.Controls.Add(this.btnClear);
            this.tpgAdd.Controls.Add(this.btnAddNode);
            this.tpgAdd.Controls.Add(this.txbAddPhoneNum);
            this.tpgAdd.Controls.Add(this.txbAddInCharge);
            this.tpgAdd.Controls.Add(this.txbAddCarInfo);
            this.tpgAdd.Controls.Add(this.label7);
            this.tpgAdd.Controls.Add(this.label6);
            this.tpgAdd.Controls.Add(this.label5);
            this.tpgAdd.Location = new System.Drawing.Point(4, 22);
            this.tpgAdd.Name = "tpgAdd";
            this.tpgAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tpgAdd.Size = new System.Drawing.Size(350, 238);
            this.tpgAdd.TabIndex = 0;
            this.tpgAdd.Text = "添加";
            this.tpgAdd.UseVisualStyleBackColor = true;
            // 
            // tpgUpdate
            // 
            this.tpgUpdate.Controls.Add(this.btnDeleteNode);
            this.tpgUpdate.Controls.Add(this.btnUpdateNode);
            this.tpgUpdate.Controls.Add(this.gpbNodeDetail);
            this.tpgUpdate.Controls.Add(this.label1);
            this.tpgUpdate.Controls.Add(this.cboNode);
            this.tpgUpdate.Location = new System.Drawing.Point(4, 22);
            this.tpgUpdate.Name = "tpgUpdate";
            this.tpgUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tpgUpdate.Size = new System.Drawing.Size(350, 238);
            this.tpgUpdate.TabIndex = 1;
            this.tpgUpdate.Text = "更新";
            this.tpgUpdate.UseVisualStyleBackColor = true;
            // 
            // cboNode
            // 
            this.cboNode.FormattingEnabled = true;
            this.cboNode.Location = new System.Drawing.Point(87, 20);
            this.cboNode.Name = "cboNode";
            this.cboNode.Size = new System.Drawing.Size(121, 20);
            this.cboNode.TabIndex = 0;
            this.cboNode.SelectedIndexChanged += new System.EventHandler(this.cboNode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择节点";
            // 
            // gpbNodeDetail
            // 
            this.gpbNodeDetail.Controls.Add(this.txbPhoneNum);
            this.gpbNodeDetail.Controls.Add(this.label4);
            this.gpbNodeDetail.Controls.Add(this.txbInCharge);
            this.gpbNodeDetail.Controls.Add(this.label3);
            this.gpbNodeDetail.Controls.Add(this.txbCarInfo);
            this.gpbNodeDetail.Controls.Add(this.label2);
            this.gpbNodeDetail.Location = new System.Drawing.Point(8, 57);
            this.gpbNodeDetail.Name = "gpbNodeDetail";
            this.gpbNodeDetail.Size = new System.Drawing.Size(334, 173);
            this.gpbNodeDetail.TabIndex = 2;
            this.gpbNodeDetail.TabStop = false;
            this.gpbNodeDetail.Text = "详细信息";
            // 
            // btnUpdateNode
            // 
            this.btnUpdateNode.Location = new System.Drawing.Point(223, 18);
            this.btnUpdateNode.Name = "btnUpdateNode";
            this.btnUpdateNode.Size = new System.Drawing.Size(53, 23);
            this.btnUpdateNode.TabIndex = 3;
            this.btnUpdateNode.Text = "更新";
            this.btnUpdateNode.UseVisualStyleBackColor = true;
            this.btnUpdateNode.Click += new System.EventHandler(this.btnUpdateNode_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "车牌号码：";
            // 
            // txbCarInfo
            // 
            this.txbCarInfo.Location = new System.Drawing.Point(90, 37);
            this.txbCarInfo.Name = "txbCarInfo";
            this.txbCarInfo.Size = new System.Drawing.Size(146, 21);
            this.txbCarInfo.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "负 责 人：";
            // 
            // txbInCharge
            // 
            this.txbInCharge.Location = new System.Drawing.Point(90, 78);
            this.txbInCharge.Name = "txbInCharge";
            this.txbInCharge.Size = new System.Drawing.Size(146, 21);
            this.txbInCharge.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "联系方式：";
            // 
            // txbPhoneNum
            // 
            this.txbPhoneNum.Location = new System.Drawing.Point(90, 118);
            this.txbPhoneNum.Name = "txbPhoneNum";
            this.txbPhoneNum.Size = new System.Drawing.Size(146, 21);
            this.txbPhoneNum.TabIndex = 5;
            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.Location = new System.Drawing.Point(283, 18);
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(53, 23);
            this.btnDeleteNode.TabIndex = 4;
            this.btnDeleteNode.Text = "删除";
            this.btnDeleteNode.UseVisualStyleBackColor = true;
            this.btnDeleteNode.Click += new System.EventHandler(this.btnDeleteNode_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "车牌号码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "负 责 人：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "联系方式：";
            // 
            // txbAddCarInfo
            // 
            this.txbAddCarInfo.Location = new System.Drawing.Point(125, 39);
            this.txbAddCarInfo.Name = "txbAddCarInfo";
            this.txbAddCarInfo.Size = new System.Drawing.Size(146, 21);
            this.txbAddCarInfo.TabIndex = 3;
            // 
            // txbAddInCharge
            // 
            this.txbAddInCharge.Location = new System.Drawing.Point(125, 78);
            this.txbAddInCharge.Name = "txbAddInCharge";
            this.txbAddInCharge.Size = new System.Drawing.Size(146, 21);
            this.txbAddInCharge.TabIndex = 4;
            // 
            // txbAddPhoneNum
            // 
            this.txbAddPhoneNum.Location = new System.Drawing.Point(125, 119);
            this.txbAddPhoneNum.Name = "txbAddPhoneNum";
            this.txbAddPhoneNum.Size = new System.Drawing.Size(146, 21);
            this.txbAddPhoneNum.TabIndex = 5;
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(96, 168);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(75, 23);
            this.btnAddNode.TabIndex = 6;
            this.btnAddNode.Text = "添加";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(192, 168);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // AdminNodeInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 264);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AdminNodeInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "管理节点信息";
            this.tabControl1.ResumeLayout(false);
            this.tpgAdd.ResumeLayout(false);
            this.tpgAdd.PerformLayout();
            this.tpgUpdate.ResumeLayout(false);
            this.tpgUpdate.PerformLayout();
            this.gpbNodeDetail.ResumeLayout(false);
            this.gpbNodeDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgAdd;
        private System.Windows.Forms.TabPage tpgUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboNode;
        private System.Windows.Forms.GroupBox gpbNodeDetail;
        private System.Windows.Forms.Button btnUpdateNode;
        private System.Windows.Forms.TextBox txbCarInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbInCharge;
        private System.Windows.Forms.TextBox txbPhoneNum;
        private System.Windows.Forms.Button btnDeleteNode;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.TextBox txbAddPhoneNum;
        private System.Windows.Forms.TextBox txbAddInCharge;
        private System.Windows.Forms.TextBox txbAddCarInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClear;
    }
}