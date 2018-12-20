using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;

namespace ArcGISEngineApplication
{
    public partial class InputMapName : Form
    {
        public static string mapName = "";
        public static Font font;


        public InputMapName()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mapName = textBox1.Text;
            if (mapName == null)
            {
                MessageBox.Show("请输入有效的图名", "提示");
                return;
            }
            else
            {
                Form1 form1 = new Form1();
                this.Hide();
            }
           
        }

        private void InputMapName_Load(object sender, EventArgs e)
        {

        }
        

        private void button2_Click(object sender, EventArgs e)
        {
          
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = colorDialog1.Color;
                Form1.fontColor = colorDialog1.Color;
            }

            IColor pColor = new RgbColor();
            pColor.RGB = 255;
            
        }

        private void button3_Click(object sender, EventArgs e) 
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
                font = fontDialog1.Font;
            }

        }
    }
}
