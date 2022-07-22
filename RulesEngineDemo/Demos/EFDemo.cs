using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using RulesEngineDemo.Models;
using RulesEngineDemo.Workflows;
using static System.Console;
using static RulesEngineDemo.Data.Contracts;
using static RulesEngineDemo.Demos.Helpers;

namespace RulesEngineDemo.Demos;

public static class EFDemo
{
    private static readonly RulesEngineDemoContext _db;

    static EFDemo()
    {
        // Save the workflow array to the database
        _db = new RulesEngineDemoContext();
        _db.Database.EnsureDeleted();
        if (_db.Database.EnsureCreated())
        {
            _db.Workflows.AddRange(Validations.Workflow, Calculations.Workflow);
            _db.SaveChanges();
        }
    }

    public static async void Run()
    {
        // Initialize the rules engine with an array of workflows
        var workflows = _db.Workflows.Include(i => i.Rules)
                           .ThenInclude(i => i.Rules)
                           .ToArray();
        var engine = new RulesEngine.RulesEngine(workflows);
        
        WriteLine("\nRunning validations...\n");
        
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

        WriteLine("\nRunning calculations...\n");

        WriteLine("Contract1...");
        contract = new RuleParameter("contract", Contract1);
        insured = new RuleParameter("insured", new Insured { State = "NY" });
        results = await engine.ExecuteAllRulesAsync("Calculations", contract, insured);
        ReportResults(results);

        WriteLine($"\n\tPremium before: {Contract1.Premium:c}");
        decimal newPremium = (decimal)results.First()?.ActionResult?.Output;
        Contract1.Premium = newPremium;
        WriteLine($"\tPremium after: {Contract1.Premium:c}");
    }
}
