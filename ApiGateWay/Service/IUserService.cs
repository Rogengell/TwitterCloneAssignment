using ApiGateWay.Request_Responce;

namespace ApiGateWay.Service
{
    public interface IUserService
    {
        Task<GeneralResponce> GetAllUser();
        Task<GeneralResponce> GetUser(string searchRequest);
        Task<GeneralResponce> GetUserByTag(string searchRequest);
    }
}