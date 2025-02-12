namespace InterpreterInCSharp;
public struct Token
{
    public TokenType Type;
    public string? Literal;
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

    LPARAN,
    RPARAN,
    LBRACE,
    RBRACE,

    //keywords
    FUNCTION,
    LET
}
public static class TokenExtensions
{
    public static readonly Dictionary<TokenType, string> tokenTypeToStringDict = new()
    {
        {TokenType.ILLEGAL, "ILLEGAL"},
        {TokenType.EOF, "EOF"},
        {TokenType.IDENT, "IDENT"},
        {TokenType.INT, "INT"},
        {TokenType.ASSIGN, "="},
        {TokenType.PLUS, "+"},
        {TokenType.COMMA, ","},
        {TokenType.SEMICOLON, ";"},
        {TokenType.LPARAN,"("},
        {TokenType.RPARAN,")"},
        {TokenType.LBRACE, "{"},
        {TokenType.RBRACE, "}"},
        {TokenType.FUNCTION, "FUNCTION"},
        {TokenType.LET, "LET"}
    };
    public static string ToLiteral(this TokenType tokenType)
    {
        return tokenTypeToStringDict[tokenType];
    }
}