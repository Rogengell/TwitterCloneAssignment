using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApiGateWay.Request_Responce;
using EFramework.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Newtonsoft.Json;
using Polly;
using Serilog.Context;

namespace ApiGateWay.Service
{
    public class LoginService : ApiGateWay.Service.ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly Settings _settings;
        private readonly SecretSettings _secretSettings;
        private readonly ILogger<LoginService> _logger;

        public LoginService(IHttpClientFactory httpClientFactory, Settings settings, SecretSettings secretSettings, ILogger<LoginService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("RetryClient");
            _settings = settings;
            _secretSettings = secretSettings;
            _logger = logger;
        }

        public async Task<GeneralResponce> Login(string email, string password)
        {
            using (LogContext.PushProperty("Username", email ?? "UnknownUser"))
                try
                {
                    _logger.LogInformation($"Prepare for calling LoginService login for user {email}");
                    HttpClient client = _httpClient;
                    LoginRequest loginRequest = new LoginRequest(email, password);

                    string json = JsonConvert.SerializeObject(loginRequest);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _logger.LogInformation($"Response from LoginService login for user {email}");
                    HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Login", content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _logger.LogInformation($"Authorization failed for user {email}");
                        return new GeneralResponce(401, "Authorization failed. Check the token and claims.");
                    }

                    var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                    if (generalResponce == null)
                    {
                        _logger.LogInformation($"Connection failed for user {email}");
                        return new GeneralResponce(400, "connection failed");
                    }
                    _logger.LogInformation($"Login successful for user {email}");
                    return generalResponce;
                }
                catch (System.Exception ex)
                {
                    var message = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException?.Message;
                    Console.WriteLine(message);
                    _logger.LogError(message, $"An error occurred during login for user {email}");
                    return new GeneralResponce(400, message);
                }
        }

        public async Task<GeneralResponce> CreateAccount(CreateRequest createRequest)
        {
            using (LogContext.PushProperty("Username", createRequest.email ?? "UnknownUser"))
            {
                _logger.LogInformation($"Prepare for calling LoginService CreateAccount for user {createRequest.email}");
                try
                {
                    HttpClient client = _httpClient;
                    string json = JsonConvert.SerializeObject(createRequest);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _logger.LogInformation($"Response from LoginService CreateAccount for user {createRequest.email}");
                    HttpResponseMessage response = await client.PutAsync("http://loginserviceapi:8082/LoginService/Create", content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _logger.LogInformation($"Authorization failed for user {createRequest.email}");
                        return new GeneralResponce(401, "Authorization failed. Check the token and claims.");
                    }

                    var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                    if (generalResponce == null)
                    {
                        _logger.LogInformation($"Connection failed for user {createRequest.email}");
                        return new GeneralResponce(400, "connection failed");
                    }
                    _logger.LogInformation($"Create successful for user {createRequest.email}");
                    return generalResponce;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _logger.LogError(ex, $"An error occurred during CreateAccount for user {createRequest.email}");
                    return new GeneralResponce(400, ex.Message);
                }
            }
        }

        public async Task<GeneralResponce> UpdateAccount(UpdateRequest updateRequest)
        {
            using (LogContext.PushProperty("Username", updateRequest.Email ?? "UnknownUser"))
            {
                _logger.LogInformation($"Prepare for calling LoginService UpdateAccount for user {updateRequest.Email}");
                try
                {
                    HttpClient client = _httpClient;
                    string json = JsonConvert.SerializeObject(updateRequest);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _logger.LogInformation($"Response from LoginService UpdateAccount for user {updateRequest.Email}");
                    HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Update", content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _logger.LogInformation($"Authorization failed for user {updateRequest.Email}");
                        return new GeneralResponce(401, "Authorization failed. Check the token and claims.");
                    }

                    var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                    if (generalResponce == null)
                    {
                        _logger.LogInformation($"Connection failed for user {updateRequest.Email}");
                        return new GeneralResponce(400, "connection failed");
                    }
                    _logger.LogInformation($"Update successful for user {updateRequest.Email}");
                    return generalResponce;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _logger.LogError(ex, $"An error occurred during UpdateAccount for user {updateRequest.Email}");
                    return new GeneralResponce(400, ex.Message);
                }
            }
        }

        public async Task<GeneralResponce> DeleteAccount(DeleteRequest deleteRequest)
        {
            using (LogContext.PushProperty("Username", deleteRequest.email ?? "UnknownUser"))
            {
                _logger.LogInformation($"Prepare for calling LoginService DeleteAccount for user {deleteRequest.email}");
                try
                {
                    HttpClient client = _httpClient;
                    string json = JsonConvert.SerializeObject(deleteRequest);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _logger.LogInformation($"Response from LoginService DeleteAccount for user {deleteRequest.email}");
                    HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Delete", content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _logger.LogInformation($"Authorization failed for user {deleteRequest.email}");
                        return new GeneralResponce(401, "Authorization failed. Check the token and claims.");
                    }

                    var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                    if (generalResponce == null)
                    {
                        _logger.LogInformation($"Connection failed for user {deleteRequest.email}");
                        return new GeneralResponce(400, "connection failed");
                    }

                    _logger.LogInformation($"Delete successful for user {deleteRequest.email}");
                    return generalResponce;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _logger.LogError(ex, $"An error occurred during DeleteAccount for user {deleteRequest.email}");
                    return new GeneralResponce(400, ex.Message);
                }
            }
        }

        public async Task<GeneralResponce> GetAuthenticated()
        {
            using (LogContext.PushProperty("Username", "UnknownUser"))
            {
                _logger.LogInformation($"Prepare for calling LoginService GetAuthenticated");
                try
                {
                    HttpClient client = _httpClient;

                    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _logger.LogInformation($"Response from LoginService GetAuthenticated");
                    HttpResponseMessage response = await client.GetAsync("http://loginserviceapi:8082/Faucet/GetToken");
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _logger.LogInformation($"Authorization failed for user UnknownUser");
                        return new GeneralResponce(401, "Authorization failed. Check the token and claims.");
                    }

                    var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                    if (generalResponce == null)
                    {
                        _logger.LogInformation($"Connection failed for user UnknownUser");
                        return new GeneralResponce(400, "connection failed");
                    }

                    _logger.LogInformation($"GetAuthenticated successful for user UnknownUser");
                    return generalResponce;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _logger.LogError(ex, $"An error occurred during GetAuthenticated");
                    return new GeneralResponce(400, ex.Message);
                }
            }
        }
    }
}