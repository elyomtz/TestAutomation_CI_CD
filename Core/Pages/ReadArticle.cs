using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TestAutomation_CI_CD.Core.Pages
{
    public class ReadArticles : BasePage
    {
        public ReadArticles(IWebDriver driver) : base(driver)
        {
        }

        private readonly By insightsLink = By.LinkText("Insights");
        private readonly By cookiesBtn = By.Id("onetrust-accept-btn-handler");
        private readonly By arrowBtn = By.ClassName("slider__right-arrow");
        private readonly By headersSlides = By.CssSelector("div.text-ui-23 p");
        private readonly By linksBtn = By.CssSelector("a.link-with-bottom-arrow");
        private readonly By slideText = By.XPath("//div[@class='text-ui-23']//span[@class='museo-sans-light']");

        public void ClickOnInsights()
        {
            Click(insightsLink);
        }

        public int ClickCarouselArrow(int clicks)
        {
            WaitAndClick(cookiesBtn);

            int counter = 0;
            IWebElement carouselArrow = driver.FindElement(arrowBtn);

            for (int i = 0; i < clicks; i++)
            {
                carouselArrow.Click();
                counter++;
            }

            //adjust offset
            counter = counter + 3;
            counter = counter % 5;

            return counter;
        }

        public List<string> GetSlidesTexts()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            ReadOnlyCollection<IWebElement> elementsInsights = driver.FindElements(headersSlides);

            int slidesCounter = 0;
            List<string> textsSlidesInsights = new List<string>();

            foreach (var element in elementsInsights)
            {
                string text = (string)js.ExecuteScript("return arguments[0].textContent;", element);

                text = Regex.Replace(text, @"[^a-zA-Z0-9]+", "-").Trim('-');

                textsSlidesInsights.Add(text);

                slidesCounter++;

                if (slidesCounter == 5)
                {
                    break;
                }
            }

            return textsSlidesInsights;
        }

        public void ClickReadMoreBtn(int index)
        {
            IReadOnlyCollection<IWebElement> links = driver.FindElements(linksBtn);

            List<string> urls = links
                .Select(link => link.GetAttribute("href"))
                .Where(href => !string.IsNullOrEmpty(href))
                .Distinct()
                .ToList();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            string btnReadMore = urls[index];

            string xpath = $"//a[@href='{btnReadMore}']";

            js.ExecuteScript(@"var element = document.evaluate(arguments[0],document,null,XPathResult.FIRST_ORDERED_NODE_TYPE,null).singleNodeValue;if (element) {element.click();}", xpath);

        }

        public bool CompareTexts(List<string> textsSlidesInsights)
        {
            var elementsNavigate = driver.FindElements(slideText);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            foreach (var element in elementsNavigate)
            {
                string text = (string)js.ExecuteScript("return arguments[0].textContent;", element);

                text = Regex.Replace(text, @"[^a-zA-Z0-9]+", "-").Trim('-');

                foreach (var elemInsights in textsSlidesInsights)
                {

                    if (text.Contains(elemInsights) || elemInsights.Contains(text))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}