using System;
using System.Collections.Generic;
using System.IO;
using MoGo.ChromosomeTypes;
using MoGo.World;
using NinjaTrader.Cbi;
using NinjaTrader.Strategy;

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

            foreach (var chromosomeType in chromosomeTypes)
            {
                Write(chromosomeType.Name + ",");
            }

            WriteLine("Fitness");
        }

        public void WriteParamValues(int iteration, double fitness, Gene gene)
        {
            Write(iteration + ",");

            for (var index = 0; index < gene.Chromosomes.Count; index++)
            {
                Write(gene.Chromosomes[index] + ",");
            }

            WriteLine(fitness.ToString("N3"));
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