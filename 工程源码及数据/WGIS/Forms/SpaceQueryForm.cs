using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;

namespace WGIS.Forms
{
    public partial class SpaceQueryForm : Form
    {
        private AxMapControl axMapControl;
        private IMap mMap;//当前MapControl控件的Map对象
        private int x, y;

        public SpaceQueryForm(int x, int y, AxMapControl axMapControl)
        {
            InitializeComponent();
            this.x = x;
            this.y = y;
            this.axMapControl = axMapControl;
            this.mMap = axMapControl.Map;
        }

        private void SpaceQueryForm_Load(object sender, EventArgs e)
        {
            //清空目标图层列表
            checkListLayer.Items.Clear();
            string layerName;//设置临时变量存储图层名称
            //对Map中的每一个图层进行判断并添加图层名称
            for (int i = 0; i < mMap.LayerCount; i++)
            {
                //如果该图层为图层组类型，则分别对所包含的每个图层进行操作
                if (mMap.get_Layer(i) is GroupLayer)
                {
                    //使用ICompositeLayer接口进行遍历操作
                    ICompositeLayer compositeLayer = mMap.get_Layer(i) as ICompositeLayer;
                    for (int j = 0; j < compositeLayer.Count; j++)
                    {
                        //将图层的名称添加到checkListTargetLayer控件中
                        layerName = compositeLayer.get_Layer(j).Name;
                        checkListLayer.Items.Add(layerName);
                    }
                }
                //如果图层不是图层组类型，则直接添加名称
                else
                {
                    layerName = mMap.get_Layer(i).Name;
                    checkListLayer.Items.Add(layerName);
                }
            }
        }

        /// <summary>
        /// 获取要素图层
        /// </summary>
        private IFeatureLayer GetFeatureLayerByName(IMap map, string layerName)
        {
            //对地图图层进行遍历
            for (int i = 0; i < map.LayerCount; i++)
            {
                //如果该图层为图层组类型，则分别对包含的每个图层进行操作
                if (map.get_Layer(i) is GroupLayer)
                {
                    //使用ICompositeLayer接口进行遍历操作
                    ICompositeLayer compositeLayer = map.get_Layer(i) as ICompositeLayer;
                    for (int j = 0; j < compositeLayer.Count; j++)
                    {
                        //如果图层名称为所要查询的图层名称，则返回IFeaturelayer接口的矢量图层对象
                        if (compositeLayer.get_Layer(j).Name == layerName)
                        {
                            return (IFeatureLayer)compositeLayer.get_Layer(j);
                        }
                    }
                }
                //如果图层不是图层组类型，则直接进行判断
                else
                {
                    if (map.get_Layer(i).Name == layerName)
                    {
                        return (IFeatureLayer)map.get_Layer(i);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 执行空间查询
        /// </summary>
        private void SelectFeaturesBySpatial()
        {
            IActiveView pActView = mMap as IActiveView;
            IPoint pt = pActView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

            ITopologicalOperator pTopo = pt as ITopologicalOperator;
            IGeometry pGeo = pTopo.Buffer((double)NumericDistance.Value);

            //定义和创建用于查询的ISpatialFilter接口对象
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
            //对选择的目标图层进行遍历，并对每一个图层进行空间查询，查询结果将放在选择集中
            IFeatureLayer featureLayer;
             //对所有选择的目标图层进行遍历
            for(int i = 0; i < checkListLayer.CheckedItems.Count; i++)
            {
                //根据选择的目标图层名称获得对应的矢量图层
                featureLayer = GetFeatureLayerByName(mMap, (string)checkListLayer.CheckedItems[i]);
                //进行接口转换,使用IFeatureSelection接口选择要素
                IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
                //使用IFeatureSelection接口的SelectFeature方法，根据空间查询过滤器选择要素，将其放在新的选择集中
                featureSelection.SelectFeatures((IQueryFilter)spatialFilter, esriSelectionResultEnum.esriSelectionResultAdd, false);
            }

            //进行接口转换，使用IActiveView接口进行视图操作
            IActiveView activeView = mMap as IActiveView;
            //进行部分刷新操作，只刷新选择集的内容
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);
        }

        /// <summary>
        /// 执行空间查询
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SelectFeaturesBySpatial();
                this.Close();
            }
            catch { }
        }

        /// <summary>
        /// 关闭空间查询窗口
        /// </summary>
        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
