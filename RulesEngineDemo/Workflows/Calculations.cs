using RulesEngine.Models;

namespace RulesEngineDemo.Workflows;

public static class Calculations
{
    public static Workflow Workflow
        => new Workflow
        {
            WorkflowName = "Calculations",
            Rules = new List<Rule>
            {
                new Rule
                {
                    RuleName = "CalculateDiscountedPremium",
                    Properties = new() { ["Description"] = "New Yorkers get a discount", },
                    Enabled = true,
                    Expression = "insured.State == \"NY\"",
                    SuccessEvent = "true",
                    ErrorMessage = "Does not qualify for a discount",
                    Actions = new RuleActions
                    {
                        OnSuccess = new ActionInfo
                        {
                            Name = "OutputExpression",
                            Context = new() { ["Expression"] = "contract.Premium * 0.9" },
                        },
                    },
                },
            },
        };
}
