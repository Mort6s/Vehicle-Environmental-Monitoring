using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

namespace JingCloud
{
    public partial class lblPM25 : Form
    {
        sqlClass sql = new sqlClass();
        public static string DBConstr = sqlClass.DBConstr;

        string pMouseOperate = null;
        string valueMax = "";
        string valueMin = "";
        string type = "";
        string timeUpdate = "";
        DateTime dtUpdate;
        private IPoint pPointPt = null;                      //鼠标点击点
        
        //鹰眼同步
        private bool bCanDrag;              //鹰眼地图上的矩形框可移动的标志
        private IPoint pMoveRectPoint;      //记录在移动鹰眼地图上的矩形框时鼠标的位置
        private IEnvelope pEnv;             //记录数据视图的Extent

        CreateGeoData createGeodata = new CreateGeoData();  //创建地理数据

        int defaultTrans = 50;              //设置默认的透明度

        //需要修改的地理空间数据库路径：在某一路径下先创建，将其路径赋值到变量workspace_Path中
        string workspace_Path = @"E:\Programme\车载\Project\JingCloud\Data.gdb";
        
        public lblPM25()
        {
            InitializeComponent();
        }
        
        //以下为状态栏添加工具信息、空位、比例尺、坐标
        //在状态栏显示工具栏工具信息
        private void axToolbarControl1_OnMouseMove(object sender, IToolbarControlEvents_OnMouseMoveEvent e)
        {
            // 取得鼠标所在工具的索引号
            int index = axToolbarControl1.HitTest(e.x, e.y, false);
            if (index != -1)
            {
                // 取得鼠标所在工具的 ToolbarItem
                IToolbarItem toolbarItem = axToolbarControl1.GetItem(index);
                // 设置状态栏信息
                MessageLabel.Text = toolbarItem.Command.Tooltip;
            }
            else
            {
                MessageLabel.Text = " 就绪  ";
            }
        }
        
        //显示比例尺、坐标
        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            // 显示当前比例尺
            ScaleLabel.Text = " 比例尺 1:" + ((long)this.axMapControl1.MapScale).ToString();
            // 显示当前坐标（为了去除esri，当前坐标的后面的坐标单位为“esriUnknownUnits ”或“ esriMeters ”之类，直接用取子串(Substring)的方法截去
            CoordinateLabel.Text = " 当前坐标 X = " + e.mapX.ToString() + " Y = " + e.mapY.ToString() + " " + this.axMapControl1.MapUnits.ToString().Substring(4);
        }

        private ILayer GetOverviewLayer(IMap map)
        {
            //获取主视图的第一个图层
            ILayer pLayer = map.get_Layer(0);
            //遍历其他图层，并比较视图范围的宽度，返回宽度最大的图层
            ILayer pTempLayer = null;
            for (int i = 1; i < map.LayerCount; i++)
            {
                pTempLayer = map.get_Layer(i);
                if (pLayer.AreaOfInterest.Width < pTempLayer.AreaOfInterest.Width)
                    pLayer = pTempLayer;
            }
            return pLayer;
        }

        //实现鹰眼功能
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            SynchronizeEagleEye();
            ////获取鹰眼图层
            //this.axMapControl2.AddLayer(this.GetOverviewLayer(this.axMapControl1.Map));
            //// 设置 MapControl 显示范围至数据的全局范围
            //this.axMapControl2.Extent = this.axMapControl1.FullExtent;
            //// 刷新鹰眼控件地图
            //this.axMapControl2.Refresh();
        }

        private void SynchronizeEagleEye()
        {
            if (this.axMapControl2.LayerCount > 0)
            {
                this.axMapControl2.ClearLayers();
            }
            //设置鹰眼和主地图的坐标系统一致
            this.axMapControl2.SpatialReference = this.axMapControl1.SpatialReference;

            //获取鹰眼图层
            ILayer pLayer = GetLayerByName("县界_region");
            this.axMapControl2.AddLayer(pLayer);
            // 设置 MapControl 显示范围至数据的全局范围
            this.axMapControl2.Extent = this.axMapControl1.FullExtent;
            pEnv = this.axMapControl1.Extent as IEnvelope;
            DrawRectangle(pEnv);
            // 刷新鹰眼控件地图
            this.axMapControl2.Refresh();
        }

        //在鹰眼地图上面画矩形框
        private void DrawRectangle(IEnvelope pEnvelope)
        {
            //在绘制前，清除鹰眼中之前绘制的矩形框
            IGraphicsContainer pGraphicsContainer = this.axMapControl2.Map as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            pGraphicsContainer.DeleteAllElements();
            //得到当前视图范围
            IRectangleElement pRectangleElement = new RectangleElementClass();
            IElement pElement = pRectangleElement as IElement;
            pElement.Geometry = pEnvelope;
            //设置矩形框（实质为中间透明度面）
            IRgbColor pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pColor.Transparency = 255;
            ILineSymbol pOutLine = new SimpleLineSymbolClass();
            pOutLine.Width = 2;
            pOutLine.Color = pColor;

            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pColor = new RgbColorClass();
            pColor.Transparency = 0;
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutLine;
            //向鹰眼中添加矩形框
            IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;
            pFillShapeElement.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement((IElement)pFillShapeElement, 0);
            //刷新
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        //为鹰眼控件绘制鹰眼矩形框, MapControl1 添加 OnExtentUpdated 事件，此事件是在主 Map 控件的显示范围改变时响应，从而相应更新鹰眼控件中的矩形框
        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            ////得到当前视图范围
            //pEnv = (IEnvelope)e.newEnvelope;
            //DrawRectangle(pEnv);

            //创建鹰眼中线框
            IEnvelope pEnv = (IEnvelope)e.newEnvelope;
            IRectangleElement pRectangleEle = new RectangleElementClass();
            IElement pEle = pRectangleEle as IElement;
            pEle.Geometry = pEnv;

            //设置线框的边线对象，包括颜色和线宽
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;
            // 产生一个线符号对象 
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 2;
            pOutline.Color = pColor;

            // 设置颜色属性 
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 0;

            // 设置线框填充符号的属性 
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pEle as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;

            // 得到鹰眼视图中的图形元素容器
            IGraphicsContainer pGra = axMapControl2.Map as IGraphicsContainer;
            IActiveView pAv = pGra as IActiveView;
            // 在绘制前，清除 axMapControl2 中的任何图形元素 
            pGra.DeleteAllElements();
            // 鹰眼视图中添加线框
            pGra.AddElement((IElement)pFillShapeEle, 0);
            // 刷新鹰眼
            pAv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        //鹰眼与主 Map 控件互动
        //为鹰眼控件 MapControl2 添加 OnMouseDown 事件
        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (this.axMapControl2.Map.LayerCount != 0)
            {
                // 按下鼠标左键移动矩形框 
                if (e.button == 1)
                {
                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(e.mapX, e.mapY);
                    IEnvelope pEnvelope = this.axMapControl1.Extent;
                    pEnvelope.CenterAt(pPoint);
                    this.axMapControl1.Extent = pEnvelope;
                    this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                // 按下鼠标右键绘制矩形框 
                else if (e.button == 2)
                {
                    IEnvelope pEnvelop = this.axMapControl2.TrackRectangle();
                    this.axMapControl1.Extent = pEnvelop;
                    this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }

        //为鹰眼控件 MapControl2 添加 OnMouseMove 事件，主要实现按下鼠标左键的时候移动矩形框，同时也改变主的图控件的显示范围
        private void axMapControl2_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            // 如果不是左键按下就直接返回 
            if (e.button != 1) return;
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(e.mapX, e.mapY);
            this.axMapControl1.CenterAt(pPoint);
            this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
        
        private void trbChooseTime_Scroll(object sender, EventArgs e)
        {
            ////显示滑块当前位置代表的时间
            //string index = this.trbChooseTime.Value.ToString();
            //this.txbChooseTime.Text = index;
        }

        /// <summary>
        /// 获取RGB颜色
        /// </summary>
        /// <param name="intR">红</param>
        /// <param name="intG">绿</param>
        /// <param name="intB">蓝</param>
        /// <returns></returns>
        private IRgbColor GetRgbColor(int intR, int intG, int intB)
        {
            IRgbColor pRgbColor = null;
            if (intR < 0 || intR > 255 || intG < 0 || intG > 255 || intB < 0 || intB > 255)
            {
                return pRgbColor;
            }
            pRgbColor = new RgbColorClass();
            pRgbColor.Red = intR;
            pRgbColor.Green = intG;
            pRgbColor.Blue = intB;
            return pRgbColor;
        }

        /// <summary>
        /// 查询功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            object typeCheck = this.cbxType.SelectedItem;
            string nodeID = "1";
            int needCount = 60;

            double lngMax = 0;
            double lngMin = 0;
            double latMax = 0;
            double latMin = 0;

            double cellSize = 0.0002;
            double xSize = 0.00423;
            double ySize = 0;

            string unitStr = "";       //设置单位
            string location = this.txbSerch.Text.ToString();
            string loflag = "";

            /*---------------------确定每隔多长时间取点------------------------*/
                        
            if (location.IndexOf("云龙") > -1)   //匹配成功
            {
                loflag = "云龙区";
            }
            else if (location.IndexOf("上海") > -1 || location.IndexOf("交通") > -1 || location.IndexOf("交大") > -1)
            {
                loflag = "交大";
                cellSize = 0.0001;
            }
            else if (location.IndexOf("无锡") > -1 || location.IndexOf("博览") > -1 || location.IndexOf("太湖") > -1)
            {
                loflag = "无锡";
                nodeID = "8";
                cellSize = 0.00001;
                ySize = 0.000554;
                xSize = 0.000523;
            }
            else
            {
                loflag = "";
            }
            
            if (typeCheck != null && loflag != "")  //判断选择区域是否存在
            {

                //判断查询类型
                type = typeCheck.ToString();

                switch (type)
                {
                    case "温度":
                        type = "Temperature";
                        unitStr = "℃";
                        break;
                    case "湿度":
                        type = "Humidity";
                        unitStr = "%RH";
                        break;
                    case "SO2浓度":
                        type = "SO";
                        unitStr = "PPM";
                        break;
                    case "PM2.5":
                        type = "PM25";
                        unitStr = "μg/m³";
                        break;
                    case "PM10":
                        type = "PM10";
                        unitStr = "μg/m³";
                        break;
                }

                string date = this.dtpChooseDate.Value.ToString("yyyy-MM-dd");
                string timefrom = this.txbFromTime.Text.ToString();
                string timeto = this.txbToTime.Text.ToString();

                this.MessageLabel.Text = "正在查询, 请稍后";
                ClearLayer();   //清除上次查询结果

                IFeatureClass contourRegion = createGeodata.ReadFeatureClass(workspace_Path, "contourRegion");
                if (contourRegion == null)
                {
                    contourRegion = createGeodata.CreateFeatureClass(workspace_Path, "contourRegion", esriGeometryType.esriGeometryPoint, (int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
                }
                
                createGeodata.DeleteFeatureByIFeatureCursor(contourRegion);

                List<M_Point> locList = new List<M_Point>();
                using (SqlConnection DBConn = new SqlConnection(DBConstr))
                {
                    DBConn.Open();
                    using (SqlCommand cmd = DBConn.CreateCommand())
                    {
                        string where = "";

                        //将时间范围规整成sql语言需要的格式
                        if (0 <= int.Parse(timefrom) && int.Parse(timefrom) <= 9)
                        {
                            timefrom = "0" + timefrom + ":00:00";
                        }
                        else
                        {
                            timefrom = timefrom + ":00:00";
                        }
                        if (0 <= int.Parse(timeto) && int.Parse(timeto) <= 9)
                        {
                            timeto = "0" + timeto + ":00:00";
                        }
                        else
                        {
                            timeto = timeto + ":00:00";
                        }

                        where = " where Time >= '" + date + " " + timefrom + "' and Time <= '" + date + " " + timeto + "' and Longitude <> 0";
                        //where = " where Time >= '"+date+" 16:00:00' and Time <= '"+date+" 19:00:00'";
                        //where = " where convert(varchar(10), Time, 120) = @date ";
                        cmd.CommandText = "select ID,Longitude,Latitude,Time," + type + " from T_Location_" + nodeID + where + " order by Time desc";
                        //cmd.Parameters.Add(new SqlParameter("date", date));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                M_Point loc = new M_Point();
                                loc.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                                loc.longitude = reader.GetDouble(reader.GetOrdinal("Longitude"));
                                loc.latitude = reader.GetDouble(reader.GetOrdinal("Latitude"));
                                loc.time = reader.GetDateTime(reader.GetOrdinal("Time"));
                                loc.value = reader.GetString(reader.GetOrdinal(type));

                                try
                                {
                                    int checkInt = int.Parse(loc.value);
                                }
                                catch 
                                {
                                    continue;
                                }
                                    
                                locList.Add(loc);
                            }
                        }
                    }
                }
                if (locList.Count != 0)
                {
                    #region 绘制空气云图

                    int flag = 0;
                    IPoint pPoint = new PointClass();
                    IFeature feature = contourRegion.CreateFeature();

                    if (locList.Count > needCount)
                    {
                        int countInternal = locList.Count / needCount;
                        int count = 0;

                        for (int i = 0; i < locList.Count; i++)
                        {
                            count++;
                            if (count == countInternal)
                            {
                                pPoint.PutCoords(locList[i].longitude, locList[i].latitude);
                                feature = contourRegion.CreateFeature();
                                feature.Shape = (IGeometry)pPoint;
                                feature.set_Value(feature.Fields.FindField("samp"), locList[i].value);
                                feature.Store();

                                //获取最新最大范围
                                if (flag == 0)
                                {
                                    flag = 1;
                                    lngMax = locList[i].longitude;
                                    lngMin = locList[i].longitude;
                                    latMax = locList[i].latitude;
                                    latMin = locList[i].latitude;
                                    valueMax = locList[i].value;
                                    valueMin = locList[i].value;
                                }
                                else
                                {
                                    //获取最新最大范围
                                    if (locList[i].longitude >= lngMax) lngMax = locList[i].longitude;
                                    if (locList[i].longitude < lngMin) lngMin = locList[i].longitude;
                                    if (locList[i].latitude >= latMax) latMax = locList[i].latitude;
                                    if (locList[i].latitude < latMin) latMin = locList[i].latitude;

                                    //获取最新数值范围
                                    try
                                    {
                                        if (int.Parse(locList[i].value) >= int.Parse(valueMax)) valueMax = locList[i].value;
                                        if (int.Parse(locList[i].value) < int.Parse(valueMin)) valueMin = locList[i].value;
                                    }
                                    catch { }
                                }

                                count = 0;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < locList.Count; i++)
                        {
                            pPoint.PutCoords(locList[i].longitude, locList[i].latitude);
                            feature = contourRegion.CreateFeature();
                            feature.Shape = (IGeometry)pPoint;
                            feature.set_Value(feature.Fields.FindField("samp"), locList[i].value);
                            feature.Store();
                            //获取最新最大范围
                            if (flag == 0)
                            {
                                flag = 1;
                                lngMax = locList[i].longitude;
                                lngMin = locList[i].longitude;
                                latMax = locList[i].latitude;
                                latMin = locList[i].latitude;
                                valueMax = locList[i].value;
                                valueMin = locList[i].value;
                            }
                            else
                            {
                                //获取最新最大范围
                                if (locList[i].longitude >= lngMax) lngMax = locList[i].longitude;
                                if (locList[i].longitude < lngMin) lngMin = locList[i].longitude;
                                if (locList[i].latitude >= latMax) latMax = locList[i].latitude;
                                if (locList[i].latitude < latMin) latMin = locList[i].latitude;

                                //获取最新数值范围
                                try
                                {
                                    if (int.Parse(locList[i].value) >= int.Parse(valueMax)) valueMax = locList[i].value;
                                    if (int.Parse(locList[i].value) < int.Parse(valueMin)) valueMin = locList[i].value;
                                }
                                catch { }
                            }
                        }
                    }

                    this.lblMax.Text = valueMax + unitStr;
                    this.lblMin.Text = valueMin + unitStr;

                    IFeatureLayer contourLayer = new FeatureLayerClass();
                    contourLayer.FeatureClass = contourRegion;
                    contourLayer.Name = "M_Point1";
                    contourLayer.Visible = false;
                    axMapControl1.AddLayer(contourLayer);

                    //设置各个空气质量参数的等高线间隔
                    double interval;
                    if (type == "Temperature")
                    {
                        interval = 0.5;
                    }
                    else if (type == "Humidity")
                    {
                        interval = 2;
                    }
                    else if (type == "SO")
                    {
                        interval = 5;
                    }
                    else
                    {
                        interval = 5;
                    }
                    CreateRasterAndContour(1, contourLayer, cellSize, interval);

                    /*--------------------------跳转到边缘区域---------------------------*/

                    double xdif = lngMax - lngMin;
                    double ydif = latMax - latMin;

                    if (xdif > ydif * 1.5)
                    {
                        lngMax = lngMin + ydif + xSize;
                        lngMin += xSize;
                    }
                    else
                    {
                        latMax = latMin + xdif - ySize;
                        latMin += ySize;
                    }

                    IPolygon polygon = new PolygonClass();
                    IPointCollection points = new PolygonClass();
                    IPoint point = new PointClass();
                    
                    point.PutCoords(lngMin, latMin);
                    points.AddPoint(point);
                    point.PutCoords(lngMin, latMax);
                    points.AddPoint(point);
                    point.PutCoords(lngMax, latMin);
                    points.AddPoint(point);
                    point.PutCoords(lngMax, latMax);
                    points.AddPoint(point);

                    polygon = (IPolygon)points;

                    this.axMapControl1.ActiveView.Extent = polygon.Envelope;
                    this.axMapControl1.ActiveView.Refresh();


                    MessageLabel.Text = "就绪";

                    ShowChooseLayer();     //仅显示当前选中的时间段的一个渲染图层

                    #endregion
                }
                else
                {
                    if (loflag == "云龙区")
                    {
                        //#region 绘制空气云图

                        //int flag = 0;
                        //IPoint pPoint = new PointClass();
                        //IFeature feature = contourRegion.CreateFeature();

                        //if (locList.Count > needCount)
                        //{
                        //    int countInternal = locList.Count / needCount;
                        //    int count = 0;

                        //    for (int i = 0; i < locList.Count; i++)
                        //    {
                        //        count++;
                        //        if (count == countInternal)
                        //        {
                        //            pPoint.PutCoords(locList[i].longitude, locList[i].latitude);
                        //            feature = contourRegion.CreateFeature();
                        //            feature.Shape = (IGeometry)pPoint;
                        //            feature.set_Value(feature.Fields.FindField("samp"), locList[i].value);
                        //            feature.Store();

                        //            //获取最新最大范围
                        //            if (flag == 0)
                        //            {
                        //                flag = 1;
                        //                lngMax = locList[i].longitude;
                        //                lngMin = locList[i].longitude;
                        //                latMax = locList[i].latitude;
                        //                latMin = locList[i].latitude;
                        //                valueMax = locList[i].value;
                        //                valueMin = locList[i].value;
                        //            }
                        //            else
                        //            {
                        //                //获取最新最大范围
                        //                if (locList[i].longitude >= lngMax) lngMax = locList[i].longitude;
                        //                if (locList[i].longitude < lngMin) lngMin = locList[i].longitude;
                        //                if (locList[i].latitude >= latMax) latMax = locList[i].latitude;
                        //                if (locList[i].latitude < latMin) latMin = locList[i].latitude;

                        //                //获取最新数值范围
                        //                try
                        //                {
                        //                    if (int.Parse(locList[i].value) >= int.Parse(valueMax)) valueMax = locList[i].value;
                        //                    if (int.Parse(locList[i].value) < int.Parse(valueMin)) valueMin = locList[i].value;
                        //                }
                        //                catch { }
                        //            }

                        //            count = 0;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    for (int i = 0; i < locList.Count; i++)
                        //    {
                        //        pPoint.PutCoords(locList[i].longitude, locList[i].latitude);
                        //        feature = contourRegion.CreateFeature();
                        //        feature.Shape = (IGeometry)pPoint;
                        //        feature.set_Value(feature.Fields.FindField("samp"), locList[i].value);
                        //        feature.Store();
                        //        //获取最新最大范围
                        //        if (flag == 0)
                        //        {
                        //            flag = 1;
                        //            lngMax = locList[i].longitude;
                        //            lngMin = locList[i].longitude;
                        //            latMax = locList[i].latitude;
                        //            latMin = locList[i].latitude;
                        //            valueMax = locList[i].value;
                        //            valueMin = locList[i].value;
                        //        }
                        //        else
                        //        {
                        //            //获取最新最大范围
                        //            if (locList[i].longitude >= lngMax) lngMax = locList[i].longitude;
                        //            if (locList[i].longitude < lngMin) lngMin = locList[i].longitude;
                        //            if (locList[i].latitude >= latMax) latMax = locList[i].latitude;
                        //            if (locList[i].latitude < latMin) latMin = locList[i].latitude;

                        //            //获取最新数值范围
                        //            try
                        //            {
                        //                if (int.Parse(locList[i].value) >= int.Parse(valueMax)) valueMax = locList[i].value;
                        //                if (int.Parse(locList[i].value) < int.Parse(valueMin)) valueMin = locList[i].value;
                        //            }
                        //            catch { }
                        //        }
                        //    }
                        //}

                        //this.lblMax.Text = valueMax + unitStr;
                        //this.lblMin.Text = valueMin + unitStr;

                        //IFeatureLayer contourLayer = new FeatureLayerClass();
                        //contourLayer.FeatureClass = contourRegion;
                        //contourLayer.Name = "M_Point1";
                        //contourLayer.Visible = false;
                        //axMapControl1.AddLayer(contourLayer);

                        ////设置各个空气质量参数的等高线间隔
                        //double interval;
                        //if (type == "Temperature")
                        //{
                        //    interval = 0.5;
                        //}
                        //else if (type == "Humidity")
                        //{
                        //    interval = 2;
                        //}
                        //else if (type == "SO")
                        //{
                        //    interval = 5;
                        //}
                        //else
                        //{
                        //    interval = 5;
                        //}
                        //CreateRasterAndContour(1, contourLayer, cellSize, interval);

                        ///*--------------------------跳转到边缘区域---------------------------*/

                        //double xdif = lngMax - lngMin;
                        //double ydif = latMax - latMin;

                        //if (xdif > ydif * 1.5)
                        //{
                        //    lngMax = lngMin + ydif + xSize;
                        //    lngMin += xSize;
                        //}
                        //else
                        //{
                        //    latMax = latMin + xdif - ySize;
                        //    latMin += ySize;
                        //}

                        //IPolygon polygon = new PolygonClass();
                        //IPointCollection points = new PolygonClass();
                        //IPoint point = new PointClass();

                        //point.PutCoords(lngMin, latMin);
                        //points.AddPoint(point);
                        //point.PutCoords(lngMin, latMax);
                        //points.AddPoint(point);
                        //point.PutCoords(lngMax, latMin);
                        //points.AddPoint(point);
                        //point.PutCoords(lngMax, latMax);
                        //points.AddPoint(point);

                        //polygon = (IPolygon)points;

                        //this.axMapControl1.ActiveView.Extent = polygon.Envelope;
                        //this.axMapControl1.ActiveView.Refresh();


                        //MessageLabel.Text = "就绪";

                        //ShowChooseLayer();     //仅显示当前选中的时间段的一个渲染图层

                        //#endregion
                    }
                    else
                    {
                        MessageBox.Show("数据库中无此记录！");
                    }
                }
            }
            else 
            {
                MessageBox.Show("数据选择错误！");
            }
        }

        /// <summary>
        /// 创建栅格及等值线
        /// </summary>
        /// <param name="pFeatureLayer">需要操作的图层</param>
        /// <param name="dCellSize">栅格像元大小</param>
        /// <param name="interval">等值线的间距</param>
        private void CreateRasterAndContour(int index, IFeatureLayer pFeatureLayer, double dCellSize, double interval)
        {
            IInterpolationOp2 pInterpolationOp = new ESRI.ArcGIS.GeoAnalyst.RasterInterpolationOpClass();
            IGeoDataset pInputDataset = (IGeoDataset)pFeatureLayer.FeatureClass;
            IRasterRadius pRadius = new ESRI.ArcGIS.GeoAnalyst.RasterRadiusClass();
            object o1 = Type.Missing;
            pRadius.SetVariable(12, ref o1);
            //设置高程字段
            IFeatureClassDescriptor pFCDescriptor = new FeatureClassDescriptor() as IFeatureClassDescriptor;
            pFCDescriptor.Create(pFeatureLayer.FeatureClass, null, "samp");
            //double dCellSize = 0.1364;//像元大小
            object oCell = dCellSize;
            IRasterAnalysisEnvironment pEnv = (IRasterAnalysisEnvironment)pInterpolationOp;
            pEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref oCell);
            IRaster pOutRaster = null;
            object o2 = Type.Missing;
            pOutRaster = pInterpolationOp.IDW(pFCDescriptor as IGeoDataset, 2.5, pRadius, ref o2) as IRaster;
            IRasterLayer pOutRasterLayer = new RasterLayerClass();
            pOutRasterLayer.CreateFromRaster(pOutRaster);
            pOutRasterLayer.Name = "M_Raster" + index.ToString();
            pOutRasterLayer.Visible = false;
            axMapControl1.AddLayer(pOutRasterLayer);

            ISurfaceOp pSurfaceOp = new RasterSurfaceOp() as ISurfaceOp;
            object odbase = 0;

            IFeatureClass pOutLineFC = pSurfaceOp.Contour(pOutRaster as IGeoDataset, interval, ref odbase) as IFeatureClass;
            IFeatureLayer pFLayercontour = new FeatureLayerClass();
            pFLayercontour.FeatureClass = pOutLineFC;
            pFLayercontour.Name = "M_Contour" + index.ToString();
            //ILayer pLayercontour = pFLayercontour as ILayer;
            //axMapControl1.AddLayer(pLayercontour);
            pFLayercontour.Visible = false;
            axMapControl1.AddLayer(pFLayercontour);

            UsingRasterClassifyColorRampRenderer(index); //调用分级渲染的函数
        }

        /// <summary>
        /// 仅显示当前选中的时间段的一个渲染图层
        /// </summary>
        void ShowChooseLayer()
        {
            try
            {
                string index = this.txbFromTime.Text.ToString();
                IRasterLayer rLayer;
                IFeatureLayer fLayer;

                rLayer = GetLayerByName("M_Raster1") as IRasterLayer;
                rLayer.Visible = true;

                fLayer = GetLayerByName("M_Contour1") as IFeatureLayer;
                if (this.ckbShowContour.Checked == true)
                {
                    fLayer.Visible = true;
                }
                else
                {
                    fLayer.Visible = false;
                }

                fLayer = GetLayerByName("M_Point1") as IFeatureLayer;
                if (this.ckbShowPoint.Checked == true)
                {
                    fLayer.Visible = true;
                }
                else
                {
                    fLayer.Visible = false;
                }
            }
            catch { }

            //for (int i = 0; i < 10; i++)
            //{
            //    if (i != int.Parse(index))
            //    {
            //        rLayer = GetLayerByName("M_Raster" + i.ToString()) as IRasterLayer;
            //        rLayer.Visible = false;

            //        fLayer = GetLayerByName("M_Contour" + i.ToString()) as IFeatureLayer;
            //        fLayer.Visible = false;

            //        fLayer = GetLayerByName("M_Point" + i.ToString()) as IFeatureLayer;
            //        fLayer.Visible = false;
            //    }
            //    else
            //    {
            //        rLayer = GetLayerByName("M_Raster" + i.ToString()) as IRasterLayer;
            //        rLayer.Visible = true;

            //        fLayer = GetLayerByName("M_Contour" + i.ToString()) as IFeatureLayer;
            //        fLayer.Visible = true;

            //        fLayer = GetLayerByName("M_Point" + i.ToString()) as IFeatureLayer;
            //        fLayer.Visible = true;
            //        break;
            //    }
            //}

            //rLayer = GetLayerByName("M_Raster1") as IRasterLayer;
            //rLayer.Visible = true;

            //fLayer = GetLayerByName("M_Contour1") as IFeatureLayer;
            //fLayer.Visible = true;

            //fLayer = GetLayerByName("M_Point1") as IFeatureLayer;
            //fLayer.Visible = true;

            //axMapControl1.Refresh();
        }

        /// <summary>
        /// 清除上一次查询的结果
        /// </summary>
        void ClearLayer()
        {
            int index;
            //for (int i = 0; i < 10; i++)
            //{
            //    index = GetLayerIDByName("M_Raster" + i.ToString());
            //    if (index != 32767)
            //    {
            //        this.axMapControl1.DeleteLayer(index);
            //    }
            //    index = GetLayerIDByName("M_Contour" + i.ToString());
            //    if (index != 32767)
            //    {
            //        this.axMapControl1.DeleteLayer(index);
            //    }
            //    index = GetLayerIDByName("M_Point" + i.ToString());
            //    if (index != 32767)
            //    {
            //        this.axMapControl1.DeleteLayer(index);
            //    }
            //}
            index = GetLayerIDByName("M_Raster1");
            if (index != 32767)
            {
                this.axMapControl1.DeleteLayer(index);
            }
            index = GetLayerIDByName("M_Contour1");
            if (index != 32767)
            {
                this.axMapControl1.DeleteLayer(index);
            }
            index = GetLayerIDByName("M_Point1");
            if (index != 32767)
            {
                this.axMapControl1.DeleteLayer(index);
            }
            index = GetLayerIDByName("M_Receive");
            if (index != 32767)
            {
                this.axMapControl1.DeleteLayer(index);
            }
        }

        /// <summary>
        /// 按Raster的名称获取Layer的ID值
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public int GetLayerIDByName(string layerName)
        {
            ILayer pRLayer = null;
            int i;

            for (i = 0; i <= axMapControl1.LayerCount - 1; i++)
            {
                pRLayer = axMapControl1.get_Layer(i);
                if (pRLayer != null && pRLayer.Name == layerName)
                {
                    return i;
                }
            }

            return 32767;
        }

        /// <summary>
        /// 按Raster的名称获取Layer
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public ILayer GetLayerByName(string layerName)
        {
            ILayer pRLayer = null;

            for (int i = 0; i <= axMapControl1.LayerCount - 1; i++)
            {
                pRLayer = axMapControl1.get_Layer(i);
                if (pRLayer != null && pRLayer.Name == layerName)
                {
                    break;
                }
            }

            return pRLayer;
        }

        /// <summary>
        /// 分级渲染函数
        /// </summary>
        private void UsingRasterClassifyColorRampRenderer(int index)
        {
            //get { raster input from layer
            IRasterLayer pRLayer;
            pRLayer = GetLayerByName("M_Raster"+index.ToString()) as IRasterLayer;
            IRaster pRaster;
            pRaster = pRLayer.Raster;

            int max = int.Parse(valueMax);
            int min = int.Parse(valueMin);

            //IRgbColor fromColor = new RgbColorClass();  //最低程度颜色
            //IRgbColor toColor = new RgbColorClass();    //最高程度颜色

            //Define the from and to colors for the color ramp.
            //switch (type)
            //{

            //    //valueMin = loc.value;

            //    case "Temperature":
            //        if (max >= 22 && max < 26)
            //        {
            //            fromColor.Red = 76;
            //            fromColor.Green = 255;
            //            fromColor.Blue = 127;
            //        }
            //        else if (max >= 26 && max < 28)
            //        {
            //            fromColor.Red = 209;
            //            fromColor.Green = 237;
            //            fromColor.Blue = 69;
            //        }
            //        else if (max >= 28 && max < 30)
            //        {
            //            fromColor.Red = 255;
            //            fromColor.Green = 236;
            //            fromColor.Blue = 94;
            //        }
            //        else if(max >= 30)
            //        {
            //            fromColor.Red = 255;
            //            fromColor.Green = 54;
            //            fromColor.Blue = 54;
            //        }

            //        if (min >= 22 && min < 26)
            //        {
            //            toColor.Red = 76;
            //            toColor.Green = 255;
            //            toColor.Blue = 127;
            //        }
            //        else if (min >= 26 && min < 28)
            //        {
            //            toColor.Red = 209;
            //            toColor.Green = 237;
            //            toColor.Blue = 69;
            //        }
            //        else if (min >= 28 && min < 30)
            //        {
            //            toColor.Red = 255;
            //            toColor.Green = 236;
            //            toColor.Blue = 94;
            //        }
            //        else if (min >= 30)
            //        {
            //            toColor.Red = 255;
            //            toColor.Green = 54;
            //            toColor.Blue = 54;
            //        }
            //        break;
            //    case "Humidity":
            //        if (max >= 20 && max < 30)
            //        {
            //            fromColor.Red = 255;
            //            fromColor.Green = 54;
            //            fromColor.Blue = 54;
            //        }
            //        else if (max >= 30 && max < 40)
            //        {
            //            fromColor.Red = 255;
            //            fromColor.Green = 236;
            //            fromColor.Blue = 94;
            //        }
            //        else if (max >= 40 && max < 50)
            //        {
            //            fromColor.Red = 237;
            //            fromColor.Green = 184;
            //            fromColor.Blue = 69;
            //        }
            //        else if(max >= 50 && max < 60)
            //        {
            //            fromColor.Red = 209;
            //            fromColor.Green = 237;
            //            fromColor.Blue = 69;
            //        }
            //        else if (max >= 60)
            //        {
            //            fromColor.Red = 76;
            //            fromColor.Green = 255;
            //            fromColor.Blue = 127;
            //        }

            //        if (min >= 20 && min < 30)
            //        {
            //            toColor.Red = 255;
            //            toColor.Green = 54;
            //            toColor.Blue = 54;
            //        }
            //        else if (min >= 30 && min < 40)
            //        {
            //            toColor.Red = 255;
            //            toColor.Green = 236;
            //            toColor.Blue = 94;
            //        }
            //        else if (min >= 40 && min < 50)
            //        {
            //            toColor.Red = 237;
            //            toColor.Green = 184;
            //            toColor.Blue = 69;
            //        }
            //        else if (min >= 50 && min < 60)
            //        {
            //            toColor.Red = 209;
            //            toColor.Green = 237;
            //            toColor.Blue = 69;
            //        }
            //        else if (min >= 60)
            //        {
            //            toColor.Red = 76;
            //            toColor.Green = 255;
            //            toColor.Blue = 127;
            //        }
            //        break;
            //    case "SO":
            //        if (max < 30)
            //        {
            //            fromColor.Red = 255;
            //            fromColor.Green = 54;
            //            fromColor.Blue = 54;
            //        }
            //        else if (max >= 30 && max < 45)
            //        {
            //            fromColor.Red = 235;
            //            fromColor.Green = 103;
            //            fromColor.Blue = 70;
            //        }
            //        else if (max >= 45 && max < 60)
            //        {
            //            fromColor.Red = 237;
            //            fromColor.Green = 184;
            //            fromColor.Blue = 69;
            //        }
            //        else if(max >= 60 && max < 75)
            //        {
            //            fromColor.Red = 255;
            //            fromColor.Green = 236;
            //            fromColor.Blue = 94;
            //        }
            //        else if (max >= 75)
            //        {
            //            fromColor.Red = 76;
            //            fromColor.Green = 255;
            //            fromColor.Blue = 127;
            //        }

            //        if (min < 30)
            //        {
            //            toColor.Red = 255;
            //            toColor.Green = 54;
            //            toColor.Blue = 54;
            //        }
            //        else if (min >= 30 && min < 45)
            //        {
            //            toColor.Red = 235;
            //            toColor.Green = 103;
            //            toColor.Blue = 70;
            //        }
            //        else if (min >= 45 && min < 60)
            //        {
            //            toColor.Red = 237;
            //            toColor.Green = 184;
            //            toColor.Blue = 69;
            //        }
            //        else if (min >= 60 && min < 75)
            //        {
            //            toColor.Red = 255;
            //            toColor.Green = 236;
            //            toColor.Blue = 94;
            //        }
            //        else if (min >= 75)
            //        {
            //            toColor.Red = 76;
            //            toColor.Green = 255;
            //            toColor.Blue = 127;
            //        }
            //        break;
            //    case "PM25":
            //        fromColor.Red = 76;
            //        fromColor.Green = 255;
            //        fromColor.Blue = 127;

            //        toColor.Red = 255;
            //        toColor.Green = 54;
            //        toColor.Blue = 54;

            //        //if (max < 50)
            //        //{
            //        //    fromColor.Red = 76;
            //        //    fromColor.Green = 255;
            //        //    fromColor.Blue = 127;
            //        //}
            //        //else if (max >= 50 && max < 70)
            //        //{
            //        //    fromColor.Red = 209;
            //        //    fromColor.Green = 237;
            //        //    fromColor.Blue = 69;
            //        //}
            //        //else if (max >= 70 && max < 90)
            //        //{
            //        //    fromColor.Red = 255;
            //        //    fromColor.Green = 236;
            //        //    fromColor.Blue = 94;
            //        //}
            //        //else if(max >= 90)
            //        //{
            //        //    fromColor.Red = 255;
            //        //    fromColor.Green = 54;
            //        //    fromColor.Blue = 54;  
            //        //}

            //        //if (min < 50)
            //        //{
            //        //    toColor.Red = 76;
            //        //    toColor.Green = 255;
            //        //    toColor.Blue = 127;
            //        //}
            //        //else if (min >= 50 && min < 70)
            //        //{
            //        //    toColor.Red = 209;
            //        //    toColor.Green = 237;
            //        //    toColor.Blue = 69;
            //        //}
            //        //else if (min >= 70 && min < 90)
            //        //{
            //        //    toColor.Red = 255;
            //        //    toColor.Green = 236;
            //        //    toColor.Blue = 94;
            //        //}
            //        //else if (min >= 90)
            //        //{
            //        //    toColor.Red = 255;
            //        //    toColor.Green = 54;
            //        //    toColor.Blue = 54;
            //        //}
            //        break;
            //    case "PM10":
            //        fromColor.Red = 76;
            //        fromColor.Green = 255;
            //        fromColor.Blue = 127;

            //        toColor.Red = 255;
            //        toColor.Green = 54;
            //        toColor.Blue = 54;
            //        //if (max < 50)
            //        //{
            //        //    fromColor.Red = 76;
            //        //    fromColor.Green = 255;
            //        //    fromColor.Blue = 127;
            //        //}
            //        //else if (max >= 50 && max < 70)
            //        //{
            //        //    fromColor.Red = 209;
            //        //    fromColor.Green = 237;
            //        //    fromColor.Blue = 69;
            //        //}
            //        //else if (max >= 70 && max < 90)
            //        //{
            //        //    fromColor.Red = 255;
            //        //    fromColor.Green = 236;
            //        //    fromColor.Blue = 94;
            //        //}
            //        //else if(max >= 90)
            //        //{
            //        //    fromColor.Red = 255;
            //        //    fromColor.Green = 54;
            //        //    fromColor.Blue = 54;  
            //        //}

            //        //if (min < 50)
            //        //{
            //        //    toColor.Red = 76;
            //        //    toColor.Green = 255;
            //        //    toColor.Blue = 127;
            //        //}
            //        //else if (min >= 50 && min < 70)
            //        //{
            //        //    toColor.Red = 209;
            //        //    toColor.Green = 237;
            //        //    toColor.Blue = 69;
            //        //}
            //        //else if (min >= 70 && min < 90)
            //        //{
            //        //    toColor.Red = 255;
            //        //    toColor.Green = 236;
            //        //    toColor.Blue = 94;
            //        //}
            //        //else if (min >= 90)
            //        //{
            //        //    toColor.Red = 255;
            //        //    toColor.Green = 54;
            //        //    toColor.Blue = 54;
            //        //}
            //        break;
            //}

            IRgbColor fromColor = new RgbColorClass();  //最低程度颜色
            fromColor.Red = 76;
            fromColor.Green = 255;
            fromColor.Blue = 127;
            IRgbColor toColor = new RgbColorClass();    //最高程度颜色
            toColor.Red = 255;
            toColor.Green = 54;
            toColor.Blue = 54;


            //Create the color ramp.
            IAlgorithmicColorRamp colorRamp = new AlgorithmicColorRampClass();
            colorRamp.Size = 255;
            colorRamp.FromColor = fromColor;
            colorRamp.ToColor = toColor;
            bool createColorRamp;
            colorRamp.CreateRamp(out createColorRamp);
            //Create a stretch renderer.
            IRasterStretchColorRampRenderer stretchRenderer = new RasterStretchColorRampRendererClass();
            IRasterRenderer rasterRenderer = (IRasterRenderer)stretchRenderer;
            //Set the renderer properties.
            //IRaster raster = rasterDataset.CreateDefaultRaster();
            rasterRenderer.Raster = pRaster;
            rasterRenderer.Update();
            stretchRenderer.BandIndex = 0;
            stretchRenderer.ColorRamp = colorRamp;
            //Set the stretch type.
            IRasterStretch stretchType = (IRasterStretch)rasterRenderer;
            stretchType.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations;
            stretchType.StandardDeviationsParam = 2;
            //return rasterRenderer;

            pRLayer.Renderer = (IRasterRenderer)rasterRenderer;

            //将图层设置为透明
            ILayerEffects pLayerEffects = pRLayer as ILayerEffects;
            pLayerEffects.Transparency = short.Parse(this.txbTransp.Text.ToString());

            //axMapControl1.ActiveView.Refresh();
            //this.axMapControl1.Update();
        }

        /// <summary>
        /// 搜索功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchName = this.txbSerch.Text.Trim();
            bool flag = false;

            IGeoFeatureLayer tGeoFeatureLayer;
            IFeatureCursor featureCursor;
            IQueryFilter queryFilter = new QueryFilterClass();

            //清除上次查询结果
            this.axMapControl1.Map.ClearSelection();

            for (int i = 0; i < this.axMapControl1.LayerCount; i++) //搜索当前所有图层
            {
                tGeoFeatureLayer = this.axMapControl1.Map.get_Layer(i) as IGeoFeatureLayer;

                if (tGeoFeatureLayer == null)
                {
                    continue;
                }

                IAnnotationExpressionParser tAnnoExpParse = null;

                //获取当前图层的标注集
                IAnnotateLayerPropertiesCollection tAnnotateLayerPropertiesCollection = tGeoFeatureLayer.AnnotationProperties;
                IAnnotateLayerProperties tAnnotateLayerProperties = null;
                IElementCollection tElementCollection = null;

                if (tAnnotateLayerPropertiesCollection.Count > 0)
                {
                    tAnnotateLayerPropertiesCollection.QueryItem(0, out tAnnotateLayerProperties, out tElementCollection, out tElementCollection);
                    ILabelEngineLayerProperties tLabelEngineLayerProperties = tAnnotateLayerProperties as ILabelEngineLayerProperties;
                    //获取标注解析类
                    IAnnotationExpressionEngine tAnnoExpEngine = tLabelEngineLayerProperties.ExpressionParser;
                    if (tLabelEngineLayerProperties.IsExpressionSimple == true)
                    {
                        tAnnoExpParse = tAnnoExpEngine.SetExpression("", tLabelEngineLayerProperties.Expression);
                    }
                    else
                    {
                        tAnnoExpParse = tAnnoExpEngine.SetCode(tLabelEngineLayerProperties.Expression, "");
                    }

                    //在当前图层的FeatureClass中搜索与目标关键字一致的要素并转向
                    IFeatureClass featureClass = tGeoFeatureLayer.FeatureClass; 
                    IFeature feature = null;
                    queryFilter.WhereClause = "";
                    featureCursor = featureClass.Search(queryFilter, true);
                    
                    while(true)     //轮询featureClass
                    {
                        feature = featureCursor.NextFeature();
                        if (feature == null) break;
                        string labelValue = tAnnoExpParse.FindLabel(feature);
                        if (labelValue.IndexOf(searchName) > -1)   //匹配成功
                        {
                            flag = true;
                            
                            //this.axMapControl1.FlashShape(feature.Shape, 2, 300, Type.Missing);

                            //缩放至选择
                            IEnvelope pEnvelope = new EnvelopeClass();
                            pEnvelope.Union(feature.Extent);

                            string check = tGeoFeatureLayer.Name.Substring(0, 1);
                            if (check == "县" || check == "地")
                            {
                                pEnvelope.Expand(0.3, 0.3, false);
                            }
                            else if (check == "镇" || check == "乡")
                            {
                                pEnvelope.Expand(0.01, 0.01, false);
                            }
                            this.axMapControl1.ActiveView.Extent = pEnvelope;

                            //IPolygon polygon = new PolygonClass();
                            //IPointCollection points = new PolygonClass();
                            //IPoint point = new PointClass();
                            //point.PutCoords(117.2607272941, 34.2613238191);
                            //points.AddPoint(point);
                            //point.PutCoords(117.7607272941, 34.2613238191);
                            //points.AddPoint(point);
                            //point.PutCoords(117.2607272941, 34.7613238191);
                            //points.AddPoint(point);
                            //point.PutCoords(117.7607272941, 34.7613238191);
                            //points.AddPoint(point);

                            //polygon = (IPolygon)points;

                            //this.axMapControl1.ActiveView.Extent = polygon.Envelope;

                            this.axMapControl1.ActiveView.Refresh();

                            break;
                        }
                    }
                }
                if (flag == true)
                {
                    break;
                }
            }
            if (flag == false)
            {
                //没有得到pFeature的提示
                MessageBox.Show("没有找到名为" + searchName + "的地区", "提示");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearLayer();

            axMapControl1.Refresh();
        }
        
        //显示与隐藏等值线
        private void ckbShowContour_CheckedChanged(object sender, EventArgs e)
        {
            IFeatureLayer pFLayercontour = GetLayerByName("M_Contour1") as IFeatureLayer;

            if (this.ckbShowContour.Checked == true)
            {
                pFLayercontour.Visible = true;
            }
            else
            {
                pFLayercontour.Visible = false;
            }

            //等值线图层刷新
            this.axMapControl1.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, pFLayercontour, null);
        }

        //显示与隐藏节点Point
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            IFeatureLayer pFLayerPoint = GetLayerByName("M_Point1") as IFeatureLayer;

            if (this.ckbShowPoint.Checked == true)
            {
                pFLayerPoint.Visible = true;
            }
            else
            {
                pFLayerPoint.Visible = false;
            }

            //点集图层刷新
            this.axMapControl1.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, pFLayerPoint, null);
        }

        //设置渐变色
        private void pnlColorBar_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color FColor = Color.FromArgb(76, 255, 127);
            Color TColor = Color.FromArgb(255, 54, 54);

            Brush b = new LinearGradientBrush(this.pnlColorBar.ClientRectangle, FColor, TColor, LinearGradientMode.Horizontal);
            g.FillRectangle(b, this.ClientRectangle);
        }

        //private void axMapControl1_OnFullExtentUpdated_1(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
        //{
        //    //获取鹰眼图层
        //    this.axMapControl2.AddLayer(this.GetOverviewLayer(this.axMapControl1.Map));
        //    // 设置 MapControl 显示范围至数据的全局范围
        //    this.axMapControl2.Extent = this.axMapControl1.FullExtent;
        //    // 刷新鹰眼控件地图
        //    this.axMapControl2.Refresh();
        //}

        /// <summary>
        /// 向ListBox中添加新记录
        /// </summary>
        /// <param name="input"></param>
        //public void putList(string input)
        //{
        //    //this.textBox1.Text = input;
        //    this.Invoke(new MethodInvoker(delegate()
        //    {
        //        this.lsbStatus.Items.Add(input);	//listBox1中填加目录名

        //        //自动滚动到底部
        //        lsbStatus.TopIndex = lsbStatus.Items.Count - (int)(lsbStatus.Height / lsbStatus.ItemHeight);
        //    }));
        //}

        double x = 0;
        double y = 0;
        int rFlag = 0;
        IFeatureClass receiveRegion;
        IPoint pPoint = new PointClass();

        private void tmrReceive_Tick(object sender, EventArgs e)
        {
            string nodeID = "8"; 
            IFeatureLayer rLayer;

            using (SqlConnection DBConn = new SqlConnection(DBConstr))
            {
                DBConn.Open();
                using (SqlCommand cmd = DBConn.CreateCommand())
                {
                    string where = "";

                    where = " where Time > '" + timeUpdate + "' and Time <> '" + timeUpdate + "' ";
                    //where = " where Time >= '"+date+" 16:00:00' and Time <= '"+date+" 19:00:00'";
                    //where = " where convert(varchar(10), Time, 120) = @date ";
                    cmd.CommandText = "select ID,Longitude,Latitude,Time,Temperature,Humidity,SO,PM25,PM10 from T_Location_" + nodeID + where + " order by Time desc";
                    //cmd.Parameters.Add(new SqlParameter("date", date));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //int flag = 0;
                        //DateTime timeMax = dtUpdate;
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

                            //string locStr = "NO." + nodeID + " Lng:" + loc.longitude + " Lat:" + loc.latitude +
                            //    " 温度：" + loc.temperature + " 湿度：" + loc.humidity + " SO浓度：" + loc.so + " PM2.5浓度：" + loc.pm25 +
                            //    " PM10浓度：" + loc.pm10;
                            //putList(loc.time + " " + locStr);

                            //在界面中显示实时节点数据
                            this.lblLng.Text = loc.longitude.ToString();
                            this.lblLat.Text = loc.latitude.ToString();
                            this.lblTem.Text = loc.temperature;
                            this.lblHum.Text = loc.humidity;
                            this.lblSO2.Text = loc.so;
                            this.lblPM25S.Text = loc.pm25;
                            this.lblPM10.Text = loc.pm10;

                            //更新时间方便下次调用
                            timeUpdate = loc.time.ToString("yyyy-MM-dd HH:mm:ss");

                            x = loc.longitude;
                            y = loc.latitude;
                        }
                    }
                }
            }

            if (x != 0)
            {
                if (rFlag == 0)     //第一次进入时
                {

                    //创建图层region
                    receiveRegion = createGeodata.ReadFeatureClass(workspace_Path, "receiveRegion");
                    if (receiveRegion == null)
                    {
                        receiveRegion = createGeodata.CreateFeatureClass(workspace_Path, "receiveRegion", esriGeometryType.esriGeometryPoint, (int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
                    }

                    createGeodata.DeleteFeatureByIFeatureCursor(receiveRegion);
                    rLayer = new FeatureLayerClass();

                    //添加图层
                    rLayer.FeatureClass = receiveRegion;
                    rLayer.Name = "M_Receive";
                    axMapControl1.AddLayer(rLayer);


                    //地图转向到最新点
                    IPolygon polygon = new PolygonClass();
                    IPointCollection points = new PolygonClass();
                    IPoint epoint = new PointClass();
                    epoint.PutCoords(x - 0.01, y - 0.01);
                    points.AddPoint(epoint);
                    epoint.PutCoords(x - 0.01, y + 0.01);
                    points.AddPoint(epoint);
                    epoint.PutCoords(x + 0.01, y - 0.01);
                    points.AddPoint(epoint);
                    epoint.PutCoords(x + 0.01, y + 0.01);
                    points.AddPoint(epoint);

                    polygon = (IPolygon)points;

                    this.axMapControl1.ActiveView.Extent = polygon.Envelope;

                    this.axMapControl1.ActiveView.Refresh();

                    rFlag = 1;
                }
                else
                {
                    rLayer = GetLayerByName("M_Receive") as IFeatureLayer;
                    rLayer.FeatureClass = receiveRegion;
                }

                //添加此点至featureclass
                IFeature feature = receiveRegion.CreateFeature();
                pPoint.PutCoords(x, y);
                feature = receiveRegion.CreateFeature();
                feature.Shape = (IGeometry)pPoint;
                feature.Store();

                //新增实时点闪烁
                this.axMapControl1.FlashShape(feature.Shape, 2, 300, Type.Missing);

                //Map点集图层刷新
                this.axMapControl1.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, rLayer, null);
            }
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            ////屏幕坐标点转化为地图坐标点
            //pPointPt = (this.axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

            //if (e.button == 1)
            //{
            //    IActiveView pActiveView = this.axMapControl1.ActiveView;
            //    IEnvelope pEnvelope = new EnvelopeClass();

            //    switch (pMouseOperate)
            //    {
            //        case "Pan":
            //            this.axMapControl1.Pan();
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //else if (e.button == 2)
            //{
            //    pMouseOperate = "";
            //    this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            //}
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 2)
            {
                ClearLayer();
                axMapControl1.Refresh();

                dtUpdate = DateTime.Now;
                timeUpdate = dtUpdate.ToString();
                //putList(timeUpdate + " 正在实时接收节点数据...");
                timeUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.tmrReceive.Enabled = true;

            }
            else if (this.tabControl1.SelectedIndex == 0)
            {
                this.tmrReceive.Enabled = false;
            }
        }

        private void tkbTransparency_Scroll(object sender, EventArgs e)
        {
            this.txbTransp.Text = this.tkbTransparency.Value.ToString();

            try
            {
                IRasterLayer pRLayer;
                pRLayer = GetLayerByName("M_Raster1") as IRasterLayer;

                //将图层设置透明
                ILayerEffects pLayerEffects = pRLayer as ILayerEffects;
                pLayerEffects.Transparency = (short)this.tkbTransparency.Value;

                //栅格图层刷新
                this.axMapControl1.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, pRLayer, null);
            }
            catch { }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.tkbTransparency.Value = defaultTrans;
            this.txbTransp.Text = defaultTrans.ToString();
        }

        private void cmbChooseNodeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbChooseNodeID.SelectedIndex == 0)
            {
                this.lblCarInfo.Text = "苏CQD923";
                this.lblIncharge.Text = "毛亚青";
                this.lblPhone.Text = "18796253331";
            }
            else if (this.cmbChooseNodeID.SelectedIndex == 1)
            {
                this.lblCarInfo.Text = "苏CQD925";
                this.lblIncharge.Text = "王翔";
                this.lblPhone.Text = "18796256166";
            }
        }
    }
}
