using System;
using System.Collections.Generic;
using System.Text;
using TNDStudios.Apps.GitScanner.Objects;

namespace TNDStudios.Apps.GitScanner.Helpers.ThreatAssessment
{
    public class ThreatAssessment
    {

    }

    public interface IThreatAssessor
    {
        bool CanSave { get; }

        ThreatAssessment Assess(PackageReference packageReference);
        bool Save(ThreatAssessment threatAssessment);
    }
}
