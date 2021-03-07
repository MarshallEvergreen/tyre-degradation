using System.IO;
using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.Services.Results;

namespace TyreDegradation.MainApp.ViewModels
{
    public class SelectionCardViewModel : BindableBase
    {
        public SelectionCardViewModel(
            ITyreInformation tyreInformation,
            ITrackInformation trackInformation,
            ResultsService resultsService)
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // Get the Location of the data directory relative to the execution location of the app
            // this is where the TyresXML.xml and TrackDegradationCoefficients.txt are located.
            
            var dataLocation = Path.Combine(location, @"..\..\..\..\..\Data");
            var tyreInfoFile = Path.Combine(dataLocation, "TyresXML.xml");
            var trackInfoFile = Path.Combine(dataLocation, "TrackDegradationCoefficients.txt");
            
            // Read in the data from the above files. 
            // If this isn't working for some reason just hardcode the path in the function calls below
            // to wherever the files are located on your local machine. 
            var tyreInfo = tyreInformation.GetTyreData(tyreInfoFile);
            var trackInfo = trackInformation.GetTrackData(trackInfoFile);
            
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