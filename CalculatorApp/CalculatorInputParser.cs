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
		protected List<string> _delimiters = [",", "\n"];

		/// <summary>
		/// Extract input args from single input string
		/// </summary>
		/// <param name="input">input string  of form //{delimiter}\n{numbers} or {numbers}</param>
		/// <returns></returns>
		public string[] GetInputs(string? input)
		{
			if (string.IsNullOrWhiteSpace(input))
				input = "0";

			input = input.TrimStart();

			// Req 7. Support 1 custom delimiter of any length using the format: //[{delimiter}]\n{numbers}
			// Could use RegEx here but is simple enough to parse manually
			int endIxDelimiter = input.IndexOf("]\n");
			if (input.Length >= 6 && input.Substring(0, 3) == "//[" &&
				endIxDelimiter >= 4)
			{
				string delimiter = input.Substring(3, endIxDelimiter - 3);
				if (!_delimiters.Contains(delimiter))
					_delimiters.Add(delimiter); // add passed in delimiter if not already defined

				// strip this part
				if (input.Length > endIxDelimiter + 2)
					input = input.Substring(endIxDelimiter + 2);
				else
					input = string.Empty;
			}

			// Req 6. check for custom delimiter of form //{delimiter}\n{numbers}
			if (input.Length >= 4 && input.Substring(0, 2) == "//" && input[3] == '\n')
			{
				string delimiter = input[2].ToString();
				if (!_delimiters.Contains(delimiter))
					_delimiters.Add(delimiter); // add passed in delimiter if not already defined

				// strip this part
				if (input.Length > 4)
					input = input.Substring(4);
				else
					input = string.Empty;
			}   // has custom delimiter

			return input.Split(_delimiters.ToArray(), StringSplitOptions.None);
		}   // GetInputs
	}	// class
}	// namespace
