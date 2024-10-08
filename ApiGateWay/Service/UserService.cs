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

                HttpResponseMessage response = await client.GetAsync("http://userserviceapi:8081/UserService/GetAllUser");

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


        public async Task<GeneralResponce> GetUser(string searchRequest)
        {
            try
            {
                Console.WriteLine("Getting all users");

                HttpClient client = new HttpClient();
                UserRequest userRequest = new UserRequest(searchRequest);

                string json = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                System.Console.WriteLine("sending request");
                HttpResponseMessage response = await client.PostAsync("http://userserviceapi:8081/UserService/GetUser", content);

                string responseBody = await response.Content.ReadAsStringAsync();

                var generalResponce = JsonConvert.DeserializeObject<GeneralResponce>(responseBody);
                System.Console.WriteLine("got response");
                System.Console.WriteLine(generalResponce._users);
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
                Console.WriteLine("Getting all users");

                HttpClient client = new HttpClient();
                UserRequest userRequest = new UserRequest(searchRequest);

                string json = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://userserviceapi:8081/UserService/GetUserByTag", content);

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
    }
}