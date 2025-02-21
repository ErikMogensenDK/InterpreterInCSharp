namespace InterpreterInCSharp;

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
	public IStatement[] Statements;

	public string TokenLiteral()
	{
		if (Statements.Count() > 0)
			return Statements[0].TokenLiteral();
		else
			return "";
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

public class Identifier : IExpression
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