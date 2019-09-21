namespace DataCenter
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeReceiveStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdminNodeInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdminTypeInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelper = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.Blank = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStatus,
            this.tsmiQuery,
            this.tsmiAdmin,
            this.tsmiHelper});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(694, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiStatus
            // 
            this.tsmiStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNodeReceiveStatus});
            this.tsmiStatus.Name = "tsmiStatus";
            this.tsmiStatus.Size = new System.Drawing.Size(44, 21);
            this.tsmiStatus.Text = "实时";
            // 
            // tsmiNodeReceiveStatus
            // 
            this.tsmiNodeReceiveStatus.Name = "tsmiNodeReceiveStatus";
            this.tsmiNodeReceiveStatus.Size = new System.Drawing.Size(148, 22);
            this.tsmiNodeReceiveStatus.Text = "节点接收状况";
            this.tsmiNodeReceiveStatus.Click += new System.EventHandler(this.tsmiNodeReceiveStatus_Click);
            // 
            // tsmiQuery
            // 
            this.tsmiQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHistory});
            this.tsmiQuery.Name = "tsmiQuery";
            this.tsmiQuery.Size = new System.Drawing.Size(44, 21);
            this.tsmiQuery.Text = "查询";
            // 
            // tsmiHistory
            // 
            this.tsmiHistory.Name = "tsmiHistory";
            this.tsmiHistory.Size = new System.Drawing.Size(124, 22);
            this.tsmiHistory.Text = "历史信息";
            this.tsmiHistory.Click += new System.EventHandler(this.tsmiHistory_Click);
            // 
            // tsmiAdmin
            // 
            this.tsmiAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdminNodeInfo,
            this.tsmiAdminTypeInfo});
            this.tsmiAdmin.Name = "tsmiAdmin";
            this.tsmiAdmin.Size = new System.Drawing.Size(44, 21);
            this.tsmiAdmin.Text = "管理";
            // 
            // tsmiAdminNodeInfo
            // 
            this.tsmiAdminNodeInfo.Name = "tsmiAdminNodeInfo";
            this.tsmiAdminNodeInfo.Size = new System.Drawing.Size(152, 22);
            this.tsmiAdminNodeInfo.Text = "节点信息";
            this.tsmiAdminNodeInfo.Click += new System.EventHandler(this.tsmiAdminNodeInfo_Click);
            // 
            // tsmiAdminTypeInfo
            // 
            this.tsmiAdminTypeInfo.Name = "tsmiAdminTypeInfo";
            this.tsmiAdminTypeInfo.Size = new System.Drawing.Size(152, 22);
            this.tsmiAdminTypeInfo.Text = "传感器信息";
            this.tsmiAdminTypeInfo.Click += new System.EventHandler(this.tsmiAdminTypeInfo_Click);
            // 
            // tsmiHelper
            // 
            this.tsmiHelper.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.tsmiHelper.Name = "tsmiHelper";
            this.tsmiHelper.Size = new System.Drawing.Size(44, 21);
            this.tsmiHelper.Text = "帮助";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(100, 22);
            this.tsmiAbout.Text = "关于";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.Blank});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(694, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(32, 17);
            this.tsslStatus.Text = "就绪";
            // 
            // Blank
            // 
            this.Blank.AutoSize = false;
            this.Blank.Name = "Blank";
            this.Blank.Size = new System.Drawing.Size(500, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 562);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(710, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据中心软件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeReceiveStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmiHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelper;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStripStatusLabel Blank;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdmin;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdminNodeInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdminTypeInfo;
    }
}

