using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalysis;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.DataSourcesGDB;
using WGIS.Forms;
using ESRI.ArcGIS.Display;
using System.Drawing.Drawing2D;
using ESRI.ArcGIS.Server;
using System.Configuration;

namespace WGIS.Classes
{
    /// <summary>
    /// 最短路径分析
    /// </summary>
    [Guid("4ffd8016-0be3-4730-8fc3-924b0ebc23b6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("WGIS.Classes.RouteQuery")]
    public sealed class RouteQuery : BaseTool
    {
        private AxMapControl axMapControl;
        public INAContext m_NAContext;//网络分析上下文
        private INetworkDataset networkDataset;//网络数据集
        public IFeatureWorkspace pFWorkspace;
        public IFeatureClass inputFClass;//打开stops数据集
        private IFeatureDataset featureDataset;
        public bool networkanalasia = false;//判断是否点击新路线按钮，进入添加起点阶段
        public int clickedcount = 0;//mapcontrol加点显示点数
        private IActiveView m_ipActiveView;
        public IGraphicsContainer PGC;
        private IMap m_ipMap;

        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper;

        public RouteQuery(AxMapControl axMapControl)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "路径查询";  //localizable text 
            base.m_message = "路径查询";  //localizable text
            base.m_toolTip = "路径查询";  //localizable text
            base.m_name = "RouteQuery";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                this.axMapControl = axMapControl;
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        /// <summary>
        /// 初始化，读取shp及网络数据集
        /// </summary>
        private void initialize()
        {
            axMapControl.ActiveView.Clear();
            axMapControl.ActiveView.Refresh();
            //获取当前应用程序的目录名称
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            int t;
            for (t = 0; t < path.Length; t++)
            {
                if (path.Substring(t, 4) == "WGIS")
                {
                    break;
                }
            }
            //根据目录名称获取数据存取路径
            string name = path.Substring(0, t-1) + "\\DATA\\data\\RouteQuery.mdb";
            //打开工作空间
            pFWorkspace = OpenWorkspace(name) as IFeatureWorkspace;
            //打开网络数据集
            networkDataset = OpenNetworkDataset(pFWorkspace as IWorkspace, "road_ND", "road");
            //创建网络分析上下文，建立一种解决关系
            m_NAContext = CreateSolverContext(networkDataset);
            //打开数据集
            inputFClass = pFWorkspace.OpenFeatureClass("stops");
            IFeatureCursor pFeatureCursor = inputFClass.Search(null, false);
            IFeature pFeature = pFeatureCursor.NextFeature();
            while (pFeature != null)
            {
                pFeature.Delete();
                pFeature = pFeatureCursor.NextFeature();
            }  
            //road_ND_JUNCTIONS图层
            IFeatureLayer vertex = new FeatureLayerClass();
            vertex.FeatureClass = pFWorkspace.OpenFeatureClass("road_ND_Junctions");
            vertex.Name = vertex.FeatureClass.AliasName;
            axMapControl.AddLayer(vertex, 0);
            //road图层
            IFeatureLayer road3;
            road3 = new FeatureLayerClass();
            road3.FeatureClass = pFWorkspace.OpenFeatureClass("road");
            road3.Name = road3.FeatureClass.AliasName;
            axMapControl.AddLayer(road3, 0);
            //为networkdataset生成一个图层，并将该图层添加到axmapcontrol中
            ILayer pLayer;//网络图层
            INetworkLayer pNetworkLayer;
            pNetworkLayer = new NetworkLayerClass();
            pNetworkLayer.NetworkDataset = networkDataset;
            pLayer = pNetworkLayer as ILayer;
            pLayer.Name = "Network Dataset";
            axMapControl.AddLayer(pLayer, 0);
            //生成一个网络分析图层并添加到axmaptrol中
            ILayer layer1;
            INALayer nalayer = m_NAContext.Solver.CreateLayer(m_NAContext);
            layer1 = nalayer as ILayer;
            layer1.Name = m_NAContext.Solver.DisplayName;
            axMapControl.AddLayer(layer1, 0);
            m_ipActiveView = axMapControl.ActiveView;
            m_ipMap = m_ipActiveView.FocusMap;
            PGC = m_ipMap as IGraphicsContainer;
        }

        /// <summary>
        /// 打开工作空间
        /// </summary>
        public IWorkspace OpenWorkspace(string strMDBName)
        {
            IWorkspaceFactory workspaceFactory;
            workspaceFactory = new AccessWorkspaceFactoryClass();
            return workspaceFactory.OpenFromFile(strMDBName, 0);
        }

        /// <summary>
        /// 打开网络数据集
        /// </summary>
        public INetworkDataset OpenNetworkDataset(IWorkspace networkDatasetWorkspace, System.String networkDatasetName, System.String featureDatasetName)
        {
            if (networkDatasetWorkspace == null || networkDatasetName == "" || featureDatasetName == null)
            {
                return null;
            }
            IFeatureWorkspace featureWorkspace = networkDatasetWorkspace as IFeatureWorkspace;
            featureDataset = featureWorkspace.OpenFeatureDataset(featureDatasetName);
            IFeatureDatasetExtensionContainer featureDatasetExtensionContainer = featureDataset as IFeatureDatasetExtensionContainer;
            IFeatureDatasetExtension featureDatasetExtension = featureDatasetExtensionContainer.FindExtension(esriDatasetType.esriDTNetworkDataset);
            IDatasetContainer3 datasetContainer3 = (IDatasetContainer3)featureDatasetExtension;
            if (datasetContainer3 == null)
                return null;
            IDataset dataset = datasetContainer3.get_DatasetByName(esriDatasetType.esriDTNetworkDataset, networkDatasetName);
            return dataset as INetworkDataset;
        }

        /// <summary>
        /// 创建网络分析上下文
        /// </summary>
        public INAContext CreateSolverContext(INetworkDataset networkDataset)
        {
            //获取创建网络分析上下文所需的IDENETWORKDATASET类型参数
            IDENetworkDataset deNDS = GetDENetworkDataset(networkDataset);
            INASolver naSolver;
            naSolver = new NARouteSolver();
            INAContextEdit contextEdit = naSolver.CreateContext(deNDS, naSolver.Name) as INAContextEdit;
            contextEdit.Bind(networkDataset, new GPMessagesClass());
            return contextEdit as INAContext;
        }

        /// <summary>
        /// 得到创建网络分析上下文所需的IDENETWORKDATASET类型参数
        /// </summary>
        public IDENetworkDataset GetDENetworkDataset(INetworkDataset networkDataset)
        {
            //将网络分析数据集QI添加到DATASETCOMPOENT
            IDatasetComponent dstComponent;
            dstComponent = networkDataset as IDatasetComponent;
            //获得数据元素
            return dstComponent.DataElement as IDENetworkDataset;
        }

        /// <summary>
        /// 获取距离鼠标点击最近的点
        /// </summary>
        public void CreateFeature(IFeatureClass featureClass, IPointCollection PointCollection)
        {
            //是否为点图层
            if (featureClass.ShapeType != esriGeometryType.esriGeometryPoint)
            {
                return;
            }

            for (int i = 0; i < PointCollection.PointCount; i++)
            {
               IFeature feature = featureClass.CreateFeature();

               feature.Shape = PointCollection.get_Point(i);
               IRowSubtypes rowSubtypes = (IRowSubtypes)feature;
               feature.Store();
            }
        }

        /// <summary>
        /// 加载停靠点
        /// </summary>
        public void loadNANetworkLocations(string strNAClassName, IFeatureClass inputFC, double snapTolerance)
        {
            INAClass naClass;
            INamedSet classes;
            classes = m_NAContext.NAClasses;
            naClass = classes.get_ItemByName(strNAClassName) as INAClass;
            //删除naClasses中添加的项
            naClass.DeleteAllRows();
            //加载网络分析对象，设置容差值
            INAClassLoader classLoader = new NAClassLoader();
            classLoader.Locator = m_NAContext.Locator;
            if (snapTolerance > 0) classLoader.Locator.SnapTolerance = snapTolerance;
            classLoader.NAClass = naClass;
            //字段匹配
            INAClassFieldMap pNAClassFieldMap = new NAClassFieldMap();
            pNAClassFieldMap.CreateMapping(naClass.ClassDefinition, inputFC.Fields);
            classLoader.FieldMap = pNAClassFieldMap;
            //加载网络分析类
            int rowsln = 0;
            int rowsLocated = 0;
            IFeatureCursor featureCursor = inputFC.Search(null, true);
            classLoader.Load((ICursor)featureCursor, null, ref rowsln, ref rowsLocated);
            ((INAContextEdit)m_NAContext).ContextChanged();
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add RouteQuery.OnCreate implementation
            networkanalasia = true;
            initialize();
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add RouteQuery.OnClick implementation
 
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add RouteQuery.OnMouseDown implementation
            if (networkanalasia == true)
            {
                IPointCollection m_ipPoints;//输入点集合
                IPoint ipNew;
                m_ipPoints = new MultipointClass();
                ipNew = axMapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                object o = Type.Missing;
                m_ipPoints.AddPoint(ipNew, ref o, ref o);
                CreateFeature(inputFClass, m_ipPoints);//获取用鼠标点击最近点
                //把最近的点显示出来
                IElement element;
                ITextElement textelement = new TextElementClass();
                element = textelement as IElement;
                ITextSymbol textSymbol = new ESRI.ArcGIS.Display.TextSymbol();

                textelement.Symbol = textSymbol;
                clickedcount++;
                textelement.Text = clickedcount.ToString();
                element.Geometry = m_ipActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                PGC.AddElement(element, 0);

                m_ipActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add RouteQuery.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add RouteQuery.OnMouseUp implementation
        }
        #endregion
    }
}
