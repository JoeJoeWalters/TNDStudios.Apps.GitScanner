using System;
using System.Collections.Generic;
using System.Text;
using TNDStudios.Apps.GitScanner.Objects;

namespace TNDStudios.Apps.GitScanner.Helpers.ThreatAssessment
{
    public class CachedThreatAssessor : IThreatAssessor
    {
        public bool CanSave => true;

        public ThreatAssessment Assess(PackageReference packageReference)
        {
            throw new NotImplementedException();
        }

        public bool Save(ThreatAssessment threatAssessment)
        {
            throw new NotImplementedException();
        }
    }
}
