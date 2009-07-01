using System;
using System.Collections.Generic;
using System.IO;
using MoGo.ChromosomeTypes;
using MoGo.World;
using NinjaTrader.Cbi;
//using NinjaTrader.Strategy;

namespace MoGo.NinjaTrader
{
    public class ParameterWriter : IDisposable
    {
        private readonly StreamWriter _outputFile;

        public ParameterWriter(string name, bool loggingEnabled)
        {
            if (loggingEnabled)
            {
                _outputFile = File.AppendText(Core.UserDataDir + name + " " + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".csv");
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            if (_outputFile != null)
            {
                _outputFile.Flush();
                _outputFile.Close();
            }
        }

        #endregion


        public void WriteParameterNames(IList<BaseChromosomeType> chromosomeTypes)
        {
            Write("Iteration,");
            WriteLine("Fitness,");
            WriteLine("Net Profit,");
            WriteLine("Sharpe Ratio,");
            WriteLine("Total Trades,");
            WriteLine("Trades Per Day,");
            WriteLine("Avg. Profit,");
            WriteLine("Max Drawdown,");

            foreach (var chromosomeType in chromosomeTypes)
            {
                Write(chromosomeType.Name + ",");
            }

        }

        public void WriteParamValues(int iteration, double fitness, Gene gene, double cumProfit, double drawDown, double sharpeRatio, double tradesPerDay, int totalTrades, double avgProfit)
        {
            Write(iteration + ",");
            WriteLine(fitness.ToString("N3") + ",");
            WriteLine(cumProfit.ToString("C") + ",");
            WriteLine(sharpeRatio.ToString("N2") + ",");
            WriteLine(totalTrades.ToString() + ",");
            WriteLine(tradesPerDay.ToString("N2") + ",");
            WriteLine(avgProfit.ToString("C") + ",");
            WriteLine(drawDown.ToString("C") + ",");



            for (var index = 0; index < gene.Chromosomes.Count; index++)
            {
                Write(gene.Chromosomes[index] + ",");
            }

        }

        public void WriteLine(string s)
        {
            if (_outputFile != null)
            {
                _outputFile.WriteLine(s);
            }
        }

        public void Write(string s)
        {
            if (_outputFile != null)
            {
                _outputFile.Write(s);
            }
        }

        public void Flush()
        {
            if (_outputFile != null)
            {
                _outputFile.Flush();
            }
        }
    }
}