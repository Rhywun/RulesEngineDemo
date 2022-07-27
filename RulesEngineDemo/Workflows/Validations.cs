using RulesEngine.Models;

namespace RulesEngineDemo.Workflows;

public static class Validations
{
    public static Workflow Workflow
        => new Workflow
        {
            WorkflowName = "Validations",
            Rules = new List<Rule>
            {
                new Rule
                {
                    RuleName = "InsuranceTypeIsValid",
                    Properties = new() { ["Description"] = "Validates the insurance type.", },
                    Enabled = true,
                    SuccessEvent = "InsuranceType is valid",
                    ErrorMessage = "InsuranceType is invalid",
                    Operator = "Or",
                    Rules = new List<Rule>
                    {
                        new Rule
                        {
                            RuleName = "InsuranceTypeCA",
                            Properties = new() { ["Description"] = "Validates the insurance type for CA." },
                            Enabled = true,
                            Expression = "insured.State = \"CA\" and contract.InsuranceType = \"XOL\"",
                            SuccessEvent = "InsuranceType is valid for CA",
                            ErrorMessage = "InsuranceType for CA must be XOL",
                        },
                        new Rule
                        {
                            RuleName = "InsuranceTypeNY",
                            Properties = new() { ["Description"] = "Validates the insurance type for NY." },
                            Enabled = true,
                            Expression = "insured.State == \"NY\" and contract.InsuranceType = \"FD\"",
                            SuccessEvent = "InsuranceType is valid for NY",
                            ErrorMessage = "InsuranceType for NY must be FD",
                        },
                        new Rule
                        {
                            RuleName = "InsuranceTypeOther",
                            Properties = new() { ["Description"] = "Validates the insurance type for states other than NY or CA." },
                            Enabled = true,
                            Expression = "not (insured.State in (\"CA\", \"NY\")) && contract.InsuranceType in (\"FD\", \"XOL\", \"ETC\")",
                            SuccessEvent = "InsuranceType is valid for a state other than NY or CA",
                            ErrorMessage = "InsuranceType for states other than NY or CA must be one of FD, XOL, or ETC",
                        },
                    }
                },
                new Rule
                {
                    RuleName = "ClaimLimitIsNotExceeded",
                    Properties = new() { ["Description"] = "Validates the claim amount." },
                    Enabled = true,
                    Expression = "contract.ClaimAmount <= contract.ClaimLimit",
                    SuccessEvent = "ClaimAmount is not greater than ClaimLimit",
                    ErrorMessage = "ClaimAmount must not be greater than ClaimLimit",
                },
                new Rule
                {
                    RuleName = "ContractTermIsValid",
                    Properties = new() { ["Description"] = "Validates the contract term." },
                    Enabled = true,
                    LocalParams = new List<ScopedParam>
                    {
                        new ScopedParam
                        {
                            Name = "MinimumExpirationDate",
                            Expression = "contract.EffectiveDate.AddMonths(contract.TermMonths).AddDays(-5)",
                        },
                        new ScopedParam
                        {
                            Name = "MaximumExpirationDate",
                            Expression = "contract.EffectiveDate.AddMonths(contract.TermMonths).AddDays(5)",
                        },
                    },
                    Expression = "contract.ExpirationDate >= MinimumExpirationDate and contract.ExpirationDate <= MaximumExpirationDate",
                    SuccessEvent = "ExpirationDate is EffectiveDate plus TermMonths +/- 5 days",
                    ErrorMessage = "ExpirationDate must be EffectiveDate plus TermMonths +/- 5 days",
                },
                new Rule
                {
                    RuleName = "RequireClientName",
                    Properties = new() { ["Description"] = "ClientName is required" },
                    Enabled = true,
                    Expression = "contract.ClientName != null",
                    SuccessEvent = "ClientName is present",
                    ErrorMessage = "ClientName is required",
                },
            },
        };
}
