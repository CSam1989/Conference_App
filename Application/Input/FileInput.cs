using System.Collections.Generic;
using System.IO;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Conference.Composite;

namespace Application.Input
{
    public class FileInput : IInputStrategy
    {
        private readonly IInputValidationService _inputValidation;
        private readonly SpecialLengthSettings _specialLength;

        public FileInput(SpecialLengthSettings specialLength, IInputValidationService inputValidation)
        {
            _specialLength = specialLength;
            _inputValidation = inputValidation;
        }

        public IList<ConferenceComponent> Read()
        {
            IList<ConferenceComponent> talks = new List<ConferenceComponent>();

            using var reader = new StreamReader("./Data/Talks.txt", Encoding.UTF8);

            while (!reader.EndOfStream)
            {
                var record = reader.ReadLine();

                if (!string.IsNullOrEmpty(record))
                {
                    var indexLastSpace = record.LastIndexOf(' ');
                    var name = record.Substring(0, indexLastSpace);
                    var durationString = record.Substring(indexLastSpace + 1);

                    if (_inputValidation.IsValidTalkTitle(name) &&
                        _inputValidation.IsValidTalkDuration(
                            durationString == _specialLength.Name
                                ? _specialLength.Name
                                : durationString.Substring(0, durationString.Length - 3),
                            out var duration))
                        talks.Add(new ConferenceLeaf(name, duration, _specialLength));
                }
            }

            return talks;
        }
    }
}