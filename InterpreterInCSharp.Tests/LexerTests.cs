using InterpreterInCSharp;
namespace InterpreterInCSharp.Tests;

[TestClass]
public class LexerTests
{
    [TestMethod]
    public void NextToken_returnsExpectedTokens()
    {
        string input = "=+(){},;";
        List<Token> expectedTokens = new List<Token>()
        {
            new Token{Type = TokenType.ASSIGN, Literal = "="},
            new Token{Type = TokenType.PLUS, Literal = "+"},
            new Token{Type = TokenType.LPARAN, Literal = "("},
            new Token{Type = TokenType.RPARAN, Literal = ")"},
            new Token{Type = TokenType.LBRACE, Literal = "{"},
            new Token{Type = TokenType.RBRACE, Literal = "}"},
            new Token{Type = TokenType.COMMA, Literal = ","},
            new Token{Type = TokenType.SEMICOLON, Literal = ";"},
        };
        Lexer lexer = new(input);
        for (int i = 0; i < input.Length; i++)
        {
            Token currentToken = lexer.NextToken();
            expectedTokens[i].Literal.ShouldBeEqualTo(currentToken.Literal);
#pragma warning disable CS8604 // Possible null reference argument.
            expectedTokens[i].ToString().ShouldBeEqualTo(currentToken.ToString());
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
    [TestMethod]
    public void NextToken_returnsExpectedTokensInComplexCase()
    {
        
    }
}