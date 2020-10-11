using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using TNDStudios.Apps.GitScanner.Objects;

namespace TNDStudios.Apps.GitScanner.Helpers.Scanners
{
    public class CSProjectScanner : IProjectScanner
    {
        public ProjectScannerResult Scan(string content)
        {
            ProjectScannerResult result = new ProjectScannerResult();
            var doc = XDocument.Parse(content);
            result.Packages = doc.XPathSelectElements("//PackageReference")
                .Select(pr => new PackageReference
                {
                    Type = PackageReferenceType.Nuget,
                    Include = pr.Attribute("Include").Value,
                    Version = new Version(pr.Attribute("Version").Value)
                }).ToList();

            return result;
        }
    }
}
