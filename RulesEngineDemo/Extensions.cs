using static System.Console;

namespace RulesEngineDemo;

public static class Extensions
{
	/// <summary>
	/// Writes the string representation of the specified object, with an optional header
	/// and top margin.
	/// </summary>
	/// <param name="o">The object to write.</param>
	/// <param name="header">The header test.</param>
	/// <param name="topMargin">true to write a blank line first, otherwise false.</param>
	public static void Dump(this object o, string header = default, bool topMargin = true)
	{
		if (topMargin)
			WriteLine(Environment.NewLine);
		
		if (header != null)
			WriteLine(header);
		
		WriteLine(string.Join("", Enumerable.Repeat('-', header.Length)));
		WriteLine(ObjectDumper.Dump(o));
	}
}
