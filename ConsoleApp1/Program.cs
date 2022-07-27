using System;
using ConsoleApp1.Models;
using RulesEngine.Models;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
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
				ClaimAmount = 400,
				Premium = 999,
			};
			
			var engine = new SRNetReportingRulesEngine.Engine();
			var param1 = new RuleParameter("contract", contract);
			var param2 = new RuleParameter("insured", new Insured { State = "NY" });
			engine.RunWorkflow("Validations", new[] { param1, param2 });
		}
	}
}
