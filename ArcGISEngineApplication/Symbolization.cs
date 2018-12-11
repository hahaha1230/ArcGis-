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
        private string symbolizationWay = null;
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
                //过滤掉点图层
                if (getFeatureType(getLayersByName(pMap.get_Layer(i).Name)) != 1)
                {  
                    cbLayer.Items.Add(pMap.get_Layer(i).Name);
                }
               
            }
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (cbLayer.Text == null||symbolizationWay==null)
            {
                MessageBox.Show("需要先选择图层和方法哦！");
                return;
            }
            ILayer pLayer = getLayersByName(cbLayer.Text);
            switch (symbolizationWay)
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
                        polygonClassificationDye(pLayer);
                    }
                    break;
                case "唯一值着色":
                    if (type == CURRENT_LAYER_POLYGON)
                    {
                        polygonUniqueDye(pLayer);
                    }
                    else if (type == CURRENT_LAYER_LINE)
                    {
                        polygonUniqueDye(pLayer);
                    }
                    break;
                case "质量图着色":
                      if (type == CURRENT_LAYER_POLYGON)
                    {
                        polygonQualityDye(pLayer);
                    }
                    else if (type == CURRENT_LAYER_LINE)
                    {
                        polygonQualityDye(pLayer);
                    }
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
            pTransRenderer.TransparencyField = cbField1.Text;
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
            string strPopFiled = cbField1.Text;
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

        private void polygonUniqueDye(ILayer pLayer) 
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


            string strNameField =cbField1.Text;

            IMap pMap = axMapControl1.Map;
            pMap.ReferenceScale = 0;
            m_pGeoFeatureL = pLayer as IGeoFeatureLayer;
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


        private void polygonQualityDye (ILayer pLayer)
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
            string strPopField1 = cbField1.Text;
            string strPopField2 = cbField2.Text;

            IMap pMap = axMapControl1.Map;
            pMap.ReferenceScale = pMap.MapScale;
            pFeatureLayer = (IGeoFeatureLayer)pLayer;
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
            try
            {
                while (pRowBuffer != null)
                {
                    for (iFieldIndex = 0; iFieldIndex <= numFields - 1; iFieldIndex++)
                    {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("该字段中有不合法的值，请重新选择字段");
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

            pChartRenderer.Label = cbField1.Text;
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
            if (cbLayer.Text == null)
            {
                return;
            }
            cbField1.Items.Clear();
            IFeatureLayer featureLayer = (IFeatureLayer)getLayersByName(cbLayer.Text);
            IFeatureClass pFeatureClass = featureLayer.FeatureClass;
            IFields pFields = pFeatureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
               // MessageBox.Show(pFields.get_Field(i).Name + ":" + pFields.get_Field(i).Type);
                if (pFields.get_Field(i).Type == esriFieldType.esriFieldTypeDouble)
                {  
                    cbField1.Items.Add(pFields.get_Field(i).Name);
                }
               
            }
            getFeatureType(getLayersByName(cbLayer.Text));
           
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

        private void 分级着色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            cbField2.Enabled = false;
            symbolizationWay = "分级着色";
        }

        private void 简单着色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbField2.Enabled = false;
            textBox1.Enabled = false;
            symbolizationWay = "简单着色";
        }

        private void 唯一值着色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbField2.Enabled = false;
            textBox1.Enabled = false;
            symbolizationWay = "唯一值着色";
        }

        private void 质量图着色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            cbField2.Enabled = true;
            symbolizationWay = "质量图着色";
        }

        private void cbField1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbField2.Enabled == true)
            {
                cbField2.Items.Clear();
                IFeatureLayer featureLayer = (IFeatureLayer)getLayersByName(cbLayer.Text);
                IFeatureClass pFeatureClass = featureLayer.FeatureClass;
                IFields pFields = pFeatureClass.Fields;
                for (int i = 0; i < pFields.FieldCount; i++)
                {
                   // MessageBox.Show(pFields.get_Field(i).Name + ":" + pFields.get_Field(i).Type);
                    //设置筛选条件为只能是int类型的字段而且去除掉cbfield1已经选过的字段
                    if ((pFields.get_Field(i).Type == esriFieldType.esriFieldTypeDouble) &&
                        (!pFields.get_Field(i).Name.Equals(cbField1.Text)))
                    {
                         cbField2.Items.Add(pFields.get_Field(i).Name);
                    }
                }
             
            }
        }
        
    }
}
