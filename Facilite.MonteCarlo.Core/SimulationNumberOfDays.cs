using System;
using System.Collections.Generic;
using System.Linq;
using Facilite.MonteCarlo.Core.IO;

namespace Facilite.MonteCarlo.Core
{
    public class SimulationNumberOfDays : Simulation
    {
        private int numberOfDays;

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

                AddResultSimulation(simulatedNumberOfItemsCompleted);
            }
        }

        public override void CreateForecasts()
        {
            var orderedResults = SimulationResults.OrderByDescending(r => r.NumberOfItemsCompleted);
            var orderedAscPercentiles = Percentiles.OrderBy(p => p.Value);

            int counter = 0;
            int numberOfOccurences = 0;

            foreach(Percentile p in orderedAscPercentiles)
            {
                counter = 0;
                numberOfOccurences = (int)(p.Value * NumberOfSimulations);
                
                foreach (SimulationResult result in orderedResults)
                {
                    counter += result.Occurences;
                    if (counter >= numberOfOccurences)
                    { 
                        Forecasts.Add(new ForecastItems(result.NumberOfItemsCompleted, p, numberOfDays));
                        break;
                    }
                }
            }
        }

    }
}
