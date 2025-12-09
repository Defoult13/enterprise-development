using AutoMapper;
using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Application.Contracts.RealEstateObjects;
using RealEstate.Application.Contracts.RealEstateRequests;
using RealEstate.Domain.Models;

namespace RealEstate.Application;

/// <summary>
/// AutoMapper configuration for mapping between domain entities and DTOs used by the application layer.
/// </summary>
public class RealEstateMappingProfile : Profile
{
    /// <summary>
    /// Initializes mapping rules for entities and DTOs.
    /// </summary>
    public RealEstateMappingProfile()
    {
        CreateMap<Counterparty, CounterpartyDto>();
        CreateMap<CounterpartyCreateUpdateDto, Counterparty>();

        CreateMap<RealEstateObject, RealEstateObjectDto>();
        CreateMap<RealEstateObjectCreateUpdateDto, RealEstateObject>();

        CreateMap<RealEstateRequest, RealEstateRequestDto>();
        CreateMap<RealEstateRequestCreateUpdateDto, RealEstateRequest>();
    }
}