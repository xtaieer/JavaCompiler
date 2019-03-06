using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegularExpressionVisitor
{
    void Leaf(params char[] chars);
    void Add(IRegularExpression lhs, IRegularExpression rhs);
    void Or(IRegularExpression lhs, IRegularExpression rhs);
    void Closure(IRegularExpression operand);
}
