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