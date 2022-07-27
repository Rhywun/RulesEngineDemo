using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using RulesEngine.Models;
using static SRNetReportingRulesEngine.Helpers;

namespace SRNetReportingRulesEngine
{
	public class Engine
	{
		private static RulesEngineContext _db;
		private static RulesEngine.RulesEngine _engine;

		public Engine()
		{
			_db = new RulesEngineContext();

			// NOTE: Debugging
			// var logs = new List<string>();
			// var serviceProvider = _db.GetInfrastructure();
			// var loggerFactory = (ILoggerFactory)serviceProvider.GetService(typeof(ILoggerFactory));
			// loggerFactory?.AddProvider(new MyLoggerProvider(logs));

			var workflows = _db.Workflows.Include(workflow => workflow.Rules)
			                   .ThenInclude(rule => rule.LocalParams)
			                   // .Include(rule => rule.Rules)
			                   // .ThenInclude(rule => rule.Rules)
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

		public async void RunWorkflow(string workflow, RuleParameter[] ruleParams)
		{
			var results = await _engine.ExecuteAllRulesAsync(workflow, ruleParams);
			
			// TODO: Return a success/fail and a message, instead of printing them to the console
			ReportResults(results);
		}
	}
}
