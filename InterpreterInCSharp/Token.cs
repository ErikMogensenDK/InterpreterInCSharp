
namespace InterpreterInCSharp;
public struct Token
{
    public string TokenType;
    public string literal;

    public const string ILLEGAL = "ILLEGAL";
    public const string EOF = "EOF";

    //identifiers
    public const string IDENT = "IDENT";
    public const string INT = "INT";

    //operators
    public const string ASSIGN = "=";
    public const string PLUS = "+";

    //delimiters
    public const string COMMA = ",";
    public const string SEMICOLON = ";";

}