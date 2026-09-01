using TestAutomation_CI_CD.Business;

namespace TestAutomation_CI_CD.Tests
{
    public class SearchJobsTest : BaseTest
    {
        [TestCase("java", "Argentina")]
        [Category("Unit")]
        public void TestEpam_SearchRemoteJob(string programmingLanguage, string country)
        {
            SearchRemoteJobs searchRemoteJobs = new SearchRemoteJobs(driver);
            searchRemoteJobs.SearchRemoteJobsService(country, programmingLanguage);
            var result = searchRemoteJobs.VerifySearchRemoteJobsResultsService(programmingLanguage);
            Assert.IsTrue(result);
        }
    }
}