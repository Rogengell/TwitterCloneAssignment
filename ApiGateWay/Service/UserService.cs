using System.Net;
using System.Net.Http.Headers;
using ApiGateWay.Request_Responce;
using EasyNetQ;
using Newtonsoft.Json;

namespace ApiGateWay.Service
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly Settings _settings;
        private readonly SecretSettings _secretSettings;
        private readonly ILogger<UserService> _logger;


        public UserService(IHttpClientFactory httpClientFactory, Settings settings, SecretSettings secretSettings, ILogger<UserService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("RetryClient");
            _settings = settings;
            _secretSettings = secretSettings;
            _logger = logger;
        }

        public async Task<GeneralResponce> GetAllUser()
        {
            try
            {
                Console.WriteLine("Getting all users");

                HttpClient client = _httpClient;
                UserRequest userRequest = new UserRequest();

                string json = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("http://userserviceapi:8081/UserService/GetAllUser");

                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new GeneralResponce(401, "Authorization failed. Check the token and claims.");
                }

                var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                if (generalResponce == null)
                {
                    return new GeneralResponce(400, "connection failed");
                }

                return generalResponce;
            }
            catch (System.Exception ex)
            {
                return new GeneralResponce(400, ex.Message);
            }
        }


        public async Task<GeneralResponce> GetUser(string searchRequest)
        {
            try
            {
                HttpClient client = _httpClient;
                UserRequest userRequest = new UserRequest(searchRequest);

                string json = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync("http://userserviceapi:8081/UserService/GetUser", content);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new GeneralResponce(401, "Authorization failed. Check the token and claims.");
                }

                string responseBody = await response.Content.ReadAsStringAsync();

                var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                if (generalResponce == null)
                {
                    return new GeneralResponce(400, "connection failed");
                }

                return generalResponce;
            }
            catch (System.Exception ex)
            {
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> GetUserByTag(string searchRequest)
        {
            try
            {
                HttpClient client = _httpClient;
                UserRequest userRequest = new UserRequest(searchRequest);

                string json = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync("http://userserviceapi:8081/UserService/GetUserByTag", content);

                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new GeneralResponce(401, "Authorization failed. Check the token and claims.");
                }

                var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                if (generalResponce == null)
                {
                    return new GeneralResponce(400, "connection failed");
                }

                return generalResponce;
            }
            catch (System.Exception ex)
            {
                return new GeneralResponce(400, ex.Message);
            }
        }
    }
}