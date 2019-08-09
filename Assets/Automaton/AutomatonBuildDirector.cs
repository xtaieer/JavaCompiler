using System.Collections.Generic;

public class AutomatonBuildDirector
{
    private enum RegularOperator
    {
        NONE = 0,
        AND,
        OR,
        ASTERISK,
        QUESTION_MARK,
        PLUS_SIGN,
        BRACKET
    }

    private class Operand
    {
        public Operand(Node start, Node end)
        {
            Start = start;
            End = end;
        }

        public Node Start
        {
            get;
            private set;
        }

        public Node End
        {
            get;
            private set;
        }
    }

    private static readonly Edge NUMBER = new RangeCharEdge('0', '9');
    private static readonly Edge LETTER = new RangeCharEdge('A', 'z');
    private static readonly Edge UPPER_LETTER = new RangeCharEdge('A', 'Z');
    private static readonly Edge LOWER_LETTER = new RangeCharEdge('a', 'z');
    private static readonly Edge RANDOM = new RandomCharEdge();

    private static readonly Dictionary<RegularOperator, int> PRIORITY_MAP = new Dictionary<RegularOperator, int>();
    private static readonly Dictionary<char, RegularOperator> OPERATOR_MAP = new Dictionary<char, RegularOperator>();

    static AutomatonBuildDirector()
    {
        PRIORITY_MAP.Add(RegularOperator.OR, 0);
        PRIORITY_MAP.Add(RegularOperator.AND, 1);
        PRIORITY_MAP.Add(RegularOperator.ASTERISK, 2);
        PRIORITY_MAP.Add(RegularOperator.PLUS_SIGN, 2);
        PRIORITY_MAP.Add(RegularOperator.QUESTION_MARK, 2);
        PRIORITY_MAP.Add(RegularOperator.BRACKET, 3);

        OPERATOR_MAP.Add('|', RegularOperator.OR);
        OPERATOR_MAP.Add('*', RegularOperator.ASTERISK);
        OPERATOR_MAP.Add('+', RegularOperator.PLUS_SIGN);
        OPERATOR_MAP.Add('?', RegularOperator.QUESTION_MARK);
    }

    public IAutomaton Build(string regular)
    {
        IAutomatonBuilder builder = new NFABuilder();

        Stack<Operand> operands = new Stack<Operand>();
        Stack<RegularOperator> regularOperators = new Stack<RegularOperator>();
        int currentIndex = 0;
        int bracketCount = 0;
        bool appendAndOp = false;
        while (currentIndex < regular.Length)
        {
            char c = regular[currentIndex];
            switch (c)
            {
                case '|':
                    {
                        RegularOperator rop = OPERATOR_MAP[c];
                        while (regularOperators.Count > 0 && regularOperators.Peek() != RegularOperator.BRACKET && PRIORITY_MAP[regularOperators.Peek()] >= PRIORITY_MAP[rop])
                        {
                            Calculate(builder, operands, regularOperators.Pop());
                        }
                        regularOperators.Push(rop);
                        appendAndOp = false;
                    }
                    break;
                case '*':
                case '+':
                case '?':
                    {
                        Calculate(builder, operands, OPERATOR_MAP[c]);
                    }
                    break;
                case '(':
                    if (appendAndOp)
                    {
                        while (regularOperators.Count > 0
                        && regularOperators.Peek() != RegularOperator.BRACKET
                        && PRIORITY_MAP[regularOperators.Peek()] >= PRIORITY_MAP[RegularOperator.AND])
                        {
                            Calculate(builder, operands, regularOperators.Pop());
                        }
                        regularOperators.Push(RegularOperator.AND);
                    }
                    regularOperators.Push(RegularOperator.BRACKET);
                    appendAndOp = false;
                    bracketCount++;
                    break;
                case ')':
                    if (bracketCount > 0)
                    {
                        bracketCount--;
                        RegularOperator regularOperator = regularOperators.Pop();
                        while (regularOperator != RegularOperator.BRACKET)
                        {
                            Calculate(builder, operands, regularOperator);
                            regularOperator = regularOperators.Pop();
                        };
                    }
                    break;
                case '[':
                    break;
                case '.':
                    {
                        if (appendAndOp)
                        {
                            while (regularOperators.Count > 0
                            && regularOperators.Peek() != RegularOperator.BRACKET
                            && PRIORITY_MAP[regularOperators.Peek()] >= PRIORITY_MAP[RegularOperator.AND])
                            {
                                Calculate(builder, operands, regularOperators.Pop());
                            }
                            regularOperators.Push(RegularOperator.AND);
                        }
                        Operand operand = new Operand(builder.GenerateNode(), builder.GenerateNode());
                        builder.AddTransition(operand.Start, operand.End, RANDOM);
                        operands.Push(operand);
                        appendAndOp = true;
                    }
                    break;
                default:
                    {
                        if (appendAndOp)
                        {
                            while (regularOperators.Count > 0
                            && regularOperators.Peek() != RegularOperator.BRACKET
                            && PRIORITY_MAP[regularOperators.Peek()] >= PRIORITY_MAP[RegularOperator.AND])
                            {
                                Calculate(builder, operands, regularOperators.Pop());
                            }
                            regularOperators.Push(RegularOperator.AND);
                        }
                        Operand operand = new Operand(builder.GenerateNode(), builder.GenerateNode());
                        builder.AddTransition(operand.Start, operand.End, new SingleCharEdge(c));
                        operands.Push(operand);
                        appendAndOp = true;
                    }
                    break;
            }
            currentIndex++;
        }
        while (regularOperators.Count > 0)
        {
            Calculate(builder, operands, regularOperators.Pop());
        }
        builder.SetStart(operands.Peek().Start);
        builder.Accept(operands.Peek().End);
        return builder.GetResult();
    }

    private void Calculate(IAutomatonBuilder builder ,Stack<Operand> operands, RegularOperator op)
    {
        switch (op)
        {
            case RegularOperator.AND:
                if (operands.Count >= 2)
                {
                    Operand rh = operands.Pop();
                    Operand lh = operands.Pop();
                    builder.Merge(lh.End, rh.Start);
                    operands.Push(new Operand(lh.Start, rh.End));

                }
                break;
            case RegularOperator.OR:
                if (operands.Count >= 2)
                {
                    Operand rh = operands.Pop();
                    Operand lh = operands.Pop();
                    Operand operand = new Operand(builder.GenerateNode(), builder.GenerateNode());
                    builder.AddTransition(operand.Start, lh.Start, null);
                    builder.AddTransition(operand.Start, rh.Start, null);

                    builder.AddTransition(rh.End, operand.End, null);
                    builder.AddTransition(lh.End, operand.End, null);
                    operands.Push(operand);
                }
                break;
            case RegularOperator.ASTERISK:
                if (operands.Count >= 1)
                {
                    Operand operand = operands.Pop();
                    builder.AddTransition(operand.Start, operand.End, null);
                    builder.AddTransition(operand.End, operand.Start, null);
                    operands.Push(operand);
                }
                break;
            case RegularOperator.PLUS_SIGN:
                break;
            case RegularOperator.QUESTION_MARK:
                break;
        }
    }
}
