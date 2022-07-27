using System.Text;

namespace SRNetReportingRulesEngine.Models
{
    public class Result
    {
        public string RuleName { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Output { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(IsSuccess ? "Pass" : "Fail");
            sb.Append(" - ");
            sb.Append(RuleName);
            sb.Append(" - ");
            sb.Append(Message);
            if (Output != null)
            {
                sb.Append(" - Output: ");
                sb.Append(Output);
            }
            return sb.ToString();
        }
    }
}
