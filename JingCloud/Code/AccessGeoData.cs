using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace JingCloud
{
    //提供方法：获得内存或地理数据库工作空间/要素类/栅格图层
    class AccessGeoData
    {
        IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();

        #region 获得地理数据库中的要素工作空间
        /// <summary>
        /// 获得要素工作空间
        /// </summary>
        /// <returns></returns>
        public IFeatureWorkspace OpenFeatureWorkspace(string path)
        {
            IFeatureWorkspace FeatureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(path, 0);
            return FeatureWorkspace;
        }
        #endregion

        #region//获得内存中的要素工作空间
        /// <summary>
        /// 获得内存中的要素工作空间
        /// </summary>
        /// <returns></returns>
        public IFeatureWorkspace FeatureWorkspace()
        {
            IWorkspaceFactory pWorkspacFactory = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName pWorkspaceName = pWorkspacFactory.Create("", "pWorkspace", null, 0);
            IName pName = (IName)pWorkspaceName;
            IWorkspace pWorkspace = (IWorkspace)pName.Open();
            IFeatureWorkspace pFeatuerwok = (IFeatureWorkspace)pWorkspace;
            return pFeatuerwok;
        }
        #endregion

        #region//打开要素类，返回FeatureClass
        /// <summary>
        /// 打开要素类，返回FeatureClass
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IFeatureClass FeatureClass(string path, string filename)
        {

            IFeatureWorkspace FeatureWorkspace = OpenFeatureWorkspace(path);
            return FeatureWorkspace.OpenFeatureClass(filename); 
        }
        #endregion

        #region//打开栅格数据集，返回RasterLayer
        /// <summary>
        /// 打开栅格数据集，返回RasterLayer
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IRasterLayer RasterLayer(string path, string filename)
        {
            IRasterWorkspaceEx workspaceEx = (IRasterWorkspaceEx)workspaceFactory.OpenFromFile(path, 0);
            IRasterLayer rasterLayer = new RasterLayerClass();
            IRasterDataset rasterDataset = workspaceEx.OpenRasterDataset(filename);
            rasterLayer.CreateFromDataset(rasterDataset);
            return rasterLayer;
        }
        #endregion

    }
    
    
}
