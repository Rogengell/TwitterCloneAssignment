
using EFramework.Data;
using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog.Context;
using UserServiceApi.Request_Responce;

namespace UserServiceApi.Service
{
    public class UserService : IUserService
    {
        private readonly AGWDbContext _context;
        private readonly IAsyncPolicy _retryPolicy;
        private readonly ILogger<UserService> _logger;



        public UserService()
        {

        }
        public UserService(AGWDbContext context, IAsyncPolicy retryPolicy, ILogger<UserService> logger)
        {
            _context = context;
            _retryPolicy = retryPolicy;
            _logger = logger;
        }

        public async Task<GeneralResponse> GetAllUser()
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation("Prepairing to call DB_GetAllUser");
                try
                {
                    return await _retryPolicy.ExecuteAsync(async () =>
                    {

                        var users = await _context.usersTables.ToListAsync();

                        if (users == null)
                        {
                            var searchResult = new GeneralResponse(404, "no user found");
                            _logger.LogWarning($"GetAllUser failed with status {searchResult._status}");
                            return searchResult;
                        }
                        else
                        {
                            var searchResult = new GeneralResponse(200, "Success", users);
                            _logger.LogInformation($"GetAllUser successful");
                            return searchResult;
                        }
                    });

                }
                catch (System.Exception ex)
                {
                    var message = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException?.Message;
                    var searchResult = new GeneralResponse(400, ex.Message);
                    _logger.LogError(message, $"An error occurred during GetAllUser");
                    return searchResult;
                }
            }
        }


        public async Task<GeneralResponse> GetUser(UserRequest searchRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation("Prepairing to call DB_GetUser");
                try
                {
                    return await _retryPolicy.ExecuteAsync(async () =>
                    {

                        var users = await _context.usersTables
                            .Where(u => u.UserName == searchRequest.UserName).ToListAsync();

                        if (users == null)
                        {
                            var searchResult = new GeneralResponse(404, "no user found with the username: " + searchRequest.UserName);
                            _logger.LogWarning($"GetUser failed with status {searchResult._status}");
                            return searchResult;
                        }
                        else
                        {
                            var searchResult = new GeneralResponse(200, "Success", users);
                            _logger.LogInformation($"GetUser successful");
                            return searchResult;
                        }
                    });

                }
                catch (System.Exception ex)
                {
                    var searchResult = new GeneralResponse(400, ex.Message);
                    _logger.LogError(ex, $"An error occurred during GetUser");
                    return searchResult;
                }
            }
        }

        public async Task<GeneralResponse> GetUserByTag(UserRequest searchRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation("Prepairing to call DB_GetUserByTag");
                try
                {
                    return await _retryPolicy.ExecuteAsync(async () =>
                    {

                        var users = await _context.usersTables
                            .Where(u => u.Gender == searchRequest.UserName).ToListAsync();

                        if (users == null)
                        {
                            var searchResult = new GeneralResponse(404, "no user found with this tag: " + searchRequest.UserName);
                            _logger.LogWarning($"GetUserByTag failed with status {searchResult._status}");
                            return searchResult;
                        }
                        else
                        {
                            var searchResult = new GeneralResponse(200, "Success", users);
                            _logger.LogInformation($"GetUserByTag successful");
                            return searchResult;
                        }
                    });
                }
                catch (System.Exception ex)
                {
                    var searchResult = new GeneralResponse(400, ex.Message);
                    _logger.LogError(ex, $"An error occurred during GetUserByTag");
                    return searchResult;
                }
            }
        }
    }
}