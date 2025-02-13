﻿using CalculatorApp.Interfaces;

namespace CalculatorApp
{
	/// <summary>
	/// With Req 6, parsing is getting more complicated. This parser is responsible for extracting the input args from a single provided line
	/// </summary>
	public class CalculatorInputParser : ICalculatorInputParser
	{
		/// <summary>
		/// Extract input args from single input string
		/// </summary>
		/// <param name="input">input string  of form //{delimiter}\n{numbers} or {numbers}</param>
		/// <param name="defaultDelimiters">Default delimiters to include</param>
		/// <returns></returns>
		public string[] GetInputs(string? input, IEnumerable<string> defaultDelimiters)
		{
			if (string.IsNullOrWhiteSpace(input))
				input = "0";

			input = input.TrimStart();

			// make sure to reset delimiters each time parsing input
			var delimiters = defaultDelimiters.ToList();

			// Req 8. Support multiple delimiters of any length using the format: //[{delimiter1}][{delimiter2}]...\n{numbers}
			// Could use RegEx here but is simple enough to parse manually
			int endIxDelimiter = input.IndexOf("]\n");
			if (input.Length >= 6 && input.Substring(0, 3) == "//[" &&
				endIxDelimiter >= 4)
			{
				string delimiterString = input.Substring(3, endIxDelimiter - 3);
				// Assume delimiters are within brackets of either type. There is an edge case here where user may want to support delimiters that actually contain brackets.
				// That is not supported, but could be handled if it was desired.
				var delimitersFound = delimiterString.Split(['[', ']'], StringSplitOptions.RemoveEmptyEntries);

				foreach (var delimiter in delimitersFound)
				{
					if (!delimiters.Contains(delimiter))
						delimiters.Add(delimiter); // add passed in delimiter if not already defined
				}

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
				if (!delimiters.Contains(delimiter))
					delimiters.Add(delimiter); // add passed in delimiter if not already defined

				// strip this part
				if (input.Length > 4)
					input = input.Substring(4);
				else
					input = string.Empty;
			}   // has custom delimiter

			return input.Split(delimiters.ToArray(), StringSplitOptions.None);
		}   // GetInputs

		
	}	// class
}	// namespace
