using Dashboard.API.Services;

namespace Dashboard.Tests;

public class AnalyticsServiceTests
{
    [Fact]
    public void SimulateActiveUsers_ShouldUpdateCurrentActiveUsers()
    {
        // Arrange
        var service = new AnalyticsService();

        // Act
        service.OnActiveUsersUpdated += (activeUsers) =>
        {
            // Assert
            Assert.NotNull(activeUsers);
            Assert.InRange(activeUsers.Count, 1, 1000);
        };

        // Simulate the data
        var timer = new Timer(service.SimulateActiveUsers, null, 0, Timeout.Infinite);
        Thread.Sleep(100); // Give it some time to update
    }

    [Fact]
    public void SimulateTotalSales_ShouldUpdateCurrentTotalSales()
    {
        // Arrange
        var service = new AnalyticsService();

        // Act
        service.OnTotalSalesUpdated += (totalSales) =>
        {
            // Assert
            Assert.NotNull(totalSales);
            Assert.InRange(totalSales.Amount, 1, 100000);
        };

        // Simulate the data
        var timer = new Timer(service.SimulateTotalSales, null, 0, Timeout.Infinite);
        Thread.Sleep(100); // Give it some time to update
    }

    [Fact]
    public void SimulateTopProducts_ShouldUpdateCurrentTopProducts()
    {
        // Arrange
        var service = new AnalyticsService();

        // Act
        service.OnTopSellingProductsUpdated += (products) =>
        {
            // Assert
            Assert.NotNull(products);
            foreach (var product in products)
            {
                Assert.InRange(product.QuantitySold, 1, 500);
            }
        };

        // Simulate the data
        var timer = new Timer(service.SimulateTopProducts, null, 0, Timeout.Infinite);
        Thread.Sleep(100); // Give it some time to update
    }
}