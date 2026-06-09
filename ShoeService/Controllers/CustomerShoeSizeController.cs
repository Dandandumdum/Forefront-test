using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using ShoeService.Models;
using ShoeService.Db;
using ShoeService.Services;

namespace ShoeService.Controllers;

/// <summary>
/// A service that provides information about customer shoe sizes.
/// 
/// Author: Kalle P. Hackare (kph@backfront.localhost)
/// </summary>
[ApiController]
[Route("api")]
public class CustomerShoeSizeController : ControllerBase
{
    private readonly CustomerRepository _customerRepository;
    private readonly CustomerShoeService _customerShoeService;

    private readonly StatisticsService _statisticsService;
    private const string DATE_FORMAT = "yyyy-MM-dd";

    public CustomerShoeSizeController(CustomerRepository customerRepository, CustomerShoeService customerShoeService, StatisticsService statisticsService)
    {
        _customerRepository = customerRepository;
        _customerShoeService = customerShoeService;
        _statisticsService = statisticsService;
    }

    /// <summary>
    /// Returns the shoe size of the provided customer in the requested format
    /// </summary>
    /// Changed to Post with a Body as more secure

    /// <returns>the found and format-converted customer shoe size</returns>
    [HttpPost]
    [Route("shoe-size")]
    [Produces("application/json")]
    public async Task<ActionResult<CustomerShoeSize>> GetShoeSize(
        [FromBody] CustomerRequest request)
    {

        if (request == null || string.IsNullOrEmpty(request.CustomerIdentification) || string.IsNullOrEmpty(request.SizeFormat))
        {
            return BadRequest("Missing required fields");
        }

        CustomerShoeSize? customerShoeSize = await _customerShoeService.GetCustomerShoeSize(request);
        if (customerShoeSize == null)
        {
            return BadRequest("Invalid size format");
        }

        await _statisticsService.RegisterStatistic("requests", 1);

        return Ok(customerShoeSize);
        
    }

}