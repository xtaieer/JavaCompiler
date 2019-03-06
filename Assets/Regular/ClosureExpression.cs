public class ClosureExpression : UnaryExpression
{
    public ClosureExpression(IRegularExpression operand) : base(operand) { }

    public override void Accept(IRegularExpressionVisitor visitor)
    {
        visitor.Closure(_operand);
    }

    public override string ToString()
    {
        return "[" + _operand.ToString() + "]";
    }
}
