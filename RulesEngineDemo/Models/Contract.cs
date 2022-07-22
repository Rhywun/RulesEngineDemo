namespace RulesEngineDemo.Models;

public class Contract
{
	public int      Id             { get; set; }
	public string   ClientName     { get; set; }
	public int      InsuredId	   { get; set; }
	public string   InsuranceType  { get; set; }
	public DateTime EffectiveDate  { get; set; }
	public DateTime ExpirationDate { get; set; }
	public int      TermMonths     { get; set; }
	public int      ClaimLimit     { get; set; }
	public int      ClaimAmount    { get; set; }
	public decimal  Premium        { get; set; }
}
