using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MoGo.Optimisation
{
    [Serializable]
    public class OptimiserParameters
    {
        public OptimiserParameters()
        {
        }

        public OptimiserParameters(int generations, int populationSize, double reproductionRate, double mutationRate,
                                   double screeningThreshold, bool exportGenes, int? seed, string fitnessFunction,
                                   int minimumTrades, IEnumerable<string> parameterConditions)
        {
            Print("===============================================================================");
            Print("MoGo -- Open Source Genetic Optimizer");
            Print("===============================================================================");
            MaximumGenerations = generations;
            PopulationSize = populationSize;
            ReproductionRate = reproductionRate;
            MutationRate = mutationRate;
            ScreeningThreshold = screeningThreshold;
            ExportGenes = exportGenes;
            Seed = seed;

            MinimumTrades = minimumTrades;
            FitnessFunctionType = fitnessFunction;

            ParameterConditions = new List<string>(parameterConditions).ToArray();
        }

        public int MaximumGenerations { get; set; }

        public double MutationRate { get; set; }

        public int PopulationSize { get; set; }

        public double ReproductionRate { get; set; }

        public bool ExportGenes { get; set; }

        public double ScreeningThreshold { get; set; }

        public int? Seed { get; set; }

        public string FitnessFunctionType { get; set; }

        public int MinimumTrades { get; set; }

        [XmlArray]
        public string[] ParameterConditions { get; set; }
    }
}