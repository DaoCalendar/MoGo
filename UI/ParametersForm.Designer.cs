using System.ComponentModel;
using System.Windows.Forms;

namespace MoGo.UI
{
    public partial class ParametersForm
    {
        /*		CODE FROM DESIGNER: SECTION 1, ParametersForm
         */

        private Button btnOk;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private LinkLabel linkLabel1;
        private NumericUpDown generationsSpin;
        private CheckBox saveLogCheckbox;

        /* CODE FROM DESIGNER: SECTION 2
         *
         */

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.screenThresholdSpin = new System.Windows.Forms.NumericUpDown();
            this.mutationRateSpin = new System.Windows.Forms.NumericUpDown();
            this.reproductionPercentSpin = new System.Windows.Forms.NumericUpDown();
            this.populationSizeSpin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.generationsSpin = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.saveLogCheckbox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.strategyParameterConditionsGrid = new System.Windows.Forms.DataGridView();
            this.Condition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minimumTradesSpin = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fitnessFunctionComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screenThresholdSpin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutationRateSpin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reproductionPercentSpin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationSizeSpin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.generationsSpin)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strategyParameterConditionsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumTradesSpin)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.screenThresholdSpin);
            this.groupBox1.Controls.Add(this.mutationRateSpin);
            this.groupBox1.Controls.Add(this.reproductionPercentSpin);
            this.groupBox1.Controls.Add(this.populationSizeSpin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.generationsSpin);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.saveLogCheckbox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(18, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Genetic optimiser";
            // 
            // screenThresholdSpin
            // 
            this.screenThresholdSpin.DecimalPlaces = 2;
            this.screenThresholdSpin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.screenThresholdSpin.Location = new System.Drawing.Point(181, 122);
            this.screenThresholdSpin.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.screenThresholdSpin.Name = "screenThresholdSpin";
            this.screenThresholdSpin.Size = new System.Drawing.Size(60, 20);
            this.screenThresholdSpin.TabIndex = 10;
            this.screenThresholdSpin.ThousandsSeparator = true;
            // 
            // mutationRateSpin
            // 
            this.mutationRateSpin.Location = new System.Drawing.Point(181, 96);
            this.mutationRateSpin.Name = "mutationRateSpin";
            this.mutationRateSpin.Size = new System.Drawing.Size(60, 20);
            this.mutationRateSpin.TabIndex = 10;
            this.mutationRateSpin.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // reproductionPercentSpin
            // 
            this.reproductionPercentSpin.Location = new System.Drawing.Point(181, 70);
            this.reproductionPercentSpin.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.reproductionPercentSpin.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.reproductionPercentSpin.Name = "reproductionPercentSpin";
            this.reproductionPercentSpin.Size = new System.Drawing.Size(60, 20);
            this.reproductionPercentSpin.TabIndex = 10;
            this.reproductionPercentSpin.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // populationSizeSpin
            // 
            this.populationSizeSpin.Location = new System.Drawing.Point(181, 45);
            this.populationSizeSpin.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.populationSizeSpin.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.populationSizeSpin.Name = "populationSizeSpin";
            this.populationSizeSpin.Size = new System.Drawing.Size(60, 20);
            this.populationSizeSpin.TabIndex = 10;
            this.populationSizeSpin.ThousandsSeparator = true;
            this.populationSizeSpin.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(1, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(172, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Stop threshold (0 = none)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // generationsSpin
            // 
            this.generationsSpin.Location = new System.Drawing.Point(181, 21);
            this.generationsSpin.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.generationsSpin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.generationsSpin.Name = "generationsSpin";
            this.generationsSpin.Size = new System.Drawing.Size(40, 20);
            this.generationsSpin.TabIndex = 1;
            this.generationsSpin.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Mutation rate (%)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // saveLogCheckbox
            // 
            this.saveLogCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveLogCheckbox.Location = new System.Drawing.Point(98, 153);
            this.saveLogCheckbox.Name = "saveLogCheckbox";
            this.saveLogCheckbox.Size = new System.Drawing.Size(97, 17);
            this.saveLogCheckbox.TabIndex = 6;
            this.saveLogCheckbox.Text = "Save all trials";
            this.saveLogCheckbox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.saveLogCheckbox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Reproduction rate (%)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Population size";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Maximum generations";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.errorLabel);
            this.groupBox2.Controls.Add(this.strategyParameterConditionsGrid);
            this.groupBox2.Location = new System.Drawing.Point(290, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(345, 292);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Strategy parameter constraints";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(39, 270);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(266, 13);
            this.errorLabel.TabIndex = 1;
            this.errorLabel.Text = "There are one or more errors in the selected expression";
            this.errorLabel.Visible = false;
            // 
            // strategyParameterConditionsGrid
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.strategyParameterConditionsGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.strategyParameterConditionsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.strategyParameterConditionsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.strategyParameterConditionsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.strategyParameterConditionsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Condition});
            this.strategyParameterConditionsGrid.Location = new System.Drawing.Point(19, 22);
            this.strategyParameterConditionsGrid.Name = "strategyParameterConditionsGrid";
            this.strategyParameterConditionsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.strategyParameterConditionsGrid.RowHeadersWidth = 30;
            this.strategyParameterConditionsGrid.Size = new System.Drawing.Size(307, 239);
            this.strategyParameterConditionsGrid.TabIndex = 0;
            // 
            // Condition
            // 
            this.Condition.HeaderText = "Expression";
            this.Condition.Name = "Condition";
            // 
            // minimumTradesSpin
            // 
            this.minimumTradesSpin.Location = new System.Drawing.Point(181, 65);
            this.minimumTradesSpin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.minimumTradesSpin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minimumTradesSpin.Name = "minimumTradesSpin";
            this.minimumTradesSpin.Size = new System.Drawing.Size(60, 20);
            this.minimumTradesSpin.TabIndex = 10;
            this.minimumTradesSpin.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Minimum Trades";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.Location = new System.Drawing.Point(537, 322);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(89, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "MoGo homepage";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(290, 319);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.minimumTradesSpin);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.fitnessFunctionComboBox);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(18, 206);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(257, 102);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fitness function";
            // 
            // fitnessFunctionComboBox
            // 
            this.fitnessFunctionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fitnessFunctionComboBox.FormattingEnabled = true;
            this.fitnessFunctionComboBox.Location = new System.Drawing.Point(17, 38);
            this.fitnessFunctionComboBox.Name = "fitnessFunctionComboBox";
            this.fitnessFunctionComboBox.Size = new System.Drawing.Size(224, 21);
            this.fitnessFunctionComboBox.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(14, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Fitness function";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 352);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ParametersForm";
            this.ShowInTaskbar = false;
            this.Text = "- MoGo - 1.1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ParametersForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.screenThresholdSpin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutationRateSpin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reproductionPercentSpin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationSizeSpin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.generationsSpin)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strategyParameterConditionsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumTradesSpin)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown populationSizeSpin;
        private NumericUpDown screenThresholdSpin;
        private NumericUpDown reproductionPercentSpin;
        private NumericUpDown minimumTradesSpin;
        private NumericUpDown mutationRateSpin;
        private Label label6;
        private GroupBox groupBox3;
        private ComboBox fitnessFunctionComboBox;
        private Label label11;
        private DataGridView strategyParameterConditionsGrid;
        private Label errorLabel;
        private DataGridViewTextBoxColumn Condition;
    }
}