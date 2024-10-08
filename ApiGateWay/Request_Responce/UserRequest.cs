using System.ComponentModel.DataAnnotations;

namespace ApiGateWay.Request_Responce
{
    public class UserRequest
    {
        public string? UserName { get; set; }


        public UserRequest()
        {
        }

        public UserRequest(string username)
        {
            this.UserName = username;
        }
    }

}