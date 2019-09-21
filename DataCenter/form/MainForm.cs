using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataCenter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            NodeReceiveStatusForm frmNodeReceiveStatus = new NodeReceiveStatusForm();
            frmNodeReceiveStatus.MdiParent = this;
            frmNodeReceiveStatus.WindowState = FormWindowState.Maximized;
            frmNodeReceiveStatus.Show();
        }

        private void tsmiHistory_Click(object sender, EventArgs e)
        {
            //判断是否已经打开此窗口，默认最大化窗体
            if (!ShowChildrenForm(typeof(QueryHistoryForm)))
            {
                QueryHistoryForm frmQueryHistory = new QueryHistoryForm();
                frmQueryHistory.MdiParent = this;
                frmQueryHistory.WindowState = FormWindowState.Maximized;
                frmQueryHistory.Show();
            }
        }

        private void tsmiNodeReceiveStatus_Click(object sender, EventArgs e)
        {
            //判断是否已经打开此窗口，默认最大化窗体
            if (!ShowChildrenForm(typeof(NodeReceiveStatusForm)))
            {
                NodeReceiveStatusForm frmNodeReceiveStatus = new NodeReceiveStatusForm();
                frmNodeReceiveStatus.MdiParent = this;
                frmNodeReceiveStatus.WindowState = FormWindowState.Maximized;
                frmNodeReceiveStatus.Show();
            }
        }

        /// <summary>
        /// 在主窗口MainForm中,同一类型的子窗口只打开一个
        /// </summary>
        private bool ShowChildrenForm(Type frmType)
        {
            Form frm = FindChildForm(frmType);
            if (frm == null)
                return false;
            //让子窗口获得焦点
            frm.Activate();
            return true;
        }

        /// <summary>
        /// 查找子窗口
        /// </summary>
        public Form FindChildForm(Type frmType)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.GetType() == frmType)
                    return frm;
            }
            return null;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
        
        private void tsmiAdminNodeInfo_Click(object sender, EventArgs e)
        {
            Form form = new AdminNodeInfoForm();
            form.Show();
        }

        private void tsmiAdminAreaInfo_Click(object sender, EventArgs e)
        {
            //Form form = new AdminAreaInfoForm();
            //form.Show();
        }

        private void tsmiAdminTypeInfo_Click(object sender, EventArgs e)
        {
            Form form = new AdminTypeInfoForm();
            form.Show();
        }
    }
}
