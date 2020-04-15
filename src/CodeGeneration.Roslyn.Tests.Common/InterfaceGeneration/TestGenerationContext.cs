using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.Roslyn.Tests.Common.InterfaceGeneration
{
	public class TestGenerationContext : ITestGenerationContext
	{
		private readonly ITestInterfaceGenerationOptions _options;
		private readonly List<CompilationEntryData> _entries = new List<CompilationEntryData>();

		public TestGenerationContext(ITestInterfaceGenerationOptions options)
		{
			_options = options;
		}

		public void AddEntry(CompilationEntryData compilationEntryData)
		{
			_entries.Add(compilationEntryData);
		}

		public ITestInterfaceGenerationOptions Options => _options;

		public IReadOnlyList<CompilationEntryData> Entries => _entries;

		public override string ToString()
		{
			return string.Join(Environment.NewLine, Entries);
		}
	}
}