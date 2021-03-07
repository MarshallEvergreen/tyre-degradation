using System;
using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Services.Results;

namespace TyreDegradation.MainApp.ViewModels
{
    public class TyreResultsViewModel : BindableBase
    {
        private const string Transparent = "#00FFFFFF";
        private string _average;
        private string _averageColour;
        private string _range;
        private string _rangeColour;

        public TyreResultsViewModel(ResultsService resultsService, TyrePlacement placement)
        {
            resultsService.DegradationResults[placement] += result =>
            {
                var average = Convert.ToInt32(result.Average);
                var range = Convert.ToInt32(result.Range);

                AverageColour = GetColourForValue(average);
                Average = average.ToString();

                RangeColour = GetColourForValue(range);
                Range = range.ToString();
            };

            resultsService.SelectionsInvalid += _ => SetResultsToNa();
            SetResultsToNa();
        }

        public string Average
        {
            get => _average;
            set => SetProperty(ref _average, value);
        }

        public string Range
        {
            get => _range;
            set => SetProperty(ref _range, value);
        }

        public string AverageColour
        {
            get => _averageColour;
            set => SetProperty(ref _averageColour, value);
        }

        public string RangeColour
        {
            get => _rangeColour;
            set => SetProperty(ref _rangeColour, value);
        }

        private string GetColourForValue(int value)
        {
            return value switch
            {
                < 999 => "Green",
                > 999 and < 3000 => "Orange",
                > 3000 => "Red",
                _ => Transparent
            };
        }

        private void SetResultsToNa()
        {
            Average = "NA";
            Range = "NA";
            AverageColour = Transparent;
            RangeColour = Transparent;
        }
    }
}