using System.Collections.Generic;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Contract.Interfaces
{
    public interface ITyreInformation
    {
        public Dictionary<TyrePlacement, List<TyreInformation>> GetTyreData(string context);
    }
}