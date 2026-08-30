using log4net;
using OpenQA.Selenium;
using TestAutomation_CI_CD.Core.Logger;

namespace TestAutomation_CI_CD.Business
{
    public class FileDownload
    {
        private readonly IWebDriver driver;
        private readonly Core.Pages.FileDownload fileDownload;
        private readonly ILog logger = LoggerManager.Create<FileDownload>();

        public FileDownload(IWebDriver driver)
        {
            this.driver = driver;
            fileDownload = new Core.Pages.FileDownload(driver);
        }

        public void FileDownloadService()
        {
            logger.Info("Navigating to EPAM website");
            fileDownload.NavigateToEpam();
            logger.Info("Downloading file");
            fileDownload.DownloadFile(driver);
        }

        public void WaitForFileService()
        {
            logger.Info("Waiting for file to be downloaded");
            fileDownload.WaitForFile();
        }

        public bool FileExistsService()
        {
            logger.Info("Checking if file has been downloaded");
            return fileDownload.FileExists();
        }

    }
}
