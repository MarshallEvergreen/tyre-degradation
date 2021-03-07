using System.Collections.Generic;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Business.Validation.UnitTests
{
    public class SelectedTyresBuilder
    {
        private Dictionary<TyrePlacement, TyreInformation> _selectedTyres;

        public SelectedTyresBuilder()
        {
            _selectedTyres = new Dictionary<TyrePlacement, TyreInformation>();
        }

        public Dictionary<TyrePlacement, TyreInformation> Build()
        {
            return _selectedTyres;
        }
        
        public SelectedTyresBuilder FrontLeftTyre(TyreCompound compound, string family)
        {
            _selectedTyres[TyrePlacement.FrontLeft] = new TyreInformation
            {
                Type = compound,
                Family = family
            };

            return this;
        }
        
        public SelectedTyresBuilder FrontRightTyre(TyreCompound compound, string family)
        {
            _selectedTyres[TyrePlacement.FrontRight] = new TyreInformation
            {
                Type = compound,
                Family = family
            };

            return this;
        }
        
        public SelectedTyresBuilder RearLeftTyre(TyreCompound compound, string family)
        {
            _selectedTyres[TyrePlacement.RearLeft] = new TyreInformation
            {
                Type = compound,
                Family = family
            };

            return this;
        }
        
        public SelectedTyresBuilder RearRightTyre(TyreCompound compound, string family)
        {
            _selectedTyres[TyrePlacement.RearRight] = new TyreInformation
            {
                Type = compound,
                Family = family
            };

            return this;
        }
    }
}