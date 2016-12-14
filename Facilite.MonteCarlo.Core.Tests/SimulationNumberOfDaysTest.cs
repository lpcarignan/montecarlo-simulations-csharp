using System;
using Facilite.MonteCarlo.Core;
using Facilite.MonteCarlo.Core.IO;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Facilite.MonteCarlo.Core.Tests
{
    [TestClass]
    public class SimulationNumberOfDaysTest
    {
        private Fixture _fixture;

        private Simulation _simulation;
        private int[] _historicalThroughput;
        private Mock<ThroughputReader> _mockHistoricalThroughput;
        private Mock<SimulationResultsWriter> _mockResultsWriter;

        [TestInitialize()]
        public void Test_Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Initialize_Valid_ObjectsCalled()
        {
            // Arrange
            CreateMockObjects();
            CreateSimulationForNumberOfDays();

            // Act
            _simulation.Initialize();

            // Assert
            _mockHistoricalThroughput.Verify(t => t.GetHistoricalThroughput(), Times.Once);
            AssertEmptyForecast();            
        }

        [TestMethod]
        public void Execute_Valid_ResultsExpected()
        {
            // Arrange
            CreateMockObjects();
            CreateSimulationForNumberOfDays();

            _simulation.Initialize();

            // Act
            _simulation.Execute();

            // Assert
            _mockHistoricalThroughput.Verify(t => t.GetHistoricalThroughput(), Times.Once);
            AssertEmptyForecast();
            //simulation.CreateForecasts();
            //simulation.PrintSimulationResults();
        }

        [TestMethod]
        public void CreateForecasts_Valid_ResultsExpected()
        {
            // Arrange
            CreateMockObjects();
            CreateSimulationForNumberOfDays();

            _simulation.Initialize();
            _simulation.Execute();

            // Act
            _simulation.CreateForecasts();

            // Assert
            _mockHistoricalThroughput.Verify(t => t.GetHistoricalThroughput(), Times.Once);
            AssertNonEmptyForecast();
            //simulation.PrintSimulationResults();
        }

        private void CreateMockObjects()
        {
            _mockHistoricalThroughput = new Mock<ThroughputReader>();
            _mockResultsWriter = new Mock<SimulationResultsWriter>();

            _historicalThroughput = new int[] { 2, 7, 3, 9, 0, 3, 6, 8, 3 };

            _mockHistoricalThroughput.Setup<int[]>(t => t.GetHistoricalThroughput()).Returns(_historicalThroughput);
        }

        private void CreateSimulationForNumberOfDays()
        {
            _simulation = new SimulationNumberOfDays(
                _fixture.Create<int>(),
                _fixture.Create<int>(),
                _mockHistoricalThroughput.Object,
                _mockResultsWriter.Object);
        }

        private void AssertEmptyForecast()
        {
            Assert.IsNotNull(_simulation.Forecasts);
            Assert.AreEqual<int>(0, _simulation.Forecasts.Count);
        }

        private void AssertNonEmptyForecast()
        {
            Assert.IsNotNull(_simulation.Forecasts);
            Assert.IsTrue(_simulation.Forecasts.Count > 0);
        }
    }
}
