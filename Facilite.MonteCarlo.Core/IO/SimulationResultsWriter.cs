using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilite.MonteCarlo.Core.IO
{
    public interface SimulationResultsWriter
    {
        void WriteResults(List<Forecast> forecasts, List<SimulationResult> results);
    }
}
