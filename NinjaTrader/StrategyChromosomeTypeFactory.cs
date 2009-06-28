using System.Collections.Generic;
using MoGo.ChromosomeTypes;
using NinjaTrader.Strategy;

namespace MoGo.NinjaTrader
{
    public class StrategyChromosomeTypeFactory
    {
        private readonly ParameterCollection _parameters;

        public StrategyChromosomeTypeFactory(StrategyBase strategy)
        {
            _parameters = strategy.Parameters;
        }

        public IList<BaseChromosomeType> GetChromosomeTypes()
        {
            IList<BaseChromosomeType> chromosomes = new List<BaseChromosomeType>();

            foreach (Parameter parameter in _parameters)
            {
                if (parameter.ParameterType == typeof (double))
                {
                    chromosomes.Add(new DoubleChromosomeType(parameter.Name, parameter.Min, parameter.Max,
                                                             parameter.Increment));
                }
                else if (parameter.ParameterType == typeof (int))
                {
                    chromosomes.Add(new IntegerChromosomeType(parameter.Name, (int) parameter.Min, (int) parameter.Max,
                                                              (int) parameter.Increment));
                }
                //else if (parameter.ParameterType == typeof (bool))
                //{
                //    chromosomes.Add(new BooleanChromosomeType(parameter.Name));
                //}
                //else if (parameter.ParameterType.IsEnum)
                //{
                //    chromosomes.Add(new EnumChromosomeType(parameter.Name, parameter.ParameterType));
                //}
                //else
                //{
                //    throw new ArgumentException("Unknown parameter type: " + parameter.ParameterType.FullName);
                //}
            }

            return chromosomes;
        }
    }
}