using System;
using System.Collections.Generic;
using TyreDegradation.Business.Calculations;
using TyreDegradation.Business.Validation;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Services.Results
{
    public class ResultsService
    {
        private readonly DegradationCalculator _degradationCalculator;
        private readonly Dictionary<TyrePlacement, TyreInformation> _selectedTyres;
        private readonly TyreSelectionValidator _tyreSelectionValidator;

        public readonly Dictionary<TyrePlacement, Action<DegradationResults>> DegradationResults;
        private TrackInformation _selectedTrack;
        private int _temperature;
        public Action<string> SelectionsInvalid;
        public Action SelectionsValid;

        public ResultsService()
        {
            DegradationResults = new Dictionary<TyrePlacement, Action<DegradationResults>>
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
            _tyreSelectionValidator = new TyreSelectionValidator();
        }

        public void SetSelectedTyre(TyrePlacement placement, TyreInformation tyreInformation)
        {
            _selectedTyres[placement] = tyreInformation;
            RecalculateForEachTyre();
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
            foreach (var (placement, _) in DegradationResults)
                if (_selectedTyres[placement] == null)
                    return;
            if (_selectedTrack is null) return;

            var validationResult = _tyreSelectionValidator.Validate(_selectedTyres);
            if (validationResult.Result == false)
            {
                SelectionsInvalid.Invoke(validationResult.Message);
                return;
            }

            SelectionsValid.Invoke();
            foreach (var (placement, action) in DegradationResults)
            {
                var result = CalculateDegradationResults(placement);
                action.Invoke(result);
            }
        }

        private DegradationResults CalculateDegradationResults(TyrePlacement placement)
        {
            return _degradationCalculator.Calculate(_selectedTyres[placement], _selectedTrack.DegradationPoints,
                _temperature);
        }
    }
}