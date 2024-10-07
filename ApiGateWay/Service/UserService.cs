using ApiGateWay.Request_Responce;
using EasyNetQ;

namespace ApiGateWay.Service
{
    public class UserService : IUserService
    {
        private readonly IBus _bus;
        public UserService(IBus bus)
        {
            _bus = bus;
        }

        public async Task<GeneralResponce> GetAllUser()
        {
            try
            {
                Console.WriteLine("Getting all users");

                var replyQueue = "GetAllUser_queue_" + System.Guid.NewGuid();
                var getAllUsers = replyQueue;

                await _bus.PubSub.PublishAsync(getAllUsers);

                GeneralResponce? generalResponce = null;
                var subscriptionResult = _bus.PubSub.Subscribe<GeneralResponce>(replyQueue, result =>
                {
                    generalResponce = result;
                });


                while (generalResponce == null)
                {
                    await Task.Delay(1000);
                }

                subscriptionResult.Dispose();

                if (generalResponce._status == 200)
                {
                    return new GeneralResponce(200, "Success", generalResponce._users);
                }

                return new GeneralResponce(404, "no user found");
            }
            catch (System.Exception ex)
            {
                return new GeneralResponce(400, ex.Message);
                throw;
            }
        }


        public async Task<GeneralResponce> GetUser(string searchRequest)
        {
            try
            {
                Console.WriteLine("Getting users by username");

                var replyQueue = "GetAllUser_queue_" + System.Guid.NewGuid();
                var getUsers = new UserRequest("GetUser", searchRequest, replyQueue);

                await _bus.PubSub.PublishAsync(getUsers);

                GeneralResponce? generalResponce = null;
                var subscriptionResult = _bus.PubSub.Subscribe<GeneralResponce>(replyQueue, result =>
                {
                    generalResponce = result;
                });


                while (generalResponce == null)
                {
                    await Task.Delay(1000);
                }

                subscriptionResult.Dispose();

                if (generalResponce._status == 200)
                {
                    return new GeneralResponce(200, "Success", generalResponce._users);
                }

                return new GeneralResponce(404, "no user found with the username: " + searchRequest);
            }
            catch (System.Exception ex)
            {
                return new GeneralResponce(400, ex.Message);
                throw;
            }
        }

        public async Task<GeneralResponce> GetUserByTag(string createRequest)
        {
            //TODO: implement GetUserByTag logic for rmq
            throw new NotImplementedException();
        }
    }
}