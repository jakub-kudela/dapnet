namespace DeclickerDisplay
{
    partial class CorrectionForm
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
            this.statisticsGroupBox = new System.Windows.Forms.GroupBox();
            this.i_NumberLabel = new System.Windows.Forms.Label();
            this.i_NumberTextBox = new System.Windows.Forms.TextBox();
            this.i_LenghtLabel = new System.Windows.Forms.Label();
            this.i_LengthTextBox = new System.Windows.Forms.TextBox();
            this.c_EffectivityLabel = new System.Windows.Forms.Label();
            this.c_EffectivityTextBox = new System.Windows.Forms.TextBox();
            this.deviationLabel = new System.Windows.Forms.Label();
            this.deviationTextBox = new System.Windows.Forms.TextBox();
            this.meanLabel = new System.Windows.Forms.Label();
            this.meanTextBox = new System.Windows.Forms.TextBox();
            this.correctionGroupBox = new System.Windows.Forms.GroupBox();
            this.correctionGraph = new ZedGraph.ZedGraphControl();
            this.displayGroupBox = new System.Windows.Forms.GroupBox();
            this.displayReviewedCheckBox = new System.Windows.Forms.CheckBox();
            this.displayIdealCheckBox = new System.Windows.Forms.CheckBox();
            this.displayDegradationsSymbolsCheckBox = new System.Windows.Forms.CheckBox();
            this.displayDegradationsCheckBox = new System.Windows.Forms.CheckBox();
            this.displayAllButton = new System.Windows.Forms.Button();
            this.displayButton = new System.Windows.Forms.Button();
            this.displaySamplesSymbolsCheckBox = new System.Windows.Forms.CheckBox();
            this.countLabel = new System.Windows.Forms.Label();
            this.indexLabel = new System.Windows.Forms.Label();
            this.displayDegradedSymbolsCheckBox = new System.Windows.Forms.CheckBox();
            this.indexTextBox = new System.Windows.Forms.TextBox();
            this.countTextBox = new System.Windows.Forms.TextBox();
            this.displayCorrectedSymbolsCheckBox = new System.Windows.Forms.CheckBox();
            this.displayCorrectedCheckBox = new System.Windows.Forms.CheckBox();
            this.displaySamplesCheckBox = new System.Windows.Forms.CheckBox();
            this.displayDegradedCheckBox = new System.Windows.Forms.CheckBox();
            this.statisticsGroupBox.SuspendLayout();
            this.correctionGroupBox.SuspendLayout();
            this.displayGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // statisticsGroupBox
            // 
            this.statisticsGroupBox.Controls.Add(this.i_NumberLabel);
            this.statisticsGroupBox.Controls.Add(this.i_NumberTextBox);
            this.statisticsGroupBox.Controls.Add(this.i_LenghtLabel);
            this.statisticsGroupBox.Controls.Add(this.i_LengthTextBox);
            this.statisticsGroupBox.Controls.Add(this.c_EffectivityLabel);
            this.statisticsGroupBox.Controls.Add(this.c_EffectivityTextBox);
            this.statisticsGroupBox.Controls.Add(this.deviationLabel);
            this.statisticsGroupBox.Controls.Add(this.deviationTextBox);
            this.statisticsGroupBox.Controls.Add(this.meanLabel);
            this.statisticsGroupBox.Controls.Add(this.meanTextBox);
            this.statisticsGroupBox.Location = new System.Drawing.Point(713, 278);
            this.statisticsGroupBox.Name = "statisticsGroupBox";
            this.statisticsGroupBox.Size = new System.Drawing.Size(169, 199);
            this.statisticsGroupBox.TabIndex = 7;
            this.statisticsGroupBox.TabStop = false;
            this.statisticsGroupBox.Text = "Statistics";
            // 
            // i_NumberLabel
            // 
            this.i_NumberLabel.AutoSize = true;
            this.i_NumberLabel.Location = new System.Drawing.Point(6, 16);
            this.i_NumberLabel.Name = "i_NumberLabel";
            this.i_NumberLabel.Size = new System.Drawing.Size(53, 13);
            this.i_NumberLabel.TabIndex = 18;
            this.i_NumberLabel.Text = "I_Number";
            // 
            // i_NumberTextBox
            // 
            this.i_NumberTextBox.AcceptsReturn = true;
            this.i_NumberTextBox.Location = new System.Drawing.Point(76, 13);
            this.i_NumberTextBox.Name = "i_NumberTextBox";
            this.i_NumberTextBox.ReadOnly = true;
            this.i_NumberTextBox.Size = new System.Drawing.Size(87, 20);
            this.i_NumberTextBox.TabIndex = 17;
            this.i_NumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // i_LenghtLabel
            // 
            this.i_LenghtLabel.AutoSize = true;
            this.i_LenghtLabel.Location = new System.Drawing.Point(6, 42);
            this.i_LenghtLabel.Name = "i_LenghtLabel";
            this.i_LenghtLabel.Size = new System.Drawing.Size(49, 13);
            this.i_LenghtLabel.TabIndex = 22;
            this.i_LenghtLabel.Text = "I_Length";
            // 
            // i_LengthTextBox
            // 
            this.i_LengthTextBox.AcceptsReturn = true;
            this.i_LengthTextBox.Location = new System.Drawing.Point(75, 39);
            this.i_LengthTextBox.Name = "i_LengthTextBox";
            this.i_LengthTextBox.ReadOnly = true;
            this.i_LengthTextBox.Size = new System.Drawing.Size(87, 20);
            this.i_LengthTextBox.TabIndex = 21;
            this.i_LengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // c_EffectivityLabel
            // 
            this.c_EffectivityLabel.AutoSize = true;
            this.c_EffectivityLabel.Location = new System.Drawing.Point(6, 68);
            this.c_EffectivityLabel.Name = "c_EffectivityLabel";
            this.c_EffectivityLabel.Size = new System.Drawing.Size(66, 13);
            this.c_EffectivityLabel.TabIndex = 24;
            this.c_EffectivityLabel.Text = "C_Effectivity";
            // 
            // c_EffectivityTextBox
            // 
            this.c_EffectivityTextBox.AcceptsReturn = true;
            this.c_EffectivityTextBox.Location = new System.Drawing.Point(76, 65);
            this.c_EffectivityTextBox.Name = "c_EffectivityTextBox";
            this.c_EffectivityTextBox.ReadOnly = true;
            this.c_EffectivityTextBox.Size = new System.Drawing.Size(87, 20);
            this.c_EffectivityTextBox.TabIndex = 23;
            this.c_EffectivityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // deviationLabel
            // 
            this.deviationLabel.AutoSize = true;
            this.deviationLabel.Location = new System.Drawing.Point(5, 120);
            this.deviationLabel.Name = "deviationLabel";
            this.deviationLabel.Size = new System.Drawing.Size(52, 13);
            this.deviationLabel.TabIndex = 20;
            this.deviationLabel.Text = "Deviation";
            // 
            // deviationTextBox
            // 
            this.deviationTextBox.AcceptsReturn = true;
            this.deviationTextBox.Location = new System.Drawing.Point(75, 117);
            this.deviationTextBox.Name = "deviationTextBox";
            this.deviationTextBox.ReadOnly = true;
            this.deviationTextBox.Size = new System.Drawing.Size(87, 20);
            this.deviationTextBox.TabIndex = 19;
            this.deviationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // meanLabel
            // 
            this.meanLabel.AutoSize = true;
            this.meanLabel.Location = new System.Drawing.Point(5, 94);
            this.meanLabel.Name = "meanLabel";
            this.meanLabel.Size = new System.Drawing.Size(34, 13);
            this.meanLabel.TabIndex = 10;
            this.meanLabel.Text = "Mean";
            // 
            // meanTextBox
            // 
            this.meanTextBox.AcceptsReturn = true;
            this.meanTextBox.Location = new System.Drawing.Point(75, 91);
            this.meanTextBox.Name = "meanTextBox";
            this.meanTextBox.ReadOnly = true;
            this.meanTextBox.Size = new System.Drawing.Size(87, 20);
            this.meanTextBox.TabIndex = 9;
            this.meanTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // correctionGroupBox
            // 
            this.correctionGroupBox.Controls.Add(this.correctionGraph);
            this.correctionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.correctionGroupBox.Name = "correctionGroupBox";
            this.correctionGroupBox.Size = new System.Drawing.Size(695, 465);
            this.correctionGroupBox.TabIndex = 6;
            this.correctionGroupBox.TabStop = false;
            this.correctionGroupBox.Text = "Correction";
            // 
            // correctionGraph
            // 
            this.correctionGraph.Location = new System.Drawing.Point(6, 17);
            this.correctionGraph.Name = "correctionGraph";
            this.correctionGraph.ScrollGrace = 0D;
            this.correctionGraph.ScrollMaxX = 0D;
            this.correctionGraph.ScrollMaxY = 0D;
            this.correctionGraph.ScrollMaxY2 = 0D;
            this.correctionGraph.ScrollMinX = 0D;
            this.correctionGraph.ScrollMinY = 0D;
            this.correctionGraph.ScrollMinY2 = 0D;
            this.correctionGraph.Size = new System.Drawing.Size(683, 442);
            this.correctionGraph.TabIndex = 0;
            // 
            // displayGroupBox
            // 
            this.displayGroupBox.Controls.Add(this.displayReviewedCheckBox);
            this.displayGroupBox.Controls.Add(this.displayIdealCheckBox);
            this.displayGroupBox.Controls.Add(this.displayDegradationsSymbolsCheckBox);
            this.displayGroupBox.Controls.Add(this.displayDegradationsCheckBox);
            this.displayGroupBox.Controls.Add(this.displayAllButton);
            this.displayGroupBox.Controls.Add(this.displayButton);
            this.displayGroupBox.Controls.Add(this.displaySamplesSymbolsCheckBox);
            this.displayGroupBox.Controls.Add(this.countLabel);
            this.displayGroupBox.Controls.Add(this.indexLabel);
            this.displayGroupBox.Controls.Add(this.displayDegradedSymbolsCheckBox);
            this.displayGroupBox.Controls.Add(this.indexTextBox);
            this.displayGroupBox.Controls.Add(this.countTextBox);
            this.displayGroupBox.Controls.Add(this.displayCorrectedSymbolsCheckBox);
            this.displayGroupBox.Controls.Add(this.displayCorrectedCheckBox);
            this.displayGroupBox.Controls.Add(this.displaySamplesCheckBox);
            this.displayGroupBox.Controls.Add(this.displayDegradedCheckBox);
            this.displayGroupBox.Location = new System.Drawing.Point(713, 12);
            this.displayGroupBox.Name = "displayGroupBox";
            this.displayGroupBox.Size = new System.Drawing.Size(169, 260);
            this.displayGroupBox.TabIndex = 5;
            this.displayGroupBox.TabStop = false;
            this.displayGroupBox.Text = "Display";
            // 
            // displayReviewedCheckBox
            // 
            this.displayReviewedCheckBox.AutoSize = true;
            this.displayReviewedCheckBox.Location = new System.Drawing.Point(6, 213);
            this.displayReviewedCheckBox.Name = "displayReviewedCheckBox";
            this.displayReviewedCheckBox.Size = new System.Drawing.Size(74, 17);
            this.displayReviewedCheckBox.TabIndex = 23;
            this.displayReviewedCheckBox.Text = "Reviewed";
            this.displayReviewedCheckBox.UseVisualStyleBackColor = true;
            this.displayReviewedCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayIdealCheckBox
            // 
            this.displayIdealCheckBox.AutoSize = true;
            this.displayIdealCheckBox.Location = new System.Drawing.Point(6, 236);
            this.displayIdealCheckBox.Name = "displayIdealCheckBox";
            this.displayIdealCheckBox.Size = new System.Drawing.Size(49, 17);
            this.displayIdealCheckBox.TabIndex = 22;
            this.displayIdealCheckBox.Text = "Ideal";
            this.displayIdealCheckBox.UseVisualStyleBackColor = true;
            this.displayIdealCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayDegradationsSymbolsCheckBox
            // 
            this.displayDegradationsSymbolsCheckBox.AutoSize = true;
            this.displayDegradationsSymbolsCheckBox.Location = new System.Drawing.Point(124, 190);
            this.displayDegradationsSymbolsCheckBox.Name = "displayDegradationsSymbolsCheckBox";
            this.displayDegradationsSymbolsCheckBox.Size = new System.Drawing.Size(38, 17);
            this.displayDegradationsSymbolsCheckBox.TabIndex = 21;
            this.displayDegradationsSymbolsCheckBox.Text = "[+]";
            this.displayDegradationsSymbolsCheckBox.UseVisualStyleBackColor = true;
            this.displayDegradationsSymbolsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayDegradationsCheckBox
            // 
            this.displayDegradationsCheckBox.AutoSize = true;
            this.displayDegradationsCheckBox.Location = new System.Drawing.Point(6, 190);
            this.displayDegradationsCheckBox.Name = "displayDegradationsCheckBox";
            this.displayDegradationsCheckBox.Size = new System.Drawing.Size(89, 17);
            this.displayDegradationsCheckBox.TabIndex = 20;
            this.displayDegradationsCheckBox.Text = "Degradations";
            this.displayDegradationsCheckBox.UseVisualStyleBackColor = true;
            this.displayDegradationsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayAllButton
            // 
            this.displayAllButton.Location = new System.Drawing.Point(6, 92);
            this.displayAllButton.Name = "displayAllButton";
            this.displayAllButton.Size = new System.Drawing.Size(157, 23);
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
            // displaySamplesSymbolsCheckBox
            // 
            this.displaySamplesSymbolsCheckBox.AutoSize = true;
            this.displaySamplesSymbolsCheckBox.Location = new System.Drawing.Point(124, 144);
            this.displaySamplesSymbolsCheckBox.Name = "displaySamplesSymbolsCheckBox";
            this.displaySamplesSymbolsCheckBox.Size = new System.Drawing.Size(38, 17);
            this.displaySamplesSymbolsCheckBox.TabIndex = 17;
            this.displaySamplesSymbolsCheckBox.Text = "[+]";
            this.displaySamplesSymbolsCheckBox.UseVisualStyleBackColor = true;
            this.displaySamplesSymbolsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
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
            // indexLabel
            // 
            this.indexLabel.AutoSize = true;
            this.indexLabel.Location = new System.Drawing.Point(6, 17);
            this.indexLabel.Name = "indexLabel";
            this.indexLabel.Size = new System.Drawing.Size(33, 13);
            this.indexLabel.TabIndex = 5;
            this.indexLabel.Text = "Index";
            // 
            // displayDegradedSymbolsCheckBox
            // 
            this.displayDegradedSymbolsCheckBox.AutoSize = true;
            this.displayDegradedSymbolsCheckBox.Location = new System.Drawing.Point(124, 167);
            this.displayDegradedSymbolsCheckBox.Name = "displayDegradedSymbolsCheckBox";
            this.displayDegradedSymbolsCheckBox.Size = new System.Drawing.Size(38, 17);
            this.displayDegradedSymbolsCheckBox.TabIndex = 16;
            this.displayDegradedSymbolsCheckBox.Text = "[+]";
            this.displayDegradedSymbolsCheckBox.UseVisualStyleBackColor = true;
            this.displayDegradedSymbolsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // indexTextBox
            // 
            this.indexTextBox.AcceptsReturn = true;
            this.indexTextBox.Location = new System.Drawing.Point(76, 14);
            this.indexTextBox.Name = "indexTextBox";
            this.indexTextBox.Size = new System.Drawing.Size(87, 20);
            this.indexTextBox.TabIndex = 4;
            this.indexTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // countTextBox
            // 
            this.countTextBox.Location = new System.Drawing.Point(76, 40);
            this.countTextBox.Name = "countTextBox";
            this.countTextBox.Size = new System.Drawing.Size(87, 20);
            this.countTextBox.TabIndex = 3;
            this.countTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // displayCorrectedSymbolsCheckBox
            // 
            this.displayCorrectedSymbolsCheckBox.AutoSize = true;
            this.displayCorrectedSymbolsCheckBox.Location = new System.Drawing.Point(124, 121);
            this.displayCorrectedSymbolsCheckBox.Name = "displayCorrectedSymbolsCheckBox";
            this.displayCorrectedSymbolsCheckBox.Size = new System.Drawing.Size(38, 17);
            this.displayCorrectedSymbolsCheckBox.TabIndex = 13;
            this.displayCorrectedSymbolsCheckBox.Text = "[+]";
            this.displayCorrectedSymbolsCheckBox.UseVisualStyleBackColor = true;
            this.displayCorrectedSymbolsCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayCorrectedCheckBox
            // 
            this.displayCorrectedCheckBox.AutoSize = true;
            this.displayCorrectedCheckBox.Location = new System.Drawing.Point(6, 121);
            this.displayCorrectedCheckBox.Name = "displayCorrectedCheckBox";
            this.displayCorrectedCheckBox.Size = new System.Drawing.Size(72, 17);
            this.displayCorrectedCheckBox.TabIndex = 12;
            this.displayCorrectedCheckBox.Text = "Corrected";
            this.displayCorrectedCheckBox.UseVisualStyleBackColor = true;
            this.displayCorrectedCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displaySamplesCheckBox
            // 
            this.displaySamplesCheckBox.AutoSize = true;
            this.displaySamplesCheckBox.Location = new System.Drawing.Point(6, 144);
            this.displaySamplesCheckBox.Name = "displaySamplesCheckBox";
            this.displaySamplesCheckBox.Size = new System.Drawing.Size(66, 17);
            this.displaySamplesCheckBox.TabIndex = 15;
            this.displaySamplesCheckBox.Text = "Samples";
            this.displaySamplesCheckBox.UseVisualStyleBackColor = true;
            this.displaySamplesCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // displayDegradedCheckBox
            // 
            this.displayDegradedCheckBox.AutoSize = true;
            this.displayDegradedCheckBox.Location = new System.Drawing.Point(6, 167);
            this.displayDegradedCheckBox.Name = "displayDegradedCheckBox";
            this.displayDegradedCheckBox.Size = new System.Drawing.Size(73, 17);
            this.displayDegradedCheckBox.TabIndex = 14;
            this.displayDegradedCheckBox.Text = "Degraded";
            this.displayDegradedCheckBox.UseVisualStyleBackColor = true;
            this.displayDegradedCheckBox.CheckedChanged += new System.EventHandler(this.displayCheckBox_CheckedChanged);
            // 
            // CorrectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 492);
            this.Controls.Add(this.statisticsGroupBox);
            this.Controls.Add(this.correctionGroupBox);
            this.Controls.Add(this.displayGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CorrectionForm";
            this.Text = "Correction";
            this.Load += new System.EventHandler(this.CorrectionForm_Load);
            this.statisticsGroupBox.ResumeLayout(false);
            this.statisticsGroupBox.PerformLayout();
            this.correctionGroupBox.ResumeLayout(false);
            this.displayGroupBox.ResumeLayout(false);
            this.displayGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox statisticsGroupBox;
        private System.Windows.Forms.Label i_NumberLabel;
        private System.Windows.Forms.TextBox i_NumberTextBox;
        private System.Windows.Forms.Label i_LenghtLabel;
        private System.Windows.Forms.TextBox i_LengthTextBox;
        private System.Windows.Forms.Label deviationLabel;
        private System.Windows.Forms.TextBox deviationTextBox;
        private System.Windows.Forms.Label meanLabel;
        private System.Windows.Forms.TextBox meanTextBox;
        private System.Windows.Forms.GroupBox correctionGroupBox;
        private ZedGraph.ZedGraphControl correctionGraph;
        private System.Windows.Forms.GroupBox displayGroupBox;
        private System.Windows.Forms.CheckBox displayDegradationsSymbolsCheckBox;
        private System.Windows.Forms.CheckBox displayDegradationsCheckBox;
        private System.Windows.Forms.Button displayAllButton;
        private System.Windows.Forms.Button displayButton;
        private System.Windows.Forms.CheckBox displaySamplesSymbolsCheckBox;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Label indexLabel;
        private System.Windows.Forms.CheckBox displayDegradedSymbolsCheckBox;
        private System.Windows.Forms.TextBox indexTextBox;
        private System.Windows.Forms.TextBox countTextBox;
        private System.Windows.Forms.CheckBox displayCorrectedSymbolsCheckBox;
        private System.Windows.Forms.CheckBox displayCorrectedCheckBox;
        private System.Windows.Forms.CheckBox displaySamplesCheckBox;
        private System.Windows.Forms.CheckBox displayDegradedCheckBox;
        private System.Windows.Forms.CheckBox displayIdealCheckBox;
        private System.Windows.Forms.CheckBox displayReviewedCheckBox;
        private System.Windows.Forms.Label c_EffectivityLabel;
        private System.Windows.Forms.TextBox c_EffectivityTextBox;

    }
}