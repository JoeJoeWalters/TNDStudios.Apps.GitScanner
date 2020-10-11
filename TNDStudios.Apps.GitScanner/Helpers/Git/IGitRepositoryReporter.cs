using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Apps.GitScanner.Helpers.Git
{
    public interface IGitRepositoryReporter
    {
        List<string> List(string userName, string password);
    }
}
