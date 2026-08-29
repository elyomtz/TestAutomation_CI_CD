using OpenQA.Selenium;

namespace TestAutomation_CI_CD.Core.Screenshot
{
    public class ScreenshotMaker
    {
        private readonly IWebDriver driver;

        public ScreenshotMaker(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string Capture(string testName)
        {
            var screenshotDriver = (ITakesScreenshot)driver;
            var screenshot = screenshotDriver.GetScreenshot();
            var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var filePath = Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"), fileName));

            screenshot.SaveAsFile(filePath);

            return filePath;
        }
    }
}