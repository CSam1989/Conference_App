using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Settings;

namespace Application.Conference.Composite
{
    public class ConferenceLeaf: ConferenceComponent
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
            string durationString = $"{Duration.ToString()}min";

            if (Duration == _specialLength.Length)
                durationString = _specialLength.Name;

            return $"{Name} {durationString}";
        }

        public override bool IsComposite()
        {
            return false;
        }
    }
}
