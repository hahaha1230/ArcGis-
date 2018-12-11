namespace ArcGISEngineApplication
{
    partial class Symbolization
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLayer = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbField1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.简单着色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分级着色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.唯一值着色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.质量图着色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbClassify = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbField2 = new System.Windows.Forms.ComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gbClassify.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.gbClassify);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.cbLayer);
            this.splitContainer1.Size = new System.Drawing.Size(564, 405);
            this.splitContainer1.SplitterDistance = 89;
            this.splitContainer1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "选择图层";
            // 
            // cbLayer
            // 
            this.cbLayer.FormattingEnabled = true;
            this.cbLayer.Location = new System.Drawing.Point(143, 92);
            this.cbLayer.Name = "cbLayer";
            this.cbLayer.Size = new System.Drawing.Size(121, 23);
            this.cbLayer.TabIndex = 2;
            this.cbLayer.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(246, 36);
            this.button1.TabIndex = 4;
            this.button1.Text = "符号化";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbField1
            // 
            this.cbField1.FormattingEnabled = true;
            this.cbField1.Location = new System.Drawing.Point(29, 22);
            this.cbField1.Name = "cbField1";
            this.cbField1.Size = new System.Drawing.Size(121, 23);
            this.cbField1.TabIndex = 5;
            this.cbField1.SelectedIndexChanged += new System.EventHandler(this.cbField1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.简单着色ToolStripMenuItem,
            this.分级着色ToolStripMenuItem,
            this.唯一值着色ToolStripMenuItem,
            this.质量图着色ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(102, 405);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 简单着色ToolStripMenuItem
            // 
            this.简单着色ToolStripMenuItem.Name = "简单着色ToolStripMenuItem";
            this.简单着色ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.简单着色ToolStripMenuItem.Text = "简单着色";
            this.简单着色ToolStripMenuItem.Click += new System.EventHandler(this.简单着色ToolStripMenuItem_Click);
            // 
            // 分级着色ToolStripMenuItem
            // 
            this.分级着色ToolStripMenuItem.Name = "分级着色ToolStripMenuItem";
            this.分级着色ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.分级着色ToolStripMenuItem.Text = "分级着色";
            this.分级着色ToolStripMenuItem.Click += new System.EventHandler(this.分级着色ToolStripMenuItem_Click);
            // 
            // 唯一值着色ToolStripMenuItem
            // 
            this.唯一值着色ToolStripMenuItem.Name = "唯一值着色ToolStripMenuItem";
            this.唯一值着色ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.唯一值着色ToolStripMenuItem.Text = "唯一值着色";
            this.唯一值着色ToolStripMenuItem.Click += new System.EventHandler(this.唯一值着色ToolStripMenuItem_Click);
            // 
            // 质量图着色ToolStripMenuItem
            // 
            this.质量图着色ToolStripMenuItem.Name = "质量图着色ToolStripMenuItem";
            this.质量图着色ToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.质量图着色ToolStripMenuItem.Text = "质量图着色";
            this.质量图着色ToolStripMenuItem.Click += new System.EventHandler(this.质量图着色ToolStripMenuItem_Click);
            // 
            // gbClassify
            // 
            this.gbClassify.Controls.Add(this.textBox1);
            this.gbClassify.Location = new System.Drawing.Point(47, 168);
            this.gbClassify.Name = "gbClassify";
            this.gbClassify.Size = new System.Drawing.Size(139, 90);
            this.gbClassify.TabIndex = 9;
            this.gbClassify.TabStop = false;
            this.gbClassify.Text = "分级个数";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbField2);
            this.groupBox1.Controls.Add(this.cbField1);
            this.groupBox1.Location = new System.Drawing.Point(271, 168);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 90);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择字段";
            // 
            // cbField2
            // 
            this.cbField2.Enabled = false;
            this.cbField2.FormattingEnabled = true;
            this.cbField2.Location = new System.Drawing.Point(29, 51);
            this.cbField2.Name = "cbField2";
            this.cbField2.Size = new System.Drawing.Size(121, 23);
            this.cbField2.TabIndex = 6;
            // 
            // Symbolization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 405);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Symbolization";
            this.Text = "Symbolization";
            this.Load += new System.EventHandler(this.Symbolization_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbClassify.ResumeLayout(false);
            this.gbClassify.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLayer;
        private System.Windows.Forms.ComboBox cbField1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 简单着色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分级着色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 唯一值着色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 质量图着色ToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbClassify;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbField2;
    }
}