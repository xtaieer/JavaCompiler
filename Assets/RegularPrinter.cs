using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Regular;
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

    void IRegularExpressionVisitor.Leaf(params char[] chars)
    {
        if(chars.Length > 1)
        {
            Print('[');
            for(int i = 0; i < chars.Length; i ++)
            {
                Print(chars[i]);
            }
            Print(']');
        }
        else if(chars.Length == 1)
        {
            Print(chars[0]);
        }
    }

    void IRegularExpressionVisitor.Or(IRegularExpression lhs, IRegularExpression rhs)
    {
        lhs.Accept(this);
        Print('|');
        rhs.Accept(this);
    }

    void Print(char c)
    {
        sb.Append(c);
    }

    public void Result()
    {
        Debug.Log(sb.ToString());
        sb.Clear();
    }
}
