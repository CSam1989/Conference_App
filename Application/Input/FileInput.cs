using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Microsoft.Extensions.Configuration;

namespace Application.Input
{
    public class FileInput : IInputStrategy
    {
        private readonly SpecialLengthSettings _specialLength;

        public FileInput(SpecialLengthSettings specialLength)
        {
            _specialLength = specialLength;
        }

        public IList<Talk> Read()
        {
            IList<Talk> talks = new List<Talk>();

            using var reader = new StreamReader("./Data/Talks.txt", Encoding.UTF8);

            while (!reader.EndOfStream)
            {
                var record = reader.ReadLine();

                if (!string.IsNullOrEmpty(record))
                {
                    var indexLastSpace = record.LastIndexOf(' ');
                    var name = record.Substring(0, indexLastSpace);
                    var durationString = record.Substring(indexLastSpace + 1);

                    var duration = durationString == _specialLength.Name 
                        ? _specialLength.Length 
                        : int.Parse(durationString.Substring(0, durationString.Length - 3));

                    talks.Add(new Talk
                    {
                        Name = name,
                        Duration = duration
                    });
                }
            }

            return talks;
        }
    }
}
