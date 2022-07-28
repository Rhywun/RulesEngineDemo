using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEngine.Models;

namespace SRNetReportingRulesEngine.Tests.Workflows.Validations
{
	[TestClass]
	public class ValidateExpirationDate
	{
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
				TermMonths = 12,
				ClaimLimit = default,
				ClaimAmount = default,
				Premium = default,
			};
			var param1 = new RuleParameter("contract", contract);
			var param2 = new RuleParameter("insured", new Insured { State = default });

			// Act
			var results = await Engine.RunWorkflow("Validations", new[] { param1, param2 });
			bool isSuccess = results.Single(result => result.RuleName == "ValidateExpirationDate")
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
				TermMonths = 12,
				ClaimLimit = default,
				ClaimAmount = default,
				Premium = default,
			};
			var param1 = new RuleParameter("contract", contract);
			var param2 = new RuleParameter("insured", new Insured { State = default });

			// Act
			var results = await Engine.RunWorkflow("Validations", new[] { param1, param2 });
			bool isSuccess = results.Single(result => result.RuleName == "ValidateExpirationDate")
			                        .IsSuccess;

			// Assert
			Assert.IsFalse(isSuccess);
		}
	}
}
