using Microsoft.EntityFrameworkCore.Query;
using Moq;
using UserServiceApi.Service;
using UserServiceApi.Request_Responce;
using Xunit.Abstractions;
using Model;
using UserServiceApi.Controllers;

namespace UserServiceApi.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public async Task<List<UsersTable>> InitialSetup()
    {
        var userList = new List<UsersTable>
            {
                new UsersTable { Id = 1, Email = "user1@example.com", Password = "password1", UserName = "user1", Gender = "male" },
                new UsersTable { Id = 2, Email = "user2@example.com", Password = "password2", UserName = "user2", Gender = "female" },
            };

        return userList;
    }
    [Fact]
    public async void EnpointTestGetAllUsers()
    {
        // Arrange
        var mock = new Mock<IUserService>();

        var userList = await InitialSetup();

        var respons = new GeneralResponse(200, "Success", userList);

        mock.Setup(client => client.GetAllUser()).ReturnsAsync(respons);

        var userController = new UserServiceController(mock.Object);

        // Act
        var result = await userController.GetAllUser();

        // Assert
        Assert.Equal(2, result._users?.Count);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void EnpointTestGetUser()
    {
        // Arrange
        var mock = new Mock<IUserService>();

        var userList = await InitialSetup();

        var resp = new GeneralResponse(200, "Success", userList);

        mock.Setup(client => client.GetUser(It.IsAny<UserRequest>())).ReturnsAsync(resp);

        var userController = new UserServiceController(mock.Object);

        // Act
        var result = await userController.GetUser(new UserRequest("user1"));
        // Assert
        Assert.Equal("user1", result._users?[0].UserName);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }

    [Fact]
    public async void EnpointTestGetUserByTag()
    {
        // Arrange
        var mock = new Mock<IUserService>();

        var userList = await InitialSetup();

        var resp = new GeneralResponse(200, "Success", userList);

        mock.Setup(client => client.GetUserByTag(It.IsAny<UserRequest>())).ReturnsAsync(resp);

        var userController = new UserServiceController(mock.Object);

        // Act
        var result = await userController.GetUserByTag(new UserRequest("female"));
        // Assert
        Assert.Equal("female", result._users?[1].Gender);
        Assert.Equal(200, result._status);
        Assert.Equal("Success", result._message);
    }
}