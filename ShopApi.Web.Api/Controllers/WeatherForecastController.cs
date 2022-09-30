using Microsoft.AspNetCore.Mvc;
using ShopApi.Contracts;
using ShopApi.Entities.Models;

namespace ShopApi.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILoggerManager _logger;
    private readonly IRepositoryManager _repositoryManager;


    public WeatherForecastController(ILoggerManager logger, IRepositoryManager repositoryManager)
    {
        _logger = logger;
        _repositoryManager = repositoryManager;
    }
    
    public IEnumerable<Product> Get()
    {
        var result = _repositoryManager.Product.FindAll(false);
        return result.AsEnumerable();
    }
}