using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilite.MonteCarlo.Core
{
    public class ForecastDate : Forecast
    {
        public ForecastDate(int numberOfItemsCompleted, Percentile percentile, int numberOfDays) : 
            base (numberOfItemsCompleted, percentile, numberOfDays)
        {

        }

        public override String ToString()
        {
            return String.Format("In {2} days, {1} confidence of completing {0} items",
                new object[] {
                    NumberOfItemsCompleted, // {0}
                    Percentile,             // {1}
                    NumberOfDays });        // {2}

        }
    }
}
