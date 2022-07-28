using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using SRNetReportingRulesEngine.Models;
using static SRNetReportingRulesEngine.Helpers;

namespace SRNetReportingRulesEngine
{
    public static class Engine
	{
		private static readonly RulesEngine.RulesEngine _engine;

		static Engine()
		{
			var db = new RulesEngineContext();

			// NOTE: Debugging
			// var logs = new List<string>();
			// var serviceProvider = _db.GetInfrastructure();
			// var loggerFactory = (ILoggerFactory)serviceProvider.GetService(typeof(ILoggerFactory));
			// loggerFactory?.AddProvider(new MyLoggerProvider(logs));

			var workflows = db.Workflows.Include(workflow => workflow.Rules)
			                   .ThenInclude(rule => rule.LocalParams)
			                   .ToArray();

			// NOTE: Debugging
			// Console.WriteLine("---------- LOGS ------------------");
			// foreach (string log in logs)
			// {
			// 	Console.WriteLine(log);
			// }
			// Console.WriteLine("----------------------------------");

			_engine = new RulesEngine.RulesEngine(workflows);
		}

		public static async Task<List<RuleResult>> RunWorkflow(string workflow, RuleParameter[] ruleParams)
		{
			var results = await _engine.ExecuteAllRulesAsync(workflow, ruleParams);

			// NOTE: Debugging
			// ReportResults(results);

			return GetResults(results);
		}
	}
}
