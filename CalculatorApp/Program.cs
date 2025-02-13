﻿
using CalculatorApp;
using CalculatorApp.Interfaces;

// CalculatorApp performs calculations for the Restaurant365 challenge

using Microsoft.Extensions.DependencyInjection;



// Wire up DI and launch the application
var serviceProvider = new ServiceCollection()
				.AddSingleton<Application, Application>()
				.AddSingleton<ICalculatorService, CalculatorService>()
				.AddSingleton<ICalculatorInputParser, CalculatorInputParser>()
				.BuildServiceProvider();

Application application = serviceProvider.GetRequiredService<Application>();
application.Run ();

