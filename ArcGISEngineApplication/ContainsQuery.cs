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

namespace ArcGISEngineApplication
{
    public partial class ContainsQuery : Form
    {
        private IMap pMap;
        private ILayer layers;
        private IFeatureLayer currentLayer;
        private int layerCount;
        private AxMapControl axMapControl1;

        public ContainsQuery()
        {
            InitializeComponent();
        }

        public ContainsQuery(IMap map, ILayer pLayer, AxMapControl axMapControl) 
        {
            InitializeComponent();
            pMap = map;
            layers = pLayer;
            axMapControl1 = axMapControl;
            layerCount = pMap.LayerCount;
        }


        private void ContainsQuery_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < layerCount; i++)
            {
                comboBox1.Items.Add(pMap.get_Layer(i).Name);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == null)
            {
                MessageBox.Show("请先选择图层");
                return;
            }

            Form1.CURRENT_LAYER_NAME = comboBox1.Text;
            Form1.pMouseOperator = "drawPolygons";
            this.Visible = false;
        }

        public void display(IFeatureCursor pFeatureCursor, IFeatureClass pFeatureClass)
        {
            dataGridView1.Visible = true;
            IFeature pFeature = pFeatureCursor.NextFeature();
            IFields pFields = pFeatureClass.Fields;




            DataTable pTable = new DataTable();
            for (int i = 0; i < pFields.FieldCount; i++)          //获取所有列
            {
                DataColumn pColumn = new DataColumn(pFields.get_Field(i).Name);
                pTable.Columns.Add(pColumn);
            }
            while (pFeature != null)
            {
                DataRow pRow = pTable.NewRow();
                for (int i = 0; i < pFields.FieldCount; i++)        //添加每一列的值
                {
                    pRow[i] = pFeature.get_Value(i);
                }
                pTable.Rows.Add(pRow);
                pFeature = pFeatureCursor.NextFeature();
            }
            MessageBox.Show("一共选择了"+pTable.Rows.Count+"条数据");
            
            dataGridView1.DataSource = pTable;
           
        }
    }
}
