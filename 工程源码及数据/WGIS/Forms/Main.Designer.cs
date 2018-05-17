namespace WGIS
{
    partial class WGIS
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WGIS));
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.OpenMxd = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.Open = new System.Windows.Forms.ToolStripMenuItem();
            this.addData = new System.Windows.Forms.ToolStripMenuItem();
            this.Save = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFixedZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFixedZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFullExtent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAttributeQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRouteQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBeginRoute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOKRoute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEndRoute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSpaceQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuThemeMap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLifeMap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTourMap = new System.Windows.Forms.ToolStripMenuItem();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.attributeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ScaleLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CoordinateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Blank = new System.Windows.Forms.ToolStripStatusLabel();
            this.axMapControl2 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.OpenMxd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(1335, 27);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // OpenMxd
            // 
            this.OpenMxd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.menuView,
            this.menuQuery,
            this.menuThemeMap});
            this.OpenMxd.Location = new System.Drawing.Point(0, 0);
            this.OpenMxd.Name = "OpenMxd";
            this.OpenMxd.Size = new System.Drawing.Size(1379, 32);
            this.OpenMxd.TabIndex = 4;
            this.OpenMxd.Text = "menuStrip1";
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open,
            this.addData,
            this.Save});
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(58, 28);
            this.File.Text = "&文件";
            // 
            // Open
            // 
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(152, 28);
            this.Open.Text = "&打开";
            this.Open.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // addData
            // 
            this.addData.Name = "addData";
            this.addData.Size = new System.Drawing.Size(152, 28);
            this.addData.Text = "&添加数据";
            this.addData.Click += new System.EventHandler(this.addShpToolStripMenuItem_Click);
            // 
            // Save
            // 
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(152, 28);
            this.Save.Text = "&保存";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuZoom,
            this.menuPan,
            this.menuFullExtent});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(58, 28);
            this.menuView.Text = "&视图";
            // 
            // menuZoom
            // 
            this.menuZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuZoomIn,
            this.menuZoomOut,
            this.menuFixedZoomIn,
            this.menuFixedZoomOut});
            this.menuZoom.Name = "menuZoom";
            this.menuZoom.Size = new System.Drawing.Size(152, 28);
            this.menuZoom.Text = "&缩放";
            // 
            // menuZoomIn
            // 
            this.menuZoomIn.Name = "menuZoomIn";
            this.menuZoomIn.Size = new System.Drawing.Size(152, 28);
            this.menuZoomIn.Text = "放大";
            this.menuZoomIn.Click += new System.EventHandler(this.menuZoomIn_Click);
            // 
            // menuZoomOut
            // 
            this.menuZoomOut.Name = "menuZoomOut";
            this.menuZoomOut.Size = new System.Drawing.Size(152, 28);
            this.menuZoomOut.Text = "缩小";
            this.menuZoomOut.Click += new System.EventHandler(this.menuZoomOut_Click);
            // 
            // menuFixedZoomIn
            // 
            this.menuFixedZoomIn.Name = "menuFixedZoomIn";
            this.menuFixedZoomIn.Size = new System.Drawing.Size(152, 28);
            this.menuFixedZoomIn.Text = "中心放大";
            this.menuFixedZoomIn.Click += new System.EventHandler(this.menuFixedZoomIn_Click);
            // 
            // menuFixedZoomOut
            // 
            this.menuFixedZoomOut.Name = "menuFixedZoomOut";
            this.menuFixedZoomOut.Size = new System.Drawing.Size(152, 28);
            this.menuFixedZoomOut.Text = "中心缩小";
            this.menuFixedZoomOut.Click += new System.EventHandler(this.menuFixedZoomOut_Click);
            // 
            // menuPan
            // 
            this.menuPan.Name = "menuPan";
            this.menuPan.Size = new System.Drawing.Size(152, 28);
            this.menuPan.Text = "&漫游";
            this.menuPan.Click += new System.EventHandler(this.menuPan_Click);
            // 
            // menuFullExtent
            // 
            this.menuFullExtent.Name = "menuFullExtent";
            this.menuFullExtent.Size = new System.Drawing.Size(152, 28);
            this.menuFullExtent.Text = "&全图显示";
            this.menuFullExtent.Click += new System.EventHandler(this.menuFullExtent_Click);
            // 
            // menuQuery
            // 
            this.menuQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAttributeQuery,
            this.menuRouteQuery,
            this.menuSpaceQuery});
            this.menuQuery.Name = "menuQuery";
            this.menuQuery.Size = new System.Drawing.Size(58, 28);
            this.menuQuery.Text = "查询";
            // 
            // menuAttributeQuery
            // 
            this.menuAttributeQuery.Name = "menuAttributeQuery";
            this.menuAttributeQuery.Size = new System.Drawing.Size(152, 28);
            this.menuAttributeQuery.Text = "属性查询";
            this.menuAttributeQuery.Click += new System.EventHandler(this.menuAttributeQuery_Click);
            // 
            // menuRouteQuery
            // 
            this.menuRouteQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBeginRoute,
            this.menuOKRoute,
            this.menuEndRoute});
            this.menuRouteQuery.Name = "menuRouteQuery";
            this.menuRouteQuery.Size = new System.Drawing.Size(152, 28);
            this.menuRouteQuery.Text = "&路径查询";
            // 
            // menuBeginRoute
            // 
            this.menuBeginRoute.Name = "menuBeginRoute";
            this.menuBeginRoute.Size = new System.Drawing.Size(170, 28);
            this.menuBeginRoute.Text = "&添加途经点";
            this.menuBeginRoute.Click += new System.EventHandler(this.menuBeginRoute_Click);
            // 
            // menuOKRoute
            // 
            this.menuOKRoute.Name = "menuOKRoute";
            this.menuOKRoute.Size = new System.Drawing.Size(170, 28);
            this.menuOKRoute.Text = "&执行";
            this.menuOKRoute.Click += new System.EventHandler(this.menuOKRoute_Click);
            // 
            // menuEndRoute
            // 
            this.menuEndRoute.Name = "menuEndRoute";
            this.menuEndRoute.Size = new System.Drawing.Size(170, 28);
            this.menuEndRoute.Text = "&结束";
            this.menuEndRoute.Click += new System.EventHandler(this.menuEndRoute_Click);
            // 
            // menuSpaceQuery
            // 
            this.menuSpaceQuery.Name = "menuSpaceQuery";
            this.menuSpaceQuery.Size = new System.Drawing.Size(152, 28);
            this.menuSpaceQuery.Text = "&空间查询";
            this.menuSpaceQuery.Click += new System.EventHandler(this.menuSpaceQuery_Click);
            // 
            // menuThemeMap
            // 
            this.menuThemeMap.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMap,
            this.menuLifeMap,
            this.menuTourMap});
            this.menuThemeMap.Name = "menuThemeMap";
            this.menuThemeMap.Size = new System.Drawing.Size(94, 28);
            this.menuThemeMap.Text = "主题地图";
            // 
            // menuMap
            // 
            this.menuMap.Name = "menuMap";
            this.menuMap.Size = new System.Drawing.Size(152, 28);
            this.menuMap.Text = "&普通地图";
            this.menuMap.Click += new System.EventHandler(this.menuMap_Click);
            // 
            // menuLifeMap
            // 
            this.menuLifeMap.Name = "menuLifeMap";
            this.menuLifeMap.Size = new System.Drawing.Size(152, 28);
            this.menuLifeMap.Text = "&生活地图";
            this.menuLifeMap.Click += new System.EventHandler(this.menuLifeMap_Click);
            // 
            // menuTourMap
            // 
            this.menuTourMap.Name = "menuTourMap";
            this.menuTourMap.Size = new System.Drawing.Size(152, 28);
            this.menuTourMap.Text = "&游览地图";
            this.menuTourMap.Click += new System.EventHandler(this.menuTourMap_Click);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(30, 35);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1223, 28);
            this.axToolbarControl1.TabIndex = 3;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attributeToolStripMenuItem,
            this.addDataToolStripMenuItem,
            this.outputDataToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(171, 116);
            // 
            // attributeToolStripMenuItem
            // 
            this.attributeToolStripMenuItem.Name = "attributeToolStripMenuItem";
            this.attributeToolStripMenuItem.Size = new System.Drawing.Size(170, 28);
            this.attributeToolStripMenuItem.Text = "打开属性表";
            this.attributeToolStripMenuItem.Click += new System.EventHandler(this.attributeToolStripMenuItem_Click);
            // 
            // addDataToolStripMenuItem
            // 
            this.addDataToolStripMenuItem.Name = "addDataToolStripMenuItem";
            this.addDataToolStripMenuItem.Size = new System.Drawing.Size(170, 28);
            this.addDataToolStripMenuItem.Text = "修改数据源";
            this.addDataToolStripMenuItem.Click += new System.EventHandler(this.addDataToolStripMenuItem_Click);
            // 
            // outputDataToolStripMenuItem
            // 
            this.outputDataToolStripMenuItem.Name = "outputDataToolStripMenuItem";
            this.outputDataToolStripMenuItem.Size = new System.Drawing.Size(170, 28);
            this.outputDataToolStripMenuItem.Text = "导出数据";
            this.outputDataToolStripMenuItem.Click += new System.EventHandler(this.outputDataToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(170, 28);
            this.deleteToolStripMenuItem.Text = "移除图层";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(495, 127);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(758, 435);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axMapControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(750, 403);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "地图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(3, 3);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(744, 397);
            this.axMapControl1.TabIndex = 1;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
            this.axMapControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.axMapControl1_OnExtentUpdated);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.axPageLayoutControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(750, 403);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "布局";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(3, 3);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(744, 397);
            this.axPageLayoutControl1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MessageLabel,
            this.ScaleLabel,
            this.CoordinateLabel,
            this.Blank});
            this.statusStrip1.Location = new System.Drawing.Point(0, 581);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1379, 33);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MessageLabel
            // 
            this.MessageLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(50, 28);
            this.MessageLabel.Text = "就绪";
            // 
            // ScaleLabel
            // 
            this.ScaleLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ScaleLabel.Name = "ScaleLabel";
            this.ScaleLabel.Size = new System.Drawing.Size(68, 28);
            this.ScaleLabel.Text = "比例尺";
            // 
            // CoordinateLabel
            // 
            this.CoordinateLabel.Name = "CoordinateLabel";
            this.CoordinateLabel.Size = new System.Drawing.Size(82, 28);
            this.CoordinateLabel.Text = "当前坐标";
            // 
            // Blank
            // 
            this.Blank.Name = "Blank";
            this.Blank.Size = new System.Drawing.Size(1164, 28);
            this.Blank.Spring = true;
            // 
            // axMapControl2
            // 
            this.axMapControl2.Location = new System.Drawing.Point(31, 353);
            this.axMapControl2.Name = "axMapControl2";
            this.axMapControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl2.OcxState")));
            this.axMapControl2.Size = new System.Drawing.Size(237, 205);
            this.axMapControl2.TabIndex = 5;
            this.axMapControl2.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl2_OnMouseDown);
            this.axMapControl2.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl2_OnMouseMove);
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(30, 97);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(238, 238);
            this.axTOCControl1.TabIndex = 2;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            // 
            // WGIS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 614);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.axMapControl2);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.OpenMxd);
            this.MainMenuStrip = this.OpenMxd;
            this.Name = "WGIS";
            this.Text = "WGIS";
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.OpenMxd.ResumeLayout(false);
            this.OpenMxd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.MenuStrip OpenMxd;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem Open;
        private System.Windows.Forms.ToolStripMenuItem addData;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem attributeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel MessageLabel;
        private System.Windows.Forms.ToolStripStatusLabel Blank;
        private System.Windows.Forms.ToolStripStatusLabel ScaleLabel;
        private System.Windows.Forms.ToolStripStatusLabel CoordinateLabel;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuZoom;
        private System.Windows.Forms.ToolStripMenuItem menuPan;
        private System.Windows.Forms.ToolStripMenuItem menuFullExtent;
        private System.Windows.Forms.ToolStripMenuItem menuZoomIn;
        private System.Windows.Forms.ToolStripMenuItem menuZoomOut;
        private System.Windows.Forms.ToolStripMenuItem menuFixedZoomIn;
        private System.Windows.Forms.ToolStripMenuItem menuFixedZoomOut;
        private System.Windows.Forms.ToolStripMenuItem menuQuery;
        private System.Windows.Forms.ToolStripMenuItem menuAttributeQuery;
        private System.Windows.Forms.ToolStripMenuItem Save;
        private System.Windows.Forms.ToolStripMenuItem addDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuRouteQuery;
        private System.Windows.Forms.ToolStripMenuItem menuSpaceQuery;
        private System.Windows.Forms.ToolStripMenuItem menuThemeMap;
        private System.Windows.Forms.ToolStripMenuItem menuMap;
        private System.Windows.Forms.ToolStripMenuItem menuLifeMap;
        private System.Windows.Forms.ToolStripMenuItem menuTourMap;
        private System.Windows.Forms.ToolStripMenuItem menuBeginRoute;
        private System.Windows.Forms.ToolStripMenuItem menuOKRoute;
        private System.Windows.Forms.ToolStripMenuItem menuEndRoute;
    }
}

