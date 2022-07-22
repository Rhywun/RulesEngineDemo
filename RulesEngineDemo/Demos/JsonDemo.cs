using Newtonsoft.Json;
using RulesEngine.Models;
using static System.Console;
using static RulesEngineDemo.Data.Contracts;

namespace RulesEngineDemo.Demos;

// TODO: This class is out-of-date
public static class JsonDemo
{
    public static void Run()
    {
        WriteLine("\nRunning JSON Demo");
        WriteLine("-----------------");

        // Set up rules and engine from json file

        string[] files = Directory.GetFiles("Rules", "*.json");
        if (files == null || files.Length == 0)
            throw new Exception("Rules not found.");

        string fileData = File.ReadAllText(files[0]);
        var workflow = JsonConvert.DeserializeObject<List<Workflow>>(fileData);
        var engine = new RulesEngine.RulesEngine(workflow.ToArray());

        // Run engine and get results

        var results = engine.ExecuteAllRulesAsync("Calculations", Contract1).Result;

        results.ForEach(result =>
        {
            if (result.IsSuccess)
            {
                WriteLine($"Pass: {result.Rule.RuleName}");
            }
            else
            {
                WriteLine(
                    $"Fail: {result.Rule.RuleName} - {result.ExceptionMessage ?? result.Rule.ErrorMessage}");
            }

            if (result.ActionResult != null)
            {
                WriteLine(result.ActionResult
                                            .Output); //ActionResult.Output contains the evaluated value of the action
            }
        });

        if (results.TrueForAll(r => r.IsSuccess))
        {
            WriteLine("✔️ All rules passed");
        }
    }
}
