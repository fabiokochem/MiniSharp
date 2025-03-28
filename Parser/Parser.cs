using ToyLang.Lexer;
using static ToyLang.Parser.Expr;
using static ToyLang.Parser.Stmt;

namespace ToyLang.Parser
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _current = 0;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public List<Stmt> Parse()
        {
            var statements = new List<Stmt>();
            while (!IsAtEnd())
            {
                statements.Add(Declaration());
            }
            return statements;
        }

        private Stmt Declaration()
        {
            if (Match(TokenType.Let)) return VarDeclaration();
            return Statement();
        }

        private Stmt VarDeclaration()
        {
            Token name = Consume(TokenType.Identifier, "Expected variable name.");
            Consume(TokenType.Equal, "Expected '=' after variable name.");
            Expr initializer = Expression();
            Consume(TokenType.Semicolon, "Expected ';' after variable declaration.");
            return new Var(name.Lexeme, initializer);
        }

        private Stmt Statement()
        {
            if (Match(TokenType.Print)) return PrintStatement();
            return ExpressionStatement();
        }

        private Stmt PrintStatement()
        {
            Expr value = Expression();
            Consume(TokenType.Semicolon, "Expected ';' after value.");
            return new Print(value);
        }

        private Stmt ExpressionStatement()
        {
            Expr expr = Expression();
            Consume(TokenType.Semicolon, "Expected ';' after expression.");
            return new ExprStmt(expr);
        }

        private Expr Expression()
        {
            return Equality();
        }

        private Expr Equality()
        {
            Expr expr = Term();

            while (Match(TokenType.EqualEqual, TokenType.BangEqual))
            {
                Token op = Previous();
                Expr right = Term();
                expr = new Binary(expr, op.Lexeme, right);
            }

            return expr;
        }

        private Expr Term()
        {
            Expr expr = Factor();

            while (Match(TokenType.Plus, TokenType.Minus))
            {
                Token op = Previous();
                Expr right = Factor();
                expr = new Binary(expr, op.Lexeme, right);
            }

            return expr;
        }

        private Expr Factor()
        {
            Expr expr = Primary();

            while (Match(TokenType.Star, TokenType.Slash))
            {
                Token op = Previous();
                Expr right = Primary();
                expr = new Binary(expr, op.Lexeme, right);
            }

            return expr;
        }

        private Expr Primary()
        {
            if (Match(TokenType.Number))
            {
                return new Literal(Previous().Literal!);
            }

            if (Match(TokenType.Identifier))
            {
                return new Variable(Previous().Lexeme);
            }

            if (Match(TokenType.LeftParen))
            {
                Expr expr = Expression();
                Consume(TokenType.RightParen, "Expected ')' after expression.");
                return expr;
            }

            throw Error(Peek(), "Expected expression.");
        }

        // Helpers

        private bool Match(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (Check(type))
                {
                    Advance();
                    return true;
                }
            }
            return false;
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd()) return false;
            return Peek().Type == type;
        }

        private Token Advance()
        {
            if (!IsAtEnd()) _current++;
            return Previous();
        }

        private bool IsAtEnd() => Peek().Type == TokenType.EOF;

        private Token Peek() => _tokens[_current];

        private Token Previous() => _tokens[_current - 1];

        private Token Consume(TokenType type, string message)
        {
            if (Check(type)) return Advance();
            throw Error(Peek(), message);
        }

        private Exception Error(Token token, string message)
        {
            return new Exception($"[Line {token.Line}] Error at '{token.Lexeme}': {message}");
        }
    }
}
