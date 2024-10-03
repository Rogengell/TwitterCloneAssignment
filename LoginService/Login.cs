using ApiGateWay.Request_Responce;
using EasyNetQ;
using EFramework.Data;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Model;

namespace LoginService;

public class Login
{
    private readonly static AGWDbContext _context = new AGWDbContextFactory().CreateDbContext();
    private static IBus _bus;
    static async void Main(string[] args)
    {
        await StartLisener();
        Console.ReadLine();
    }

    public static async Task StartLisener()
    {
        if (_bus == null)
        {
            string rabbitMqConnectionString = "host=localhost"; // Update as necessary
            _bus = RabbitHutch.CreateBus(rabbitMqConnectionString);
        }
            
        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.LoginRequest>("loginSubscription", LoginStart);
        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.CreateRequest>("loginSubscription", CreateUser);
        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.UpdateRequest>("loginSubscription", UpdateUser);
        _bus.PubSub.Subscribe<ApiGateWay.Request_Responce.DeleteRequest>("loginSubscription", DeleteUser);

        Console.ReadLine();
        StopListener();
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
                var user = await _context.usersTables
                    .Where(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password).FirstOrDefaultAsync();

                if (user == null)
                {
                    var searchResult = new GeneralResponce(404, "User not found");
                    await _bus.PubSub.PublishAsync(searchResult, loginRequest.ReplyTo);
                }
                else
                {
                    var searchResult = new GeneralResponce(200, "Success", user);
                    await _bus.PubSub.PublishAsync(searchResult, loginRequest.ReplyTo);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                var searchResult = new GeneralResponce(400, ex.Message);
                await _bus.PubSub.PublishAsync(searchResult, loginRequest.ReplyTo);
            }
    }

    public static async Task CreateUser(ApiGateWay.Request_Responce.CreateRequest createRequest)
    {
        try
            {
                _context.usersTables?.Add(new UsersTable{
                    Email = createRequest.email,
                    Password = createRequest.password
                });
                await _context.SaveChangesAsync();
                var searchResult = new GeneralResponce(200, "Success");
                await _bus.PubSub.PublishAsync(searchResult, createRequest.ReplyTo);
            }
            catch (System.Exception ex)
            {
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