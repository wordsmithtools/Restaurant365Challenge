using CalculatorApp.Interfaces;

namespace CalculatorApp
{
	/// <summary>
	/// Performs challenge calculator options
	/// </summary>
	public class CalculatorService : ICalculatorService
	{
		protected readonly ICalculatorInputParser _inputParser;
		public CalculatorService(ICalculatorInputParser inputParser)
		{
			_inputParser = inputParser;
		}

		/// <summary>
		/// Maximum allowed value to sum, otherwise treat as 0
		/// </summary>
		private const decimal MAX_INPUT_VALUE = 1000;

		/// <summary>
		/// Default delimiters if not passed in
		/// </summary>
		private static readonly List<string> DEFAULT_DELIMITERS = [",", "\n"];

		/// <summary>
		/// Calculate sum of numbers
		/// </summary>
		/// <param name="input">input values separated by delimiters, in format //[{delimiter1}][{delimiter2}]...\n{numbers}</param>
		/// <param name="showFormula">whether to show the formula in the output</param>
		/// <returns>output value</returns>
		public string GetSum(string? input, bool showFormula)
		{
			return GetSum(input, showFormula, false, MAX_INPUT_VALUE, DEFAULT_DELIMITERS);
		}   // GetSum

		/// <summary>
		/// Calculate sum of positive numbers from 0-1000 with default delimiters of [,] and [\n]
		/// </summary>
		/// <param name="input">input values separated by delimiters, in format //[{delimiter1}][{delimiter2}]...\n{numbers}</param>
		/// <param name="showFormula">whether to show the formula in the output</param>
		/// <param name="allowNegativeNumbers">whether to allow negative numbers</param>
		/// <param name="maxInputValue">max value to support; higher values treated as 0</param>
		/// <param name="defaultDelimiters">Default delimiters to include</param>
		/// <returns>output value</returns>
		public string GetSum(string? input, bool showFormula, bool allowNegativeNumbers, decimal maxInputValue, IEnumerable<string> defaultDelimiters)
		{
			var parts = _inputParser.GetInputs(input, defaultDelimiters);

			var numbers = parts.Select(p => StringToNumberInRange(p, maxInputValue));

			ValidateInput(numbers, allowNegativeNumbers);

			if (showFormula)
				return string.Join('+', numbers) + " = " + numbers.Sum();
			else
				return numbers.Sum().ToString();
		}   // GetSum

		/// <summary>
		/// Run validation checks on input
		/// </summary>
		/// <param name="numbers"></param>
		/// <exception cref="Exception"></exception>
		void ValidateInput(IEnumerable<decimal> numbers, bool allowNegativeNumbers)
		{
			// Req 4. Support option to deny negative numbers by throwing an exception that includes all of the negative numbers provided
			if (!allowNegativeNumbers)
			{
				var negativeNumbers = numbers.Where(n => n < 0);
				if (negativeNumbers.Any())
				{
					throw new Exception("The following negative numbers are not permitted: " + string.Join(",", negativeNumbers));
				}
			}
		}   // ValidateInput

		/// <summary>
		/// Convert string value in the range (up to maxInputValue) to a decimal number, treating empty or invalid input as 0
		/// </summary>
		/// <param name="value">input string value</param>
		/// <param name="maxInputValue">max value to support; higher values treated as 0</param>
		/// <returns></returns>
		decimal StringToNumberInRange(string value, decimal maxInputValue)
		{
			decimal d;
			if (decimal.TryParse(value, out d))
			{
				// Req 5. Make any value greater than 1000 an invalid number, otherwise use 0
				if(d <= maxInputValue)
					return d;
			}
			return 0;
		}
	}	// class
}	// namespace
