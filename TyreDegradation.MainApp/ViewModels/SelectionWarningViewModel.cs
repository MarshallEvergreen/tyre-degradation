using System.Windows;
using Prism.Mvvm;
using TyreDegradation.Services.Results;

namespace TyreDegradation.MainApp.ViewModels
{
    public class SelectionWarningViewModel : BindableBase
    {
        private Visibility _visibility;
        private string _warningMessage;

        public SelectionWarningViewModel(ResultsService resultsService)
        {
            _visibility = Visibility.Collapsed;
            _warningMessage = string.Empty;

            resultsService.SelectionsValid += () => WarningVisibility = Visibility.Collapsed;
            resultsService.SelectionsInvalid += s =>
            {
                WarningMessage = s;
                WarningVisibility = Visibility.Visible;
            };
        }

        public string WarningMessage
        {
            get => _warningMessage;
            set => SetProperty(ref _warningMessage, value);
        }

        public Visibility WarningVisibility
        {
            get => _visibility;
            set => SetProperty(ref _visibility, value);
        }
    }
}