using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display; 


namespace WGIS.Classes
{
    /// <summary>
    /// Summary description for Pan.
    /// </summary>
    [Guid("0dc22ab3-e9c9-4817-9ffe-1bedf6bf48d1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("WGIS.Classes.Pan")]
    public sealed class Pan : BaseTool
    {
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
        //获取视图范围 
        private IScreenDisplay m_focusScreenDisplay = null;
        //标记操作进程 
        private bool m_PanOperation; 


        public Pan()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "漫游";  //localizable text 
            base.m_message = "漫游";  //localizable text
            base.m_toolTip = "漫游";  //localizable text
            base.m_name = "Pan";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
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

            // TODO:  Add Pan.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add Pan.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add Pan.OnMouseDown implementation
            //判断是否鼠标左键 
            if (Button != 1)
                return;
            //获取视图范围并开始漫游 
            IActiveView pActiveView = m_hookHelper.ActiveView;
            m_focusScreenDisplay = pActiveView.ScreenDisplay;
            IPoint pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y); m_focusScreenDisplay.PanStart(pPoint);
            //标记漫游操作为真 
            m_PanOperation = true;  

        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add Pan.OnMouseMove implementation
            //判断是否鼠标左键 
            if (Button != 1) return;
            //是否漫游状态 
            if (!m_PanOperation) return;
            //追踪鼠标 
            IPoint pPoint = m_focusScreenDisplay.DisplayTransformation.ToMapPoint(X, Y); m_focusScreenDisplay.PanMoveTo(pPoint);  

        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add Pan.OnMouseUp implementation
            //判断是否鼠标左键 
            if (Button != 1) return;
            //是否漫游状态 
            if (!m_PanOperation) return;
            IEnvelope pExtent = m_focusScreenDisplay.PanStop();
            //判断移动区域是否为空 
            if (pExtent != null)
            {
                m_focusScreenDisplay.DisplayTransformation.VisibleBounds = pExtent;
                m_focusScreenDisplay.Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
            }
            //关闭漫游状态 
            m_PanOperation = false; 

        }
        #endregion
    }
}
