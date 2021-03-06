using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Xml.Linq;
using TyreDegradation.Contract.Enums;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Data.Parsers
{
    public class TyreInformationParser : ITyreInformation
    {
        private readonly IFileSystem _fileSystem;

        public TyreInformationParser(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Dictionary<TyrePlacement, List<TyreInformation>> GetTyreData(string context)
        {
            var tyres = XElement.Parse(_fileSystem.File.ReadAllText(context)).Elements();
            var allTyreInfo = new Dictionary<TyrePlacement, List<TyreInformation>>
            {
                [TyrePlacement.FrontLeft] = new(),
                [TyrePlacement.FrontRight] = new(),
                [TyrePlacement.RearLeft] = new(),
                [TyrePlacement.RearRight] = new()
            };
            
            foreach (var tyre in tyres)
            {
                var tyreInfo = new TyreInformation
                {
                    Name = (string) tyre.Element("Name")?.Value,
                    Family = (string) tyre.Element("Family")?.Value,
                    Type = (string) tyre.Element("Type")?.Value,
                    Placement = PlacementStringToEnum((string)tyre.Element("Placement")?.Value),
                    DegradationCoefficient = Convert.ToInt32(tyre.Element("DegradationCoefficient")?.Value)
                };
                allTyreInfo[tyreInfo.Placement].Add(tyreInfo);
            }

            return allTyreInfo;
        }

        private TyrePlacement PlacementStringToEnum(string placement)
        {
            switch (placement)
            {
                case "FL":
                    return TyrePlacement.FrontLeft;
                case "FR":
                    return TyrePlacement.FrontRight;
                case "RL":
                    return TyrePlacement.RearLeft;
                case "RR":
                    return TyrePlacement.RearRight;
                default:
                    throw new ArgumentException();
            }
        }
    }
}