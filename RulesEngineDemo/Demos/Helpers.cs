using RulesEngine.Models;
using static System.Console;

namespace RulesEngineDemo.Demos;

internal static class Helpers
{
    internal static void ReportResults(List<RuleResultTree> results)
    {
        // Loop through each parent rule
        results.ForEach(result =>
        {
            WriteLine(result.IsSuccess
                ? $"\tPass: {result.Rule.RuleName} - {result.Rule.SuccessEvent}"
                : $"\tFail: {result.Rule.RuleName} " +
                  $"- {(string.IsNullOrEmpty(result.ExceptionMessage) ? result.Rule.ErrorMessage : result.ExceptionMessage)}");

            if (result.ActionResult.Output != null)
                WriteLine($"\tOutput: {result.ActionResult.Output}");

            if (result.ChildResults is null)
            {
                return;
            }

            if (result.Rule.Operator == "Or")
            {
                WriteLine("\t\tOne of the following rules should pass:");
            }
            else if (result.Rule.Operator == "And")
            {
                WriteLine("\t\tAll of the following rules should pass:");
            }

            // Loop through each child rule
            foreach (var childResult in result.ChildResults)
            {
                WriteLine(childResult.IsSuccess
                    ? $"\t\tPass: {childResult.Rule.RuleName} - {childResult.Rule.SuccessEvent}"
                    : $"\t\tFail: {childResult.Rule.RuleName} " +
                      $"- {(string.IsNullOrEmpty(childResult.ExceptionMessage) ? childResult.Rule.ErrorMessage : childResult.ExceptionMessage)}");

                if (childResult.ActionResult.Output != null)
                    WriteLine($"\t\tOutput: {childResult.ActionResult.Output}");
            }
        });
    }
}
