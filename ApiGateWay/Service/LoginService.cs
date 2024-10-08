using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWay.Request_Responce;
using EFramework.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Newtonsoft.Json;

namespace ApiGateWay.Service
{
    public class LoginService : ApiGateWay.Service.ILoginService
    {
        public LoginService()
        { }

        public async Task<GeneralResponce> Login(string email, string password)
        {
            try
            {
                HttpClient client = new HttpClient();
                LoginRequest loginRequest = new LoginRequest(email, password);

                string json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Login", content);

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
                var message = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException?.Message;
                Console.WriteLine(message);
                return new GeneralResponce(400, message);
            }
        }

        public async Task<GeneralResponce> CreateAccount(CreateRequest createRequest)
        {
            try
            {
                HttpClient client = new HttpClient();
                string json = JsonConvert.SerializeObject(createRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Create", content);

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
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> UpdateAccount(UpdateRequest updateRequest)
        {
            try
            {
                HttpClient client = new HttpClient();
                string json = JsonConvert.SerializeObject(updateRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Update", content);

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
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> DeleteAccount(DeleteRequest deleteRequest)
        {
            try
            {
                HttpClient client = new HttpClient();
                string json = JsonConvert.SerializeObject(deleteRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://loginserviceapi:8082/LoginService/Delete", content);

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
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }
    }
}