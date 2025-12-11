using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts;
using RealEstate.Application.Contracts.RealEstateRequests;

namespace RealEstate.Api.Host.Controllers;

/// <summary>
/// API controller for CRUD operations over real-estate requests.
/// </summary>
/// <param name="appService">Application service for real-estate requests.</param>
/// <param name="logger">Logger instance.</param>
[Route("api/[controller]")]
[ApiController]
public class RealEstateRequestController(
    IApplicationService<RealEstateRequestDto, RealEstateRequestCreateUpdateDto, int> appService,
    ILogger<RealEstateRequestController> logger)
    : CrudControllerBase<RealEstateRequestDto, RealEstateRequestCreateUpdateDto, int>(appService, logger);