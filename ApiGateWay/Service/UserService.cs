using ApiGateWay.Request_Responce;
using EasyNetQ;
using Newtonsoft.Json;

namespace ApiGateWay.Service
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        public async Task<GeneralResponce> GetAllUser()
        {
            try
            {
                Console.WriteLine("Getting all users");
                
                HttpClient client = new HttpClient();
                UserRequest userRequest = new UserRequest();
                
                string json = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await client.PostAsync("http://userserviceapi:8081/UserServiceApi/GetAllUser", content);

                string responseBody = await response.Content.ReadAsStringAsync();

                var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                if (generalResponce._status == 200)
                {
                    return new GeneralResponce(200, "Success", generalResponce._users);
                }

                return new GeneralResponce(404, generalResponce._message);
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
                Console.WriteLine("Getting all users");
                
                HttpClient client = new HttpClient();
                UserRequest userRequest = new UserRequest(searchRequest);
                
                string json = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await client.PostAsync("http://userserviceapi:8081/UserServiceApi/GetUser", content);

                string responseBody = await response.Content.ReadAsStringAsync();

                var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                if (generalResponce._status == 200)
                {
                    return new GeneralResponce(200, "Success", generalResponce._users);
                }

                return new GeneralResponce(404, generalResponce._message);
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
                Console.WriteLine("Getting all users");
                
                HttpClient client = new HttpClient();
                UserRequest userRequest = new UserRequest(searchRequest);
                
                string json = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await client.PostAsync("http://userserviceapi:8081/UserServiceApi/GetUserByTag", content);

                string responseBody = await response.Content.ReadAsStringAsync();

                var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);

                if (generalResponce._status == 200)
                {
                    return new GeneralResponce(200, "Success", generalResponce._users);
                }

                return new GeneralResponce(404, generalResponce._message);
            }
            catch (System.Exception ex)
            {
                return new GeneralResponce(400, ex.Message);
            }
        }
    }
}