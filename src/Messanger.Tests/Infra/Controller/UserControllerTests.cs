using FluentAssertions;
using Messanger.API.Dtos.Users;
using Messanger.Tests.Fixtures;
using System.Net;
using System.Net.Http.Json;

namespace Messanger.Tests.Infra.Controller;

public class UserControllerTests
{
    public readonly HttpClient _client;

    public UserControllerTests()
    {
        ApiTestFactory factory = new ApiTestFactory();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostUsers_ShouldCreateUser_WhenRequestIsValid()
    {
        //Arrange
        var request = new CreateUserDto
        {
            Username = "testuser",
            Password = "TestPassword123!",
            RePassword = "TestPassword123!"
        };

        //Act
        var response = await _client.PostAsJsonAsync("/store", request);

        //Assert
        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
