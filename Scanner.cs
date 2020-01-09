using System;
using System.Collections.Generic;

namespace CLox
{
    internal class Scanner
    {
        private string source;
        private List<Token> tokens = new List<Token>();
        private int start = 0;
        private int current = 0;
        private int line = 1;

        public Scanner(string source)
        {
            this.source = source;
        }

        internal List<Token> scanTokens()
        {
            while (!atEnd())
            {
                start = current;
                scanToken();
            }
            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        private void scanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '{': addToken(TokenType.LEFT_BRACE); break;
                case '}': addToken(TokenType.RIGHT_PAREN); break;
                case ',': addToken(TokenType.COMMA); break;
                case '.': addToken(TokenType.DOT); break;
                case '-': addToken(TokenType.MINUS); break;
                case '+': addToken(TokenType.PLUS); break;
                case ';': addToken(TokenType.SEMICOLON); break;
                case '*': addToken(TokenType.STAR); break;
                default:
                    Program.error(line, "Unexpected characer");
                    break;
            }
        }

        private void addToken(TokenType tokentype)
        {
            addToken(tokentype, null);
        }

        private void addToken(TokenType tokentype, object literal)
        {
            string text = source.Substring(start, current - start);
            tokens.Add(new Token(tokentype, text, literal, line));
        }

        private char advance()
        {
            current++;
            return source[current];
        }

        private bool atEnd()
        {
            return current >= source.Length;
        }
    }
}