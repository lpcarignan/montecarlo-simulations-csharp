using System;
using Facilite.MonteCarlo.Core;
using Ploeh.AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facilite.MonteCarlo.Core.Tests
{
    [TestClass]
    public class PercentileTest
    {
        private Fixture _fixture;
        private Percentile _percentile;

        [TestInitialize]
        public void Test_Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void ToString_ExactString_Success()
        {
            // Arrange
            _percentile = new Percentile(0.5);

            // Act
            String literal = _percentile.ToString();

            // Assert
            Assert.AreEqual<String>("50 %", literal);
        }

        [TestMethod]
        public void Value_PropertyMatchesValueInConstructor_Success()
        {
            // Arrange
            double expectedValue = 0.5;
            _percentile = new Percentile(expectedValue);

            // Act
            String literal = _percentile.ToString();

            // Assert
            Assert.AreEqual<double>(expectedValue, _percentile.Value);
        }

        [TestMethod]
        public void ToString_PercentageSymbol_Success()
        {
            // Arrange
            _percentile = _fixture.Create<Percentile>();

            // Act
            String literal = _percentile.ToString();

            // Assert
            Assert.IsTrue(literal.Contains("%"));
        }


    }
}
