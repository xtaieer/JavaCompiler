using System;
using System.IO;
using System.Collections.Generic;
using Xtaieer.Lex;
using UnityEngine;

namespace Xtaieer.Grammar
{
    public class GrammarAnalyzer
    {
        /*
         * S -> Nonterminal:Right'
         * Right' -> Right;Right' | Right
         * Right -> Minal Right | Minal
         * Minal -> Terminal | Nonterminal
         */
        private class Token
        {
            public static readonly Token EMPTY = new Token(""); 

            public string Name
            {
                get;
                private set;
            }

            public Token(string name)
            {
                Name = name;
            }

            public override bool Equals(object obj)
            {
                if(obj is Token)
                {
                    Token t = obj as Token;
                    if (t.Name != null)
                    {
                        return Name.Equals(t.Name);
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

        private List<string> terminals = new List<string>();
        private LexicalAnalyzer<Token> lex;
        
        public GrammarAnalyzer()
        {
            lex = new LexicalAnalyzer<Token>(
                new LexicalAnalyzer<Token>.RegularAndAction[] {
                    new LexicalAnalyzer<Token>.RegularAndAction("\t| ", (lex, line, column) => { return Token.EMPTY;  }),
                    new LexicalAnalyzer<Token>.RegularAndAction("\".\"", (lex, line, column) => { return new Token(new string(lex[1], 1)); }),
                    new LexicalAnalyzer<Token>.RegularAndAction("([A-Z]|[a-z]|[0-9])+", (lex, line, column) => {return new Token(lex); }),
                    new LexicalAnalyzer<Token>.RegularAndAction(":|;|%|", (lex, line, column) => {return new Token(lex); })
                }
            );
        }

        public void AddTerminal(string terminal)
        {
            for(int i = 0; i < terminal.Length; i ++)
            {
                if(!char.IsLetterOrDigit(terminal[i]))
                {
                    throw new ArgumentException("终结符只能使用字母和数字");
                }
            }
            terminals.Add(terminal);
        }

        public void AddProduction(string production)
        {
            lex.SetInput(new StringReader(production));
            Token token = lex.NextToken();
            while(token != null) {
                token = lex.NextToken();
            }
        }


    }
}
