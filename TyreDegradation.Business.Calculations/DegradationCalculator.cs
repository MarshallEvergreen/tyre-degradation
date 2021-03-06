using System;
using System.Collections.Generic;
using System.Linq;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Business.Calculations
{
    public class DegradationCalculator
    {
        private readonly Dictionary<TyreCompound, double> _degradationCoefficients;

        public DegradationCalculator()
        {
            _degradationCoefficients = new Dictionary<TyreCompound, double>
            {
                {TyreCompound.SuperSoft, 0.8},
                {TyreCompound.Soft, 0.8},
                {TyreCompound.Medium, 0.9},
                {TyreCompound.Hard, 0.75}
            };
        }

        public DegradationResults Calculate(TyreInformation tyreInformation, IEnumerable<int> trackPoints,
            double temperature)
        {
            var coefficient = _degradationCoefficients[tyreInformation.Type];
            var pointTyreDegradations = trackPoints.Select(trackPoint =>
                (trackPoint * temperature) / (tyreInformation.DegradationCoefficient * coefficient)).ToList();

            return new DegradationResults
            {
                Average = pointTyreDegradations.Average(),
                Range = pointTyreDegradations.Max() - pointTyreDegradations.Min()
            };
        }
    }
}