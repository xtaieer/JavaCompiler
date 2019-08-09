namespace Xtaieer.Regular
{
    public class ClosureExpression : UnaryExpression
    {
        public ClosureExpression(IRegularExpression operand) : base(operand) { }

        public override void Accept(IRegularExpressionVisitor visitor)
        {
            visitor.Closure(operand);
        }

        public override string ToString()
        {
            return "[" + operand.ToString() + "]";
        }
    }
}
