using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Input;

namespace Application.Common.Factories
{
    public class InputFactory : IInputFactory
    {
        private readonly SpecialLengthSettings _specialLength;

        public InputFactory(SpecialLengthSettings specialLength)
        {
            _specialLength = specialLength;
        }

        //Violates OCP
        public IInputStrategy GetInputStrategy(string inputType)
        {
            switch (inputType.ToLower())
            {
                case "file":
                    return new FileInput(_specialLength);
                case "manual":
                    return new ManualInput(_specialLength);
            }

            return null;
        }
    }
}
