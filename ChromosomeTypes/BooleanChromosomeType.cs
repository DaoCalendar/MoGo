using System;

namespace MoGo.ChromosomeTypes
{
    public class BooleanChromosomeType : BaseChromosomeType
    {
        public BooleanChromosomeType(string name) : base(name)
        {
        }

        public override Type Type
        {
            get { return typeof (bool); }
        }

        public override object GetRandomValue(Random random)
        {
            return random.NextDouble() < 0.5;
        }

        public override object Mutate(object value, Random random)
        {
            return GetRandomValue(random);
        }
    }
}