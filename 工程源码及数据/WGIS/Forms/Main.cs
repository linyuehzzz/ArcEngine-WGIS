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
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DataSourcesFile;
using WGIS.Classes;
using WGIS.Forms;

namespace WGIS
{
    public partial class WGIS : Form
    {
        private ILayer pLayer;//选定的图层
        private string pMapUnits;
        private Pan pan = null;
        private RouteQuery pPathFinder = null;
        private SpaceQuery pSpaceQuery = null;

        public WGIS()
        {
            InitializeComponent();
            pMapUnits = "Unknown";
        }

        #region 1.打开文件
        /// <summary>
        /// 1.1 打开.mxd文件
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenMXD = new OpenFileDialog(); //可实例化类
            // Gets or sets the file dialog box title. (Inherited from FileDialog.)
            OpenMXD.Title = "打开地图"; // OpenFileDialog类的属性Title
            // Gets or sets the initial directory displayed by the file dialog box. 
            OpenMXD.InitialDirectory = "D:"; 
            // Gets or sets the current file name filter string ,Save as file type
            OpenMXD.Filter ="Map Documents (*.mxd)|*.mxd"; 
            if (OpenMXD.ShowDialog() == DialogResult.OK) //ShowDialog是类的方法
            { 
                //FileName:Gets or sets a string containing the file name selected in the file dialog box
                string MxdPath = OpenMXD.FileName; 
                axMapControl1.LoadMxFile(MxdPath); //IMapControl2的方法
            } 
        }

        /// <summary>
        /// 1.2 打开.shp文件
        /// </summary>
        private void addShpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenShpFile = new OpenFileDialog();
            OpenShpFile.Title = "打开Shape文件";
            OpenShpFile.InitialDirectory = "C:";
            OpenShpFile.Filter = "Shape文件(*.shp)|*.shp";
            OpenShpFile.Multiselect = true; 
            if (OpenShpFile.ShowDialog() == DialogResult.OK)
            {
                string ShapPath = OpenShpFile.FileName;
                int Position = ShapPath.LastIndexOf("\\"); //利用"\\"将文件路径分成两部分
                string FilePath = ShapPath.Substring(0, Position);
                string ShpName = ShapPath.Substring(Position + 1);
                axMapControl1.AddShapeFile(FilePath, ShpName);
            }
        }

        /// <summary>
        /// 1.3 打开.lyr文件
        /// </summary>
        private void MenuAddLyr_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenLyrFile = new OpenFileDialog();
            OpenLyrFile.Title = "打开Lyr";
            OpenLyrFile.InitialDirectory = "C:";
            OpenLyrFile.Filter = "lyr文件(*.lyr)|*.lyr";
            if (OpenLyrFile.ShowDialog() == DialogResult.OK)
            {
                string LayPath = OpenLyrFile.FileName;
                axMapControl1.AddLayerFromFile(LayPath);
            }
        }
        #endregion

        #region 2.鹰眼
        /// <summary>
        /// 当主图视野发生变化时，重新绘制鸟瞰图中的红色矩形框
        /// </summary>
        private void axMapControl1_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            // 得到新范围
            IEnvelope pEnvelope = (IEnvelope)e.newEnvelope;
            IGraphicsContainer pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            //在绘制前，清除axMapControl2中的任何图形元素
            pGraphicsContainer.DeleteAllElements();
            IRectangleElement pRectangleEle = new RectangleElementClass();
            IElement pElement = pRectangleEle as IElement;
            pElement.Geometry = pEnvelope;
            //设置鹰眼图中的红线框
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255; pColor.Green = 0; pColor.Blue = 0; pColor.Transparency = 255;
            //产生一个线符号对象
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 3; pOutline.Color = pColor;
            //设置颜色属性
            pColor = new RgbColorClass();
            pColor.Red = 255; pColor.Green = 0; pColor.Blue = 0; pColor.Transparency = 0;
            //设置填充符号的属性
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor; pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);//部分刷新
        }

        /// <summary>
        /// 使主图和鸟瞰图的数据保持一致
        /// </summary>
        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            #region 坐标单位替换
            esriUnits mapUnits = axMapControl1.MapUnits;
            switch (mapUnits)
            {
                case esriUnits.esriCentimeters:
                    pMapUnits = "Centimeters";
                    break;
                case esriUnits.esriDecimalDegrees:
                    pMapUnits = "Decimal Degrees";
                    break;
                case esriUnits.esriDecimeters:
                    pMapUnits = "Decimeters";
                    break;
                case esriUnits.esriFeet:
                    pMapUnits = "Feet";
                    break;
                case esriUnits.esriInches:
                    pMapUnits = "Inches";
                    break;
                case esriUnits.esriKilometers:
                    pMapUnits = "Kilometers";
                    break;
                case esriUnits.esriMeters:
                    pMapUnits = "Meters";
                    break;
                case esriUnits.esriMiles:
                    pMapUnits = "Miles";
                    break;
                case esriUnits.esriMillimeters:
                    pMapUnits = "Millimeters";
                    break;
                case esriUnits.esriNauticalMiles:
                    pMapUnits = "NauticalMiles";
                    break;
                case esriUnits.esriPoints:
                    pMapUnits = "Points";
                    break;
                case esriUnits.esriUnknownUnits:
                    pMapUnits = "Unknown";
                    break;
                case esriUnits.esriYards:
                    pMapUnits = "Yards";
                    break;
            }
            #endregion

            if (axMapControl1.LayerCount > 0)
            {
                axMapControl2.Map = new MapClass();
                for (int i = axMapControl1.Map.LayerCount - 1; i >= 0; i--)
                {
                    axMapControl2.AddLayer(axMapControl1.get_Layer(i));
                }
                axMapControl2.Extent = axMapControl1.FullExtent;
                axMapControl2.Refresh();
            }
            CopyMapFromMapControlToPageLayoutControl();//调用地图复制函数
        }

        /// <summary>
        /// 获取主图中鼠标位置
        /// </summary>
        private void axMapControl2_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button == 1)
            {
                IPoint pPoint = new PointClass();
                pPoint.PutCoords(e.mapX, e.mapY);
                axMapControl1.CenterAt(pPoint);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        /// <summary>
        /// 在主图中使用鼠标拖拽视图时，鸟瞰图中出现红色矩形框
        /// </summary>
        private void axMapControl2_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            if (axMapControl2.Map.LayerCount > 0)
            {
                if (e.button == 1)
                {
                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(e.mapX, e.mapY);
                    axMapControl1.CenterAt(pPoint);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                else if (e.button == 2)
                {
                    IEnvelope pEnv = axMapControl2.TrackRectangle();
                    axMapControl1.Extent = pEnv;
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }
        #endregion

        #region 4.数据/布局视图切换
        /// <summary>
        /// 将数据视图中的地图拷贝到布局视图中
        /// </summary>
        private void CopyMapFromMapControlToPageLayoutControl()
        {
            //获得IObjectCopy接口
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            //获得要拷贝的图层 
            System.Object pSourceMap = axMapControl1.Map;
            //获得拷贝图层
            System.Object pCopiedMap = pObjectCopy.Copy(pSourceMap);
            //获得要重绘的地图 
            System.Object pOverwritedMap = axPageLayoutControl1.ActiveView.FocusMap;
            //重绘pagelayout地图
            pObjectCopy.Overwrite(pCopiedMap, ref pOverwritedMap);
        }

        /// <summary>
        /// 屏幕变化后刷新屏幕
        /// </summary>
        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            //获得IActiveView接口
            IActiveView pPageLayoutView = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;
            //获得IDisplayTransformation接口
            IDisplayTransformation pDisplayTransformation = pPageLayoutView.ScreenDisplay.DisplayTransformation;
            //设置可视范围
            pDisplayTransformation.VisibleBounds = axMapControl1.Extent;
            axPageLayoutControl1.ActiveView.Refresh(); //刷新地图
            //根据MapControl的视图范围，确定PageLayoutControl的视图范围
            CopyMapFromMapControlToPageLayoutControl();
        }
        #endregion

        #region 5.显示状态栏信息
        /// <summary>
        /// 显示当前状态、比例尺和坐标
        /// </summary>
        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            // 取得鼠标所在工具的索引号  
            int index = axToolbarControl1.HitTest(e.x, e.y, false);
            if (index != -1)
            {
                // 取得鼠标所在工具的 ToolbarItem  
                IToolbarItem toolbarItem = axToolbarControl1.GetItem(index);
                // 设置状态栏信息  
                MessageLabel.Text = toolbarItem.Command.Message;
            }
            else
            {
                MessageLabel.Text = " 就绪 ";
            }
            // 显示当前比例尺
            ScaleLabel.Text = " 比例尺 1:" + ((long)this.axMapControl1.MapScale).ToString();
            // 显示当前坐标
            CoordinateLabel.Text = " 当前坐标 X = " + e.mapX.ToString() + " Y = " + e.mapY.ToString() + " " + pMapUnits.ToString();
        }
        #endregion

        #region 6.漫游
        /// <summary>
        /// 点击菜单响应
        /// </summary>
        private void menuPan_Click(object sender, EventArgs e)
        {
            //声明并初始化
            pan = new Pan();
            //关联MapControl
            pan.OnCreate(this.axMapControl1.Object);
            //设置鼠标形状
            this.axMapControl1.CurrentTool = pan;
            this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPan; 
        }

        /// <summary>
        /// 点击
        /// </summary>
        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (pan != null)
                pan.OnMouseDown(e.button, e.shift, e.x, e.y);

            if (pPathFinder != null)
            {
                pPathFinder.OnMouseDown(e.button, e.shift, e.x, e.y);
            }

            if (pSpaceQuery != null)
                pSpaceQuery.OnMouseDown(e.button, e.shift, e.x, e.y);
        }

        /// <summary>
        /// 取消点击
        /// </summary>
        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            //漫游（BaseTool方法）
            if (pan != null)
                pan.OnMouseUp(e.button, e.shift, e.x, e.y);
        }
        #endregion

        #region 7.属性查询
        /// <summary>
        /// 打开属性查询窗口
        /// </summary>
        private void menuAttributeQuery_Click(object sender, EventArgs e)
        {
            AttributeQueryForm frmattributequery = new AttributeQueryForm(this.axMapControl1);
            frmattributequery.Show(); 
        }
        #endregion

        #region 8.缩放
        /// <summary>
        /// 8.1 缩小
        /// </summary>
        private void menuZoomOut_Click(object sender, EventArgs e)
        {
            //Tool的定义和初始化 
            ITool tool = new ControlsMapZoomOutToolClass();
            //查询接口获取ICommand 
            ICommand command = tool as ICommand;
            //Tool通过ICommand与MapControl的关联 
            command.OnCreate(this.axMapControl1.Object);
            command.OnClick();
            //MapControl的当前工具设定为tool 
            this.axMapControl1.CurrentTool = tool;
        }

        /// <summary>
        /// 8.2 放大
        /// </summary>
        private void menuZoomIn_Click(object sender, EventArgs e)
        {
            //Tool的定义和初始化 
            ITool tool = new ControlsMapZoomInToolClass();
            //查询接口获取ICommand 
            ICommand command = tool as ICommand;
            //Tool通过ICommand与MapControl的关联 
            command.OnCreate(this.axMapControl1.Object);
            command.OnClick();
            //MapControl的当前工具设定为tool 
            this.axMapControl1.CurrentTool = tool;
        }

        /// <summary>
        /// 8.3 中心放大
        /// </summary>
        private void menuFixedZoomIn_Click(object sender, EventArgs e)
        {
            //声明与初始化 
            FixedZoomIn fixedZoomin = new FixedZoomIn();
            //与MapControl关联 
            fixedZoomin.OnCreate(this.axMapControl1.Object);
            fixedZoomin.OnClick();
        }

        /// <summary>
        /// 8.4 中心缩小
        /// </summary>
        private void menuFixedZoomOut_Click(object sender, EventArgs e)
        {
            ICommand command = new ControlsMapZoomOutFixedCommandClass();
            command.OnCreate(this.axMapControl1.Object);
            command.OnClick();
        }
        #endregion

        #region 9.全图显示
        /// <summary>
        /// 全图显示
        /// </summary>
        private void menuFullExtent_Click(object sender, EventArgs e)
        {
            axMapControl1.Extent = axMapControl1.FullExtent;
        }
        #endregion

        #region 10.保存文件
        /// <summary>
        /// 地图文档编辑过后进行保存
        /// </summary>
        private void Save_Click(object sender, EventArgs e)
        {
            //获取pMapDocument对象
            IMxdContents pMxdC;
            pMxdC = axMapControl1.Map as IMxdContents;
            IMapDocument pMapDocument = new MapDocumentClass();
            pMapDocument.Open(axMapControl1.DocumentFilename, "");
            IActiveView pActiveView = axMapControl1.Map as IActiveView;
            pMapDocument.ReplaceContents(pMxdC);
            //判断pMapDocument是否为空
            if (pMapDocument == null) return;
            //检查地图文档是否是只读
            if (pMapDocument.get_IsReadOnly(pMapDocument.DocumentFilename) == true)
            {
                MessageBox.Show("本地图文档是只读的，不能保存！");
                return;
            }

            //根据相对的路径保存地图文档
            pMapDocument.Save(pMapDocument.UsesRelativePaths, true);
        }
        #endregion

        #region 11.图层操作
        /// <summary>
        /// 11.1 移除图层
        /// </summary>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (axTOCControl1 != null)
            {
                axMapControl1.Map.DeleteLayer(pLayer);
                pLayer = null;
            }
        }

        /// <summary>
        /// 11.2 查看属性表
        /// </summary>
        private void attributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAttribute frm1 = new FrmAttribute(pLayer);
            frm1.Show();
        }

        /// <summary>
        /// 右键点击某一图层
        /// </summary>
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (axMapControl1.LayerCount > 0)
            {
                esriTOCControlItem pItem = new esriTOCControlItem();
                pLayer = new FeatureLayerClass();
                IBasicMap pBasicMap = new MapClass();
                object pOther = new object();
                object pIndex = new object();
                // Returns the item in the TOCControl at the specified coordinates.
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pBasicMap, ref pLayer, ref pOther, ref pIndex);
            }//TOCControl类的ITOCControl接口的HitTest方法
            if (e.button == 2)
            {
                contextMenuStrip2.Show(axTOCControl1, e.x, e.y);
            }
        }

        /// <summary>
        /// 11.3 修改数据源
        /// </summary>
        private void addDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog OpenSHP = new FolderBrowserDialog();
            if (DialogResult.OK == OpenSHP.ShowDialog())
            {
                IDataLayer2 pDLayer = (IDataLayer2)pLayer;
                IDatasetName pDsName = (IDatasetName)(pDLayer.DataSourceName);
                IWorkspaceName ws = pDsName.WorkspaceName;
                string ShpPath = OpenSHP.SelectedPath;
                ws.PathName = ShpPath;
                pDsName.WorkspaceName = ws;
            }
        }

        /// <summary>
        /// 11.4 导出.shp数据
        /// </summary>
        private void outputDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                string file = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf("\\"));
                if (!System.IO.Directory.Exists(file))
                {
                    System.IO.Directory.CreateDirectory(file);
                }
                try
                {
                    ILayer pLayer = axMapControl1.Map.get_Layer(0);
                    if (pLayer != null)
                    {
                        IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                        if (pFeatureLayer.Visible)
                        {
                            ExportFeature(pFeatureLayer.FeatureClass,dlg.FileName);
                        }
                    }
                    MessageBox.Show("导出成功");
                }
                catch
                {
                    MessageBox.Show("导出失败！");
                }
            }
        }

        private void ExportFeature(IFeatureClass pInFeatureClass,string pPath)
        {
            //创建一个输出shp文件的工作空间        
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            string parentPath = pPath.Substring(0, pPath.LastIndexOf("\\"));
            string fileName = pPath.Substring(pPath.LastIndexOf("\\") + 1, pPath.Length - pPath.LastIndexOf("\\") - 1);
            IWorkspaceName pWorkspaceName = pWorkspaceFactory.Create(parentPath,fileName, null, 0);
            //创建一个要素类       
            IName name = (IName)pWorkspaceName;       
            IWorkspace pOutWorkspace = (IWorkspace)name.Open();

            IDataset pInDataset = pInFeatureClass as IDataset;
            IFeatureClassName pInFCName = pInDataset.FullName as IFeatureClassName;
            IWorkspace pInWorkspace = pInDataset.Workspace;
            IDataset pOutDataset = pOutWorkspace as IDataset;
            IWorkspaceName pOutWorkspaceName = pOutDataset.FullName as IWorkspaceName;
            IFeatureClassName pOutFCName = new FeatureClassNameClass();
            IDatasetName pDatasetName = pOutFCName as IDatasetName;
            pDatasetName.WorkspaceName = pOutWorkspaceName;
            pDatasetName.Name = pInFeatureClass.AliasName;
            IFieldChecker pFieldChecker = new FieldCheckerClass();
            pFieldChecker.InputWorkspace = pInWorkspace;
            pFieldChecker.ValidateWorkspace = pOutWorkspace;
            IFields pFields = pInFeatureClass.Fields;
            IFields pOutFields;
            IEnumFieldError pEnumFieldError;
            pFieldChecker.Validate(pFields, out pEnumFieldError, out pOutFields);
            IFeatureDataConverter pFeatureDataConverter = new FeatureDataConverterClass();
            pFeatureDataConverter.ConvertFeatureClass(pInFCName, null, null, pOutFCName, null, pOutFields, "", 100, 0);
        }
        #endregion

        #region 12.空间查询
        /// <summary>
        /// 点击地图，查询缓冲区内其他图层要素
        /// </summary>
        private void menuSpaceQuery_Click(object sender, EventArgs e)
        {
            //声明并初始化
            pSpaceQuery = new SpaceQuery(this.axMapControl1);
            //关联MapControl
            pSpaceQuery.OnCreate(this.axMapControl1.Object);
        }
        #endregion

        #region 13.主题地图
        /// <summary>
        /// 14.1 普通地图
        /// </summary>
        private void menuMap_Click(object sender, EventArgs e)
        {
            IEnumLayer pEnumLayer = axMapControl1.Map.Layers;
            pEnumLayer.Reset();

            ILayer pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                if(pLayer.Name == "1-普通地图-教学楼")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1-普通地图-宿舍楼")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1-普通地图-图书馆")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1-普通地图-食堂")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1-普通地图-医院")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1/3-普通地图/游览地图-景点")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1-普通地图-行政办公")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1-普通地图-文化活动中心")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1-普通地图-文化场馆")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1-普通地图-体育设施")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1/2-普通地图/生活地图-基础道路")
                    pLayer.Visible = true;
                else if (pLayer.Name == "广场")
                    pLayer.Visible = true;
                else if (pLayer.Name == "建筑物")
                    pLayer.Visible = true;
                else if (pLayer.Name == "山")
                    pLayer.Visible = true;
                else if (pLayer.Name == "水")
                    pLayer.Visible = true;
                else
                    pLayer.Visible = false;
                pLayer = pEnumLayer.Next();
            }

            axMapControl1.Refresh();
        }

        /// <summary>
        /// 14.2 生活地图
        /// </summary>
        private void menuLifeMap_Click(object sender, EventArgs e)
        {
            IEnumLayer pEnumLayer = axMapControl1.Map.Layers;
            pEnumLayer.Reset();

            ILayer pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                if (pLayer.Name == "2-生活地图-餐馆")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2-生活地图-超市便利店")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2/3-生活地图/游览地图-交通站点")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2-生活地图-打印店")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2-生活地图-公共厕所")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2-生活地图-火车取票")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2-生活地图-校园卡服务")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2-生活地图-空调卡服务")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2-生活地图-热水卡充值")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2-生活地图-快递点")
                    pLayer.Visible = true;
                else if (pLayer.Name == "1/2-普通地图/生活地图-基础道路")
                    pLayer.Visible = true;
                else if (pLayer.Name == "广场")
                    pLayer.Visible = true;
                else if (pLayer.Name == "建筑物")
                    pLayer.Visible = true;
                else if (pLayer.Name == "山")
                    pLayer.Visible = true;
                else if (pLayer.Name == "水")
                    pLayer.Visible = true;
                else
                    pLayer.Visible = false;
                pLayer = pEnumLayer.Next();
            }
            axMapControl1.Refresh();
        }

        /// <summary>
        /// 14.3 游览地图
        /// </summary>
        private void menuTourMap_Click(object sender, EventArgs e)
        {
            IEnumLayer pEnumLayer = axMapControl1.Map.Layers;
            pEnumLayer.Reset();

            ILayer pLayer = pEnumLayer.Next();
            while (pLayer != null)
            {
                if (pLayer.Name == "1/3-普通地图/游览地图-景点")
                    pLayer.Visible = true;
                else if (pLayer.Name == "2/3-生活地图/游览地图-交通站点")
                    pLayer.Visible = true;
                else if (pLayer.Name == "3-游览地图-古建筑")
                    pLayer.Visible = true;
                else if (pLayer.Name == "3-游览地图-地铁线")
                    pLayer.Visible = true;
                else if (pLayer.Name == "3-游览地图-校车路线")
                    pLayer.Visible = true;
                else if (pLayer.Name == "3-游览地图-游览路线")
                    pLayer.Visible = true;
                else if (pLayer.Name == "广场")
                    pLayer.Visible = true;
                else if (pLayer.Name == "建筑物")
                    pLayer.Visible = true;
                else if (pLayer.Name == "山")
                    pLayer.Visible = true;
                else if (pLayer.Name == "水")
                    pLayer.Visible = true;
                else
                    pLayer.Visible = false;
                pLayer = pEnumLayer.Next();
            }
            axMapControl1.Refresh();
        }
        #endregion

        #region 14.路径查询
        /// <summary>
        /// 添加途经点
        /// </summary>
        private void menuBeginRoute_Click(object sender, EventArgs e)
        {
            //声明并初始化
            pPathFinder = new RouteQuery(this.axMapControl1);
            //关联MapControl
            pPathFinder.OnCreate(this.axMapControl1.Object);
        }

        /// <summary>
        /// 开始路径查询
        /// </summary>
        private void menuOKRoute_Click(object sender, EventArgs e)
        {
            try
            {
                pPathFinder.loadNANetworkLocations("Stops", pPathFinder.inputFClass, 80);
                IGPMessages gpMessages = new GPMessagesClass();
                bool flag = pPathFinder.m_NAContext.Solver.Solve(pPathFinder.m_NAContext, gpMessages, null);

                IFeatureClass routesFC = pPathFinder.m_NAContext.NAClasses.get_ItemByName("Routes") as IFeatureClass;
                IFeatureLayer pRouteFeature = new FeatureLayerClass();
                pRouteFeature.FeatureClass = routesFC;
                pRouteFeature.Name = routesFC.AliasName;
                axMapControl1.AddLayer(pRouteFeature, 0);

                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
            catch 
            {
                MessageBox.Show("无路径分析结果", "提示"); 
                return;
            }
        }

        /// <summary>
        /// 清除路径和图层信息
        /// </summary>
        private void menuEndRoute_Click(object sender, EventArgs e)
        {
            //解决完后，删除图层内容
            ITable pTable_inputFClass = pPathFinder.inputFClass as ITable;
            pTable_inputFClass.DeleteSearchedRows(null);
            //删除上一次路径route网络上下文
            IFeatureClass routesFC;
            routesFC = pPathFinder.m_NAContext.NAClasses.get_ItemByName("Routes") as IFeatureClass;
            ITable pTable1 = routesFC as ITable;
            pTable1.DeleteSearchedRows(null);
            //删除上一次路径Stops网络上下文
            INAClass stopsNAClass = pPathFinder.m_NAContext.NAClasses.get_ItemByName("Stops") as INAClass;
            ITable ptable2 = stopsNAClass as ITable;
            ptable2.DeleteSearchedRows(null);
            //删除上一次barries网络上下文
            INAClass barriesNAClass = pPathFinder.m_NAContext.NAClasses.get_ItemByName("Barriers") as INAClass;
            ITable pTable3 = barriesNAClass as ITable;
            pTable3.DeleteSearchedRows(null);
            pPathFinder.PGC.DeleteAllElements();
            pPathFinder.clickedcount = 0;
            axMapControl1.Refresh();
        }
        #endregion
    }
}
