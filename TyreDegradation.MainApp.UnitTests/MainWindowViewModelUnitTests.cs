using System.Collections.Generic;
using System.Windows;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.MainApp.ViewModels;
using TyreDegradation.Services.Results;

namespace TyreDegradation.MainApp.UnitTests
{
    [TestClass]
    public class MainWindowViewModelUnitTests
    {
        private MainWindowViewModel _mainWindowViewModel;

        private MockTrackInformationBuilder _mockTrackInformationBuilder;
        private MockTyreInformationBuilder _mockTyreInformationBuilder;
        private ResultsService _resultsService;
        private Mock<ITrackInformation> _trackInformation;
        private Mock<ITyreInformation> _tyreInformation;

        [TestInitialize]
        public void Initialize()
        {
            _mockTyreInformationBuilder = new MockTyreInformationBuilder();
            _tyreInformation = _mockTyreInformationBuilder.Build();

            _mockTrackInformationBuilder = new MockTrackInformationBuilder();
            _trackInformation = _mockTrackInformationBuilder.Build();

            _resultsService = new ResultsService();
            _mainWindowViewModel = new MainWindowViewModel(
                _tyreInformation.Object,
                _trackInformation.Object,
                _resultsService);
        }

        private void SelectTyresAndATrack()
        {
            _mainWindowViewModel.SelectionCard.FrontLeft.SelectedTyre = "Tyre 1 - F1";
            _mainWindowViewModel.SelectionCard.FrontRight.SelectedTyre = "Tyre 2 - F1";
            _mainWindowViewModel.SelectionCard.RearLeft.SelectedTyre = "Tyre 3 - F1";
            _mainWindowViewModel.SelectionCard.RearRight.SelectedTyre = "Tyre 4 - F1";
            _mainWindowViewModel.SelectionCard.TrackSelector.Track = "SomeTrack";
        }

        private void InvalidTyreSelection()
        {
            _mainWindowViewModel.SelectionCard.FrontLeft.SelectedTyre = "Tyre 5 - F2";
            _mainWindowViewModel.SelectionCard.FrontRight.SelectedTyre = "Tyre 2 - F1";
            _mainWindowViewModel.SelectionCard.RearLeft.SelectedTyre = "Tyre 3 - F1";
            _mainWindowViewModel.SelectionCard.RearRight.SelectedTyre = "Tyre 4 - F1";
            _mainWindowViewModel.SelectionCard.TrackSelector.Track = "SomeTrack";
        }

        private void AveragesAndRangesAre(string average, string range)
        {
            _mainWindowViewModel.ResultsCard.FrontLeft.Average.Should().Be(average);
            _mainWindowViewModel.ResultsCard.FrontLeft.Range.Should().Be(range);

            _mainWindowViewModel.ResultsCard.FrontRight.Average.Should().Be(average);
            _mainWindowViewModel.ResultsCard.FrontRight.Range.Should().Be(range);

            _mainWindowViewModel.ResultsCard.RearLeft.Average.Should().Be(average);
            _mainWindowViewModel.ResultsCard.RearLeft.Range.Should().Be(range);

            _mainWindowViewModel.ResultsCard.RearRight.Average.Should().Be(average);
            _mainWindowViewModel.ResultsCard.RearRight.Range.Should().Be(range);
        }

        private void AverageAndRangeColoursAre(string averageColour, string rangeColour)
        {
            _mainWindowViewModel.ResultsCard.FrontLeft.AverageColour.Should().Be(averageColour);
            _mainWindowViewModel.ResultsCard.FrontLeft.RangeColour.Should().Be(rangeColour);

            _mainWindowViewModel.ResultsCard.FrontRight.AverageColour.Should().Be(averageColour);
            _mainWindowViewModel.ResultsCard.FrontRight.RangeColour.Should().Be(rangeColour);

            _mainWindowViewModel.ResultsCard.RearLeft.AverageColour.Should().Be(averageColour);
            _mainWindowViewModel.ResultsCard.RearLeft.RangeColour.Should().Be(rangeColour);

            _mainWindowViewModel.ResultsCard.RearRight.AverageColour.Should().Be(averageColour);
            _mainWindowViewModel.ResultsCard.RearRight.RangeColour.Should().Be(rangeColour);
        }

        [TestMethod]
        public void TyrePlacement_NameIsCorrectForEachViewModel()
        {
            _mainWindowViewModel.SelectionCard.FrontLeft.TyrePlacement.Should().Be("FrontLeft");
            _mainWindowViewModel.SelectionCard.FrontRight.TyrePlacement.Should().Be("FrontRight");
            _mainWindowViewModel.SelectionCard.RearLeft.TyrePlacement.Should().Be("RearLeft");
            _mainWindowViewModel.SelectionCard.RearRight.TyrePlacement.Should().Be("RearRight");
        }

        [TestMethod]
        public void TyrePlacement_EachViewModelHasTheRightTyres()
        {
            _mainWindowViewModel.SelectionCard
                .FrontLeft.AvailableTyres.Should().BeEquivalentTo(new List<string> {"Tyre 1 - F1", "Tyre 5 - F2"});

            _mainWindowViewModel.SelectionCard
                .FrontRight.AvailableTyres.Should().BeEquivalentTo(new List<string> {"Tyre 2 - F1", "Tyre 6 - F2"});

            _mainWindowViewModel.SelectionCard
                .RearLeft.AvailableTyres.Should().BeEquivalentTo(new List<string> {"Tyre 3 - F1", "Tyre 7 - F2"});

            _mainWindowViewModel.SelectionCard
                .RearRight.AvailableTyres.Should().BeEquivalentTo(new List<string> {"Tyre 4 - F1", "Tyre 8 - F2"});
        }

        [TestMethod]
        public void Tracks_CorrectTracksAreAvailable()
        {
            _mainWindowViewModel.SelectionCard
                .TrackSelector.Tracks.Should().BeEquivalentTo(new List<string> {"SomeTrack"});
        }

        [TestMethod]
        public void Temperature_DefaultTemperatureIsSet()
        {
            _mainWindowViewModel.SelectionCard
                .TemperatureSelector.Temperature.Should().Be("25");
        }

        [TestMethod]
        public void Results_NoResultsAvailable_WhenTyresAndTrackNotSelected()
        {
            AveragesAndRangesAre("NA", "NA");
        }

        [TestMethod]
        public void Results_NotAvailable_WhenTyresSelected_ButTrackNotSelected()
        {
            _mainWindowViewModel.SelectionCard.FrontLeft.SelectedTyre = "Tyre 1 - F1";
            _mainWindowViewModel.SelectionCard.FrontRight.SelectedTyre = "Tyre 2 - F1";
            _mainWindowViewModel.SelectionCard.RearLeft.SelectedTyre = "Tyre 3 - F1";
            _mainWindowViewModel.SelectionCard.RearRight.SelectedTyre = "Tyre 4 - F1";

            AveragesAndRangesAre("NA", "NA");
        }

        [TestMethod]
        public void Results_NotAvailable_WhenTyresNotSelected_ButTrackSelected()
        {
            _mainWindowViewModel.SelectionCard.TrackSelector.Track = "SomeTrack";

            AveragesAndRangesAre("NA", "NA");
        }

        [TestMethod]
        public void Results_Available_WhenTyresSelected_AndTrackSelected()
        {
            SelectTyresAndATrack();

            AveragesAndRangesAre("47", "31");
        }

        [TestMethod]
        public void Results_Update_WhenTemperatureChanges()
        {
            SelectTyresAndATrack();

            AveragesAndRangesAre("47", "31");

            _mainWindowViewModel.SelectionCard.TemperatureSelector.Temperature = "50";

            AveragesAndRangesAre("94", "62");
        }

        [TestMethod]
        public void ResultsBackground_GreenWhenInRightRange()
        {
            SelectTyresAndATrack();

            AverageAndRangeColoursAre("Green", "Green");
        }

        [TestMethod]
        public void ResultsBackground_OrangeWhenInRightRange()
        {
            SelectTyresAndATrack();

            _mainWindowViewModel.SelectionCard.TemperatureSelector.Temperature = "900";

            AverageAndRangeColoursAre("Orange", "Orange");
        }

        [TestMethod]
        public void ResultsBackground_RedWhenInRightRange()
        {
            SelectTyresAndATrack();

            _mainWindowViewModel.SelectionCard.TemperatureSelector.Temperature = "90000";

            AverageAndRangeColoursAre("Red", "Red");
        }

        [TestMethod]
        public void InvalidSelections_WarningIsRaised()
        {
            InvalidTyreSelection();

            _mainWindowViewModel.SelectionWarning.WarningVisibility.Should().Be(Visibility.Visible);
        }

        [TestMethod]
        public void InvalidSelections_ResultsAreSetToNA()
        {
            InvalidTyreSelection();

            AveragesAndRangesAre("NA", "NA");
            AverageAndRangeColoursAre("#00FFFFFF", "#00FFFFFF");
        }
    }
}