using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Settings;
using Microsoft.Extensions.Configuration;

namespace Presentation
{
    public class App
    {
        private readonly IConfiguration _config;

        public App(
            IConfiguration config)
        {
            _config = config;
        }

        // Equivalent to Main in Program.cs
        public void Run()
        {
            var specialLength = _config.GetSection("ApplicationConstants:SpecialTalkLength").Get<SpecialLengthSettings>();

            Console.WriteLine(specialLength.Name);
            Console.WriteLine(specialLength.Length);

            var sessions = _config.GetSection("ApplicationConstants:Sessions").GetChildren();

            foreach (var session in sessions)
            {
                var test = session.Get<SessionSettings>();

                Console.WriteLine(test.Name);
                Console.WriteLine(test.MaxLength);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
