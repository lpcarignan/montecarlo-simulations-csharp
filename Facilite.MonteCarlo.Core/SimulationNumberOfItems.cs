using System;
using Facilite.MonteCarlo.Core.IO;

namespace Facilite.MonteCarlo.Core
{
    public class SimulationNumberOfItems : Simulation
    {
        private int numberOfItems;

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
        }

        public override void CreateForecasts()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
