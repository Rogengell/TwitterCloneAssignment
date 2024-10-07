using ApiGateWay.Request_Responce;

namespace UserServiceApi.Service;

public interface IUserService
{
    Task<GeneralResponce> GetAllUser();
    Task<GeneralResponce> GetUser(UserRequest searchRequest);
    Task<GeneralResponce> GetUserByTag(UserRequest searchRequest);
}
