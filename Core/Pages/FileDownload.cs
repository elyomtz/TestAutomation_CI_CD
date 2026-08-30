using log4net;
using OpenQA.Selenium;
using TestAutomation_CI_CD.Core.Configuration;
using TestAutomation_CI_CD.Core.Logger;

namespace TestAutomation_CI_CD.Core.Pages
{
    public class FileDownload : BasePage
    {
        public FileDownload(IWebDriver driver) : base(driver)
        {
        }

        private readonly By fileLink = By.XPath("//a[contains(@href,'code-of-')]");
        private readonly By cookiesBtn = By.Id("onetrust-accept-btn-handler");
        private readonly ILog logger = LoggerManager.Create<FileDownload>();

        public void DownloadFile(IWebDriver driver)
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            WaitAndClick(cookiesBtn);
            WaitAndClick(fileLink);
            WaitAndClick(fileLink);
        }

        public void WaitForFile()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            var fileName = ConfigurationManager.Settings.FileName;
            var timeout = ConfigurationManager.Settings.TimeoutSeconds;

            string finalPath = Path.Combine(folder, fileName);
            string tempPath = finalPath + ".crdownload";

            DateTime end = DateTime.Now.Add(TimeSpan.FromSeconds(timeout));

            while (DateTime.Now < end)
            {
                if (File.Exists(finalPath) && !File.Exists(tempPath))
                    return;

                Thread.Sleep(500);
            }

            throw new TimeoutException($"File '{fileName}' was not downloaded.");
        }

        public bool FileExists()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            var fileName = ConfigurationManager.Settings.FileName;
            var result = File.Exists(Path.Combine(folder, fileName));
            if (!result)
            {
                logger.Info($"File '{fileName}' was not downloaded.");
                return false;
            }
            return true;
        }

    }
}