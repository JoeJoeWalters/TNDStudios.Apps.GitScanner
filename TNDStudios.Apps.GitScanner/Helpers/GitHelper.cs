using System;
using System.Collections.Generic;
using System.Text;
using LibGit2Sharp;

namespace TNDStudios.Apps.GitScanner.Helpers
{
    public interface IGitHelper
    {
        void Connect(string userName, string password);
        void Clone(string remote, string localPath);
    }

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
        public void Clone(string remote, string localPath)
        {
            var co = new CloneOptions();
            co.CredentialsProvider = (_url, _user, _cred) => _credentials;
            Repository.Clone(remote, localPath, co);
        }

        ~Lib2GitHelper()
        {

        }
    }
}
