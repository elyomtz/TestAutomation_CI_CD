using log4net;
using OpenQA.Selenium;
using TestAutomation_CI_CD.Core.Driver;
using TestAutomation_CI_CD.Core.Logger;
using TestAutomation_CI_CD.Core.Screenshot;

namespace TestAutomation_CI_CD.Tests
{
    public class BaseTest
    {
        protected ILog Logger = null!;
        protected IWebDriver driver = null!;
        protected ScreenshotMaker ScreenshotMaker = null!;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.InitDriver();

            Logger = LoggerManager.Create<BaseTest>();

            Logger.Info($"Starting test: {TestContext.CurrentContext.Test.Name}");

            ScreenshotMaker = new ScreenshotMaker(driver);
        }


        [TearDown]
        public void TearDown()
        {
            var testName = TestContext.CurrentContext.Test.Name;

            var result = TestContext.CurrentContext.Result.Outcome.Status;

            Logger.Info($"Test '{testName}' finished with result: {result}");

            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                int index = testName.IndexOf('(');
                string name = index >= 0 ? testName.Substring(0, index) : testName;
                var screenshot = ScreenshotMaker.Capture(name);

                Logger.Error($"Failure screenshot: {screenshot}");
            }

            driver.Close();
            driver?.Dispose();
        }
    }
}
