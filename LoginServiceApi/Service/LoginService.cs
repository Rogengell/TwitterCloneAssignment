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
using Polly;
using Serilog.Context;

namespace LoginServiceApi.Service
{
    public class LoginService : LoginServiceApi.Service.ILoginService
    {
        private readonly AGWDbContext _context;
        private readonly IAsyncPolicy _retryPolicy;
        private readonly ILogger<LoginService> _logger;


        public LoginService() { }
        public LoginService(AGWDbContext dbContext, IAsyncPolicy retryPolicy, ILogger<LoginService> logger)
        {
            _context = dbContext;
            _retryPolicy = retryPolicy;
            _logger = logger;
        }

        public async Task<GeneralResponce> Login(LoginRequest loginRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation($"Calling DB_get for user {loginRequest.Email}");
                try
                {
                    return await _retryPolicy.ExecuteAsync(async () =>
                    {
                        var user = await _context.usersTables
                            .Where(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password).FirstOrDefaultAsync();

                        if (user == null)
                        {
                            var searchResult = new GeneralResponce(404, "User not found");
                            _logger.LogWarning($"Login failed for user {loginRequest.Email} with status {searchResult._status}");
                            return searchResult;
                        }
                        else
                        {
                            var searchResult = new GeneralResponce(200, "Success", user);
                            _logger.LogInformation($"Login successful for user {loginRequest.Email} with status {searchResult._status}");
                            return searchResult;
                        }
                    });
                }
                catch (System.Exception ex)
                {
                    var message = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException?.Message;
                    var searchResult = new GeneralResponce(400, message);
                    _logger.LogError(message, $"An error occurred during Login for user {loginRequest.Email}");
                    return searchResult;
                }
            }
        }

        public async Task<GeneralResponce> CreateAccount(CreateRequest createRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation($"Prepare for calling DB_create for user {createRequest.email}");
                try
                {
                    return await _retryPolicy.ExecuteAsync(async () =>
                    {
                        _context.usersTables?.Add(new UsersTable
                        {
                            Email = createRequest.email,
                            Password = createRequest.password
                        });
                        await _context.SaveChangesAsync();
                        var searchResult = new GeneralResponce(200, "Success");
                        _logger.LogInformation($"Calling DB_create for user {createRequest.email} with status {searchResult._status}");
                        return searchResult;
                    });
                }
                catch (System.Exception ex)
                {
                    var message = ex.Message + "\n" + ex.StackTrace + "\n" + ex.InnerException?.Message;
                    var searchResult = new GeneralResponce(400, message);
                    _logger.LogError(message, $"An error occurred during DB_create for user {createRequest.email}");
                    return searchResult;
                }
            }
        }

        public async Task<GeneralResponce> UpdateAccount(UpdateRequest updateRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation($"Prepare for calling DB_update for user {updateRequest.Email}");
                try
                {
                    return await _retryPolicy.ExecuteAsync(async () =>
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
                        _logger.LogInformation($"Calling DB_update for user {updateRequest.Email} with status {searchResult._status}");
                        return searchResult;
                    });
                }
                catch (System.Exception ex)
                {
                    var searchResult = new GeneralResponce(400, ex.Message);
                    _logger.LogError(ex, $"An error occurred during DB_update for user {updateRequest.Email}");
                    return searchResult;
                }
            }
        }

        public async Task<GeneralResponce> DeleteAccount(DeleteRequest deleteRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation($"Prepare for calling DB_delete for user {deleteRequest.email}");
                try
                {
                    return await _retryPolicy.ExecuteAsync(async () =>
                    {
                        _context.usersTables?.Remove(new UsersTable
                        {
                            Id = deleteRequest.Id.Value,
                            Email = deleteRequest.email,
                            Password = deleteRequest.password
                        });
                        await _context.SaveChangesAsync();
                        var searchResult = new GeneralResponce(200, "Success");
                        _logger.LogInformation($"Calling DB_delete for user {deleteRequest.email} with status {searchResult._status}");
                        return searchResult;
                    });
                }
                catch (System.Exception ex)
                {
                    var searchResult = new GeneralResponce(400, ex.Message);
                    _logger.LogError(ex, $"An error occurred during DB_delete for user {deleteRequest.email}");
                    return searchResult;
                }
            }
        }
    }
}