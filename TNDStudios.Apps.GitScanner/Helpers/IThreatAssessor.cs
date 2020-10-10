using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Apps.GitScanner.Helpers
{
    public class ThreatAssessment
    {

    }

    public interface IThreatAssessor
    {
        ThreatAssessment Assess(PackageReference packageReference);
    }
}
