using System;

namespace MoGo.ChromosomeTypes
{
    public abstract class BaseChromosomeType
    {
        private readonly string _name;

        protected BaseChromosomeType(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public abstract Type Type { get; }

        public abstract object GetRandomValue(Random random);

        public abstract object Mutate(object value, Random random);


        protected double GetRandomNumber(double min, double max, double granularity, Random random)
        {
            return Math.Round(((max - min) * random.NextDouble() + min) / granularity) * granularity;
        }
    }
}