using System;
using System.Linq;
using CodeGeneration.Roslyn.Tests.Common.InterfaceGeneration;

namespace CodeGeneration.Roslyn.MetricsCollector.Tests
{
	public class MetricsCollectorStubInheritanceListBuilder : IInheritanceListBuilder
	{
		public Func<ITestGenerationContext, InterfaceData[]> GetInheritedInterfaces(ITestInterfaceGenerationOptions options, int count)
		{
			return (context) =>
			{
				var interfaces = Enumerable.Range(context.Entries.Count, count).Select(GetInheritedInterfaceData).ToArray();
				foreach (var @interface in interfaces)
				{
					var namespaceData = new NamespaceData(@interface.Namespace, @interface);
					var compilationEntryData = new CompilationEntryData(options.UsingNamespaces, namespaceData);
					context.AddEntry(compilationEntryData);
				}
				return interfaces;
			};
		}

		private static InterfaceData GetInheritedInterfaceData(int index)
		{
			var metricsCollectorInterfaceAttributeData = new MetricsCollectorInterfaceAttributeData("Context_" + Guid.NewGuid());
			return new InterfaceData("ITestInheritedInterface" + index, "TestNamespaceForITestInheritedInterface" + index, new AttributeData[] { metricsCollectorInterfaceAttributeData }, Array.Empty<InterfaceMethodData>(), Array.Empty<InterfaceData>(), true);
		}
	}
}