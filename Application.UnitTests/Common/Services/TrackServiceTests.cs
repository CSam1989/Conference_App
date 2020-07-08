using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Services;
using Application.Common.Settings;
using Application.Conference.Composite;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Common.Services
{
    [TestFixture]
    public class TrackServiceTests: TestBase
    {
        private ITrackService _trackService;
        private SessionSettings _sessionSettings;

        [SetUp]
        public void Setup()
        {
            var timeService = new TimeService();
            _trackService = new TrackService(timeService, SpecialLengthSettings.Object);

            _sessionSettings = Sessions.Object.SessionList.First();
        }

        [Test]
        public void CalculateTalksForSession_WhenGivenNormalValues_ReturnsNewListWithSessionTalks()
        {
            var result = _trackService.CalculateTalksForSession(
                TestTalks, 
                _sessionSettings.MaxLength,
                _sessionSettings.StartSession);

            result.ShouldBeOfType<List<ConferenceComponent>>();
            result.Count.ShouldBe(3);

            var component = result.FirstOrDefault();

            component.ShouldBeOfType<ConferenceLeaf>();
            component.TimeStamp.Hour.ShouldBe(_sessionSettings.StartSession);
        }

        [Test]
        public void CalculateTalksForSession_WhenGivenNoList_ThrowsError()
        {
            Should.Throw<ArgumentNullException>(() => _trackService.CalculateTalksForSession(
                null,
                _sessionSettings.MaxLength,
                _sessionSettings.StartSession));
        }

        [Test]
        public void CalculateTalksForSession_WhenGivenNegativeMaxMinutes_ThrowsError()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => _trackService.CalculateTalksForSession(
                TestTalks,
                -1,
                _sessionSettings.StartSession));
        }

        [Test]
        public void CalculateTalksForSession_WhenGivenNegativeStartingTime_ThrowsError()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => _trackService.CalculateTalksForSession(
                TestTalks,
                _sessionSettings.MaxLength,
                -1));
        }

        [Test]
        public void CalculateAfterSessionEvent_WhenGivenTotalDurationShorterThenMinStartEvent_ReturnsSessionEventWithMinStartEventAsStartingTime()
        {
            var result = _trackService.CalculateAfterSessionEvent(TestTalks, "Test", 12);

            result.ShouldBeOfType<ConferenceLeaf>();
            result.Name.ShouldBe("Test");
            result.TimeStamp.Hour.ShouldBe(12);
        }

        [Test]
        public void CalculateAfterSessionEvent_WhenGivenEmptyList_ReturnsNull()
        {
            var result = _trackService.CalculateAfterSessionEvent(new List<ConferenceComponent>(), "Test", 10);

            result.ShouldBeNull();
        }
    }
}
