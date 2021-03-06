using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Services.Results;

namespace TyreDegradation.App.ViewModels
{
    public class ResultsCardViewModel : BindableBase
    {
        public ResultsCardViewModel(ResultsService resultsService)
        {
            FrontLeft = new TyreResultsViewModel(resultsService, TyrePlacement.FrontLeft);
            FrontRight = new TyreResultsViewModel(resultsService, TyrePlacement.FrontRight);
            RearLeft = new TyreResultsViewModel(resultsService, TyrePlacement.RearLeft);
            RearRight = new TyreResultsViewModel(resultsService, TyrePlacement.RearRight);
        }
        public TyreResultsViewModel FrontLeft { get; }
        public TyreResultsViewModel FrontRight { get; }
        public TyreResultsViewModel RearRight { get; }
        public TyreResultsViewModel RearLeft { get; }

    }
}