using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TNDStudios.Apps.GitScanner.Helpers.Git
{
    public class GitHubRepositoryReporter : IGitRepositoryReporter
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly string _token;

        public GitHubRepositoryReporter(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public GitHubRepositoryReporter(string token)
        {
            _token = token;
        }

        public List<string> List()
        {
            Credentials credentials = 
                String.IsNullOrEmpty(_token) ? 
                new Credentials(_userName, _password) : 
                new Credentials(_token);

            IGitHubClient client = 
                new GitHubClient(
                    new ProductHeaderValue("TNDStudios.Apps.GitScanner")) 
                { 
                    Credentials = credentials
                };

            return client.Repository.GetAllForCurrent().Result.Select(repo => repo.CloneUrl).ToList();
        }
    }
}
