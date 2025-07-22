using System.Net.Http.Json;
using System.Text;
using ConcessionariaCarvalho;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace TESTS
{
    public class SalesPersonControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SalesPersonControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        private async Task<string> GetAdminTokenAsync()
        {
            var client = _factory.CreateClient();
            var loginRequest = new
            {
                Email = "Admin@gmail.com",
                Password = "Admin123"
            };

            var response = await client.PostAsJsonAsync("/api/AdminAuth/login", loginRequest);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!.token;
        }

        [Fact]
        public async Task Register_SalesPerson_WithValidAdminToken_ShouldReturnSuccess()
        {
            var client = _factory.CreateClient();
            var token = await GetAdminTokenAsync();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var registerRequest = new
            {
                Name = "João Silva",
                Email = $"joao{timestamp}@test.com",
                Password = "Test123",
                Phone = "(11) 99999-9999",
                Cpf = $"{(timestamp % 900 + 100):D3}.{(timestamp % 900 + 100):D3}.{(timestamp % 900 + 100):D3}-{(timestamp % 90 + 10):D2}"
            };

            var response = await client.PostAsJsonAsync("/api/SalesPerson/register", registerRequest);


            var statusCode = response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue($"Status: {statusCode}, Content: {content}");
        }

        [Fact]
        public async Task Register_SalesPerson_WithoutToken_ShouldReturnUnauthorized()
        {
            var client = _factory.CreateClient();
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var unique = timestamp.ToString().Substring(5);
            var registerRequest = new
            {
                Name = "João Silva",
                Email = $"joao{timestamp}@test.com",
                Password = "Test123",
                Phone = "11999999999",
                Cpf = $"123.456.{unique.PadLeft(3, '0')}-{(timestamp % 100).ToString().PadLeft(2, '0')}"
            };

            var response = await client.PostAsJsonAsync("/api/SalesPerson/register", registerRequest);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Register_SalesPerson_WithInvalidToken_ShouldReturnUnauthorized()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "token_invalido");

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var registerRequest = new
            {
                Name = "João Silva",
                Email = $"joao{timestamp}@test.com",
                Password = "Test123",
                Phone = "(11) 99999-9999",
                Cpf = $"{(timestamp % 900 + 100):D3}.{(timestamp % 900 + 100):D3}.{(timestamp % 900 + 100):D3}-{(timestamp % 90 + 10):D2}"
            };

            var response = await client.PostAsJsonAsync("/api/SalesPerson/register", registerRequest);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        private class LoginResponse
        {
            public string token { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
        }
    }
}
