using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataCenter
{
    public partial class AdminNodeInfoForm : Form
    {
        sqlClass sql = new sqlClass();
        public static string DBConstr = sqlClass.DBConstr;

        public AdminNodeInfoForm()
        {
            InitializeComponent();

            //建立数据库连接
            //获取所有节点列表信息
            cboNodeQuery();
        }

        private void cboNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem myItem = (ComboBoxItem)cboNode.Items[cboNode.SelectedIndex];
            this.txbCarInfo.Text = myItem.Text;
            int nodeID = int.Parse(myItem.Value.ToString());

            using (SqlConnection DBConn = new SqlConnection(DBConstr))
            {
                DBConn.Open();
                using (SqlCommand cmd = DBConn.CreateCommand())
                {
                    cmd.CommandText = "select ID, CarInfo, InCharge, PhoneNum from T_Node where ID=" + nodeID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.txbPhoneNum.Text = reader.GetString(reader.GetOrdinal("PhoneNum"));
                            this.txbInCharge.Text = reader.GetString(reader.GetOrdinal("InCharge"));
                        }
                    }
                }
            }
        }

        private void btnUpdateNode_Click(object sender, EventArgs e)
        {
            if (cboNode.SelectedIndex != -1)    //判断当前下拉框中是否有一项被选中
            {
                ComboBoxItem myItem = (ComboBoxItem)cboNode.Items[cboNode.SelectedIndex];
                int nodeID = int.Parse(myItem.Value.ToString());    //获取nodeID
                string carInfo = this.txbCarInfo.Text.Trim().ToString();
                string incharge = this.txbInCharge.Text.Trim().ToString();
                string phone = this.txbPhoneNum.Text.Trim().ToString();

                using (SqlConnection DBConn = new SqlConnection(DBConstr))
                {
                    DBConn.Open();
                    using (SqlCommand cmd = DBConn.CreateCommand())
                    {
                        cmd.CommandText = "update T_Node set CarInfo='" + carInfo +
                            "', InCharge='" + incharge +
                            "', PhoneNum='" + phone + "' where ID=" + nodeID;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("更新成功！");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一个节点！");
            }
        }

        private void btnDeleteNode_Click(object sender, EventArgs e)
        {
            if (cboNode.SelectedIndex != -1)    //判断当前下拉框中是否有一项被选中
            {
                if (MessageBox.Show("确定删除？", "注意", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    ComboBoxItem myItem = (ComboBoxItem)cboNode.Items[cboNode.SelectedIndex];
                    int nodeID = int.Parse(myItem.Value.ToString());    //获取nodeID

                    using (SqlConnection DBConn = new SqlConnection(DBConstr))
                    {
                        DBConn.Open();
                        using (SqlCommand cmd = DBConn.CreateCommand())
                        {
                            cmd.CommandText = "delete from T_Node where ID=" + nodeID;
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("删除成功！");

                            //重载下拉框
                            this.cboNode.Items.Clear();
                            this.txbCarInfo.Text = "";
                            this.txbInCharge.Text = "";
                            this.txbPhoneNum.Text = "";
                            cboNodeQuery();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一个节点！");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txbAddPhoneNum.Text = "";
            this.txbAddInCharge.Text = "";
            this.txbAddCarInfo.Text = "";
        }

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            string carInfo = this.txbAddCarInfo.Text.Trim().ToString();
            string inCharge = this.txbAddInCharge.Text.Trim().ToString();
            string phone = this.txbAddPhoneNum.Text.Trim().ToString();

            using (SqlConnection DBConn = new SqlConnection(DBConstr))
            {
                DBConn.Open();
                using (SqlCommand cmd = DBConn.CreateCommand())
                {
                    cmd.CommandText = "insert into T_Node(CarInfo, InCharge, PhoneNum) values('"+
                        carInfo + "','" + inCharge + "','" + phone + "')";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("添加成功！");

                    //重载下拉框
                    this.cboNode.Items.Clear();
                    cboNodeQuery();
                }
            }
        }
        
        /// <summary>
        /// 将数据库中的T_Node表中节点车辆信息数据加载到下拉框中
        /// </summary>
        private void cboNodeQuery()
        {
            try
            {
                using (SqlConnection DBConn = new SqlConnection(DBConstr))
                {
                    DBConn.Open();
                    using (SqlCommand cmd = DBConn.CreateCommand())
                    {
                        cmd.CommandText = "select ID, CarInfo from T_Node order by ID";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //List<string> carInfo = new List<string>();
                            while (reader.Read())
                            {
                                ComboBoxItem newitem = new ComboBoxItem();
                                newitem.Text = reader.GetString(reader.GetOrdinal("CarInfo"));
                                newitem.Value = reader.GetInt32(reader.GetOrdinal("ID"));
                                cboNode.Items.Add(newitem);
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}
