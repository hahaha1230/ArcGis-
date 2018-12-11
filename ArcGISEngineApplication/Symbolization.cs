using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ArcGISEngineApplication
{
    public partial class Symbolization : Form
    {
        private AxMapControl axMapControl1;
        private int CURRENT_LAYER_POINT = 1;               //记录当前选择的是点、线还是面图层
        private int CURRENT_LAYER_LINE = 2;
        private int CURRENT_LAYER_POLYGON = 3;
        private IMap pMap;
        private int type;
        public Symbolization()
        {
            InitializeComponent();
        }

        public Symbolization(AxMapControl axMapControl)
        {
            InitializeComponent();
            axMapControl1 = axMapControl;
            pMap = axMapControl1.Map;
        }
 
        private void Symbolization_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                comboBox2.Items.Add(pMap.get_Layer(i).Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == null||comboBox1.Text==null)
            {
                MessageBox.Show("需要先选择图层和方法哦！");
                return;
            }
            ILayer pLayer = getLayersByName(comboBox2.Text);
            switch (comboBox1.Text.ToString())
            {  
                case "简单着色":
                   
                    if (type == CURRENT_LAYER_POLYGON)
                    { 
                        simpleDye(pLayer);
                      
                    }
                    else if (type == CURRENT_LAYER_LINE)
                    { 
                        lineSimpleDye(pLayer);
                    }
                    break;
                case "分级着色":
                    if (type == CURRENT_LAYER_POLYGON)
                    {
                        polygonClassificationDye(pLayer);
                    }
                    else if (type == CURRENT_LAYER_LINE)
                    { 
                        
                    }
                    break;
                case "唯一值着色":
                    break;
                case "质量图着色":
                    break;
                case "依比例符号着色":
                    break;
                default:
                    break;
                   
            }
            
        }
      
       
        private void lineSimpleDye(ILayer pLayer) { 
             try
            {
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置线符号
                ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
                simpleLineSymbol.Width = 0;//定义线的宽度 
                simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame; //定义线的样式                               
                simpleLineSymbol.Color = GetRgbColor(255, 100, 0);//定义线的颜色
                ISymbol symbol = simpleLineSymbol as ISymbol;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = symbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axMapControl1.Update();
            }
            catch (Exception ex)
            {


            }

        }

        private IRgbColor GetRgbColor(int r, int g, int b)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = r;
            rgbColor.Green = g;
            rgbColor.Blue = b;
            return rgbColor;
        }

        private void simpleDye(ILayer pLayer)
        {
            //这里以面状图层为例
            IGeoFeatureLayer pGeoFeatureLayer =pLayer as IGeoFeatureLayer;
            //新建一个填充符号
            IFillSymbol pSimpleFills;
            pSimpleFills = new SimpleFillSymbolClass();
            IRgbColor color = new RgbColorClass();
            color.Red = 120;
            color.Green = 110;
            color.Blue = 0;
            pSimpleFills.Color = color;
            //新建线符号
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
            color.Red = 255;
            color.Green = 0;
            color.Blue = 0;
            pLineSymbol.Color = color;
            pLineSymbol.Width = 3;
            //线符号作为该填充符号的外边缘
            pSimpleFills.Outline = pLineSymbol;
            ISimpleRenderer pSimpleRenderer;
            pSimpleRenderer = new SimpleRendererClass();
            pSimpleRenderer.Symbol = (ISymbol)pSimpleFills;


            //设置字段作为要素透明设置的属性
            ITransparencyRenderer pTransRenderer;
            pTransRenderer = pSimpleRenderer as ITransparencyRenderer;
            pTransRenderer.TransparencyField = comboBox3.Text;
            pGeoFeatureLayer.Renderer = pTransRenderer as IFeatureRenderer;

            //刷新显示
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void polygonClassificationDye(ILayer pLayer)
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
            string strPopFiled = comboBox3.Text;
            int numDesiredClasses = int.Parse(textBox1.Text.ToString());

            IMap pMap = axMapControl1.Map;
            pMap.ReferenceScale = 0;
            pGeoFeatureLayer = pLayer as IGeoFeatureLayer;
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

        private ILayer getLayersByName(string layerName)
        {
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (layerName.Equals(pMap.get_Layer(i).Name))
                {
                    return pMap.get_Layer(i);
                }
            }
            return null;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == null)
            {
                return;
            }
            comboBox3.Items.Clear();
            IFeatureLayer featureLayer = (IFeatureLayer)getLayersByName(comboBox2.Text);
            IFeatureClass pFeatureClass = featureLayer.FeatureClass;
            IFields pFields = pFeatureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                comboBox3.Items.Add(pFields.get_Field(i).Name);
            }
            getFeatureType(getLayersByName(comboBox2.Text));
           
        }
        #region 根据图层名获取图层类型（点、线、面）
        private int getFeatureType(ILayer pLayer)
        {

            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
            switch (pFeatureLayer.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    type = CURRENT_LAYER_POINT;
                    break;
                case esriGeometryType.esriGeometryLine:
                    type = CURRENT_LAYER_LINE;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    type = CURRENT_LAYER_POLYGON;
                    break;
                default:
                    type = 0;
                    break;
            }
            return type;
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        
    }
}
