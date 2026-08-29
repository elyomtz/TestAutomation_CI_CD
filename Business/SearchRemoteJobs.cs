using log4net;
using OpenQA.Selenium;
using TestAutomation_CI_CD.Core.Logger;

namespace TestAutomation_CI_CD.Business
{
    public class SearchRemoteJobs
    {
        private readonly IWebDriver driver;
        private readonly Core.Pages.SearchRemoteJobs searchRemoteJobs;
        private readonly ILog logger = LoggerManager.Create<FileDownload>();

        public SearchRemoteJobs(IWebDriver driver)
        {
            this.driver = driver;
            searchRemoteJobs = new Core.Pages.SearchRemoteJobs(driver);
        }

        public bool SearchRemoteJobsService(string country, string programmingLanguage)
        {
            logger.Info("Navigating to EPAM website");
            searchRemoteJobs.NavigateToEpam();
            logger.Info("Click on Careers");
            searchRemoteJobs.ClickOnCareers();
            searchRemoteJobs.StartSearch();
            logger.Info($"Searching for {programmingLanguage} in {country}");
            searchRemoteJobs.SearchProgrammingLanguage(programmingLanguage);
            searchRemoteJobs.FindCountry(country);
            logger.Info("Selecting remote job");
            searchRemoteJobs.SelectRemote();
            searchRemoteJobs.SearchJob();
            logger.Info("Getting the results for the search options");
            return searchRemoteJobs.ClickLastElement();
        }

        public bool VerifySearchRemoteJobsResultsService(string programmingLanguage)
        {
            logger.Info($"Verifying if the remote job contains: {programmingLanguage}");
            return searchRemoteJobs.FindTextFromSearch(programmingLanguage);
        }
    }
}
