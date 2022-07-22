using RulesEngine.Models;
using RulesEngineDemo.Models;

namespace RulesEngineDemo.Tests.Validations;

[TestClass]
public class InsuranceTypeIsValid
{
	private static RulesEngine.RulesEngine _engine;

	[ClassInitialize]
	public static void ClassInitialize(TestContext context)
	{
		_engine = new RulesEngine.RulesEngine(new[] { Workflows.Validations.Workflow });
	}

	[TestMethod]
	[DataRow("CA", "XOL")]
	[DataRow("NY", "FD")]
	[DataRow("FL", "ETC")]
	[DataRow("FL", "FD")]
	[DataRow("FL", "XOL")]
	public async Task ValidContract_Pass(string state, string insuranceType)
	{
		// Arrange
		var contract = new Contract
		{
			Id = default,
			ClientName = default,
			InsuredId = default,
			InsuranceType = insuranceType,
			EffectiveDate = default,
			ExpirationDate = default,
			TermMonths = default,
			ClaimLimit = default,
			ClaimAmount = default,
			Premium = default,
		};

		// Act
		var param1 = new RuleParameter("contract", contract);
		var param2 = new RuleParameter("insured", new Insured { State = state });
		var results = await _engine.ExecuteAllRulesAsync("Validations", param1, param2);
		var isSuccess = results.Single(result => result.Rule.RuleName == "InsuranceTypeIsValid")
		                       .IsSuccess;

		// Assert
		Assert.IsTrue(isSuccess);
	}

	[TestMethod]
	[DataRow("CA", "FD")]
	[DataRow("CA", "DUMMY")]
	[DataRow("NY", "XOL")]
	[DataRow("NY", "DUMMY")]
	[DataRow("FL", "DUMMY")]
	public async Task InvalidContract_Fail(string state, string insuranceType)
	{
		// Arrange
		var contract = new Contract
		{
			Id = default,
			ClientName = default,
			InsuredId = default,
			InsuranceType = insuranceType,
			EffectiveDate = default,
			ExpirationDate = default,
			TermMonths = default,
			ClaimLimit = default,
			ClaimAmount = default,
			Premium = default,
		};
		var param1 = new RuleParameter("contract", contract);
		var param2 = new RuleParameter("insured", new Insured { State = state });

		// Act
		var results = await _engine.ExecuteAllRulesAsync("Validations", param1, param2);
		var isSuccess = results.Single(result => result.Rule.RuleName == "InsuranceTypeIsValid")
		                       .IsSuccess;

		// Assert
		Assert.IsFalse(isSuccess);
	}
}
