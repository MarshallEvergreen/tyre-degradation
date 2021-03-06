using System.Collections.Generic;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Contract.Interfaces
{
    public interface ITrackInformation
    {
        public Dictionary<string, TrackInformation> GetTrackData(string context);
    }
}