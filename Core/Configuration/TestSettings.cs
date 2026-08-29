namespace TestAutomation_CI_CD.Core.Configuration
{
    public class TestSettings
    {
        public string Url { get; set; } = string.Empty;
        public BrowserType Browser { get; set; }
        public int TimeoutSeconds { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ApiTestingUrl { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string InvalidEndpoint { get; set; } = string.Empty;

    }
}
