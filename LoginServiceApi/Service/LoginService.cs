using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using EFramework.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using LoginServiceApi.Request_Responce;

namespace LoginServiceApi.Service
{
    public class LoginService : LoginServiceApi.Service.ILoginService
    {
        private readonly AGWDbContext _context;

        public LoginService() { }
        public LoginService(AGWDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<GeneralResponce> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _context.usersTables
                    .Where(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password).FirstOrDefaultAsync();

                if (user == null)
                {
                    var searchResult = new GeneralResponce(404, "User not found");
                    return searchResult;
                }
                else
                {
                    var searchResult = new GeneralResponce(200, "Success", user);
                    return searchResult;
                }
            }
            catch (System.Exception ex)
            {
                var message = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException?.Message;
                var searchResult = new GeneralResponce(400, message);
                return searchResult;
            }
        }

        public async Task<GeneralResponce> CreateAccount(CreateRequest createRequest)
        {
            try
            {
                _context.usersTables?.Add(new UsersTable
                {
                    Email = createRequest.email,
                    Password = createRequest.password
                });
                await _context.SaveChangesAsync();
                var searchResult = new GeneralResponce(200, "Success");
                return searchResult;
            }
            catch (System.Exception ex)
            {
                var message = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException?.Message;
                var searchResult = new GeneralResponce(400, message);
                return searchResult;
            }
        }

        public async Task<GeneralResponce> UpdateAccount(UpdateRequest updateRequest)
        {
            try
            {
                _context.usersTables?.Update(new UsersTable
                {
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
                return searchResult;
            }
            catch (System.Exception ex)
            {
                var searchResult = new GeneralResponce(400, ex.Message);
                return searchResult;
            }
        }

        public async Task<GeneralResponce> DeleteAccount(DeleteRequest deleteRequest)
        {
            try
            {
                _context.usersTables?.Remove(new UsersTable
                {
                    Id = deleteRequest.Id.Value,
                    Email = deleteRequest.email,
                    Password = deleteRequest.password
                });
                await _context.SaveChangesAsync();
                var searchResult = new GeneralResponce(200, "Success");
                return searchResult;
            }
            catch (System.Exception ex)
            {
                var searchResult = new GeneralResponce(400, ex.Message);
                return searchResult;
            }
        }
    }
}