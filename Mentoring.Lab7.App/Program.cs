using System;
using Mentoring.Lab7.Data;
using Microsoft.Extensions.Configuration;

namespace Mentoring.Lab7.App
{
    class Program
    {
        private const string ConfigurationFileName = "appSettings.json";

        private static readonly IConfiguration _configuration;

        static Program()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(ConfigurationFileName)
                .Build();
        }

        static void Main(string[] args)
        {
            var context = new MongoDataContext(_configuration["connectionString"]);
            var app = new App(context.Books);

            app.RunAsync().GetAwaiter().GetResult();

            Console.WriteLine("End working.");
            Console.ReadKey();
        }
    }
}
