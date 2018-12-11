namespace ArcGISEngineApplication
{
    partial class Intersection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbInputLayer = new System.Windows.Forms.ComboBox();
            this.cbOverLayLayer = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.resultName = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入图层";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "叠置图层";
            // 
            // cbInputLayer
            // 
            this.cbInputLayer.FormattingEnabled = true;
            this.cbInputLayer.Location = new System.Drawing.Point(135, 46);
            this.cbInputLayer.Name = "cbInputLayer";
            this.cbInputLayer.Size = new System.Drawing.Size(264, 23);
            this.cbInputLayer.TabIndex = 2;
            this.cbInputLayer.SelectedIndexChanged += new System.EventHandler(this.cbInputLayer_SelectedIndexChanged);
            // 
            // cbOverLayLayer
            // 
            this.cbOverLayLayer.FormattingEnabled = true;
            this.cbOverLayLayer.Location = new System.Drawing.Point(137, 91);
            this.cbOverLayLayer.Name = "cbOverLayLayer";
            this.cbOverLayLayer.Size = new System.Drawing.Size(261, 23);
            this.cbOverLayLayer.TabIndex = 3;
            this.cbOverLayLayer.SelectedIndexChanged += new System.EventHandler(this.cbOverLayLayer_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(160, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 33);
            this.button1.TabIndex = 4;
            this.button1.Text = "叠置求交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(307, 255);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 29);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "输出路径";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 206);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(248, 25);
            this.textBox1.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(409, 206);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 25);
            this.button3.TabIndex = 8;
            this.button3.Text = "选择路径";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "结果名";
            // 
            // resultName
            // 
            this.resultName.Location = new System.Drawing.Point(154, 172);
            this.resultName.Name = "resultName";
            this.resultName.Size = new System.Drawing.Size(231, 25);
            this.resultName.TabIndex = 10;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 289);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(435, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // Intersection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 360);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.resultName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbOverLayLayer);
            this.Controls.Add(this.cbInputLayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Intersection";
            this.Text = "Intersection";
            this.Load += new System.EventHandler(this.Intersection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbInputLayer;
        private System.Windows.Forms.ComboBox cbOverLayLayer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox resultName;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}