using System;
using System.Xml.Linq;

namespace TyreDegradation.Data.Parsers
{
    public class TyreInformation
    {
        public TyreInformation(XElement element)
        {
            Name = (string) element.Element("Name")?.Value;
            Family = (string) element.Element("Family")?.Value;
            Type = (string) element.Element("Type")?.Value;
            Placement = (string) element.Element("Placement")?.Value;
            DegradationCoefficient = Convert.ToInt32(element.Element("DegradationCoefficient")?.Value);
        }

        public string Name { get; }
        public string Family { get; }
        public string Type { get; }
        public string Placement { get; }
        public int DegradationCoefficient { get; }
    }
}