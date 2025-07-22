using System.Net.Http.Json;
using ConcessionariaCarvalho;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TESTS
{
    public class GuestControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GuestControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Register_Guest_WithValidData_ShouldReturnSuccess()
        {
            var client = _factory.CreateClient();
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var registerRequest = new
            {
                Name = "Maria Silva",
                Email = $"maria{timestamp}@test.com",
                Password = "Test123",
                Phone = "(11) 99999-9999",
                Cpf = $"{(timestamp % 900 + 100):D3}.{(timestamp % 900 + 100):D3}.{(timestamp % 900 + 100):D3}-{(timestamp % 90 + 10):D2}"
            };

            var response = await client.PostAsJsonAsync("/api/Guest/register", registerRequest);


            var statusCode = response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue($"Status: {statusCode}, Content: {content}");
        }
        [Fact]
        public async Task Register_Guest_WithInvalidData_ShouldReturnBadRequest()
        {
            var client = _factory.CreateClient();
            var registerRequest = new
            {
                Name = "",
                Email = "email_invalido",
                Password = "",
                Phone = "",
                Cpf = ""
            };

            var response = await client.PostAsJsonAsync("/api/Guest/register", registerRequest);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_Guest_WithValidCredentials_ShouldReturnToken()
        {
            var client = _factory.CreateClient();
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var unique = timestamp.ToString().Substring(5);


            var registerRequest = new
            {
                Name = "Pedro Santos",
                Email = $"pedro{timestamp}@test.com",
                Password = "Test123",
                Phone = "(11) 99999-9999",
                Cpf = $"{(timestamp % 900 + 100):D3}.{(timestamp % 900 + 100):D3}.{(timestamp % 900 + 100):D3}-{(timestamp % 90 + 10):D2}"
            };
            var registerResponse = await client.PostAsJsonAsync("/api/Guest/register", registerRequest);


            registerResponse.IsSuccessStatusCode.Should().BeTrue($"Registration failed with status: {registerResponse.StatusCode}, content: {await registerResponse.Content.ReadAsStringAsync()}");


            var loginRequest = new
            {
                Email = $"pedro{timestamp}@test.com",
                Password = "Test123"
            };

            var response = await client.PostAsJsonAsync("/api/Guest/login", loginRequest);


            var statusCode = response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue($"Status: {statusCode}, Content: {content}");

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            result.Should().NotBeNull();
            result!.token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Login_Guest_WithInvalidCredentials_ShouldReturnUnauthorized()
        {
            var client = _factory.CreateClient();
            var loginRequest = new
            {
                Email = "naoexiste@test.com",
                Password = "senhaerrada"
            };

            var response = await client.PostAsJsonAsync("/api/Guest/login", loginRequest);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        private class LoginResponse
        {
            public string token { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
        }
    }
}
