using Dashboard.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsController(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("active-users")]
    public IActionResult GetActiveUsers()
    {
        return Ok(_analyticsService.CurrentActiveUsers);
    }

    [HttpGet("total-sales")]
    public IActionResult GetTotalSales()
    {
        return Ok(_analyticsService.CurrentTotalSales);
    }

    [HttpGet("top-products")]
    public IActionResult GetTopProducts()
    {
        return Ok(_analyticsService.CurrentTopProducts);
    }
}