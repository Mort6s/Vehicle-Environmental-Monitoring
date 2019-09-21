using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataCenter
{
    public partial class QueryHistoryForm : Form
    {
        sqlClass sql = new sqlClass();
        public static string DBConstr = sqlClass.DBConstr;

        /// <summary>
        /// 初始化窗体及下拉列表中所有的节点信息
        /// </summary>
        public QueryHistoryForm()
        {
            InitializeComponent();
            
            //建立数据库连接
            //获取所有节点列表信息
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
                                cboChooseNode.Items.Add(newitem);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void cboChooseNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem myItem = (ComboBoxItem)cboChooseNode.Items[cboChooseNode.SelectedIndex];
            labCarInfo.Text = myItem.Text.ToString();
            labNodeID.Text = myItem.Value.ToString();
        }

        /// <summary>
        /// 查询节点对应记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryNode_Click(object sender, EventArgs e)
        {
            this.dgvNodeHistory.Rows.Clear();
            ComboBoxItem myItem = (ComboBoxItem)cboChooseNode.Items[cboChooseNode.SelectedIndex];
            string date = dtpChooseDate.Value.ToString("yyyy-MM-dd");
            if (myItem.Text != "")
            {
                List<M_Location> locList = new List<M_Location>();
                using (SqlConnection DBConn = new SqlConnection(DBConstr))
                {
                    DBConn.Open();
                    using (SqlCommand cmd = DBConn.CreateCommand())
                    {
                        string where = " where convert(varchar(10), Time, 120) = @date ";
                        //string where = " where convert(varchar(10), Time, 120)=" + date;
                        cmd.CommandText = "select ID,Longitude,Latitude,Time,Temperature,Humidity,SO,PM25,PM10 from T_Location_" + myItem.Value.ToString() + where + " order by Time desc";
                        cmd.Parameters.Add(new SqlParameter("date", date));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                M_Location loc = new M_Location();
                                loc.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                                loc.longitude = reader.GetDouble(reader.GetOrdinal("Longitude"));
                                loc.latitude = reader.GetDouble(reader.GetOrdinal("Latitude"));
                                loc.time = reader.GetDateTime(reader.GetOrdinal("Time"));
                                loc.temperature = reader.GetString(reader.GetOrdinal("Temperature"));
                                loc.humidity = reader.GetString(reader.GetOrdinal("Humidity"));
                                loc.so = reader.GetString(reader.GetOrdinal("SO"));
                                loc.pm25 = reader.GetString(reader.GetOrdinal("PM25"));
                                loc.pm10 = reader.GetString(reader.GetOrdinal("PM10"));
                                locList.Add(loc);
                            }
                        }
                    }
                    for (int i = 0; i < locList.Count; i++)
                    {
                        int index = this.dgvNodeHistory.Rows.Add();
                        this.dgvNodeHistory.Rows[index].Cells[0].Value = locList[i].time;
                        this.dgvNodeHistory.Rows[index].Cells[1].Value = locList[i].longitude;
                        this.dgvNodeHistory.Rows[index].Cells[2].Value = locList[i].latitude;
                        this.dgvNodeHistory.Rows[index].Cells[3].Value = locList[i].temperature;
                        this.dgvNodeHistory.Rows[index].Cells[4].Value = locList[i].humidity;
                        this.dgvNodeHistory.Rows[index].Cells[5].Value = locList[i].so;
                        this.dgvNodeHistory.Rows[index].Cells[6].Value = locList[i].pm25;
                        this.dgvNodeHistory.Rows[index].Cells[7].Value = locList[i].pm10;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一个节点!", "注意");
            }
        }

    }
}
