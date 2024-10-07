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
        {}

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

                if (generalResponce._status == 200)
                {
                    return new GeneralResponce(200, "Success", generalResponce._user); 
                }
                return new GeneralResponce(404, generalResponce._message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> CreateAccount(CreateRequest createRequest)
        {
            try
            {
                var generalResponce = new GeneralResponce(200, "Success");
                if (generalResponce?._status == 200)
                {
                    return new GeneralResponce(200, "Success"); 
                }
                return new GeneralResponce(404, "Create Failed");
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
                
                var generalResponce = new GeneralResponce(200, "Success");
                if (generalResponce?._status == 200)
                {
                    return new GeneralResponce(200, "Success"); 
                }
                return new GeneralResponce(404, "Account Update Failed");
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
                var generalResponce = new GeneralResponce(200, "Success");
                if (generalResponce?._status == 200)
                {
                    return new GeneralResponce(200, "Success", generalResponce._user); 
                }
                return new GeneralResponce(404, "Delete Account Failed");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }
    }
}