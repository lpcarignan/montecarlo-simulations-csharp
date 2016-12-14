using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilite.MonteCarlo.Core
{
    public class ForecastItems : Forecast
    {
        public ForecastItems(int numberOfItemsCompleted, Percentile percentile, int numberOfDays) : 
            base (numberOfItemsCompleted, percentile, numberOfDays)
        {

        }

        public override String ToString()
        {
            return String.Format("{0} items completed with {1} confidence in {2} days",
                new object[] {
                    NumberOfItemsCompleted, // {0}
                    Percentile,             // {1}
                    NumberOfDays });        // {2}

        }
    }
}
