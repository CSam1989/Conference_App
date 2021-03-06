﻿using System.IO;
using Application.Common.Factories;
using Application.Common.Interfaces;
using Application.Common.Services;
using Application.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    // Found on https://github.com/Ballcapz/Console-Application-Dependency-Injection
    internal class Program
    {
        // Will now be used for setup & IOC Container
        private static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            // Calls the Run method in App, which is replacing Main
            serviceProvider.GetService<App>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);

            //Add Settings to IOC Container
            var specialLength = config.GetSection("ApplicationConstants:SpecialTalkLength")
                .Get<SpecialLengthSettings>();
            services.AddSingleton(specialLength);

            var sessionSettings = config.GetSection("ApplicationConstants:Sessions").GetChildren();
            var sessions = new Sessions();

            foreach (var session in sessionSettings)
            {
                sessions.SessionList.Add(session.Get<SessionSettings>());
                ;
            }

            services.AddSingleton(sessions);

            //Add Services To IOC Container
            services.AddSingleton<IInputFactory, InputFactory>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<IInputValidationService, InputValidationService>();

            services.AddTransient<App>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }
    }
}