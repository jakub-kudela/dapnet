namespace DeclickerDisplay
{
    partial class DetectionForm
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
            this.detectionGroupBox = new System.Windows.Forms.GroupBox();
            this.detectionGraph = new ZedGraph.ZedGraphControl();
            this.countTextBox = new System.Windows.Forms.TextBox();
            this.countLabel = new System.Windows.Forms.Label();
            this.displayGroupBox = new System.Windows.Forms.GroupBox();
            this.displayIdealCheckBox = new System.Windows.Forms.CheckBox();
            this.displayAnalyzedCheckBox = new System.Windows.Forms.CheckBox();
            this.displayExcitationsSymbolsCheckBox = new System.Windows.Forms.CheckBox();
            this.displayAllButton = new System.Windows.Forms.Button();
            this.displayButton = new System.Windows.Forms.Button();
            this.displayExcitationsCheckBox = new System.Windows.Forms.CheckBox();
            this.displayDegradedSymbolsCheckBox = new System.Windows.Forms.CheckBox();
            this.indexLabel = new System.Windows.Forms.Label();
            this.displayDegradationsSymbolsCheckBox = new System.Windows.Forms.CheckBox();
            this.indexTextBox = new System.Windows.Forms.TextBox();
            this.displaySamplesSymbolsCheckBox = new System.Windows.Forms.CheckBox();
            this.displaySamplesCheckBox = new System.Windows.Forms.CheckBox();
            this.displayDegradationsCheckBox = new System.Windows.Forms.CheckBox();
            this.displayDegradedCheckBox = new System.Windows.Forms.CheckBox();
            this.statisticsGroupBox = new System.Windows.Forms.GroupBox();
            this.d_EffectivityLabel = new System.Windows.Forms.Label();
            this.d_EffectivityTextBox = new System.Windows.Forms.TextBox();
            this.a_LengthLabel = new System.Windows.Forms.Label();
            this.a_LengthTextBox = new System.Windows.Forms.TextBox();
            this.a_NumberLabel = new System.Windows.Forms.Label();
            this.i_NumberLabel = new System.Windows.Forms.Label();
            this.i_NumberTextBox = new System.Windows.Forms.TextBox();
            this.a_NumberTextBox = new System.Windows.Forms.TextBox();
            this.i_LenghtLabel = new System.Windows.Forms.Label();
            this.i_LengthTextBox = new System.Windows.Forms.TextBox();
            this.p_EffectivityLabel = new System.Windows.Forms.Label();
            this.p_EffectivityTextBox = new System.Windows.Forms.TextBox();
            this.n_EffectivityLabel = new System.Windows.Forms.Label();
            this.n_EffectivityTextBox = new System.Windows.Forms.TextBox();
            this.detectionGroupBox.SuspendLayout();
            this.displayGroupBox.SuspendLayout();
            this.statisticsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // detectionGroupBox
            // 
            this.detectionGroupBox.Controls.Add(this.detectionGraph);
            this.detectionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.detectionGroupBox.Name = "detectionGroupBox";
            this.detectionGroupBox.Size = new System.Drawing.Size(695, 465);
            this.detectionGroupBox.TabIndex = 3;
            this.detectionGroupBox.TabStop = false;
            this.detectionGroupBox.Text = "Detection";
            // 
            // detectionGraph
            // 
            this.detectionGraph.Location = new System.Drawing.Point(6, 17);
            this.detectionGraph.Name = "detectionGraph";
            this.detectionGraph.ScrollGrace = 0D;
            this.detectionGraph.ScrollMaxX = 0D;
            this.detectionGraph.ScrollMaxY = 0D;
            this.detectionGraph.ScrollMaxY2 = 0D;
            this.detectionGraph.ScrollMinX = 0D;
            this.detectionGraph.ScrollMinY = 0D;
            this.detectionGraph.ScrollMinY2 = 0D;
            this.detectionGraph.Size = new System.Drawing.Size(683, 442);
            this.detectionGraph.TabIndex = 0;
            // 
            // countTextBox
            // 
            this.countTextBox.Location = new System.Drawing.Point(75, 40);
            this.countTextBox.Name = "countTextBox";
            this.countTextBox.Size = new System.Drawing.Size(87, 20);
            this.countTextBox.TabIndex = 3;
            this.countTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(6, 43);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(35, 13);
            this.countLabel.TabIndex = 6;
            this.countLabel.Text = "Count";
            // 
            // displayGroupBox
            // 
            this.displayGroupBox.Controls.Add(this.displayIdealCheckBox);
            this.displayGroupBox.Controls.Add(this.displayAnalyzedCheckBox);
            this.displayGroupBox.Controls.Add(this.displayExcitationsSymbolsCheckBox);
            this.displayGroupBox.Controls.Add(this.displayAllButton);
            this.displayGroupBox.Controls.Add(this.displayButton);
            this.displayGroupBox.Controls.Add(this.displayExcitationsCheckBox);
            this.displayGroupBox.Controls.Add(this.displayDegradedSymbolsCheckBox);
            this.displayGroupBox.Controls.Add(this.countLabel);
            this.displayGroupBox.Controls.Add(this.indexLabel);
            this.displayGroupBox.Controls.Add(this.displayDegradationsSymbolsCheckBox);
            this.displayGroupBox.Controls.Add(this.indexTextBox);
            this.displayGroupBox.Controls.Add(this.countTextBox);
            this.displayGroupBox.Controls.Add(this.displaySamplesSymbolsCheckBox);
            this.displayGroupBox.Controls.Add(this.displaySamplesCheckBox);
            this.displayGroupBox.Controls.Add(this.displayDegradationsCheckBox);
            this.displayGroupBox.Controls.Add(this.displayDegradedCheckBox);
            this.displayGroupBox.Location = new System.Drawing.Point(713, 12);
            this.displayGroupBox.Name = "displayGroupBox";
            this.displayGroupBox.Size = new System.Drawing.Size(169, 260);
            this.displayGroupBox.TabIndex = 2;
            this.displayGroupBox.TabStop = false;
            this.displayGroupBox.Text = "Display";
            // 
            // displayIdealCheckBox
            // 
            this.displayIdealCheckBox.AutoSize = true;
            this.displayIdealCheckBox.Location = new System.Drawing.Point(6, 236);
            this.displayIdealCheckBox.Name = "displayIdealCheckBox";
            this.displayIdealCheckBox.Size = new System.Drawing.Size(49, 17);
            this.displayIdealCheckBox.TabIndex = 23;
            this.displayIdealCheckBox.Text = "Ideal";
            this.displayIdealCheckBox.UseVisualStyleBackColor = true;
            this.displayIdealCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayAnalyzedCheckBox
            // 
            this.displayAnalyzedCheckBox.AutoSize = true;
            this.displayAnalyzedCheckBox.Location = new System.Drawing.Point(6, 213);
            this.displayAnalyzedCheckBox.Name = "displayAnalyzedCheckBox";
            this.displayAnalyzedCheckBox.Size = new System.Drawing.Size(69, 17);
            this.displayAnalyzedCheckBox.TabIndex = 22;
            this.displayAnalyzedCheckBox.Text = "Analyzed";
            this.displayAnalyzedCheckBox.UseVisualStyleBackColor = true;
            this.displayAnalyzedCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayExcitationsSymbolsCheckBox
            // 
            this.displayExcitationsSymbolsCheckBox.AutoSize = true;
            this.displayExcitationsSymbolsCheckBox.Location = new System.Drawing.Point(124, 121);
            this.displayExcitationsSymbolsCheckBox.Name = "displayExcitationsSymbolsCheckBox";
            this.displayExcitationsSymbolsCheckBox.Size = new System.Drawing.Size(38, 17);
            this.displayExcitationsSymbolsCheckBox.TabIndex = 21;
            this.displayExcitationsSymbolsCheckBox.Text = "[+]";
            this.displayExcitationsSymbolsCheckBox.UseVisualStyleBackColor = true;
            this.displayExcitationsSymbolsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayAllButton
            // 
            this.displayAllButton.Location = new System.Drawing.Point(6, 92);
            this.displayAllButton.Name = "displayAllButton";
            this.displayAllButton.Size = new System.Drawing.Size(156, 23);
            this.displayAllButton.TabIndex = 19;
            this.displayAllButton.Text = "Display all";
            this.displayAllButton.UseVisualStyleBackColor = true;
            this.displayAllButton.Click += new System.EventHandler(this.displayAllButton_Click);
            // 
            // displayButton
            // 
            this.displayButton.Location = new System.Drawing.Point(6, 66);
            this.displayButton.Name = "displayButton";
            this.displayButton.Size = new System.Drawing.Size(156, 23);
            this.displayButton.TabIndex = 18;
            this.displayButton.Text = "Display";
            this.displayButton.UseVisualStyleBackColor = true;
            this.displayButton.Click += new System.EventHandler(this.displayButton_Click);
            // 
            // displayExcitationsCheckBox
            // 
            this.displayExcitationsCheckBox.AutoSize = true;
            this.displayExcitationsCheckBox.Location = new System.Drawing.Point(6, 121);
            this.displayExcitationsCheckBox.Name = "displayExcitationsCheckBox";
            this.displayExcitationsCheckBox.Size = new System.Drawing.Size(77, 17);
            this.displayExcitationsCheckBox.TabIndex = 20;
            this.displayExcitationsCheckBox.Text = "Excitations";
            this.displayExcitationsCheckBox.UseVisualStyleBackColor = true;
            this.displayExcitationsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayDegradedSymbolsCheckBox
            // 
            this.displayDegradedSymbolsCheckBox.AutoSize = true;
            this.displayDegradedSymbolsCheckBox.Location = new System.Drawing.Point(124, 167);
            this.displayDegradedSymbolsCheckBox.Name = "displayDegradedSymbolsCheckBox";
            this.displayDegradedSymbolsCheckBox.Size = new System.Drawing.Size(38, 17);
            this.displayDegradedSymbolsCheckBox.TabIndex = 17;
            this.displayDegradedSymbolsCheckBox.Text = "[+]";
            this.displayDegradedSymbolsCheckBox.UseVisualStyleBackColor = true;
            this.displayDegradedSymbolsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // indexLabel
            // 
            this.indexLabel.AutoSize = true;
            this.indexLabel.Location = new System.Drawing.Point(6, 17);
            this.indexLabel.Name = "indexLabel";
            this.indexLabel.Size = new System.Drawing.Size(33, 13);
            this.indexLabel.TabIndex = 5;
            this.indexLabel.Text = "Index";
            // 
            // displayDegradationsSymbolsCheckBox
            // 
            this.displayDegradationsSymbolsCheckBox.AutoSize = true;
            this.displayDegradationsSymbolsCheckBox.Location = new System.Drawing.Point(124, 190);
            this.displayDegradationsSymbolsCheckBox.Name = "displayDegradationsSymbolsCheckBox";
            this.displayDegradationsSymbolsCheckBox.Size = new System.Drawing.Size(38, 17);
            this.displayDegradationsSymbolsCheckBox.TabIndex = 16;
            this.displayDegradationsSymbolsCheckBox.Text = "[+]";
            this.displayDegradationsSymbolsCheckBox.UseVisualStyleBackColor = true;
            this.displayDegradationsSymbolsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // indexTextBox
            // 
            this.indexTextBox.AcceptsReturn = true;
            this.indexTextBox.Location = new System.Drawing.Point(75, 14);
            this.indexTextBox.Name = "indexTextBox";
            this.indexTextBox.Size = new System.Drawing.Size(87, 20);
            this.indexTextBox.TabIndex = 4;
            this.indexTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // displaySamplesSymbolsCheckBox
            // 
            this.displaySamplesSymbolsCheckBox.AutoSize = true;
            this.displaySamplesSymbolsCheckBox.Location = new System.Drawing.Point(124, 144);
            this.displaySamplesSymbolsCheckBox.Name = "displaySamplesSymbolsCheckBox";
            this.displaySamplesSymbolsCheckBox.Size = new System.Drawing.Size(38, 17);
            this.displaySamplesSymbolsCheckBox.TabIndex = 13;
            this.displaySamplesSymbolsCheckBox.Text = "[+]";
            this.displaySamplesSymbolsCheckBox.UseVisualStyleBackColor = true;
            this.displaySamplesSymbolsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displaySamplesCheckBox
            // 
            this.displaySamplesCheckBox.AutoSize = true;
            this.displaySamplesCheckBox.Location = new System.Drawing.Point(6, 144);
            this.displaySamplesCheckBox.Name = "displaySamplesCheckBox";
            this.displaySamplesCheckBox.Size = new System.Drawing.Size(66, 17);
            this.displaySamplesCheckBox.TabIndex = 12;
            this.displaySamplesCheckBox.Text = "Samples";
            this.displaySamplesCheckBox.UseVisualStyleBackColor = true;
            this.displaySamplesCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayDegradationsCheckBox
            // 
            this.displayDegradationsCheckBox.AutoSize = true;
            this.displayDegradationsCheckBox.Location = new System.Drawing.Point(6, 190);
            this.displayDegradationsCheckBox.Name = "displayDegradationsCheckBox";
            this.displayDegradationsCheckBox.Size = new System.Drawing.Size(89, 17);
            this.displayDegradationsCheckBox.TabIndex = 14;
            this.displayDegradationsCheckBox.Text = "Degradations";
            this.displayDegradationsCheckBox.UseVisualStyleBackColor = true;
            this.displayDegradationsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayDegradedCheckBox
            // 
            this.displayDegradedCheckBox.AutoSize = true;
            this.displayDegradedCheckBox.Location = new System.Drawing.Point(6, 167);
            this.displayDegradedCheckBox.Name = "displayDegradedCheckBox";
            this.displayDegradedCheckBox.Size = new System.Drawing.Size(73, 17);
            this.displayDegradedCheckBox.TabIndex = 15;
            this.displayDegradedCheckBox.Text = "Degraded";
            this.displayDegradedCheckBox.UseVisualStyleBackColor = true;
            this.displayDegradedCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // statisticsGroupBox
            // 
            this.statisticsGroupBox.Controls.Add(this.d_EffectivityLabel);
            this.statisticsGroupBox.Controls.Add(this.d_EffectivityTextBox);
            this.statisticsGroupBox.Controls.Add(this.a_LengthLabel);
            this.statisticsGroupBox.Controls.Add(this.a_LengthTextBox);
            this.statisticsGroupBox.Controls.Add(this.a_NumberLabel);
            this.statisticsGroupBox.Controls.Add(this.i_NumberLabel);
            this.statisticsGroupBox.Controls.Add(this.i_NumberTextBox);
            this.statisticsGroupBox.Controls.Add(this.a_NumberTextBox);
            this.statisticsGroupBox.Controls.Add(this.i_LenghtLabel);
            this.statisticsGroupBox.Controls.Add(this.i_LengthTextBox);
            this.statisticsGroupBox.Controls.Add(this.p_EffectivityLabel);
            this.statisticsGroupBox.Controls.Add(this.p_EffectivityTextBox);
            this.statisticsGroupBox.Controls.Add(this.n_EffectivityLabel);
            this.statisticsGroupBox.Controls.Add(this.n_EffectivityTextBox);
            this.statisticsGroupBox.Location = new System.Drawing.Point(713, 278);
            this.statisticsGroupBox.Name = "statisticsGroupBox";
            this.statisticsGroupBox.Size = new System.Drawing.Size(169, 199);
            this.statisticsGroupBox.TabIndex = 4;
            this.statisticsGroupBox.TabStop = false;
            this.statisticsGroupBox.Text = "Statistics";
            // 
            // d_EffectivityLabel
            // 
            this.d_EffectivityLabel.AutoSize = true;
            this.d_EffectivityLabel.Location = new System.Drawing.Point(6, 120);
            this.d_EffectivityLabel.Name = "d_EffectivityLabel";
            this.d_EffectivityLabel.Size = new System.Drawing.Size(67, 13);
            this.d_EffectivityLabel.TabIndex = 32;
            this.d_EffectivityLabel.Text = "D_Effectivity";
            // 
            // d_EffectivityTextBox
            // 
            this.d_EffectivityTextBox.AcceptsReturn = true;
            this.d_EffectivityTextBox.Location = new System.Drawing.Point(75, 117);
            this.d_EffectivityTextBox.Name = "d_EffectivityTextBox";
            this.d_EffectivityTextBox.ReadOnly = true;
            this.d_EffectivityTextBox.Size = new System.Drawing.Size(87, 20);
            this.d_EffectivityTextBox.TabIndex = 31;
            this.d_EffectivityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // a_LengthLabel
            // 
            this.a_LengthLabel.AutoSize = true;
            this.a_LengthLabel.Location = new System.Drawing.Point(6, 42);
            this.a_LengthLabel.Name = "a_LengthLabel";
            this.a_LengthLabel.Size = new System.Drawing.Size(53, 13);
            this.a_LengthLabel.TabIndex = 30;
            this.a_LengthLabel.Text = "A_Length";
            // 
            // a_LengthTextBox
            // 
            this.a_LengthTextBox.AcceptsReturn = true;
            this.a_LengthTextBox.Location = new System.Drawing.Point(76, 39);
            this.a_LengthTextBox.Name = "a_LengthTextBox";
            this.a_LengthTextBox.ReadOnly = true;
            this.a_LengthTextBox.Size = new System.Drawing.Size(87, 20);
            this.a_LengthTextBox.TabIndex = 29;
            this.a_LengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // a_NumberLabel
            // 
            this.a_NumberLabel.AutoSize = true;
            this.a_NumberLabel.Location = new System.Drawing.Point(6, 16);
            this.a_NumberLabel.Name = "a_NumberLabel";
            this.a_NumberLabel.Size = new System.Drawing.Size(57, 13);
            this.a_NumberLabel.TabIndex = 26;
            this.a_NumberLabel.Text = "A_Number";
            // 
            // i_NumberLabel
            // 
            this.i_NumberLabel.AutoSize = true;
            this.i_NumberLabel.Location = new System.Drawing.Point(6, 68);
            this.i_NumberLabel.Name = "i_NumberLabel";
            this.i_NumberLabel.Size = new System.Drawing.Size(53, 13);
            this.i_NumberLabel.TabIndex = 18;
            this.i_NumberLabel.Text = "I_Number";
            // 
            // i_NumberTextBox
            // 
            this.i_NumberTextBox.AcceptsReturn = true;
            this.i_NumberTextBox.Location = new System.Drawing.Point(75, 65);
            this.i_NumberTextBox.Name = "i_NumberTextBox";
            this.i_NumberTextBox.ReadOnly = true;
            this.i_NumberTextBox.Size = new System.Drawing.Size(87, 20);
            this.i_NumberTextBox.TabIndex = 17;
            this.i_NumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // a_NumberTextBox
            // 
            this.a_NumberTextBox.AcceptsReturn = true;
            this.a_NumberTextBox.Location = new System.Drawing.Point(76, 13);
            this.a_NumberTextBox.Name = "a_NumberTextBox";
            this.a_NumberTextBox.ReadOnly = true;
            this.a_NumberTextBox.Size = new System.Drawing.Size(87, 20);
            this.a_NumberTextBox.TabIndex = 25;
            this.a_NumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // i_LenghtLabel
            // 
            this.i_LenghtLabel.AutoSize = true;
            this.i_LenghtLabel.Location = new System.Drawing.Point(6, 94);
            this.i_LenghtLabel.Name = "i_LenghtLabel";
            this.i_LenghtLabel.Size = new System.Drawing.Size(49, 13);
            this.i_LenghtLabel.TabIndex = 22;
            this.i_LenghtLabel.Text = "I_Length";
            // 
            // i_LengthTextBox
            // 
            this.i_LengthTextBox.AcceptsReturn = true;
            this.i_LengthTextBox.Location = new System.Drawing.Point(75, 91);
            this.i_LengthTextBox.Name = "i_LengthTextBox";
            this.i_LengthTextBox.ReadOnly = true;
            this.i_LengthTextBox.Size = new System.Drawing.Size(87, 20);
            this.i_LengthTextBox.TabIndex = 21;
            this.i_LengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // p_EffectivityLabel
            // 
            this.p_EffectivityLabel.AutoSize = true;
            this.p_EffectivityLabel.Location = new System.Drawing.Point(6, 172);
            this.p_EffectivityLabel.Name = "p_EffectivityLabel";
            this.p_EffectivityLabel.Size = new System.Drawing.Size(66, 13);
            this.p_EffectivityLabel.TabIndex = 20;
            this.p_EffectivityLabel.Text = "P_Effectivity";
            // 
            // p_EffectivityTextBox
            // 
            this.p_EffectivityTextBox.AcceptsReturn = true;
            this.p_EffectivityTextBox.Location = new System.Drawing.Point(75, 169);
            this.p_EffectivityTextBox.Name = "p_EffectivityTextBox";
            this.p_EffectivityTextBox.ReadOnly = true;
            this.p_EffectivityTextBox.Size = new System.Drawing.Size(87, 20);
            this.p_EffectivityTextBox.TabIndex = 19;
            this.p_EffectivityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // n_EffectivityLabel
            // 
            this.n_EffectivityLabel.AutoSize = true;
            this.n_EffectivityLabel.Location = new System.Drawing.Point(6, 146);
            this.n_EffectivityLabel.Name = "n_EffectivityLabel";
            this.n_EffectivityLabel.Size = new System.Drawing.Size(67, 13);
            this.n_EffectivityLabel.TabIndex = 10;
            this.n_EffectivityLabel.Text = "N_Effectivity";
            // 
            // n_EffectivityTextBox
            // 
            this.n_EffectivityTextBox.AcceptsReturn = true;
            this.n_EffectivityTextBox.Location = new System.Drawing.Point(76, 143);
            this.n_EffectivityTextBox.Name = "n_EffectivityTextBox";
            this.n_EffectivityTextBox.ReadOnly = true;
            this.n_EffectivityTextBox.Size = new System.Drawing.Size(87, 20);
            this.n_EffectivityTextBox.TabIndex = 9;
            this.n_EffectivityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DetectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 492);
            this.Controls.Add(this.statisticsGroupBox);
            this.Controls.Add(this.detectionGroupBox);
            this.Controls.Add(this.displayGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DetectionForm";
            this.Text = "Detection";
            this.Load += new System.EventHandler(this.DetectionForm_Load);
            this.detectionGroupBox.ResumeLayout(false);
            this.displayGroupBox.ResumeLayout(false);
            this.displayGroupBox.PerformLayout();
            this.statisticsGroupBox.ResumeLayout(false);
            this.statisticsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox detectionGroupBox;
        private ZedGraph.ZedGraphControl detectionGraph;
        private System.Windows.Forms.TextBox countTextBox;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.GroupBox displayGroupBox;
        private System.Windows.Forms.Label indexLabel;
        private System.Windows.Forms.TextBox indexTextBox;
        private System.Windows.Forms.GroupBox statisticsGroupBox;
        private System.Windows.Forms.Label a_LengthLabel;
        private System.Windows.Forms.TextBox a_LengthTextBox;
        private System.Windows.Forms.Label a_NumberLabel;
        private System.Windows.Forms.TextBox a_NumberTextBox;
        private System.Windows.Forms.Label i_LenghtLabel;
        private System.Windows.Forms.TextBox i_LengthTextBox;
        private System.Windows.Forms.Label p_EffectivityLabel;
        private System.Windows.Forms.TextBox p_EffectivityTextBox;
        private System.Windows.Forms.Label i_NumberLabel;
        private System.Windows.Forms.TextBox i_NumberTextBox;
        private System.Windows.Forms.Label n_EffectivityLabel;
        private System.Windows.Forms.TextBox n_EffectivityTextBox;
        private System.Windows.Forms.CheckBox displayDegradedSymbolsCheckBox;
        private System.Windows.Forms.CheckBox displayDegradationsSymbolsCheckBox;
        private System.Windows.Forms.CheckBox displaySamplesSymbolsCheckBox;
        private System.Windows.Forms.CheckBox displaySamplesCheckBox;
        private System.Windows.Forms.CheckBox displayDegradedCheckBox;
        private System.Windows.Forms.CheckBox displayDegradationsCheckBox;
        private System.Windows.Forms.Button displayAllButton;
        private System.Windows.Forms.Button displayButton;
        private System.Windows.Forms.Label d_EffectivityLabel;
        private System.Windows.Forms.TextBox d_EffectivityTextBox;
        private System.Windows.Forms.CheckBox displayExcitationsSymbolsCheckBox;
        private System.Windows.Forms.CheckBox displayExcitationsCheckBox;
        private System.Windows.Forms.CheckBox displayIdealCheckBox;
        private System.Windows.Forms.CheckBox displayAnalyzedCheckBox;


    }
}

