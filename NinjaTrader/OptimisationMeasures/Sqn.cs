using System;
using NinjaTrader.Gui.Design;
using NinjaTrader.Strategy;

namespace MoGo.NinjaTrader.OptimisationMeasures
{
    /// <summary>
    /// </summary>
    [DisplayName("System quality number")]
    public class Sqn : OptimizationType
    {
        /// <summary>
        /// Return the performance value of a backtesting result.
        /// </summary>
        /// <param name="systemPerformance"></param>
        /// <returns></returns>
        public override double GetPerformanceValue(SystemPerformance systemPerformance)
        {
            // This calc comes from NT standard net profit opt type
            var averageProfitPerTrade = (systemPerformance.AllTrades.TradesPerformance.GrossProfit +
                                         systemPerformance.AllTrades.TradesPerformance.GrossLoss) /
                                        systemPerformance.AllTrades.Count;

            double stddev = 0;

            // Now figure std dev of profit
            // Note: I forget my statistics & pulled this algorithm from the internet,
            foreach (Trade trade in systemPerformance.AllTrades)
            {
                var tradeProfit = (trade.ProfitPoints * trade.Quantity *
                                   trade.Entry.Instrument.MasterInstrument.PointValue);

                stddev += Math.Pow(tradeProfit - averageProfitPerTrade, 2);
            }

            stddev /= systemPerformance.AllTrades.Count;
            stddev = Math.Sqrt(stddev);

            return (Math.Sqrt(systemPerformance.AllTrades.Count) * averageProfitPerTrade) / stddev;
        }
    }
}