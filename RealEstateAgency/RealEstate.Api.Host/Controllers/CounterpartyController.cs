using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Application.Contracts.RealEstateRequests;

namespace RealEstate.Api.Host.Controllers;

/// <summary>
/// API controller for CRUD operations over counterparties.
/// Inherits all endpoints from <see cref="CrudControllerBase{TDto,TCreateUpdateDto,TKey}"/>.
/// </summary>
/// <param name="appService">Application service for counterparties.</param>
/// <param name="logger">Logger instance.</param>
[Route("api/[controller]")]
[ApiController]
public class CounterpartyController(
    ICounterpartyService appService,
    ILogger<CounterpartyController> logger)
    : CrudControllerBase<CounterpartyDto, CounterpartyCreateUpdateDto, int>(appService, logger)
{
    /// <summary>
    /// Gets all requests created by the specified counterparty.
    /// </summary>
    /// <param name="id">Counterparty identifier.</param>
    [HttpGet("{id}/requests")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<RealEstateRequestDto>>> GetRealEstateRequests([FromRoute] int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetRealEstateRequests), GetType().Name, id);

        try
        {
            var res = await appService.GetRealEstateRequests(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetRealEstateRequests), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Not found during {method} method of {controller} for id={id}",nameof(GetRealEstateRequests), GetType().Name, id);

            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetRealEstateRequests), GetType().Name);

            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}