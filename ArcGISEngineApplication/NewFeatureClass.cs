using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ArcGISEngineApplication
{
    public partial class NewFeatureClass : Form
    {
        private string pDirectory;
        private string pFilePath;
        private string pFileName;
        IWorkspace pworkspace = null;

      
        public NewFeatureClass()
        {
            InitializeComponent();
        }

        private void NewFeatureClass_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pDirectory = selectDatabase();
            pFilePath = System.IO.Path.GetDirectoryName(pDirectory);
            pFileName = System.IO.Path.GetFileName(pDirectory);
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            //工作空间
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(pFilePath, 0) as IFeatureWorkspace;
           
              //创建字段集2
              IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
                 //创建必要字段
                   IObjectClassDescription ocDescription = (IObjectClassDescription)fcDescription;

            //必要字段
              IFields pFields = new FieldsClass();
           pFields = ocDescription.RequiredFields;

            //要素类的几何类型、坐标系
            int shapeFileIndex = pFields.FindField(fcDescription.ShapeFieldName);
            IField pField = pFields.get_Field(shapeFileIndex);
            IGeometryDef pGeometryDef = pField.GeometryDef;
            IGeometryDefEdit pGeometryDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();

            //更改为可选的坐标系
            IProjectedCoordinateSystem pProjectedCoordinateSystem = pSpatialReferenceFactory.CreateProjectedCoordinateSystem(
               (int)esriSRProjCS4Type.esriSRProjCS_Xian1980_GK_Zone_21);

            pGeometryDefEdit.SpatialReference_2 = pProjectedCoordinateSystem;


            //自定义字段
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            addField(pFieldsEdit, "FID");
            addField(pFieldsEdit, "Row");
            addField(pFieldsEdit, "Column");
            addField(pFieldsEdit, "Number");
            //传入字段
            IFieldChecker pFieldChecker = new FieldCheckerClass();
            IEnumFieldError pEnumFieldError = null;
            IFields validatedFields=null;
            pFieldChecker.ValidateWorkspace = (IWorkspace)pFeatureWorkspace;
            pFieldChecker.Validate(pFields, out pEnumFieldError,out validatedFields);
          
            //创建要素类
            IFeatureClass pFeatureClass = pFeatureWorkspace.CreateFeatureClass(pFileName, validatedFields
                , ocDescription.InstanceCLSID, ocDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple, fcDescription.ShapeFieldName,"");

            MessageBox.Show("创建要素类成功");

        }

        private void addField(IFieldsEdit xFieldsEdit, string fieldName)
        {
            IField xField = new FieldClass();
            IFieldEdit xFieldEdit = (IFieldEdit)xField;
            xFieldEdit.Name_2 = fieldName;
            //默认设置要素字段名
            xFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            //数值精度
            xFieldEdit.Precision_2 = 7;
            xFieldEdit.Scale_2 = 3;
            xFieldsEdit.AddField(xField);
        }

        private string  selectDatabase()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "个人数据库（*.mdb）|*.mdb";
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = "";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                return path;
            }
            else
            {

                return null;
            }
        }



        /**
         * 官网示例
         * */
        public void IFeatureClass_CreateFeature_Example(IFeatureClass featureClass)
        {
            //Function is designed to work with polyline data     
            if (featureClass.ShapeType != ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline) { return; }
            //create a geometry for the features shape       
            ESRI.ArcGIS.Geometry.IPolyline polyline = new ESRI.ArcGIS.Geometry.PolylineClass();
            ESRI.ArcGIS.Geometry.IPoint point = new ESRI.ArcGIS.Geometry.PointClass();
            point.X = 0; point.Y = 0;
            polyline.FromPoint = point;
            point = new ESRI.ArcGIS.Geometry.PointClass();
            point.X = 10; point.Y = 10; polyline.ToPoint = point;
            IFeature feature = featureClass.CreateFeature();
            //Apply the constructed shape to the new features shape       
            feature.Shape = polyline;
            ISubtypes subtypes = (ISubtypes)featureClass;
            IRowSubtypes rowSubtypes = (IRowSubtypes)feature;
            if (subtypes.HasSubtype)// does the feature class have subtypes?        
            {
                rowSubtypes.SubtypeCode = 1; //in this example 1 represents the Primary Pipeline subtype       
            }
            // initalize any default values that the feature has       
            rowSubtypes.InitDefaultValues();
            //Commit the default values in the feature to the database       
            feature.Store();
            //update the value on a string field that indicates who installed the feature.  
            feature.set_Value(feature.Fields.FindField("InstalledBy"), "K Johnston");
            //Commit the updated values in the feature to the database     
            feature.Store();
        }
    }

}
