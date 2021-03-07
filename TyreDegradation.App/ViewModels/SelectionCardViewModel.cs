using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.Services.Results;

namespace TyreDegradation.App.ViewModels
{
    public class SelectionCardViewModel : BindableBase
    {
        public SelectionCardViewModel(
            ITyreInformation tyreInformation,
            ITrackInformation trackInformation,
            ResultsService resultsService)
        {
            // TODO Remove hardcoded path
            var tyreInfo = tyreInformation.GetTyreData(@"C:\dev\tyre-degradation\Data\TyresXML.xml");
            var trackInfo = trackInformation.GetTrackData(@"C:\dev\tyre-degradation\Data\TrackDegradationCoefficients.txt");
            FrontLeft = new TyreComboBoxViewModel(resultsService, TyrePlacement.FrontLeft, tyreInfo);
            FrontRight = new TyreComboBoxViewModel(resultsService, TyrePlacement.FrontRight, tyreInfo);
            RearLeft = new TyreComboBoxViewModel(resultsService, TyrePlacement.RearLeft, tyreInfo);
            RearRight = new TyreComboBoxViewModel(resultsService, TyrePlacement.RearRight, tyreInfo);
            TrackSelector = new TrackSelectorComboBoxViewModel(resultsService, trackInfo);
            TemperatureSelector = new TemperatureSelectorViewModel(resultsService);
        }

        public TyreComboBoxViewModel FrontLeft { get; }
        public TyreComboBoxViewModel FrontRight { get; }
        public TyreComboBoxViewModel RearRight { get; }
        public TyreComboBoxViewModel RearLeft { get; }
        public TrackSelectorComboBoxViewModel TrackSelector { get; }
        public TemperatureSelectorViewModel TemperatureSelector { get; }
    }
}