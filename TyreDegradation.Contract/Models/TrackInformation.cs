using System.Collections.Generic;

namespace TyreDegradation.Contract.Models
{
    public class TrackInformation
    {
        public string Name { get; init; }
        public string Location { get; init; }
        public List<int> DegradationPoints { get; init; }
    }
}