using Bogus;
using RealEstate.Application.Contracts.RealEstateRequests;
using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Generator.Kafka.Host.Generator;

/// <summary>
/// Generates randomized <see cref="RealEstateRequestCreateUpdateDto"/> instances for Kafka publishing.
/// </summary>
public class RequestGenerator
{
    /// <summary>
    /// Generates a list of <see cref="RealEstateRequestCreateUpdateDto"/> instances.
    /// </summary>
    /// <param name="count">Number of DTOs to generate.</param>
    /// <returns>Generated list of request DTOs.</returns>
    public static IList<RealEstateRequestCreateUpdateDto> Generate(int count)
    {
        var from = DateTime.Today.AddYears(-2);
        var to = DateTime.Today;

        return new Faker<RealEstateRequestCreateUpdateDto>()
            .CustomInstantiator(f =>
            {
                var createdAt = f.Date.Between(from, to);
                return new RealEstateRequestCreateUpdateDto(
                    ClientId: f.Random.Int(1, 24),
                    PropertyId: f.Random.Int(1, 24),
                    Type: f.PickRandom<RequestType>(),
                    Amount: f.Random.Decimal(1_000_000m, 20_000_000m),
                    CreatedAt: DateOnly.FromDateTime(createdAt)
                );
            })
            .Generate(count);
    }
}