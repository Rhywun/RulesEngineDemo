using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEngine.Models;

namespace SRNetReportingRulesEngine.Tests.Workflows.Calculations
{
	[TestClass]
	public class CalculateDiscountedPremium
	{
		[TestMethod]
		public async Task ValidContract_Pass()
		{
			// Arrange
			var contract = new Contract
			{
				Id = default,
				ClientName = default,
				InsuredId = default,
				InsuranceType = default,
				EffectiveDate = default,
				ExpirationDate = default,
				TermMonths = default,
				ClaimLimit = default,
				ClaimAmount = default,
				Premium = 999m,
			};
			var param1 = new RuleParameter("contract", contract);
			var param2 = new RuleParameter("insured", new Insured { State = "NY" });

			// Act
			var results = await Engine.RunWorkflow("Calculations", new[] { param1, param2 });
			var result = results.Single(result => result.RuleName == "CalculateDiscountedPremium");

			// Assert
			Assert.AreEqual(899.1m, (decimal)result.Output);
		}

		[TestMethod]
		[DataRow("CA")]
		[DataRow("FL")]
		[DataRow("TX")]
		public async Task InvalidContract_Fail(string state)
		{
			// Arrange
			var param1 = new RuleParameter("contract", default);
			var param2 = new RuleParameter("insured", new Insured { State = state });

			// Act
			var results = await Engine.RunWorkflow("Calculations", new[] { param1, param2 });
			var result = results.Single(result => result.RuleName == "CalculateDiscountedPremium");

			// Assert
			Assert.IsFalse(result.IsSuccess);
		}
	}
}
