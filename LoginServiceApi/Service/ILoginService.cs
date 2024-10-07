using ApiGateWay.Request_Responce;

namespace LoginServiceApi.Service;
public interface ILoginService
{
    Task<GeneralResponce> Login(LoginRequest loginRequest);
    Task<GeneralResponce> CreateAccount(CreateRequest createRequest);
    Task<GeneralResponce> UpdateAccount(UpdateRequest updateRequest);
    Task<GeneralResponce> DeleteAccount(DeleteRequest deleteRequest);
}