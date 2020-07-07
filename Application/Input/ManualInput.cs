using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Conference.Composite;

namespace Application.Input
{
    public class ManualInput: IInputStrategy
    {
        private readonly SpecialLengthSettings _specialLength;
        private readonly IInputValidationService _inputValidation;

        public ManualInput(SpecialLengthSettings specialLength, IInputValidationService inputValidation)
        {
            _specialLength = specialLength;
            _inputValidation = inputValidation;
        }

        public IList<ConferenceComponent> Read()
        {
            IList<ConferenceComponent> talks = new List<ConferenceComponent>();
            string name;
            int duration;

            name = ReadName("Title of next talk? (\"S\" = Stop)");

            while (name.ToUpper() != "S")
            {
                duration = ReadDuration(
                    $"Length of talk in minutes? (\"{_specialLength.Name}\" = {_specialLength.Length}min)");
                talks.Add(new ConferenceLeaf(name, duration, _specialLength));

                name = ReadName("Title of next talk? (\"S\" = Stop)");
            }

            return talks;
        }

        private string ReadName(string question)
        {
            string output;

            do
            {
                Console.Write($"{question}: ");
                output = Console.ReadLine();
            } while (!_inputValidation.IsValidTalkTitle(output));

            return output;
        }

        private int ReadDuration(string question)
        {
            string input;
            int output;

            do
            {
                Console.Write($"{question}: ");
                input = Console.ReadLine();
            } while (!_inputValidation.IsValidTalkDuration(input, out output));

            return output;
        }
    }
}
