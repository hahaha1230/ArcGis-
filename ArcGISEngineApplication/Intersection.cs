using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;

namespace ArcGISEngineApplication
{
    public partial class Intersection : Form
    {
        private string strOutputPath;
        private string outputFullPath;
        private AxMapControl axMapControl1;
        private IMap pMap;
        //private double tolerance; //容差
        private string operationType;
     

        public Intersection()
        {
            InitializeComponent();
        }
        public Intersection(AxMapControl axMapControl,string cmd)
        {
            InitializeComponent();
            axMapControl1 = axMapControl;
            pMap = axMapControl1.Map;
            operationType = cmd;
        }

        private void Intersection_Load(object sender, EventArgs e)
        {

            IAoInitialize m_AoInitialize = new AoInitializeClass();
esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseUnavailable;

        licenseStatus = m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);

            for (int i = 0; i < pMap.LayerCount; i++)
            {
                cbInputLayer.Items.Add(pMap.get_Layer(i).Name);
            }
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                cbOverLayLayer.Items.Add(pMap.get_Layer(i).Name);
            }

        }

        private void cbInputLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbOverLayLayer.Items.Clear();
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (pMap.get_Layer(i).Name != cbInputLayer.Text)
                {
                    cbOverLayLayer.Items.Add(pMap.get_Layer(i).Name);
                }
            }
        }

        private void cbOverLayLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbInputLayer.Items.Clear();
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (pMap.get_Layer(i).Name != cbOverLayLayer.Text)
                {
                    cbInputLayer.Items.Add(pMap.get_Layer(i).Name);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
              Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            gp.AddOutputsToMap = true;
            IGeoProcessorResult results = null;

            string resultname = null;
            if (resultName.Text != null)
            {
                resultname = resultName.Text + ".shp";
            }
            else
            {
                resultname = cbInputLayer.Text + "_" + cbOverLayLayer.Text + "_" + "Intersect.shp";
            }
            outputFullPath = System.IO.Path.Combine(strOutputPath, resultname);

            switch (operationType)
            {
                case "intersection":
                    results = IntersectOverlay(gp);
                    break;
                case "union":
                    results = UnionOverlay(gp);
                    break;
                case "clip":
                    results = EraseOverlay(gp);
                    break;
                case "xor":
                    results = SymDiffOverlay(gp);
                    break;
            }
            
          
           

            try
            {
                //将结果添加到当前地图中
                FileInfo info = new FileInfo(outputFullPath);
                string path = outputFullPath.Substring(0, outputFullPath.Length - info.Name.Length);
                axMapControl1.AddShapeFile(path, info.Name);
                axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("出错：" + ee.Message);
            }

        }

        private IGeoProcessorResult IntersectOverlay(Geoprocessor gp)
        {
            IGpValueTableObject vtobject = new GpValueTableObjectClass();
            vtobject.SetColumns(1);
            object row = null;
            row = GetFeatureLayer(cbInputLayer.Text);
            vtobject.AddRow(ref row);
            row = GetFeatureLayer(cbOverLayLayer.Text);
            vtobject.AddRow(ref row);
            IVariantArray pVarArray = new VarArrayClass();
            pVarArray.Add(vtobject);
            pVarArray.Add(outputFullPath);

            // Execute the Intersect tool.
            IGeoProcessorResult results = gp.Execute("intersect_analysis", pVarArray, null) as IGeoProcessorResult;
            return results;
        }

        #region 擦除操作   
        private IGeoProcessorResult EraseOverlay(Geoprocessor gp)
        {
          //  ESRI.ArcGIS.AnalysisTools.Erase erase = new ESRI.ArcGIS.AnalysisTools.Erase();
          //  IFeatureLayer inputLayer = GetFeatureLayer(cbInputLayer.Text);
          //  erase.in_features = inputLayer;
          //  IFeatureLayer eraseLayer = GetFeatureLayer(cbOverLayLayer.Text);
          ////  string aaa = System.IO.Path.Combine(strOutputPath, "Erase.shp");
          //  erase.out_feature_class =outputFullPath;
          //  double tolerance=0.1;
          //  erase.cluster_tolerance =tolerance;
          //  //erase.cluster_tolerance = tolerance;

          //  IGeoProcessorResult results = (IGeoProcessorResult)gp.Execute(erase, null);

            IGpValueTableObject vtobject = new GpValueTableObjectClass();
            vtobject.SetColumns(1);
            object row = "";
            row = GetFeatureLayer(cbInputLayer.Text);
            vtobject.AddRow(ref row);
            row = GetFeatureLayer(cbOverLayLayer.Text);
            vtobject.AddRow(ref row);
            IVariantArray pVarArray = new VarArrayClass();
            pVarArray.Add(vtobject);

           
            pVarArray.Add(outputFullPath);
            double tolerance = 0.1;
            pVarArray.Add(tolerance);

            IGeoProcessorResult results = gp.Execute("Erase_analysis", pVarArray, null) as IGeoProcessorResult;
            return results;
        }
        #endregion


        #region 求和操作
        private IGeoProcessorResult UnionOverlay(Geoprocessor gp)
        {
            IGpValueTableObject vtobject = new GpValueTableObjectClass();
            vtobject.SetColumns(1);
            object row = "";
            row = GetFeatureLayer(cbInputLayer.Text);
            vtobject.AddRow(ref row);
            row = GetFeatureLayer(cbOverLayLayer.Text);
            vtobject.AddRow(ref row);
            IVariantArray pVarArray = new VarArrayClass();
            pVarArray.Add(vtobject);
            pVarArray.Add(outputFullPath);
          

            IGeoProcessorResult results = gp.Execute("Union_analysis", pVarArray, null) as IGeoProcessorResult;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pVarArray);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(vtobject);

            return results;
        }
        #endregion



        #region 异或操作
        private IGeoProcessorResult SymDiffOverlay(Geoprocessor gp)
        {
            SymDiff symDiff = new SymDiff();
            symDiff.in_features = GetFeatureLayer(cbInputLayer.Text);
            symDiff.update_features = GetFeatureLayer(cbOverLayLayer.Text);
            symDiff.out_feature_class = outputFullPath;

            IGeoProcessorResult results = (IGeoProcessorResult)gp.Execute(symDiff, null);
            return results;
        }
           #endregion 


        #region "根据图层名获取图层信息"
        private IFeatureLayer GetFeatureLayer(string layerName)
        {
            /*for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (pMap.get_Layer(i).Name.Equals(layerName))
                {
                    return pMap.get_Layer(i) as IFeatureLayer;
                }
               
            }
            return null;*/
            //get the layers from the maps
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
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";// IFeatureLayer
            //uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}";  // IGeoFeatureLayer
            //uid.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}";  // IDataLayer
            if (pMap.LayerCount != 0)
            {
                IEnumLayer layers = pMap.get_Layers(uid, true);
                return layers;
            }
            return null;
        }
        #endregion

      
        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                strOutputPath = folderBrowserDialog1.SelectedPath;
                textBox1.Text = strOutputPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           /* if (textBox2.Text != "")
            {
                try
                {
                    tolerance = Convert.ToDouble(textBox2.Text);
                }
                catch (Exception error)
                {
                    MessageBox.Show("请输入有效数值");
                    return;
                }
            }*/
        }
    }
}
