namespace Talabat.Domain.Consts;

public class RegexPatterns
{
	public const string EgyptianPhone = @"^01[0|1|2|5][0-9]{8}$";
	public const string Password = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";

}
