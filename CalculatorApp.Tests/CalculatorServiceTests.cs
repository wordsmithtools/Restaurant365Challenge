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

			value = calculatorService.GetSum("1,2,3,4,5,6,7,8,9,10,11,12", false);
			Assert.AreEqual("78", value);

			// Req 3. Support newline as alternative delimiter
			value = calculatorService.GetSum("1\n2,3", false);
			Assert.AreEqual("6", value);

			value = calculatorService.GetSum("-1\n2\nabc", false);
			Assert.AreEqual("1", value);


			// constraint removed in Req. 2
			//bool failed = false;
			//try
			//{
			//	value = calculatorService.GetSum("1,2,3", false);
			//}
			//catch (Exception)
			//{ 
			//	failed = true;
			//}
			//Assert.IsTrue(failed, "This should have thrown an exception as there are too many values");

		}
	}
}