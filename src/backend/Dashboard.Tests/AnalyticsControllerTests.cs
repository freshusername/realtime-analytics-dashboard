using Dashboard.API.Controllers;
using Dashboard.API.Models;
using Dashboard.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dashboard.Tests;

public class AnalyticsControllerTests
{
    [Fact]
    public void GetActiveUsers_ShouldReturnCurrentActiveUsers()
    {
        // Arrange
        var mockService = new Mock<AnalyticsService>();
        mockService.Setup(s => s.CurrentActiveUsers).Returns(new ActiveUser { Count = 100, Timestamp = DateTime.Now });
        var controller = new AnalyticsController(mockService.Object);

        // Act
        var result = controller.GetActiveUsers() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var activeUsers = result.Value as ActiveUser;
        Assert.NotNull(activeUsers);
        Assert.Equal(100, activeUsers.Count);
    }

    [Fact]
    public void GetTotalSales_ShouldReturnCurrentTotalSales()
    {
        // Arrange
        var mockService = new Mock<AnalyticsService>();
        mockService.Setup(s => s.CurrentTotalSales).Returns(new TotalSales { Amount = 5000, Timestamp = DateTime.Now });
        var controller = new AnalyticsController(mockService.Object);

        // Act
        var result = controller.GetTotalSales() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var totalSales = result.Value as TotalSales;
        Assert.NotNull(totalSales);
        Assert.Equal(5000, totalSales.Amount);
    }

    [Fact]
    public void GetTopProducts_ShouldReturnCurrentTopProducts()
    {
        // Arrange
        var mockService = new Mock<AnalyticsService>();
        var products = new List<TopSellingProduct>
        {
            new TopSellingProduct { Name = "Product A", QuantitySold = 100 },
            new TopSellingProduct { Name = "Product B", QuantitySold = 200 }
        };
        mockService.Setup(s => s.CurrentTopProducts).Returns(products);
        var controller = new AnalyticsController(mockService.Object);

        // Act
        var result = controller.GetTopProducts() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var topProducts = result.Value as List<TopSellingProduct>;
        Assert.NotNull(topProducts);
        Assert.Equal(2, topProducts.Count);
        Assert.Equal("Product A", topProducts[0].Name);
        Assert.Equal(100, topProducts[0].QuantitySold);
    }
}