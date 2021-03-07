using System;
using Prism.Mvvm;
using TyreDegradation.Services.Results;

namespace TyreDegradation.MainApp.ViewModels
{
    public class TemperatureSelectorViewModel : BindableBase
    {
        private readonly ResultsService _resultsService;
        private int _temperature;

        public TemperatureSelectorViewModel(ResultsService resultsService)
        {
            _resultsService = resultsService;
            _temperature = 25;
            _resultsService.SetTemperature(_temperature);
        }

        public string Temperature
        {
            get => _temperature.ToString();
            set
            {
                _temperature = Convert.ToInt32(value);
                _resultsService.SetTemperature(_temperature);
            }
        }
    }
}