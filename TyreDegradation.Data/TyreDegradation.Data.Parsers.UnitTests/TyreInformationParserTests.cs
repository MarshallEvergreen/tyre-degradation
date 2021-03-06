using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TyreDegradation.Contract.Enums;

namespace TyreDegradation.Data.Parsers.UnitTests
{
    [TestClass]
    public class TyreInformationParserTests
    {
        private TyreInformationParser _tyreInformationParser;
        private MockFileSystem _fileSystem;

        private const string SingleTyreFile = "SingleTyre.xml";
        private const string MultipleTyreFile = "MultipleTyres.xml";

        private const string Tyre1Name = "SuperSoft - Front Tyre 1";
        private const string Tyre1Family = "F1";
        private const string Tyre1Type = "SuperSoft";
        private const string Tyre1Placement = "FL";
        private const int Tyre1DegradationCoefficient = 10;
        
        private const string Tyre2Name = "SuperSoft - Front Tyre 2";
        private const string Tyre2Family = "F2";
        private const string Tyre2Type = "SuperSoft";
        private const string Tyre2Placement = "FL";
        private const int Tyre2DegradationCoefficient = 15;

        private static readonly string SingleTyreInformation = $@"
                                        <Tyres>
                                            <Tyre>
                                                <Name>{Tyre1Name}</Name>
                                                <Family>{Tyre1Family}</Family>
                                                <Type>{Tyre1Type}</Type>
                                                <Placement>{Tyre1Placement}</Placement>
                                                <DegradationCoefficient>{Tyre1DegradationCoefficient}</DegradationCoefficient>
                                            </Tyre>
                                        </Tyres>";

        private static readonly string MultipleTyreInformation = $@"
                                        <Tyres>
                                            <Tyre>
                                                <Name>{Tyre1Name}</Name>
                                                <Family>{Tyre1Family}</Family>
                                                <Type>{Tyre1Type}</Type>
                                                <Placement>{Tyre1Placement}</Placement>
                                                <DegradationCoefficient>{Tyre1DegradationCoefficient}</DegradationCoefficient>
                                            </Tyre>
                                            <Tyre>
                                                <Name>{Tyre2Name}</Name>
                                                <Family>{Tyre2Family}</Family>
                                                <Type>{Tyre2Type}</Type>
                                                <Placement>{Tyre2Placement}</Placement>
                                                <DegradationCoefficient>{Tyre2DegradationCoefficient}</DegradationCoefficient>
                                            </Tyre>
                                        </Tyres>";

        [TestInitialize]
        public void TestInitialize()
        {
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {
                    SingleTyreFile, SingleTyreInformation
                },
                {
                    MultipleTyreFile, MultipleTyreInformation
                }
            });
            _tyreInformationParser = new TyreInformationParser(_fileSystem);
        }

        [TestMethod]
        public void SingleTyre_OnlyContainsInfoForOneTyre()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(SingleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft].Should().HaveCount(1);
        }
        
        [TestMethod]
        public void SingleTyre_NameParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(SingleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].Name.Should().Be(Tyre1Name);
        }
        
        [TestMethod]
        public void SingleTyre_FamilyParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(SingleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].Family.Should().Be(Tyre1Family);
        }
        
        [TestMethod]
        public void SingleTyre_TypeParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(SingleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].Type.Should().Be(Tyre1Type);
        }
        
        [TestMethod]
        public void SingleTyre_PlacementParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(SingleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].Placement.Should().Be(TyrePlacement.FrontLeft);
        }
        
        [TestMethod]
        public void SingleTyre_DegradationCoefficientParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(SingleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].DegradationCoefficient.Should().Be(Tyre1DegradationCoefficient);
        }
        
        [TestMethod]
        public void MultipleTyres_ContainsInfoForExpectedNumberOfTyres()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(MultipleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft].Should().HaveCount(2);
        }
        
        [TestMethod]
        public void MultipleTyres_NameParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(MultipleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].Name.Should().Be(Tyre1Name);
            tyreInfo[TyrePlacement.FrontLeft][1].Name.Should().Be(Tyre2Name);
        }
        
        [TestMethod]
        public void MultipleTyres_FamilyParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(MultipleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].Family.Should().Be(Tyre1Family);
            tyreInfo[TyrePlacement.FrontLeft][1].Family.Should().Be(Tyre2Family);
        }
        
        [TestMethod]
        public void MultipleTyres_TypeParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(MultipleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].Type.Should().Be(Tyre1Type);
            tyreInfo[TyrePlacement.FrontLeft][1].Type.Should().Be(Tyre2Type);
        }
        
        [TestMethod]
        public void MultipleTyres_PlacementParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(MultipleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].Placement.Should().Be(TyrePlacement.FrontLeft);
            tyreInfo[TyrePlacement.FrontLeft][1].Placement.Should().Be(TyrePlacement.FrontLeft);
        }
        
        [TestMethod]
        public void MultipleTyres_DegradationCoefficientParsedCorrectly()
        {
            var tyreInfo = _tyreInformationParser.GetTyreData(MultipleTyreFile);

            tyreInfo[TyrePlacement.FrontLeft][0].DegradationCoefficient.Should().Be(Tyre1DegradationCoefficient);
            tyreInfo[TyrePlacement.FrontLeft][1].DegradationCoefficient.Should().Be(Tyre2DegradationCoefficient);
        }
    }
}