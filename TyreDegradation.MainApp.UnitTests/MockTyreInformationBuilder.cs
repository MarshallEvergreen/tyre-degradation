using System.Collections.Generic;
using Moq;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.MainApp.UnitTests
{
    public class MockTyreInformationBuilder
    {
        private readonly Mock<ITyreInformation> _mock;
        private readonly Dictionary<TyrePlacement, List<TyreInformation>> _tyreInformation;

        public MockTyreInformationBuilder()
        {
            _mock = new Mock<ITyreInformation>();
            _tyreInformation = new Dictionary<TyrePlacement, List<TyreInformation>>();
        }

        public IReadOnlyDictionary<TyrePlacement, List<TyreInformation>> Tyres => _tyreInformation;

        public Mock<ITyreInformation> Build()
        {
            AddFrontLeftTyres();
            AddFrontRightTyres();
            AddRearLeftTyres();
            AddRearRightTyres();

            _mock.Setup(m => m.GetTyreData(It.IsAny<string>())).Returns(_tyreInformation);
            return _mock;
        }

        private void AddFrontLeftTyres()
        {
            _tyreInformation[TyrePlacement.FrontLeft] = new List<TyreInformation>();
            var frontLeftTyres = new List<TyreInformation>
            {
                new()
                {
                    Name = "Tyre 1",
                    Family = "F1",
                    Placement = TyrePlacement.FrontLeft,
                    Type = TyreCompound.SuperSoft,
                    DegradationCoefficient = 10
                },
                new()
                {
                    Name = "Tyre 5",
                    Family = "F2",
                    Placement = TyrePlacement.FrontLeft,
                    Type = TyreCompound.SuperSoft,
                    DegradationCoefficient = 10
                }
            };
            _tyreInformation[TyrePlacement.FrontLeft] = frontLeftTyres;
        }

        private void AddFrontRightTyres()
        {
            _tyreInformation[TyrePlacement.FrontRight] = new List<TyreInformation>();
            var frontRightTyres = new List<TyreInformation>
            {
                new()
                {
                    Name = "Tyre 2",
                    Family = "F1",
                    Placement = TyrePlacement.FrontRight,
                    Type = TyreCompound.SuperSoft,
                    DegradationCoefficient = 10
                },
                new()
                {
                    Name = "Tyre 6",
                    Family = "F2",
                    Placement = TyrePlacement.FrontRight,
                    Type = TyreCompound.SuperSoft,
                    DegradationCoefficient = 10
                }
            };
            _tyreInformation[TyrePlacement.FrontRight] = frontRightTyres;
        }

        private void AddRearLeftTyres()
        {
            _tyreInformation[TyrePlacement.RearLeft] = new List<TyreInformation>();
            var rearLeftTyres = new List<TyreInformation>
            {
                new()
                {
                    Name = "Tyre 3",
                    Family = "F1",
                    Placement = TyrePlacement.RearLeft,
                    Type = TyreCompound.SuperSoft,
                    DegradationCoefficient = 10
                },
                new()
                {
                    Name = "Tyre 7",
                    Family = "F2",
                    Placement = TyrePlacement.RearLeft,
                    Type = TyreCompound.SuperSoft,
                    DegradationCoefficient = 10
                }
            };
            _tyreInformation[TyrePlacement.RearLeft] = rearLeftTyres;
        }

        private void AddRearRightTyres()
        {
            _tyreInformation[TyrePlacement.RearRight] = new List<TyreInformation>();
            var rearRightTyres = new List<TyreInformation>
            {
                new()
                {
                    Name = "Tyre 4",
                    Family = "F1",
                    Placement = TyrePlacement.RearRight,
                    Type = TyreCompound.SuperSoft,
                    DegradationCoefficient = 10
                },
                new()
                {
                    Name = "Tyre 8",
                    Family = "F2",
                    Placement = TyrePlacement.RearRight,
                    Type = TyreCompound.SuperSoft,
                    DegradationCoefficient = 10
                }
            };
            _tyreInformation[TyrePlacement.RearRight] = rearRightTyres;
        }
    }
}