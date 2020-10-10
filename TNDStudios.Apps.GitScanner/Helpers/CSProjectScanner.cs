using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TNDStudios.Apps.GitScanner.Helpers
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
