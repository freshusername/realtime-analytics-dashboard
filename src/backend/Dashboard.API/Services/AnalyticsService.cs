using Dashboard.API.Models;

namespace Dashboard.API.Services;

/// <summary>
/// Used to generate real-time analytics data for active users, total sales and top-selling products
/// </summary>
public class AnalyticsService
{
    private readonly Random _random = new Random();
    private readonly List<TopSellingProduct> _products;
    private Timer _activeUsersTimer;
    private Timer _totalSalesTimer;
    private Timer _topProductsTimer;

    public virtual ActiveUser CurrentActiveUsers { get; private set; }
    public virtual TotalSales CurrentTotalSales { get; private set; }
    public virtual List<TopSellingProduct> CurrentTopProducts { get; private set; }

    public virtual event Action<ActiveUser> OnActiveUsersUpdated;
    public virtual event Action<TotalSales> OnTotalSalesUpdated;
    public virtual event Action<List<TopSellingProduct>> OnTopSellingProductsUpdated;

    public AnalyticsService()
    {
        _products = new List<TopSellingProduct>
        {
            new() { Name = "Product A", QuantitySold = 0 },
            new() { Name = "Product B", QuantitySold = 0 },
            new() { Name = "Product C", QuantitySold = 0 }
        };

        _activeUsersTimer = new Timer(SimulateActiveUsers, null, 0, 10000); // 10 seconds
        _totalSalesTimer = new Timer(SimulateTotalSales, null, 0, 10000); // 10 seconds
        _topProductsTimer = new Timer(SimulateTopProducts, null, 0, 30000); // 30 seconds
    }

    internal void SimulateActiveUsers(object state)
    {
        var activeUsers = new ActiveUser
        {
            Count = _random.Next(1, 1000),
            Timestamp = DateTime.Now
        };
        CurrentActiveUsers = activeUsers;
        OnActiveUsersUpdated?.Invoke(activeUsers);
    }

    internal void SimulateTotalSales(object state)
    {
        var totalSales = new TotalSales
        {
            Amount = _random.Next(1, 100000),
            Timestamp = DateTime.Now
        };
        CurrentTotalSales = totalSales;
        OnTotalSalesUpdated?.Invoke(totalSales);
    }

    internal void SimulateTopProducts(object state)
    {
        foreach (var product in _products)
        {
            product.QuantitySold = _random.Next(1, 500);
        }
        CurrentTopProducts = new List<TopSellingProduct>(_products);
        OnTopSellingProductsUpdated?.Invoke(_products);
    }
}