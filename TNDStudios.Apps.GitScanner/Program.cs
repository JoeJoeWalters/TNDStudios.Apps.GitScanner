using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using TNDStudios.Apps.GitScanner.Helpers;

namespace TNDStudios.Apps.GitScanner
{
    class Program
    {
        static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            // DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IConfiguration>(configuration)
                .AddTransient<IGitHelper, Lib2GitHelper>()
                .BuildServiceProvider();

            string userName = configuration["Git::UserName"];
            string password = configuration["Git::Password"];
            String[] repositoriesToScan = { "https://github.com/TNDStudios/TNDStudios.Azure.FunctionApp.git" };

            IGitHelper gitHelper = serviceProvider.GetRequiredService<IGitHelper>();
            gitHelper.Connect(userName, password);

            foreach(string repositoryUrl in repositoriesToScan)
            {
                gitHelper.Clone(repositoryUrl, @"c:\temp");
            }
        }
    }
}
