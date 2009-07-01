using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using MoGo.ChromosomeTypes;
using MoGo.NinjaTrader;
using MoGo.Optimisation;
using MoGo.UI;
using MoGo.World;

namespace NinjaTrader.Strategy
{
    /// <summary>
    /// </summary>
    [Gui.Design.DisplayName("- MoGo -")]
    public class MoGoNT : OptimizationMethod
    {
        private static readonly string ParametersFilename = Environment.GetEnvironmentVariable("TEMP") +
                                                            "/MoGoParameters.xml";

        private static OptimiserParameters __lastParameters;

        private GeneScore _bestResult;

        private int _iterationNumber;
        private ParametersForm _parametersForm;
        private ParameterWriter _parameterWriter;
        private DateTime _startDateTime;

        static MoGoNT()
        {
            __lastParameters = GetStartingParameters();
        }

        /// <summary>
        /// Runs the optimiser
        /// </summary>
        public override void Optimize()
        {
            var chromosomeTypes = new StrategyChromosomeTypeFactory(Strategy).GetChromosomeTypes();

            var optimiserParameters = GetParameters(chromosomeTypes);

            _startDateTime = DateTime.Now;
            _iterationNumber = 0;

            try
            {
                using (
                    _parameterWriter =
                    new ParameterWriter(Strategy.Instrument.FullName, optimiserParameters.ExportGenes))
                {
                    Strategy.Print(
                        string.Format(
                            "{0}:  Start time: {1}  Parameters: {2}  Population {3}  MaximumGenerations: {4}  Log results: {5}  Screen threshold: {6}",
                            Strategy.Instrument.FullName, _startDateTime, Strategy.Parameters.Count,
                            optimiserParameters.PopulationSize, optimiserParameters.MaximumGenerations,
                            optimiserParameters.ExportGenes, optimiserParameters.ScreeningThreshold));

                    _parameterWriter.WriteParameterNames(chromosomeTypes);

                    var optimiser = new Optimiser(new FitnessEvaluator(Strategy, chromosomeTypes));
                    optimiser.IterationComplete += HandleIterationComplete;
                    optimiser.GenerationComplete += HandleGenerationComplete;
                    optimiser.ReportNoProgress += HandleNoProgress;

                    var progressForm = new ProgressForm();
                    progressForm.Show(optimiser);

                    optimiser.Run(chromosomeTypes, optimiserParameters);

                    var endDateTime = DateTime.Now;

                    Strategy.Print(Strategy.Instrument.FullName + ": completed: " + endDateTime + ", total iterations: " +
                                   (_iterationNumber + 1) + ", total time (minutes): " +
                                   (endDateTime - _startDateTime).TotalMinutes.ToString("N1") + ", best result:");

                    if (_bestResult != null)
                    {
                        Strategy.Print(_bestResult.Gene.ToString(chromosomeTypes));
                    }
                }
            }
            finally
            {
                _parametersForm.Close();
            }
        }

        private void HandleNoProgress(object sender, CancelEventArgs e)
        {
            e.Cancel =
                MessageBox.Show(
                    string.Format(
                        "MoGo has been unable to find any further valid untested genes in {0:n0} attempts,\nso the odds are high that it has exhausted all possibilities.\n\nDo you wish to stop looking?",
                        Evolver.AttemptLimit), "MoGo - attempt threshold reached", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        private void HandleIterationComplete(object sender, IterationCompleteEventArgs args)
        {
            _iterationNumber = args.IterationNumber;

            args.Stop = UserAbort;
            _parameterWriter.WriteParamValues(args.IterationNumber, args.Score.Fitness, args.Score.Gene,
                args.Score.performance.AllTrades.TradesPerformance.Currency.CumProfit,
                args.Score.performance.AllTrades.TradesPerformance.Currency.DrawDown,
                args.Score.performance.AllTrades.TradesPerformance.SharpeRatio,
                args.Score.performance.AllTrades.TradesPerformance.TradesPerDay,
                args.Score.performance.AllTrades.TradesPerformance.TradesCount,
                args.Score.performance.AllTrades.TradesPerformance.Currency.AvgProfit);

            if (__lastParameters.ScreeningThreshold > 0 && args.Score.Fitness > __lastParameters.ScreeningThreshold)
            {
                Strategy.Print(Strategy.Instrument.FullName + ": Met threshold of " +
                               __lastParameters.ScreeningThreshold + " with fitness of " +
                               args.Score.Fitness.ToString("N2") + " on iteration " + (_iterationNumber + 1));

                args.Stop = true;
            }
        }


        private void HandleGenerationComplete(object sender, GenerationCompleteEventArgs args)
        {
            // Update best if necessary
            if (_bestResult == null || args.Scores[0] > _bestResult)
            {
                _bestResult = args.Scores[0];
            }

            var iterationsPerSecond = _iterationNumber / (DateTime.Now - _startDateTime).TotalSeconds;

            Strategy.Print(Strategy.Instrument.FullName + ": completed generation " + (args.GenerationNumber + 1) + "/" +
                           __lastParameters.MaximumGenerations + ", iterations per second: " +
                           iterationsPerSecond.ToString("N1") + ", max fitness this gen: " +
                           args.Scores[0].Fitness.ToString("N2"));

            _parameterWriter.Flush();
        }


        private OptimiserParameters GetParameters(IList<BaseChromosomeType> chromosomeTypes)
        {
            _parametersForm = new ParametersForm();
            _parametersForm.Initialise(__lastParameters, chromosomeTypes);

            OptimiserParameters parameters = null;

            _parametersForm.ParametersSet +=
                delegate(object sender, ParameterEventArgs args) { parameters = args.Parameters; };

            _parametersForm.ShowDialog();

            //while (parameters == null)
            //{
            //    Application.DoEvents();
            //}

            __lastParameters = parameters;

            SaveParameters(parameters);

            return parameters;
        }

        private static OptimiserParameters GetStartingParameters()
        {
            OptimiserParameters parameters = null;

            try
            {
                if (File.Exists(ParametersFilename))
                {
                    var parametersSerialiser = new XmlSerializer(typeof (OptimiserParameters));

                    using (var stream = File.OpenRead(ParametersFilename))
                    {
                        parameters = (OptimiserParameters) parametersSerialiser.Deserialize(stream);
                    }
                }
            }
            catch
            {
            }

            if (parameters == null)
            {
                parameters = new OptimiserParameters(5, 256, 0.1, 0.05, 0, false, null, string.Empty, 50, 1000, false, 1,
                                                     new List<string>());
            }

            return parameters;
        }

        private void SaveParameters(OptimiserParameters parameters)
        {
            try
            {
                var parametersSerialiser = new XmlSerializer(typeof (OptimiserParameters));

                using (var xmlTextWriter = new XmlTextWriter(ParametersFilename, Encoding.ASCII))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    parametersSerialiser.Serialize(xmlTextWriter, parameters);
                }
            }
            catch
            {
            }
        }
    }
}