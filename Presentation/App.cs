using System;
using System.Collections.Generic;
using System.Text;
using Application;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Microsoft.Extensions.Configuration;

namespace Presentation
{
    public class App
    {
        private readonly IConfiguration _config;
        private readonly IInputStrategy _input;

        public App(
            IConfiguration config,
            IInputStrategy input)
        {
            _config = config;
            _input = input;
        }

        // Equivalent to Main in Program.cs
        public void Run()
        {
            IList<Talk> talks = _input.Read();

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
