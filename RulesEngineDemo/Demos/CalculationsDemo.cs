using static RulesEngineDemo.Data.Contracts;
using static System.Console;
using static RulesEngineDemo.Demos.Helpers;
using RulesEngine.Models;
using RulesEngineDemo.Models;
using RulesEngineDemo.Workflows;

namespace RulesEngineDemo.Demos;

public static class CalculationsDemo
{
    public static async void Run()
    {
        Console.WriteLine("\nRunning Calculations demo...\n");

        // Set up the rules engine with workflow(s)
        var engine = new RulesEngine.RulesEngine(new[] { Calculations.Workflow });

        // Execute a workflow's rules against an input
        WriteLine("Contract1...");
        var contract = new RuleParameter("contract", Contract1);
        var insured = new RuleParameter("insured", new Insured { State = "NY" });
        var results = await engine.ExecuteAllRulesAsync("Calculations", contract, insured);

        // Display the results
        ReportResults(results);

        WriteLine($"\n\tPremium before: {Contract1.Premium:c}");
        decimal newPremium = (decimal)results.First()?.ActionResult?.Output;
        Contract1.Premium = newPremium;
        WriteLine($"\tPremium after: {Contract1.Premium:c}");
    }
}
