

namespace InterpreterInCSharp;

public class Parser
{
	Lexer l;
	public List<string> errors;
	public Dictionary<TokenType, PrefixParseFn> PrefixParseFns= new();
	public Dictionary<TokenType, InfixParseFn> InfixParseFns= new();

	Token curToken;
	Token peekToken;

	public Parser(Lexer lexer)
	{
		l=lexer;
		NextToken();
		NextToken();
		errors = [];

		PrefixParseFns = new();
		RegisterPrefixParseFunction(TokenType.IDENT, ParseIdentifier);
	}

	public List<string> Errors()
	{
		return errors;
	}

	private void peekError(TokenType type)
	{
		string message = $"Expected next token to be '{type}', but got: '{peekToken.Type}' instead";
		errors.Add(message);
	}

    private void NextToken()
    {
		curToken = peekToken;
		peekToken = l.NextToken();
    }

	public ProgramBaseNode ParseProgram()
	{
		var program = new ProgramBaseNode();
		program.Statements = new();

		while (!CurTokenIs(TokenType.EOF))
		{
			var statement = ParseStatement();
			if (statement != null)
			{
				program.Statements.Add(statement);
			}
			NextToken();
		}
		return program;
	}

    private IStatement ParseStatement()
    {
		switch (curToken.Type)
		{
			case TokenType.LET:
				return ParseLetStatement();

			case TokenType.RETURN:
				return ParseReturnStatement();
			default: 
				return ParseExpressionStatement();
		}
    }

    private IStatement ParseExpressionStatement()
    {
		var stmt = new ExpressionStatement(){token = curToken};
		stmt.expression = ParseExpression((int)Precedence.LOWEST);

		if (PeekTokenIs(TokenType.SEMICOLON))
		{
			NextToken();
		}
		return stmt;
    }

    private IExpression ParseExpression(int precedence)
    {
		var prefix = PrefixParseFns[curToken.Type];
		if (prefix == null)
		{
			return null;
		}
		var leftExp = prefix();
		return leftExp;
    }

    private ReturnStatement ParseReturnStatement()
    {
		var stmt = new ReturnStatement(){token = curToken};

		//TODO fill out the expression in ReturnValue


		NextToken();

		while (!CurTokenIs(TokenType.SEMICOLON))
		{
			NextToken();
		}
		return stmt;
    }

    private LetStatement ParseLetStatement()
    {
		var stmt = new LetStatement();
		stmt.token = curToken;

		if (!ExpectPeek(TokenType.IDENT))
		{
			peekError(TokenType.IDENT);
		}

		stmt.Name = new(){Token = curToken, Value = curToken.Literal};

		//expressions go here
		while(!CurTokenIs(TokenType.SEMICOLON))
		{
			NextToken();
		}
		return stmt;
    }

    private bool CurTokenIs(TokenType type)
    {
		return curToken.Type == type;
    }

	private bool PeekTokenIs(TokenType type)
	{
		return peekToken.Type == type;
	}

    private bool ExpectPeek(TokenType type)
    {
		if(PeekTokenIs(type))
		{
			NextToken();
			return true;
		}
		else
			return false;
    }

	private void RegisterInfixParseFunction(TokenType token, InfixParseFn fn)
	{
		InfixParseFns[token] = fn;
	}

	private void RegisterPrefixParseFunction(TokenType token, PrefixParseFn fn)
	{
		PrefixParseFns[token] = fn;
	}

	private IExpression ParseIdentifier()
	{
		return new Identifier(){Token = curToken, Value = curToken.Literal};
	}
}

