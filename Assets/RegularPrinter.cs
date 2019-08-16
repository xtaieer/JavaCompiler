using UnityEngine;
using Xtaieer.Regular;
using System.Text;

public class RegularPrinter : IRegularExpressionVisitor
{
    StringBuilder sb = new StringBuilder();

    void IRegularExpressionVisitor.Closure(IRegularExpression operand)
    {
        bool isLeaf = operand is LeafExpression;
        if(!isLeaf)
        {
            Print('(');
            operand.Accept(this);
            Print(')');
        }
        else
        {
            operand.Accept(this);
        }
        Print('*');
    }

    void IRegularExpressionVisitor.Concat(IRegularExpression lhs, IRegularExpression rhs)
    {
        bool isLeftOr = lhs is OrExpression;
        bool isRightOr = rhs is OrExpression;
        if(isLeftOr)
        {
            Print('(');
            lhs.Accept(this);
            Print(')');
        }
        else
        {
            lhs.Accept(this);
        }

        if(isRightOr)
        {
            Print('(');
            rhs.Accept(this);
            Print(')');
        }
        else
        {
            rhs.Accept(this);
        }
    }

    void IRegularExpressionVisitor.Epsilon()
    {
    }

    void IRegularExpressionVisitor.Or(IRegularExpression lhs, IRegularExpression rhs)
    {
        lhs.Accept(this);
        Print('|');
        rhs.Accept(this);
    }

    void IRegularExpressionVisitor.RangeCharLeaf(char from, char to)
    {
        Print("[" + from + "-" + to + "]");
    }

    void IRegularExpressionVisitor.SingleCharLeaf(char c)
    {
        Print(c);
    }

    void IRegularExpressionVisitor.QuestionMark(IRegularExpression operand)
    {
        bool isLeaf = operand is LeafExpression;
        if (!isLeaf)
        {
            Print('(');
            operand.Accept(this);
            Print(')');
        }
        else
        {
            operand.Accept(this);
        }
        Print('?');
    }

    void IRegularExpressionVisitor.PositiveClosure(IRegularExpression operand)
    {
        bool isLeaf = operand is LeafExpression;
        if (!isLeaf)
        {
            Print('(');
            operand.Accept(this);
            Print(')');
        }
        else
        {
            operand.Accept(this);
        }
        Print('+');
    }

    void Print(char c)
    {
        sb.Append(c);
    }

    void Print(string str)
    {
        sb.Append(str);
    }

    public void Result()
    {
        Debug.Log(sb.ToString());
        sb.Clear();
    }
}
