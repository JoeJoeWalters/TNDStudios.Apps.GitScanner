using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TNDStudios.Apps.GitScanner.Helpers
{
    public class GitHelperBase
    {
        /// <summary>
        /// Special implementation of Directory recusing delete that enables
        /// deletion of GIT folders where Directory.Delete("", true) would fail
        /// in it's recursive delete
        /// </summary>
        internal void DeleteDirectory(string directory)
        {
            foreach (var subDirectory in Directory.EnumerateDirectories(directory))
            {
                DeleteDirectory(subDirectory);
            }
            foreach (var file in Directory.EnumerateFiles(directory))
            {
                var fileInfo = new FileInfo(file);
                fileInfo.Attributes = FileAttributes.Normal;
                fileInfo.Delete();
            }
            Directory.Delete(directory);
        }
    }
}
