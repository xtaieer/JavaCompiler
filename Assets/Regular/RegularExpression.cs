using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegularExpression
{
    void Accept(IRegularExpressionVisitor visitor);
}
