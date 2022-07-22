using RulesEngineDemo.Models;

namespace RulesEngineDemo.Data;

public static class Contracts
{
    /// <summary>
    /// A valid contract which also gets a discount.
    /// </summary>
    public static Contract Contract1 = new Contract()
    {
        Id = 1,
        ClientName = "Joe Blow",
        InsuredId = 123, // State = "NY"
        InsuranceType = "FD",
        EffectiveDate = new DateTime(2022, 1, 1),
        ExpirationDate = new DateTime(2023, 1, 2),
        TermMonths = 12,
        ClaimLimit = 500,
        ClaimAmount = 400,
        Premium = 999m,
    };

    /// <summary>
    /// A contract with invalid insurance type and claim amount.
    /// </summary>
    public static Contract Contract2 = new Contract()
    {
        Id = 2,
        ClientName = "Jane Doe",
        InsuredId = 456, // State = "CA"
        InsuranceType = "FD",
        EffectiveDate = new DateTime(2022, 2, 1),
        ExpirationDate = new DateTime(2023, 2, 1),
        TermMonths = 12,
        ClaimLimit = 500,
        ClaimAmount = 600,
        Premium = 1099m,
    };

    /// <summary>
    /// A contract with invalid term.
    /// </summary>
    public static Contract Contract3 = new Contract()
    {
        Id = 3,
        ClientName = "John Q. Public",
        InsuredId = 789, // State = "CA"
        InsuranceType = "XOL",
        EffectiveDate = new DateTime(2022, 2, 1),
        ExpirationDate = new DateTime(2023, 2, 11),
        TermMonths = 12,
        ClaimLimit = 500,
        ClaimAmount = 400,
        Premium = 1199m,
    };
}
