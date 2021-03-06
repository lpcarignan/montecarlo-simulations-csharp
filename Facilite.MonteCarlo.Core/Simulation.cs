﻿using System;
using System.Collections.Generic;
using System.Linq;
using Facilite.MonteCarlo.Core.IO;

namespace Facilite.MonteCarlo.Core
{
    public abstract class Simulation
    {
        private const int INITIAL_NUMBER_OF_SIMULATIONS = 10000;
        private static Percentile[] INITIAL_PERCENTILES =
            {
                new Percentile(0.3),
                new Percentile(0.5),
                new Percentile(0.7),
                new Percentile(0.85),
                new Percentile(0.9)
            };

        private ThroughputReader throughputReader;
        private SimulationResultsWriter resultsWriter;

        public int NumberOfSimulations { get; protected set; }
        public int[] HistoricalThroughput { get; private set; }
        public List<Forecast> Forecasts { get; set; }

        protected Random RandomIndexGenerator { get; set; }
        protected List<SimulationResult> SimulationResults;

        protected List<Percentile> Percentiles;

        protected Simulation(
            int numberOfSimulations,
            ThroughputReader throughputReader,
            SimulationResultsWriter resultsWriter)
        {
            this.throughputReader = throughputReader;
            this.resultsWriter = resultsWriter;
            this.NumberOfSimulations = numberOfSimulations;
            SimulationResults = new List<SimulationResult>(NumberOfSimulations);
            Percentiles = new List<Percentile>(INITIAL_PERCENTILES);
            Forecasts = new List<Forecast>();
        }

        public void Initialize()
        {
            HistoricalThroughput = throughputReader.GetHistoricalThroughput();
            RandomIndexGenerator = new Random();
        }

        public abstract void Execute();

        public abstract void CreateForecasts();

        protected abstract Func<SimulationResult, int, int, bool> ResultSimulationFilter {get;}

        protected void AddResultSimulation(int numberOfItemsCompleted, int numberOfDays)
        {
            var list = SimulationResults.Where(r => ResultSimulationFilter(r, numberOfItemsCompleted, numberOfDays));
            if (list.Count() == 0)
            {
                SimulationResults.Add(new SimulationResult(numberOfItemsCompleted, numberOfDays));

            }
            else if (list.Count() == 1)
            {
                list.ElementAt(0).IncrementOccurence();
            }
            else
            {
                throw new Exception("Ayoye");
            }
        }

        public void PrintSimulationResults()
        {
            resultsWriter.WriteResults(Forecasts, SimulationResults);
        }

        protected List<int> TransformPercentilesToOccurences(List<Percentile> orderedPercentiles)
        {
            List<int> percentileOccurences = new List<int>();

            int index = 0;
            foreach (Percentile p in orderedPercentiles)
            {
                percentileOccurences.Add(Convert.ToInt32(p.Value * (double)NumberOfSimulations));
                index++;
            }

            return percentileOccurences;
        }
    }
}
