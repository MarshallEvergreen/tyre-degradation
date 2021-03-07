using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TyreDegradation.Data.Parsers.UnitTests
{
    [TestClass]
    public class TrackInformationParserTests
    {
        private const string SingleTrackInfoFile = "SingleTrackData.txt";
        private const string MultipleTrackInfoFile = "MultipleTrackData.txt";
        private readonly List<int> _monacoDegradationPoints = new() {765, 456, 45, 456, 4};
        private readonly string _MonacoLocation = "Monaco";

        private readonly string _monacoName = "Monaco";
        private readonly List<int> _silverstoneDegradationPoints = new() {234, 2323, 43, 234, 45, 3434, 34, 3343, 434};
        private readonly string _silverstoneLocation = "Towcester";

        private readonly string _silverstoneName = "SilverStone";
        private MockFileSystem _fileSystem;
        private TrackInformationParser _trackInformationParser;

        [TestInitialize]
        public void TestInitialize()
        {
            var silverstoneTrackInfo =
                CreateTrackInfoString(_silverstoneName, _silverstoneLocation, _silverstoneDegradationPoints);

            var monacoTrackInfo = CreateTrackInfoString(_monacoName, _MonacoLocation, _monacoDegradationPoints);

            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {
                    SingleTrackInfoFile, new MockFileData(silverstoneTrackInfo)
                },
                {
                    MultipleTrackInfoFile, new MockFileData(silverstoneTrackInfo + "\n" + monacoTrackInfo)
                }
            });

            _trackInformationParser = new TrackInformationParser(_fileSystem);
        }

        private string CreateTrackInfoString(string name, string location, List<int> points)
        {
            return name + "|" + location + "|" + string.Join(",", points.ToArray());
        }

        [TestMethod]
        public void SingleTrack_OnlyContainsInfoForOneTrack()
        {
            var trackInfo = _trackInformationParser.GetTrackData(SingleTrackInfoFile);

            trackInfo.Should().HaveCount(1);
        }

        [TestMethod]
        public void SingleTrack_TrackNameParsedCorrectly()
        {
            var trackInfo = _trackInformationParser.GetTrackData(SingleTrackInfoFile);

            trackInfo["SilverStone"].Name.Should().Be(_silverstoneName);
        }

        [TestMethod]
        public void SingleTrack_TrackLocationParsedCorrectly()
        {
            var trackInfo = _trackInformationParser.GetTrackData(SingleTrackInfoFile);

            trackInfo["SilverStone"].Location.Should().Be(_silverstoneLocation);
        }

        [TestMethod]
        public void SingleTrack_TrackDegradationPointsParsedCorrectly()
        {
            var trackInfo = _trackInformationParser.GetTrackData(SingleTrackInfoFile);

            trackInfo["SilverStone"].DegradationPoints.Should().Equal(_silverstoneDegradationPoints);
        }

        [TestMethod]
        public void MultipleTracks_ContainsInformationForCorrectNumberOfTrack()
        {
            var trackInfo = _trackInformationParser.GetTrackData(MultipleTrackInfoFile);

            trackInfo.Should().HaveCount(2);
        }

        [TestMethod]
        public void MultipleTracks_NamesParsedCorrectly()
        {
            var trackInfo = _trackInformationParser.GetTrackData(MultipleTrackInfoFile);

            trackInfo["SilverStone"].Name.Should().Be(_silverstoneName);
            trackInfo["Monaco"].Name.Should().Be(_monacoName);
        }

        [TestMethod]
        public void MultipleTracks_LocationsParsedCorrectly()
        {
            var trackInfo = _trackInformationParser.GetTrackData(MultipleTrackInfoFile);

            trackInfo["SilverStone"].Location.Should().Be(_silverstoneLocation);
            trackInfo["Monaco"].Location.Should().Be(_MonacoLocation);
        }

        [TestMethod]
        public void MultipleTracks_DegradationPointsParsedCorrectly()
        {
            var trackInfo = _trackInformationParser.GetTrackData(MultipleTrackInfoFile);

            trackInfo["SilverStone"].DegradationPoints.Should().Equal(_silverstoneDegradationPoints);
            trackInfo["Monaco"].DegradationPoints.Should().Equal(_monacoDegradationPoints);
        }
    }
}