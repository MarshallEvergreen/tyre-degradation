using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Interfaces;

namespace TyreDegradation.App.ViewModels
{
    public class SelectionCardViewModel : BindableBase
    {
        public SelectionCardViewModel(ITyreInformation tyreInformation)
        {
            // TODO Remove hardcoded path
            var info = tyreInformation.GetTyreData(@"C:\dev\tyre-degradation\Data\TyresXML.xml");
            FrontLeft = new TyreComboBoxViewModel(TyrePlacement.FrontLeft, info);
            FrontRight = new TyreComboBoxViewModel(TyrePlacement.FrontRight, info);
            RearLeft = new TyreComboBoxViewModel(TyrePlacement.RearLeft, info);
            RearRight = new TyreComboBoxViewModel(TyrePlacement.RearRight, info);
        }
        public TyreComboBoxViewModel FrontLeft { get; }
        public TyreComboBoxViewModel FrontRight { get; }
        public TyreComboBoxViewModel RearRight { get; }
        public TyreComboBoxViewModel RearLeft { get; }

    }
}