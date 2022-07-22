using RulesEngine.Models;
using RulesEngineDemo.Models;

namespace RulesEngineDemo.Tests.Validations;

[TestClass]
public class ClaimLimitIsNotExceeded
{
	private static RulesEngine.RulesEngine _engine;

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		_engine = new RulesEngine.RulesEngine(new[] { Workflows.Validations.Workflow });
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
			ClaimLimit = 500,
			ClaimAmount = 400, // Valid
			Premium = default,
		};

		// Act
		var param1 = new RuleParameter("contract", contract);
		var param2 = new RuleParameter("insured", new Insured { State = "NY" });
		var results = await _engine.ExecuteAllRulesAsync("Validations", param1, param2);
		var isSuccess = results.Single(result => result.Rule.RuleName == "ClaimLimitIsNotExceeded")
		                       .IsSuccess;

		// Assert
		Assert.IsTrue(isSuccess);
	}

	[TestMethod]
	public async Task InvalidContract_Fail()
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
			ClaimLimit = 500,
			ClaimAmount = 600, // Invalid
			Premium = default,
		};

		// Act
		var param1 = new RuleParameter("contract", contract);
		var param2 = new RuleParameter("insured", new Insured { State = "NY" });
		var results = await _engine.ExecuteAllRulesAsync("Validations", param1, param2);
		var isSuccess = results.Single(result => result.Rule.RuleName == "ClaimLimitIsNotExceeded")
		                       .IsSuccess;

		// Assert
		Assert.IsFalse(isSuccess);
	}
}
