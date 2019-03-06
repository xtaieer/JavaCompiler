public abstract class UnaryExpression : IRegularExpression
{
    protected IRegularExpression _operand;
    
    public UnaryExpression(IRegularExpression operand)
    {
        _operand = operand;
    }

    public abstract void Accept(IRegularExpressionVisitor visitor);
}
