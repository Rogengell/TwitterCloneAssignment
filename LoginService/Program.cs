using ApiGateWay.Request_Responce;
using EasyNetQ;
using EFramework.Data;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Model;
using Helpers;

namespace LoginService;

public class Login
{
    private readonly static AGWDbContext _context = new AGWDbContextFactory().CreateDbContext();
    private static IBus _bus;
    static async Task Main(string[] args)
    {
        await StartLisener();
        while (true);
    }

    public static async Task StartLisener()
    {
        if (_bus == null)
        {
            _bus = ConnectionHelper.GetRMQConnection();
        }
            
        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.LoginRequest>("loginSubscription", LoginStart);
        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.CreateRequest>("loginSubscription", CreateUser);
        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.UpdateRequest>("loginSubscription", UpdateUser);
        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.DeleteRequest>("loginSubscription", DeleteUser);
    }

    public static void StopListener()
    {
        _bus?.Dispose(); // Dispose of the bus when no longer needed
        Console.WriteLine("Bus disposed, listener stopped.");
    }

    public static async Task LoginStart(ApiGateWay.Request_Responce.LoginRequest loginRequest)
    {
        try
            {
                System.Console.WriteLine("LoginStart");
                var user = await _context.usersTables
                    .Where(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password).FirstOrDefaultAsync();
                System.Console.WriteLine("Database Query Complete");
                if (user == null)
                {
                    System.Console.WriteLine("User not found");
                    var searchResult = new GeneralResponce(404, "User not found");
                    await _bus.PubSub.PublishAsync(searchResult, loginRequest.ReplyTo);
                }
                else
                {
                    System.Console.WriteLine("User found");
                    var searchResult = new GeneralResponce(200, "Success", user);
                    System.Console.WriteLine(loginRequest.ReplyTo);
                    await _bus.PubSub.PublishAsync(searchResult, loginRequest.ReplyTo);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Crashed create" + ex.Message);
                var searchResult = new GeneralResponce(400, ex.Message);
                await _bus.PubSub.PublishAsync(searchResult, loginRequest.ReplyTo);
            }
    }

    public static async Task CreateUser(ApiGateWay.Request_Responce.CreateRequest createRequest)
    {
        try
            {
                System.Console.WriteLine("CreateUser");
                System.Console.WriteLine(createRequest.email);
                System.Console.WriteLine(createRequest.password);
                _context.usersTables?.Add(new UsersTable{
                    Email = createRequest.email,
                    Password = createRequest.password
                });
                await _context.SaveChangesAsync();
                System.Console.WriteLine("CreateUser Saved");
                var searchResult = new GeneralResponce(200, "Success");
                System.Console.WriteLine("Fin");
                await _bus.PubSub.PublishAsync(searchResult, createRequest.ReplyTo);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Crached create " + ex.Message);
                System.Console.WriteLine(ex.InnerException?.Message);
                System.Console.WriteLine(ex.Source);

                var searchResult = new GeneralResponce(400, ex.Message);
                await _bus.PubSub.PublishAsync(searchResult, createRequest.ReplyTo);
            }
    }

    public static async Task UpdateUser(ApiGateWay.Request_Responce.UpdateRequest updateRequest)
    {
        try
        {
            _context.usersTables?.Update(new UsersTable{
                Id = (int)updateRequest.Id,
                Email = updateRequest.Email,
                Password = updateRequest.Password,
                UserName = updateRequest.UserName,
                Mobile = updateRequest.Mobile,
                Address = updateRequest.Address,
                FirstName = updateRequest.FirstName,
                LastName = updateRequest.LastName,
                Gender = updateRequest.Gender

            });
            await _context.SaveChangesAsync();
            var searchResult = new GeneralResponce(200, "Success");
            await _bus.PubSub.PublishAsync(searchResult, updateRequest.ReplyTo);
        }
        catch (System.Exception ex)
        {
            var searchResult = new GeneralResponce(400, ex.Message);
            await _bus.PubSub.PublishAsync(searchResult, updateRequest.ReplyTo);
        }
    }

    public static async Task DeleteUser(ApiGateWay.Request_Responce.DeleteRequest deleteRequest)
    {
        try
        {
            _context.usersTables?.Remove(new UsersTable{
                Id = (int)deleteRequest.Id,
                Email = deleteRequest.email,
                Password = deleteRequest.password
            });
            await _context.SaveChangesAsync();
            var searchResult = new GeneralResponce(200, "Success");
            await _bus.PubSub.PublishAsync(searchResult, deleteRequest.ReplyTo);
        }
        catch (System.Exception ex)
        {
            var searchResult = new GeneralResponce(400, ex.Message);
            await _bus.PubSub.PublishAsync(searchResult, deleteRequest.ReplyTo);
        }
    }
}