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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;


namespace ArcGISEngineApplication
{
    public partial class Form2 : Form
    {
        public ILayer currentLayer;
        public static string bbb;
        private IMap m_map = null;
        private IActiveView m_activeView = null;
        string strOBJECTID = null;
        public Form2(ILayer layer,IMap pMap)
        {
           // ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeComponent();
            currentLayer= layer;
            m_map = pMap;
            m_activeView = m_map as IActiveView;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = currentLayer.Name + "  属性表";
            FindOIDField();
            if (strOBJECTID == null) return;

            IFeatureLayer pFeatureLayer = currentLayer as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
            IFeature pFeature = pFeatureCursor.NextFeature();
            IFields pFields = pFeatureClass.Fields;

            toolStripStatusLabel1.Text = "总记录数：" + pFeatureLayer.FeatureClass.FeatureCount(null).ToString();

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
            
            dataGridView1.DataSource = pTable;
         
        }

        private void FindOIDField()
        {
            if (currentLayer == null) return;
            IFields fields = (currentLayer as IFeatureLayer).FeatureClass.Fields;
            IField field;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                field = fields.get_Field(i);
                if (field.Type == esriFieldType.esriFieldTypeOID)
                {
                    strOBJECTID = field.Name;
                    break;
                }
            }
        }

        public static  string ParseFieldType(esriFieldType fieldType)
        {
            switch (fieldType)
            {
                case esriFieldType.esriFieldTypeBlob:
                    return "System.String";
                case esriFieldType.esriFieldTypeDate:
                    return "System.DateTime";
                case esriFieldType.esriFieldTypeDouble:
                    return "System.Double";
                case esriFieldType.esriFieldTypeGeometry:
                    return "System.String";
                case esriFieldType.esriFieldTypeGlobalID:
                    return "System.String";
                case esriFieldType.esriFieldTypeGUID:
                    return "System.String";
                case esriFieldType.esriFieldTypeInteger:
                    return "System.Int32";
                case esriFieldType.esriFieldTypeOID:
                    return "System.String";
                case esriFieldType.esriFieldTypeRaster:
                    return "System.String";
                case esriFieldType.esriFieldTypeSingle:
                    return "System.Single";
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "System.Int32";
                case esriFieldType.esriFieldTypeString:
                    return "System.String";
                default:
                    return "System.String";
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            m_map.ClearSelection();
            m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null,
                m_activeView.Extent);

            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
            if (selectedRows == null)
            {
                return;
            }

            string strOID = string.Empty;
            List<string> OIDList = new List<string>();

            for (int i = 0; i < selectedRows.Count; i++)
            {
                DataGridViewRow row = selectedRows[i];
                strOID = row.Cells[strOBJECTID].Value.ToString();
                OIDList.Add(strOID);
            }
            SelectFeatures(OIDList);
            toolStripStatusLabel2.Text = "选择记录数：" + OIDList.Count.ToString();

        }
        //选择要素
        private void SelectFeatures(List<string> oidList)
        {
            IFeatureClass featureClass = (currentLayer as IFeatureLayer).FeatureClass;
            string strID = string.Empty;
            string[] IDs = oidList.ToArray();
            for (int i = 0; i < IDs.Length; i++)
            {
                strID = IDs[i];
                 IFeature selectedFeature = featureClass.GetFeature(Convert.ToInt32(strID));
                m_map.SelectFeature(currentLayer as IFeatureLayer, selectedFeature);

            }
            m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null,
                m_activeView.Extent);

        }

        private void 按属性查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       
    }
}
