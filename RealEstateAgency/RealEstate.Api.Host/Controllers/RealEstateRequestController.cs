using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts.RealEstateRequests;
using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Application.Contracts.RealEstateObjects;

namespace RealEstate.Api.Host.Controllers;

/// <summary>
/// API controller for CRUD operations over real-estate requests,
/// and for retrieving entities referenced by a request (counterparty and real-estate object).
/// </summary>
/// <param name="appService">Application service for real-estate requests.</param>
/// <param name="logger">Logger instance.</param>
[Route("api/[controller]")]
[ApiController]
public class RealEstateRequestController(
    IRealEstateRequestService appService,
    ILogger<RealEstateRequestController> logger)
    : CrudControllerBase<RealEstateRequestDto, RealEstateRequestCreateUpdateDto, int>(appService, logger)
{
    /// <summary>
    /// Gets a counterparty referenced by the request (by counterparty id).
    /// </summary>
    /// <param name="id">Counterparty identifier.</param>
    /// <returns>Counterparty DTO.</returns>
    [HttpGet("counterparty/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CounterpartyDto>> GetCounterparty([FromRoute] int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetCounterparty), GetType().Name, id);

        try
        {
            var res = await appService.GetCounterparty(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetCounterparty), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetCounterparty), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Gets a real-estate object referenced by the request (by real-estate object id).
    /// </summary>
    /// <param name="id">Real-estate object identifier.</param>
    /// <returns>Real-estate object DTO.</returns>
    [HttpGet("real-estate-object/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<RealEstateObjectDto>> GetRealEstate([FromRoute] int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetRealEstate), GetType().Name, id);

        try
        {
            var res = await appService.GetRealEstate(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetRealEstate), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetRealEstate), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}