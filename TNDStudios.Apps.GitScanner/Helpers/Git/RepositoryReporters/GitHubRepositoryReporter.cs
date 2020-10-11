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
    public class GitHubRepository
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("private")]
        public bool Private { get; set; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("fork")]
        public bool Fork { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime Created { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime Updated { get; set; }

        [JsonPropertyName("pushed_at")]
        public DateTime Pushed { get; set; }

        [JsonPropertyName("default_branch")]
        public DateTime DefaultBranch { get; set; }
    }

    public class GitHubRepositoryReporter : IGitRepositoryReporter
    {
        private readonly string _userName;
        private readonly string _password;

        public GitHubRepositoryReporter(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public List<string> List()
        {
            IGitHubClient client = 
                new GitHubClient(
                    new ProductHeaderValue("TNDStudios.Apps.GitScanner")) 
                { 
                    Credentials = new Credentials(_userName, _password) 
                };

            return client.Repository.GetAllForUser(_userName).Result.Select(repo => repo.CloneUrl).ToList();
        }
    }
}
