using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TestAutomation_CI_CD.Core.Configuration;

namespace TestAutomation_CI_CD.Core.Driver
{
    public static class DriverFactory
    {
        private static IWebDriver? driver;

        public static IWebDriver InitDriver()
        {
            var downloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadFolder);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);

            driver = ConfigurationManager.Settings.Browser switch
            {
                BrowserType.Chrome => new ChromeDriver(chromeOptions),
                BrowserType.Edge => new EdgeDriver(),
                BrowserType.Firefox => new FirefoxDriver(),
                _ => throw new ArgumentException($"Unsupported browser: {ConfigurationManager.Settings.Browser}")
            };

            driver.Manage().Window.Maximize();
            return driver;
        }

        public static void QuitDriver()
        {
            driver?.Quit();
        }

    }
}
