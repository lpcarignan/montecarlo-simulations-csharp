using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilite.MonteCarlo.Core
{
    public abstract class Forecast
    {
        public Percentile Percentile { get; }
        public int NumberOfItemsCompleted { get; }
        public int NumberOfDays { get; }

        protected Forecast(int numberOfItemsCompleted, Percentile percentile, int numberOfDays)
        {
            NumberOfItemsCompleted = numberOfItemsCompleted;
            Percentile = percentile;
            NumberOfDays = numberOfDays;
        }
    }
}
