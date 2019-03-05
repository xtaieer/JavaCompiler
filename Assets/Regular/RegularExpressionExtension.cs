using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RegularExpressionExtension 
{
    public static IRegularExpression And(this IRegularExpression lhs, IRegularExpression rhs)
    {
        return new AndExpression(lhs, rhs);
    }

    public static IRegularExpression Or(this IRegularExpression lhs, IRegularExpression rhs)
    {
        return new OrExpression(lhs, rhs);
    }

    public static IRegularExpression Closure(this IRegularExpression operand)
    {
        return new ClosureExpression(operand);
    }
}
