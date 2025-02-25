namespace InterpreterInCSharp;

public class Parser
{
	Lexer l;

	Token curToken;
	Token peekToken;
	public Parser(Lexer lexer)
	{
		l=lexer;
		NextToken();
		NextToken();
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

		while (curToken.Type != TokenType.EOF)
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
			default: 
				return null;
		}
    }

    private LetStatement ParseLetStatement()
    {
		var stmt = new LetStatement();
		stmt.token = curToken;

		if (!ExpectPeek(TokenType.IDENT))
		{
			return null;
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
}

public interface INode
{
	public string TokenLiteral();
}

public interface IStatement: INode
{
	public void StatementNode();
}

public interface IExpression: INode
{
	public void ExpressionNode();
}

public class ProgramBaseNode: INode
{
	public List<IStatement> Statements;

	public string TokenLiteral()
	{
		if (Statements.Count() > 0)
			return Statements[0].TokenLiteral();
		else
			return "";
	}

}

public class Expression : IExpression
{
	public Token token;
	public string Value;

    public void ExpressionNode()
    {
        throw new NotImplementedException();
    }

    public string TokenLiteral()
    {
		return token.Literal;
    }
}

public class LetStatement : IStatement
{
	public Token token;
	public Identifier Name; 
	public IExpression Value;

    public void StatementNode()
    {
    }

    public string TokenLiteral()
    {
		return token.Literal;
    }
}

public class Identifier: IExpression
{
	public Token Token;
	public string Value;

    public void ExpressionNode()
    {
        throw new NotImplementedException();
    }

    public string TokenLiteral()
    {
		return Token.Literal;
    }
}