using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.App.ViewModels
{
    public class TyreComboBoxViewModel : BindableBase
    {
        private readonly TyrePlacement _tyrePlacement;

        public TyreComboBoxViewModel(
            TyrePlacement tyrePlacement, 
            IReadOnlyDictionary<TyrePlacement, List<TyreInformation>> availableTyres)
        {
            _tyrePlacement = tyrePlacement;
            TyreName = tyrePlacement.ToString();
            
            AvailableTyres = new ObservableCollection<string>();
            foreach (var tyre in availableTyres[_tyrePlacement])
            {
                AvailableTyres.Add(tyre.Name);
            }
            AvailableTyres = AvailableTyres;
        }
        
        public ObservableCollection<string> AvailableTyres { get; }
        public string TyreName { get; }
    }
}