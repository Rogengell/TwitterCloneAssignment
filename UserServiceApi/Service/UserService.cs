
using EFramework.Data;
using Microsoft.EntityFrameworkCore;
using Polly;
using UserServiceApi.Request_Responce;

namespace UserServiceApi.Service
{
    public class UserService : IUserService
    {
        private readonly AGWDbContext _context;
        private readonly IAsyncPolicy _retryPolicy;


        public UserService()
        {

        }
        public UserService(AGWDbContext context, IAsyncPolicy retryPolicy)
        {
            _context = context;
            _retryPolicy = retryPolicy;

        }

        public async Task<GeneralResponse> GetAllUser()
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () => {

                    var users = await _context.usersTables.ToListAsync();

                    if (users == null)
                    {
                        var searchResult = new GeneralResponse(404, "no user found");
                        return searchResult;
                    }
                    else
                    {

                        var searchResult = new GeneralResponse(200, "Success", users);
                        return searchResult;
                    }
                });

            }
            catch (System.Exception ex)
            {
                var message = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException?.Message;
                var searchResult = new GeneralResponse(400, ex.Message);
                return searchResult;
            }
        }


        public async Task<GeneralResponse> GetUser(UserRequest searchRequest)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () => {

                    var users = await _context.usersTables
                        .Where(u => u.UserName == searchRequest.UserName).ToListAsync();

                    if (users == null)
                    {
                        var searchResult = new GeneralResponse(404, "no user found with the username: " + searchRequest.UserName);
                        return searchResult;
                    }
                    else
                    {
                        var searchResult = new GeneralResponse(200, "Success", users);
                        return searchResult;
                    }
                });

            }
            catch (System.Exception ex)
            {
                var searchResult = new GeneralResponse(400, ex.Message);
                return searchResult;
            }
        }

        public async Task<GeneralResponse> GetUserByTag(UserRequest searchRequest)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () => {

                    var users = await _context.usersTables
                        .Where(u => u.Gender == searchRequest.UserName).ToListAsync();

                    if (users == null)
                    {
                        var searchResult = new GeneralResponse(404, "no user found with this tag: " + searchRequest.UserName);
                        return searchResult;
                    }
                    else
                    {
                        var searchResult = new GeneralResponse(200, "Success", users);
                        return searchResult;
                    }
                });

            }
            catch (System.Exception ex)
            {
                var searchResult = new GeneralResponse(400, ex.Message);
                return searchResult;
            }
        }
    }
}