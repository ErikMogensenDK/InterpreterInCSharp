using InterpreterInCSharp;

namespace InterpreterInCSharp.Tests;

[TestClass]
public class ParserTests
{
    [TestMethod]
    public void ParseProgram_returnsExpectedAst()
    {
		string input = @"
		let x = 5;
		let y = 10;
		let foobar = 838383;
		";
		Lexer l = new(input);
		Parser p = new(l);

		var program = p.ParseProgram();
		Assert.IsNotNull(program);
		Assert.AreEqual(3, program.Statements.Count());

		List<string> expectedIdentifiers = new (){"x", "y", "foobar"};

		// int i = 0;
		// int tt = 0;
		for(int j = 0; j< expectedIdentifiers.Count(); j++)
		{
			var stmt = program.Statements[j];
			if (!testLetStatement(stmt, expectedIdentifiers[j]))
			{
				Console.WriteLine("ERROR - See output above");
				Assert.IsTrue(false);
			}
		}
	}

    private bool testLetStatement(IStatement stmt, string name)
    {
		if (stmt.TokenLiteral() != "let")
		{
			Console.WriteLine($"stmt.TokenLiteral not 'let'. Got {stmt.TokenLiteral()}");
			return false;
		}
		LetStatement letStmt = (LetStatement)stmt;

		if (stmt.GetType() != new LetStatement().GetType())
		{
			Console.WriteLine($"Check for type did not work! expected {new LetStatement().GetType()}, but got{stmt.GetType()}");
			return false;
		}
		if (letStmt.Name.Value != name)
		{
			Console.WriteLine($"letStmt.Name.Value not '{name}'. Got {letStmt.Name.Value}");
			return false;
		}
		if (letStmt.Name.TokenLiteral() != name)
		{
			Console.WriteLine($"letStmt.Name.TokenLiteral not '{name}'. Got {letStmt.Name.TokenLiteral()}");
			return false;
		}
		return true;
    }
}