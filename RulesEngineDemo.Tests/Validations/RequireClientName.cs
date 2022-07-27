using RulesEngine.Models;
using RulesEngineDemo.Models;

namespace RulesEngineDemo.Tests.Validations;

[TestClass]
public class RequireClientName
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
			ClientName = "CLIENTNAME", // Valid
			InsuredId = default,
			InsuranceType = default,
			EffectiveDate = default,
			ExpirationDate = default,
			TermMonths = default,
			ClaimLimit = default,
			ClaimAmount = default,
			Premium = default,
		};

		// Act
		var param1 = new RuleParameter("contract", contract);
		var param2 = new RuleParameter("insured", new Insured { State = default });
		var results = await _engine.ExecuteAllRulesAsync("Validations", param1, param2);
		var isSuccess = results.Single(result => result.Rule.RuleName == "RequireClientName")
							   .IsSuccess;

		// Assert
		Assert.IsTrue(isSuccess);
	}

	[TestMethod]
    [DataRow("")]
    [DataRow(null)]
	public async Task InvalidContract_Fail(string clientName)
	{
		// Arrange
		var contract = new Contract
		{
			Id = default,
			ClientName = clientName,
			InsuredId = default,
			InsuranceType = default,
			EffectiveDate = default,
			ExpirationDate = default,
			TermMonths = default,
			ClaimLimit = default,
			ClaimAmount = default,
			Premium = default,
		};

		// Act
		var param1 = new RuleParameter("contract", contract);
		var param2 = new RuleParameter("insured", new Insured { State = default });
		var results = await _engine.ExecuteAllRulesAsync("Validations", param1, param2);
		var isSuccess = results.Single(result => result.Rule.RuleName == "RequireClientName")
							   .IsSuccess;

		// Assert
		Assert.IsFalse(isSuccess);
	}
}
