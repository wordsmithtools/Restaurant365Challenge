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

			value = calculatorService.GetSum("1\n2\nabc", false);
			Assert.AreEqual("3", value);

			// Req 4. Deny negative numbers by throwing an exception that includes all of the negative numbers provided
			var ex = Assert.ThrowsException<Exception>(() => calculatorService.GetSum("1,-1", false));
			Assert.IsTrue(ex.Message.EndsWith(": -1"));

			ex = Assert.ThrowsException<Exception>(() => calculatorService.GetSum("1,-1,-.5", false));
			Assert.IsTrue(ex.Message.EndsWith(": -1,-0.5"));

			// Req 5. Make any value greater than 1000 an invalid number
			value = calculatorService.GetSum("1,1000,2", false);
			Assert.AreEqual("1003", value);

			value = calculatorService.GetSum("1,1000.1,2", false);
			Assert.AreEqual("3", value);

			value = calculatorService.GetSum("1,1001,2", false);
			Assert.AreEqual("3", value);

			value = calculatorService.GetSum("1,999.99,2", false);
			Assert.AreEqual("1002.99", value);

			// Req 6. check for custom delimiter of form //{delimiter}\n{numbers}
			value = calculatorService.GetSum("//#\n2#5", false);
			Assert.AreEqual("7", value);

			value = calculatorService.GetSum("2#5,", false);
			Assert.AreEqual("0", value);

			value = calculatorService.GetSum("//,\n2,ff,100", false);
			Assert.AreEqual("102", value);

			value = calculatorService.GetSum("//,\n", false);
			Assert.AreEqual("0", value);

			// Req 7. Support 1 custom delimiter of any length using the format: //[{delimiter}]\n{numbers}
			value = calculatorService.GetSum("//[***]\n11***22***33", false);
			Assert.AreEqual("66", value);

			value = calculatorService.GetSum("   //[***]\n11***22***33    ", false); // should ignore whitespace
			Assert.AreEqual("66", value);

			value = calculatorService.GetSum("//[***]\n", false);
			Assert.AreEqual("0", value);

			value = calculatorService.GetSum("//[***]\n11***22\n33,//[]\n", false);
			Assert.AreEqual("66", value);

			value = calculatorService.GetSum("//[12]\n5125", false);
			Assert.AreEqual("10", value);

			// Req 8. Support multiple delimiters of any length using the format: //[{delimiter1}][{delimiter2}]...\n{numbers}
			value = calculatorService.GetSum("//[*][!!][r9r]\n11r9r22*hh*33!!44", false);
			Assert.AreEqual("110", value);

			value = calculatorService.GetSum("//[*][!!][][]]\n1*2!!3*0!2", false);
			Assert.AreEqual("6", value);

			value = calculatorService.GetSum(" //[*][!!][x]]\n1*2!!3*0!!2", false);
			Assert.AreEqual("8", value);

		}
	}	// class
}	// namespace