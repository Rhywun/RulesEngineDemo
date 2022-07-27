using System;
using System.Collections.Generic;
using System.Linq;
using RulesEngine.Models;
using SRNetReportingRulesEngine.Models;
using static System.Console;

namespace SRNetReportingRulesEngine
{
    internal static class Helpers
	{
		internal static List<RuleResult> GetResults(List<RuleResultTree> results)
		{
			return results.Select(result => new RuleResult
			{
				RuleName = result.Rule.RuleName,
				IsSuccess = result.IsSuccess,
				Message = result.IsSuccess ? result.Rule.SuccessEvent : (string.IsNullOrEmpty(result.ExceptionMessage) ? result.Rule.ErrorMessage : result.ExceptionMessage),
				Output = result.ActionResult.Output,
			}).ToList();
		}
		
		internal static void ReportResults(List<RuleResultTree> results)
		{
			results.ForEach(result =>
			{
				WriteLine(result.IsSuccess
					? $"Pass: {result.Rule.RuleName} - {result.Rule.SuccessEvent}"
					: $"Fail: {result.Rule.RuleName} " +
					  $"- {(string.IsNullOrEmpty(result.ExceptionMessage) ? result.Rule.ErrorMessage : result.ExceptionMessage)}");

				if (result.ActionResult.Output != null)
					WriteLine($"\tOutput: {result.ActionResult.Output}");
			});
		}
	}
}
