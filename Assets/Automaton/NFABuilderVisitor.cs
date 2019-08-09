using System.Collections.Generic;
using Xtaieer.Regular;

public class NFABuilderVisitor : IRegularExpressionVisitor
{
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

    private IAutomatonBuilder builder;
    private Stack<Operand> operands = new Stack<Operand>();

    public NFABuilderVisitor(IAutomatonBuilder builder)
    {
        this.builder = builder;
    }

    void IRegularExpressionVisitor.Closure(IRegularExpression operand)
    {
        operand.Accept(this);
        Node start = builder.GenerateNode();
        Node end = builder.GenerateNode();
        Operand op = operands.Pop();
        builder.AddTransition(start, op.Start, null);
        builder.AddTransition(op.End, end, null);
        builder.AddTransition(start, end, null);
        builder.AddTransition(op.End, op.Start, null);
        operands.Push(new Operand(start, end));
    }

    void IRegularExpressionVisitor.Concat(IRegularExpression lhs, IRegularExpression rhs)
    {
        lhs.Accept(this);
        rhs.Accept(this);
        Operand r = operands.Pop();
        Operand l = operands.Pop();
        builder.Merge(l.End, r.Start);
        operands.Push(new Operand(l.Start, r.End));
    }

    void IRegularExpressionVisitor.Epsilon()
    {
    }

    void IRegularExpressionVisitor.Or(IRegularExpression lhs, IRegularExpression rhs)
    {
        lhs.Accept(this);
        rhs.Accept(this);
        Operand r = operands.Pop();
        Operand l = operands.Pop();
        Node start = builder.GenerateNode();
        Node end = builder.GenerateNode();
        builder.AddTransition(start, r.Start, null);
        builder.AddTransition(start, l.Start, null);
        builder.AddTransition(r.End, end, null);
        builder.AddTransition(l.End, end, null);
        operands.Push(new Operand(start, end));
    }

    void IRegularExpressionVisitor.RangeCharLeaf(char from, char to)
    {
        Node start = builder.GenerateNode();
        Node end = builder.GenerateNode();
        builder.AddTransition(start, end, new RangeCharEdge(from, to));
        operands.Push(new Operand(start, end));
    }

    void IRegularExpressionVisitor.SingleCharLeaf(char c)
    {
        Node start = builder.GenerateNode();
        Node end = builder.GenerateNode();
        builder.AddTransition(start, end, new SingleCharEdge(c));
        operands.Push(new Operand(start, end));
    }

    void IRegularExpressionVisitor.QuestionMark(IRegularExpression operand)
    {
        Node start = builder.GenerateNode();
        Node end = builder.GenerateNode();
        Operand op = operands.Pop();
        builder.AddTransition(start, end, null);
        builder.AddTransition(start, op.Start, null);
        builder.AddTransition(op.End, end, null);
        operands.Push(new Operand(start, end));
    }
}
