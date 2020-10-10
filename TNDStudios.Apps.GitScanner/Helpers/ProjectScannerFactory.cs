using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TNDStudios.Apps.GitScanner.Helpers
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
