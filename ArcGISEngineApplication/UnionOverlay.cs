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
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;

namespace ArcGISEngineApplication
{
    public partial class UnionOverlay : Form
    {

        private string strOutputPath;
        private string outputFullPath;
        private AxMapControl axMapControl1;
        private IMap pMap;
        private double tolerance; //容差

        public UnionOverlay()
        {
            InitializeComponent();
        }
        public UnionOverlay(AxMapControl axMapControl)
        {
            InitializeComponent();
            axMapControl1 = axMapControl;
            pMap = axMapControl1.Map;
        }

        private void UnionOverlay_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            gp.AddOutputsToMap = true;
            //DisplayEnvironmentPameters(gp);
            IGeoProcessorResult results = null;

            results = UnionOverlay1(gp);

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


        private IGeoProcessorResult UnionOverlay1(Geoprocessor gp)
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

            string resultname = null;
            if (resultName.Text != null)
            {
                resultname = resultName.Text + ".shp";
            }
            else
            {
                resultname = cbInputLayer.Text + "_" + cbOverLayLayer.Text + "_" + "Intersect.shp";
            }
            string outputFullPath = System.IO.Path.Combine(strOutputPath, resultname);
            pVarArray.Add(outputFullPath);
            pVarArray.Add(tolerance);

            IGeoProcessorResult results = gp.Execute("Union_analysis", pVarArray, null) as IGeoProcessorResult;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pVarArray);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(vtobject);

            return results;
        }


        #region "根据图层名获取图层信息"
        private IFeatureLayer GetFeatureLayer(string layerName)
        {
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (pMap.get_Layer(i).Name.Equals(layerName))
                {
                    return pMap.get_Layer(i) as IFeatureLayer;
                }

            }
            return null;
        }

        #endregion

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                try
                {
                    tolerance = Convert.ToDouble(textBox2.Text);
                }
                catch (Exception error){
                    MessageBox.Show("请输入有效数值");
                    return;
                }
            }
               
        }

        private void UnionOverlay_Load_1(object sender, EventArgs e)
        {

        }
    }
}
