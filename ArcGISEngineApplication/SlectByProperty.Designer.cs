namespace ArcGISEngineApplication
{
    partial class SlectByProperty
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
            this.comboBoxLayers = new System.Windows.Forms.ComboBox();
            this.comboBoxMethod = new System.Windows.Forms.ComboBox();
            this.Method = new System.Windows.Forms.Label();
            this.LabelLayers = new System.Windows.Forms.Label();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.Fields = new System.Windows.Forms.Label();
            this.buttonGetValue = new System.Windows.Forms.Button();
            this.buttonChar = new System.Windows.Forms.Button();
            this.buttonChars = new System.Windows.Forms.Button();
            this.buttonIs = new System.Windows.Forms.Button();
            this.buttonNot = new System.Windows.Forms.Button();
            this.buttonBrace = new System.Windows.Forms.Button();
            this.buttonOr = new System.Windows.Forms.Button();
            this.buttonBig = new System.Windows.Forms.Button();
            this.buttonBigEqual = new System.Windows.Forms.Button();
            this.buttonSmallEqual = new System.Windows.Forms.Button();
            this.buttonAnd = new System.Windows.Forms.Button();
            this.buttonSmall = new System.Windows.Forms.Button();
            this.buttonNotEqual = new System.Windows.Forms.Button();
            this.buttonLike = new System.Windows.Forms.Button();
            this.buttonEqual = new System.Windows.Forms.Button();
            this.listBoxValues = new System.Windows.Forms.ListBox();
            this.labelValues = new System.Windows.Forms.Label();
            this.textBoxWhereClause = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelDescription2 = new System.Windows.Forms.Label();
            this.labelDescription3 = new System.Windows.Forms.Label();
            this.labelDescription1 = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxLayers
            // 
            this.comboBoxLayers.BackColor = System.Drawing.Color.White;
            this.comboBoxLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayers.FormattingEnabled = true;
            this.comboBoxLayers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBoxLayers.Location = new System.Drawing.Point(107, 32);
            this.comboBoxLayers.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxLayers.Name = "comboBoxLayers";
            this.comboBoxLayers.Size = new System.Drawing.Size(423, 23);
            this.comboBoxLayers.TabIndex = 39;
            this.comboBoxLayers.SelectedValueChanged += new System.EventHandler(this.comboBoxLayers_SelectedValueChanged);
            // 
            // comboBoxMethod
            // 
            this.comboBoxMethod.BackColor = System.Drawing.SystemColors.Info;
            this.comboBoxMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethod.FormattingEnabled = true;
            this.comboBoxMethod.Items.AddRange(new object[] {
            "生成新的选择集 ",
            "添加到当前的选择集 ",
            "从当前的选择集中去除",
            "在当前的选择集中选择"});
            this.comboBoxMethod.Location = new System.Drawing.Point(107, 80);
            this.comboBoxMethod.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxMethod.Name = "comboBoxMethod";
            this.comboBoxMethod.Size = new System.Drawing.Size(423, 23);
            this.comboBoxMethod.TabIndex = 41;
            // 
            // Method
            // 
            this.Method.AutoSize = true;
            this.Method.Location = new System.Drawing.Point(16, 80);
            this.Method.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Method.Name = "Method";
            this.Method.Size = new System.Drawing.Size(75, 15);
            this.Method.TabIndex = 43;
            this.Method.Text = "查询方法:";
            // 
            // LabelLayers
            // 
            this.LabelLayers.AutoSize = true;
            this.LabelLayers.Location = new System.Drawing.Point(13, 40);
            this.LabelLayers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelLayers.Name = "LabelLayers";
            this.LabelLayers.Size = new System.Drawing.Size(75, 15);
            this.LabelLayers.TabIndex = 42;
            this.LabelLayers.Text = "图层名称:";
            // 
            // listBoxFields
            // 
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.HorizontalScrollbar = true;
            this.listBoxFields.ItemHeight = 15;
            this.listBoxFields.Location = new System.Drawing.Point(18, 160);
            this.listBoxFields.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.ScrollAlwaysVisible = true;
            this.listBoxFields.Size = new System.Drawing.Size(155, 214);
            this.listBoxFields.TabIndex = 45;
            this.listBoxFields.SelectedIndexChanged += new System.EventHandler(this.listBoxFields_SelectedIndexChanged);
            this.listBoxFields.DoubleClick += new System.EventHandler(this.listBoxFields_DoubleClick);
            // 
            // Fields
            // 
            this.Fields.AutoSize = true;
            this.Fields.Location = new System.Drawing.Point(16, 138);
            this.Fields.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Fields.Name = "Fields";
            this.Fields.Size = new System.Drawing.Size(75, 15);
            this.Fields.TabIndex = 44;
            this.Fields.Text = "属性字段:";
            // 
            // buttonGetValue
            // 
            this.buttonGetValue.Location = new System.Drawing.Point(273, 314);
            this.buttonGetValue.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGetValue.Name = "buttonGetValue";
            this.buttonGetValue.Size = new System.Drawing.Size(120, 29);
            this.buttonGetValue.TabIndex = 82;
            this.buttonGetValue.Text = "获得唯一值";
            this.buttonGetValue.UseVisualStyleBackColor = true;
            // 
            // buttonChar
            // 
            this.buttonChar.Location = new System.Drawing.Point(237, 278);
            this.buttonChar.Margin = new System.Windows.Forms.Padding(4);
            this.buttonChar.Name = "buttonChar";
            this.buttonChar.Size = new System.Drawing.Size(28, 29);
            this.buttonChar.TabIndex = 81;
            this.buttonChar.Text = "_";
            this.buttonChar.UseVisualStyleBackColor = true;
            this.buttonChar.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonChars
            // 
            this.buttonChars.Location = new System.Drawing.Point(208, 278);
            this.buttonChars.Margin = new System.Windows.Forms.Padding(4);
            this.buttonChars.Name = "buttonChars";
            this.buttonChars.Size = new System.Drawing.Size(28, 29);
            this.buttonChars.TabIndex = 80;
            this.buttonChars.Text = "%";
            this.buttonChars.UseVisualStyleBackColor = true;
            this.buttonChars.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonIs
            // 
            this.buttonIs.Location = new System.Drawing.Point(208, 314);
            this.buttonIs.Margin = new System.Windows.Forms.Padding(4);
            this.buttonIs.Name = "buttonIs";
            this.buttonIs.Size = new System.Drawing.Size(57, 29);
            this.buttonIs.TabIndex = 79;
            this.buttonIs.Text = " Is ";
            this.buttonIs.UseVisualStyleBackColor = true;
            this.buttonIs.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonNot
            // 
            this.buttonNot.Location = new System.Drawing.Point(338, 278);
            this.buttonNot.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNot.Name = "buttonNot";
            this.buttonNot.Size = new System.Drawing.Size(57, 29);
            this.buttonNot.TabIndex = 78;
            this.buttonNot.Text = " Not ";
            this.buttonNot.UseVisualStyleBackColor = true;
            this.buttonNot.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonBrace
            // 
            this.buttonBrace.Location = new System.Drawing.Point(273, 278);
            this.buttonBrace.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBrace.Name = "buttonBrace";
            this.buttonBrace.Size = new System.Drawing.Size(57, 29);
            this.buttonBrace.TabIndex = 77;
            this.buttonBrace.Text = "( )";
            this.buttonBrace.UseVisualStyleBackColor = true;
            this.buttonBrace.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonOr
            // 
            this.buttonOr.Location = new System.Drawing.Point(338, 242);
            this.buttonOr.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOr.Name = "buttonOr";
            this.buttonOr.Size = new System.Drawing.Size(57, 29);
            this.buttonOr.TabIndex = 76;
            this.buttonOr.Text = " Or ";
            this.buttonOr.UseVisualStyleBackColor = true;
            this.buttonOr.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonBig
            // 
            this.buttonBig.Location = new System.Drawing.Point(208, 205);
            this.buttonBig.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBig.Name = "buttonBig";
            this.buttonBig.Size = new System.Drawing.Size(57, 29);
            this.buttonBig.TabIndex = 75;
            this.buttonBig.Text = " > ";
            this.buttonBig.UseVisualStyleBackColor = true;
            this.buttonBig.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonBigEqual
            // 
            this.buttonBigEqual.Location = new System.Drawing.Point(273, 205);
            this.buttonBigEqual.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBigEqual.Name = "buttonBigEqual";
            this.buttonBigEqual.Size = new System.Drawing.Size(57, 29);
            this.buttonBigEqual.TabIndex = 74;
            this.buttonBigEqual.Text = " >= ";
            this.buttonBigEqual.UseVisualStyleBackColor = true;
            this.buttonBigEqual.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonSmallEqual
            // 
            this.buttonSmallEqual.Location = new System.Drawing.Point(273, 242);
            this.buttonSmallEqual.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSmallEqual.Name = "buttonSmallEqual";
            this.buttonSmallEqual.Size = new System.Drawing.Size(57, 29);
            this.buttonSmallEqual.TabIndex = 73;
            this.buttonSmallEqual.Text = " <= ";
            this.buttonSmallEqual.UseVisualStyleBackColor = true;
            this.buttonSmallEqual.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonAnd
            // 
            this.buttonAnd.Location = new System.Drawing.Point(338, 205);
            this.buttonAnd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAnd.Name = "buttonAnd";
            this.buttonAnd.Size = new System.Drawing.Size(57, 29);
            this.buttonAnd.TabIndex = 72;
            this.buttonAnd.Text = " And ";
            this.buttonAnd.UseVisualStyleBackColor = true;
            this.buttonAnd.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonSmall
            // 
            this.buttonSmall.Location = new System.Drawing.Point(208, 242);
            this.buttonSmall.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSmall.Name = "buttonSmall";
            this.buttonSmall.Size = new System.Drawing.Size(57, 29);
            this.buttonSmall.TabIndex = 71;
            this.buttonSmall.Text = " < ";
            this.buttonSmall.UseVisualStyleBackColor = true;
            this.buttonSmall.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonNotEqual
            // 
            this.buttonNotEqual.Location = new System.Drawing.Point(273, 169);
            this.buttonNotEqual.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNotEqual.Name = "buttonNotEqual";
            this.buttonNotEqual.Size = new System.Drawing.Size(57, 29);
            this.buttonNotEqual.TabIndex = 70;
            this.buttonNotEqual.Text = " < > ";
            this.buttonNotEqual.UseVisualStyleBackColor = true;
            this.buttonNotEqual.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonLike
            // 
            this.buttonLike.Location = new System.Drawing.Point(338, 169);
            this.buttonLike.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLike.Name = "buttonLike";
            this.buttonLike.Size = new System.Drawing.Size(57, 29);
            this.buttonLike.TabIndex = 69;
            this.buttonLike.Text = " Like ";
            this.buttonLike.UseVisualStyleBackColor = true;
            this.buttonLike.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // buttonEqual
            // 
            this.buttonEqual.Location = new System.Drawing.Point(208, 169);
            this.buttonEqual.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEqual.Name = "buttonEqual";
            this.buttonEqual.Size = new System.Drawing.Size(57, 29);
            this.buttonEqual.TabIndex = 68;
            this.buttonEqual.Text = " = ";
            this.buttonEqual.UseVisualStyleBackColor = true;
            this.buttonEqual.Click += new System.EventHandler(this.clauseElementClicked);
            // 
            // listBoxValues
            // 
            this.listBoxValues.FormattingEnabled = true;
            this.listBoxValues.HorizontalScrollbar = true;
            this.listBoxValues.ItemHeight = 15;
            this.listBoxValues.Location = new System.Drawing.Point(428, 160);
            this.listBoxValues.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxValues.Name = "listBoxValues";
            this.listBoxValues.ScrollAlwaysVisible = true;
            this.listBoxValues.Size = new System.Drawing.Size(149, 214);
            this.listBoxValues.TabIndex = 84;
            // 
            // labelValues
            // 
            this.labelValues.AutoSize = true;
            this.labelValues.Location = new System.Drawing.Point(418, 138);
            this.labelValues.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelValues.Name = "labelValues";
            this.labelValues.Size = new System.Drawing.Size(68, 15);
            this.labelValues.TabIndex = 83;
            this.labelValues.Text = " 属性值:";
            // 
            // textBoxWhereClause
            // 
            this.textBoxWhereClause.Location = new System.Drawing.Point(36, 420);
            this.textBoxWhereClause.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxWhereClause.Multiline = true;
            this.textBoxWhereClause.Name = "textBoxWhereClause";
            this.textBoxWhereClause.Size = new System.Drawing.Size(507, 68);
            this.textBoxWhereClause.TabIndex = 85;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(36, 496);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(100, 29);
            this.buttonClear.TabIndex = 88;
            this.buttonClear.Text = "清空语句";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(444, 496);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 29);
            this.buttonCancel.TabIndex = 87;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(333, 496);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 29);
            this.buttonOk.TabIndex = 86;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // labelDescription2
            // 
            this.labelDescription2.AutoSize = true;
            this.labelDescription2.Location = new System.Drawing.Point(190, 401);
            this.labelDescription2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDescription2.Name = "labelDescription2";
            this.labelDescription2.Size = new System.Drawing.Size(47, 15);
            this.labelDescription2.TabIndex = 91;
            this.labelDescription2.Text = "layer";
            // 
            // labelDescription3
            // 
            this.labelDescription3.AutoSize = true;
            this.labelDescription3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDescription3.Location = new System.Drawing.Point(270, 398);
            this.labelDescription3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDescription3.Name = "labelDescription3";
            this.labelDescription3.Size = new System.Drawing.Size(58, 18);
            this.labelDescription3.TabIndex = 90;
            this.labelDescription3.Text = "Where";
            // 
            // labelDescription1
            // 
            this.labelDescription1.AutoSize = true;
            this.labelDescription1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDescription1.Location = new System.Drawing.Point(33, 398);
            this.labelDescription1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDescription1.Name = "labelDescription1";
            this.labelDescription1.Size = new System.Drawing.Size(148, 18);
            this.labelDescription1.TabIndex = 89;
            this.labelDescription1.Text = "Select * From ";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelResult.Location = new System.Drawing.Point(159, 500);
            this.labelResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(0, 18);
            this.labelResult.TabIndex = 92;
            // 
            // SlectByProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 527);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.labelDescription2);
            this.Controls.Add(this.labelDescription3);
            this.Controls.Add(this.labelDescription1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxWhereClause);
            this.Controls.Add(this.listBoxValues);
            this.Controls.Add(this.labelValues);
            this.Controls.Add(this.buttonGetValue);
            this.Controls.Add(this.buttonChar);
            this.Controls.Add(this.buttonChars);
            this.Controls.Add(this.buttonIs);
            this.Controls.Add(this.buttonNot);
            this.Controls.Add(this.buttonBrace);
            this.Controls.Add(this.buttonOr);
            this.Controls.Add(this.buttonBig);
            this.Controls.Add(this.buttonBigEqual);
            this.Controls.Add(this.buttonSmallEqual);
            this.Controls.Add(this.buttonAnd);
            this.Controls.Add(this.buttonSmall);
            this.Controls.Add(this.buttonNotEqual);
            this.Controls.Add(this.buttonLike);
            this.Controls.Add(this.buttonEqual);
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.Fields);
            this.Controls.Add(this.Method);
            this.Controls.Add(this.LabelLayers);
            this.Controls.Add(this.comboBoxMethod);
            this.Controls.Add(this.comboBoxLayers);
            this.Name = "SlectByProperty";
            this.Text = "SlectByProperty";
            this.Load += new System.EventHandler(this.SlectByProperty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxLayers;
        private System.Windows.Forms.ComboBox comboBoxMethod;
        private System.Windows.Forms.Label Method;
        private System.Windows.Forms.Label LabelLayers;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.Label Fields;
        private System.Windows.Forms.Button buttonGetValue;
        private System.Windows.Forms.Button buttonChar;
        private System.Windows.Forms.Button buttonChars;
        private System.Windows.Forms.Button buttonIs;
        private System.Windows.Forms.Button buttonNot;
        private System.Windows.Forms.Button buttonBrace;
        private System.Windows.Forms.Button buttonOr;
        private System.Windows.Forms.Button buttonBig;
        private System.Windows.Forms.Button buttonBigEqual;
        private System.Windows.Forms.Button buttonSmallEqual;
        private System.Windows.Forms.Button buttonAnd;
        private System.Windows.Forms.Button buttonSmall;
        private System.Windows.Forms.Button buttonNotEqual;
        private System.Windows.Forms.Button buttonLike;
        private System.Windows.Forms.Button buttonEqual;
        private System.Windows.Forms.ListBox listBoxValues;
        private System.Windows.Forms.Label labelValues;
        private System.Windows.Forms.TextBox textBoxWhereClause;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelDescription2;
        private System.Windows.Forms.Label labelDescription3;
        private System.Windows.Forms.Label labelDescription1;
        private System.Windows.Forms.Label labelResult;
    }
}