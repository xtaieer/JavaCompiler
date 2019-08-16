using System;
using System.Collections.Generic;
using System.IO;
using Xtaieer.Regular;
using Xtaieer.Automaton.Nfa;

namespace Xtaieer.Lex
{
    public class LexicalAnalyzer<Token>
    {
        public class RegularAndAction
        {
            internal string Regular
            {
                get;
                private set;
            }

            internal Func<string,int, int, Token> Action
            {
                get;
                private set;
            }

            public RegularAndAction(string regular, Func<string, int, int, Token> action)
            {
                Regular = regular;
                Action = action;
            }
        }

        private class AcceptNodeInfo
        {
            internal int Priority
            {
                get;
                private set;
            }

            internal Func<string, int, int, Token> Action
            {
                get;
                private set;
            }

            public AcceptNodeInfo(int priority, Func<string, int, int, Token> action)
            {
                Priority = priority;
                Action = action;
            }
        }

        private const int BUFFER_SIZE = 1024;

        private NFA nfa;
        private TextReader input;
        private Dictionary<Node, AcceptNodeInfo> acceptNodeInfos;

        private char[] buffer = new char[BUFFER_SIZE];
        private int startIndex;
        private int forwardIndex;
        private int length = 0;

        public LexicalAnalyzer(RegularAndAction[] regularsAndActions)
        {
            acceptNodeInfos = new Dictionary<Node, AcceptNodeInfo>(regularsAndActions.Length);

            NFABuilder nfaBuilder = new NFABuilder();
            List<Node> startNodes = new List<Node>(regularsAndActions.Length);
            NFABuilderVisitor visitor = new NFABuilderVisitor(nfaBuilder);

            for(int i = 0; i < regularsAndActions.Length; i ++)
            {
                visitor.Reset();
                IRegularExpression exp = RegularParser.Parse(regularsAndActions[i].Regular);
                exp.Accept(visitor);
                NFABuilderVisitor.Operand operand = visitor.GetResult();
                startNodes.Add(operand.Start);
                acceptNodeInfos.Add(operand.End, new AcceptNodeInfo(i, regularsAndActions[i].Action));
                nfaBuilder.AddAccept(operand.End);
            }

            Node start = nfaBuilder.GenerateNode();
            foreach(Node s in startNodes)
            {
                nfaBuilder.AddTransition(start, s, null);
            }
            nfaBuilder.SetStart(start);
            nfa = nfaBuilder.GetResult();
        }

        public bool Accept(string str)
        {
            return nfa.IsAccept(str);
        } 

        public void SetInput(TextReader input)
        {
            this.input = input;
            startIndex = 0;
            forwardIndex = 0;
            length = input.Read(buffer, 0, BUFFER_SIZE);
        }

        public Token NextToken()
        {
            forwardIndex = startIndex;

            HashSet<Node> ready = new HashSet<Node>();
            Stack<List<Node>> states = new Stack<List<Node>>();

            List<Node> currentStates = new List<Node>();
            foreach(Node n in nfa.Start.EmptyClosureIterator())
            {
                if(!ready.Contains(n))
                {
                    ready.Add(n);
                    currentStates.Add(n);
                }
            }
            states.Push(currentStates);

            while(forwardIndex < length)
            {
                currentStates = new List<Node>();
                ready.Clear();

                foreach(Node n in states.Peek())
                {
                    foreach(Node node in n.ClosureIterator(buffer[forwardIndex]))
                    {
                        foreach(Node en in node.EmptyClosureIterator())
                        {
                            if(!ready.Contains(en))
                            {
                                currentStates.Add(en);
                                ready.Add(en);
                            }
                        }
                    }
                }
                if (currentStates.Count != 0)
                {
                    states.Push(currentStates);
                    forwardIndex++;
                }
                else
                {
                    break;
                }
            }

            while(states.Count != 0)
            {
                forwardIndex--;
                List<Node> state = states.Pop();
                Node acceptNode = null;
                foreach(Node n in state)
                {
                    if(nfa.Accept(n))
                    {
                        if(acceptNode != null)
                        {
                            if(acceptNodeInfos[n].Priority < acceptNodeInfos[acceptNode].Priority)
                            {
                                acceptNode = n;
                            }
                        }
                        else
                        {
                            acceptNode = n;
                        }
                    }
                }
                if(acceptNode != null)
                {
                    Token token = acceptNodeInfos[acceptNode].Action(new string(buffer, startIndex, forwardIndex - startIndex + 1), 0, 0);
                    startIndex = forwardIndex + 1;
                    return token;
                }
            }
            return default;
        }
    }
}
