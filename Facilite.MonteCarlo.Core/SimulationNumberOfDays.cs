using System;
using System.Collections.Generic;
using System.Linq;
using Facilite.MonteCarlo.Core.IO;

namespace Facilite.MonteCarlo.Core
{
    public class SimulationNumberOfDays : Simulation
    {
        private int numberOfDays;

        private Func<SimulationResult, int, int, bool> filterDelegate;

        public SimulationNumberOfDays(
            int numberOfDays,
            int numberOfSimulations,
            ThroughputReader throughputReader,
            SimulationResultsWriter resultsWriter) : base(
                numberOfSimulations,
                throughputReader,
                resultsWriter)
        {
            this.numberOfDays = numberOfDays;
            this.filterDelegate = delegate (SimulationResult result, int numberOfItemsCompleted, int numberOfDaysToCompleted)
            {
                return result.NumberOfItemsCompleted == numberOfItemsCompleted;
            };
        }

        protected override Func<SimulationResult, int, int, bool> ResultSimulationFilter
        {
            get { return filterDelegate; }
        }

        public override void Execute()
        {                                    
            // Variables used in the inner for
            int[] throughputResults;
            int randomIndex;

            int simulatedNumberOfItemsCompleted;
            for (int i = 0; i < NumberOfSimulations; i++)
            {
                throughputResults = new int[numberOfDays];
                for (int j = 0; j < numberOfDays; j++)
                {
                    randomIndex = RandomIndexGenerator.Next(0, HistoricalThroughput.Length);
                    throughputResults[j] = HistoricalThroughput[randomIndex];
                }

                simulatedNumberOfItemsCompleted = throughputResults.Sum();

                AddResultSimulation(simulatedNumberOfItemsCompleted, this.numberOfDays);
            }
        }

        public override void CreateForecasts()
        {
            var orderedDescResults = SimulationResults.OrderByDescending(r => r.NumberOfItemsCompleted);
            List<Percentile> percentilesOrdered = Percentiles.OrderBy(p => p.Value).ToList<Percentile>();
            List<int> percentilesOccurences = TransformPercentilesToOccurences(percentilesOrdered);
            int counter = 0;
            
            foreach (SimulationResult result in orderedDescResults)
            {
                counter += result.Occurences;
                if (counter >= percentilesOccurences[0])
                {
                    Forecasts.Add(new ForecastItems(result.NumberOfItemsCompleted, percentilesOrdered[0], this.numberOfDays));

                    percentilesOccurences.RemoveAt(0);
                    percentilesOrdered.RemoveAt(0);

                    if (percentilesOccurences.Count == 0)
                        break;
                }
            }
        }
    }
}
