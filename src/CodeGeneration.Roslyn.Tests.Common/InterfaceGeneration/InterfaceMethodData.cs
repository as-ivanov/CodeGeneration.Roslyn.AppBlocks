using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.Roslyn.Tests.Common.InterfaceGeneration
{
	public class InterfaceMethodData
	{
		private readonly Type _returnType;
		private readonly string _name;
		private readonly AttributeData[] _attributeDataList;
		private readonly MethodParameterData[] _parameters;

		private InterfaceMethodData(Type returnType, string name, AttributeData[] attributeDataList,
			MethodParameterData[] parameters)
		{
			_returnType = returnType;
			_name = name;
			_attributeDataList = attributeDataList;
			_parameters = parameters;
		}

		public string Name => _name;

		public Type ReturnType => _returnType;

		public AttributeData[] AttributeDataList => _attributeDataList;

		public MethodParameterData[] Parameters => _parameters;

		public static IEnumerable<Func<ITestGenerationContext, InterfaceMethodData>> GetPossibleVariations(
			ITestInterfaceGenerationOptions options)
		{
			var methodAttributeCombinations = options.MethodAttributeDataBuilder.GetCombinations(options);
			var methodParameterPossibleVariations = MethodParameterData.GetPossibleVariations(options).ToList();
			foreach (var attributeData in methodAttributeCombinations)
			{
				foreach (var returnType in options.InterfaceMethodReturnTypes)
				{
					foreach (var parametersCount in options.MethodParameterNumbers)
					{
						foreach (var parameters in methodParameterPossibleVariations
							.Combinations(parametersCount))
						{
							yield return (context) => new InterfaceMethodData(returnType, "Method" + context.NextId(),
								attributeData(context).ToArray(), parameters.ToArray());
						}
					}
				}
			}
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			foreach (var attributeData in AttributeDataList)
			{
				sb.AppendLine(attributeData.ToString());
			}
			var returnType = ReturnType == typeof(void) ? "void" : ReturnType.FullName;
			sb.AppendLine($"{returnType} {Name}({string.Join(",", Parameters.Select(_ => _.ToString()))});");
			return sb.ToString();
		}
	}
}