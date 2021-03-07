using System.Collections.Generic;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Business.Validation
{
    public class TyreSelectionValidator
    {
        public MessagedResult Validate(Dictionary<TyrePlacement, TyreInformation> selectedTyres)
        {
            var frontLeft = selectedTyres[TyrePlacement.FrontLeft];
            var frontRight = selectedTyres[TyrePlacement.FrontRight];
            var rearLeft = selectedTyres[TyrePlacement.RearLeft];
            var rearRight = selectedTyres[TyrePlacement.RearRight];

            var frontLeftType = frontLeft.Type;
            var frontRightType = frontRight.Type;
            var rearLeftType = rearLeft.Type;
            var rearRightType = rearRight.Type;

            if (!(frontLeftType == frontRightType && frontRightType == rearLeftType && rearLeftType == rearRightType))
                return new MessagedResult
                {
                    Result = false,
                    Message = "Not All Tyres Are Same Compound"
                };

            var frontLeftFamily = frontLeft.Family;
            var frontRightFamily = frontRight.Family;

            if (frontLeftFamily != frontRightFamily)
                return new MessagedResult
                {
                    Result = false,
                    Message = "Front Axle Tyres Are Not The Same Family"
                };

            var rearLeftFamily = rearLeft.Family;
            var rearRightFamily = rearRight.Family;

            if (rearLeftFamily != rearRightFamily)
                return new MessagedResult
                {
                    Result = false,
                    Message = "Rear Axle Tyres Are Not The Same Family"
                };

            return new MessagedResult
            {
                Result = true
            };
        }
    }
}