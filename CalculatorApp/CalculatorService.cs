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

			ValidateInput(parts);

			var numbers = parts.Select(p => StringToNumber(p));
			if (showFormula)
				return string.Join('+', numbers) + " = " + numbers.Sum();
			else
				return numbers.Sum().ToString();
		}	// GetSum

		/// <summary>
		/// Run validation checks on input
		/// </summary>
		/// <param name="parts"></param>
		/// <exception cref="Exception"></exception>
		void ValidateInput(string[] parts)
		{
			// This constraint removed 1/15/2025 per Requirement #2
			// Throw an exception when more than 2 numbers are provided
			// if (parts.Length > 2)
			//	throw new Exception("A maximum of 2 numbers may be provided");
		}

		/// <summary>
		/// Convert string value to a decimal number, treating empty or invalid input as 0
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		decimal StringToNumber(string value)
		{
			decimal d;
			if(decimal.TryParse(value, out d))
				return d;
			return 0;
		}
	}	// class
}	// namespace
