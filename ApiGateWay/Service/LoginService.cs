using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWay.Request_Responce;
using EasyNetQ;
using EFramework.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace ApiGateWay.Service
{
    public class LoginService : ILoginService
    {
        private readonly IBus _bus;
        public LoginService(IBus bus)
        {
            _bus = bus;
        }

        public async Task<GeneralResponce> Login(string email, string password)
        {
            try
            {
                Console.WriteLine();

                var replyQueue = "Login_queue_" + System.Guid.NewGuid();
                var LoginRequest = new LoginRequest(email, password, replyQueue);

                await _bus.PubSub.PublishAsync(LoginRequest);
      
                GeneralResponce generalResponce = null;
                var subscriptionResult = _bus.PubSub.Subscribe<GeneralResponce>(replyQueue, result =>
                {
                    generalResponce = result;
                });

                int count = 0;
                while (generalResponce == null && count < 10)
                {
                    await Task.Delay(1000);
                    count++;
                }

                subscriptionResult.Dispose();

                if (generalResponce?._status == 200)
                {
                    return new GeneralResponce(200, "Success", generalResponce._user); 
                }
                return new GeneralResponce(404, "User not found");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> CreateAccount(CreateRequest createRequest)
        {
            try
            {
                var replyQueue = "Create_Account_queue_" + System.Guid.NewGuid();
                createRequest.ReplyTo=replyQueue;

                await _bus.PubSub.PublishAsync(createRequest);

                GeneralResponce generalResponce = null;
                var subscriptionResult = _bus.PubSub.Subscribe<GeneralResponce>(replyQueue, result =>
                {
                    generalResponce = result;
                });

                int count = 0;
                while (generalResponce == null && count < 10)
                {
                    await Task.Delay(1000);
                    count++;
                }

                subscriptionResult.Dispose();

                if (generalResponce?._status == 200)
                {
                    return new GeneralResponce(200, "Success"); 
                }
                return new GeneralResponce(404, "Create Failed");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> UpdateAccount(UpdateRequest updateRequest)
        {
            try
            {
                var replyQueue = "Update_Account_queue_" + System.Guid.NewGuid();
                updateRequest.ReplyTo=replyQueue;

                await _bus.PubSub.PublishAsync(updateRequest);

                GeneralResponce generalResponce = null;
                var subscriptionResult = _bus.PubSub.Subscribe<GeneralResponce>(replyQueue, result =>
                {
                    generalResponce = result;
                });

                int count = 0;
                while (generalResponce == null && count < 10)
                {
                    await Task.Delay(1000);
                    count++;
                }

                subscriptionResult.Dispose();

                if (generalResponce?._status == 200)
                {
                    return new GeneralResponce(200, "Success"); 
                }
                return new GeneralResponce(404, "Account Update Failed");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        public async Task<GeneralResponce> DeleteAccount(DeleteRequest deleteRequest)
        {
            try
            {
                var replyQueue = "Delete_Account_queue_" + System.Guid.NewGuid();
                deleteRequest.ReplyTo=replyQueue;

                await _bus.PubSub.PublishAsync(deleteRequest);

                GeneralResponce generalResponce = null;
                var subscriptionResult = _bus.PubSub.Subscribe<GeneralResponce>(replyQueue, result =>
                {
                    generalResponce = result;
                });

                int count = 0;
                while (generalResponce == null && count < 10)
                {
                    await Task.Delay(1000);
                    count++;
                }

                subscriptionResult.Dispose();

                if (generalResponce?._status == 200)
                {
                    return new GeneralResponce(200, "Success", generalResponce._user); 
                }
                return new GeneralResponce(404, "Delete Account Failed");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }
    }
}