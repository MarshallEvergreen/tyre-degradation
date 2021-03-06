using System;
using System.Collections.Generic;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Services.Results
{
    public class ResultsService
    {
        private Dictionary<TyrePlacement, TyreInformation> _selectedTyres;
        private TrackInformation _selectedTrack;
        public ResultsService()
        {
            AverageRecalculated = new Dictionary<TyrePlacement, Action<double>>
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
        }

        public readonly Dictionary<TyrePlacement, Action<double>> AverageRecalculated;

        public void SetSelectedTyre(TyrePlacement placement)
        {
            AverageRecalculated[placement].Invoke(new Random().Next(1, 3000));
        }
        
        public void SetSelectedTrack(TrackInformation trackInformation)
        {
            _selectedTrack = trackInformation;
            foreach (var keyAction in AverageRecalculated)
            {
                keyAction.Value.Invoke(new Random().Next(1, 3000));
            }
        }
    }
}