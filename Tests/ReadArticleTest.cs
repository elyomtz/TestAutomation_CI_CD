using TestAutomation_CI_CD.Business;

namespace TestAutomation_CI_CD.Tests
{
    public class ReadArticleTest : BaseTest
    {
        [TestCase(3)]
        [Category("Unit")]
        public void TestEpam_ValidateTitleInsights(int clicks)
        {
            ReadArticles readArticles = new ReadArticles(driver);
            List<string> result = readArticles.ReadArticlesService(clicks);
            var testResult = readArticles.CompareTextsService(result);
            Assert.IsTrue(testResult);
        }
    }
}