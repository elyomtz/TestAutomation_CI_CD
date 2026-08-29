using log4net;
using RestSharp;
using TestAutomation_CI_CD.Core.Logger;


namespace TestAutomation_CI_CD.Core.API_Test_Core
{
    public class ApiClient
    {
        private readonly ILog logger = LoggerManager.Create<ApiClient>();
        private readonly RestClient _client;

        public ApiClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<RestResponse<T>> GetAsync<T>(string endpoint)
        {
            logger.Info("Creating GET request");
            var request = new RestRequest(endpoint, Method.Get);

            return await _client.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest body) where TRequest : class
        {
            logger.Info("Creating POST request");
            var request = new RestRequest(endpoint, Method.Post);

            request.AddJsonBody(body);

            return await _client.ExecuteAsync<TResponse>(request);
        }

    }
}
