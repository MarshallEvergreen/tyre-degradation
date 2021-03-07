using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using TyreDegradation.Contract.Models;
using TyreDegradation.Services.Results;

namespace TyreDegradation.MainApp.ViewModels
{
    public class TrackSelectorComboBoxViewModel : BindableBase
    {
        private readonly ResultsService _resultsService;
        private readonly IReadOnlyDictionary<string, TrackInformation> _trackInformation;
        private string _track;

        public TrackSelectorComboBoxViewModel(
            ResultsService resultsService,
            IReadOnlyDictionary<string, TrackInformation> trackInformation)
        {
            _resultsService = resultsService;
            _trackInformation = trackInformation;
            
            Tracks = new ObservableCollection<string>();
            foreach (var track in trackInformation)
            {
                Tracks.Add(track.Key);
            }
        }
        
        public ObservableCollection<string> Tracks { get; }

        public string Track
        {
            get => _track;
            set
            {
                _track = value;
                _resultsService.SetSelectedTrack(_trackInformation[_track]);
            }
        }
    }
}