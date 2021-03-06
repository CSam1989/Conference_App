﻿using Application.Common.Interfaces;
using Application.Common.Services;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Common.Services
{
    [TestFixture]
    public class InputValidationServiceTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
            _inputValidation = new InputValidationService(SpecialLengthSettings.Object, Sessions.Object);
        }

        private IInputValidationService _inputValidation;

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
            var result = _inputValidation.IsValidTalkDuration(duration, out var output);

            result.ShouldBe(expectedResult);
        }
    }
}