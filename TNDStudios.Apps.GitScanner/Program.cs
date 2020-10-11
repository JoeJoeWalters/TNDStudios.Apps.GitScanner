using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using TNDStudios.Apps.GitScanner.Helpers;
using TNDStudios.Apps.GitScanner.Helpers.Git;
using TNDStudios.Apps.GitScanner.Helpers.Scanners;
using TNDStudios.Apps.GitScanner.Helpers.ThreatAssessment;

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

            string userName = configuration["Git::UserName"];
            string password = configuration["Git::Password"];

            // DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IGitHelper>(new Lib2GitHelper())
                .AddSingleton<IThreatAssessor>(new OSSThreatAssessor(configuration["SonaType::UserName"], configuration["SonaType::APIToken"]))
                .AddSingleton<IGitRepositoryReporter>(new GitHubRepositoryReporter(userName, password))
                .BuildServiceProvider();

            List<string> repositoriesToScan = (serviceProvider.GetRequiredService<IGitRepositoryReporter>()).List();

            IThreatAssessor threatAssessor = serviceProvider.GetRequiredService<IThreatAssessor>();
            IGitHelper gitHelper = serviceProvider.GetRequiredService<IGitHelper>();
            gitHelper.Connect(userName, password);

            foreach (string repositoryUrl in repositoriesToScan)
            {
                string localPath = @"c:\temp";
                gitHelper.Clone(repositoryUrl, localPath, true);
                List<string> commits = gitHelper.History(localPath);

                string[] projectFiles = Directory.GetFiles(localPath, "*.csproj", new EnumerationOptions() { RecurseSubdirectories = true, MatchCasing = MatchCasing.CaseInsensitive });

                foreach (string projectFile in projectFiles)
                {
                    IProjectScanner projectScanner = ProjectScannerFactory.Get(projectFile);
                    ProjectScannerResult scanResult = projectScanner.Scan(File.ReadAllText(projectFile));

                    foreach (var packageReference in scanResult.Packages)
                    {
                        //ThreatAssessment assessment = threatAssessor.Assess(packageReference);
                    }
                }

            }
        }
    }
}
