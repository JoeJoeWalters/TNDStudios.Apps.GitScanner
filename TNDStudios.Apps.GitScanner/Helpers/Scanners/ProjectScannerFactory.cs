using System;
using System.IO;

namespace TNDStudios.Apps.GitScanner.Helpers.Scanners
{
    public class ProjectScannerFactoryNotFoundException : Exception { }

    public static class ProjectScannerFactory
    {
        public static IProjectScanner Get(String fileName)
        {
            string extension = Path.GetExtension(fileName).TrimStart('.').ToLower();
            switch(extension)
            {
                case "csproj":
                    return new CSProjectScanner();                
            }

            throw new ProjectScannerFactoryNotFoundException();
        }
    }
}
