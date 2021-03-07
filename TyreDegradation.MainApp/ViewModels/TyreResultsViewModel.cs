using System;
using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Services.Results;

namespace TyreDegradation.MainApp.ViewModels
{
    public class TyreResultsViewModel : BindableBase
    {
        private string _average;
        private string _range;

        public TyreResultsViewModel(ResultsService resultsService, TyrePlacement placement)
        {
            resultsService.AverageRecalculated[placement] += result =>
            {
                Average = Convert.ToInt32(result.Average).ToString();
                Range = Convert.ToInt32(result.Range).ToString();
            };
            Average = "NA";
            Range =  "NA";
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
    }
}