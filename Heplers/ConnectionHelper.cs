using EasyNetQ;
using EasyNetQ.DI;
using EasyNetQ.Serialization;
using EasyNetQ.Serialization.SystemTextJson;

namespace ApiGateWay;

public class ConnectionHelper
{

    public IBus GetRMQConnection()
    {
        return RabbitHutch.CreateBus("host=rmq;username=application;password=pepsi",
            serviceRegister => serviceRegister.Register<ISerializer, SystemTextJsonSerializer>());
    }
}