using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndExpression : BinaryExpression
{
    public AndExpression(IRegularExpression lhs, IRegularExpression rhs) : base(lhs, rhs) {}

    public override void Accept(IRegularExpressionVisitor visitor)
    {
        visitor.Add(_lhs, _rhs);
    }

    public override string ToString()
    {
        return _lhs.ToString() + _rhs.ToString();
    }
}
