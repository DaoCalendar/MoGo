using System;
using System.Collections.Generic;
using MoGo.Optimisation;
using NinjaTrader.Gui.Design;
using Wintellect.PowerCollections;

namespace NinjaTrader.Strategy
{
    /// <summary>
    /// System Quality Number
    /// </summary>
    [DisplayName("- MoGo -")]
    public class FitnessFunctionWrapper : OptimizationType
    {
        private static double _lastPerformanceValue;
        private static double _minimumTrades;
        private static OptimizationType _optimisationType;

        public static double LastPerformanceValue
        {
            get { return _lastPerformanceValue; }
        }

        public static IList<Type> GetAvailableOptimisationTypes()
        {
            IDictionary<string, Type> typesByName = new Dictionary<string, Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof (OptimizationType)) && type != typeof (FitnessFunctionWrapper))
                    {
                        typesByName[type.FullName] = type;
                    }
                }
            }

            return new List<Type>(typesByName.Values);
        }

        public static void Initialise(OptimiserParameters optimiserParameters, StrategyBase strategy)
        {
            _minimumTrades = optimiserParameters.MinimumTrades;

            _optimisationType =
                (OptimizationType) Activator.CreateInstance(Type.GetType(optimiserParameters.FitnessFunctionType));
            _optimisationType.Strategy = strategy;
        }


        /// <summary>
        /// Return the performance value of a backtesting result.
        /// </summary>
        /// <param name="systemPerformance"></param>
        /// <returns></returns>
        public override double GetPerformanceValue(SystemPerformance systemPerformance)
        {
            var performanceValue = double.MinValue;

            if (systemPerformance.AllTrades.Count >= _minimumTrades)
            {
                performanceValue = _optimisationType.GetPerformanceValue(systemPerformance);
            }

            _lastPerformanceValue = performanceValue;

            return performanceValue;
        }
    }
}