using System;
using System.Globalization;
using Application.Common.Settings;

namespace Application.Conference.Composite
{
    public class ConferenceLeaf : ConferenceComponent
    {
        private readonly SpecialLengthSettings _specialLength;

        public ConferenceLeaf(string name, int duration, SpecialLengthSettings specialLength)
        {
            _specialLength = specialLength;
            Name = name;
            Duration = duration;
        }

        public override string Print()
        {
            var durationString = $"{Duration.ToString()}min";

            if (Duration == _specialLength.Length)
                durationString = _specialLength.Name;

            if (Duration == 0)
                durationString = null;

            return $"{TimeStamp.ToString("hh:mm tt", new CultureInfo("en-US"))} {Name} {durationString}{Environment.NewLine}";
        }
    }
}