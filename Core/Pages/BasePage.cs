using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestAutomation_CI_CD.Core.Configuration;

namespace TestAutomation_CI_CD.Core.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToEpam()
        {
            var url = ConfigurationManager.Settings.Url;
            driver.Navigate().GoToUrl(url);
        }

        public IWebElement WaitUntilVisible(By locator)
        {
            return wait.Until(driver =>
            {
                var element = driver.FindElement(locator);

                return element.Displayed
                    ? element
                    : null;
            })!;
        }

        public IReadOnlyCollection<IWebElement> WaitForElementsVisible(By locator)
        {
            return wait.Until(d =>
            {
                var elements = d.FindElements(locator);

                return elements.Count > 0 && elements.All(e => e.Displayed)
                    ? elements
                    : null;
            });
        }

        public IWebElement WaitUntilClickable(By locator)
        {
            return wait.Until(driver =>
            {
                var element = driver.FindElement(locator);

                return element.Displayed && element.Enabled
                    ? element
                    : null;
            })!;
        }

        protected void EnterText(By locator, string text)
        {
            try
            {
                var element = WaitUntilVisible(locator);
                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to enter text in: {locator}", ex);
            }
        }

        protected void Click(By locator)
        {
            try
            {
                WaitUntilClickable(locator).Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to click element: {locator}", ex);
            }
        }

        protected void WaitAndClick(By locator)
        {
            int attempts = 0;
            var explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromSeconds(0.25)
            };

            while (attempts < 3)
            {
                try
                {
                    IWebElement elemToFound = explicitWait.Until(driver =>
                    {
                        var elem = driver.FindElement(locator);
                        return elem.Displayed ? elem : null;
                    });
                    elemToFound.Click();
                    break;
                }
                catch
                {
                    attempts++;
                }
            }
        }
    }
}