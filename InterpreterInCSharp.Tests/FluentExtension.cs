namespace InterpreterInCSharp.Tests;
public static class FluentExtension
{
	public static void ShouldBeEqualTo(this string str, string otherString)
	{
		Assert.AreEqual(str, otherString);
	}
}