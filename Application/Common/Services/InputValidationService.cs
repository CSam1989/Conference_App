﻿using System;
using System.Linq;
using Application.Common.Interfaces;
using Application.Common.Settings;

namespace Application.Common.Services
{
    public class InputValidationService : IInputValidationService
    {
        private readonly Sessions _sessions;
        private readonly SpecialLengthSettings _specialLength;

        public InputValidationService(SpecialLengthSettings specialLength, Sessions sessions)
        {
            _specialLength = specialLength;
            _sessions = sessions;
        }

        public bool IsValidTalkTitle(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !name.Any(char.IsDigit);
        }

        public bool IsValidTalkDuration(string duration, out int output)
        {
            if (string.Equals(duration, _specialLength.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                output = _specialLength.Length;
                return true;
            }

            var highestMaxSessionDuration = _sessions.SessionList.Max(x => x.MaxLength);

            return int.TryParse(duration, out output) && output >= 0 && output <= highestMaxSessionDuration;
        }
    }
}