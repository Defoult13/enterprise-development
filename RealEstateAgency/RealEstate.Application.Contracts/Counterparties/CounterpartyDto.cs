namespace RealEstate.Application.Contracts.Counterparties;

/// <summary>
/// DTO for getting counterparty data.
/// </summary>
/// <param name="Id">Counterparty id.</param>
/// <param name="FullName">Full name.</param>
/// <param name="PassportNumber">Passport number.</param>
/// <param name="Phone">Phone number.</param>
public sealed record CounterpartyDto(
    int Id,
    string FullName,
    string PassportNumber,
    string Phone
);