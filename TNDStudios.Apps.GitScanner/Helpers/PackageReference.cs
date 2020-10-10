using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Apps.GitScanner.Helpers
{
    public enum PackageReferenceType : Int16
    {
        Unknown = 0,
        Nuget = 1,
    }

    public class PackageReference
    {
        public PackageReferenceType Type { get; set; }
        public string Include { get; set; }
        public Version Version { get; set; }
    }
}
