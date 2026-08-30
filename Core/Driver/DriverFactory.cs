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
            var headless = ConfigurationManager.Settings.Headless;

            var chromeOptions = new ChromeOptions();
            if (headless)
            {
                chromeOptions.AddArgument("--headless=new");
                chromeOptions.AddArgument("--window-size=1920,1080");
            }
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadFolder);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);

            var edgeOptions = new EdgeOptions();
            if (headless)
            {
                edgeOptions.AddArgument("--headless=new");
                edgeOptions.AddArgument("--window-size=1920,1080");
            }
            edgeOptions.AddUserProfilePreference("download.default_directory", downloadFolder);
            edgeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            edgeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            edgeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
            edgeOptions.AddUserProfilePreference("safebrowsing.enabled", true);

            var firefoxOptions = new FirefoxOptions();
            if (headless)
            {
                firefoxOptions.AddArgument("--headless");
                firefoxOptions.AddArgument("--width=1920");
                firefoxOptions.AddArgument("--height=1080");
            }
            firefoxOptions.SetPreference("browser.download.folderList", 2);
            firefoxOptions.SetPreference("browser.download.dir", downloadFolder);
            firefoxOptions.SetPreference("browser.download.useDownloadDir", true);
            firefoxOptions.SetPreference("browser.download.manager.showWhenStarting", false);
            firefoxOptions.SetPreference("pdfjs.disabled", true);
            firefoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk","application/pdf");

            driver = ConfigurationManager.Settings.Browser switch
            {
                BrowserType.Chrome => new ChromeDriver(chromeOptions),
                BrowserType.Edge => new EdgeDriver(edgeOptions),
                BrowserType.Firefox => new FirefoxDriver(firefoxOptions),
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
