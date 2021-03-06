using System.Collections.Generic;
using System.IO.Abstractions;
using System.Xml.Linq;

namespace TyreDegradation.Data.Parsers
{
    public class TyreInformationParser
    {
        private readonly IFileSystem _fileSystem;
        public TyreInformationParser(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        
        public Dictionary<string, TyreInformation> ParseTyreData(string file)
        {
            var tyres = XElement.Parse(_fileSystem.File.ReadAllText(file)).Elements();
            var allTyreInfo = new Dictionary<string, TyreInformation>();
            foreach (var tyre in tyres)
            {
                var tyreInfo = new TyreInformation(tyre);
                allTyreInfo[tyreInfo.Name] = tyreInfo;
            }            
            return allTyreInfo;
        }
    }
}