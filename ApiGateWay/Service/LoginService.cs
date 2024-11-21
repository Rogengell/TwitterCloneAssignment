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

namespace ApiGateWay.Service
{
    public class LoginService : ApiGateWay.Service.ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly Settings _settings;
        private readonly SecretSettings _secretSettings;
        public LoginService(IHttpClientFactory httpClientFactory, Settings settings, SecretSettings secretSettings)
        {
            _httpClient = httpClientFactory.CreateClient("RetryClient");
            _settings = settings;
            _secretSettings = secretSettings;
        }

        public async Task<GeneralResponce> Login(string email, string password)
        {
            try
            {
                HttpClient client = _httpClient;
                LoginRequest loginRequest = new LoginRequest(email, password);

                string json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Login", content);

                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Authorization failed. Check the token and claims.");
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
                var message = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException?.Message;
                Console.WriteLine(message);
                return new GeneralResponce(400, message);
            }
        }

        public async Task<GeneralResponce> CreateAccount(CreateRequest createRequest)
        {
            try
            {
                HttpClient client = _httpClient;
                string json = JsonConvert.SerializeObject(createRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsync("http://loginserviceapi:8082/LoginService/Create", content);

                string responseBody = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Authorization failed. Check the token and claims.");
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
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> UpdateAccount(UpdateRequest updateRequest)
        {
            try
            {
                HttpClient client = _httpClient;
                string json = JsonConvert.SerializeObject(updateRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Update", content);

                string responseBody = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Authorization failed. Check the token and claims.");
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
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> DeleteAccount(DeleteRequest deleteRequest)
        {
            try
            {
                HttpClient client = _httpClient;
                string json = JsonConvert.SerializeObject(deleteRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Delete", content);

                string responseBody = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Authorization failed. Check the token and claims.");
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
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> GetAuthenticated()
        {
            try
            {
                HttpClient client = _httpClient;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secretSettings.MICRO_SERVICE_TOKEN.Trim());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("http://loginserviceapi:8082/Faucet/GetToken");
                string responseBody = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Authorization failed. Check the token and claims.");
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
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }
    }
}