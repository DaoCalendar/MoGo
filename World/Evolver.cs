using System;
using System.Collections.Generic;
using System.ComponentModel;
using MoGo.ChromosomeTypes;

namespace MoGo.World
{
    /// <summary>
    /// </summary>
    public class Evolver
    {
        public const int AttemptLimit = 1000000;
        private readonly IList<BaseChromosomeType> _chromosomeTypes;

        private readonly IGeneValidator _geneValidator;
        private readonly Random _random;


        public Evolver(IList<BaseChromosomeType> chromosomes, int? seed, IGeneValidator geneValidator)
        {
            _chromosomeTypes = chromosomes;
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
            _geneValidator = geneValidator;
        }

        public event CancelEventHandler ReportNoProgress;

        public List<Gene> GetInitialPopulation(int populationSize)
        {
            var population = new List<Gene>();
            var attemptCounter = 0;

            while (population.Count < populationSize)
            {
                attemptCounter++;

                var gene = CreateNew();

                if (_geneValidator.Valid(gene))
                {
                    population.Add(gene);
                    attemptCounter = 0;
                }

                if (attemptCounter > AttemptLimit)
                {
                    if (OnReportNoProgress())
                    {
                        break;
                    }

                    attemptCounter = 0;
                }
            }

            return population;
        }

        public List<Gene> GetNextGeneration(double reproductionRate, double mutationRate,
                                            IEnumerable<Gene> sortedPopulation)
        {
            var newPopulation = new List<Gene>();
            var populationList = new List<Gene>(sortedPopulation);

            IList<Gene> parentCandidates = populationList.GetRange(0, (int) (populationList.Count * reproductionRate));
            var attemptCounter = 0;

            while (newPopulation.Count < populationList.Count && parentCandidates.Count > 0)
            {
                attemptCounter++;

                var parents = GetRandomParents(parentCandidates, 2);
                var gene = Reproduce(mutationRate, parents);

                if (_geneValidator.Valid(gene))
                {
                    newPopulation.Add(gene);
                    attemptCounter = 0;
                }

                if (attemptCounter > AttemptLimit)
                {
                    if (OnReportNoProgress())
                    {
                        break;
                    }

                    attemptCounter = 0;
                }
            }

            return newPopulation;
        }

        private Gene CreateNew()
        {
            var gene = new Gene();

            foreach (var chromosomeType in _chromosomeTypes)
            {
                gene.Chromosomes.Add(chromosomeType.GetRandomValue(_random));
            }

            return gene;
        }


        private Gene Reproduce(double mutationRate, params Gene[] parents)
        {
            var child = new Gene();

            for (var i = 0; i < _chromosomeTypes.Count; i++)
            {
                var chromosome = parents[_random.Next(parents.Length)].Chromosomes[i];

                if (_random.NextDouble() < mutationRate)
                {
                    chromosome = _chromosomeTypes[i].Mutate(chromosome, _random);
                }

                child.Chromosomes.Add(chromosome);
            }

            return child;
        }

        private Gene[] GetRandomParents(IEnumerable<Gene> parentCandidates, int parentsRequired)
        {
            IList<Gene> candidateList = new List<Gene>(parentCandidates);
            var parents = new List<Gene>();

            while (parents.Count < parentsRequired && candidateList.Count > 0)
            {
                var parentIndex = _random.Next(candidateList.Count);
                parents.Add(candidateList[parentIndex]);

                candidateList.RemoveAt(parentIndex);
            }

            return parents.ToArray();
        }

        private bool OnReportNoProgress()
        {
            var args = new CancelEventArgs();

            if (ReportNoProgress != null)
            {
                ReportNoProgress(this, args);
            }

            return args.Cancel;
        }
    }
}