using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts.RealEstateObjects;
using RealEstate.Application.Contracts.RealEstateRequests;

namespace RealEstate.Api.Host.Controllers;

/// <summary>
/// API controller for CRUD operations over real-estate objects.
/// Inherits all endpoints from <see cref="CrudControllerBase{TDto,TCreateUpdateDto,TKey}"/>.
/// </summary>
/// <param name="appService">Application service for real-estate objects.</param>
/// <param name="logger">Logger instance.</param>
[Route("api/[controller]")]
[ApiController]
public class RealEstateObjectController(
    IRealEstateObjectService appService,
    ILogger<RealEstateObjectController> logger)
    : CrudControllerBase<RealEstateObjectDto, RealEstateObjectCreateUpdateDto, int>(appService, logger)
{
    /// <summary>
    /// Gets all requests related to the specified real-estate object.
    /// </summary>
    /// <param name="id">Real-estate object identifier.</param>
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
            logger.LogWarning(ex, "Not found during {method} method of {controller} for id={id}", nameof(GetRealEstateRequests), GetType().Name, id);

            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetRealEstateRequests), GetType().Name);

            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}