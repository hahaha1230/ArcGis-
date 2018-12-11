using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace ArcGISEngineApplication
{
    public partial class InputText : Form
    {
        public static string inputText=null;

        public InputText()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            inputText = textBox1.Text;
            
            if (inputText == null)
            {
                MessageBox.Show("提示", "请输入要插入的文字");
                return;
            }
            else
            { 
                Form1 form1 = new Form1();
                
                this.Hide();
               
            }
            
        }

        private void InputText_Load(object sender, EventArgs e)
        {

        }

       
    }
}
