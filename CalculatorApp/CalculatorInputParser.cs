using CalculatorApp.Interfaces;

namespace CalculatorApp
{
	/// <summary>
	/// With Req 6, parsing is getting more complicated. This parser is responsible for extracting the input args from a single provided line
	/// </summary>
	public class CalculatorInputParser : ICalculatorInputParser
	{
		/// <summary>
		/// Supported delimiters
		/// </summary>
		protected List<char> _delimiters = [',', '\n'];

		/// <summary>
		/// Extract input args from single input string
		/// </summary>
		/// <param name="input">input string  of form //{delimiter}\n{numbers} or {numbers}</param>
		/// <returns></returns>
		public string[] GetInputs(string? input)
		{
			if (string.IsNullOrWhiteSpace(input))
				input = "0";

			input = input.Trim();

			// Req 6. check for custom delimiter of form //{delimiter}\n{numbers}
			if(input.Length >= 4 && input.Substring(0, 2) == "//" && input[3] == '\n')
			{
				if (!_delimiters.Contains(input[2]))
					_delimiters.Add(input[2]); // add passed in delimiter if not already defined

				// strip this part
				if(input.Length > 4)
					input = input.Substring(4);
			}   // has custom delimiter

			return input.Split(_delimiters.ToArray());
		}
	}	// class
}	// namespace
