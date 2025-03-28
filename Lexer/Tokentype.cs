namespace ToyLang.Lexer
{
    public enum TokenType
    {
        // Palavras-chave
        Let,
        If,
        Else,
        While,
        Print,

        // Identificadores e literais
        Identifier,
        Number,

        // Operadores
        Plus,
        Minus,
        Star,
        Slash,
        Equal,
        EqualEqual,
        BangEqual,
        Less,
        LessEqual,
        Greater,
        GreaterEqual,

        // Símbolos
        LeftParen,
        RightParen,
        LeftBrace,
        RightBrace,
        Semicolon,

        // Outros
        EOF,
    }
}
