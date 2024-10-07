using ApiGateWay.Request_Responce;
using EFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace UserServiceApi.Service
{
    public class UserService : IUserService
    {
        private readonly AGWDbContext _context;
        public UserService(AGWDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponce> GetAllUser()
        {
            try
            {
                Console.WriteLine("Getting all users");


                var users = await _context.usersTables.ToListAsync();

                if (users != null)
                {
                    return new GeneralResponce(200, "Success", users);
                }

                return new GeneralResponce(404, "no user found");
            }
            catch (System.Exception ex)
            {
                return new GeneralResponce(400, ex.Message);
                throw;
            }
        }


        public async Task<GeneralResponce> GetUser(UserRequest searchRequest)
        {
            try
            {
                Console.WriteLine("Getting users by username");

                var users = await _context.usersTables
                    .Where(u => u.UserName == searchRequest.UserName).ToListAsync();

                if (users != null)
                {
                    return new GeneralResponce(200, "Success", users);
                }

                return new GeneralResponce(404, "no user found with the username: " + searchRequest);
            }
            catch (System.Exception ex)
            {
                return new GeneralResponce(400, ex.Message);
                throw;
            }
        }

        public async Task<GeneralResponce> GetUserByTag(UserRequest searchRequest)
        {
            // try
            // {
            //     Console.WriteLine("Getting users by username");

            //     var users = await _context.usersTables
            //         .Where(u => u.Gender == searchRequest.Gender).ToListAsync();

            //     if (users != null)
            //     {
            //         return new GeneralResponce(200, "Success", users);
            //     }

            //     return new GeneralResponce(404, "no user found with the username: " + searchRequest);
            // }
            // catch (System.Exception ex)
            // {
            //     return new GeneralResponce(400, ex.Message);
            //     throw;
            // }
            return null;
        }
    }
}