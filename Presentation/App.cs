using System;
using System.Collections.Generic;
using System.Text;
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
            Console.WriteLine("Hello World!");
        }
    }
}
