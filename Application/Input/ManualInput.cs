using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Settings;

namespace Application.Input
{
    public class ManualInput: IInputStrategy
    {
        private readonly SpecialLengthSettings _specialLength;

        public ManualInput(SpecialLengthSettings specialLength)
        {
            _specialLength = specialLength;
        }

        public IList<Talk> Read()
        {
            IList<Talk> talks = new List<Talk>();
            string name;
            int duration;

            name = ReadName("Title of next talk? (\"S\" = Stop)");

            while (name.ToUpper() != "S")
            {
                duration = ReadDuration(
                    $"Length of talk in minutes? (\"{_specialLength.Name}\" = {_specialLength.Length}min)");
                talks.Add(new Talk()
                {
                    Name = name,
                    Duration = duration
                });

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
            } while (string.IsNullOrWhiteSpace(output) || output.Any(char.IsDigit));

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

                if (input != null && input.ToLower() == _specialLength.Name)
                    return _specialLength.Length;
            } while (!int.TryParse(input, out output) || output < 0);

            return output;
        }
    }
}
