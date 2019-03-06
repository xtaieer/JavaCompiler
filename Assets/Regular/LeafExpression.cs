using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafExpression : IRegularExpression
{
    private char[] _chars;

    public LeafExpression(params char[] chars)
    {
        _chars = chars;
    }

    public LeafExpression(string chars) : this(chars.ToCharArray()) {}

    public void Accept(IRegularExpressionVisitor visitor)
    {
        visitor.Leaf(_chars);
    }
}
