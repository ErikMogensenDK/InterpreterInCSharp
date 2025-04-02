namespace InterpreterInCSharp;

public interface INode
{
	public string TokenLiteral();
	public string String();

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

	public string String()
	{
		string strings = "";

		foreach(var stmt in Statements)
		{
			strings += stmt.String();
		}
		return strings;
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

    public string String()
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

	public string String()
	{
		var myString = "";
		myString += token.Literal + " "; 
		myString += Name.String();
		myString += " = ";
		if (Value != null)
		{
			myString += Value.String(); 
		}
		myString += ";";
		return myString;
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

	public string String()
	{
		return Value;
	}
}

public class ReturnStatement : IStatement
{
	public Token token;
	public Expression ReturnValue;

	public void StatementNode()
    {
        throw new NotImplementedException();
    }

    public string TokenLiteral()
    {
		return token.Literal;
    }

	public string String()
	{
		var myString = "";
		myString += token.Literal;

		if (ReturnValue != null)
		{
			myString += ReturnValue.String();
		}
		myString += ";";
		return myString;
	}
}

public class ExpressionStatement : IStatement
{
	public Token token;
	public IExpression expression;
    public void StatementNode()
    {
        throw new NotImplementedException();
    }

    public string String()
    {
		if (expression != null)
			return expression.String();
		return "";
    }

    public string TokenLiteral()
    {
		return token.Literal;
    }
}

public delegate IExpression PrefixParseFn();
public delegate IExpression InfixParseFn(IExpression expression);


public enum Precedence
{
	_,
	LOWEST,
	EQUALS, 	// ==
	LESSGREATER,// > or <
	SUM, 		// +
	PRODUCT, 	// *
	PREFIX, 	// -X or !X
	CALL 		// myFunction(X)
}

public class IntegerLiteral : IExpression
{
	public Token Token;
	public int Value;

    public void ExpressionNode()
    {
        throw new NotImplementedException();
    }

    public string String()
    {
		return Token.Literal;
    }

    public string TokenLiteral()
    {
		return Token.Literal;
    }
}


public class PrefixExpression : IExpression
{
	public Token Token;
	public string Operator;
	public IExpression Right;

    public void ExpressionNode()
    {
        throw new NotImplementedException();
    }

    public string String()
    {
		return TokenLiteral();
    }

    public string TokenLiteral()
    {
		string myString = "";
		myString += $"(";
		myString += Operator;
		myString += Right.String();
		myString += $")";
		return myString;
    }
}

public class InfixExpression : IExpression
{
	public Token Token;
	public IExpression Left;
	public string Operator;
	public IExpression Right;

    public void ExpressionNode()
    {
        throw new NotImplementedException();
    }

    public string String()
    {
		string myString = "";
		myString += $"(";
		myString += Left.String();
		myString += " " + Operator + " ";
		myString += Right.String();
		myString += $")";
		return myString;
    }

    public string TokenLiteral()
    {
		return Token.Literal;
    }
}
