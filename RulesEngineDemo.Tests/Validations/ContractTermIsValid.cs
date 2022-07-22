using RulesEngine.Models;
using RulesEngineDemo.Models;

namespace RulesEngineDemo.Tests.Validations;

[TestClass]
public class ContractTermIsValid
{
	private static RulesEngine.RulesEngine _engine;

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		_engine = new RulesEngine.RulesEngine(new[] { Workflows.Validations.Workflow });
	}

	[TestMethod]
	[DataRow(2023, 2, 1)]
	[DataRow(2023, 1, 30)]
	[DataRow(2023, 2, 4)]
	public async Task ValidContract_Pass(int year, int month, int date)
	{
		// Arrange
		var contract = new Contract
		{
			Id = default,
			ClientName = default,
			InsuredId = default,
			InsuranceType = default,
			EffectiveDate = new DateTime(2022, 2, 1),
			ExpirationDate = new DateTime(year, month, date), // Valid
			TermMonths = default,
			ClaimLimit = default,
			ClaimAmount = default,
			Premium = default,
		};

		// Act
		var param1 = new RuleParameter("contract", contract);
		var param2 = new RuleParameter("insured", new Insured { State = "NY" });
		var results = await _engine.ExecuteAllRulesAsync("Validations", param1, param2);
		var isSuccess = results.Single(result => result.Rule.RuleName == "ContractTermIsValid")
		                       .IsSuccess;

		// Assert
		Assert.IsTrue(isSuccess);
	}

	[TestMethod]
	[DataRow(2023, 1, 1)]
	[DataRow(2023, 3, 1)]
	public async Task InvalidContract_Fail(int year, int month, int date)
	{
		// Arrange
		var contract = new Contract
		{
			Id = default,
			ClientName = default,
			InsuredId = default,
			InsuranceType = default,
			EffectiveDate = new DateTime(2022, 2, 1),
			ExpirationDate = new DateTime(year, month, date), // Invalid
			TermMonths = default,
			ClaimLimit = default,
			ClaimAmount = default,
			Premium = default,
		};

		// Act
		var param1 = new RuleParameter("contract", contract);
		var param2 = new RuleParameter("insured", new Insured { State = "NY" });
		var results = await _engine.ExecuteAllRulesAsync("Validations", param1, param2);
		var isSuccess = results.Single(result => result.Rule.RuleName == "ContractTermIsValid")
		                       .IsSuccess;

		// Assert
		Assert.IsFalse(isSuccess);
	}
}
