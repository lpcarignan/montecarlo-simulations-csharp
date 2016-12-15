using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilite.MonteCarlo.Core
{
    public class SimulationResult
    {
        public int NumberOfItemsCompleted { get; }
        public int NumberOfDays { get; }
        public int Occurences { get; private set; }

        public SimulationResult(int numberOfItemsCompleted, int numberOfDays)
        {
            this.NumberOfItemsCompleted = numberOfItemsCompleted;
            this.NumberOfDays = numberOfDays;
            this.Occurences = 1;
        }

        public void IncrementOccurence()
        {
            Occurences++;
        }

        public override string ToString()
        {
            return String.Format("{0} items delivered in {1} days [{2} occurences]", 
                NumberOfItemsCompleted,
                NumberOfDays,
                Occurences);
        }
    }
}
