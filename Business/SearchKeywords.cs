using log4net;
using OpenQA.Selenium;
using TestAutomation_CI_CD.Core.Logger;

namespace TestAutomation_CI_CD.Business
{
    public class SearchKeywords
    {
        private readonly IWebDriver driver;
        private readonly Core.Pages.SearchKeywords searchKeywords;
        private readonly ILog logger = LoggerManager.Create<FileDownload>();

        public SearchKeywords(IWebDriver driver)
        {
            this.driver = driver;
            searchKeywords = new Core.Pages.SearchKeywords(driver);
        }

        public void SearchKeywordsService(string keyword1, string keyword2, string keyword3)
        {
            logger.Info("Navigating to EPAM website");
            searchKeywords.NavigateToEpam();
            logger.Info($"Searching for keywords {keyword1}, {keyword2}, {keyword3}");
            searchKeywords.GlobalSearch(driver, keyword1, keyword2, keyword3);
        }

        public bool ValidateResultsService(string keyword1, string keyword2, string keyword3)
        {
            logger.Info("Verifying if keywords are found in the search results");
            return searchKeywords.ValidateResults(driver, keyword1, keyword2, keyword3);
        }
    }
}