﻿using System;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Input;

namespace Application.Common.Factories
{
    public class InputFactory : IInputFactory
    {
        private readonly IInputValidationService _inputValidation;
        private readonly SpecialLengthSettings _specialLength;

        public InputFactory(SpecialLengthSettings specialLength, IInputValidationService inputValidation)
        {
            _specialLength = specialLength;
            _inputValidation = inputValidation;
        }

        //Violates OCP
        public IInputStrategy GetInputStrategy(string inputType)
        {
            switch (inputType.ToLower())
            {
                case "file":
                    return new FileInput(_specialLength, _inputValidation);
                case "manual":
                    return new ManualInput(_specialLength, _inputValidation);
            }

            throw new NotSupportedException($"{inputType} is not a supported input type.");
        }
    }
}