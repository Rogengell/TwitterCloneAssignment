
using EFramework.Data;
using Microsoft.EntityFrameworkCore;
using UserServiceApi.Request_Responce;

namespace UserServiceApi.Service
{
    public class UserService : IUserService
    {
        private readonly AGWDbContext _context;
        public UserService(AGWDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> GetAllUser()
        {
            try
            {
                Console.WriteLine("Getting all users");


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
                Console.WriteLine("Getting users by username");

                var users = await _context.usersTables
                    .Where(u => u.UserName == searchRequest.UserName).ToListAsync();

                if (users == null)
                {
                    var searchResult = new GeneralResponse(404, "no user found with the username: " + searchRequest);
                    return searchResult;
                }
                else
                {
                    var searchResult = new GeneralResponse(200, "Success", users);
                    return searchResult;
                }

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
                Console.WriteLine("Getting users by username");

                var users = await _context.usersTables
                    .Where(u => u.Gender == searchRequest.UserName).ToListAsync();

                if (users == null)
                {
                    var searchResult = new GeneralResponse(404, "no user found with this tag: " + searchRequest);
                    return searchResult;
                }
                else
                {
                    var searchResult = new GeneralResponse(200, "Success", users);
                    return searchResult;
                }

            }
            catch (System.Exception ex)
            {
                var searchResult = new GeneralResponse(400, ex.Message);
                return searchResult;
            }
        }
    }
}