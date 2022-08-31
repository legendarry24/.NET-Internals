Goal: The goal of this project is to get practical experience in usage of new API related to memory management and asynchronous programming and also understand how it affects memory and performance characteristics of your app.

Overview: The overall solution performs calculation of sum of all the integers separated by ',' which are written in file on multiple lines.

Structure:
	AppData
		// test files are located here
	BenchmarkResults
		// contains reports of the benchmarks ran
	Benchmarks
		// classes that contain benchmark tests for Core classes
		BenchmarkBase.cs 
			// this benchmark class contains a list of test files to run tests on. 
			// For development stage just comment the [Params] attribute and assign the name of single file with 1 or 10 lines to the TestFileName field.
	Core
		IntegersCalculator.cs       // the top level class that performs calculation of sum of all integers in a file
		SingleLineCalculator.cs     // class that is responsible for caclulation of sum of integeres for a single line (string)
		IntegersParser.cs           // class that is responsible to parse integers from a line (string)
		FileInputReader.cs          // class that is responsible for reading lines of integers from the file
		FileInputAsyncEnumerable.cs // async enumerable that must read lines of integers from file asynchronously
	Utils
		InputGenerator.cs // class that generates lines of integers separated by a separator with a certain percentage of duplicates
		Program.cs        // starting point of the console app

How to use:
	1) Launch benchmarks in Release mode.
	2) Explore results in the bin\Release\net5.0\BenchmarkDotNet.Artifacts\results
	3) Comment out FullBenchmarksRun(); in Main and uncomment TestMethod().Wait(); in order to debug your code during implementation
	4) Debug methods for a particular task by calling particular method of IntegersCalculator class in the TestMethod() method of Program.cs
	5) Once implementation is working properly, comment out TestMethod().Wait(); in Main and uncomment FullBenchmarksRun();
	6) Change configuration to Release mode and launch benchmarks for 1 file (do not uncommend Params attribute in BenchmarkBase class)
	7) Explore results, if the results are good for you, include them into BenchmarkResults/My folder (use .asciidoc files)
		Pay attention, results are overriden on every run in the bin\Release\net5.0\BenchmarkDotNet.Artifacts\results folder.
		The minimal enough result is to run benchmarks on a SINGLE file with 10 lines (configured by default).
		Also there is a possibility to run benchmarks on all files with data.

Tasks:
	Task 1: Use Span<T> to optimize memory allocations
	Task 2: Implement async version of sum calculation. Use Memory<T> to reduce heap memory allocations
	Task 3: Implement caching in SingleLineCaclulator to improve performacne using private dictionary of string hash and sum of integers within this string. Use ValueTask to reduce heap memory allocations 
	Task 4: Implement IAsyncEnumerable EnumerateAsync. Implement FileInputAsyncEnumerable and FileInputAsyncEnumerator
