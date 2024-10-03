using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWay.Data;
using ApiGateWay.Request_Responce;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace ApiGateWay.Service
{
    public class LoginService : ILoginService
    {
        private readonly AGWDbContext _context;
        public LoginService(AGWDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponce> Login(string username, string password)
        {
            try
            {
                var user = await _context.usersTables
                    .Where(u => u.UserName == username && u.Password == password)
                    .Select(x => new UsersTable
                        {
                            UserName = x.UserName,
                            Password = x.Password,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Mobile = x.Mobile,
                            Gender = x.Gender,
                            Address = x.Address
                        }).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new LoginResponce(404, "User not found");
                }
                return new LoginResponce(200, "Success", user);;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new LoginResponce(400, ex.Message);
            }
        }
    }
}