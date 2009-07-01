using System;
using System.Collections.Generic;
using MoGo.ChromosomeTypes;
using MoGo.Optimisation;
using MoGo.World;
using NinjaTrader.Strategy;

namespace MoGo.NinjaTrader
{
    public class FitnessEvaluator : IFitnessEvaluator
    {
        private readonly StrategyBase _strategy;
        private IList<int> _strategyParameterIndices;

        public FitnessEvaluator(StrategyBase strategy, IList<BaseChromosomeType> chromosomeTypes)
        {
            _strategy = strategy;

            InitialiseStrategyParameterIndices(chromosomeTypes);
        }


        #region IFitnessEvaluator Members

        public void Initialise(OptimiserParameters parameters)
        {
            FitnessFunctionWrapper.Initialise(parameters, _strategy);
        }

        public GeneScore Evaluate(Gene gene)
        {
            for (var i = 0; i < gene.Chromosomes.Count; i++)
            {
                // NT needs everything to be doubles
                _strategy.Parameters[_strategyParameterIndices[i]].Value = Convert.ToDouble(gene.Chromosomes[i]);
            }

            _strategy.RunIteration();

            return new GeneScore(gene, FitnessFunctionWrapper.LastPerformanceValue, _strategy.Performance);
        }

        #endregion


        private void InitialiseStrategyParameterIndices(IList<BaseChromosomeType> chromosomeTypes)
        {
            _strategyParameterIndices = new int[chromosomeTypes.Count];

            for (var i = 0; i < chromosomeTypes.Count; i++)
            {
                for (var j = 0; j < _strategy.Parameters.Count; j++)
                {
                    if (chromosomeTypes[i].Name == _strategy.Parameters[j].Name)
                    {
                        _strategyParameterIndices[i] = j;
                    }
                }
            }
        }
    }
}