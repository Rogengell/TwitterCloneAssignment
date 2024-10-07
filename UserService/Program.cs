using ApiGateWay.Request_Responce;
using EasyNetQ;
using EFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace UserService;

public class User
{
    private readonly static AGWDbContext _context = new AGWDbContextFactory().CreateDbContext();

    private static IBus _bus;

    static async Task Main(string[] args)
    {
        await StartListener();
        while (true) ;
    }

    public static async Task StartListener()
    {
        if (_bus == null)
        {
            
        }

        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.UserRequest>("userSubscription", GetAllUser);
        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.UserRequest>("userSubscription", GetUser);
    }

    public static void StopListener()
    {
        _bus?.Dispose(); // Dispose of the bus when no longer needed
        Console.WriteLine("Bus disposed, listener stopped.");
    }

    public static async Task GetAllUser(ApiGateWay.Request_Responce.UserRequest userRequest)
    {
        try
        {
            var users = await _context.usersTables.ToListAsync();

            if (users == null)
            {
                var searchResult = new GeneralResponce(404, "User not found");
                await _bus.PubSub.PublishAsync(searchResult, userRequest.replyTo);
            }
            else
            {
                var searchResult = new GeneralResponce(200, "Success", users);
                await _bus.PubSub.PublishAsync(searchResult, userRequest.replyTo);
            }
        }
        catch (System.Exception ex)
        {
            var searchResult = new ApiGateWay.Request_Responce.GeneralResponce(400, ex.Message);
            await _bus.PubSub.PublishAsync(searchResult, userRequest.replyTo);
        }
    }

    public static async Task GetUser(ApiGateWay.Request_Responce.UserRequest userRequest)
    {
        try
        {
            var users = await _context.usersTables
                .Where(u => u.UserName == userRequest.UserName).ToListAsync();

            if (users == null)
            {
                var searchResult = new GeneralResponce(404, "User not found");
                await _bus.PubSub.PublishAsync(searchResult, userRequest.replyTo);
            }
            else
            {
                var searchResult = new GeneralResponce(200, "Success", users);
                await _bus.PubSub.PublishAsync(searchResult, userRequest.replyTo);
            }
        }
        catch (System.Exception ex)
        {
            var searchResult = new ApiGateWay.Request_Responce.GeneralResponce(400, ex.Message);
            await _bus.PubSub.PublishAsync(searchResult, userRequest.replyTo);
        }
    }
}