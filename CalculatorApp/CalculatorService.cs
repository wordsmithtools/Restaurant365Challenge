namespace CalculatorApp
{
	/// <summary>
	/// Performs challenge calculator options
	/// </summary>
	public class CalculatorService : ICalculatorService
	{
		/// <summary>
		/// Supported delimiters
		/// </summary>
		protected char[] _delimiters = { ',', '\n' };
		
		/// <summary>
		/// Maximum allowed value to sum, otherwise treat as 0
		/// </summary>
		private const decimal MAX_INPUT_VALUE = 1000;

		/// <summary>
		/// Sum a maximum of 2 numbers using a comma delimiter
		/// </summary>
		/// <param name="input">input values separated by comma</param>
		/// <param name="showFormula">whether to show the formula as output</param>
		/// <returns>formula used and the sum</returns>
		public string GetSum(string? input, bool showFormula)
		{
			if (string.IsNullOrWhiteSpace(input))
				input = "0";
			var parts = input.Split(_delimiters);

			var numbers = parts.Select(p => StringToNumberInRange(p));

			ValidateInput(numbers);

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
		void ValidateInput(IEnumerable<decimal> numbers)
		{
			// Req 4. Deny negative numbers by throwing an exception that includes all of the negative numbers provided
			var negativeNumbers = numbers.Where(n => n < 0);
			if (negativeNumbers.Any())
			{
				throw new Exception("The following negative numbers are not permitted: " + string.Join(",", negativeNumbers));
			}
		}   // ValidateInput

		/// <summary>
		/// Convert string value in the range (up to 1000) to a decimal number, treating empty or invalid input as 0
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		decimal StringToNumberInRange(string value)
		{
			decimal d;
			if (decimal.TryParse(value, out d))
			{
				// Req 5. Make any value greater than 1000 an invalid number, otherwise use 0
				if(d <= MAX_INPUT_VALUE)
					return d;
			}
			return 0;
		}
	}	// class
}	// namespace
