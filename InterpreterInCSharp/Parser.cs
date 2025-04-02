namespace InterpreterInCSharp;

public class Parser
{
	Lexer l;
	public List<string> errors;
	public Dictionary<TokenType, PrefixParseFn> PrefixParseFns= new();
	public Dictionary<TokenType, InfixParseFn> InfixParseFns= new();

	Token curToken;
	Token peekToken;
	Dictionary<TokenType, Precedence> precedences = new(){
		[TokenType.EQ] = Precedence.EQUALS,
		[TokenType.NOT_EQ] = Precedence.EQUALS,
		[TokenType.LT] = Precedence.LESSGREATER,
		[TokenType.GT] = Precedence.LESSGREATER,
		[TokenType.PLUS] = Precedence.SUM,
		[TokenType.MINUS] = Precedence.SUM,
		[TokenType.SLASH] = Precedence.PRODUCT,
		[TokenType.ASTERISK] = Precedence.PRODUCT,
	};


	public Parser(Lexer lexer)
	{
		l=lexer;
		NextToken();
		NextToken();
		errors = [];

		PrefixParseFns = new();
		RegisterPrefixParseFunction(TokenType.IDENT, ParseIdentifier);
		RegisterPrefixParseFunction(TokenType.INT, ParseInteger);
		RegisterPrefixParseFunction(TokenType.BANG, ParsePrefixExpression);
		RegisterPrefixParseFunction(TokenType.MINUS, ParsePrefixExpression);
		InfixParseFns = new();
		RegisterInfixParseFunction(TokenType.PLUS, ParseInfixExpression);
		RegisterInfixParseFunction(TokenType.MINUS, ParseInfixExpression);
		RegisterInfixParseFunction(TokenType.SLASH, ParseInfixExpression);
		RegisterInfixParseFunction(TokenType.ASTERISK, ParseInfixExpression);
		RegisterInfixParseFunction(TokenType.GT, ParseInfixExpression);
		RegisterInfixParseFunction(TokenType.LT, ParseInfixExpression);
		RegisterInfixParseFunction(TokenType.EQ, ParseInfixExpression);
		RegisterInfixParseFunction(TokenType.NOT_EQ, ParseInfixExpression);
	}

	public ProgramBaseNode ParseProgram()
	{
		var program = new ProgramBaseNode();
		program.Statements = new();

		while (!CurTokenIs(TokenType.EOF))
		{
			IStatement statement = curToken.Type switch {
				TokenType.LET => ParseLetStatement(),
				TokenType.RETURN => ParseReturnStatement(),
				_ => ParseExpressionStatement()
			};

            if (statement != null)
				program.Statements.Add(statement);

			NextToken();
		}
		return program;
	}

	private IExpression ParsePrefixExpression()
	{
		var exp = new PrefixExpression()
		{
			Token = curToken,
		};
		exp.Operator = curToken.Literal;
		NextToken();

		exp.Right = ParseExpression((int)Precedence.PREFIX);
		return exp;
    }

	private IExpression ParseInfixExpression(IExpression expression)
	{
		var res = new InfixExpression() { 
			Left = expression, 
			Token = curToken, 
			Operator = curToken.Literal};

		if(!precedences.TryGetValue(curToken.Type, out var curPrecedence))
			curPrecedence = Precedence.LOWEST;
		NextToken();

		res.Right = ParseExpression((int)curPrecedence);
		return res;
	}

    private IExpression ParseInteger()
    {
		if (!int.TryParse(curToken.Literal, out int result))
		{
			errors.Add($"Could not parse {curToken.Literal} as integer");
		}
		var intLiteral = new IntegerLiteral() { Token = new() { Type = TokenType.INT, Literal = curToken.Literal }, Value = result };

		return intLiteral;
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
		throw new NotImplementedException();
    }

    private void NoPrefixParseFnError(TokenType type)
    {
		errors.Add($"No prefix parse function for: '{type}' was found");
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

	private int PeekPrecedence()
	{
		if (peekToken.Type == TokenType.EOF)
			return 0;
		if (!precedences.Keys.Contains(peekToken.Type))
			return 0;
		return (int)precedences[peekToken.Type];
	}
}

