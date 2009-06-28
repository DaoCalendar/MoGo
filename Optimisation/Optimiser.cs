using System;
using System.Collections.Generic;
using System.ComponentModel;
using MoGo.ChromosomeTypes;
using MoGo.World;

namespace MoGo.Optimisation
{
    public class Optimiser
    {
        #region Delegates

        public delegate void GenerationCompleteDelegate(object sender, GenerationCompleteEventArgs args);

        public delegate void IterationCompleteDelegate(object sender, IterationCompleteEventArgs args);

        #endregion

        private readonly IFitnessEvaluator _fitnessEvaluator;

        public Optimiser(IFitnessEvaluator fitnessEvaluator)
        {
            _fitnessEvaluator = fitnessEvaluator;
        }

        public event IterationCompleteDelegate IterationComplete;
        public event GenerationCompleteDelegate GenerationComplete;
        public event EventHandler Complete;

        public event CancelEventHandler ReportNoProgress;

        public IList<GeneScore> Run(IList<BaseChromosomeType> chromosomeTypes, OptimiserParameters parameters)
        {
            var allScores = new List<GeneScore>();

            var exiting = false;
            var iterationCount = 0;
            var totalIterations = parameters.MaximumGenerations * parameters.PopulationSize;

            var geneValidator = GetGeneValidator(chromosomeTypes, parameters.ParameterConditions);

            var evolver = new Evolver(chromosomeTypes, parameters.Seed,
                                      new CompositeGeneValidator(geneValidator, new OnceOnlyGeneValidator()));

            evolver.ReportNoProgress += ReportNoProgress;
            evolver.ReportNoProgress += delegate(object sender, CancelEventArgs args) { exiting |= args.Cancel; };

            _fitnessEvaluator.Initialise(parameters);

            IList<Gene> population = evolver.GetInitialPopulation(parameters.PopulationSize);

            for (var generation = 0; generation < parameters.MaximumGenerations; generation++)
            {
                var scoresThisGeneration = new List<GeneScore>();

                foreach (var gene in population)
                {
                    var score = _fitnessEvaluator.Evaluate(gene);
                    scoresThisGeneration.Add(score);

                    iterationCount++;

                    if (OnIterationComplete(score, iterationCount, totalIterations - iterationCount))
                    {
                        exiting = true;
                        break;
                    }
                }

                scoresThisGeneration.Sort();

                if (exiting)
                {
                    break;
                }

                population = evolver.GetNextGeneration(parameters.ReproductionRate, parameters.MutationRate,
                                                       scoresThisGeneration.ConvertAll(score => score.Gene));

                allScores.AddRange(scoresThisGeneration);

                OnGenerationComplete(generation, scoresThisGeneration);
            }

            allScores.Sort();

            OnComplete();

            return allScores;
        }

        private IGeneValidator GetGeneValidator(IList<BaseChromosomeType> chromosomeTypes,
                                                IEnumerable<string> parameterConditions)
        {
            var validatorFactory = new GeneValidatorFactory(chromosomeTypes);
            var validators = new List<IGeneValidator>();

            foreach (var condition in parameterConditions)
            {
                validators.Add(validatorFactory.GetGeneValidator(condition));
            }

            return new CompositeGeneValidator(validators.ToArray());
        }


        private bool OnIterationComplete(GeneScore score, int iterationCount, int iterationsRemaining)
        {
            if (IterationComplete != null)
            {
                var args = new IterationCompleteEventArgs(score, iterationCount, iterationsRemaining);
                IterationComplete(this, args);

                return args.Stop;
            }

            return false;
        }

        private void OnGenerationComplete(int generation, IList<GeneScore> scores)
        {
            if (GenerationComplete != null)
            {
                GenerationComplete(this, new GenerationCompleteEventArgs(scores, generation));
            }
        }

        private void OnComplete()
        {
            if (Complete != null)
            {
                Complete(this, EventArgs.Empty);
            }
        }
    }
}