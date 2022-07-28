using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEngine.Models;

namespace SRNetReportingRulesEngine.Tests.Workflows.Validations
{
	[TestClass]
	public class ValidateInsuranceTypeCA
	{
		private static Engine _engine;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			_engine = new Engine();
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
				InsuranceType = "XOL",
				EffectiveDate = default,
				ExpirationDate = default,
				TermMonths = default,
				ClaimLimit = default,
				ClaimAmount = default,
				Premium = default,
			};
			var param1 = new RuleParameter("contract", contract);
			var param2 = new RuleParameter("insured", new Insured { State = "CA" });

			// Act
			var results = await _engine.RunWorkflow("Validations", new[] { param1, param2 });
			bool isSuccess = results.Single(result => result.RuleName == "ValidateInsuranceTypeCA")
			                        .IsSuccess;

			// Assert
			Assert.IsTrue(isSuccess);
		}

		[TestMethod]
		[DataRow("FD")]
		[DataRow("DUMMY")]
		public async Task InvalidContract_Fail(string insuranceType)
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
			var param2 = new RuleParameter("insured", new Insured { State = "CA" });

			// Act
			var results = await _engine.RunWorkflow("Validations", new[] { param1, param2 });
			bool isSuccess = results.Single(result => result.RuleName == "ValidateInsuranceTypeCA")
			                        .IsSuccess;

			// Assert
			Assert.IsFalse(isSuccess);
		}
	}
}
