namespace CalculatorApp.Interfaces
{
	/// <summary>
	/// With Req 6, parsing is getting more complicated. This parser is responsible for extracting the input args from a single provided line
	/// </summary>
	public interface ICalculatorInputParser
	{
		string[] GetInputs(string? input);
	}
}
