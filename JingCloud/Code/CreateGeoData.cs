using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;

namespace JingCloud
{
    class CreateGeoData
    {
        IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
        ISpatialReferenceFactory spatialRefFactory = new ESRI.ArcGIS.Geometry.SpatialReferenceEnvironment();
        ISpatialReference spatialRef;

        #region 在内存中创建要素类
        /// <summary>
        /// 在内存中创建要素类
        /// </summary>
        /// <returns></returns>
        public IFeatureClass MemoryFeatureClass()
        {
            AccessGeoData accData = new AccessGeoData();
            IFeatureWorkspace pFeaWorkspace = accData.FeatureWorkspace();
            IFields fields = new FieldsClass();
            IFieldsEdit fedits = (IFieldsEdit)fields;
            IField objfield = new FieldClass();
            IFieldEdit objEdit = (IFieldEdit)objfield;
            objEdit.Name_2 = "OBJECTID";
            objEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fedits.AddField(objEdit);
            IField Spfield = new FieldClass();
            IFieldEdit fEdit = (IFieldEdit)Spfield;
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            pGeoDefEdit.SpatialReference_2 = spatialRef;
            fEdit.Name_2 = "Shape";
            fEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            fEdit.GeometryDef_2 = pGeoDef;
            fedits.AddField(Spfield);
            IField fieldZ = new FieldClass();
            IFieldEdit EditZ = (IFieldEdit)fieldZ;
            EditZ.Name_2 = "Z";
            EditZ.Type_2 = esriFieldType.esriFieldTypeDouble;
            fedits.AddField(EditZ);
            IFeatureClass pFeaClass = pFeaWorkspace.CreateFeatureClass("FeaturePoints", fields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            return pFeaClass;
        }
        #endregion

        #region//在地理数据库中创建要素类
        /// <summary>
        /// 在地理数据库中创建要素类
        /// </summary>
        /// <param name="workspacePath">工作空间路径</param>
        /// <param name="feaclassName">所创建要素类名称</param>
        /// <param name="GeometeyType">枚举要素类类型: esriGeimetryType(</param>
        /// <param name="gcsType">地理空间坐标系类型,格式如: (int)esriSRGeoCS3Type.esriSRGeoCS_Xian1980</param>
        /// <returns></returns>
        public IFeatureClass CreateFeatureClass(string workspacePath, string feaclassName, esriGeometryType GeometeyType, int gcsType)
        {
            AccessGeoData accData = new AccessGeoData();
            IFeatureWorkspace pFeaWorkspace = accData.OpenFeatureWorkspace(workspacePath);
            spatialRef = spatialRefFactory.CreateGeographicCoordinateSystem(gcsType);
            //字段编辑：包括ObjectID 与 Shape
            IFields fields = new FieldsClass();
            IFieldsEdit fedits = (IFieldsEdit)fields;
            //构建唯一标识符字段
            IField objfield = new FieldClass();
            IFieldEdit objEdit = (IFieldEdit)objfield;
            objEdit.Name_2 = "OBJECTID";
            objEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fedits.AddField(objEdit);
            //构建shape
            IField Spfield = new FieldClass();
            IFieldEdit fEdit = (IFieldEdit)Spfield;
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.GeometryType_2 = GeometeyType;
            pGeoDefEdit.SpatialReference_2 = spatialRef;
            fEdit.Name_2 = "Shape";
            fEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            fEdit.GeometryDef_2 = pGeoDef;
            fedits.AddField(Spfield);
            //附加一个可编辑字段，存放属性值
            IField fieldZ = new FieldClass();
            IFieldEdit EditZ = (IFieldEdit)fieldZ;
            EditZ.Name_2 = "Value";
            EditZ.Type_2 = esriFieldType.esriFieldTypeDouble;
            fedits.AddField(EditZ);
            //若有其他需求，在此处添加字段，参加Value字段添加方式
            IFeatureClass pFeaClass = pFeaWorkspace.CreateFeatureClass(feaclassName, fields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            return pFeaClass;
        }
        //重载
        public IFeatureClass CreateFeatureClass(string workspacePath, string feaclassName, esriGeometryType GeometeyType, ISpatialReference spatialRef)
        {
            AccessGeoData accData = new AccessGeoData();
            IFeatureWorkspace pFeaWorkspace = accData.OpenFeatureWorkspace(workspacePath);
            //字段编辑：包括ObjectID 与 Shape
            IFields fields = new FieldsClass();
            IFieldsEdit fedits = (IFieldsEdit)fields;
            //构建唯一标识符字段
            IField objfield = new FieldClass();
            IFieldEdit objEdit = (IFieldEdit)objfield;
            objEdit.Name_2 = "OBJECTID";
            objEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fedits.AddField(objEdit);
            //构建shape
            IField Spfield = new FieldClass();
            IFieldEdit fEdit = (IFieldEdit)Spfield;
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
            pGeoDefEdit.GeometryType_2 = GeometeyType;
            pGeoDefEdit.SpatialReference_2 = spatialRef;
            fEdit.Name_2 = "Shape";
            fEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            fEdit.GeometryDef_2 = pGeoDef;
            fedits.AddField(Spfield);
            //附加一个可编辑字段，存放属性值
            IField fieldZ = new FieldClass();
            IFieldEdit EditZ = (IFieldEdit)fieldZ;
            EditZ.Name_2 = "Value";
            EditZ.Type_2 = esriFieldType.esriFieldTypeDouble;
            fedits.AddField(EditZ);
            //若有其他需求，在此处添加字段，参加Value字段添加方式
            IFeatureClass pFeaClass = pFeaWorkspace.CreateFeatureClass(feaclassName, fields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            return pFeaClass;
        }

        public IFeatureClass ReadFeatureClass(string workspacePath, string feaclassName)
        {
            AccessGeoData accData = new AccessGeoData();
            IFeatureWorkspace pFeaWorkspace = accData.OpenFeatureWorkspace(workspacePath);
            //若有其他需求，在此处添加字段，参加Value字段添加方式
            IFeatureClass pFeaClass = null;
            try
            {
                pFeaClass = pFeaWorkspace.OpenFeatureClass(feaclassName);
            }
            catch (Exception)
            {

                //throw;
            }

            return pFeaClass;
        }

        //删除图层的所有元素
        public void DeleteFeatureByIFeatureCursor(IFeatureClass pFeatureclass)
        {
            IQueryFilter pQueryFilter = new QueryFilterClass();
            //pQueryFilter.WhereClause = strWhereClause;
            IFeatureCursor pFeatureCursor = pFeatureclass.Update(pQueryFilter, false);
            IFeature pFeature = pFeatureCursor.NextFeature();
            while (pFeature != null)
            {
                pFeatureCursor.DeleteFeature();
                pFeature = pFeatureCursor.NextFeature();
            }
        }
        #endregion



        #region 在文件中创建ShapeFile
        /// <summary>
        /// 在文件中创建ShapeFile
        /// </summary>
        /// <param name="shpName"></param>
        /// <param name="shapeFieldName"></param>
        /// <param name="featureType"></param>
        /// <returns></returns>
        public IFeatureClass CreateShapefile(string path, string shpName, string[] strFldArray, ISpatialReference spatialRef)
        {
            if (File.Exists(shpName))
            {
                MessageBox.Show("文件已存在");
                return null;
            }
            else
            {
                //确定Shapefile数据格式工作区
                IFeatureWorkspace pFeatWorkSpa;
                IWorkspaceFactory pWorkSpaFac = new ShapefileWorkspaceFactoryClass();
                esriGeometryType featureType = esriGeometryType.esriGeometryPolyline;
                //定义属性列参数
                IFields pFlds = new FieldsClass();
                IField pFld = new FieldClass();
                IFieldEdit pFldEdit = (IFieldEdit)pFld;
                IFieldsEdit pFldsEdit = (IFieldsEdit)pFlds;
                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;

                //确定保存路径
                pFeatWorkSpa = (IFeatureWorkspace)pWorkSpaFac.OpenFromFile(path, 0);

                //确定几何类型及坐标系
                pFldEdit.Name_2 = "Shape";
                pFldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                pGeoDefEdit.GeometryType_2 = featureType;
                pGeoDefEdit.SpatialReference_2 = spatialRef;
                pFldEdit.GeometryDef_2 = pGeoDef;

                //添加空间数据属性列
                pFldsEdit.AddField(pFld);

                //添加其余属性列
                foreach (string currFldName in strFldArray)
                {
                    pFld = new FieldClass();
                    pFldEdit = (IFieldEdit)pFld;
                    pFldEdit.Name_2 = currFldName;
                    pFldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pFldsEdit.AddField(pFld);
                }
                return pFeatWorkSpa.CreateFeatureClass(shpName, pFlds, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            }
        }
        #endregion

        #region 在地理数据库中创建栅格
        public void SaveRaster(string path, string filename, IRaster raster)
        {
            IWorkspace workspace = (IWorkspace)workspaceFactory.OpenFromFile(path, 0);
            ISaveAs SRaster = (ISaveAs)raster;
            SRaster.SaveAs(filename, workspace, "TIFF");
        }
        #endregion
    }
}
