using System.ComponentModel.DataAnnotations;

namespace ApiGateWay.Request_Responce
{
    public class UserRequest
    {
        public string status { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string? replyTo { get; set; }


        public UserRequest(string status, string username, string replyTo)
        {
            this.status = status;
            this.UserName = username;
            this.replyTo = replyTo;
        }
    }

}