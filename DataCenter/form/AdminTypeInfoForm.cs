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
    public partial class AdminTypeInfoForm : Form
    {
        sqlClass sql = new sqlClass();
        public static string DBConstr = sqlClass.DBConstr;

        public AdminTypeInfoForm()
        {
            InitializeComponent();

            //建立数据库连接
            //获取所有矿种列表信息
            cboNodeQuery();
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
                        cmd.CommandText = "select ID, Type from T_MineType order by ID";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //List<string> carInfo = new List<string>();
                            while (reader.Read())
                            {
                                ComboBoxItem newitem = new ComboBoxItem();
                                newitem.Text = reader.GetString(reader.GetOrdinal("Type"));
                                newitem.Value = reader.GetInt32(reader.GetOrdinal("ID"));
                                cboType.Items.Add(newitem);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem myItem = (ComboBoxItem)cboType.Items[cboType.SelectedIndex];
            this.txbTypeName.Text = myItem.Text;
            int typeID = int.Parse(myItem.Value.ToString());

            using (SqlConnection DBConn = new SqlConnection(DBConstr))
            {
                DBConn.Open();
                using (SqlCommand cmd = DBConn.CreateCommand())
                {
                    cmd.CommandText = "select ID, Type, UnitPrice from T_MineType where ID=" + typeID;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.txbUnitPrice.Text = reader.GetDouble(reader.GetOrdinal("UnitPrice")).ToString();
                        }
                    }
                }
            }
        }

        private void btnUpdateType_Click(object sender, EventArgs e)
        {
            //if (cboType.SelectedIndex != -1)    //判断当前下拉框中是否有一项被选中
            //{
            //    ComboBoxItem myItem = (ComboBoxItem)cboType.Items[cboType.SelectedIndex];
            //    int typeID = int.Parse(myItem.Value.ToString());    //获取nodeID
            //    string typeName = this.txbTypeName.Text.Trim().ToString();
            //    string strPrice = this.txbUnitPrice.Text.Trim().ToString();
            //    double price = Convert.ToDouble(strPrice);

            //    using (SqlConnection DBConn = new SqlConnection(DBConstr))
            //    {
            //        DBConn.Open();
            //        using (SqlCommand cmd = DBConn.CreateCommand())
            //        {
            //            cmd.CommandText = "update T_MineType set Type='" + typeName +
            //                "', UnitPrice='" + price + "' where ID=" + typeID;
            //            cmd.ExecuteNonQuery();
            //            MessageBox.Show("更新成功！");
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请选择矿种！");
            //}
        }

        private void btnDeleteType_Click(object sender, EventArgs e)
        {
            //if (cboType.SelectedIndex != -1)    //判断当前下拉框中是否有一项被选中
            //{
            //    if (MessageBox.Show("确定删除？", "注意", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //    {
            //        ComboBoxItem myItem = (ComboBoxItem)cboType.Items[cboType.SelectedIndex];
            //        int typeID = int.Parse(myItem.Value.ToString());    //获取nodeID

            //        using (SqlConnection DBConn = new SqlConnection(DBConstr))
            //        {
            //            DBConn.Open();
            //            using (SqlCommand cmd = DBConn.CreateCommand())
            //            {
            //                cmd.CommandText = "delete from T_MineType where ID=" + typeID;
            //                cmd.ExecuteNonQuery();
            //                MessageBox.Show("删除成功！");

            //                //重载下拉框
            //                this.cboType.Items.Clear();
            //                this.txbUnitPrice.Text = "";
            //                this.txbTypeName.Text = "";
            //                cboNodeQuery();
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请选择一个矿种！");
            //}
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            //string typeName = this.txbAddTypeName.Text.Trim().ToString();
            //string strPrice = this.txbAddUnitPrice.Text.Trim().ToString();
            //double price = Convert.ToDouble(strPrice);

            //using (SqlConnection DBConn = new SqlConnection(DBConstr))
            //{
            //    DBConn.Open();
            //    using (SqlCommand cmd = DBConn.CreateCommand())
            //    {
            //        cmd.CommandText = "insert into T_MineType(Type, UnitPrice) values('" +
            //            typeName + "','" + price + "')";
            //        cmd.ExecuteNonQuery();
            //        MessageBox.Show("添加成功！");

            //        //重载下拉框
            //        this.cboType.Items.Clear();
            //        cboNodeQuery();
            //    }
            //}
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txbAddTypeName.Text = "";
            this.txbAddUnitPrice.Text = "";
        }
    }
}
