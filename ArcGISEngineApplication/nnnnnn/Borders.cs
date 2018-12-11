using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;

namespace MapAndRelatedObjects.地图的组成
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    [Guid("83f2d911-60e6-4876-8e14-a551b06d283d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapAndRelatedObjects.地图的组成.Borders")]
    public sealed class Borders : BaseCommand
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
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        IPageLayoutControl3 pPageLayoutControl;
        public Borders()
        {
            base.m_category = "MapAndRelatedObjects"; 
            base.m_caption = "添加地图边框";
            base.m_message = "添加地图边框";
            base.m_toolTip = "添加地图边框";
            base.m_name = "AddBorders";   

            try
            {
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            pPageLayoutControl = m_hookHelper.Hook as IPageLayoutControl3;
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add Borders.OnClick implementation
            // TODO: Add Backgrounds.OnClick implementation
            GetSymbol symbolForm = new GetSymbol(esriSymbologyStyleClass.esriStyleClassBorders );
            symbolForm.Text = "选择边框";
            IStyleGalleryItem styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassBorders);
            symbolForm.Dispose();
            if (styleGalleryItem == null) return;
            //Get the frame containing the focus map
            IFrameProperties frameProperties = (IFrameProperties)pPageLayoutControl.GraphicsContainer.FindFrame(pPageLayoutControl.ActiveView.FocusMap);
            frameProperties.Border = (IBorder )styleGalleryItem.Item;
            pPageLayoutControl.Refresh(esriViewDrawPhase.esriViewBackground, null, null);
        }

        #endregion
    }
}
