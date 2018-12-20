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
    public partial class NewField : Form
    {
        private IFeatureLayer pFeatureLayer;
        public NewField()
        {
            InitializeComponent();
        }

         public NewField(IFeatureLayer pFL)
        {
            InitializeComponent();
            pFeatureLayer = pFL;
        }

        private void NewField_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {
                MessageBox.Show("请输入字段名");
                return;
            }
            try
            {
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = pField as IFieldEdit;
                pFieldEdit.Name_2 = textBox1.Text;
                pFieldEdit.Type_2 = getType();
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                pFeatureClass.AddField(pField);
                MessageBox.Show("新建字段成功");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错了");
            }
            
        }
        private esriFieldType getType()
        {
            esriFieldType type=esriFieldType.esriFieldTypeInteger;
            switch (comboBox1.Text)
            {
                case " Double":
                    type = esriFieldType.esriFieldTypeDouble;
                    break;
                case "Geometry":
                    type = esriFieldType.esriFieldTypeGeometry;
                    break;
                case "GUID":
                    type = esriFieldType.esriFieldTypeGUID;
                    break;
                case "Int":
                    type = esriFieldType.esriFieldTypeInteger;
                    break;
                case "OID":
                    type = esriFieldType.esriFieldTypeOID;
                    break;
                case "String":
                    type = esriFieldType.esriFieldTypeString;
                    break;
                default:
                    break;

            }
            return type;

        }
    }
}
