using System;
using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Services.Results;

namespace TyreDegradation.App.ViewModels
{
    public class TyreResultsViewModel : BindableBase
    {
        private double _average;
        private double _range;

        public TyreResultsViewModel(ResultsService resultsService, TyrePlacement placement)
        {
            resultsService.AverageRecalculated[placement] += d => Average = d;
        }

        public double Average
        {
            get => _average;
            set => SetProperty(ref _average, value);
        }

        public double Range
        {
            get => _range;
            set => SetProperty(ref _range, value);
        }
    }
}