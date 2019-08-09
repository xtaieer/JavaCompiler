namespace Xtaieer.Regular
{
    public class OrExpression : BinaryExpression
    {
        public OrExpression(IRegularExpression lhs, IRegularExpression rhs) : base(lhs, rhs) { }

        public override void Accept(IRegularExpressionVisitor visitor)
        {
            visitor.Or(lhs, rhs);
        }

        public override string ToString()
        {
            return lhs.ToString() + "|" + rhs.ToString();
        }
    }
}
