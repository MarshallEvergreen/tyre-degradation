using System;
using System.Collections.Generic;
using TyreDegradation.Business.Calculations;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Services.Results
{
    public class ResultsService
    {
        private Dictionary<TyrePlacement, TyreInformation> _selectedTyres;
        private TrackInformation _selectedTrack;
        private int _temperature;
        private readonly DegradationCalculator _degradationCalculator;

        public ResultsService()
        {
            AverageRecalculated = new Dictionary<TyrePlacement, Action<DegradationResults>>
            {
                {TyrePlacement.FrontLeft, null},
                {TyrePlacement.FrontRight, null},
                {TyrePlacement.RearLeft, null},
                {TyrePlacement.RearRight, null}
            };
            _selectedTyres = new Dictionary<TyrePlacement, TyreInformation>
            {
                {TyrePlacement.FrontLeft, null},
                {TyrePlacement.FrontRight, null},
                {TyrePlacement.RearLeft, null},
                {TyrePlacement.RearRight, null}
            };
            
            _degradationCalculator = new DegradationCalculator();
        }

        public readonly Dictionary<TyrePlacement, Action<DegradationResults>> AverageRecalculated;

        public void SetSelectedTyre(TyrePlacement placement, TyreInformation tyreInformation)
        {
            _selectedTyres[placement] = tyreInformation;
            if (_selectedTrack is null)
            {
                return;
            }

            var result = CalculateDegradationResults(placement);
            AverageRecalculated[placement].Invoke(result);
        }

        public void SetSelectedTrack(TrackInformation trackInformation)
        {
            _selectedTrack = trackInformation;
            RecalculateForEachTyre();
        }
        
        public void SetTemperature(int temperature)
        {
            _temperature = temperature;
            RecalculateForEachTyre();
        }

        private void RecalculateForEachTyre()
        {
            if (_selectedTrack is null)
            {
                return;
            }
            foreach (var (placement, action) in AverageRecalculated)
            {
                var result = CalculateDegradationResults(placement);
                action?.Invoke(result);
            }
        } 

        private DegradationResults CalculateDegradationResults(TyrePlacement placement)
        {
            return _degradationCalculator.Calculate(_selectedTyres[placement], _selectedTrack.DegradationPoints, _temperature);
        }
    }
}