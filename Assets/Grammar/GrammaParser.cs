using System;
using System.Collections.Generic;
using Xtaieer.Lex;

namespace Xtaieer.Grammar
{
    public class GrammaParser
    {
        /*
         * S -> Nonterminal:Right'
         * Right' -> Right;Right' | Right
         * Right -> Minal Right | Minal
         * Minal -> Terminal | Nonterminal
         */

        private enum TokenType
        {
            EMPTY,
            SYMBOL,
            MINAL,
            SINGLE_CHAR_MINAL
        }
        private class Token
        {
            public static readonly Token EMPTY = new Token("", TokenType.EMPTY);

            public string Name
            {
                get;
                private set;
            }

            public TokenType Type
            {
                get;
                private set;
            }

            public Token(string name, TokenType type)
            {
                Type = type;
                Name = name;
            }

            public override bool Equals(object obj)
            {
                if (obj is Token)
                {
                    Token t = obj as Token;
                    if (t.Name != null)
                    {
                        return Name.Equals(t.Name) && Type == t.Type;
                    }
                }
                return false;
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private Dictionary<string, Terminal> terminals = new Dictionary<string, Terminal>();
        private Dictionary<string, Nonterminal> nonterminals = new Dictionary<string, Nonterminal>();
        private LexicalAnalyzer<Token> lex;
        private Token current;

        public GrammaParser() {
            lex = new LexicalAnalyzer<Token>(
                    new LexicalAnalyzer<Token>.RegularAndAction[] {
                    new LexicalAnalyzer<Token>.RegularAndAction("\t| ", (lex, line, column) => { return Token.EMPTY;  }),
                    new LexicalAnalyzer<Token>.RegularAndAction("\".\"", (lex, line, column) => { return new Token(new string(lex[1], 1), TokenType.SINGLE_CHAR_MINAL); }),
                    new LexicalAnalyzer<Token>.RegularAndAction("([A-Z]|[a-z]|[0-9])+", (lex, line, column) => {return new Token(lex, TokenType.MINAL); }),
                    new LexicalAnalyzer<Token>.RegularAndAction(":|;|%|", (lex, line, column) => {return new Token(lex, TokenType.SYMBOL); })
                }
            );
        }

        public void AddTerminal(string terminal)
        {
            for (int i = 0; i < terminal.Length; i++)
            {
                if (!char.IsLetterOrDigit(terminal[i]))
                {
                    throw new ArgumentException("终结符只能使用字母和数字");
                }
            }
            terminals.Add(terminal, new Terminal(terminal));
        }



        private void RightT()
        {
            Right();
        }

        private LinkedList<Minal> Right(LinkedList<Minal> left)
        {
            Minal minal = Minal();
            left.AddBefore(left.First, minal);
            if(current.Type == TokenType.MINAL || current.Type == TokenType.SINGLE_CHAR_MINAL)
            {
                return Right(left);
            }
            return left;
        }

        private Minal Minal()
        {
            Token token = current;
            Match(TokenType.MINAL);
            if (terminals.ContainsKey(token.Name))
            {
                Minal minal = terminals[token.Name];
                return minal;
            }
            else
            {
                if(nonterminals.ContainsKey(token.Name))
                {
                    return nonterminals[token.Name];
                }
                else
                {
                    Nonterminal nonterminal = new Nonterminal(token.Name);
                    nonterminals.Add(token.Name, nonterminal);
                    return nonterminal;
                }
            }
        }

        private void Terminal()
        {

        }

        private void Nonterminal()
        {

        }

        private void Match(TokenType type)
        {
            current = lex.NextToken();
        }
    }
}
