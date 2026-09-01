using TestAutomation_CI_CD.Business;

namespace TestAutomation_CI_CD.Tests
{
    public class SearchKeywordsTest : BaseTest
    {
        [TestCase("BLOCKCHAIN", "Cloud", "Automation")]
        [Category("Unit")]
        public void TestEpam_FindKeywords(string keyword1, string keyword2, string keyword3)
        {
            SearchKeywords searchKeywords = new SearchKeywords(driver);
            searchKeywords.SearchKeywordsService(keyword1, keyword2, keyword3);
            var result = searchKeywords.ValidateResultsService(keyword1, keyword2, keyword3);
            Assert.IsTrue(result);
        }
    }
}