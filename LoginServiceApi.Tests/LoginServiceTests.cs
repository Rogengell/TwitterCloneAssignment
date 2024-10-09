using LoginServiceApi.Request_Responce;
using LoginServiceApi.Service;
using Microsoft.IdentityModel.Tokens;
using Moq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace LoginServiceApi.Tests;

public class UnitTest1
{
    private WireMockServer _server;
    public async Task InitializeAsync()
    {

        _server = WireMockServer.Start(
            new WireMockServerSettings
            {
                Urls = new[] { "http://localhost:8082" }
            }
        );

        _server.Given(
            Request.Create().WithPath("/LoginService/Login").UsingPost()
        ).RespondWith(
            Response.Create().WithStatusCode(200)
        );
    }

    public async Task DisposeAsync()
    {
        _server.Stop();
    }

    [Fact]
    public async void EndpointTests()
    {
        // Arrange
        var mock = new Mock<LoginService>();

        // mock.Setup(
        //     client => client.Login("test", "test")
        // );

        var loginController = new LoginServiceController(mock.Object);
        // Act
        var result = await loginController.Login(new LoginRequest("test", "test"));
        // Assert 
        // Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }
}