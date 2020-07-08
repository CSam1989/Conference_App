using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Factories;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Input;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Common.Factories
{
    [TestFixture]
    public class InputFactoryTests
    {
        private IInputFactory _inputFactory;

        [SetUp]
        public void Setup()
        {
            var specialLengthSettings = new Mock<SpecialLengthSettings>();
            var inputValidation = new Mock<IInputValidationService>();

            _inputFactory = new InputFactory(specialLengthSettings.Object, inputValidation.Object);
        }

        [Test]
        public void WhenFileInputTypeIsGiven_ReturnsCorrectObject()
        {
            var result = _inputFactory.GetInputStrategy("file");

            result.ShouldBeOfType<FileInput>();
        }

        [Test]
        public void WhenManualInputTypeIsGiven_ReturnsCorrectObject()
        {
            var result = _inputFactory.GetInputStrategy("manual");

            result.ShouldBeOfType<ManualInput>();
        }

        [Test]
        public void WhenWrongInputTypeIsGiven_ThrowsError()
        {
            Should.Throw<NotSupportedException>(() => _inputFactory.GetInputStrategy("test"));
        }
    }
}
