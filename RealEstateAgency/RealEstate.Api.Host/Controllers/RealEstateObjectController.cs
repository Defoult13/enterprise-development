using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts;
using RealEstate.Application.Contracts.RealEstateObjects;

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
    IApplicationService<RealEstateObjectDto, RealEstateObjectCreateUpdateDto, int> appService,
    ILogger<RealEstateObjectController> logger)
    : CrudControllerBase<RealEstateObjectDto, RealEstateObjectCreateUpdateDto, int>(appService, logger);