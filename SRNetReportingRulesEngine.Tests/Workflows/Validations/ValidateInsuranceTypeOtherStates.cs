using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEngine.Models;

namespace SRNetReportingRulesEngine.Tests.Workflows.Validations
{
	[TestClass]
	public class ValidateInsuranceTypeOtherStates
	{
		[TestMethod]
		[DataRow("ETC")]
		[DataRow("FD")]
		[DataRow("XOL")]
		public async Task ValidContract_Pass(string insuranceType)
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
			var param2 = new RuleParameter("insured", new Insured { State = "FL" });

			// Act
			var results = await Engine.RunWorkflow("Validations", new[] { param1, param2 });
			bool isSuccess = results.Single(result => result.RuleName == "ValidateInsuranceTypeOtherStates")
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
				InsuranceType = "DUMMY",
				EffectiveDate = default,
				ExpirationDate = default,
				TermMonths = default,
				ClaimLimit = default,
				ClaimAmount = default,
				Premium = default,
			};
			var param1 = new RuleParameter("contract", contract);
			var param2 = new RuleParameter("insured", new Insured { State = "FL" });

			// Act
			var results = await Engine.RunWorkflow("Validations", new[] { param1, param2 });
			bool isSuccess = results.Single(result => result.RuleName == "ValidateInsuranceTypeOtherStates")
			                        .IsSuccess;

			// Assert
			Assert.IsFalse(isSuccess);
		}
	}
}
