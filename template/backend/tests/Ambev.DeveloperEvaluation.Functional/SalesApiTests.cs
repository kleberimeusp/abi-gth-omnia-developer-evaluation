using System.Net;
using System.Text;
using System.Text.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Ambev.DeveloperEvaluation.Functional
{
    public class SalesApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SalesApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateSale_ShouldReturn201Created()
        {
            // Arrange
            var sale = new
            {
                SaleNumber = "20240220001",
                SaleDate = "2024-02-20T10:30:00",
                Customer = "John Doe",
                TotalAmount = 200.50,
                Branch = "New York",
                Items = new[]
                {
                    new { ProductName = "Laptop", Quantity = 5, UnitPrice = 1000.00 }
                }
            };
            var content = new StringContent(JsonSerializer.Serialize(sale), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/sales", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetSales_ShouldReturn200Ok()
        {
            // Act
            var response = await _client.GetAsync("/api/sales");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSaleById_ShouldReturn404NotFound_WhenSaleDoesNotExist()
        {
            // Arrange
            var nonExistentSaleId = "00000000-0000-0000-0000-000000000000";

            // Act
            var response = await _client.GetAsync($"/api/sales/{nonExistentSaleId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
