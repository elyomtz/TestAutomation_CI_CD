using log4net;
using OpenQA.Selenium;
using TestAutomation_CI_CD.Core.Logger;

namespace TestAutomation_CI_CD.Business
{
    public class ReadArticles
    {
        private readonly IWebDriver driver;
        private readonly Core.Pages.ReadArticles readArticles;
        private readonly ILog logger = LoggerManager.Create<FileDownload>();

        public ReadArticles(IWebDriver driver)
        {
            this.driver = driver;
            readArticles = new Core.Pages.ReadArticles(driver);
        }

        public List<string> ReadArticlesService(int clicks)
        {
            logger.Info("Navigating to EPAM website");
            readArticles.NavigateToEpam();
            logger.Info("Clicking the Insights button");
            readArticles.ClickOnInsights();
            logger.Info("Clicking on the carousel arrows");
            int result = readArticles.ClickCarouselArrow(clicks);
            List<string> slidesText = readArticles.GetSlidesTexts();
            logger.Info("Clicking the Read More button");
            readArticles.ClickReadMoreBtn(result);
            return slidesText;
        }

        public bool CompareTextsService(List<string> textsSlidesInsights)
        {
            logger.Info("Comparing if the Article contains the text that was displayed on the carousel slide");
            return readArticles.CompareTexts(textsSlidesInsights);
        }
    }
}
