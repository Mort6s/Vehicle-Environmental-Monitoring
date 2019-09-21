using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;

namespace DataCenter
{
    public partial class NodeReceiveStatusForm : Form
    {
        Thread thread1;
        Socket s;
        private int port = 12345;               //默认侦听端口号
        private string host = "103.229.127.7";    //默认当前主机号
        //private UdpClient udpClient;
        //string GprsDateNumber = "";
        sqlClass sql = new sqlClass();
        public static string DBConstr = sqlClass.DBConstr;

        public NodeReceiveStatusForm()
        {
            InitializeComponent();
            //tBSetSQL("$GPRMC,020347.00,A,3415.76370,N,11715.26747,E,0.587,357.45,260815,,,A*6BX133,162,53,34,29D");
        }

        /// <summary>
        /// 向ListBox中添加新记录
        /// </summary>
        /// <param name="input"></param>
        public void putList(string input)
        {
            //this.textBox1.Text = input;
            this.Invoke(new MethodInvoker(delegate()
            {
                this.lsbStatus.Items.Add(input);	//listBox1中填加目录名

                //自动滚动到底部
                lsbStatus.TopIndex = lsbStatus.Items.Count - (int)(lsbStatus.Height / lsbStatus.ItemHeight);
            }));
        }

        private void btnSocketStart_Click(object sender, EventArgs e)
        {
            try
            {
                port = int.Parse(this.txbListenPort.Text.Trim());   //获取文本框中的侦听端口号
                host = this.txbServerIP.Text.ToString();            //获取文本框中的服务器IP地址
            }
            catch { }
            thread1 = new Thread(new ThreadStart(ReceiveData));
            thread1.Start();
            this.btnSocketStart.Enabled = false;     //禁用开始按钮
            this.txbListenPort.Enabled = false;      //禁用文本框
            this.txbServerIP.Enabled = false;        //禁用文本框
            this.chbShowOriData.Enabled = false;     //禁用选择框
        }

        private void ReceiveData()
        {
            //创建终结点
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);

            //创建Socket并开始监听
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //创建一个Socket对象，如果用UDP协议，则要用SocketTyype.Dgram类型的套接字
            s.Bind(ipe);    //绑定EndPoint对象(端口和ip地址)
            putList("程序正在等待客户端建立连接..."); 
            s.Listen(0);    //开始监听

            //接受到Client连接，为此连接建立新的Socket，并接受消息
            Socket temp = s.Accept();   //为新建立的连接创建新的Socket
            putList(DateTime.Now + " 已成功建立连接...");
            while (true)
            {
                string recvStr = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = temp.Receive(recvBytes, recvBytes.Length, 0); //从客户端接受消息
                recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);

                //显示原始数据
                if (this.chbShowOriData.Checked == true)
                {
                    putList(DateTime.Now + " 原始数据：" + recvStr);    //把客户端传来的信息显示出来
                }

                //解析数据包并存储数据库
                tBSetSQL(recvStr);
            }
        }
        
        //UDP接收
        //private void ReceiveData()
        //{
        //    //在本机指定的端口接收
        //    udpClient = new UdpClient(port);
        //    IPEndPoint remote = null;
        //    putList("数据接收中...");
        //    //接收从远程主机发送过来的信息
        //    while (true)
        //    {
        //        try
        //        {
        //            //关闭udpClient时此句会产生异常
        //            byte[] bytes = udpClient.Receive(ref remote);
        //            putList("已收到数据，正在解析...");
        //            string str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        //            //TextBoxCallback tx = SetTextBox;
        //            //this.Dispatcher.Invoke(tx, DateTime.Now + " 原始数据：" + str);
        //            putList(DateTime.Now + " 原始数据：" + str);
        //            tBSetSQL(str);
        //            //this.Dispatcher.Invoke(tq, str);
        //        }
        //        catch (Exception ex)
        //        {
        //            //TextBoxCallback tx = SetTextBox;
        //            //this.Dispatcher.Invoke(tx, ex.Message);
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}

        //$GPGGA,090553.000,3415.842653,N,11715.223921,E,1,4,22.78,72.663,M,-0.742,M,,*4,X  PM25,PM10,SO,Hu,Te  D
        public void tBSetSQL(string str)
        {
            double Lat = 0;     //纬度值
            double Lng = 0;     //经度值

            string sensorStr = null;
            string temperature = null;  //温度值
            string humidity = null;     //湿度值
            string so = null;           //SO浓度值
            string pm25 = null;         //PM2.5浓度值
            string pm10 = null;         //PM10浓度值

            try
            {
                string lat = str.Split(',')[3];
                string lng = str.Split(',')[5];

                if (str[0] != '$' && lat != "")
                {
                    return;
                }
                else
                {
                    Lat = fen_du(lat);
                    Lng = fen_du(lng);
                }

                string nodeID = "8";
                //解析各个传感器的值：X  PM25,PM10,SO,Hu,Te  D
                sensorStr = str.Split('X')[1];
                sensorStr = sensorStr.Substring(0, sensorStr.Length - 1);   //去除最后一个字符D

                pm25 = sensorStr.Split(',')[0];         //PM2.5浓度值
                pm10 = sensorStr.Split(',')[1];         //PM10浓度值
                so = sensorStr.Split(',')[2];           //SO浓度值
                humidity = sensorStr.Split(',')[3];     //湿度值
                temperature = sensorStr.Split(',')[4].Substring(0, 2);      //温度值
                
                using (SqlConnection conn = new SqlConnection(DBConstr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "insert into T_Location_" + nodeID + "(Latitude,Longitude,Time,Temperature,Humidity,SO,PM25,PM10)values(@G_Lat,@G_Lng,getdate(),@Tem,@Hum,@So,@Pm25,@Pm10)";
                        cmd.Parameters.Add(new SqlParameter("G_Lat", Lat));
                        cmd.Parameters.Add(new SqlParameter("G_Lng", Lng));
                        cmd.Parameters.Add(new SqlParameter("Tem", temperature));
                        cmd.Parameters.Add(new SqlParameter("Hum", humidity));
                        cmd.Parameters.Add(new SqlParameter("So", so));
                        cmd.Parameters.Add(new SqlParameter("Pm25", pm25));
                        cmd.Parameters.Add(new SqlParameter("Pm10", pm10));

                        cmd.ExecuteNonQuery();
                    }
                }
                
                //TextBoxCallback tx = SetTextBox;
                //this.Dispatcher.Invoke(tx, DateTime.Now + " 转化数据：" + GprsDateNumber + " Lat:" + Lat + " | Lng:" + Lng + "\r\n");
                putList(DateTime.Now + " 节点 " + nodeID + " 纬度 " + Lat + " | 经度 " + Lng +
                    " | 温度 " + temperature + " | 湿度 " + humidity + " | SO浓度 " + so + " | PM2.5浓度 " + pm25 + " | PM10浓度 " + pm10);
            }
            catch (Exception ex)
            {
                putList(DateTime.Now + " 接收失败：" + ex.Message + " 不是有效数据！");
                //TextBoxCallback tx = SetTextBox;
                //this.Dispatcher.Invoke(tx, ex.Message);
            }
        }

        //$GPRMC,075937.00,A,3415.77609,N,11715.24414,E,2.250,347.79,060514,,,A*64
        /// <summary>
        ///  将GPS报文中坐标的分值转换为度值
        /// </summary>
        /// <param name="fen"></param>
        /// <returns></returns>
        public static double fen_du(string fen)
        {
            double intfen1 = 0;
            double intfen2 = 0;

            string strfen1 = fen.Split('.')[0];
            string strfen2 = fen.Split('.')[1];

            string strfen = strfen1.Substring(strfen1.Length - 2, 2) + "." + strfen2;
            intfen1 = Convert.ToDouble(strfen1.Substring(0, strfen1.Length - 2));
            intfen2 = Convert.ToDouble(strfen) / 60;

            return intfen1 + intfen2;
        }

        private void btnSocketStop_Click(object sender, EventArgs e)
        {
            s.Close();                          //关闭socket
            thread1.Abort();                    //关闭线程
            this.btnSocketStart.Enabled = true; //使开始按键可用
            this.chbShowOriData.Enabled = true;
            this.txbServerIP.Enabled = true;
            this.txbListenPort.Enabled = true;
            putList(DateTime.Now + " 接收结束");         //输出list
        }
    }
}
