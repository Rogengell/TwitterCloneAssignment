using ApiGateWay.Request_Responce;

public interface ILoginService
{
    Task<GeneralResponce> Login(string email, string password);
    Task<GeneralResponce> CreateAccount(CreateRequest createRequest);
    Task<GeneralResponce> UpdateAccount(UpdateRequest updateRequest);
    Task<GeneralResponce> DeleteAccount(DeleteRequest deleteRequest);
}