using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace TyreDegradation.Data.Parsers
{
    public class TrackInformationParser
    {
        private readonly IFileSystem _fileSystem;

        public TrackInformationParser(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Dictionary<string, TrackInformation> ParseTrackData(string file)
        {
            var fileData = _fileSystem.File.ReadLines(file);
            Dictionary<string, TrackInformation> data = new();
            foreach (var track in fileData)
            {
                var trackInformation = CreateTrackInformation(track);
                data[trackInformation.Name] = trackInformation;
            }

            return data;
        }

        private TrackInformation CreateTrackInformation(string singleTackData)
        {
            var separatedInfo = singleTackData.Split("|");
            var points = separatedInfo[2].Split(',').Select(int.Parse).ToList();
            var trackInfo = new TrackInformation
            {
                Name = separatedInfo[0],
                Location = separatedInfo[1],
                DegradationPoints = points
            };
            return trackInfo;
        }
    }
}