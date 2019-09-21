using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingCloud
{
    public class M_Point
    {
        public int ID;              //节点ID
        public double longitude;    //位置经度
        public double latitude;     //位置纬度
        public DateTime time;       //到达时间
        public string value;        //值
    }

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

}
