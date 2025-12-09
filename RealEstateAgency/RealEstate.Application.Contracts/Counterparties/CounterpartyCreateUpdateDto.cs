namespace RealEstate.Application.Contracts.Counterparties;

/// <summary>
/// DTO for creating or updating a counterparty.
/// </summary>
/// <param name="FullName">Full name.</param>
/// <param name="PassportNumber">Passport number.</param>
/// <param name="Phone">Phone number.</param>
public sealed record CounterpartyCreateUpdateDto(
    string FullName,
    string PassportNumber,
    string Phone
);