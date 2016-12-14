using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilite.MonteCarlo.Core
{
    public class Percentile
    {
        public double Value { get; }
        public String PercentileString { get; private set; }

        public Percentile(double percentile)
        {
            Value = percentile;
        }

        public override String ToString()
        {
            return String.Format("{0:P0}", new object[] { Value });
        }
    }
}
