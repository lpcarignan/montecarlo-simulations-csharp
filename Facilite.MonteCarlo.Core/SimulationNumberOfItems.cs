using System;
using System.Linq;
using Facilite.MonteCarlo.Core.IO;
using System.Collections.Generic;

namespace Facilite.MonteCarlo.Core
{
    public class SimulationNumberOfItems : Simulation
    {
        private int numberOfItems;

        private Func<SimulationResult, int, int, bool> filterDelegate;

        public SimulationNumberOfItems(
            int numberOfItems,
            int numberOfSimulations,
            ThroughputReader throughputReader,
            SimulationResultsWriter resultsWriter) : base(
                numberOfSimulations,
                throughputReader,
                resultsWriter)
        {
            this.numberOfItems = numberOfItems;
            filterDelegate = delegate (SimulationResult result, int numberOfItemsCompleted, int numberOfDaysToComplete)
            {
                return result.NumberOfDays == numberOfDaysToComplete;
            };
        }

        protected override Func<SimulationResult, int, int, bool> ResultSimulationFilter
        {
            get { return filterDelegate; }
        }

        public override void Execute()
        {
            
            int simulatedNumberOfItemsCompleted;
            int randomIndex;
            int daysToReachNumberOfItems;
            for (int i = 0; i < NumberOfSimulations; i++)
            {
                simulatedNumberOfItemsCompleted = 0;
                daysToReachNumberOfItems = 0;
                while (simulatedNumberOfItemsCompleted < this.numberOfItems)
                {
                    randomIndex = RandomIndexGenerator.Next(0, HistoricalThroughput.Length);
                    simulatedNumberOfItemsCompleted += HistoricalThroughput[randomIndex];
                    daysToReachNumberOfItems++;
                }

                AddResultSimulation(this.numberOfItems, daysToReachNumberOfItems);
            }
        }

        public override void CreateForecasts()
        {
            var orderedResults = SimulationResults.OrderBy(r => r.NumberOfDays);
            List<Percentile> percentilesOrdered = Percentiles.OrderBy(p => p.Value).ToList<Percentile>();
            List<int> percentileOccurences = TransformPercentilesToOccurences(percentilesOrdered);
            int counter = 0;

            foreach (SimulationResult result in orderedResults)
            {
                counter += result.Occurences;
                if (counter >= percentileOccurences[0])
                {
                    Forecasts.Add(new ForecastDate(this.numberOfItems, percentilesOrdered[0], result.NumberOfDays));

                    percentileOccurences.RemoveAt(0);
                    percentilesOrdered.RemoveAt(0);

                    if (percentileOccurences.Count == 0)
                        break;                                                                                        
                }                
            }
        }


    }
}
