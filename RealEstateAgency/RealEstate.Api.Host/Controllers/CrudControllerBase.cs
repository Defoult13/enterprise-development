using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts;

namespace RealEstate.Api.Host.Controllers;

/// <summary>
/// Generic base controller for CRUD endpoints over a DTO type.
/// Delegates all operations to <see cref="IApplicationService{TDto, TCreateUpdateDto, TKey}"/> and provides
/// basic logging and HTTP status code mapping.
/// </summary>
/// <typeparam name="TDto">DTO type returned by the API.</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO type used for create and update operations.</typeparam>
/// <typeparam name="TKey">Entity/DTO key type.</typeparam>
/// <param name="appService">Application service that performs CRUD operations.</param>
/// <param name="logger">Logger instance for request tracing and error reporting.</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new entity from the provided DTO.
    /// </summary>
    /// <param name="newDto">DTO with data required to create a new entity.</param>
    /// <returns>201 Created with the created DTO; 500 on unexpected errors.</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {@dto} parameter", nameof(Create), GetType().Name, newDto);
        try
        {
            var res = await appService.Create(newDto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(this.Create), res);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Create), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Updates an existing entity by id using the provided DTO.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <param name="newDto">DTO with data required to update the entity.</param>
    /// <returns>200 OK with the updated DTO; 404 if the entity was not found; 500 on unexpected errors.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Edit([FromRoute] TKey id, [FromBody] TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{@dto} parameters", nameof(Edit), GetType().Name, id, newDto);
        try
        {
            var res = await appService.Update(newDto, id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Edit), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Edit), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Deletes an entity by id.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <returns>200 OK if deleted; 204 No Content if nothing was deleted; 500 on unexpected errors.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete([FromRoute] TKey id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            var res = await appService.Delete(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Delete), GetType().Name);
            return res ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Delete), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Returns all entities.
    /// </summary>
    /// <returns>200 OK with the list of DTOs; 500 on unexpected errors.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var res = await appService.GetAll();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAll), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Returns an entity by id.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <returns>200 OK with the DTO; 404 if not found; 500 on unexpected errors.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get([FromRoute] TKey id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var res = await appService.Get(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return res is null
                ? NotFound($"Entity with id={id} was not found.")
                : Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Get), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}