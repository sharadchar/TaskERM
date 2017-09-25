using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using BusinessLogic.Model;
using System.Collections.Generic;

namespace ERMPower_Test
{
    [TestClass]
    public class SummaryTest
    {
        private Summary summary;

        [TestInitialize]
        public void Init()
        {
            summary = new Summary();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WhenTheGivenPercentageIsNegativeMustThrowError()
        {
            //
            // Arrange
            //
            Summary summary = new Summary();
            var lpData = new List<LP_data>
            {
                new LP_data {DataValue = 10},
                new LP_data {DataValue = 20},
                new LP_data {DataValue = 30},
            };
            //
            // Act
            //
            summary.CalculateFileSummary(-1, lpData.AsReadOnly(), data => data.DataValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WhenTheCollectionIsNullMustThrowError()
        {
            //
            // Act
            //
            summary.CalculateFileSummary<LP_data>(-1, null, data => data.DataValue);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void WhenTheCollectionIsEmptyMustThrowError()
        {
            //
            // Arrange
            //
            var lpData = new List<LP_data>();
            //
            // Act
            //
            summary.CalculateFileSummary(10, lpData.AsReadOnly(), data => data.DataValue);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void WhenThePropertyExpressionIsNullMustThrowError()
        {
            //
            // Arrange
            //
            var lpData = new List<LP_data>
            {
                new LP_data {DataValue = 10},
                new LP_data {DataValue = 20},
                new LP_data {DataValue = 30},
            };
            //
            // Act
            //
            summary.CalculateFileSummary(10, lpData.AsReadOnly(), null);
        }


        [TestMethod]
        public void WhenDataIsProperMustReturnTheMedianSummary()
        {
            //
            // Arrange
            //
            var lpData = new List<LP_data>
            {
                new LP_data {DataValue = 60},
                new LP_data {DataValue = 50},
                new LP_data {DataValue = 40},
                new LP_data {DataValue = 30},
                new LP_data {DataValue = 20},
                new LP_data {DataValue = 10},
            };
            //
            // Act
            //
            var medianSummary = summary.CalculateFileSummary(10, lpData.AsReadOnly(), data => data.DataValue);
            //
            // Assert
            //
            Assert.AreEqual(35, medianSummary.Median);
        }
    }
}
