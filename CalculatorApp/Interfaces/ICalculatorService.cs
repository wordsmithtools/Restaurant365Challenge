namespace CalculatorApp
{
	public interface ICalculatorService
	{
		/// <summary>
		/// Calculate sum of inputs
		/// </summary>
		/// <param name="input">input string</param>
		/// <param name="showFormula">true to include formula in output</param>
		/// <returns></returns>
		string GetSum(string? input, bool showFormula);
	}
}
