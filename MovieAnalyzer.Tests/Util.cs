using System.Text.Json;

namespace MovieAnalyzer.Tests
{
	internal class Util
	{
		public static bool AreEqualByJson(object expected, object actual)
		{
			var expectedJson = JsonSerializer.Serialize(expected);
			var actualJson = JsonSerializer.Serialize(actual);
			return string.Equals(expectedJson, actualJson);
		}
	}
}
