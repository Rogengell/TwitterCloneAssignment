using ApiGateWay.Request_Responce;

public interface ILoginService
{
    Task<LoginResponce> Login(string email, string password);
    Task<LoginResponce> CreateAccount(CreateRequest createRequest);
    Task<LoginResponce> UpdateAccount(UpdateRequest updateRequest);
    Task<LoginResponce> DeleteAccount(DeleteRequest deleteRequest);
}