using System;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Conference.Builder;

namespace Presentation
{
    public class App
    {
        private readonly IInputFactory _inputFactory;
        private readonly Sessions _sessions;
        private readonly SpecialLengthSettings _specialLength;
        private readonly ITimeService _timeService;
        private readonly ITrackService _trackService;

        public App(
            ITrackService trackService,
            ITimeService timeService,
            IInputFactory inputFactory,
            SpecialLengthSettings specialLength,
            Sessions sessions)
        {
            _trackService = trackService;
            _timeService = timeService;
            _inputFactory = inputFactory;
            _specialLength = specialLength;
            _sessions = sessions;
        }

        // Equivalent to Main in Program.cs
        public void Run()
        {
            //Adds global exception handling
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            string inputStyle;
            do
            {
                Console.Write("Choose your input (\"Manual\" or \"File\"): ");
                inputStyle = Console.ReadLine();
            } while (inputStyle != null && inputStyle.ToLower() != "manual" && inputStyle.ToLower() != "file");

            var inputStrategy = _inputFactory.GetInputStrategy(inputStyle);

            var allTalks = inputStrategy.Read();

            var builder = new ConferenceBuilder(allTalks, _trackService, _sessions);
            var conference = builder.BuildConference();

            Console.WriteLine(conference.Print());
        }

        private void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}