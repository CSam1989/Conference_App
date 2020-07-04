﻿using System;
using System.Collections.Generic;
using System.Text;
using Application;
using Application.Common.Factories;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Conference.Builder;
using Application.Conference.Composite;
using Application.Input;
using Microsoft.Extensions.Configuration;

namespace Presentation
{
    public class App
    {
        private readonly ITrackService _trackService;
        private readonly IInputFactory _inputFactory;
        private readonly Sessions _sessions;

        public App(
            ITrackService trackService,
            IInputFactory inputFactory,
            Sessions sessions)
        {
            _trackService = trackService;
            _inputFactory = inputFactory;
            _sessions = sessions;
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

            var builder = new ConferenceBuilder(allTalks, _trackService, _sessions);
            var conference = builder.BuildConference();

            Console.WriteLine(conference.Print());
            
        }
    }
}
