using System.ComponentModel.DataAnnotations;

namespace UserServiceApi.Request_Responce
{
    public class UserRequest
    {
        public string status { get; set; }
        public string? UserName { get; set; }


        public UserRequest(string status, string username, string replyTo)
        {
            this.status = status;
            this.UserName = username;
        }
    }

}