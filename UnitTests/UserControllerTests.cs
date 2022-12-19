using Application.DTO;
using Application.DTO.User;
using Faker;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class UserControllerTests
    {
        private readonly HttpClient _client;

        public UserControllerTests()
        {
            var _application = new Application();
            _client = _application.CreateClient();

        }

        [Fact]
        public async Task ShouldCreateUser()
        {
            UserCreateDTO userCreateDTO = new();
            userCreateDTO.Email = Internet.Email();
            userCreateDTO.Password = "!Admin123";
            var response = await _client.PostAsJsonAsync("/api/user", userCreateDTO);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseText = await response.Content.ReadAsStringAsync();
            UserBaseDTO user = JsonConvert.DeserializeObject<UserBaseDTO>(responseText);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            user.Should().NotBeNull();
            user.Email.Should().Be(userCreateDTO.Email);

        }

        [Fact]
        public async Task ShouldGetUser()
        {
            var response = await _client.GetAsync("/api/user/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseText = await response.Content.ReadAsStringAsync();
            UserBaseDTO user = JsonConvert.DeserializeObject<UserBaseDTO>(responseText);

            user.Should().NotBeNull();
            user.Id.Should().Be("1");
        }

        [Fact]
        public async Task ShouldGetAllUsers()
        {
            var response = await _client.GetAsync("/api/user");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseText = await response.Content.ReadAsStringAsync();
            List<UserBaseDTO> users = JsonConvert.DeserializeObject<List<UserBaseDTO>>(responseText);

            users.Should().NotBeNull();
            users.Should().HaveCountGreaterOrEqualTo(50);
        }
    }
}