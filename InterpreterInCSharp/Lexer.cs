


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
            _currentSymbol = '\xff';
        else
            _currentSymbol = _input[_readPosition];
        _position = _readPosition;
        _readPosition++;
        // if (_currentSymbol == ' ')
        //     ReadChar();
    }

    public Token NextToken()
    {
        Token token = new();
        SkipWhiteSpace();
        switch (_currentSymbol)
        {
            case '=':
                {
                    token = new(TokenType.ASSIGN, _currentSymbol.ToString());
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
            default:
                {
                    if (IsLetter(_currentSymbol))
                    {
                        string identifier = ReadIdentifier();
                        var type = Token.LookUpIdentifier(identifier);
                        return new(type, identifier); //early return nescessary, since ReadChar was already called by "ReadIdentifier"
                    }
                    else if (IsDigit(_currentSymbol))
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
        while (IsDigit(_currentSymbol))
        {
            ReadChar();
        }
        return _input[startPosition .. _position];
    }

    private bool IsDigit(char currentSymbol)
    {
        if (char.IsDigit(currentSymbol))
            return true;
        return false;
    }

    private void SkipWhiteSpace()
    {
        while (_currentSymbol == ' ' || _currentSymbol == '\n' || _currentSymbol == '\r' || _currentSymbol == '\t')
            ReadChar();
    }

    private string ReadIdentifier()
    {
        var startPosition = _position; 
        while (IsLetter(_currentSymbol))
        {
            ReadChar();
        }
        return _input[startPosition .. _position];
    }

    private bool IsLetter(char someChar)
    {
        if (char.IsLetter(someChar))
            return true;
        if (someChar == '_')
            return true;
        return false;
    }
}