using Prism.Mvvm;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.Services.Results;

namespace TyreDegradation.MainApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(ITyreInformation tyreInformation, ITrackInformation trackInformation,
            ResultsService resultsService)
        {
            SelectionCard = new SelectionCardViewModel(tyreInformation, trackInformation, resultsService);
            ResultsCard = new ResultsCardViewModel(resultsService);
            SelectionWarning = new SelectionWarningViewModel(resultsService);
        }

        public string Title => "Tyre Degradation Application";
        public SelectionCardViewModel SelectionCard { get; }
        public ResultsCardViewModel ResultsCard { get; }
        public SelectionWarningViewModel SelectionWarning { get; }
    }
}