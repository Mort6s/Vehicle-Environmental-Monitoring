using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

[WebService(Namespace = "http://103.229.127.7:8080/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]

public class Service : System.Web.Services.WebService
{
    public static string DBConstr = @"Data Source=103.229.127.7;Initial Catalog=JingCloud_DB;User ID=sa;Pwd=qwe1234ASD";
    
    public class M_Location
    {
        public int ID;              //节点ID
        public double longitude;    //位置经度
        public double latitude;     //位置纬度
        public DateTime time;       //到达时间
        public string temperature;  //湿度值
        public string humidity;     //湿度值
        public string so;           //SO浓度值
        public string pm25;         //PM2.5浓度值
        public string pm10;         //PM10浓度值
    }

    static Service()
    {
        //将类的静态方法中设置KEY
        ditujiupian.com.Convert.key = "d12100b84fc44f4fbd9ae3ce6f3e5071";
    }

    ////单次纠偏
    //ditujiupian.com.GetMessage get = ditujiupian.com.Convert.Wgs2Gcj(114.061011,22.543125);

    ////批量纠偏
    //List<ditujiupian.com.LngLat> list = new List<ditujiupian.com.LngLat>();
    //double lng = 114.061011;
    //double lat = 22.543125;
    //for (int i = 0; i < 10; i++)
    //{
    //    lng += 0.001;
    //    lat += 0.001;
    //    LngLat m = new LngLat();
    //    m.Lng = lng;
    //    m.lat = lat;
    //    list.Add(m);
    //}

    public Service () {

        //如果使用设计的组件，请取消注释以下行
        //InitializeComponent(); 
    }

    [WebMethod(Description = "获取实时节点记录，每隔2s调用一次本接口并返回最新一条节点数据，下一次调用参数为返回的参数Tim")]
    public string GetReceiveOnTime(string nodeID, string timeUpdate)
    {
        string str = "{\"ret\":\"success\",";

        using (SqlConnection DBConn = new SqlConnection(DBConstr))
        {
            DBConn.Open();
            using (SqlCommand cmd = DBConn.CreateCommand())
            {
                string where = "";

                where = " where Time > '" + timeUpdate + "' and Time <> '" + timeUpdate + "' ";
                cmd.CommandText = "select ID,Longitude,Latitude,Time,Temperature,Humidity,SO,PM25,PM10 from T_Location_" + nodeID + where + " order by Time desc";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
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

                        str += "\"Lng\":\"" + loc.longitude +
                            "\",\"Lat\":\"" + loc.latitude +
                            "\",\"Tim\":\"" + loc.time +
                            "\",\"PM25\":\"" + loc.pm25 +
                            "\",\"PM10\":\"" + loc.pm10 +
                            "\",\"SO\":\"" + loc.so +
                            "\",\"Hum\":\"" + loc.humidity +
                            "\",\"Tem\":\"" + loc.temperature + "\"";
                    }
                }
            }
        }
        str += "}";

        return str;
    }

    [WebMethod(Description = "获取节点最新记录，若显示实时效果则需每隔2s调用一次本接口")]
    public string GetUpdate(string nodeID)
    {
        string str = "{\"ret\":\"success\",";

        using (SqlConnection DBConn = new SqlConnection(DBConstr))
        {
            DBConn.Open();
            using (SqlCommand cmd = DBConn.CreateCommand())
            {
                cmd.CommandText = "select ID,Longitude,Latitude,Time,Temperature,Humidity,SO,PM25,PM10 from T_Location_" + nodeID + " order by Time desc";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
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

                        str += "\"Date\":\"" + loc.time.ToShortDateString() +
                            "\",\"Tim\":\"" + loc.time.ToString("HH:mm:ss") +
                            "\",\"Lng\":\"" + loc.longitude +
                            "\",\"Lat\":\"" + loc.latitude +
                            "\",\"PM25\":\"" + loc.pm25 +
                            "\",\"PM10\":\"" + loc.pm10 +
                            "\",\"SO\":\"" + loc.so +
                            "\",\"Hum\":\"" + loc.humidity +
                            "\",\"Tem\":\"" + loc.temperature + "\"";
                    }
                }
            }
        }
        str += "}";

        return str;
    }

    [WebMethod(Description = "根据时间范围返回以经纬度坐标为中心的区域范围历史数据")]
    public string QueryHistory(double Lng, double Lat, string FromTime, string ToTime, int NeedCount) {
        string str = "";
        List<M_Location> locList = new List<M_Location>();

        ditujiupian.com.GetMessage get = ditujiupian.com.Convert.BD2Wgs(Lng, Lat);
        Lng = get.Lng;
        Lat = get.Lat;

        double LngMax = Lng + 0.001;    //经度范围扩大100m
        double LngMin = Lng - 0.001;    //经度范围扩大100m
        double LatMax = Lat + 0.001;    //纬度范围扩大100m
        double LatMin = Lat - 0.001;    //纬度范围扩大100m

        using (SqlConnection DBConn = new SqlConnection(DBConstr))
        {
            DBConn.Open();
            using (SqlCommand cmd = DBConn.CreateCommand())
            {
                string where = " where Longitude > " + LngMin.ToString() + " and Longitude < " + LngMax.ToString() + " and Latitude > " +
                    LatMin.ToString() + " and Latitude < " + LatMax.ToString() + " and Time > " + FromTime + " and Time < " + ToTime;

                //where = " where Time >= '" + date + " 16:00:00' and Time <= '" + date + " 19:00:00'";
                //where = " where convert(varchar(10), Time, 120) = @date ";
                cmd.CommandText = "select ID,Longitude,Latitude,Time,Temperature,Humidity,SO,PM25,PM10 from T_Location_1" + where + " order by Time desc";
                //cmd.Parameters.Add(new SqlParameter("date", date));
                
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
        }
        if (locList.Count != 0)
        {
            int i = 0;

            str = "{\"ret\":\"success\",\"Points\":[";

            if (locList.Count > NeedCount)
            {
                int countInternal = locList.Count / NeedCount;
                int count = 0;
                int tempPM25 = 0;
                int tempPM10 = 0;
                int tempSO = 0;
                int tempHum = 0;
                int tempTem = 0;

                for (i = 0; i < locList.Count; i++)
                {
                    count++;
                    if (count == countInternal)
                    {
                        str += "{\"Lng\":\"" + locList[i].longitude +
                            "\",\"Lat\":\"" + locList[i].latitude +
                            "\",\"Tim\":\"" + locList[i].time +
                            "\",\"PM25\":\"" + locList[i].pm25 +
                            "\",\"PM10\":\"" + locList[i].pm10 +
                            "\",\"SO\":\"" + locList[i].so +
                            "\",\"Hum\":\"" + locList[i].humidity +
                            "\",\"Tem\":\"" + locList[i].temperature +
                            "\"},";
                    }
                    else
                    {
                        tempPM25 += int.Parse(locList[i].pm25);
                        tempPM10 += int.Parse(locList[i].pm10);

                    }
                }
                
            }
            else
            {
                for (i = 0; i < locList.Count; i++)
                {
                    str += "{\"Lng\":\"" + locList[i].longitude +
                        "\",\"Lat\":\"" + locList[i].latitude +
                        "\",\"Tim\":\"" + locList[i].time +
                        "\",\"PM25\":\"" + locList[i].pm25 +
                        "\",\"PM10\":\"" + locList[i].pm10 +
                        "\",\"SO\":\"" + locList[i].so +
                        "\",\"Hum\":\"" + locList[i].humidity +
                        "\",\"Tem\":\"" + locList[i].temperature +
                        "\"},";
                }
            }
            str = str.Substring(0, str.Length - 1);     //去除最后的逗号
            str += "]};";
        }
        else
        {
            str += "{\"ret\":\"none\",\"Points\":\"\"}";
        }

        return str;
    }

    [WebMethod(Description = "根据经纬度坐标获取以其为中心的范围内节点历史数据")]
    public string GetPoint(double Lng, double Lat) {
        string str = "";
        List<M_Location> locList = new List<M_Location>();

        ditujiupian.com.GetMessage get = ditujiupian.com.Convert.BD2Wgs(Lng, Lat);
        Lng = get.Lng;
        Lat = get.Lat;

        double LngMax = Lng + 0.001;    //经度范围扩大100m
        double LngMin = Lng - 0.001;    //经度范围扩大100m
        double LatMax = Lat + 0.001;    //纬度范围扩大100m
        double LatMin = Lat - 0.001;    //纬度范围扩大100m

        using (SqlConnection DBConn = new SqlConnection(DBConstr))
        {
            DBConn.Open();
            using (SqlCommand cmd = DBConn.CreateCommand())
            {
                string where = " where Longitude > " + LngMin.ToString() + " and Longitude < " + LngMax.ToString() + " and Latitude > " +
                    LatMin.ToString() + " and Latitude < " + LatMax.ToString();

                //where = " where Time >= '" + date + " 16:00:00' and Time <= '" + date + " 19:00:00'";
                //where = " where convert(varchar(10), Time, 120) = @date ";
                cmd.CommandText = "select ID,Longitude,Latitude,Time,Temperature,Humidity,SO,PM25,PM10 from T_Location_1" + where + " order by Time desc";
                //cmd.Parameters.Add(new SqlParameter("date", date));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int count = 0;

                    while (reader.Read())
                    {
                        count++;
                        if (count == 10)
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
                            count = 0;

                            locList.Add(loc);
                        }
                    }
                }

                if (locList.Count != 0)
                {
                    str = "{\"Points\":[";
                    int i = 0;
                    for (i = 0; i < locList.Count - 1; i++)
                    {
                        str += "{\"Lng\":\"" + locList[i].longitude + 
                            "\",\"Lat\":\""+locList[i].latitude+
                            "\",\"Tim\":\"" + locList[i].time +
                            "\",\"PM25\":\""+locList[i].pm25+
                            "\",\"PM10\":\""+locList[i].pm10+
                            "\",\"SO\":\""+locList[i].so+
                            "\",\"Hum\":\""+locList[i].humidity+
                            "\",\"Tem\":\""+locList[i].temperature+
                            "\"},";
                    }
                    str += "{\"Lng\":\"" + locList[i].longitude +
                            "\",\"Lat\":\"" + locList[i].latitude +
                            "\",\"Tim\":\"" + locList[i].time +
                            "\",\"PM25\":\"" + locList[i].pm25 +
                            "\",\"PM10\":\"" + locList[i].pm10 +
                            "\",\"SO\":\"" + locList[i].so +
                            "\",\"Hum\":\"" + locList[i].humidity +
                            "\",\"Tem\":\"" + locList[i].temperature +
                            "\"}";
                    str += "]};";
                }
            }
        }

        return str;
    }

    //[WebMethod(Description = "根据日期、起始时间、结束时间范围查询数据库节点记录")]
    //public string Query()

    //[WebMethod(Description = "获取节点传输的最新经纬度坐标并获取其节点数据")]
    //public string ReceiveUpdateStatus() {
    //    string str = "";

    //    return "{\"Lng\":\"117.2607272941\",\"Lat\":\"34.2613238191\",\"PM25\":\"230\",\"PM10\":\"200\",\"SO\":\"50\",\"Hum\":\"28\",\"Tem\":\"45\"}";
    //}
}