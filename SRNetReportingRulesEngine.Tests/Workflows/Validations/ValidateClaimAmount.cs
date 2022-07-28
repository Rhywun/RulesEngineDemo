using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEngine.Models;

namespace SRNetReportingRulesEngine.Tests.Workflows.Validations
{
	[TestClass]
	public class ValidateClaimAmount
	{
		private static Engine _engine;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			_engine = new Engine();
		}

		[TestMethod]
		[DataRow(400)]
		[DataRow(500)]
		public async Task ValidContract_Pass(int claimAmount)
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
				ClaimAmount = claimAmount, // Valid
				Premium = default,
			};
			var param1 = new RuleParameter("contract", contract);
			var param2 = new RuleParameter("insured", new Insured { State = default });

			// Act
			var results = await _engine.RunWorkflow("Validations", new[] { param1, param2 });
			bool isSuccess = results.Single(result => result.RuleName == "ValidateClaimAmount")
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
			var param1 = new RuleParameter("contract", contract);
			var param2 = new RuleParameter("insured", new Insured { State = default });

			// Act
			var results = await _engine.RunWorkflow("Validations", new[] { param1, param2 });
			bool isSuccess = results.Single(result => result.RuleName == "ValidateClaimAmount")
			                        .IsSuccess;

			// Assert
			Assert.IsFalse(isSuccess);
		}
	}
}
