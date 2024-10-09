using System.ComponentModel.DataAnnotations;

namespace UserServiceApi.Request_Responce
{
    public class UserRequest
    {
        public string? UserName { get; set; }


        public UserRequest()
        {
        }

        public UserRequest(string username)
        {
            //TODO: change naming of properties to search or something
            this.UserName = username;
        }
    }

}