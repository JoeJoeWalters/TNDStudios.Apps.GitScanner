using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Apps.GitScanner.Helpers.Git
{
    public class GitHubRepositoryReporter : IGitRepositoryReporter
    {
        // https://api.github.com/users/tndstudios/repos
        public List<string> List(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
