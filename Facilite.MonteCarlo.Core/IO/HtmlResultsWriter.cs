using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilite.MonteCarlo.Core.IO
{
    public class HtmlResultsWriter : SimulationResultsWriter
    {
        private static String HTML_HEADER =
            "<html>" + Environment.NewLine +
            "<head>" + Environment.NewLine +
            "\t<link rel=\"stylesheet\" type=\"text/css\" href=\"styles.css\" media=\"screen\">" + Environment.NewLine +
            "</head>" + Environment.NewLine +
            "<body>" + Environment.NewLine +
            "<dl>";

        private static String HTML_CHART_TITLE = "<dt>{0}</dt>";
        private static String HTML_BAR =
            "\t<dd class=\"percentage percentage-{0}\">" + Environment.NewLine +
            "\t\t<span class=\"text\">" + Environment.NewLine +
            "\t\t  {1} items completed: {0} %" + Environment.NewLine +
            "\t\t</span>" + Environment.NewLine +
            "\t</dd>";

        private static String HTML_FOOTER =
            "</dl>" + Environment.NewLine +
            "</body>" + Environment.NewLine +
            "</html>";

        public void WriteResults(List<Forecast> forecasts, List<SimulationResult> results)
        {
            String desktopLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String fileLocation = Path.Combine(desktopLocation, "MonteCarloResultsOverview.html");

            using (StreamWriter file = new System.IO.StreamWriter(fileLocation))
            {
                file.WriteLine(HTML_HEADER);
                file.WriteLine(String.Format(HTML_CHART_TITLE, "Forecasts for 30 days"));

                foreach (Forecast forecast in forecasts)
                {
                    file.WriteLine(String.Format(HTML_BAR, 100 * forecast.Percentile.Value, forecast.NumberOfItemsCompleted));
                }

                file.WriteLine(String.Format(HTML_FOOTER));
            }
        }
    }
}
