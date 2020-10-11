using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TNDStudios.Apps.GitScanner.Helpers
{
    public class OSSComponentReportRequest
    {
        public string[] coordinates { get; set; }
    }

    public class OSSComponentReport
    {
        /// <summary>
        /// Description of the package
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Targeted coordinates for this package
        /// </summary>
        [JsonPropertyName("coordinates")]
        public string Coordinates { get; set; }

        /// <summary>
        /// Component Details Reference
        /// </summary>
        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        [JsonPropertyName("vulnerabilities")] public OSSComponentReportVulnerability[] Vulnerabilities { get; set; }
    }

    /// <summary>
    /// Vulnerability report for a given package
    /// </summary>
    public class OSSComponentReportVulnerability
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Vulnerability Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Vulnerability Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// CVSS score
        /// </summary>
        [JsonPropertyName("cvssScore")]
        public float CvssScore { get; set; }

        /// <summary>
        /// CVSS vector
        /// </summary>
        [JsonPropertyName("cvssVector")]
        public string CvssVector { get; set; }

        /// <summary>
        /// Common Weakness Enumeration
        /// </summary>
        /// <returns></returns>
        [JsonPropertyName("cwe")]
        public string Cwe { get; set; }

        /// <summary>
        /// Common Vulnerability Enumeration
        /// </summary>
        [JsonPropertyName("cve")]
        public string Cve { get; set; }

        /// <summary>
        /// Vulnerability details reference
        /// </summary>
        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        /// <summary>
        /// Affected version ranges
        /// </summary>
        [JsonPropertyName("versionRanges")]
        public string[] VersionRanges { get; set; }

    }

    public class OSSThreatAssessor : IThreatAssessor
    {
        private readonly string _userName;
        private readonly string _token;

        private const string _sonaTypeUri = "https://ossindex.sonatype.org/api/v3/component-report";
        private const string _userAgentString = "TNDStudios.Apps.GitScanner/0.0.1";

        private const string _responseContentType = "application/vnd.ossindex.component-report.v1+json";
        private const string _requestContentType = "application/vnd.ossindex.component-report-request.v1+json";

        public OSSThreatAssessor(string userName, string token)
        {
            _userName = userName;
            _token = token;
        }

        public ThreatAssessment Assess(PackageReference packageReference)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgentString);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_responseContentType));
                if (!string.IsNullOrWhiteSpace(_userName) && !string.IsNullOrWhiteSpace(_token))
                {
                    var authToken = Encoding.ASCII.GetBytes($"{_userName}:{_token}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(authToken));
                }

                String packageUrl = String.Empty;
                switch (packageReference.Type)
                {
                    case PackageReferenceType.Nuget:
                        packageUrl = $@"pkg:nuget/{packageReference.Include}@{packageReference.Version}";
                        break;
                }

                var response = client.GetStringAsync($"{_sonaTypeUri}/{packageUrl}").Result;

                OSSComponentReport componentReport = JsonSerializer.Deserialize<OSSComponentReport>(response, new JsonSerializerOptions());
            }
            return new ThreatAssessment() { };
        }
    }
}
