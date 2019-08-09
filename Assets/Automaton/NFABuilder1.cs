using Regular;

public class NFABuilder1
{
    //private class ReguarExpressionVisitor : IRegularExpressionVisitor
    //{
    //    private NFA nfa;

    //    public NFA.Node start
    //    {
    //        get;
    //        private set;
    //    }

    //    public NFA.Node end
    //    {
    //        get;
    //        private set;
    //    }

    //    public ReguarExpressionVisitor(NFA nfa)
    //    {
    //        this.nfa = nfa;
    //        start = this.nfa.generateNode();
    //        end = this.nfa.generateNode();
    //    }

    //    void IRegularExpressionVisitor.And(IRegularExpression lhs, IRegularExpression rhs)
    //    {
    //        lhs.Accept(this);
    //        NFA.Node node = nfa.generateNode();
    //        start = end;
    //        end = node;
    //        rhs.Accept(this);
    //    }

    //    void IRegularExpressionVisitor.Or(IRegularExpression lhs, IRegularExpression rhs)
    //    {
    //        lhs.Accept(this);
    //        NFA.Node leftStart = start;
    //        NFA.Node leftEnd = end;

    //        start = nfa.generateNode();
    //        end = nfa.generateNode();

    //        rhs.Accept(this);

    //        NFA.Node rightStart = start;
    //        NFA.Node rightEnd = end;

    //        start = nfa.generateNode();
    //        end = nfa.generateNode();

    //        nfa.AddEmptyTransition(start, leftStart);
    //        nfa.AddEmptyTransition(start, rightStart);
    //        nfa.AddEmptyTransition(leftEnd, end);
    //        nfa.AddEmptyTransition(rightEnd, end);
    //    }

    //    void IRegularExpressionVisitor.Closure(IRegularExpression operand)
    //    {
    //        operand.Accept(this);

    //        NFA.Node start = nfa.generateNode();
    //        NFA.Node end = nfa.generateNode();
    //        nfa.AddEmptyTransition(start, start);
    //        nfa.AddEmptyTransition(end, end);
    //        nfa.AddEmptyTransition(end, start);

    //        nfa.AddEmptyTransition(start, end);
    //        this.start = start;
    //        this.end = end;
    //    }

    //    void IRegularExpressionVisitor.Leaf(params char[] chars)
    //    {
    //        NFA.Node from = start;
    //        for(int i = 0; i < chars.Length; i ++)
    //        {
    //            NFA.Node n = nfa.generateNode();
    //            nfa.AddTransition(from, n, chars[i]);
    //            from = n;
    //        }
    //        nfa.AddEmptyTransition(from, end);
    //    }
    //}

    //public static NFA Parse(IRegularExpression regularExpression)
    //{
    //    NFA nfa = new NFA();
    //    ReguarExpressionVisitor visitor = new ReguarExpressionVisitor(nfa);
    //    regularExpression.Accept(visitor);
    //    nfa.start = visitor.start;
    //    nfa.AddAcceptNode(visitor.end);
    //    return nfa;
    //}
}
