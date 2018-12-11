using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace ArcGISEngineApplication
{
    public partial class SelectByAttribute : Form
    {
        public SelectByAttribute()
        {
            InitializeComponent();
        }

        public SelectByAttribute(ILayer player)
        {
            InitializeComponent();
            this.featureLayer = (IFeatureLayer)player as IFeatureLayer;
            ILayerFields layerFields = (ILayerFields)featureLayer;
            for (int i = 0; i < layerFields.FieldCount; i++)
            {
                IField field = layerFields.get_Field(i);
                comboBox1.Items.Add(field.Name);
            }
        
        }

        //定义一个全局变量FeatureLayer
        public IFeatureLayer featureLayer;

        private void SelectByAttribute_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IQueryFilter pFilter = new QueryFilter();
            pFilter.WhereClause = textBox1.Text.ToString();//添加过滤参数

            try
            {
                ESRI.ArcGIS.Geodatabase.IFeatureCursor featureCursor = featureLayer.Search(pFilter, false);
                ESRI.ArcGIS.Geodatabase.IFeature pFeature;
                if (featureCursor == null)
                {
                    MessageBox.Show("feature cursor is null");
                }
                else
                {
                    MessageBox.Show("feature cursor is not null");

                }
                pFeature = featureCursor.NextFeature();
                this.Hide();
                refresh1(pFeature);
               // Form1 form1 = new Form1();
               // form1.flashShape(featureCursor);

            }
            catch (Exception ee)
            {
                MessageBox.Show("出现错误楼");

            }
        }

        private void refresh1(IFeature pFeature)
        {
           // IFeatureLayer pFeatureLayer = axMapControl1.Map.get_Layer(0) as IFeatureLayer;

          
           /* while (pFeature != null)
            {
                MessageBox.Show("2222");
                axMapControl1.Map.SelectFeature(pFeatureLayer, pFeature);
                MessageBox.Show("1111");
                //pFeature = pFeatCursor.NextFeature();
            }

            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);*/
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedItem.ToString();

        }
    }
}
