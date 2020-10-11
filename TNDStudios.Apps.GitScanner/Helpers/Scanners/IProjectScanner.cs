using System.Collections.Generic;
using TNDStudios.Apps.GitScanner.Objects;

namespace TNDStudios.Apps.GitScanner.Helpers.Scanners
{
    public class ProjectScannerResult
    {
        public List<PackageReference> Packages { get; set; }
    }

    public interface IProjectScanner
    {
        public ProjectScannerResult Scan(string content);
    }
}
