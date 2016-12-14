using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Facilite.MonteCarlo.Core.IO
{
    public class TextfileResultsWriter : SimulationResultsWriter
    {
        public void WriteResults(List<Forecast> forecasts, List<SimulationResult> results)
        {
            String desktopLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String fileLocation = Path.Combine(desktopLocation, "MonteCarloResultsOverview.txt");

            using (StreamWriter file = new System.IO.StreamWriter(fileLocation))
            {
                foreach (Forecast forecast in forecasts)
                {
                    file.WriteLine(forecast.ToString());
                }
            }


        }
    }
}
