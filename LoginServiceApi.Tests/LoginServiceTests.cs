using EFramework.Data;
using LoginServiceApi.Request_Responce;
using LoginServiceApi.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;
using Moq;
using Moq.EntityFrameworkCore;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using Xunit.Abstractions;

namespace LoginServiceApi.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    [Fact]
    public async void EndpointTestsLogin()
    {
        // Arrange
        var mock = new Mock<ILoginService>();

        var generalResponse = new GeneralResponce(200, "Success");

        mock.Setup(client => client.Login(It.IsAny<LoginRequest>())).ReturnsAsync(generalResponse);

        var loginController = new LoginServiceController(mock.Object);

        // Act
        var result = await loginController.Login(new LoginRequest("test", "test"));

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void EndpointTestsCreate()
    {
        // Arrange
        var mock = new Mock<ILoginService>();

        var generalResponse = new GeneralResponce(200, "Success");

        mock.Setup(client => client.CreateAccount(It.IsAny<CreateRequest>())).ReturnsAsync(generalResponse);

        var loginController = new LoginServiceController(mock.Object);

        // Act
        var result = await loginController.CreateAccount(new CreateRequest("test", "test"));

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void EndpointTestsUpdate()
    {
        // Arrange
        var mock = new Mock<ILoginService>();

        var generalResponse = new GeneralResponce(200, "Success");

        mock.Setup(client => client.UpdateAccount(It.IsAny<UpdateRequest>())).ReturnsAsync(generalResponse);

        var loginController = new LoginServiceController(mock.Object);

        // Act
        var result = await loginController.UpdateAccount(new UpdateRequest{
            Id = 1, 
            Email = "test", 
            Password = "test", 
            UserName = "test", 
            Mobile = "test", 
            Address = "test", 
            FirstName = "test", 
            LastName = "test", 
            Gender = "test"
        });

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void EndpointTestsDelete()
    {
        // Arrange
        var mock = new Mock<ILoginService>();

        var generalResponse = new GeneralResponce(200, "Success");

        mock.Setup(client => client.DeleteAccount(It.IsAny<DeleteRequest>())).ReturnsAsync(generalResponse);

        var loginController = new LoginServiceController(mock.Object);

        // Act
        var result = await loginController.DeleteAccount(new DeleteRequest("test", "test",1));

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void TestLoginServiceLogin()
    {
        // Arrange
        var mock = new Mock<AGWDbContext>();

        var user = new List<UsersTable>{
            new UsersTable{
                Id = 1,
                Email = "test",
                Password = "test",
                UserName = "test",
                Mobile = "test",
                Address = "test",
                FirstName = "test",
                LastName = "test",
                Gender = "test"
            }};

        mock.Setup(m => m.usersTables).ReturnsDbSet(user);

        var loginController = new LoginService(mock.Object);

        // Act
        var result = await loginController.Login(new LoginRequest("test", "test"));

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }
    [Fact]
    public async void TestLoginServiceCreate()
    {
        // Arrange
        var mock = new Mock<AGWDbContext>();

        var loginController = new LoginService(mock.Object);

        // Act
        var result = await loginController.CreateAccount(new CreateRequest("test", "test"));

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }
    [Fact]
    public async void TestLoginServiceUpdate()
    {
        // Arrange
        var mock = new Mock<AGWDbContext>();

        var loginController = new LoginService(mock.Object);

        // Act
        var result = await loginController.UpdateAccount(new UpdateRequest{
            Id = 1, 
            Email = "test", 
            Password = "test", 
            UserName = "test2", 
            Mobile = "test2", 
            Address = "test2", 
            FirstName = "test2", 
            LastName = "test2", 
            Gender = "test2"
        });

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }
    [Fact]
    public async void TestLoginServiceDelete()
    {
        // Arrange
        var mock = new Mock<AGWDbContext>();

        var loginController = new LoginService(mock.Object);

        // Act
        var result = await loginController.DeleteAccount(new DeleteRequest("test", "test",1));

        // Assert 
        _testOutputHelper.WriteLine(result._status.ToString());
        _testOutputHelper.WriteLine(result._message);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }
}