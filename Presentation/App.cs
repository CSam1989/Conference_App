using System;
using System.Collections.Generic;
using System.Text;
using Application;
using Application.Common.Factories;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Input;
using Microsoft.Extensions.Configuration;

namespace Presentation
{
    public class App
    {
        private readonly IConfiguration _config;
        private readonly IInputFactory _inputFactory;

        public App(
            IConfiguration config,
            IInputFactory inputFactory)
        {
            _config = config;
            _inputFactory = inputFactory;
        }

        // Equivalent to Main in Program.cs
        public void Run()
        {

            string inputStyle;
            do
            {
                Console.Write("Choose your input (\"Manual\" or \"File\"): ");
                inputStyle = Console.ReadLine();
            } while (inputStyle != null && (inputStyle.ToLower() != "manual" && inputStyle.ToLower() != "file"));

            var inputStrategy = _inputFactory.GetInputStrategy(inputStyle);

            var allTalks = inputStrategy.Read();

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
