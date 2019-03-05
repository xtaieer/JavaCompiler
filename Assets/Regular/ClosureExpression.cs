public class ClosureExpression : UnaryExpression
{
    public ClosureExpression(IRegularExpression operand) : base(operand) { }

    public override string ToString()
    {
        return "[" + _operand.ToString() + "]";
    }
}
