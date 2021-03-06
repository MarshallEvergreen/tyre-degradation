using TyreDegradation.Contract.Enums;

namespace TyreDegradation.Contract.Models
{
    public class TyreInformation
    {
        public string Name { get; init; }
        public string Family { get; init; }
        public TyreCompound Type { get; init; }
        public TyrePlacement Placement { get; init; }
        public int DegradationCoefficient { get; init; }
    }
}