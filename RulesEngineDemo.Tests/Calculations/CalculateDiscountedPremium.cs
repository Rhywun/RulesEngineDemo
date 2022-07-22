using RulesEngine.Models;
using RulesEngineDemo.Models;

namespace RulesEngineDemo.Tests.Calculations;

[TestClass]
public class CalculateDiscountedPremium
{
	private static RulesEngine.RulesEngine _engine;

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		_engine = new RulesEngine.RulesEngine(new[] { Workflows.Calculations.Workflow });
	}

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

		// Act
		var param1 = new RuleParameter("contract", contract);
		var param2 = new RuleParameter("insured", new Insured { State = "NY" });
		var results = await _engine.ExecuteAllRulesAsync("Calculations", param1, param2);
		var result = results.Single(result => result.Rule.RuleName == "CalculateDiscountedPremium");

		// Assert
		Assert.AreEqual(899.1m, (decimal)result.ActionResult.Output);
	}
	
	[TestMethod]
	[DataRow("CA")]
	[DataRow("FL")]
	[DataRow("TX")]
	public async Task InvalidContract_Fail(string state)
	{
		// Arrange
		var param1 = new RuleParameter("contract", null);
		var param2 = new RuleParameter("insured", new Insured { State = state });

		// Act
		var results = await _engine.ExecuteAllRulesAsync("Calculations", param1, param2);
		var result = results.Single(result => result.Rule.RuleName == "CalculateDiscountedPremium");

		// Assert
		Assert.IsFalse(result.IsSuccess);
	}
}
