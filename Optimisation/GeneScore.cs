using System;
using MoGo.World;

namespace MoGo.Optimisation
{
    public class GeneScore : IComparable<GeneScore>
    {
        private readonly double _fitness;
        private readonly Gene _gene;

        public GeneScore(Gene gene, double fitness)
        {
            _gene = gene;
            _fitness = fitness;
        }

        public Gene Gene
        {
            get { return _gene; }
        }

        public double Fitness
        {
            get { return _fitness; }
        }


        #region IComparable<GeneScore> Members

        public int CompareTo(GeneScore other)
        {
            return other.Fitness.CompareTo(Fitness);
        }

        #endregion


        public override string ToString()
        {
            return string.Format("Fitness: {0} Gene: {1}", Fitness, Gene);
        }

        public static bool operator >(GeneScore score1, GeneScore score2)
        {
            return score1.Fitness > score2.Fitness;
        }

        public static bool operator <(GeneScore score1, GeneScore score2)
        {
            return score1.Fitness < score2.Fitness;
        }
    }
}