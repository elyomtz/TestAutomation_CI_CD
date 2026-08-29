using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestAutomation_CI_CD.Core.Logger;


namespace TestAutomation_CI_CD.Core.Pages
{
    public class SearchRemoteJobs : BasePage
    {
        public SearchRemoteJobs(IWebDriver driver) : base(driver)
        {
        }

        private readonly By careers = By.LinkText("Careers");
        private readonly By cookiesBtn = By.Id("onetrust-accept-btn-handler");
        private readonly By searchBtn = By.CssSelector(".pinned-button-text");
        private readonly By countryDropdown = By.Id("react-select-2-input");
        private readonly By searchField = By.Name("search");
        private readonly By remoteCheckbox = By.ClassName("Checkbox_labelElement___nzU3");
        private readonly By submitSearchButton = By.Name("submit_search_box_button");
        private readonly By resultElements = By.CssSelector("[class*='JobCard_labelLink']");
        private readonly By headerElement = By.TagName("h1");
        private readonly By jobDetails = By.CssSelector("[class*='JobDetails_firstSkill']");
        private readonly ILog logger = LoggerManager.Create<SearchRemoteJobs>();

        public void ClickOnCareers()
        {
            Click(careers);
        }

        public void StartSearch()
        {
            WaitAndClick(cookiesBtn);
            driver.FindElement(searchBtn).Click();
        }

        public void SearchProgrammingLanguage(string programmingLanguage)
        {
            WaitAndClick(cookiesBtn);
            Click(searchField);
            EnterText(searchField, programmingLanguage);
        }

        public bool FindCountry(string country)
        {
            try
            {
                var input = driver.FindElement(countryDropdown);
                new Actions(driver)
                .Click(input)
                .SendKeys(country)
                .SendKeys(Keys.Enter)
                .Perform();
            }
            catch (Exception ex)
            {
                logger.Error($"Country not found, error: " + ex.Message);
                return false;
            }
            return true;
        }

        public bool SelectRemote()
        {
            try
            {
                WaitAndClick(remoteCheckbox);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SearchJob()
        {
            WaitAndClick(submitSearchButton);
        }
        public bool ClickLastElement()
        {
            IReadOnlyCollection<IWebElement> resultLanguage = WaitForElementsVisible(resultElements);

            if (resultLanguage.Count > 0)
            {
                int attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        //Find the latest element in the list of results
                        resultLanguage = WaitForElementsVisible(resultElements);
                        resultLanguage.Last().Click();
                        break;
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException)
                    {
                        attempts++;
                    }
                }
            }
            else
            {
                logger.Error($"Programming language not found");
                return false;
            }

            return true;
        }

        public bool FindTextFromSearch(string programmingLanguage)
        {

            WaitUntilVisible(headerElement);
            string textH1 = driver.FindElement(headerElement).Text.ToUpper();

            WaitUntilVisible(jobDetails);
            string textJobDetails = driver.FindElement(headerElement).Text.ToUpper();

            if (textH1.Contains(programmingLanguage.ToUpper()) || textJobDetails.Contains(programmingLanguage.ToUpper()))
            {
                logger.Info($"Remote job for {programmingLanguage} found");
                return true;
            }
            else
            {
                logger.Error($"Remote job for {programmingLanguage} not found in the search result");
                return false;
            }
        }
    }
}