namespace CalculatorApp
{
	public interface ICalculatorService
	{
		/// <summary>
		/// Calculate sum of positive numbers from 0-1000 with default delimiters of [,] and [\n]
		/// </summary>
		/// <param name="input">input values separated by delimiters, in format //[{delimiter1}][{delimiter2}]...\n{numbers}</param>
		/// <param name="showFormula">whether to show the formula in the output</param>
		/// <returns></returns>
		string GetSum(string? input, bool showFormula);

		/// <summary>
		/// Calculate sum of numbers
		/// </summary>
		/// <param name="input">input values separated by delimiters, in format //[{delimiter1}][{delimiter2}]...\n{numbers}</param>
		/// <param name="showFormula">whether to show the formula in the output</param>
		/// <param name="allowNegativeNumbers">true to allow negative numbers</param>
		/// <param name="maxInputValue">true to include formula in output</param>
		/// <param name="defaultDelimiters">Default delimiters to include</param>
		/// <returns></returns>
		string GetSum(string? input, bool showFormula, bool allowNegativeNumbers, decimal maxInputValue, IEnumerable<string> defaultDelimiters);
	}	// interface
}	// class
