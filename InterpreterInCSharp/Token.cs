
namespace InterpreterInCSharp;
public struct Token
{
    public TokenType Type;
    public string? Literal;
    private static Dictionary<string, TokenType> _keyWords = new(){{"let", TokenType.LET}, {"fn", TokenType.FUNCTION}};
    public Token(TokenType type, string? literal)
    {
        Type = type;
        Literal = literal;
    }

    internal static TokenType LookUpIdentifier(string identifier)
    {
        if (_keyWords.TryGetValue(identifier, out var keyWord))
            return keyWord;
        else
            return TokenType.IDENT;
    }
}
public enum TokenType
{
    ILLEGAL,
    EOF,

    // identifiers/literals
    IDENT,
    INT,

    // operators
    ASSIGN,
    PLUS,

    // delimiters
    COMMA,
    SEMICOLON,

    LPAREN,
    RPAREN,
    LBRACE,
    RBRACE,

    //keywords
    FUNCTION,
    LET
}
// public static class TokenExtensions
// {
//     public static readonly Dictionary<TokenType, string> tokenTypeToStringDict = new()
//     {
//         {TokenType.ILLEGAL, "ILLEGAL"},
//         {TokenType.EOF, "EOF"},
//         {TokenType.IDENT, "IDENT"},
//         {TokenType.INT, "INT"},
//         {TokenType.ASSIGN, "="},
//         {TokenType.PLUS, "+"},
//         {TokenType.COMMA, ","},
//         {TokenType.SEMICOLON, ";"},
//         {TokenType.LPARAN,"("},
//         {TokenType.RPARAN,")"},
//         {TokenType.LBRACE, "{"},
//         {TokenType.RBRACE, "}"},
//         {TokenType.FUNCTION, "FUNCTION"},
//         {TokenType.LET, "LET"}
//     };
//     public static string ToLiteral(this TokenType tokenType)
//     {
//         return tokenTypeToStringDict[tokenType];
//     }
// }