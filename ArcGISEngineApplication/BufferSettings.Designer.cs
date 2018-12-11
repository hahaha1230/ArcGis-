namespace ArcGISEngineApplication
{
    partial class BufferSettings
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.numUpDownInputLevel = new System.Windows.Forms.NumericUpDown();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.lblOverlayLevel = new System.Windows.Forms.Label();
            this.lblInputLevel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFeatureType = new System.Windows.Forms.Label();
            this.lblAttributeType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboBufferLayer = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBufferDistance = new System.Windows.Forms.TextBox();
            this.rdoBufferField = new System.Windows.Forms.RadioButton();
            this.rdoBufferDistance = new System.Windows.Forms.RadioButton();
            this.cboBufferField = new System.Windows.Forms.ComboBox();
            this.numUpDownOverlayLevel = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSideType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEndType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboDissolveType = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboFeatureType = new System.Windows.Forms.ComboBox();
            this.cboAttributeType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBufferAnalysis = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownInputLevel)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownOverlayLevel)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // numUpDownInputLevel
            // 
            this.numUpDownInputLevel.Location = new System.Drawing.Point(241, 92);
            this.numUpDownInputLevel.Margin = new System.Windows.Forms.Padding(4);
            this.numUpDownInputLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDownInputLevel.Name = "numUpDownInputLevel";
            this.numUpDownInputLevel.Size = new System.Drawing.Size(129, 25);
            this.numUpDownInputLevel.TabIndex = 39;
            this.numUpDownInputLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtTolerance
            // 
            this.txtTolerance.Location = new System.Drawing.Point(241, 22);
            this.txtTolerance.Margin = new System.Windows.Forms.Padding(4);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Size = new System.Drawing.Size(127, 25);
            this.txtTolerance.TabIndex = 37;
            // 
            // lblOverlayLevel
            // 
            this.lblOverlayLevel.AutoSize = true;
            this.lblOverlayLevel.Location = new System.Drawing.Point(19, 99);
            this.lblOverlayLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOverlayLevel.Name = "lblOverlayLevel";
            this.lblOverlayLevel.Size = new System.Drawing.Size(202, 15);
            this.lblOverlayLevel.TabIndex = 34;
            this.lblOverlayLevel.Text = "设置叠置分析图层精度等级：";
            // 
            // lblInputLevel
            // 
            this.lblInputLevel.AutoSize = true;
            this.lblInputLevel.Location = new System.Drawing.Point(19, 62);
            this.lblInputLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInputLevel.Name = "lblInputLevel";
            this.lblInputLevel.Size = new System.Drawing.Size(187, 15);
            this.lblInputLevel.TabIndex = 35;
            this.lblInputLevel.Text = "设置缓冲区图层精度等级：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 28);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 36;
            this.label7.Text = "设置容差：";
            // 
            // lblFeatureType
            // 
            this.lblFeatureType.AutoSize = true;
            this.lblFeatureType.Location = new System.Drawing.Point(19, 166);
            this.lblFeatureType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFeatureType.Name = "lblFeatureType";
            this.lblFeatureType.Size = new System.Drawing.Size(142, 15);
            this.lblFeatureType.TabIndex = 22;
            this.lblFeatureType.Text = "选择要素输出类型：";
            // 
            // lblAttributeType
            // 
            this.lblAttributeType.AutoSize = true;
            this.lblAttributeType.Location = new System.Drawing.Point(19, 134);
            this.lblAttributeType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttributeType.Name = "lblAttributeType";
            this.lblAttributeType.Size = new System.Drawing.Size(142, 15);
            this.lblAttributeType.TabIndex = 23;
            this.lblAttributeType.Text = "选择属性输出类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 15);
            this.label4.TabIndex = 72;
            this.label4.Text = "选择缓冲区图层：　";
            // 
            // cboBufferLayer
            // 
            this.cboBufferLayer.FormattingEnabled = true;
            this.cboBufferLayer.Location = new System.Drawing.Point(244, 30);
            this.cboBufferLayer.Margin = new System.Windows.Forms.Padding(4);
            this.cboBufferLayer.Name = "cboBufferLayer";
            this.cboBufferLayer.Size = new System.Drawing.Size(224, 23);
            this.cboBufferLayer.TabIndex = 73;
            this.cboBufferLayer.SelectedIndexChanged += new System.EventHandler(this.cboBufferLayer_SelectedIndexChanged);
            this.cboBufferLayer.SelectedValueChanged += new System.EventHandler(this.cboBufferLayer_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.txtBufferDistance);
            this.groupBox1.Controls.Add(this.rdoBufferField);
            this.groupBox1.Controls.Add(this.rdoBufferDistance);
            this.groupBox1.Controls.Add(this.cboBufferField);
            this.groupBox1.Location = new System.Drawing.Point(94, 87);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(384, 112);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置缓冲距离或字段";
            // 
            // txtBufferDistance
            // 
            this.txtBufferDistance.Location = new System.Drawing.Point(159, 24);
            this.txtBufferDistance.Margin = new System.Windows.Forms.Padding(4);
            this.txtBufferDistance.Name = "txtBufferDistance";
            this.txtBufferDistance.Size = new System.Drawing.Size(159, 25);
            this.txtBufferDistance.TabIndex = 42;
            this.txtBufferDistance.Leave += new System.EventHandler(this.txtBufferDistance_Leave);
            // 
            // rdoBufferField
            // 
            this.rdoBufferField.AutoSize = true;
            this.rdoBufferField.Location = new System.Drawing.Point(23, 65);
            this.rdoBufferField.Margin = new System.Windows.Forms.Padding(4);
            this.rdoBufferField.Name = "rdoBufferField";
            this.rdoBufferField.Size = new System.Drawing.Size(88, 19);
            this.rdoBufferField.TabIndex = 1;
            this.rdoBufferField.Text = "缓冲字段";
            this.rdoBufferField.UseVisualStyleBackColor = true;
            this.rdoBufferField.CheckedChanged += new System.EventHandler(this.rdoBufferField_CheckedChanged);
            // 
            // rdoBufferDistance
            // 
            this.rdoBufferDistance.AutoSize = true;
            this.rdoBufferDistance.Checked = true;
            this.rdoBufferDistance.Location = new System.Drawing.Point(23, 25);
            this.rdoBufferDistance.Margin = new System.Windows.Forms.Padding(4);
            this.rdoBufferDistance.Name = "rdoBufferDistance";
            this.rdoBufferDistance.Size = new System.Drawing.Size(88, 19);
            this.rdoBufferDistance.TabIndex = 0;
            this.rdoBufferDistance.TabStop = true;
            this.rdoBufferDistance.Text = "缓冲距离";
            this.rdoBufferDistance.UseVisualStyleBackColor = true;
            // 
            // cboBufferField
            // 
            this.cboBufferField.FormattingEnabled = true;
            this.cboBufferField.Location = new System.Drawing.Point(159, 62);
            this.cboBufferField.Margin = new System.Windows.Forms.Padding(4);
            this.cboBufferField.Name = "cboBufferField";
            this.cboBufferField.Size = new System.Drawing.Size(215, 23);
            this.cboBufferField.TabIndex = 41;
            this.cboBufferField.SelectedIndexChanged += new System.EventHandler(this.cboBufferField_SelectedIndexChanged);
            // 
            // numUpDownOverlayLevel
            // 
            this.numUpDownOverlayLevel.Location = new System.Drawing.Point(241, 58);
            this.numUpDownOverlayLevel.Margin = new System.Windows.Forms.Padding(4);
            this.numUpDownOverlayLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDownOverlayLevel.Name = "numUpDownOverlayLevel";
            this.numUpDownOverlayLevel.Size = new System.Drawing.Size(129, 25);
            this.numUpDownOverlayLevel.TabIndex = 38;
            this.numUpDownOverlayLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 203);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 15);
            this.label1.TabIndex = 62;
            this.label1.Text = "线缓冲的方向：　　";
            // 
            // cboSideType
            // 
            this.cboSideType.FormattingEnabled = true;
            this.cboSideType.Items.AddRange(new object[] {
            "两边",
            "左边",
            "右边"});
            this.cboSideType.Location = new System.Drawing.Point(244, 200);
            this.cboSideType.Margin = new System.Windows.Forms.Padding(4);
            this.cboSideType.Name = "cboSideType";
            this.cboSideType.Size = new System.Drawing.Size(224, 23);
            this.cboSideType.TabIndex = 66;
            this.cboSideType.SelectedIndexChanged += new System.EventHandler(this.cboSideType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 239);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 15);
            this.label3.TabIndex = 61;
            this.label3.Text = "线末端的封闭类型：";
            // 
            // cboEndType
            // 
            this.cboEndType.FormattingEnabled = true;
            this.cboEndType.Items.AddRange(new object[] {
            "圆弧型",
            "平直型"});
            this.cboEndType.Location = new System.Drawing.Point(244, 231);
            this.cboEndType.Margin = new System.Windows.Forms.Padding(4);
            this.cboEndType.Name = "cboEndType";
            this.cboEndType.Size = new System.Drawing.Size(224, 23);
            this.cboEndType.TabIndex = 65;
            this.cboEndType.SelectedIndexChanged += new System.EventHandler(this.cboEndType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(91, 270);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 15);
            this.label5.TabIndex = 63;
            this.label5.Text = "缓冲区融合类型：　";
            // 
            // cboDissolveType
            // 
            this.cboDissolveType.FormattingEnabled = true;
            this.cboDissolveType.Items.AddRange(new object[] {
            "不融合",
            "融合所有缓冲区",
            "根据字段属性融合"});
            this.cboDissolveType.Location = new System.Drawing.Point(244, 262);
            this.cboDissolveType.Margin = new System.Windows.Forms.Padding(4);
            this.cboDissolveType.Name = "cboDissolveType";
            this.cboDissolveType.Size = new System.Drawing.Size(224, 23);
            this.cboDissolveType.TabIndex = 64;
            this.cboDissolveType.SelectedIndexChanged += new System.EventHandler(this.cboDissolveType_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numUpDownOverlayLevel);
            this.groupBox2.Controls.Add(this.numUpDownInputLevel);
            this.groupBox2.Controls.Add(this.txtTolerance);
            this.groupBox2.Controls.Add(this.lblOverlayLevel);
            this.groupBox2.Controls.Add(this.lblInputLevel);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblFeatureType);
            this.groupBox2.Controls.Add(this.lblAttributeType);
            this.groupBox2.Controls.Add(this.cboFeatureType);
            this.groupBox2.Controls.Add(this.cboAttributeType);
            this.groupBox2.Location = new System.Drawing.Point(94, 308);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(384, 198);
            this.groupBox2.TabIndex = 79;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "叠置分析选项";
            // 
            // cboFeatureType
            // 
            this.cboFeatureType.FormattingEnabled = true;
            this.cboFeatureType.Items.AddRange(new object[] {
            "根据输入要素确定",
            "线",
            "点"});
            this.cboFeatureType.Location = new System.Drawing.Point(241, 161);
            this.cboFeatureType.Margin = new System.Windows.Forms.Padding(4);
            this.cboFeatureType.Name = "cboFeatureType";
            this.cboFeatureType.Size = new System.Drawing.Size(127, 23);
            this.cboFeatureType.TabIndex = 20;
            // 
            // cboAttributeType
            // 
            this.cboAttributeType.FormattingEnabled = true;
            this.cboAttributeType.Items.AddRange(new object[] {
            "所有属性",
            "不包括FID",
            "仅包括FID"});
            this.cboAttributeType.Location = new System.Drawing.Point(241, 128);
            this.cboAttributeType.Margin = new System.Windows.Forms.Padding(4);
            this.cboAttributeType.Name = "cboAttributeType";
            this.cboAttributeType.Size = new System.Drawing.Size(127, 23);
            this.cboAttributeType.TabIndex = 21;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(383, 571);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 29);
            this.btnCancel.TabIndex = 83;
            this.btnCancel.Text = "关  闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBufferAnalysis
            // 
            this.btnBufferAnalysis.Location = new System.Drawing.Point(154, 571);
            this.btnBufferAnalysis.Margin = new System.Windows.Forms.Padding(4);
            this.btnBufferAnalysis.Name = "btnBufferAnalysis";
            this.btnBufferAnalysis.Size = new System.Drawing.Size(113, 29);
            this.btnBufferAnalysis.TabIndex = 82;
            this.btnBufferAnalysis.Text = "缓冲区分析";
            this.btnBufferAnalysis.UseVisualStyleBackColor = true;
            this.btnBufferAnalysis.Click += new System.EventHandler(this.btnBufferAnalysis_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(182, 520);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(263, 25);
            this.textBox1.TabIndex = 85;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 523);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 86;
            this.label2.Text = "输出路径：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(446, 523);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 87;
            this.button1.Text = "选择";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Meters",
            "Miles",
            "Feet",
            "Millimeters",
            "Centimeters",
            "Decimeters",
            "Kilometers",
            "DecimalDegrees"});
            this.comboBox1.Location = new System.Drawing.Point(327, 26);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(47, 23);
            this.comboBox1.TabIndex = 88;
            // 
            // BufferSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 631);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboBufferLayer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBufferAnalysis);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboDissolveType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboSideType);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboEndType);
            this.Controls.Add(this.label5);
            this.Name = "BufferSettings";
            this.Text = "BufferSettings";
            this.Load += new System.EventHandler(this.BufferSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownInputLevel)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownOverlayLevel)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.NumericUpDown numUpDownInputLevel;
        private System.Windows.Forms.TextBox txtTolerance;
        private System.Windows.Forms.Label lblOverlayLevel;
        private System.Windows.Forms.Label lblInputLevel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFeatureType;
        private System.Windows.Forms.Label lblAttributeType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboBufferLayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBufferDistance;
        private System.Windows.Forms.RadioButton rdoBufferField;
        private System.Windows.Forms.RadioButton rdoBufferDistance;
        private System.Windows.Forms.ComboBox cboBufferField;
        private System.Windows.Forms.NumericUpDown numUpDownOverlayLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSideType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboEndType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboDissolveType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboFeatureType;
        private System.Windows.Forms.ComboBox cboAttributeType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBufferAnalysis;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;

    }
}