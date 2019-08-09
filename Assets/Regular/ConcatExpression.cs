namespace Xtaieer.Regular
{
    public class ConcatExpression : BinaryExpression
    {
        public ConcatExpression(IRegularExpression lhs, IRegularExpression rhs) : base(lhs, rhs) { }

        public override void Accept(IRegularExpressionVisitor visitor)
        {
            visitor.Concat(lhs, rhs);
        }

        public override string ToString()
        {
            return lhs.ToString() + rhs.ToString();
        }
    }
}
