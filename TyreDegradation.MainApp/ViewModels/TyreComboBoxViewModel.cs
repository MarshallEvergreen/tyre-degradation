using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;
using TyreDegradation.Services.Results;

namespace TyreDegradation.MainApp.ViewModels
{
    public class TyreComboBoxViewModel : BindableBase
    {
        private readonly TyrePlacement _tyrePlacement;
        private readonly Dictionary<string, TyreInformation> _availableTyreInformation;
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
            _availableTyreInformation = new Dictionary<string, TyreInformation>();
            foreach (var tyre in availableTyres[_tyrePlacement])
            {
                var tyreNameAndFamily = tyre.Name + " - " + tyre.Family;
                AvailableTyres.Add(tyreNameAndFamily);
                _availableTyreInformation[tyreNameAndFamily] = tyre;
            }
        }
        
        public ObservableCollection<string> AvailableTyres { get; }

        public string SelectedTyre
        {
            get => _selectedTyre;
            set
            {
                _selectedTyre = value;
                _resultsService.SetSelectedTyre(_tyrePlacement, _availableTyreInformation[_selectedTyre]);
            }
        }

        public string TyrePlacement { get; }
    }
}