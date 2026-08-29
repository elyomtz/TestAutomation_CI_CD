using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestAutomation_CI_CD.Core.Logger;

namespace TestAutomation_CI_CD.Core.Pages
{
    public class SearchKeywords : BasePage
    {
        public SearchKeywords(IWebDriver driver) : base(driver)
        {
        }

        private readonly By searchIconElement = By.ClassName("search-icon");
        private readonly By searchInputElement = By.Name("q");
        private readonly By findButtonElement = By.XPath(".//*[@class='search-results__input-holder']/following-sibling::button");
        private readonly By resultsElements = By.ClassName("search-results__item");
        private readonly ILog logger = LoggerManager.Create<SearchKeywords>();

        public void GlobalSearch(IWebDriver driver, string keyword1, string keyword2, string keyword3)
        {
            Click(searchIconElement);

            var searchInput = WaitUntilVisible(searchInputElement);

            var clickAndSendKeysActions = new Actions(driver);
            clickAndSendKeysActions.Click(searchInput)
                .Pause(TimeSpan.FromSeconds(1))
                .SendKeys(keyword1 + "/" + keyword2 + "/" + keyword3)
                .Perform();
            //Click “Find” button
            Click(findButtonElement);
        }

        public bool ValidateResults(IWebDriver driver, string keyword1, string keyword2, string keyword3)
        {
            // Find all search result links
            var results = driver.FindElements(resultsElements);

            string[] keywords = { keyword1, keyword2, keyword3 };

            // Validate that all links in a list contain a word “BLOCKCHAIN”/”Cloud”/”Automation” in the text
            bool allResultsValid = results.All(result =>
                keywords.Any(keyword =>
                    result.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0));

            if (allResultsValid)
            {
                logger.Info("All search results contain one of the required keywords.");
                return true;
            }
            else
            {
                logger.Error("Some search results do not contain the required keywords.");
                return false;
            }
        }
    }
}