#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLang.Lexer
{
    public class Lexer
    {
        private readonly string _source;
        private readonly List<Token> _tokens = new();
        private int _start = 0;
        private int _current = 0;
        private int _line = 1;

        private static readonly Dictionary<string, TokenType> _keywords = new()
        {
            { "let", TokenType.Let },
            { "if", TokenType.If },
            { "else", TokenType.Else },
            { "while", TokenType.While },
            { "print", TokenType.Print },
        };

        public Lexer(string source)
        {
            _source = source;
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                _start = _current;
                ScanToken();
            }

            _tokens.Add(new Token(TokenType.EOF, "", null, _line));
            return _tokens;
        }

        private void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(': AddToken(TokenType.LeftParen); break;
                case ')': AddToken(TokenType.RightParen); break;
                case '{': AddToken(TokenType.LeftBrace); break;
                case '}': AddToken(TokenType.RightBrace); break;
                case '+': AddToken(TokenType.Plus); break;
                case '-': AddToken(TokenType.Minus); break;
                case '*': AddToken(TokenType.Star); break;
                case '/': AddToken(TokenType.Slash); break;
                case ';': AddToken(TokenType.Semicolon); break;

                case '!':
                    AddToken(Match('=') ? TokenType.BangEqual : throw Error("Expected '=' after '!'"));
                    break;
                case '=':
                    AddToken(Match('=') ? TokenType.EqualEqual : TokenType.Equal);
                    break;
                case '<':
                    AddToken(Match('=') ? TokenType.LessEqual : TokenType.Less);
                    break;
                case '>':
                    AddToken(Match('=') ? TokenType.GreaterEqual : TokenType.Greater);
                    break;

                case ' ':
                case '\r':
                case '\t':
                    break;
                case '\n':
                    _line++;
                    break;

                default:
                    if (char.IsDigit(c))
                    {
                        Number();
                    }
                    else if (char.IsLetter(c))
                    {
                        Identifier();
                    }
                    else
                    {
                        throw Error($"Unexpected character '{c}'");
                    }
                    break;
            }
        }

        private void Identifier()
        {
            while (char.IsLetterOrDigit(Peek())) Advance();

            string text = _source[_start.._current];
            TokenType type = _keywords.ContainsKey(text) ? _keywords[text] : TokenType.Identifier;
            AddToken(type);
        }

        private void Number()
        {
            while (char.IsDigit(Peek())) Advance();

            string number = _source[_start.._current];
            AddToken(TokenType.Number, int.Parse(number));
        }

        private bool Match(char expected)
        {
            if (IsAtEnd() || _source[_current] != expected) return false;
            _current++;
            return true;
        }

        private char Peek() => IsAtEnd() ? '\0' : _source[_current];

        private char Advance() => _source[_current++];

        private void AddToken(TokenType type, object? literal = null)
        {
            string text = _source[_start.._current];
            _tokens.Add(new Token(type, text, literal, _line));
        }

        private bool IsAtEnd() => _current >= _source.Length;

        private Exception Error(string message) => new Exception($"[Line {_line}] Error: {message}");
    }
}
