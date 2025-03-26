namespace InterpreterInCSharp;

public class Lexer
{
    private string _input; 
    private int _position;
    private int _readPosition; 
    public char _currentSymbol; 

    public Lexer(string input)
    {
        _input = input;
        _readPosition = 0;
        ReadChar();
    }

    private void ReadChar()
    {
        if (_input.Length <= _readPosition)
            _currentSymbol = '\xff'; // \xff is used as stand-in for null
        else
            _currentSymbol = _input[_readPosition];
        _position = _readPosition;
        _readPosition++;
    }

    public Token NextToken()
    {
        Token token = new();
        SkipWhiteSpace();
        switch (_currentSymbol)
        {
            case '=':
                {
                    if(PeekChar() == '=')
                    {
                        token = new(TokenType.EQ, "==");
                        ReadChar();
                    }
                    else
                    {
                        token = new(TokenType.ASSIGN, _currentSymbol.ToString());
                    }
                    break;
                }
            case '(':
                {
                    token = new(TokenType.LPAREN, _currentSymbol.ToString());
                    break;
                }
            case ')':
                {
                    token = new(TokenType.RPAREN, _currentSymbol.ToString());
                    break;
                }
            case '{':
                {
                    token = new(TokenType.LBRACE, _currentSymbol.ToString());
                    break;
                }
            case '}':
                {
                    token = new(TokenType.RBRACE, _currentSymbol.ToString());
                    break;
                }
            case ',':
                {
                    token = new(TokenType.COMMA, _currentSymbol.ToString());
                    break;
                }
            case ';':
                {
                    token = new(TokenType.SEMICOLON, _currentSymbol.ToString());
                    break;
                }
            case '+':
                {
                    token = new(TokenType.PLUS, _currentSymbol.ToString());
                    break;
                }
            case '\xff':
                {
                    token = new(TokenType.EOF, "");
                    break;
                }
            case '-':
                {
                    token = new(TokenType.MINUS, "-");
                    break;
                }
            case '!':
                {
                    if(PeekChar() == '=')
                    {
                        token = new(TokenType.NOT_EQ, "!=");
                        ReadChar();
                    }
                    else
                        token = new(TokenType.BANG, "!");
                    break;
                }
            case '*':
                {
                    token = new(TokenType.ASTERISK, "*");
                    break;
                }
            case '/':
                {
                    token = new(TokenType.SLASH, "/");
                    break;
                }
            case '<':
                {
                    token = new(TokenType.LT, "<");
                    break;
                }
            case '>':
                {
                    token = new(TokenType.GT, ">");
                    break;
                }
            default:
                {
                    if (char.IsLetter(_currentSymbol) || _currentSymbol == '_')
                    {
                        string identifier = ReadIdentifier();
                        var type = Token.LookUpIdentifier(identifier);
                        return new(type, identifier); //early return nescessary, since ReadChar was already called by "ReadIdentifier"
                    }
                    else if (char.IsDigit(_currentSymbol))
                    {
                        string digit = ReadDigit();
                        return new(TokenType.INT, digit);
                    }
                    else
                    {
                        token = new(TokenType.ILLEGAL, _currentSymbol.ToString());
                    }
                    break;
                }
        }
        ReadChar();
        return token;
    }

    private string ReadDigit()
    {
        var startPosition = _position; 
        while (char.IsDigit(_currentSymbol))
        {
            ReadChar();
        }
        return _input[startPosition .. _position];
    }

    private void SkipWhiteSpace()
    {
        while (_currentSymbol == ' ' || _currentSymbol == '\n' || _currentSymbol == '\r' || _currentSymbol == '\t')
            ReadChar();
    }

    private string ReadIdentifier()
    {
        var startPosition = _position; 
        while (char.IsLetter(_currentSymbol) || _currentSymbol == '_')
        {
            ReadChar();
        }
        return _input[startPosition .. _position];
    }

    private char PeekChar()
    {
        if (_readPosition >= _input.Length)
            return '\xff';
        else
            return _input[_readPosition];
    }
}