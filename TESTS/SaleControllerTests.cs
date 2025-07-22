using System.Net.Http.Json;
using ConcessionariaCarvalho;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TESTS
{
    public class SaleControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SaleControllerTests(WebApplicationFactory<Program> factory)
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
        public async Task GetAll_Sales_ShouldReturnListOrEmpty()
        {
            var client = _factory.CreateClient();
            var token = await GetAdminTokenAsync();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("/api/Sale/sales");


            var statusCode = response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();


            (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                .Should().BeTrue($"Expected 200 or 404, but got {statusCode}. Content: {content}");
        }

        private class LoginResponse
        {
            public string token { get; set; } = string.Empty;
            public string email { get; set; } = string.Empty;
        }
    }
}
