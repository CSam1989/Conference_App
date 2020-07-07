using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Services;
using Application.Common.Settings;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Common.Services
{
    [TestFixture]
    public class InputValidationServiceTests
    {
        private IInputValidationService _inputValidation;

        [SetUp]
        public void Setup()
        {
            var specialLengthSettings = new Mock<SpecialLengthSettings>();

            specialLengthSettings
                .Setup(s => s.Name)
                .Returns("test");
            specialLengthSettings
                .Setup(s => s.Length)
                .Returns(1);

            var sessions = new Mock<Sessions>();
            var sessionList = new List<SessionSettings> {new SessionSettings() {MaxLength = 60}};

            sessions
                .Setup(s => s.SessionList)
                .Returns(sessionList);

            _inputValidation = new InputValidationService(specialLengthSettings.Object, sessions.Object);
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase(null, false)]
        [TestCase("1a", false)]
        [TestCase("1", false)]
        [TestCase("test", true)]
        public void GivenName_ShouldReturnExpectedResult(string name, bool expectedResult)
        {
            var result = _inputValidation.IsValidTalkTitle(name);

            result.ShouldBe(expectedResult);
        }

        [TestCase("-1", false)]
        [TestCase("1", true)]
        [TestCase("60", true)]
        [TestCase("61", false)]
        [TestCase("test", true)]
        [TestCase("t", false)]
        public void GivenDuration_ShouldReturnExpectedResult(string duration, bool expectedResult)
        {
            var result = _inputValidation.IsValidTalkDuration(duration, out int output);

            result.ShouldBe(expectedResult);
        }
    }
}
