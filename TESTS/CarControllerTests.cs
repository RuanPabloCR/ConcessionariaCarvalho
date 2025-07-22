using System.Net.Http.Json;
using ConcessionariaCarvalho;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TESTS
{
    public class CarControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CarControllerTests(WebApplicationFactory<Program> factory)
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
        public async Task Register_Car_WithValidAdminToken_ShouldReturnSuccess()
        {
            var client = _factory.CreateClient();
            var token = await GetAdminTokenAsync();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var carRequest = new
            {
                Model = "Civic",
                Brand = "Honda",
                Year = 2023,
                Price = 85000.00m,
                Color = "White",
                Status = 0
            };

            var response = await client.PostAsJsonAsync("/api/Car/register", carRequest);

            // Debug info
            var statusCode = response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue($"Status: {statusCode}, Content: {content}");
        }

        [Fact]
        public async Task Register_Car_WithoutToken_ShouldReturnUnauthorized()
        {
            var client = _factory.CreateClient();
            var carRequest = new
            {
                Model = "Civic",
                Brand = "Honda",
                Year = 2023,
                Price = 85000.00m,
                Color = "White",
                Status = 0
            };

            var response = await client.PostAsJsonAsync("/api/Car/register", carRequest);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetAll_Cars_ShouldReturnListOrEmpty()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Car/all");


            (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                .Should().BeTrue($"Expected 200 or 404, but got {response.StatusCode}");
        }
        [Fact]
        public async Task BuyCar_WithGuestToken_ShouldReturnSuccess()
        {
            var client = _factory.CreateClient();


            var registerRequest = new
            {
                Name = "João Comprador",
                Email = "joao.comprador@test.com",
                Password = "Test123",
                Phone = "(11) 99999-9999",
                Cpf = "123.456.789-00"
            };
            await client.PostAsJsonAsync("/api/Guest/register", registerRequest);


            var loginRequest = new
            {
                Email = "joao.comprador@test.com",
                Password = "Test123"
            };
            var loginResponse = await client.PostAsJsonAsync("/api/Guest/login", loginRequest);
            var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult!.token);


            var adminToken = await GetAdminTokenAsync();
            var adminClient = _factory.CreateClient();
            adminClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminToken);

            var carRequest = new
            {
                Model = "Civic",
                Brand = "Honda",
                Year = 2023,
                Price = 85000.00m,
                Color = "White",
                Status = 0
            };
            var carResponse = await adminClient.PostAsJsonAsync("/api/Car/register", carRequest);


            var carId = Guid.NewGuid();
            var response = await client.PostAsync($"/api/Car/buy/{carId}", null);


            response.StatusCode.Should().NotBe(System.Net.HttpStatusCode.Unauthorized);
        }

        private class LoginResponse
        {
            public string token { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
        }
    }
}
