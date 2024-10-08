

using UserServiceApi.Request_Responce;

namespace UserServiceApi.Service;

public interface IUserService
{
    Task<GeneralResponse> GetAllUser();
    Task<GeneralResponse> GetUser(UserRequest searchRequest);
    Task<GeneralResponse> GetUserByTag(UserRequest searchRequest);
}
