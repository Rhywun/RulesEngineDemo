namespace SRNetReportingRulesEngine
{
	public class Result
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }

		public override string ToString()
		{
			string passFail = IsSuccess ? "Pass" : "Fail";
			return $"{passFail} - {Message}";
		}
	}
}
