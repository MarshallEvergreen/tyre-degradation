using System.Collections.Generic;
using Moq;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.Contract.Models;

namespace TyreDegradation.MainApp.UnitTests
{
    public class MockTrackInformationBuilder
    {
        private readonly Mock<ITrackInformation> _mock;
        private readonly Dictionary<string, TrackInformation> _trackInformation;

        public MockTrackInformationBuilder()
        {
            _mock = new Mock<ITrackInformation>();
            _trackInformation = new Dictionary<string, TrackInformation>();
        }

        public Mock<ITrackInformation> Build()
        {
            AddTrackInformation();

            _mock.Setup(m => m.GetTrackData(It.IsAny<string>())).Returns(_trackInformation);
            return _mock;
        }

        public IReadOnlyDictionary<string, TrackInformation> Tracks => _trackInformation;

        private void AddTrackInformation()
        {
            _trackInformation["SomeTrack"] = new TrackInformation
            {
                Name = "SomeTrack",
                Location = "SomeWhere",
                DegradationPoints = new List<int>{10, 20}
            };
        }
    }
}