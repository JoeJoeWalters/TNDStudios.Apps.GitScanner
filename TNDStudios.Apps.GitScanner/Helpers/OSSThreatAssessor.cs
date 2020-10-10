using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Apps.GitScanner.Helpers
{
    public class OSSThreatAssessor : IThreatAssessor
    {
        private readonly string _userName;
        private readonly string _token;

        public OSSThreatAssessor(string userName, string token) 
        {
            _userName = userName;
            _token = token;
        }

        public ThreatAssessment Assess(PackageReference packageReference)
        {
            return new ThreatAssessment() { };
        }
    }
}
