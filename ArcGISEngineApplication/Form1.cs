using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;




using System.IO;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;
using stdole;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.DisplayUI;






namespace ArcGISEngineApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
            InitializeComponent();
            this.axMapControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.
                IMapControlEvents2_Ax_OnDoubleClickEventHandler(this.axMapControl1_OnDoubleClick);

        }

        public static Color fontColor;
        public static IColor pColor;

        private string GeoOpType = string.Empty;
        private INewLineFeedback pNewLineFeedback;           //追踪线对象
        private INewPolygonFeedback pNewPolygonFeedback;     //追踪面对象
        private IPoint pPointPt = null;                      //鼠标点击点
        private double dToltalLength = 0;                    //量测总长度
        private double dSegmentLength = 0;                   //片段距离
        private string sMapUnits = "未知单位";               //地图单位变量
        private IPoint pMovePt = null;                       //鼠标移动时的当前点
        private IPointCollection pAreaPointCol = new MultipointClass();  //面积量算时画的点进行存储； 
        private object missing = Type.Missing;
        private IFeatureLayer pFeatureLayer = null;
        public IFeatureLayer pGlobalFeatureLayer;
        public ILayer player;
        private IGraphicsContainer pGraphicsContainer;
        private string xx, yy;                               //暂时性记录坐标信息
        private string addText;
        public static string pMouseOperator = null;
        private FormMeasureResult frmMeasureResult = null;
        private ITOCControlEvents_OnMouseDownEvent ted;

        private int CURRENT_LAYER_POINT = 1;               //记录当前选择的是点、线还是面图层
        private int CURRENT_LAYER_LINE = 2;
        private int CURRENT_LAYER_POLYGON = 3;
        public static string CURRENT_LAYER_NAME;               //记录当前选择的图层名
        private int DoQueryIndex;
        private IPointCollection pointcollection = new PolygonClass();
        private ContainsQuery containQuery=new ContainsQuery();
        private string mapDocumentName = string.Empty;



        public Form1(string text)
        {
            this.addText = text;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            axTOCControl1.SetBuddyControl(axMapControl1);
            axToolbarControl1.SetBuddyControl(axMapControl1);
        }

        #region 打开shp文件
        private void 地图加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDlg = new OpenFileDialog();
            OpenDlg.Title = "请选择地理数据文件";
            OpenDlg.Filter = "矢量数据文件（*.shp）|*.shp";
            OpenDlg.Multiselect = true;
            OpenDlg.ShowDialog();
            string[] strFileName = OpenDlg.FileNames;
            if (strFileName.Length > 0)
            {
                IAddGeoData pAddGeoData = new GeoMapAO();//实例化对象
                pAddGeoData.StrFileName = strFileName;
                pAddGeoData.AxMapControl1 = axMapControl1;
                pAddGeoData.AxMapControl2 = axMapControl2;
                pAddGeoData.AddGeoMap();
            }
        }
        #endregion

        #region 打开mxd文件
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog2;
            openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Title = "打开mxd文件";
            openFileDialog2.Filter = "Map Documents (*.mxd)|*.mxd";
            openFileDialog2.ShowDialog();
            string sFilePath = openFileDialog2.FileName;
            if (axMapControl1.CheckMxFile(sFilePath))
            {
                axMapControl1.MousePointer =
                esriControlsMousePointer.esriPointerHourglass;
                axMapControl1.LoadMxFile(sFilePath, 0, Type.Missing);
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                axMapControl2.LoadMxFile(sFilePath, 0, Type.Missing);

            }
            else
            {
                MessageBox.Show(sFilePath + " 不是mxd文件");
                return;
            }
        }
        #endregion

        #region 加载栅格文件
        private void 加载栅格文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.bmp|*.bmp|*.jpg|*.jpg|*.tif|*.tif";
            ofd.ShowDialog();
            string filePath = ofd.FileName;
            //此两个路径和文件名作为参数：
            string pathName = null;
            string fileName = null;
            try
            {
                pathName = System.IO.Path.GetDirectoryName(filePath);
                fileName = System.IO.Path.GetFileName(filePath);
                //定义工作空间工厂并实例化：
                IWorkspaceFactory pWSF;
                // pWSF = new RasterWorkspaceFactoryClass();
                pWSF = new RasterWorkspaceFactoryClass();
                IWorkspace pWS;
                pWS = pWSF.OpenFromFile(pathName, 0);
                IRasterWorkspace pRWS;
                pRWS = pWS as IRasterWorkspace;
                IRasterDataset pRasterDataset;
                pRasterDataset = pRWS.OpenRasterDataset(fileName);
                //影像金字塔的判断与创建
                IRasterPyramid pRasPyrmid;
                pRasPyrmid = pRasterDataset as IRasterPyramid;
                if (pRasPyrmid != null)
                {
                    if (!(pRasPyrmid.Present))
                    {
                        pRasPyrmid.Create();
                    }
                }

                IRaster pRaster;
                pRaster = pRasterDataset.CreateDefaultRaster();
                IRasterLayer pRasterLayer;
                pRasterLayer = new RasterLayerClass();
                pRasterLayer.CreateFromRaster(pRaster);
                ILayer pLayer = pRasterLayer as ILayer;
                axMapControl1.AddLayer(pLayer, 0);
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 保存文档
        private void 保存文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (axMapControl1.CheckMxFile(mapDocumentName))
            {

                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(mapDocumentName, string.Empty);


                if (mapDoc.get_IsReadOnly(mapDocumentName))
                {
                    MessageBox.Show("当前文档为只读型!");
                    mapDoc.Close();
                    return;
                }
                mapDoc.ReplaceContents((IMxdContents)axMapControl1.Map);

                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                mapDoc.Close();
            }
        }
        #endregion


        #region   mapcontrol的mouseDown事件
        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {


            //刷新地图			
            // axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);

            //屏幕坐标点转化为地图坐标点
            pPointPt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

            //左击显示坐标
            if (e.button == 1)
            {
                //MessageBox.Show(drawPoint+"      "+drawLine+"    "+drawPolygon);
                IActiveView pActiveView = axMapControl1.ActiveView;
                IEnvelope pEnvelope = new EnvelopeClass();

                switch (pMouseOperator)
                {
                    case "drawPoint":
                        IPoint pt;
                        pt = axMapControl1.ToMapPoint(e.x, e.y);

                        ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbolClass();
                        pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSX;
                        pMarkerSymbol.Color = GetRGB(200, 0, 0);
                        pMarkerSymbol.Angle = 30;
                        pMarkerSymbol.Size = 5;
                        pMarkerSymbol.Outline = true;
                        pMarkerSymbol.OutlineSize = 1;
                        IPoint pPoint = new PointClass();
                        pPoint.PutCoords(e.mapX, e.mapY);
                        object oMarkerSymbol = pMarkerSymbol;
                        axMapControl1.DrawShape(pPoint, ref oMarkerSymbol);
                        break;
                    case "drawLine":

                        IGeometry polyline;
                        polyline = axMapControl1.TrackLine();
                        ILineElement pLineElement;
                        pLineElement = new LineElementClass();
                        IElement pElement;
                        pElement = pLineElement as IElement;
                        pElement.Geometry = polyline;
                        pGraphicsContainer = axMapControl1.Map as IGraphicsContainer;
                        //pGraphicsContainer = pMap as IGraphicsContainer;
                        pGraphicsContainer.AddElement((IElement)pLineElement, 0);
                        axMapControl1.ActiveView.Refresh();
                        // pActiveView.Refresh();
                        break;
                    case "drawPolygon":
                        IGeometry Polygon;
                        Polygon = axMapControl1.TrackPolygon();
                        IPolygonElement PolygonElement;
                        PolygonElement = new PolygonElementClass();
                        IElement pElement1;
                        pElement1 = PolygonElement as IElement;
                        pElement1.Geometry = Polygon;
                        pGraphicsContainer = axMapControl1.Map as IGraphicsContainer;
                        pGraphicsContainer.AddElement((IElement)PolygonElement, 0);
                        pActiveView.Refresh();
                        break;
                    case "drawPolygons":
                       
                         DoQueryIndex =getFeatureTypeByName(CURRENT_LAYER_NAME);
                        if (DoQueryIndex == 1)
                        {
                          IFeatureLayer pFeatLayer = axMapControl1.get_Layer(getIndexByName(CURRENT_LAYER_NAME))
                                as IFeatureLayer;
                            IPoint point = new PointClass();
                            point.PutCoords(e.mapX, e.mapY);

                            ISpatialFilter spatialFilter = new SpatialFilterClass();
                            spatialFilter.Geometry = point;
                            spatialFilter.SpatialRel = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelIntersects;
                            IFeatureCursor featureCursor = pFeatLayer.Search(spatialFilter, false);

                            IFeature pFeature1 = featureCursor.NextFeature();

                            while (pFeature1 != null)
                            {
                                axMapControl1.FlashShape(pFeature1.Shape);
                                pFeature1 = featureCursor.NextFeature();
                            }


                        }
                        else if (DoQueryIndex == 2)
                        { 
                        }

                        else if (DoQueryIndex == 3)
                        {
                            
                            IPoint point = new PointClass();
                            point.PutCoords(e.mapX, e.mapY);
                            pointcollection.AddPoints(1, ref point);
                            if (pointcollection.PointCount > 2)
                            {
                                DrawPolygon(pointcollection, axMapControl1);
                            }
                        }
                        break;


                    case "spaticalQuery":
                        /* IPoint pt1;
                    pt1 = axMapControl1.ToMapPoint(e.x, e.y);
                        IRgbColor rgb=new RgbColorClass();
                        rgb.Red=255;
                        rgb.Blue=0;
                        rgb.Green=0;
                        DrawCircle_Graphics(pt1,6,rgb);*/


                        /* polyline = axMapControl1.TrackLine();
                  
                         pLineElement = new LineElementClass();
                  
                         pElement = pLineElement as IElement;
                         pElement.Geometry = polyline;
                         pGraphicsContainer = axMapControl1.Map as IGraphicsContainer;
                         //pGraphicsContainer = pMap as IGraphicsContainer;
                         pGraphicsContainer.AddElement((IElement)pLineElement, 0);
                         axMapControl1.ActiveView.Refresh();
                         ISpatialFilter pSFilter = new SpatialFilterClass();*/
                        //pSFilter.Geometry = pLineElement;

                        break;
                    case "selectPolygon":
                        //实例化一个点
                        IPoint pPoint1 = new PointClass();
                        //以该点作拓扑算子
                        ITopologicalOperator pTopologicalOperator = pPoint1 as ITopologicalOperator;
                        //将点击的位置坐标赋予pPoint
                        pPoint1.PutCoords(e.mapX, e.mapY);
                        //以缓冲半径为0进行缓冲  得到一个点
                        IGeometry pGeometry = pTopologicalOperator.Buffer(0);
                        //以该点进行要素选择（只能选中面状要素，点和线无法选中）
                        axMapControl1.Map.SelectByShape(pGeometry, null, false);
                        //刷新视图
                        axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

                        // 获取选择集
                        ISelection pSelection = axMapControl1.Map.FeatureSelection;
                        // 打开属性标签
                        IEnumFeatureSetup pEnumFeatureSetup = pSelection as IEnumFeatureSetup;
                        pEnumFeatureSetup.AllFields = true;
                        // 获取要素
                        IEnumFeature pEnumFeature = pSelection as IEnumFeature;
                        IFeature pFeature = pEnumFeature.Next();


                        while (pFeature != null)
                        {
                            double area = 0;
                            double mu = 0;
                            if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                            {
                                //计算面积
                                IArea pArea = pFeature.Shape as IArea;
                                area = area + pArea.Area;//得到的面积单位是平方米
                                mu = area * 0.0015;//转换为亩
                            }
                            break;
                        }
                        break;



                    #region 距离量测
                    case "MeasureLength":
                        //判断追踪对象是否为空，若为空则进行实例化并设置当前鼠标点的起始点
                        if (pNewLineFeedback == null)
                        {
                            //实例化追踪线对象 
                            pNewLineFeedback = new NewLineFeedbackClass();
                            pNewLineFeedback.Display = (axMapControl1.Map as IActiveView).ScreenDisplay;
                            //设置起点，开始动态线绘制
                            pNewLineFeedback.Start(pPointPt);
                            dToltalLength = 0;
                        }
                        else //如果追踪对象不为空，则添加当前鼠标点
                        {
                            pNewLineFeedback.AddPoint(pPointPt);
                        }

                        if (dSegmentLength != 0)
                        {
                            dToltalLength = dToltalLength + dSegmentLength;
                        }
                        break;
                    #endregion

                    #region 面积量算
                    case "MeasureArea":
                        if (pNewPolygonFeedback == null)
                        {
                            //实例化追踪面对象
                            pNewPolygonFeedback = new NewPolygonFeedback();
                            pNewPolygonFeedback.Display = (axMapControl1.Map as IActiveView).ScreenDisplay;
                            pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount);
                            //开始绘制多边形
                            pNewPolygonFeedback.Start(pPointPt);
                            pAreaPointCol.AddPoint(pPointPt, ref missing, ref missing);
                        }
                        else
                        {
                            pNewPolygonFeedback.AddPoint(pPointPt);
                            pAreaPointCol.AddPoint(pPointPt, ref missing, ref missing);
                        }
                        break;
                    #endregion


                    #region 选择要素
                    case "SelFeature":
                        //MessageBox.Show("gggggggggggg");
                        IEnvelope pEnv = axMapControl1.TrackRectangle();
                        IGeometry pGeo = pEnv as IGeometry;
                        //矩形框若为空，即为点选时，对点范围进行扩展
                        if (pEnv.IsEmpty == true)
                        {
                            tagRECT r;
                            r.left = e.x - 5;
                            r.top = e.y - 5;
                            r.right = e.x + 5;
                            r.bottom = e.y + 5;
                            pActiveView.ScreenDisplay.DisplayTransformation.TransformRect(pEnv, ref r, 4);
                            pEnv.SpatialReference = pActiveView.FocusMap.SpatialReference;
                        }
                        pGeo = pEnv as IGeometry;
                        axMapControl1.Map.SelectByShape(pGeo, null, false);
                        axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);




                        break;

                    #endregion


                    case "addText":
                        IGraphicsContainer pGraphicsContainer2 = axMapControl1.Map as IGraphicsContainer;
                        ITextElement pTextEle = new TextElementClass();
                        InputText inputText = new InputText();
                        pTextEle.Text = InputText.inputText;
                        ITextSymbol pTextSymbol = new TextSymbolClass();
                        pTextSymbol.Size = 30;
                        pTextSymbol.Color = GetRGB(0, 0, 0);
                        pTextEle.Symbol = pTextSymbol;
                        IPoint pPoint2 = new PointClass();
                        pPoint2.PutCoords(e.mapX, e.mapY);
                        IElement pElement2 = pTextEle as IElement;
                        pElement2.Geometry = pPoint2;
                        pGraphicsContainer2.AddElement(pElement2, 0);
                        axMapControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);
                        pMouseOperator = "";
                        break;
                    default:
                        //改变地图控件显示范围为当前拖曳的区域
                        axMapControl1.Extent = axMapControl1.TrackRectangle();
                        break;

                }
            }
            else if (e.button == 2)
            {

                contextMenuStrip1.Show(System.Windows.Forms.Control.MousePosition.X,
                    System.Windows.Forms.Control.MousePosition.Y);
                xx = e.mapX.ToString();
                yy = e.mapY.ToString();
            }
        }
        #endregion

        #region 画面
        private void DrawPolygon(IPointCollection pPointCollection, AxMapControl axmapcontrol1)
        {
            IPolygon ppolygon;
            ppolygon = (IPolygon)pPointCollection;
            axmapcontrol1.DrawShape(ppolygon);
        }
        #endregion 

        #region   mapcontrol1的MapReplaced事件
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            mapDocumentName = axMapControl1.DocumentFilename;
            IEagOpt pEagOpt = new GeoMapAO();
            pEagOpt.AxMapControl1 = axMapControl1;
            pEagOpt.AxMapControl2 = axMapControl2;
            pEagOpt.NewGeomap();
            copyToPageLayout();
        }
        #endregion

        #region axmapcontrol2 mousedown 事件
        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IEagOpt pEagOpt = new GeoMapAO();
            pEagOpt.AxMapControl1 = axMapControl1;
            pEagOpt.AxMapControl2 = axMapControl2;
            pEagOpt.Ed = e;
            pEagOpt.MoveEagl();

        }
        #endregion

        #region axmapcontrol2 ExtentUpdated 事件
        private void axMapControl2_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            IEagOpt pEagOpt = new GeoMapAO();
            pEagOpt.AxMapControl1 = axMapControl1;
            pEagOpt.AxMapControl2 = axMapControl2;
            pEagOpt.Eu = e;
            pEagOpt.DrawrRec();
        }
        #endregion

        #region toccontrol右键
        public ITOCControlEvents_OnMouseDownEvent Ted
        {
            get { return ted; }
            set { ted = value; }
        }
        #endregion


        #region 根据图层名获取图层类型（点、线、面）
        private int getFeatureTypeByName(string layerName)
        { 
            int type;
            int i = getIndexByName(layerName);
            pFeatureLayer = axMapControl1.Map.get_Layer(i) as IFeatureLayer;
           
            switch(pFeatureLayer.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    type= CURRENT_LAYER_POINT;
                    break;
                case esriGeometryType.esriGeometryLine:
                   type=  CURRENT_LAYER_LINE;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    type=CURRENT_LAYER_POLYGON;
                    break;
                default :
                    type = 0;
                    break;
            }
            return type;
        }
         #endregion

        #region 根据图层名获取图层的索引号
       
        private int getIndexByName(string layerName)
        {
            for (int i = 0; i != axMapControl1.Map.LayerCount; ++i)
            {
                if (axMapControl1.get_Layer(i).Name == layerName)
                {
                    return i;
                }

            }
            return -1;
        }
        #endregion

        #region axtoccontrol1 的mouse down事件
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem mItem = new esriTOCControlItem();
            IBasicMap pMap = new MapClass();
            player = new FeatureLayerClass();
            object pOther = new object();
            object pIndex = new object();
            axTOCControl1.HitTest(e.x, e.y, ref mItem, ref pMap, ref player, ref pOther, ref pIndex);

            if (e.button == 2)
            {
                if (axMapControl1.LayerCount > 0)//主视图中要有地理数据 
                {


                    switch (mItem)
                    {
                        case esriTOCControlItem.esriTOCControlItemLegendClass:
                            // MessageBox.Show(pLayer.Name);
                            break;
                        case esriTOCControlItem.esriTOCControlItemLayer:
                            //MessageBox.Show(pLayer.Name);
                            // contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
                            // MessageBox.Show("点击图层的索引是："+pIndex+";图层名是"+);
                            contextMenuStrip2.Show(System.Windows.Forms.Control.MousePosition.X,
                    System.Windows.Forms.Control.MousePosition.Y);

                            break;
                        default:
                            break;

                    }

                }

            }
            else if (e.button == 1)
            {
                //点击图层的符号可以进行更改

              

                if (mItem == esriTOCControlItem.esriTOCControlItemLegendClass)
                {


                    ILegendClass plc = new LegendClassClass();
                    ILegendGroup plg = new LegendGroupClass();

                    if (pOther is ILegendGroup)
                    {
                        plg = (ILegendGroup)pOther;
                    }
                    plc = plg.get_Class((int)pIndex);
                    ISymbol pSym;
                    pSym = plc.Symbol;

                    ISymbolSelector pSymbolSelector = new SymbolSelectorClass();
                    pSymbolSelector.AddSymbol(pSym);
                    if (pSymbolSelector.SelectSymbol(0))
                    {
                        plc.Symbol = pSymbolSelector.GetSymbolAt(0);
                    }
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Refresh();


                }
            }
            axMapControl2.ActiveView.Refresh();
        }
        #endregion

        #region 全屏显示
        private void 全屏显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.Extent = axMapControl1.FullExtent;

        }
        #endregion

        #region axMapControl1_OnFullExtentUpdated事件
        //实现当主图幅范围发生变化时候，状态也相应显示当前图幅信息
        private void axMapControl1_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
        {
            IPoint l1, ur;
            l1 = axMapControl1.Extent.LowerLeft;
            ur = axMapControl1.Extent.UpperRight;
            toolStripStatusLabel3.Text = "(" + Convert.ToString(l1.X) + "," + Convert.ToString(l1.Y) + ")";
            toolStripStatusLabel4.Text = "(" + ur.X.ToString("0.00") + "," + ur.Y.ToString("0.00") + ")";
        }
        #endregion

        #region 打开属性表
        private void 属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 FT = new Form2(player as ILayer, axMapControl1.Map);
            FT.Show();
        }
        #endregion

        #region 移除图层
        //通过for循环得到选中图层索引，并直接调用DeleteLayer方法定点删除
        private void 移除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                if (axMapControl1.get_Layer(i) == player)
                {
                    axMapControl1.DeleteLayer(i);
                }
                if (axMapControl2.get_Layer(i) == player)
                {
                    axMapControl2.DeleteLayer(i);
                }
            }
            axMapControl1.ActiveView.Refresh();
            axMapControl2.ActiveView.Refresh();
        }
        #endregion

        #region axTOCControl1_OnMouseUp事件
        private void axTOCControl1_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            axMapControl2.ActiveView.Refresh();
        }
        #endregion

        #region axMapControl1_OnMouseMove事件
        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            sMapUnits = GetMapUnit(axMapControl1.Map.MapUnits);
            pMovePt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

            #region 长度量算
            if (pMouseOperator == "MeasureLength")
            {
                if (pNewLineFeedback != null)
                {
                    pNewLineFeedback.MoveTo(pMovePt);
                }


                double deltax = 0;//两点之间x差值
                double deltay = 0;//两点之间y差值

                if ((pPointPt != null) && (pNewLineFeedback != null))
                {
                    deltax = pMovePt.X - pPointPt.X;
                    deltay = pMovePt.Y - pPointPt.Y;
                    dSegmentLength = Math.Round(Math.Sqrt((deltax * deltax) + (deltay * deltay)), 3);
                    dToltalLength = dToltalLength + dSegmentLength;
                    if (frmMeasureResult != null)
                    {
                        frmMeasureResult.lblMeasureResult.Text = String.Format(
                            "当前线段长度：{0:.###}{1};\r\n总长度为: {2:.###}{1}",
                            dSegmentLength, sMapUnits, dToltalLength);
                        dToltalLength = dToltalLength - dSegmentLength; //鼠标移动到新点重新开始计算
                    }
                    frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmClosed);
                }
            }
            #endregion

            #region 面积量算
            if (pMouseOperator == "MeasureArea")
            {
                if (pNewPolygonFeedback != null)
                {
                    pNewPolygonFeedback.MoveTo(pMovePt);
                }

                IPointCollection pPointCol = new Polygon();
                IPolygon pPolygon = new PolygonClass();
                IGeometry pGeo = null;

                ITopologicalOperator pTopo = null;
                for (int i = 0; i <= pAreaPointCol.PointCount - 1; i++)
                {
                    pPointCol.AddPoint(pAreaPointCol.get_Point(i), ref missing, ref missing);
                }
                pPointCol.AddPoint(pMovePt, ref missing, ref missing);

                if (pPointCol.PointCount < 3) return;
                pPolygon = pPointCol as IPolygon;

                if ((pPolygon != null))
                {
                    pPolygon.Close();
                    pGeo = pPolygon as IGeometry;
                    pTopo = pGeo as ITopologicalOperator;
                    //使几何图形的拓扑正确
                    pTopo.Simplify();
                    pGeo.Project(axMapControl1.Map.SpatialReference);
                    IArea pArea = pGeo as IArea;

                    frmMeasureResult.lblMeasureResult.Text = String.Format(
                        "总面积为：{0:.####}平方{1};\r\n总长度为：{2:.####}{1}",
                        pArea.Area, sMapUnits, pPolygon.Length);
                    pPolygon = null;
                }
            }
            #endregion
        }
        #endregion



        #region 获取坐标信息
        private void 获取坐标信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("坐标X为：" + xx + "      坐标Y为：" + yy, "提示，如果原数据未定义坐标系，那么这个提示框显示的坐标有误");
        }
        #endregion


        #region 距离量测
        private void 距离量测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperator = "MeasureLength";

            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            if (frmMeasureResult == null || frmMeasureResult.IsDisposed)
            {
                frmMeasureResult = new FormMeasureResult();
                frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmClosed);
                frmMeasureResult.Text = "距离测量：";
                frmMeasureResult.Show();
            }
            else
            {
                frmMeasureResult.Activate();
            }
        }
        #endregion


        #region 测量结果窗口关闭响应事件
        private void frmMeasureResult_frmClosed()
        {


            //清空线对象
            if (pNewLineFeedback != null)
            {
                pNewLineFeedback.Stop();
                pNewLineFeedback = null;
            }
            //清空面对象 
            if (pNewPolygonFeedback != null)
            {
                pNewPolygonFeedback.Stop();
                pNewPolygonFeedback = null;
                pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount);//清空点集中所有的点

            }
            //清空量算的线、面对象
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            //结束量算功能
            pMouseOperator = string.Empty;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;

        }
        #endregion

        bool first = true;
        #region axMapControl1_OnDoubleClick事件
        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            #region 长度量算
            if (pMouseOperator == "MeasureLength")
            {
                if (frmMeasureResult != null)
                {
                    frmMeasureResult.lblMeasureResult.Text = "线段总长度为：" + dToltalLength + sMapUnits;
                }
                if (pNewLineFeedback != null)
                {
                    pNewLineFeedback.Stop();
                    pNewLineFeedback = null;
                    //清空所画的线对象
                    (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                dToltalLength = 0;
                dSegmentLength = 0;
            }
            #endregion

            #region 面积量算
            if (pMouseOperator == "MeasureArea")
            {
                if (pNewPolygonFeedback != null)
                {
                    pNewPolygonFeedback.Stop();
                    pNewPolygonFeedback = null;
                    //清空所画的线对象
                    (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount); //清空点集中所有点
            }
            #endregion
          

            if (pointcollection != null)
            {
                if (DoQueryIndex == 3)
                {
                    if (!first)
                        return;
                    first = false;
                    MessageBox.Show("DoQueryIndex == 3");
                    IFeatureLayer pFeaturelayer = axMapControl1.get_Layer(getIndexByName(CURRENT_LAYER_NAME)) 
                        as IFeatureLayer;
                    ISpatialFilter sptiafilter = new SpatialFilterClass();
                    sptiafilter.Geometry = pointcollection as IPolygon;
                    sptiafilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    IFeatureCursor featurecursor = pFeaturelayer.Search(sptiafilter, false);

                    /*IFeature pfeature = featurecursor.NextFeature();
                    int count = 0;
                    while (pfeature != null)
                    {
                        count++;
                        pfeature = featurecursor.NextFeature();
                    } 
                     count = 0;*/     
                    //MessageBox.Show("共查询到" + count + "个元素", "查询结果");

                    containQuery.Visible = true;
                    IFeatureClass pFeatureClass = pFeaturelayer.FeatureClass;
                    containQuery.display(featurecursor,pFeatureClass);
                    pointcollection = new PolygonClass();
                }
            }        
        }
        #endregion


        #region 点击完成退出当前操作
        private void 完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (pMouseOperator != "SelFeature")
            {
                pMouseOperator = null;
            }
            //结束线段测量功能
            if (pNewLineFeedback != null)
            {
                pNewLineFeedback.Stop();
                pMouseOperator = null;

            }
            //结束面积测量
            if (pNewPolygonFeedback != null)
            {
                pNewPolygonFeedback.Stop();
                pMouseOperator = null;
            }
        }
        #endregion


        #region 面积量测
        private void 面积量测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperator = "MeasureArea";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            if (frmMeasureResult == null || frmMeasureResult.IsDisposed)
            {
                frmMeasureResult = new FormMeasureResult();

                frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmClosed);
                frmMeasureResult.lblMeasureResult.Text = "";
                frmMeasureResult.Text = "面积量测";
                frmMeasureResult.Show();
            }
            else
            {
                frmMeasureResult.Activate();
            }
        }
        #endregion


        #region 要素选择
        private void 要素选择ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            ControlsSelectFeaturesTool pTools = new ControlsSelectFeaturesToolClass();
            pTools.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pTools as ITool;
            pMouseOperator = "SelFeature";

        }
        #endregion


        #region 缩放至选择
        private void 缩放至选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nSlection = axMapControl1.Map.SelectionCount;
            if (nSlection == 0)
            {
                MessageBox.Show("请先选择要素！", "提示");
            }
            else
            {
                ISelection selection = axMapControl1.Map.FeatureSelection;
                IEnumFeature enumFeature = (IEnumFeature)selection;
                enumFeature.Reset();
                IEnvelope pEnvelope = new EnvelopeClass();
                IFeature pFeature = enumFeature.Next();
                while (pFeature != null)
                {
                    pEnvelope.Union(pFeature.Extent);
                    pFeature = enumFeature.Next();
                }
                pEnvelope.Expand(1.1, 1.1, true);
                axMapControl1.ActiveView.Extent = pEnvelope;
                axMapControl1.ActiveView.Refresh();
            }
        }
        #endregion


        #region 清除选择
        private void 清除选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IActiveView pActiveView = axMapControl1.ActiveView;
            pActiveView.FocusMap.ClearSelection();
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, pActiveView.Extent);
        }
        #endregion


        #region 将mapcontrol里面地图copy到pagelayout里面
        private void copyToPageLayout()
        {
            //IObjectCopy接口提供Copy方法用于地图的复制
            IObjectCopy objectCopy = new ObjectCopyClass();
            object copyFromMap = axMapControl1.Map;
            object copyMap = objectCopy.Copy(copyFromMap);
            object copyToMap = axPageLayoutControl1.ActiveView.FocusMap;
            //Overwrite方法用于地图写入PageLayoutControl控件的视图中
            objectCopy.Overwrite(copyMap, ref copyToMap);
        }
        #endregion


        #region axMapControl1_OnAfterScreenDraw事件
        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            IActiveView activeView = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;
            IDisplayTransformation displayTransformation = activeView.ScreenDisplay.DisplayTransformation;
            //根据MapControl的视图范围,确定PageLayoutControl的视图范围
            displayTransformation.VisibleBounds = axMapControl1.Extent;
            axPageLayoutControl1.ActiveView.Refresh();
            copyToPageLayout();
        }
        #endregion


        #region 添加图例
        private void 图例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGraphicsContainer graphicsContainer = axPageLayoutControl1.GraphicsContainer;
            IMapFrame mapFrame = (IMapFrame)graphicsContainer.FindFrame(axPageLayoutControl1.ActiveView.FocusMap);
            if (mapFrame == null) return;
            UID uID = new UIDClass();
            uID.Value = "esriCarto.Legend";
            IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(uID, null);
            if (mapSurroundFrame == null) return;
            if (mapSurroundFrame.MapSurround == null) return;
            mapSurroundFrame.MapSurround.Name = "Legend";

            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(1, 1, 3.4, 2.4);

            IElement element = (IElement)mapSurroundFrame;
            element.Geometry = envelope;

            axPageLayoutControl1.AddElement(element, Type.Missing, Type.Missing, "Legend", 0);
            axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        #endregion


        #region 添加指北针
        private void 指北针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IElement pElement = axPageLayoutControl1.FindElementByName("MarkerNorthArrow");

            //如果存在就先删除
            if (pElement != null)
            {
                axPageLayoutControl1.ActiveView.GraphicsContainer.DeleteElement(pElement);
            }
            IPageLayout pPageLayout = axPageLayoutControl1.PageLayout;
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            IActiveView pActiveView = pPageLayout as IActiveView;
            UID pID = new UIDClass()
;
            pID.Value = "esriCore.MarkerNorthArrow";


            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveView.FocusMap) as IMapFrame;

            if (pMapFrame == null)
            {
                return;
            }
            IMapSurroundFrame pMapSurroundFrame = pMapFrame.CreateSurroundFrame(pID, null);

            if (pMapSurroundFrame == null)
            {
                return;
            }

            IEnvelope pEnv = axPageLayoutControl1.Page.PrintableBounds;//这里只是为了初始化一个Envelope
            //在这里对enlp进行设置，其将作为指北针的外框，这样就可以控制其大小和位置
            //如下面的设置是调整指北针的位置
            IEnvelope pageEnlp = axPageLayoutControl1.Page.PrintableBounds;
            pEnv.XMin = pageEnlp.XMin + pageEnlp.Width * 0.8;
            pEnv.XMax = pageEnlp.XMin + pageEnlp.Width * 0.8;
            pEnv.YMax = pageEnlp.YMax - pageEnlp.Height * 0.053;
            pEnv.YMin = pageEnlp.YMin + pageEnlp.Height * 0.8;

            pElement = (IElement)pMapSurroundFrame;
            pElement.Geometry = pEnv;
            pMapSurroundFrame.MapSurround.Name = "MarkerNorthArrow";
            INorthArrow pNorthArrow = pMapSurroundFrame.MapSurround as INorthArrow;

            pGraphicsContainer.AddElement(pElement, 0);
            axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }
        #endregion


        #region 添加图名
        private void 图名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pMouseOperator = "addMapName";
        }
        #endregion


        #region 添加文字要素
        public void AddTextElement(AxPageLayoutControl PageLayoutControl, double x, double y, string textName)
        {
            IPageLayout pPageLayout;
            IActiveView pAV;
            IGraphicsContainer pGraphicsContainer;
            IPoint pPoint;
            ITextElement pTextElement;
            IElement pElement;
            ITextSymbol pTextSymbol;
            IRgbColor pColor;
            pPageLayout = PageLayoutControl.PageLayout;
            pAV = (IActiveView)pPageLayout;
            pGraphicsContainer = (IGraphicsContainer)pPageLayout;
            pTextElement = new TextElementClass();

            IFontDisp pFont = new StdFontClass() as IFontDisp;
            pFont.Bold = true;
            pFont.Name = "宋体";
            pFont.Size = 13;

            pColor = new RgbColorClass();
            pColor.Red = 255;

            pTextSymbol = new TextSymbolClass();
            pTextSymbol.Color = (IColor)pColor;
            pTextSymbol.Font = pFont;

            pTextElement.Text = textName;
            pTextElement.Symbol = pTextSymbol;

            pPoint = new PointClass();
            pPoint.X = x;
            pPoint.Y = y;

            pElement = (IElement)pTextElement;
            pElement.Geometry = (IGeometry)pPoint;
            pGraphicsContainer.AddElement(pElement, 0);

            pAV.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }
        #endregion


        #region axPageLayoutControl1_OnMouseDown事件
        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 1)
            {

                if (pMouseOperator.Equals("addMapName"))
                {
                    AddTitle(InputMapName.mapName, e);
                    pMouseOperator = "";
                }
                else if (pMouseOperator.Equals(""))
                {

                }

            }
        }
        #endregion


        #region 在pagelayout上添加图名
        public void AddTitle(String s, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            //找到PageLayout            
            IPageLayout pPageLayout = axPageLayoutControl1.PageLayout;
            //找到元素容器           
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            //创建元素            
            ITextElement pTextElement = new TextElementClass();
            pTextElement.Text = s;
            ITextSymbol pTextSymbol = new TextSymbolClass();
            //Text的符号样式      
            pTextSymbol.Size = 30;
            // pTextSymbol.Font = ESRI.ArcGIS.ADF.COMSupport.OLE. GetIFontDispFromFont(InputMapName.font) as stdole.IFontDisp;
            //  pTextSymbol.Color = GetRGB(fontColor.R,fontColor.G,fontColor.B);     
            pTextSymbol.Color = pColor;
            pTextElement.Symbol = pTextSymbol;
            //设置位置                   
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(e.pageX, e.pageY);
            IElement pElement = pTextElement as IElement;
            pElement.Geometry = pPoint;
            //将元素添加到容器中         
            pGraphicsContainer.AddElement(pElement, 0);
            //刷新          
            axPageLayoutControl1.Refresh();
        }
        #endregion


        #region 添加比例尺
        private void 比例尺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.Geometry.IEnvelope envelope = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            AddScaleBar();
        }
        #endregion


        #region 添加比例尺
        private void AddScaleBar()
        {
            IScaleBar pScaleBar;
            IMapSurround pMapSurround;
            IElementProperties pElementPro;
            UID pUID = new UIDClass();
            pUID.Value = "esriCarTO.scalebar";
            IPageLayout pPageLayout = axPageLayoutControl1.PageLayout;
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            IMap pMap = pActiveView.FocusMap;

            //获得与地图相关的mapframe
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            //产生一个mapsurroundframe
            IMapSurroundFrame pMapSurroundFrame = pMapFrame.CreateSurroundFrame(pUID, null);
            pScaleBar = new AlternatingScaleBarClass();
            //设置比例尺属性
            pScaleBar.Division = 4;
            pScaleBar.Divisions = 4;
            pScaleBar.LabelGap = 4;
            pScaleBar.LabelPosition = esriVertPosEnum.esriAbove;
            pScaleBar.Map = pMap;
            pScaleBar.Name = "";
            pScaleBar.Subdivisions = 2;
            pScaleBar.UnitLabel = "";
            pScaleBar.UnitLabelGap = 4;
            pScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarAbove;
            pScaleBar.Units = esriUnits.esriKilometers;
            pMapSurround = pScaleBar;
            pMapSurroundFrame.MapSurround = pMapSurround;
            pElementPro = pMapSurroundFrame as IElementProperties;
            pElementPro.Name = "myscalebar";
            //这里只是为了初始化一个Envelope
            IEnvelope pEnv = axPageLayoutControl1.Page.PrintableBounds;

            //如下面的设置是调整比例尺的位置
            IEnvelope pageEnlp = axPageLayoutControl1.Page.PrintableBounds;
            // pEnv.XMin = pageEnlp.XMin + pageEnlp.Width * 0.7;
            //pEnv.XMax = pageEnlp.XMin + pageEnlp.Width * 0.8;
            // pEnv.YMax = pageEnlp.YMax +pageEnlp.Height * 0.4;
            //pEnv.YMin = pageEnlp.YMin + pageEnlp.Height * 0.2;

            pEnv.PutCoords(1, 1, 3.4, 2.4);

            //将mapframe添加到控件中
            axPageLayoutControl1.AddElement(pMapSurroundFrame as IElement, pEnv, Type.Missing, Type.Missing, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        #endregion


        #region 按条件查询
        private void 按条件查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // string sqlfilter = "FID>5";
            //SearchFeature(sqlfilter, player as IFeatureLayer);
            SelectByAttribute selectByAttribute = new SelectByAttribute(player);
            selectByAttribute.Show();

        }
        #endregion


        public void flashShape(IFeatureCursor pFeatCursor)
        {
            IFeatureLayer pFeatureLayer = axMapControl1.Map.get_Layer(0) as IFeatureLayer;

            IFeature pFeature = pFeatCursor.NextFeature();
            while (pFeature != null)
            {
                MessageBox.Show("2222");
                axMapControl1.Map.SelectFeature(pFeatureLayer, pFeature);
                MessageBox.Show("1111");
                //pFeature = pFeatCursor.NextFeature();
            }

            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
        }


        #region 添加点
        private void 添加点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pMouseOperator = "drawPoint";
        }
        #endregion


        #region 添加线
        private void 添加线ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pMouseOperator = "drawLine";
        }
        #endregion


        #region 添加面
        private void 添加面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pMouseOperator = "drawPolygon";

        }
        #endregion


        #region 显示所有字段
        private void 显示所有字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fieldName = null;
            IFeatureLayer featureLayer = (IFeatureLayer)player as IFeatureLayer;
            ILayerFields layerFields = (ILayerFields)featureLayer;
            for (int i = 0; i < layerFields.FieldCount; i++)
            {
                IField field = layerFields.get_Field(i);
                fieldName = field.Name + "、" + fieldName;
            }
            MessageBox.Show(fieldName, "字段名");
        }
        #endregion


        #region 添加文字
        private void 添加文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //axMapControl1.CurrentTool = null;
            //ControlsSelectFeaturesTool pTools = new ControlsSelectFeaturesToolClass();
            // pTools.OnCreate(axMapControl1.Object);
            // axMapControl1.CurrentTool = pTools as ITool;
            pMouseOperator = "addText";

            InputText inputText = new InputText();
            inputText.Show();
        }
        #endregion


        #region 添加图名
        private void 添加图名ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pMouseOperator = "addMapName";
            InputMapName inputMapName = new InputMapName();
            inputMapName.ShowDialog();

        }
        #endregion


        #region 按条件查询
        private void SearchFeature(string sqlfiliilter, IFeatureLayer pFeatureLayer)
        {
            IFeatureLayer pFeatLyr = pFeatureLayer;//得到进行查询的图层
            IQueryFilter pFilter = new QueryFilter();
            pFilter.WhereClause = sqlfiliilter;//添加过滤参数
            IFeatureCursor pFeatCursor = pFeatureLayer.Search(pFilter, true);
            IFeature pFeat = pFeatCursor.NextFeature();
            while (pFeat != null)
            {
                pFeat = pFeatCursor.NextFeature();
                if (pFeat != null)
                {
                    ISimpleFillSymbol pFillsyl = new SimpleFillSymbolClass();
                    pFillsyl.Color = GetRGB(220, 100, 50);
                    object oFillsyl = pFillsyl;
                    IPolygon pPolygon = pFeat.Shape as IPolygon;
                    if (pPolygon == null)
                    {
                        MessageBox.Show("这个图层里面没有polygon，建议查询四川县界图层", "提示");
                        return;
                    }
                    else
                    {
                        axMapControl1.FlashShape(pPolygon, 20, 1, pFillsyl);
                        //显示出被选中的要素
                        axMapControl1.DrawShape(pPolygon, ref oFillsyl);
                    }

                }
            }

        }
        #endregion


        # region 获取地图单位
        private string GetMapUnit(esriUnits _esriMapUnit)
        {
            string sMapUnits = string.Empty;
            switch (_esriMapUnit)
            {
                case esriUnits.esriCentimeters:
                    sMapUnits = "厘米";
                    break;
                case esriUnits.esriDecimalDegrees:
                    sMapUnits = "十进制";
                    break;
                case esriUnits.esriDecimeters:
                    sMapUnits = "分米";
                    break;
                case esriUnits.esriFeet:
                    sMapUnits = "尺";
                    break;
                case esriUnits.esriInches:
                    sMapUnits = "英寸";
                    break;
                case esriUnits.esriKilometers:
                    sMapUnits = "千米";
                    break;
                case esriUnits.esriMeters:
                    sMapUnits = "米";
                    break;
                case esriUnits.esriMiles:
                    sMapUnits = "英里";
                    break;
                case esriUnits.esriMillimeters:
                    sMapUnits = "毫米";
                    break;
                case esriUnits.esriNauticalMiles:
                    sMapUnits = "海里";
                    break;
                case esriUnits.esriPoints:
                    sMapUnits = "点";
                    break;
                case esriUnits.esriUnitsLast:
                    sMapUnits = "UnitsLast";
                    break;
                case esriUnits.esriUnknownUnits:
                    sMapUnits = "未知单位";
                    break;
                case esriUnits.esriYards:
                    sMapUnits = "码";
                    break;
                default:
                    break;
            }
            return sMapUnits;
        }
        #endregion


        #region GETRGB
        public IRgbColor GetRGB(int r, int g, int b)
        {
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = r;
            pColor.Green = g;
            pColor.Blue = b;
            return pColor;
        }
        #endregion


        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 添加到要素集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* IWorkspaceFactory pWorkspaceFactory = new AccessWorkspaceFactoryClass();
             IFeatureWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(
                 @"D:\ceshi\C#2012\ArcGISEngineApplication (8)\ArcGISEngineApplication\CeShi.mdb", 0)
                 as IFeatureWorkspace;

             //得到名为ceshi的数据集
             IFeatureDataset pFeatureDataset = pWorkspace.OpenFeatureDataset("ceshi");
             (axMapControl1.get_Layer(1) as IDatasetContainer).AddDataset(pFeatureDataset);
          
           

             //遍历要素数据集中的子类，也就是要素数据集
             IEnumDataset pEnumDataset = pFeatureDataset.Subsets;

             pEnumDataset.Reset();
             IDataset pDataset = pEnumDataset.Next();
             while (pDataset != null)
             {
                 MessageBox.Show(pDataset.Name + "type:" + pDataset.Type);
                 pDataset = pEnumDataset.Next();
             }*/

        }

        private void 根据图层名获取到索引ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = null;
            IFeatureLayer pFeatureLayer = player as IFeatureLayer;    //强转为IFeatureLayer对象
            string layerName = pFeatureLayer.FeatureClass.AliasName;   //获取别名
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                content = content + ";" + axMapControl1.get_Layer(i).Name;
                if (axMapControl1.get_Layer(i).Name.Equals(layerName))
                {
                    MessageBox.Show("图层名为：" + layerName + ";索引为：" + i);
                }
            }
        }

        private void 遍历要素类ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string wsName = WsPath();
            if (wsName != null)
            {
                try
                {
                    string content = null;
                    IWorkspaceFactory pWsFt = new AccessWorkspaceFactoryClass();
                    IWorkspace pWs = pWsFt.OpenFromFile(wsName, 0);
                    IEnumDataset pEDataset = pWs.get_Datasets(esriDatasetType.esriDTAny);
                    IDataset pDataset = pEDataset.Next();

                    while (pDataset != null)
                    {
                        content = content + "  " + pDataset.Name;
                        if (pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            IEnumDataset pESubDataset = pDataset.Subsets;
                            IDataset pSubDataset = pESubDataset.Next();
                            while (pSubDataset != null)
                            {
                                //content = content + pSubDataset.Name;

                                pSubDataset = pESubDataset.Next();
                            }

                        }
                        pDataset = pEDataset.Next();
                    }
                    if (content != null)
                    {
                        MessageBox.Show("所有的数据集为：" + content);
                    }
                    else
                    {
                        MessageBox.Show("这里面没有数据集");
                    }
                }
                catch (Exception ee)
                {

                }

            }
        }

        public string WsPath()
        {
            string wsFileName = "";
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "个人数据库（MDB）|*.mdb";
            DialogResult dialogR = openFile.ShowDialog();
            if (dialogR == DialogResult.Cancel)
            {

            }
            else
            {
                wsFileName = openFile.FileName;
            }
            return wsFileName;
        }


        public void DrawCircle_Graphics(IPoint pPoint, double radius, IRgbColor pColor)
        {
            #region 定义填充颜色与类型
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();//产生一个线符号对象
            pLineSymbol.Width = 2;
            pLineSymbol.Color = pColor;
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();//设置填充符号的属性
            pColor.Transparency = 0;
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pLineSymbol;
            #endregion

            IConstructCircularArc pConstructCircularArc = new CircularArcClass();
            pConstructCircularArc.ConstructCircle(pPoint, radius, false);
            ICircularArc pArc = pConstructCircularArc as ICircularArc;
            ISegment pSegment1 = pArc as ISegment; //通过ISegmentCollection构建Ring对象
            ISegmentCollection pSegCollection = new RingClass();
            object o = Type.Missing; //添加Segement对象即圆
            pSegCollection.AddSegment(pSegment1, ref o, ref o); //QI到IRing接口封闭Ring对象，使其有效
            IRing pRing = pSegCollection as IRing;
            pRing.Close(); //通过Ring对象使用IGeometryCollection构建Polygon对象

            IGeometryCollection pGeometryColl = new PolygonClass();
            pGeometryColl.AddGeometry(pRing, ref o, ref o); //构建一个CircleElement对象
            IElement pElement = new CircleElementClass();
            pElement.Geometry = pGeometryColl as IGeometry;
            //填充圆的颜色
            IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;
            pFillShapeElement.Symbol = pFillSymbol;

            //IGraphicsLayer pLayer = (GISMapApplication.Instance.Scene as IBasicMap).BasicGraphicsLayer;
            // IGraphicsContainer3D pGC = pLayer as IGraphicsContainer3D;
            //  pGC.AddElement(pElement);
            pGraphicsContainer.AddElement((IElement)pFillShapeElement, 0);
            axMapControl1.ActiveView.Refresh();

        }

        private void 空间查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pMouseOperator = "spaticalQuery";
        }

        private void 邻接多边形查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NearPolygon nearPolygon = new NearPolygon(axMapControl1.Map, player as ILayer, axMapControl1);
            nearPolygon.Show();
        }

        private void 属性查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SlectByProperty selectByProperty = new SlectByProperty(axMapControl1, axMapControl1.Map);
            selectByProperty.Show();
        }

        private void 打开个人地理数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "个人数据库（*.mdb）|*.mdb";
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = "";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                string folder = System.IO.Path.GetDirectoryName(path);
                string fileName = System.IO.Path.GetFileName(path);

                IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactoryClass();

                IWorkspace workSpace = workspaceFactory.OpenFromFile(path, 0);
                if (workSpace != null)
                {
                    MessageBox.Show("个人地理数据库已经打开");
                    AddDataToMap(workSpace);
                }
                else
                {
                    MessageBox.Show("数据库打不开");

                }


            }
            else
            {

                return;
            }
        }


        private void AddDataToMap(IWorkspace pWorkspace)
        {
            //获取工作空间内数据集
            IEnumDataset pEnumDataset;
            pEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureClass);
            IDataset pDataset;
            pEnumDataset.Reset();
            pDataset = pEnumDataset.Next();
            //创建图层
            IFeatureClass pFeatureClass = pDataset as IFeatureClass;

            while (pDataset != null)
            {
                IFeatureLayer pLayer = new FeatureLayerClass();
                MessageBox.Show("添加要素类" + pDataset.Name + "!");
                pFeatureClass = pDataset as IFeatureClass;
                pLayer.FeatureClass = pFeatureClass;
                //传递图层名称
                pLayer.Name = pDataset.Name;

                axMapControl1.AddLayer(pLayer);
                pDataset = pEnumDataset.Next();
            }

        }

        private void 获取要素类ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string wsName = WsPath();
            if (wsName != null)
            {
                try
                {
                    string content = null;
                    string featureClassNames = null;
                    IWorkspaceFactory pWsFt = new AccessWorkspaceFactoryClass();
                    IWorkspace pWs = pWsFt.OpenFromFile(wsName, 0);
                    IEnumDataset pEDataset = pWs.get_Datasets(esriDatasetType.esriDTAny);
                    IDataset pDataset = pEDataset.Next();

                    while (pDataset != null)
                    {
                        content = content + "  " + pDataset.Name;
                        //要素类
                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass)
                        {
                            featureClassNames += pDataset.Name;
                        }
                        //要素数据集
                        if (pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            IEnumDataset pESubDataset = pDataset.Subsets;
                            IDataset pSubDataset = pESubDataset.Next();
                            while (pSubDataset != null)
                            {
                                content += pSubDataset.Name;

                                pSubDataset = pESubDataset.Next();
                            }

                        }
                        pDataset = pEDataset.Next();
                    }
                    if (content != null)
                    {
                        MessageBox.Show("所有的数据集为：" + content + ";要素类为：" + featureClassNames);
                    }
                    else
                    {
                        MessageBox.Show("这里面没有数据集");
                    }
                }
                catch (Exception ee)
                {



                }

            }
        }

        private void 创建要素类ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFeatureClass newFeatureClass = new NewFeatureClass();
            newFeatureClass.Show();
            //定义空间参考系
            /*Type factoryType = Type.GetTypeFromProgID("esriGeometry.SpatialReferenceEnvironment");
            System.Object obj = Activator.CreateInstance(factoryType);
            ISpatialReferenceFactory3 spatialReferenceFactory = obj as ISpatialReferenceFactory3;
            ISpatialReference pSpatialReference = spatialReferenceFactory.CreateSpatialReference((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
            
            IGeometryDefEdit pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = pGeoDef as IGeometryDefEdit;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
             pGeoDefEdit.SpatialReference_2 = pSpatialReference;
            //定义一个字段几何集合
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            //定义单个字段
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "SHAPE";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            pFieldsEdit.AddField(pField);
            //定义单个字段，并添加到字段集合中
            pFieldEdit.GeometryDef_2 = pGeoDef;
            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "地名";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldsEdit.AddField(pField);
            MessageBox.Show("哈哈哈，但愿能成功");*/


        }


        private void 删除要素类ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string wsName = WsPath();
            if (wsName != "")
            {
                IWorkspaceFactory pWsFt = new AccessWorkspaceFactoryClass();
                IWorkspace pWs = pWsFt.OpenFromFile(wsName, 0);
                IFeatureWorkspace pFWs = pWs as IFeatureWorkspace;

                IFeatureClass pFeatureClass = pFWs.OpenFeatureClass("point1");

                IDataset pDataset = pFeatureClass as IDataset;
                pDataset.Delete();
                MessageBox.Show("要素类删除成功");
            }

        }

        private void 点选ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 相交运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
      
        private void 包含统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             containQuery = new ContainsQuery(axMapControl1.Map,player,axMapControl1);
             containQuery.Show();
        }

        private void 缓冲分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BufferSettings bufferSettings = new BufferSettings(axMapControl1.Map, player, axMapControl1);
            bufferSettings.Show();
        }

        private void 邻近矩阵ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            IFeatureClass pPolygonClass = GetFeatureClass(@"C:\Users\佳佳\Desktop\ArcMap\Data", "四川县界");
           // ITable pTable=
        }

        private IFeatureClass GetFeatureClass(string FilePath, string LayerName)
        {
            IWorkspaceFactory pWs = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pFw = pWs.OpenFromFile(FilePath, 0) as IFeatureWorkspace;
            IFeatureClass pRtClass = pFw.OpenFeatureClass(LayerName);
            return pRtClass;
        }

        private void 求交ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cmd="intersection";
            Intersection intersection = new Intersection(axMapControl1,cmd);
            intersection.Show();
        }

        private void 求和ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cmd = "union";
            Intersection intersection = new Intersection(axMapControl1, cmd);
            intersection.Show();
        }

        private void 裁剪ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cmd = "clip";
            Intersection intersection = new Intersection(axMapControl1, cmd);
            intersection.Show();
        }

        private void 异或ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cmd = "xor";
            Intersection intersection = new Intersection(axMapControl1, cmd);
            intersection.Show();
        }

        private void 符号化ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           Symbolization symbolization = new Symbolization(axMapControl1);
           symbolization.Show();
           
        }

        #region 符号化（没用的代码）
        private void 点密度符号化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Symbolization symbolization = new Symbolization(axMapControl1);
            symbolization.Show();
            //IMap pMap = axMapControl1.Map;
            ////这里以面状图层为例
            //IGeoFeatureLayer pGeoFeatureLayer = pMap.get_Layer(6) as IGeoFeatureLayer;
            ////新建一个填充符号
            //IFillSymbol pSimpleFills;
            //pSimpleFills = new SimpleFillSymbolClass();
            //IRgbColor color = new RgbColorClass();
            //color.Red = 120;
            //color.Green = 110;
            //color.Blue = 0;
            //pSimpleFills.Color = color;
            ////新建线符号
            //ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
            //color.Red = 255;
            //color.Green = 0;
            //color.Blue = 0;
            //pLineSymbol.Color = color;
            //pLineSymbol.Width = 3;
            ////线符号作为该填充符号的外边缘
            //pSimpleFills.Outline = pLineSymbol;
            //ISimpleRenderer pSimpleRenderer;
            //pSimpleRenderer = new SimpleRendererClass();
            //pSimpleRenderer.Symbol = (ISymbol)pSimpleFills;


            ////设置字段作为要素透明设置的属性
            //ITransparencyRenderer pTransRenderer;
            //pTransRenderer = pSimpleRenderer as ITransparencyRenderer;
            //pTransRenderer.TransparencyField = "OBJECTID";
            //pGeoFeatureLayer.Renderer = pTransRenderer as IFeatureRenderer;

            ////刷新显示
            //axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

       
        private void 分级着色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer pGeoFeatureLayer;
            ITable pTable;
            IClassifyGEN pClassify;
            ITableHistogram pTableHistogram;
            IBasicHistogram pHistogram;
            object dataFrequency;

            object dataValues;
            double[] Classes;
            int ClassesCount;
            IClassBreaksRenderer pClassBreakRenderer;
            IHsvColor pFromColor;
            IHsvColor pToColor;

            IAlgorithmicColorRamp pAIgorithmicCR;

            IEnumColors pEnumColors;

            bool ok;

            IColor pColor;
            ISimpleFillSymbol pSimpleFillS;
            int IbreakIndex;
            string strPopFiled = "OBJECTID";
            int numDesiredClasses = 5;

            IMap pMap = axMapControl1.Map;
            pMap.ReferenceScale = 0;
            pGeoFeatureLayer = (IGeoFeatureLayer)pMap.get_Layer(6);
            //从pTable的id字段中得到信息给dataValues和dataFrequency两个数组
            pTable = (ITable)pGeoFeatureLayer.FeatureClass;
            pTableHistogram = new BasicTableHistogramClass();
            pHistogram = (IBasicHistogram)pTableHistogram;
            pTableHistogram.Field = strPopFiled;
            pTableHistogram.Table = pTable;
            pHistogram.GetHistogram(out dataValues, out dataFrequency);
            //下面是分级方法，用于根据获取的值计算出符号要求的数据
            pClassify = new EqualIntervalClass();
            try
            {
                pClassify.Classify(dataValues, dataFrequency, ref numDesiredClasses);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //返回一个数组
            Classes = (double[])pClassify.ClassBreaks;
            ClassesCount = Classes.GetUpperBound(0);
            pClassBreakRenderer = new ClassBreaksRendererClass();
            pClassBreakRenderer.Field = strPopFiled;
            //设置着色对象的分级数目
            pClassBreakRenderer.BreakCount = ClassesCount;
            pClassBreakRenderer.SortClassesAscending = true;
            //产生分级着色需要的颜色带对象的起止颜色对象
            pFromColor = new HsvColorClass();
            pFromColor.Hue = 60;
            pFromColor.Saturation = 100;
            pFromColor.Value = 96;
            pToColor = new HsvColorClass();
            pToColor.Hue = 0;
            pToColor.Saturation = 100;
            pToColor.Value = 96;
            //产生颜色带对象
            pAIgorithmicCR = new AlgorithmicColorRampClass();
            pAIgorithmicCR.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
            pAIgorithmicCR.FromColor = pFromColor;
            pAIgorithmicCR.ToColor = pToColor;
            pAIgorithmicCR.Size = ClassesCount;
            pAIgorithmicCR.CreateRamp(out ok);
            //获得颜色
            pEnumColors = pAIgorithmicCR.Colors;
            //分类着色对象中的symbol和break的下标是从0开始
            for (IbreakIndex = 0; IbreakIndex <= ClassesCount - 1; IbreakIndex++)
            {
                pColor = pEnumColors.Next();
                pSimpleFillS = new SimpleFillSymbolClass();
                pSimpleFillS.Color = pColor;
                pSimpleFillS.Style = esriSimpleFillStyle.esriSFSSolid;
                //这里是构造函数不同颜色着色的方法
                pClassBreakRenderer.set_Symbol(IbreakIndex, (ISymbol)pSimpleFillS);
                //着色对象的断点
                pClassBreakRenderer.set_Break(IbreakIndex, Classes[IbreakIndex + 1]);

            }
            pGeoFeatureLayer.Renderer = (IFeatureRenderer)pClassBreakRenderer;
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void 唯一值着色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer m_pGeoFeatureL;
            IUniqueValueRenderer pUniqueValueR;
            IFillSymbol pFillSymbol;
            IColor pNextUniqueColor;
            IEnumColors pEnumRamp;
            ITable pTable;
            int iFieldNumber;
            IRow pNextRow;
            IRowBuffer pNextRowBuffer;
            ICursor pCursor;
            IQueryFilter pQueryFilter;
            string codeValue;
            IRandomColorRamp pColorRamp;


            string strNameField = "OBJECTID";

            IMap pMap = axMapControl1.Map;
            pMap.ReferenceScale = 0;
            m_pGeoFeatureL = (IGeoFeatureLayer)pMap.get_Layer(6);
            pUniqueValueR = new UniqueValueRendererClass();
            pTable = (ITable)m_pGeoFeatureL;

            iFieldNumber = pTable.FindField(strNameField);
            if (iFieldNumber == -1)
            {
                MessageBox.Show("未能找到字段" + strNameField);
                return;
            }
            //只用一个字段进行单值着色
            pUniqueValueR.FieldCount = 1;
            //用于区分着色的字段
            pUniqueValueR.set_Field(0, strNameField);
            //产生一个随机的颜色条，用的是HSV颜色模式
            pColorRamp = new RandomColorRampClass();
            pColorRamp.StartHue = 0;
            pColorRamp.MinValue = 99;
            pColorRamp.MinSaturation = 15;
            pColorRamp.EndHue = 360;
            pColorRamp.MaxValue = 100;
            pColorRamp.MaxSaturation = 30;
            //任意产生100歌颜色，如果知道要素的数据可以产生精确的颜色数目
            pColorRamp.Size = 100;
            bool ok = true;
            pColorRamp.CreateRamp(out ok);
            pEnumRamp = pColorRamp.Colors;
            pNextUniqueColor = null;
            //产生查询过滤器的对象
            pQueryFilter = new QueryFilterClass();
            pQueryFilter.AddField(strNameField);
            //根据某个字段在表中找出指向所有行的游标对象
            pCursor = pTable.Search(pQueryFilter, true);
            pNextRow = pCursor.NextRow();
            //遍历所有的要素
            while (pNextRow != null)
            {
                pNextRowBuffer = pNextRow;
                //找出row为某个定值的值
                codeValue = pNextRowBuffer.get_Value(iFieldNumber).ToString();
                //获取随机颜色带中的任意一个颜色
                pNextUniqueColor = pEnumRamp.Next();
                if (pNextUniqueColor == null)
                {
                    pEnumRamp.Reset();
                    pNextUniqueColor = pEnumRamp.Next();

                }
                pFillSymbol = new SimpleFillSymbolClass();
                pFillSymbol.Color = pNextUniqueColor;
                //将每次得到的要素字段值和修饰它的符号值放入着色对象中
                pUniqueValueR.AddValue(codeValue, strNameField, (ISymbol)pFillSymbol);
                pNextRow = pCursor.NextRow();
            }
            m_pGeoFeatureL.Renderer = (IFeatureRenderer)pUniqueValueR;
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void 质量图着色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer pGeoFeatureL;
            IFeatureLayer pFeatureLayer;
            ITable pTable;
            ICursor pCursor;
            IQueryFilter pQueryFilter;
            IRowBuffer pRowBuffer;
            int numFields = 2;
            int[] fieldIndecies = new int[numFields];
            int iFieldIndex;
            double dMaxValue;
            bool isFirstValue;
            double dFieldValue;
            IChartRenderer pChartRenderer;
            IRendererFields pRenderFields;
            IFillSymbol pFillSymbol;
            IMarkerSymbol pMarkerSymbol;
            ISymbolArray pSymbolArray;
            IChartSymbol pCharSymbol;
            //设置需要的字段信息
            string strPopField1 = "OBJECTID";
            string strPopField2 = "FID";

            IMap pMap = axMapControl1.Map;
            pMap.ReferenceScale = pMap.MapScale;
            pFeatureLayer = (IGeoFeatureLayer)pMap.get_Layer(6);
            pGeoFeatureL = (IGeoFeatureLayer)pFeatureLayer;
            pTable = (ITable)pGeoFeatureL;

            pGeoFeatureL.ScaleSymbols = true;
            pChartRenderer = new ChartRendererClass();
            //设置柱状图中所需要绘制的属性
            pRenderFields = (IRendererFields)pChartRenderer;
            pRenderFields.AddField(strPopField1, strPopField1);
            pRenderFields.AddField(strPopField2, strPopField2);
            pQueryFilter = new QueryFilterClass();
            pQueryFilter.AddField(strPopField1);
            pQueryFilter.AddField(strPopField2);
            pCursor = pTable.Search(pQueryFilter, true);
            fieldIndecies[0] = pTable.FindField(strPopField1);
            fieldIndecies[1] = pTable.FindField(strPopField2);
            isFirstValue = true;
            dMaxValue = 0;
            //迭代坊问每一个要素
            pRowBuffer = pCursor.NextRow();
            while (pRowBuffer != null)
            {
                for (iFieldIndex = 0; iFieldIndex <= numFields - 1; iFieldIndex++)
                {

                    //MessageBox.Show("哈哈："+pRowBuffer.get_Value(fieldIndecies[iFieldIndex]).ToString());
                    //迭代访问要素的字段值并对最大值进行更新和标记
                    dFieldValue = double.Parse(pRowBuffer.get_Value(fieldIndecies[iFieldIndex]).ToString());

                    if (isFirstValue)
                    {
                        //将最大值dmax初始化为第一个值
                        dMaxValue = dFieldValue;
                        isFirstValue = false;
                    }
                    else
                    {
                        if (dFieldValue > dMaxValue)
                        {
                            //获取最大值时候进行更新
                            dMaxValue = dFieldValue;
                        }
                    }
                }
                pRowBuffer = pCursor.NextRow();
            }

            if (dMaxValue <= 0)
            {
                MessageBox.Show("获取要素失败");
                return;
            }
            //设置需要渲染的柱状图符号
            IBarChartSymbol pBarChartSymbol;
            pBarChartSymbol = new BarChartSymbolClass();
            pCharSymbol = (IChartSymbol)pBarChartSymbol;
            pBarChartSymbol.Width = 12;
            pMarkerSymbol = (IMarkerSymbol)pBarChartSymbol;
            //设置柱状图的最大值
            pCharSymbol.MaxValue = dMaxValue;
            //设置柱状图的最大渲染高度
            pMarkerSymbol.Size = 80;
            //为每个柱状图设置符号
            pSymbolArray = (ISymbolArray)pBarChartSymbol;
            //为每个柱状图添加颜色
            pFillSymbol = new SimpleFillSymbolClass();
            IRgbColor color = new RgbColorClass();
            color.Red = 213;
            color.Green = 212;
            color.Blue = 252;
            pFillSymbol.Color = color;
            pSymbolArray.AddSymbol((ISymbol)pFillSymbol);
            pFillSymbol = new SimpleFillSymbolClass();
            color.Red = 193;
            color.Green = 252;
            color.Blue = 179;
            pFillSymbol.Color = color;
            pSymbolArray.AddSymbol((ISymbol)pFillSymbol);
            //设置渲染符号为柱状图
            pChartRenderer.ChartSymbol = (IChartSymbol)pBarChartSymbol;

            pChartRenderer.Label = "OBJECTID";
            //设置柱状图的背景颜色
            pFillSymbol = new SimpleFillSymbolClass();
            color.Red = 239;
            color.Green = 228;
            color.Blue = 190;
            pFillSymbol.Color = color;
            pChartRenderer.BaseSymbol = (ISymbol)pFillSymbol;
            //设置overpoaster属性为false，使柱状图显示在polygon多边形要素的中间
            pChartRenderer.UseOverposter = false;
            pChartRenderer.CreateLegend();
            //更新柱状图和刷新显示
            pGeoFeatureL.Renderer = (IFeatureRenderer)pChartRenderer;
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void 依比例符号着色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer pGeoFeatureLayer;
            IFeatureLayer pFeatureLayer;
            IProportionalSymbolRenderer pProportionalSymbolR;
            ITable pTable;
            IQueryFilter pQueryFilter;
            ICursor pCursor;
            IFillSymbol pFillSymbol;
            ICharacterMarkerSymbol pCharaterMarkerS;
            IDataStatistics pDataStatistics;
            IStatisticsResults pStatisticsResult;
            stdole.StdFont pFontDisp;
            IRotationRenderer pRotationRenderer;
            IMap pMap = axMapControl1.Map;
            pMap.ReferenceScale = 0;
            pFeatureLayer = (IGeoFeatureLayer)pMap.get_Layer(6);
            pGeoFeatureLayer = (IGeoFeatureLayer)pFeatureLayer;
            pTable = (ITable)pGeoFeatureLayer;
            pQueryFilter = new QueryFilterClass();
            pQueryFilter.AddField("");
            pCursor = pTable.Search(pQueryFilter, true);
            //使用statistics对象来计算最大值最小值
            pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Cursor = pCursor;
            //设置要统计的字段名称
            pDataStatistics.Field = "OBJECTID";
            //获取统计结果
            pStatisticsResult = pDataStatistics.Statistics;
            if (pStatisticsResult == null)
            {
                MessageBox.Show("获取对象失败");
                return;
            }
            //设置符号的北京填充色
            pFillSymbol = new SimpleFillSymbolClass();
            IRgbColor backColor = new RgbColorClass();
            backColor.Red = 239;
            backColor.Green = 228;
            backColor.Blue = 190;
            pFillSymbol.Color = backColor;
            //设置依比例符号的符号类型
            pCharaterMarkerS = new CharacterMarkerSymbolClass();
            pFontDisp = new stdole.StdFontClass();
            pFontDisp.Name = "ESRI.Bussiness";
            pFontDisp.Size = 20;
            pCharaterMarkerS.Font = (IFontDisp)pFontDisp;
            pCharaterMarkerS.CharacterIndex = 90;
            IRgbColor color = new RgbColorClass();
            color.Red = 0;
            color.Green = 0;
            color.Blue = 0;
            pCharaterMarkerS.Color = color;
            //创建一个新的依比例符变化的符号来对字段进行渲染符号化
            pCharaterMarkerS.Size = 8;
            pProportionalSymbolR = new ProportionalSymbolRendererClass();
            pProportionalSymbolR.ValueUnit = esriUnits.esriUnknownUnits;
            pProportionalSymbolR.Field = "OBJECTID";
            pProportionalSymbolR.FlanneryCompensation = false;

            //pProportionalSymbolR.MinDataValue = pStatisticsResult.Minimum;
            pProportionalSymbolR.MinDataValue = 10;
            pProportionalSymbolR.MaxDataValue = pStatisticsResult.Maximum;
            pProportionalSymbolR.BackgroundSymbol = pFillSymbol;

            pProportionalSymbolR.MinSymbol = (ISymbol)pCharaterMarkerS;
            pProportionalSymbolR.LegendSymbolCount = 5;
            pProportionalSymbolR.CreateLegendSymbols();
            //根据字段值来设置符号的旋转角度
            pRotationRenderer = (IRotationRenderer)pProportionalSymbolR;
            pRotationRenderer.RotationField = "OBJECTID";
            pRotationRenderer.RotationType = esriSymbolRotationType.esriRotateSymbolGeographic;
            //设置states图层为依比例符号变化符号渲染的图层并刷新显示
            pGeoFeatureLayer.Renderer = (IFeatureRenderer)pProportionalSymbolR;
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
#endregion

      

       /* private ITable CreatrWeightTable(string filePath, string tableName, IFeatureClass pFeatureClass, string fieldName)
        {

            IWorkspaceFactory pWs = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pFw = pWs.OpenFromFile(filePath, 0) as IFeatureWorkspace;
            //用户添加表中的必要字段
            IObjectClassDescription objectClassDescription = new ObjectClassDescriptionClass();
            IFields pTableFieldFields = objectClassDescription.RequiredFields;
            IFieldsEdit pTableFieldsEdit = pTableFieldFields as IFieldsEdit;
            int index = pFeatureClass.FindField(fieldName);

           //IFeatureClass pRtClass = pFw.OpenFeatureClass(LayerName);
           // return pRtClass;
        }*/


    }


}