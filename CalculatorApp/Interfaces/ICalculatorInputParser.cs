namespace CalculatorApp.Interfaces
{
	/// <summary>
	/// With Req 6, parsing is getting more complicated. This parser is responsible for extracting the input args from a single provided line
	/// </summary>
	public interface ICalculatorInputParser
	{
		/// <summary>
		/// Extract input args from single input string
		/// </summary>
		/// <param name="input">input string to parse</param>
		/// <param name="defaultDelimiters">default delimiters to use in addition to parsed delimiters</param>
		/// <returns></returns>
		string[] GetInputs(string? input, IEnumerable<string> defaultDelimiters);
	}	// interface
}	// namespace
