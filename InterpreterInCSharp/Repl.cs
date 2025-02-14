namespace InterpreterInCSharp;

public static class Repl
{
	const string PROMPT = ">> ";
	public static void Start()
	{
		Console.Write(PROMPT);
		string scanned = Console.ReadLine();
		if (string.IsNullOrEmpty(scanned))
			return;
		
		string line = scanned;
		Lexer l = new(line);
		for (var token = l.NextToken(); token.Type != TokenType.EOF; token = l.NextToken())
		{
			Console.WriteLine($"{{{token.Type}, Literal:{token.Literal} }}");
		}
	}
}
