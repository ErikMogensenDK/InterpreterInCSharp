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
            new Token{Type = TokenType.LPAREN, Literal = "("},
            new Token{Type = TokenType.RPAREN, Literal = ")"},
            new Token{Type = TokenType.LBRACE, Literal = "{"},
            new Token{Type = TokenType.RBRACE, Literal = "}"},
            new Token{Type = TokenType.COMMA, Literal = ","},
            new Token{Type = TokenType.SEMICOLON, Literal = ";"},
        };
        Lexer lexer = new(input);
        for (int i = 0; i < input.Length; i++)
        {
            Token currentToken = lexer.NextToken();
#pragma warning disable CS8604 // Possible null reference argument.
            expectedTokens[i].Literal.ShouldBeEqualTo(currentToken.Literal);
            Assert.AreEqual(expectedTokens[i].Type, currentToken.Type);
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
    [TestMethod]
    public void NextToken_returnsExpectedTokensInComplexCase()
    {
        var input = @"let five = 5;
let ten = 10;

let add = fn(x, y) {
  x + y;
};

let result = add(five, ten);
!-/*5;
5 < 10 > 5;
if (5 < 10) {
	return true;
} else {
	return false;
}

10 == 10;
10 != 9;
";

        var expectedTokens = new List<Token>(){
            new(TokenType.LET, "let"),
            new(TokenType.IDENT, "five"),
            new(TokenType.ASSIGN, "="),
            new(TokenType.INT, "5"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.LET, "let"),
            new(TokenType.IDENT, "ten"),
            new(TokenType.ASSIGN, "="),
            new(TokenType.INT, "10"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.LET, "let"),
            new(TokenType.IDENT, "add"),
            new(TokenType.ASSIGN, "="),
            new(TokenType.FUNCTION, "fn"),
            new(TokenType.LPAREN, "("),
            new(TokenType.IDENT, "x"),
            new(TokenType.COMMA, ","),
            new(TokenType.IDENT, "y"),
            new(TokenType.RPAREN, ")"),
            new(TokenType.LBRACE, "{"),
            new(TokenType.IDENT, "x"),
            new(TokenType.PLUS, "+"),
            new(TokenType.IDENT, "y"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.RBRACE, "}"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.LET, "let"),
            new(TokenType.IDENT, "result"),
            new(TokenType.ASSIGN, "="),
            new(TokenType.IDENT, "add"),
            new(TokenType.LPAREN, "("),
            new(TokenType.IDENT, "five"),
            new(TokenType.COMMA, ","),
            new(TokenType.IDENT, "ten"),
            new(TokenType.RPAREN, ")"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.BANG, "!"),
            new(TokenType.MINUS, "-"),
            new(TokenType.SLASH, "/"),
            new(TokenType.ASTERISK, "*"),
            new(TokenType.INT, "5"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.INT, "5"),
            new(TokenType.LT, "<"),
            new(TokenType.INT, "10"),
            new(TokenType.GT, ">"),
            new(TokenType.INT, "5"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.IF, "if"),
            new(TokenType.LPAREN, "("),
            new(TokenType.INT, "5"),
            new(TokenType.LT, "<"),
            new(TokenType.INT, "10"),
            new(TokenType.RPAREN, ")"),
            new(TokenType.LBRACE, "{"),
            new(TokenType.RETURN, "return"),
            new(TokenType.TRUE, "true"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.RBRACE, "}"),
            new(TokenType.ELSE, "else"),
            new(TokenType.LBRACE, "{"),
            new(TokenType.RETURN, "return"),
            new(TokenType.FALSE, "false"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.RBRACE, "}"),
            new(TokenType.INT, "10"),
            new(TokenType.EQ, "=="),
            new(TokenType.INT, "10"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.INT, "10"),
            new(TokenType.NOT_EQ, "!="),
            new(TokenType.INT, "9"),
            new(TokenType.SEMICOLON, ";"),
            new(TokenType.EOF, "")};
        Lexer lexer = new(input);
        for (int i = 0; i < expectedTokens.Count; i++)
        {
            Token currentToken = lexer.NextToken();

            #pragma warning disable CS8604 // Possible null reference argument.

            Assert.AreEqual(expectedTokens[i].Type, currentToken.Type);
            expectedTokens[i].Literal.ShouldBeEqualTo(currentToken.Literal);

            #pragma warning restore CS8604 // Possible null reference argument.
        }

    }
}
