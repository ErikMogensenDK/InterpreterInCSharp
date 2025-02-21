namespace InterpreterInCSharp;

class Parser
{
	Lexer l;

	Token curToken;
	Token peekToken;
	public Parser(Lexer lexer)
	{
		l=lexer;
	}
}