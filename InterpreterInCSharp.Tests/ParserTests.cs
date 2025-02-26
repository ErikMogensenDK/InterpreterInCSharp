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
		CheckForParserErrors(p);

		List<string> expectedIdentifiers = new (){"x", "y", "foobar"};

		for(int j = 0; j< expectedIdentifiers.Count(); j++)
		{
			var stmt = program.Statements[j];
			if (!testLetStatement(stmt, expectedIdentifiers[j]))
			{
				Assert.IsTrue(false);
			}
		}
	}

	[TestMethod]
	public void TestReturnStatement()
	{
		string input = @"
		return 5;
		return 10;
		return 993322;
		";
		Lexer l = new(input);
		Parser p = new(l);

		var program = p.ParseProgram();
		CheckForParserErrors(p);

		Assert.AreEqual(3, program.Statements.Count);
		foreach(var statement in program.Statements)
		{
			var stmt = (ReturnStatement)statement;
			if (stmt == null)
			{
				Console.WriteLine($"Error: Expected returnStatement, but got: '{statement.GetType()}'");
			}
			if (stmt.TokenLiteral() != "return");
			{
				Console.WriteLine($"Error: TokenLiteral to be 'return', but got: {stmt.TokenLiteral()}");
			}
		}
	}

    private void CheckForParserErrors(Parser p)
    {
		if (p.errors.Count == 0)
			return;
		else{
			foreach (var error in p.errors)
			{
				Console.WriteLine("Parser Error:");
				Console.WriteLine(error);
			}
			Assert.IsTrue(false);
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
	[TestMethod]
	public void TestStringMethods()
	{

		ProgramBaseNode program = new()
		{
			Statements = new(){
				new LetStatement()
				{
					token = new(){Type= TokenType.LET, Literal= "let"},
					Name = new Identifier(){Token= new(){Type= TokenType.IDENT, Literal= "myVar"},Value= "myVar"},
					Value = new Identifier(){Token= new(){Type= TokenType.IDENT, Literal= "anotherVar"}, Value= "anotherVar"},
				},
			},
		};

		string expectedString= "let myVar = anotherVar;";
		Assert.AreEqual(expectedString, program.String());
	}

	[TestMethod]
	public void TestParsingOfIdentierExpression()
	{
		string input = "foobar;";
		Lexer l = new(input);
		Parser p = new(l);
		var program = p.ParseProgram();
		CheckForParserErrors(p);

		Assert.AreEqual(1, program.Statements.Count);

		var stmt = (ExpressionStatement)program.Statements[0];
		if (stmt.GetType() != new ExpressionStatement().GetType())
		{
			Console.WriteLine($"Statement was not an ExpressionStatement, got: '{stmt.GetType()}'");
			Assert.IsTrue(false);
		}
		var ident = (Identifier)stmt.expression;
		if (ident.GetType() != new Identifier().GetType())
		{
			Console.WriteLine($"Nested expression was not an identifier, got: '{stmt.GetType()}'");
			Assert.IsTrue(false);
		}
		
		Assert.AreEqual("foobar", ident.Value);
		Assert.AreEqual("foobar", ident.TokenLiteral());
	}

}