using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;

namespace ArcGISEngineApplication
{
    public partial class AddCompass : Form
    {
        public IStyleGalleryItem m_styleGalleryItem;
        string stylesPath = string.Empty;
        esriSymbologyStyleClass styleClass;
        public AddCompass()
        {
            InitializeComponent();
        }
        public AddCompass(esriSymbologyStyleClass symStyleClass)
        {
            InitializeComponent();
            styleClass = symStyleClass;    
        }


        private void AddCompass_Load(object sender, EventArgs e)
        {
            LoadStyles();
        }


        private void LoadStyles()
        {
            //Get the ArcGIS install location
            string sInstall = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
            string defaultStyle = System.IO.Path.Combine(sInstall, "Styles\\ESRI.ServerStyle");
            if (System.IO.File.Exists(defaultStyle))
            {
                //Load the ESRI.ServerStyle file into the SymbologyControl
                axSymbologyControl1.LoadStyleFile(defaultStyle);
                axSymbologyControl1.StyleClass = styleClass;
                axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass).SelectItem(0);
                cbxStyles.Text = defaultStyle;
            }
            stylesPath = sInstall + "\\Styles";
            cbxStyles.Items.Clear();
            cbxStylesAddItems(stylesPath);
        }


        private void cbxStylesAddItems(string path)
        {
            string[] serverstyleFiles = System.IO.Directory.GetFiles(stylesPath, "*.serverstyle", System.IO.SearchOption.AllDirectories);

            string[] styleFiles = System.IO.Directory.GetFiles(stylesPath, "*.style", System.IO.SearchOption.AllDirectories);

            cbxStylesAddItems(serverstyleFiles);
            cbxStylesAddItems(styleFiles);
        }

        private void cbxStylesAddItems(string[] files)
        {
            if (files.GetLength(0) == 0) return;
            foreach (string file in files)
            {
                cbxStyles.Items.Add(file);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            m_styleGalleryItem = null;
            this.Hide();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void PreviewImage()
        {
            //Get and set the style class 
            ISymbologyStyleClass symbologyStyleClass = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass);

            //Preview an image of the symbol
            stdole.IPictureDisp picture = symbologyStyleClass.PreviewItem(m_styleGalleryItem, pictureBox1.Width, pictureBox1.Height);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            pictureBox1.Image = image;
        }

        private void axSymbologyControl1_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            m_styleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            PreviewImage();
        }

        public IStyleGalleryItem GetItem(ESRI.ArcGIS.Controls.esriSymbologyStyleClass styleClass)
        {
            //Set the style class
            axSymbologyControl1.StyleClass = styleClass;
            axSymbologyControl1.Update();
            //Show the modal form
            this.ShowDialog();

            //Return the selected label style
            return m_styleGalleryItem;
        }

        private void cbxStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxStyles.SelectedItem == null) return;
            axSymbologyControl1.Clear();
            stylesPath = cbxStyles.SelectedItem.ToString();
            string ext = System.IO.Path.GetExtension(stylesPath).ToLower();
            if (ext == ".serverstyle")
                axSymbologyControl1.LoadStyleFile(stylesPath);
            if (ext == ".style")
                axSymbologyControl1.LoadDesktopStyleFile(stylesPath);
            axSymbologyControl1.StyleClass = styleClass;
        }

        private void btnOtherStyles_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                stylesPath = folderBrowserDialog1.SelectedPath;
                cbxStylesAddItems(stylesPath);
            }
        }
    }
}
