using LibGit2Sharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TNDStudios.Apps.GitScanner.Helpers
{
    /// <summary>
    /// https://edi.wang/post/2019/3/26/operate-git-with-net-core
    /// https://github.com/libgit2/libgit2sharp/
    /// </summary>
    public class Lib2GitHelper : IGitHelper
    {
        private UsernamePasswordCredentials _credentials;

        public Lib2GitHelper()
        {
        }

        public void Connect(string userName, string password)
        { 
            _credentials = new UsernamePasswordCredentials { Username = userName, Password = password };
        }

        /// <summary>
        /// Clones a remote repository to a local folder
        /// </summary>
        /// <param name="remote">The url of the git repository</param>
        /// <param name="localPath">The local path the clone to</param>
        public void Clone(string remote, string localPath, bool overwrite)
        {
            if (overwrite)
            {
                Directory.Delete(localPath, true);
            }

            var co = new CloneOptions();
            co.CredentialsProvider = (_url, _user, _cred) => _credentials;
            Repository.Clone(remote, localPath, co);
        }

        public List<string> History(string localPath)
        {
            List<string> result = new List<string>();
            Repository repo = new Repository(localPath);
            result = repo.Commits.Select(x => x.Message).ToList();

            return result;
        }

        ~Lib2GitHelper()
        {

        }
    }
}
