using Microsoft.Extensions.DependencyInjection;

namespace CalculatorApp.Tests
{
	public abstract class TestBase
	{
		protected readonly ServiceProvider _serviceProvider;

		public TestBase()
		{
			// Wire up DI
			_serviceProvider = new ServiceCollection()
							.AddSingleton<ICalculatorService, CalculatorService>()
							.BuildServiceProvider();

		}

	}	// class
}	// namespace
