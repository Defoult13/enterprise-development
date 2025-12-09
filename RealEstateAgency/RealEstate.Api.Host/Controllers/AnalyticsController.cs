using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts;
using RealEstate.Application.Contracts.Analytics;
using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Api.Host.Controllers;

/// <summary>
/// API controller for aggregated, read-only analytics queries over requests, clients and properties.
/// </summary>
/// <param name="analyticsService">Analytics service implementation.</param>
/// <param name="logger">Logger instance.</param>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Returns all sellers who created SELL requests within the given period.
    /// </summary>
    /// <param name="from">Start date.</param>
    /// <param name="to">End date (exclusive).</param>
    /// <returns>List of sellers as counterparty DTOs.</returns>
    [HttpGet("sellers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CounterpartyDto>>> GetSellersByPeriod([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        logger.LogInformation("{method} method of {controller} is called with from={from}, to={to}", nameof(GetSellersByPeriod), GetType().Name, from, to);

        if (to <= from)
            return BadRequest("Parameter 'to' must be greater than 'from' (exclusive upper bound).");

        try
        {
            var res = await analyticsService.GetSellersByPeriod(from, to);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetSellersByPeriod), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetSellersByPeriod), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Returns top 5 clients by number of requests, separately for BUY and SELL.
    /// </summary>
    /// <returns>List of request types with top clients and their request counts.</returns>
    [HttpGet("top-clients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TopClientsByRequestTypeDto>>> GetTop5ClientsByRequestType()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetTop5ClientsByRequestType), GetType().Name);

        try
        {
            var res = await analyticsService.GetTop5ClientsByRequestType();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5ClientsByRequestType), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetTop5ClientsByRequestType), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Returns counts of requests grouped by property type.
    /// </summary>
    /// <returns>List of property types with request counts.</returns>
    [HttpGet("requests-by-property-type")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<RequestCountByPropertyTypeDto>>> GetRequestCountsByPropertyType()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetRequestCountsByPropertyType), GetType().Name);

        try
        {
            var res = await analyticsService.GetRequestCountsByPropertyType();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetRequestCountsByPropertyType), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetRequestCountsByPropertyType), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Returns all clients who created requests with the minimal amount across all requests.
    /// </summary>
    /// <returns>List of clients as counterparty DTOs.</returns>
    [HttpGet("min-amount-clients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CounterpartyDto>>> GetClientsWithMinimumRequestAmount()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetClientsWithMinimumRequestAmount), GetType().Name);

        try
        {
            var res = await analyticsService.GetClientsWithMinimumRequestAmount();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetClientsWithMinimumRequestAmount), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetClientsWithMinimumRequestAmount), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Returns all clients who are looking to BUY a property of the given type, ordered by full name.
    /// </summary>
    /// <param name="propertyType">Target property type.</param>
    /// <returns>List of buyers as counterparty DTOs.</returns>
    [HttpGet("buyers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CounterpartyDto>>> GetBuyersByPropertyType([FromQuery] PropertyType propertyType)
    {
        logger.LogInformation("{method} method of {controller} is called with propertyType={propertyType}", nameof(GetBuyersByPropertyType), GetType().Name, propertyType);

        try
        {
            var res = await analyticsService.GetBuyersByPropertyType(propertyType);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetBuyersByPropertyType), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetBuyersByPropertyType), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}