using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Business.Calculations.UnitTests
{
    [TestClass]
    public class TyreDegradationCalculationsTest
    {
        private DegradationCalculator _calculator;
        private List<int> _trackPoints;
        private int _temperature;
        private TyreInformation _tyreInformation;
        [TestInitialize]
        public void Initialize()
        {
            _calculator = new DegradationCalculator();
            _trackPoints = new List<int> { 6, 7, 51, 75, 255};
            _temperature = 67;
        }
        
        [TestMethod]
        public void SuperSoftTyre_CalculationCorrect()
        {
            _tyreInformation = new TyreInformation
            {
                Type = TyreCompound.SuperSoft,
                DegradationCoefficient = 10
            };
            var result = _calculator.Calculate(_tyreInformation, _trackPoints, _temperature);
            
            result.Average.Should().BeApproximately(660, 1);
            result.Range.Should().BeApproximately(2086, 1);
        }
        
        [TestMethod]
        public void SoftTyre_CalculationCorrect()
        {
            _tyreInformation = new TyreInformation
            {
                Type = TyreCompound.Soft,
                DegradationCoefficient = 10
            };
            var result = _calculator.Calculate(_tyreInformation, _trackPoints, _temperature);
            
            result.Average.Should().BeApproximately(660, 1);
            result.Range.Should().BeApproximately(2086, 1);
        }
        
        [TestMethod]
        public void MediumTyre_CalculationCorrect()
        {
            _tyreInformation = new TyreInformation
            {
                Type = TyreCompound.Medium,
                DegradationCoefficient = 10
            };
            var result = _calculator.Calculate(_tyreInformation, _trackPoints, _temperature);
            
            result.Average.Should().BeApproximately(587, 1);
            result.Range.Should().BeApproximately(1854, 1);
        }
        
        [TestMethod]
        public void HardTyre_CalculationCorrect()
        {
            _tyreInformation = new TyreInformation
            {
                Type = TyreCompound.Hard,
                DegradationCoefficient = 10
            };
            var result = _calculator.Calculate(_tyreInformation, _trackPoints, _temperature);
            
            result.Average.Should().BeApproximately(704, 1);
            result.Range.Should().BeApproximately(2224, 1);
        }
    }
}