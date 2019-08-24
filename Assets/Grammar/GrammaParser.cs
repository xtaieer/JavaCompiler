using System;
using System.Collections.Generic;
using System.IO;
using Xtaieer.Lex;

namespace Xtaieer.Grammar
{
    public class GrammaParser
    {
        /*
         * S -> Nonterminal:Right'
         * Right' -> Right;Right' | Right | Empty
         * Right -> Minal Right | Minal
         * Minal -> Terminal | Nonterminal
         */

        private enum TokenType
        {
            SPACE,
            EMPTY,
            SYMBOL,
            MINAL,
            SINGLE_CHAR_MINAL
        }

        private class Token
        {
            public static readonly Token SPACE = new Token("", TokenType.SPACE);

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
                    new LexicalAnalyzer<Token>.RegularAndAction("\t| ", (lex, line, column) => { return Token.SPACE;  }),
                    new LexicalAnalyzer<Token>.RegularAndAction("\".\"", (lex, line, column) => { return new Token(new string(lex[1], 1), TokenType.SINGLE_CHAR_MINAL); }),
                    new LexicalAnalyzer<Token>.RegularAndAction("([A-Z]|[a-z]|[0-9])+", (lex, line, column) => {return new Token(lex, TokenType.MINAL); }),
                    new LexicalAnalyzer<Token>.RegularAndAction(":|;|%|", (lex, line, column) => {return new Token(lex, TokenType.SYMBOL); }),
                    new LexicalAnalyzer<Token>.RegularAndAction("ε", (lex, line, column) => { return new Token(lex, TokenType.EMPTY);  })
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

        public void AddProduction(string production)
        {
            lex.SetInput(new StringReader(production));
            current = lex.NextToken();
            Nonterminal nonterminal = Nonterminal();
            Match(TokenType.SYMBOL);
            List<Minal[]> productions = RightT(null);
            foreach(Minal[] pro in productions)
            {
                nonterminal.AddProduction(pro);
            }
        }

        public Nonterminal[] GetResult()
        {
            List<Nonterminal> result = new List<Nonterminal>(nonterminals.Count);
            foreach(Nonterminal nonterminal in nonterminals.Values)
            {
                result.Add(nonterminal);
            }
            return result.ToArray();
        }

        private List<Minal[]> RightT(List<Minal[]> productions)
        {
            List<Minal[]> result = productions;
            if(result == null)
            {
                result = new List<Minal[]>();
            }
            if(current.Type == TokenType.EMPTY)
            {
                Minal[] minals = new Minal[] { Grammar.Minal.EMPTY };
                result.Add(minals);
            }
            else
            {
                LinkedList<Minal> minalLinkedList = new LinkedList<Minal>();
                minalLinkedList = Right(minalLinkedList);
                Minal[] minals = new Minal[minalLinkedList.Count];
                LinkedListNode<Minal> head = minalLinkedList.First;
                int index = 0;
                while (head != null)
                {
                    minals[index] = head.Value;
                    index++;
                    head = head.Next;
                }
                result.Add(minals);
                if (current != null && current.Type == TokenType.SYMBOL && current.Name == ";")
                {
                    Match(TokenType.SYMBOL);
                    return RightT(result);
                }
            }
            return result;
        }

        private LinkedList<Minal> Right(LinkedList<Minal> left)
        {
            Minal minal = Minal();
            left.AddFirst(minal);
            if(current != null && (current.Type == TokenType.MINAL || current.Type == TokenType.SINGLE_CHAR_MINAL))
            {
                return Right(left);
            }
            return left;
        }

        private Minal Minal()
        {
            if (terminals.ContainsKey(current.Name) || current.Type == TokenType.SINGLE_CHAR_MINAL)
            {
                return Terminal();
            }
            else
            {
                return Nonterminal();
            }
        }

        private Terminal Terminal()
        {
            Token token = current;
            if (current.Type == TokenType.SINGLE_CHAR_MINAL)
            {
                Match(TokenType.SINGLE_CHAR_MINAL);
                if (!terminals.ContainsKey(token.Name))
                {
                    Terminal minal = new Terminal(token.Name);
                    terminals.Add(token.Name, minal);
                }
            }
            else
            {
                Match(TokenType.MINAL);
            }
            return terminals[token.Name];
        }

        private Nonterminal Nonterminal()
        {
            Token token = current;
            Match(TokenType.MINAL);
            if (nonterminals.ContainsKey(token.Name))
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

        private void Match(TokenType type)
        {
            current = lex.NextToken();
            while (current != null && current.Type == TokenType.SPACE)
            {
                current = lex.NextToken();
            }
        }
    }
}
