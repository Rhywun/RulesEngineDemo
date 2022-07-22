using static RulesEngineDemo.Data.Contracts;
using static RulesEngineDemo.Demos.Helpers;
using static System.Console;
using RulesEngine.Models;
using RulesEngineDemo.Models;
using RulesEngineDemo.Workflows;

namespace RulesEngineDemo.Demos;

public static class ValidationsDemo
{
    public static async void Run()
    {
        WriteLine("\nRunning Validations demo...\n");

        // Set up the rules engine with workflow(s)
        var engine = new RulesEngine.RulesEngine(new[] { Validations.Workflow });

        WriteLine("Contract1...");
        var contract = new RuleParameter("contract", Contract1);
        var insured = new RuleParameter("insured", new Insured { State = "NY" });
        var results = await engine.ExecuteAllRulesAsync("Validations", contract, insured);
        ReportResults(results);

        WriteLine("Contract2...");
        contract = new RuleParameter("contract", Contract2);
        insured = new RuleParameter("insured", new Insured { State = "CA" });
        results = await engine.ExecuteAllRulesAsync("Validations", contract, insured);
        ReportResults(results);

        WriteLine("Contract3...");
        contract = new RuleParameter("contract", Contract3);
        insured = new RuleParameter("insured", new Insured { State = "CA" });
        results = await engine.ExecuteAllRulesAsync("Validations", contract, insured);
        ReportResults(results);
    }
}
