using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts;
using RealEstate.Application.Contracts.Counterparties;

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
    IApplicationService<CounterpartyDto, CounterpartyCreateUpdateDto, int> appService,
    ILogger<CounterpartyController> logger)
    : CrudControllerBase<CounterpartyDto, CounterpartyCreateUpdateDto, int>(appService, logger);