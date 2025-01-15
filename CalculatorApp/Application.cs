namespace CalculatorApp
{
	/// <summary>
	/// Main application logic for the challenge calculator
	/// </summary>
	public class Application
	{
		private readonly ICalculatorService _calculatorService;

		public Application(ICalculatorService calculatorService)
		{
			_calculatorService = calculatorService;
		}

		public void Run()
		{

			Console.WriteLine("Welcome to the challenge calculator!");
			string sum;
			while (true)
			{   // continue to accept inputs until program is closed
				Console.WriteLine("Enter a maximum of 2 numbers using a comma delimiter:");

				string? input = Console.ReadLine();

				// Since the calculator needs to allow users to input newlines or other special characters, treat backslashes as escape characters.
				// User should enter \\ for a literal backslash. Alternatively these could come in as command args (not implemented)
				if(input != null) 
					input = System.Text.RegularExpressions.Regex.Unescape(input);

				sum = _calculatorService.GetSum(input, false);
				Console.WriteLine($"The sum is: {sum}");
			}
		}
	}	// class
}	// namespace
