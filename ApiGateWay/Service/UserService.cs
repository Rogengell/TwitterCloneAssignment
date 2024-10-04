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

                subscriptionResult.Dispose();

                while (generalResponce == null)
                {
                    return new GeneralResponce(200, "Success");
                }

                return new GeneralResponce(404, "no user found");
            }
            catch (System.Exception ex)
            {
                return new GeneralResponce(400, ex.Message);
                throw;
            }
        }


        public Task<GeneralResponce> GetUser(string searchRequest)
        {
            //TODO: implement GetUser logic for rmq
            throw new NotImplementedException();
        }

        public Task<GeneralResponce> GetUserByTag(string createRequest)
        {
            //TODO: implement GetUserByTag logic for rmq
            throw new NotImplementedException();
        }
    }
}