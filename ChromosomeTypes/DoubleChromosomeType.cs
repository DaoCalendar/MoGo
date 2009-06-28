using System;

namespace MoGo.ChromosomeTypes
{
    public class DoubleChromosomeType : BaseChromosomeType
    {
        protected readonly double _granularity;
        protected readonly double _maxValue;
        protected readonly double _minValue;

        public DoubleChromosomeType(string name, double minValue, double maxValue, double granularity) : base(name)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _granularity = granularity;
        }

        public override Type Type
        {
            get { return typeof (double); }
        }

        public override object GetRandomValue(Random random)
        {
            return GetRandomNumber(_minValue, _maxValue, _granularity, random);
        }

        public override object Mutate(object value, Random random)
        {
            // Deviate up to one third of the total range
            var range = ((_maxValue - _minValue) * 0.33);

            return GetRandomNumber(Math.Max(_minValue, (double) value - range),
                                   Math.Min(_maxValue, (double) value + range), _granularity, random);
        }
    }
}