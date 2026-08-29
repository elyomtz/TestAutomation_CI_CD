using log4net;
using RestSharp;
using TestAutomation_CI_CD.Core.API_Test_Core;
using TestAutomation_CI_CD.Core.API_Test_Core.Models;
using TestAutomation_CI_CD.Core.Configuration;
using TestAutomation_CI_CD.Core.Logger;

namespace TestAutomation_CI_CD.Business
{
    public class UserService
    {
        private readonly ApiClient _apiClient;
        private readonly ILog logger = LoggerManager.Create<UserService>();

        public UserService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<RestResponse<List<User>>> GetUsersAsync()
        {
            logger.Info("using GET to retrieve user info");
            return await _apiClient.GetAsync<List<User>>(ConfigurationManager.Settings.Endpoint);
        }

        public async Task<RestResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest user)
        {
            logger.Info("using POST to set user info");
            return await _apiClient.PostAsync<CreateUserRequest, CreateUserResponse>(ConfigurationManager.Settings.Endpoint, user);
        }

        public async Task<RestResponse<string>> GetInvalidEndpointAsync()
        {
            logger.Info("using GET with invalid endpoint");
            return await _apiClient.GetAsync<string>(ConfigurationManager.Settings.InvalidEndpoint);
        }

    }
}
