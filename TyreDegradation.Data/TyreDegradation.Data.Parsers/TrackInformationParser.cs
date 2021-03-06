using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.Data.Parsers
{
    public class TrackInformationParser : FileInformationParser, ITrackInformation
    {
        public TrackInformationParser(IFileSystem fileSystem) : base(fileSystem)
        {
        }

        public Dictionary<string, TrackInformation> GetTrackData(string context)
        {
            var fileData = FileSystem.File.ReadLines(context);
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