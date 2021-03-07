using System.Collections.Generic;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Business.Validation
{
    public class TyreSelectionValidator
    {
        public MessagedResult Validate(Dictionary<TyrePlacement, TyreInformation> selectedTyres)
        {
            var frontLeftType = selectedTyres[TyrePlacement.FrontLeft].Type;
            var frontRightType = selectedTyres[TyrePlacement.FrontRight].Type;
            var rearLeftType = selectedTyres[TyrePlacement.RearLeft].Type;
            var rearRightType = selectedTyres[TyrePlacement.RearRight].Type;

            if (!(frontLeftType == frontRightType && frontRightType == rearLeftType && rearLeftType == rearRightType))
            {
                return new MessagedResult
                {
                    Result = false,
                    Message = "Not All Tyres Are Same Compound"
                };
            }
            
            return new MessagedResult
            {
                Result = true
            };
        }
    }
}