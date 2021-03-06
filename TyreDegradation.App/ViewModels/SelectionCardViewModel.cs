using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.Services.Results;

namespace TyreDegradation.App.ViewModels
{
    public class SelectionCardViewModel : BindableBase
    {
        public SelectionCardViewModel(ITyreInformation tyreInformation, ResultsService resultsService)
        {
            // TODO Remove hardcoded path
            var info = tyreInformation.GetTyreData(@"C:\dev\tyre-degradation\Data\TyresXML.xml");
            FrontLeft = new TyreComboBoxViewModel(resultsService, TyrePlacement.FrontLeft, info);
            FrontRight = new TyreComboBoxViewModel(resultsService, TyrePlacement.FrontRight, info);
            RearLeft = new TyreComboBoxViewModel(resultsService, TyrePlacement.RearLeft, info);
            RearRight = new TyreComboBoxViewModel(resultsService, TyrePlacement.RearRight, info);
        }

        public TyreComboBoxViewModel FrontLeft { get; }
        public TyreComboBoxViewModel FrontRight { get; }
        public TyreComboBoxViewModel RearRight { get; }
        public TyreComboBoxViewModel RearLeft { get; }
    }
}