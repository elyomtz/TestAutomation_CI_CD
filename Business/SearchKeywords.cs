using log4net;
using OpenQA.Selenium;
using System.Xml.Linq;
using TestAutomation_CI_CD.Core.Logger;
using TestAutomation_CI_CD.Core.Screenshot;
using TestAutomation_CI_CD.Tests;

namespace TestAutomation_CI_CD.Business
{
    public class SearchKeywords
    {
        private readonly IWebDriver driver;
        private readonly Core.Pages.SearchKeywords searchKeywords;
        private readonly ILog logger = LoggerManager.Create<FileDownload>();

        protected ScreenshotMaker ScreenshotMaker = null!;
        protected ILog Logger = null!;

        public SearchKeywords(IWebDriver driver)
        {
            this.driver = driver;
            searchKeywords = new Core.Pages.SearchKeywords(driver);
            ScreenshotMaker = new ScreenshotMaker(driver);
            Logger = LoggerManager.Create<BaseTest>();
        }

        public void SearchKeywordsService(string keyword1, string keyword2, string keyword3)
        {
            try
            {
                logger.Info("Navigating to EPAM website");
                searchKeywords.NavigateToEpam();
                logger.Info($"Searching for keywords {keyword1}, {keyword2}, {keyword3}");
                searchKeywords.GlobalSearch(driver, keyword1, keyword2, keyword3);
            }
            catch
            {
                var screenshot = ScreenshotMaker.Capture("SearchKeywordsService");
                Logger.Error($"Failure screenshot: {screenshot}");
            }
        }

        public bool ValidateResultsService(string keyword1, string keyword2, string keyword3)
        {
            try
            {
                logger.Info("Verifying if keywords are found in the search results");
                return searchKeywords.ValidateResults(driver, keyword1, keyword2, keyword3);
            }
            catch
            {
                var screenshot = ScreenshotMaker.Capture("SearchKeywordsService");
                Logger.Error($"Failure screenshot: {screenshot}");
                return false;
            }
        }
    }
}