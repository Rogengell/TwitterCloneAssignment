using ApiGateWay.Request_Responce;
using Model;

public interface ILoginService
{
    Task<LoginResponce> Login(string username, string password);
}