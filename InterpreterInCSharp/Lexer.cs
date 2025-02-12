namespace InterpreterInCSharp;

public class Lexer
{
    private string _input; 
    private int _position;
    private int _readPosition; 
    public string? CurrentSymbol { get; set; }

    public Lexer(string input)
    {
        _input = input;
        _readPosition = 0;
        ReadChar();
    }
    private void ReadChar()
    {
        if (_input.Length <= _readPosition)
        {
            CurrentSymbol = null;
        }
        else
        {
            CurrentSymbol = _input[_readPosition].ToString();
        }
        _position = _readPosition;
        _readPosition++;
    }
    public Token NextToken()
    {
        Token token = new();
        token.Literal = CurrentSymbol;
        switch (CurrentSymbol)
        {
            case "=":
            {
                token.Type = TokenType.ASSIGN;
                break;
            }
            case "(":
            {
                token.Type = TokenType.LPARAN;
                break;
            }
            case ")":
            {
                token.Type = TokenType.RPARAN;
                break;
            }
            case "{":
            {
                token.Type = TokenType.LBRACE;
                break;
            }
            case "}":
            {
                token.Type = TokenType.RBRACE;
                break;
            }
            case ",":
            {
                token.Type = TokenType.COMMA;
                break;
            }
            case ";":
            {
                token.Type = TokenType.SEMICOLON;
                break;
            }
            case "+":
            {
                token.Type = TokenType.PLUS;
                break;
            }
            default:
            {
                token.Type = TokenType.EOF;
                token.Literal = "";
                break;
            }
        }
        ReadChar();
        return token;
    }
}