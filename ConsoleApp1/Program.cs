using System;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using RulesEngine.Models;

namespace ConsoleApp1
{
	class Program
	{
		public static void Main()
		{
			MainAsync().GetAwaiter().GetResult();
		}

		private static async Task MainAsync()
		{
			var contract = new Contract
			{
				Id = 1,
				ClientName = "CLIENTNAME",
				InsuredId = 123,
				InsuranceType = "FD",
				EffectiveDate = new DateTime(2022, 1, 1),
				ExpirationDate = new DateTime(2023, 1, 1),
				TermMonths = 12,
				ClaimLimit = 600,
				ClaimAmount = 800,
				Premium = 999,
			};
			
			var engine = new SRNetReportingRulesEngine.Engine();

			var param1 = new RuleParameter("contract", contract);
			var param2 = new RuleParameter("insured", new Insured { State = "NY" });

			var results1 = await engine.RunWorkflow("Validations", new[] { param1, param2 });
			results1.ForEach(result => Console.WriteLine(result));

			var results2 = await engine.RunWorkflow("Calculations", new[] { param1, param2 });
			results2.ForEach(result => Console.WriteLine(result));
		}
	}
}
