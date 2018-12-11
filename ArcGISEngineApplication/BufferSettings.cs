using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;

namespace ArcGISEngineApplication
{
    public partial class BufferSettings : Form
    {
        private string strOutputPath = System.IO.Path.GetTempPath();
        private string outputFeatureName;    //记录输出图层的位置，这个只是比strOutputPath多一个图层名
        private string strBufferLayer;//缓冲图层
        //private string bufferedFeatureClassName;  //缓冲区要素类名
        object bufferDistanceField;
      
        double bufferDistance = 10;               //缓冲距离
        string strBufferField;                //缓冲字段名
        private IMap pMap;
        private ILayer layers;
        private IFeatureLayer currentLayer;
        private int layerCount;
        private AxMapControl axMapControl1;
        private  string strSideType;   //线缓冲的方向
        private string strEndType;    //线末端缓冲区的封闭类型
        private string strDissolveType;  //缓冲区融合类型
        private   string strDissolveFields;//缓冲区融合字段，即根据融合字段的取值进行缓冲区合并
        

        public BufferSettings()
        {
            InitializeComponent();
        }

        public BufferSettings(IMap map, ILayer pLayer, AxMapControl axMapControl)
        {
            InitializeComponent();
            pMap = map;
            layers = pLayer;
            axMapControl1 = axMapControl;
            layerCount = pMap.LayerCount;
        }

        private void BufferSettings_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < layerCount; i++)
            {
                cboBufferLayer.Items.Add(pMap.get_Layer(i).Name);
            }

            //这些参数有些是只能用于线图层，故初始化都不可用
            cboBufferField.Enabled = false;
            txtBufferDistance.Enabled = true;
            cboSideType.Enabled = false;
            cboEndType.Enabled = false;
        }

      

       
        private void btnBufferAnalysis_Click(object sender, EventArgs e)
        {

            Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            gp.AddOutputsToMap = true;
            //DisplayEnvironmentPameters(gp);

            IGeoProcessorResult results = CreateBuffer(gp);
            if ((results != null) && (results.Status == esriJobStatus.esriJobSucceeded))
            {
                MessageBox.Show("缓冲成功");
            }
            gp = null;
        }



       

        private IGeoProcessorResult CreateBuffer(Geoprocessor gp)
        {

            strBufferLayer = cboBufferLayer.Text;
            //Buffer_analysis (in_features, out_feature_class, buffer_distance_or_field, line_side, line_end_type, dissolve_option, dissolve_field) 
            ESRI.ArcGIS.AnalysisTools.Buffer buffer = new ESRI.ArcGIS.AnalysisTools.Buffer();
            IFeatureLayer bufferLayer = GetFeatureLayer(strBufferLayer);
            buffer.in_features = bufferLayer;

            string outputFullPath = System.IO.Path.Combine(strOutputPath, textBox1.Text);
            buffer.out_feature_class = outputFullPath;
            buffer.buffer_distance_or_field = bufferDistanceField + " " + (string)comboBox1.SelectedItem;
            
            buffer.line_side = strSideType;
            buffer.line_end_type = strEndType;
            buffer.dissolve_option = strDissolveType;
            buffer.dissolve_field = strDissolveFields;
          
            IGeoProcessorResult results = (IGeoProcessorResult)gp.Execute(buffer, null);
            IFeatureLayer pOutputFeatLayer = new FeatureLayerClass();
            outputFeatureName = textBox1.Text;
          
           
            FileInfo info = new FileInfo(outputFeatureName);
            string path = outputFeatureName.Substring(0, outputFeatureName.Length - info.Name.Length);
            axMapControl1.AddShapeFile(path, info.Name);
            axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);

            return results;
        }

        #region "GetFeatureLayer"
        private IFeatureLayer GetFeatureLayer(string layerName)
        {
            if (GetLayers() == null) return null;
            IEnumLayer layers = GetLayers();
            layers.Reset();

            ILayer layer = null;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == layerName)
                    return layer as IFeatureLayer;
            }
            return null;
        }

        #endregion  
  

      


        #region "GetLayers"
        private IEnumLayer GetLayers()
        {
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
            if (layerCount != 0)
            {
                IEnumLayer layers =pMap.get_Layers(uid, true);
                return layers;
            }
            return null;
        }
        #endregion

        private void txtBufferDistance_Leave(object sender, EventArgs e)
        {
            if (rdoBufferDistance.Checked)
            {
                if (txtBufferDistance.Text!=null)
                {
                try
                {
                    bufferDistance = Convert.ToDouble(txtBufferDistance.Text);
                    bufferDistanceField = bufferDistance;
                }
                catch {
                    MessageBox.Show("请输入有效的数值");
                }
                }
            }
        }

        private void cboBufferLayer_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cboBufferLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboBufferField.Items.Clear();
            IFeatureLayer featureLayer = GetFeatureLayer(cboBufferLayer.Text);
            IFeatureClass pFeatureClass = featureLayer.FeatureClass;
            IFields pFields = pFeatureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                cboBufferField.Items.Add(pFields.get_Field(i).Name);
            }


            if (cboBufferLayer.SelectedItem != null)
            {
                strBufferLayer = cboBufferLayer.SelectedItem.ToString();
                if (GetFeatureLayer(strBufferLayer) == null) return;
                if (GetFeatureLayer(strBufferLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    cboSideType.Enabled = true;
                    cboEndType.Enabled = true;
                }
                else
                {
                    cboSideType.Enabled = false;
                    cboEndType.Enabled = false;
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboSideType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSideType;
            if (cboSideType.SelectedItem != null)
            {
                selectedSideType = cboSideType.SelectedItem.ToString();
                switch (selectedSideType)
                {
                    case "两边":
                        strSideType = "FULL";
                        break;
                    case "左边":
                        strSideType = "LEFT";
                        break;
                    case "右边":
                        strSideType = "RIGHT";
                        break;
                    default:
                        break;
                }
            }
        }

        private void cboEndType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEndType;
            if (cboEndType.SelectedItem != null)
            {
                selectedEndType = cboEndType.SelectedItem.ToString();
                switch (selectedEndType)
                {
                    case "圆弧型":
                        strEndType = "ROUND";
                        break;
                    case "平直型":
                        strEndType = "FLAT";
                        break;
                    default:
                        break;
                }
            }
        }

        private void cboDissolveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectDissolveTyppe;
            if (cboDissolveType.SelectedItem != null)
            {
                selectDissolveTyppe = cboDissolveType.SelectedItem.ToString();
                switch (selectDissolveTyppe)
                {
                    case "不融合":
                        strDissolveType = "NONE";
                        break;
                    case "融合所有缓冲区":
                        strDissolveType = "ALL";
                        break;
                    case "根据字段属性融合":
                        strDissolveType = "LIST";
                        break;
                    default:
                        break;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                strOutputPath = folderBrowserDialog1.SelectedPath;
                outputFeatureName = System.IO.Path.Combine(strOutputPath, ((string)cboBufferLayer.Text + "_buffer.shp"));
                textBox1.Text = outputFeatureName;
            }
        }

        private void cboBufferField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoBufferField.Checked)
            {
                if (cboBufferField.SelectedItem != null)
                {
                    strBufferField = cboBufferField.SelectedItem.ToString();
                    bufferDistanceField = strBufferField;
                }
            }
        }

        private void rdoBufferField_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBufferField.Checked)
            {
                cboBufferField.Enabled = true;
                if (strBufferLayer != "")
                {
                    if (GetFeatureLayer(strBufferLayer) == null)
                    { 
                        return;
                    }
                    IFields fields =   GetFeatureLayer(strBufferLayer).FeatureClass.Fields;
                    IField field = null;
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        field = fields.get_Field(i);
                        if (field.Type == esriFieldType.esriFieldTypeDouble || field.Type == esriFieldType.esriFieldTypeInteger || field.Type == esriFieldType.esriFieldTypeSingle || field.Type == esriFieldType.esriFieldTypeSmallInteger)
                            cboBufferField.Items.Add(field.Name);
                    }
                }

            }
            else
                cboBufferField.Enabled = false;
        }


        
    }
}
