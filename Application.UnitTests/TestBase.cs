using System.Collections.Generic;
using Application.Common.Settings;
using Application.Conference.Composite;
using Moq;

namespace Application.UnitTests
{
    public class TestBase
    {
        public TestBase()
        {
            SpecialLengthSettings = new Mock<SpecialLengthSettings>();

            SpecialLengthSettings
                .Setup(s => s.Name)
                .Returns("test");
            SpecialLengthSettings
                .Setup(s => s.Length)
                .Returns(1);

            Sessions = new Mock<Sessions>();
            var sessionList = new List<SessionSettings> {new SessionSettings {MaxLength = 60, StartSession = 9}};

            Sessions
                .Setup(s => s.SessionList)
                .Returns(sessionList);

            TestTalks = new List<ConferenceComponent>
            {
                new ConferenceLeaf("test1", 10, SpecialLengthSettings.Object),
                new ConferenceLeaf("test2", 20, SpecialLengthSettings.Object),
                new ConferenceLeaf("test3", 30, SpecialLengthSettings.Object),
                new ConferenceLeaf("testSpecial", 1, SpecialLengthSettings.Object),
                new ConferenceLeaf("test4", 40, SpecialLengthSettings.Object)
            };
        }

        public Mock<SpecialLengthSettings> SpecialLengthSettings { get; set; }
        public Mock<Sessions> Sessions { get; set; }
        public IList<ConferenceComponent> TestTalks { get; set; }
    }
}