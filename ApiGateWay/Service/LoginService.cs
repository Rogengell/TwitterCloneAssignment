using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWay.Request_Responce;
using EFramework.Data;
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

        public async Task<LoginResponce> Login(string email, string password)
        {
            try
            {
                var user = await _context.usersTables
                    .Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();

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

        public async Task<LoginResponce> CreateAccount(CreateRequest createRequest)
        {
            try
            {
                _context.usersTables?.Add(new UsersTable{
                    Email = createRequest.email,
                    Password = createRequest.password
                });
                await _context.SaveChangesAsync();
                return new LoginResponce(200, "Success");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new LoginResponce(400, ex.Message);
            }
        }

        public async Task<LoginResponce> UpdateAccount(UpdateRequest updateRequest)
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
                return new LoginResponce(200, "Success");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new LoginResponce(400, ex.Message);
            }
        }

        public async Task<LoginResponce> DeleteAccount(DeleteRequest deleteRequest)
        {
             try
            {
                _context.usersTables?.Remove(new UsersTable{
                    Id = (int)deleteRequest.Id,
                    Email = deleteRequest.email,
                    Password = deleteRequest.password
                });
                await _context.SaveChangesAsync();
                return new LoginResponce(200, "Success");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new LoginResponce(400, ex.Message);
            }
        }
    }
}