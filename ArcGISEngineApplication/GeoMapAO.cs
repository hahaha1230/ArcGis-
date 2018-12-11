using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.SystemUI;


namespace ArcGISEngineApplication
{

    //定义设置控件的接口
    interface IComControl
    {
        //主视图控件
        AxMapControl AxMapControl1 { get; set; }
        //鹰眼视图控件
        AxMapControl AxMapControl2 { get; set; }
        //版面视图控件
        AxPageLayoutControl AxPageLayoutControl1 { get; set; }
        //定义设置颜色的方法
        IRgbColor GetRGB(int r, int g, int b);
    }

    interface IEagOpt : IComControl
    {
        //定义属性字段信息
        IMapControlEvents2_OnMouseMoveEvent Em { get; set; }
        IMapControlEvents2_OnMouseDownEvent Ed { get; set; }
        IMapControlEvents2_OnExtentUpdatedEvent Eu { get; set; }
        //使主视图与鹰眼视图同步的方法

        void NewGeomap();
        //移动鹰眼视图中红色矩形框
        void MoveEagl();

        //处理鹰眼视图中鼠标的移动
        void MouseMov();
        //绘制红色矩形框的过程
        void DrawrRec();
    }

    interface IAddGeoData : IComControl
    {
        //存放用户选择的地理数据文件
        string[] StrFileName { get; set; }
        //加载地理数据的方法
        void AddGeoMap();
    }

    interface IGeoDataOper : IComControl
    {
        //地图操作的类型
        string StrOperType { get; set; }
        //鼠标按下事件的参数
        IMapControlEvents2_OnMouseDownEvent E { get; set; }
        //实现地图交互操作的方法
        void OperMap();

    }

    interface ILaySequAttr : IComControl
    {
        ITOCControlEvents_OnMouseDownEvent Ted { get; set; }
    }

    class GeoMapAO : IAddGeoData, IGeoDataOper, IEagOpt
    {
        //实现地图控件的接口
        AxMapControl axMapControl1;
        public AxMapControl AxMapControl1
        {
            get { return axMapControl1; }
            set { axMapControl1 = value; }
        }

        AxMapControl axMapControl2;
        public AxMapControl AxMapControl2
        {
            get { return axMapControl2; }
            set { axMapControl2 = value; }
        }

        AxPageLayoutControl axPageLayoutControl1;
        public AxPageLayoutControl AxPageLayoutControl1
        {
            get { return axPageLayoutControl1; }
            set { axPageLayoutControl1 = value; }
        }

        //定义实现获取RGB颜色的颜色
        public IRgbColor GetRGB(int r, int g, int b)
        {
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = r;
            pColor.Green = g;
            pColor.Blue = b;
            return pColor;
        }


        //实现地图加载的接口
        string[] strFileName;//保存用户选择的地理数据文件名 
        public string[] StrFileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }
        //定义加载地图的方法
        public void AddGeoMap()
        {
            for (int i = 0; i < strFileName.Length; i++)
            {
                string strExt = System.IO.Path.GetExtension(strFileName[i]);
                switch (strExt)//判断文件类型，然后采取不同的方法加载文件
                {

                    case ".shp"://用户选择了矢量文件
                        {


                            string strPath = System.IO.Path.GetDirectoryName(strFileName[i]);
                            string strFile = System.IO.Path.GetFileNameWithoutExtension(strFileName[i]);
                            //向控件加载地图
                            axMapControl1.AddShapeFile(strPath, strFile);
                            axMapControl2.AddShapeFile(strPath, strFile);
                            axMapControl2.Extent = axMapControl2.FullExtent;
                            break;

                        }
                    default:
                        break;
                }
            }
        }

        //实现鼠标与地图交互的接口
        string strOperType;//标识地图操作类型
        public string StrOperType
        {
            get { return strOperType; }
            set { strOperType = value; }
        }
        //定义鼠标按下事件的变量
        IMapControlEvents2_OnMouseDownEvent e;
        public IMapControlEvents2_OnMouseDownEvent E
        {
            get { return e; }
            set { e = value; }
        }

        //定义鼠标与地图交互的方法
        public void OperMap()
        {
            switch (strOperType)
            {
                case "LKZoomIn":
                    {
                        axMapControl1.Extent = axMapControl1.TrackRectangle();
                        axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }
                    break;
                default:
                    break;
            }
        }


        IMapControlEvents2_OnMouseMoveEvent em;

        public IMapControlEvents2_OnMouseMoveEvent Em
        {
            get { return em; }
            set { em = value; }
        }
        IMapControlEvents2_OnMouseDownEvent ed;
        public IMapControlEvents2_OnMouseDownEvent Ed
        {
            get { return ed; }
            set { ed = value; }
        }
        IMapControlEvents2_OnExtentUpdatedEvent eu;
        public IMapControlEvents2_OnExtentUpdatedEvent Eu
        {
            get { return eu; }
            set { eu = value; }
        }


        //主视图与鹰眼视图同步
        public void NewGeomap()
        {
            IMap pMap = axMapControl1.Map;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                axMapControl2.Map.AddLayer(pMap.get_Layer(i));
            }
            //使鹰眼视图中显示加载地图的全图
            axMapControl2.Extent = axMapControl2.FullExtent;//使鹰眼视图中显示加载的地图全图
        }


        public void MoveEagl()
        {
            if (ed.button == 1)//探测鼠标左键
            {

                IPoint pPt = new ESRI.ArcGIS.Geometry.Point();
                //IPoint pPt = new PointClass();
                pPt.X = ed.mapX;
                pPt.Y = ed.mapY;
                IEnvelope pEnvelope = axMapControl1.Extent as IEnvelope;
                pEnvelope.CenterAt(pPt);
                axMapControl1.Extent = pEnvelope;
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
            else if (ed.button==2)//探测鼠标右键
            {
                IEnvelope pEnvelope = axMapControl2.TrackRectangle();
                axMapControl1.Extent = pEnvelope;
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);


            }
        }

        public void MouseMov()
        {
            if (em.button != 1)
            {
                return;
            }
            IPoint pPt = new ESRI.ArcGIS.Geometry.Point();
            pPt.X = em.mapX;
            pPt.Y = em.mapY;
            axMapControl1.CenterAt(pPt);
            axMapControl2.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }


        public void DrawrRec()
        {
            //绘制鹰眼图中红色矩形的代码
            IGraphicsContainer pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;//以mapcontrol2为图形容器
            IActiveView pAv = pGraphicsContainer as IActiveView;
            //在绘制前。清楚mapcontrol2中的任何图形元素
            pGraphicsContainer.DeleteAllElements();
        
            IRectangleElement pRecElement = new RectangleElementClass();
            IElement pEle = pRecElement as IElement;
            IEnvelope pEnv = eu.newEnvelope as IEnvelope;
            pEle.Geometry = pEnv;
            //设置颜色
            IRgbColor pColor = new RgbColorClass();
            pColor = GetRGB(200, 0, 0);
            pColor.Transparency = 255;
            //产生一个线符号对象
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
            pLineSymbol.Width = 2;
            pLineSymbol.Color = pColor;
           // 设置填充符号属性
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            //设置透明色
            pColor.Transparency = 0;
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pLineSymbol;
            IFillShapeElement pFillShapeElement = pRecElement as IFillShapeElement;
            pFillShapeElement.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement(pEle, 0);
            axMapControl2.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
;

        }


    }
}
