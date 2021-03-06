using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;
using TyreDegradation.Services.Results;

namespace TyreDegradation.App.ViewModels
{
    public class TyreComboBoxViewModel : BindableBase
    {
        private readonly TyrePlacement _tyrePlacement;
        private string _selectedTyre;
        private readonly ResultsService _resultsService;
        public TyreComboBoxViewModel(
            ResultsService resultsService,
            TyrePlacement tyrePlacement, 
            IReadOnlyDictionary<TyrePlacement, List<TyreInformation>> availableTyres)
        {
            _resultsService = resultsService;
            _tyrePlacement = tyrePlacement;
            TyrePlacement = tyrePlacement.ToString();
            
            AvailableTyres = new ObservableCollection<string>();
            foreach (var tyre in availableTyres[_tyrePlacement])
            {
                AvailableTyres.Add(tyre.Name);
            }
            AvailableTyres = AvailableTyres;
        }
        
        public ObservableCollection<string> AvailableTyres { get; }

        public string SelectedTyre
        {
            get => _selectedTyre;
            set
            {
                _selectedTyre = value;
                _resultsService.SetSelectedTyre(_tyrePlacement);
            }
        }

        public string TyrePlacement { get; }
    }
}