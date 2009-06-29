using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MoGo.ChromosomeTypes;
using MoGo.Optimisation;
using MoGo.World;
using NinjaTrader.Gui.Design;
using NinjaTrader.Strategy;

namespace MoGo.UI
{
    public partial class ParametersForm : Form
    {
        private static bool __supressDisplay;

        private GeneValidatorFactory _validatorFactory;

        public ParametersForm()
        {
            InitializeComponent();

            strategyParameterConditionsGrid.RowValidating += HandleGridRowValidating;
        }

        public event EventHandler<ParameterEventArgs> ParametersSet;

        public void Initialise(OptimiserParameters parameters, IList<BaseChromosomeType> chromosomeTypes)
        {
            _validatorFactory = new GeneValidatorFactory(chromosomeTypes);

            generationsSpin.Value = parameters.MaximumGenerations;
            populationSizeSpin.Value = parameters.PopulationSize;
            reproductionPercentSpin.Value = (decimal) (parameters.ReproductionRate * 100);
            mutationRateSpin.Value = (decimal) (parameters.MutationRate * 100);

            screenThresholdSpin.Value = (decimal) parameters.ScreeningThreshold;
            saveLogCheckbox.Checked = parameters.ExportGenes;

            var wrappers = GetOptimisationTypeWrappers();

            fitnessFunctionComboBox.DisplayMember = "Name";
            fitnessFunctionComboBox.ValueMember = "Type";
            fitnessFunctionComboBox.DataSource = wrappers;

            try
            {
                foreach (var wrapper in wrappers)
                {
                    if (wrapper.Type.AssemblyQualifiedName == parameters.FitnessFunctionType)
                    {
                        fitnessFunctionComboBox.SelectedItem = wrapper;
                        break;
                    }
                }
            }
            catch
            {
            }

            minimumTradesSpin.Value = parameters.MinimumTrades;

            strategyParameterConditionsGrid.Rows.Clear();

            foreach (var condition in parameters.ParameterConditions)
            {
                if (IsConditionValid(condition))
                {
                    var row = strategyParameterConditionsGrid.Rows[strategyParameterConditionsGrid.Rows.Add()];
                    row.SetValues(condition);
                }
            }
        }

        private IList<OptimisationTypeWrapper> GetOptimisationTypeWrappers()
        {
            var optimisationTypes = FitnessFunctionWrapper.GetAvailableOptimisationTypes();
            var wrappers = new List<Type>(optimisationTypes).ConvertAll(type => new OptimisationTypeWrapper(type));

            // ComboBox sorting has a bug, so sort here instead
            wrappers.Sort((w1, w2) => w1.Name.CompareTo(w2.Name));

            return wrappers;
        }

        private void HandleGridRowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var row = strategyParameterConditionsGrid.Rows[e.RowIndex];
            var conditionValue = row.Cells[e.ColumnIndex].Value;

            string errorText = null;
            var foreColor = Color.Black;

            if (conditionValue != null)
            {
                var conditionString = conditionValue.ToString().Trim();

                if (!IsConditionValid(conditionString))
                {
                    e.Cancel = true;
                    errorText = "Error parsing condition";
                    foreColor = Color.Red;
                }
            }

            row.ErrorText = errorText;
            row.DefaultCellStyle.ForeColor = foreColor;
            errorLabel.Visible = errorText != null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            if (ModifierKeys == Keys.Control)
            {
                __supressDisplay = true;
            }

            OnParametersSet();
        }

        private bool IsConditionValid(string conditionString)
        {
            return string.IsNullOrEmpty(conditionString) ||
                   _validatorFactory.GetGeneValidator(conditionString.Trim()) != null;
        }

        private OptimiserParameters GetParameters()
        {
            IList<string> conditions = new List<string>();

            foreach (DataGridViewRow row in strategyParameterConditionsGrid.Rows)
            {
                var value = row.Cells[0].Value;

                if (value != null)
                {
                    var condition = value.ToString().Trim();

                    if (condition.Length > 0)
                    {
                        conditions.Add(condition);
                    }
                }
            }

            return new OptimiserParameters((int) generationsSpin.Value, (int) populationSizeSpin.Value,
                                           (double) reproductionPercentSpin.Value / 100,
                                           (double) mutationRateSpin.Value / 100, (double) screenThresholdSpin.Value,
                                           saveLogCheckbox.Checked, null,
                                           ((Type) fitnessFunctionComboBox.SelectedValue).AssemblyQualifiedName,
                                           (int) minimumTradesSpin.Value,
                                           (int) maximumTradesSpin.Value, conditions);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://github.com/zkwentz/MoGo/tree/master");
        }

        private void ParametersForm_Load(object sender, EventArgs e)
        {
            if (__supressDisplay && ModifierKeys != Keys.Control)
            {
                OnParametersSet();

                Close();
            }
            else
            {
                __supressDisplay = false;
            }
        }

        private void OnParametersSet()
        {
            if (ParametersSet != null)
            {
                ParametersSet(this, new ParameterEventArgs(GetParameters()));
            }
        }
    }

    internal class OptimisationTypeWrapper
    {
        private readonly string _name;
        private readonly Type _type;

        public OptimisationTypeWrapper(Type type)
        {
            _type = type;

            foreach (DisplayNameAttribute displayNameAttribute in
                _type.GetCustomAttributes(typeof (DisplayNameAttribute), true))
            {
                _name = displayNameAttribute.Name;
            }
        }

        public Type Type
        {
            get { return _type; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}