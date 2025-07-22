using System.Net.Http.Json;
using ConcessionariaCarvalho;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TESTS
{
    public class AdminAuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public AdminAuthControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Login_Admin_WithValidCredentials_ShouldReturnTokenAndEmail()
        {
            var client = _factory.CreateClient();
            var loginRequest = new
            {
                Email = "Admin@gmail.com",
                Password = "Admin123"
            };

            var response = await client.PostAsJsonAsync("/api/AdminAuth/login", loginRequest);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            response.IsSuccessStatusCode.Should().BeTrue();
            result.Should().NotBeNull();
            result!.token.Should().NotBeNullOrWhiteSpace();
            result.email.Should().Be("Admin@gmail.com");
        }

        [Fact]
        public async Task Login_Admin_WithInvalidEmail_ShouldReturnUnauthorized()
        {
            var client = _factory.CreateClient();
            var loginRequest = new
            {
                Email = "invalid@gmail.com",
                Password = "Admin123"
            };

            var response = await client.PostAsJsonAsync("/api/AdminAuth/login", loginRequest);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Login_Admin_WithInvalidPassword_ShouldReturnUnauthorized()
        {
            var client = _factory.CreateClient();
            var loginRequest = new
            {
                Email = "Admin@gmail.com",
                Password = "senhaerrada"
            };

            var response = await client.PostAsJsonAsync("/api/AdminAuth/login", loginRequest);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        private class LoginResponse
        {
            public string token { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
        }
    }
}
