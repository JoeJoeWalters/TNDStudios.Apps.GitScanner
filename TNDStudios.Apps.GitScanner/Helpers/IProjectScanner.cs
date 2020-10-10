using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Apps.GitScanner.Helpers
{
    public class ProjectScannerResult
    {
        public List<PackageReference> Packages { get; set; }
    }

    public class PackageReference
    {
        public string Include { get; set; }
        public Version Version { get; set; }
    }

    public interface IProjectScanner
    {
        public ProjectScannerResult Scan(string content);
    }
}
