using Microsoft.Extensions.DependencyInjection;

namespace CalculatorApp.Tests
{
	[TestClass]
	public class CalculatorServiceTests : TestBase
	{
		/// <summary>
		/// Test requirements for GetSum method of CalculatorService
		/// </summary>
		[TestMethod]
		public void TestGetSum()
		{

			var calculatorService = _serviceProvider.GetRequiredService<ICalculatorService>();

			var value = calculatorService.GetSum("", false);
			Assert.AreEqual("0", value);

			value = calculatorService.GetSum("", true);
			Assert.AreEqual("0 = 0", value);

			value = calculatorService.GetSum("1", false);
			Assert.AreEqual("1", value);

			value = calculatorService.GetSum("1", true);
			Assert.AreEqual("1 = 1", value);

			value = calculatorService.GetSum("1,2.2", false);
			Assert.AreEqual("3.2", value);

			value = calculatorService.GetSum("1,2.2", true);
			Assert.AreEqual("1+2.2 = 3.2", value);

			value = calculatorService.GetSum("1,", false);
			Assert.AreEqual("1", value);

			value = calculatorService.GetSum("1,", true);
			Assert.AreEqual("1+0 = 1", value);

			value = calculatorService.GetSum("1,abc", false);
			Assert.AreEqual("1", value);

			value = calculatorService.GetSum("1,abc", true);
			Assert.AreEqual("1+0 = 1", value);

			try
			{
				value = calculatorService.GetSum("1,2,3", false);
				Assert.Fail("This should have thrown an exception as there are too many values");
			}
			catch (Exception ex) { }

		}
	}
}