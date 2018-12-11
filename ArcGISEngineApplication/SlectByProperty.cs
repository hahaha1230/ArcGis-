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
    public partial class SlectByProperty : Form
    {
       private IActiveView m_activeview;
       private IMap m_map;
       private AxMapControl axMapControl1;

        private esriSelectionResultEnum selectmethod = esriSelectionResultEnum.esriSelectionResultNew;/*用来记录处理结果的方法*/
        private IFeatureSelection pFeatureSelection = null;
        private int layerCount;

        public SlectByProperty()
        {
            InitializeComponent();
        }

        public SlectByProperty(AxMapControl axmapControl,IMap pMap)
        {
            InitializeComponent();
            axMapControl1 = axmapControl;
            m_map = pMap;
            layerCount = m_map.LayerCount;
            m_activeview = axmapControl.ActiveView;

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxWhereClause.Text == string.Empty)
            {
                MessageBox.Show("请构造SQL查询语句！");
                return;
            }
            int result = ExceuteAttributeSelect();
            if (result == -1)
            {
                labelResult.Text = "查询出现错误!";
                return;
            }
            labelResult.Text = string.Format("查到{0}个对象", result);
        }

        private int ExceuteAttributeSelect()
        {
            try
            {
                /*构造查询对象 搜索被查询图层 执行查询*/
                IQueryFilter pQueryFilter = new QueryFilterClass();
                IFeatureLayer pFeatureLayer = null;
                pQueryFilter.WhereClause = textBoxWhereClause.Text;
                ILayer targetLayer = GetLayerByName(comboBoxLayers.Text);
                pFeatureLayer = targetLayer as IFeatureLayer;
                pFeatureSelection = pFeatureLayer as IFeatureSelection;
                pFeatureSelection.SelectFeatures(pQueryFilter, selectmethod, false);//选择满足条件的要素

                if (pFeatureSelection.SelectionSet.Count == 0)
                {
                    MessageBox.Show("没有查询到相关的记录！");
                    return 0;
                }

                m_activeview.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                return pFeatureSelection.SelectionSet.Count;
            }
            catch
            {
                MessageBox.Show("查询语句可能有误,请检查重试");
                return -1;
            }
        }


        private ILayer GetLayerByName(string strLayerName)
        {
            ILayer pLayer = null;
            for (int i = 0; i < layerCount; i++)
            {
                pLayer = m_map.get_Layer(i);
                if (strLayerName == pLayer.Name)
                {
                    break;
                }
            }
            return pLayer;
        }

        private void SlectByProperty_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < layerCount; i++)
            {
                comboBoxLayers.Items.Add(m_map.get_Layer(i).Name);
            }
          
        }

        private void comboBoxLayers_SelectedValueChanged(object sender, EventArgs e)
        {
            listBoxFields.Items.Clear();
            listBoxValues.Items.Clear();

            string strSelectedLayerName = comboBoxLayers.Text;
            IFeatureLayer pFeatureLayer;

            try
            {
                for (int i = 0; i < layerCount; i++)
                {
                    if (m_map.get_Layer(i).Name == strSelectedLayerName)
                    {
                        if (m_map.get_Layer(i) is IFeatureLayer)
                        {
                            pFeatureLayer = m_map.get_Layer(i) as IFeatureLayer;

                            for (int j = 0; j <= pFeatureLayer.FeatureClass.Fields.FieldCount - 1; j++)
                            {
                                listBoxFields.Items.Add(pFeatureLayer.FeatureClass.Fields.get_Field(j).Name);
                            }

                            labelDescription2.Text = strSelectedLayerName;
                        }
                        else
                        {
                            MessageBox.Show("该图层不能够进行属性查询!请重新选择");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clauseElementClicked(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = ((Button)sender).Text;
        }

        private void listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxFields_DoubleClick(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = listBoxFields.SelectedItem.ToString() + " ";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.Clear();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
