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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;


namespace ArcGISEngineApplication
{
    public partial class NearPolygon : Form
    {
        private IMap pMap;
        private ILayer layers;
        private IFeatureLayer currentLayer;
        private int layerCount;
        private AxMapControl axMapControl1;

        public NearPolygon()
        {
            InitializeComponent();
        }

        public NearPolygon(IMap map,ILayer pLayer,AxMapControl axMapControl)
        {
            InitializeComponent();
            pMap = map;
            layers = pLayer;
            axMapControl1 = axMapControl;
            layerCount = pMap.LayerCount;
        }



        private void NearPolygon_Load(object sender, EventArgs e)
        {
          
            for (int i = 0; i < layerCount; i++)
            { 
                IFeatureLayer pFeatureLayer=pMap.get_Layer(i) as IFeatureLayer;
                if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    comboBox1.Items.Add(pFeatureLayer.Name);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pMap.ClearSelection();
            Form1.pMouseOperator = "selectPolygon";
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < layerCount; i++)
            {
                if (pMap.get_Layer(i).Name.Equals(comboBox1.Text))
                {
                    currentLayer = pMap.get_Layer(i) as IFeatureLayer;
                }
            }
            if (currentLayer != null)
            {
                IFeatureSelection featureSelection = currentLayer as IFeatureSelection;
                ISelectionSet selectionSet = featureSelection.SelectionSet;
                IEnumIDs selectedIDs = selectionSet.IDs;
                selectedIDs.Reset();
                int selectedID = selectedIDs.Next();
                if (selectedID == -1)
                {
                    return;
                }
                IFeature baseFeature = currentLayer.FeatureClass.GetFeature(selectedID);

                if (baseFeature != null)
                {
                    IGeometry baseGeom = baseFeature.Shape;
                    ISpatialFilter pSFilter = new SpatialFilterClass();
                    pSFilter.Geometry = baseGeom;
                    pSFilter.GeometryField = "Shape";
                    pSFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelTouches;
                   
                    featureSelection.SelectFeatures(pSFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, axMapControl1.ActiveView.Extent);
                }
                else
                {
                    MessageBox.Show("筛选之后为空");
                }

            }
            else
            {
                MessageBox.Show("请先选择面图层里面的一个要素");
            }
           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //打开所选要素的属性表
        private void button3_Click(object sender, EventArgs e)
        {
          
            IFeatureLayer featureLayer = currentLayer as IFeatureLayer;
            IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
            long id = featureSelection.SelectionSet.IDs.Next();
            IFeatureClass featureClass = featureLayer.FeatureClass;

            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "OBJECTID =" + id;
           // int fieldPosition = featureClass.FindField("井号");
            IFeatureCursor featureCursor = featureClass.Search(queryFilter, true);
            IFeature feature = null;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                Form2 form2 = new Form2(currentLayer,pMap);
                form2.Show();
            }
        }
    }
}
