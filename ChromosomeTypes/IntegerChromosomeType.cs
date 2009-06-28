using System;

namespace MoGo.ChromosomeTypes
{
    public class IntegerChromosomeType : DoubleChromosomeType
    {
        public IntegerChromosomeType(string name, int minValue, int maxValue, int granularity)
            : base(name, minValue, maxValue, granularity)
        {
        }

        public override Type Type
        {
            get { return typeof (int); }
        }

        public override object GetRandomValue(Random random)
        {
            return (int) Math.Round((double) base.GetRandomValue(random));
        }

        public override object Mutate(object value, Random random)
        {
            // Deviate maximum of 1.7, and one third of the total range
            var range = Math.Max((_maxValue - _minValue) * 0.33334, 1.7);

            return
                (int)
                GetRandomNumber(Math.Max(_minValue, (int) value - range), Math.Min(_maxValue, (int) value + range),
                                _granularity, random);
        }
    }
}