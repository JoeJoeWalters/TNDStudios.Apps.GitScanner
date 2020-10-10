using System.Collections.Generic;

namespace TNDStudios.Apps.GitScanner.Helpers
{
    public interface IGitHelper
    {
        void Connect(string userName, string password);
        void Clone(string remote, string localPath, bool overwrite);
        List<string> History(string localPath);
    }
}
