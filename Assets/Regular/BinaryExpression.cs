namespace Xtaieer.Regular
{
    public abstract class BinaryExpression : IRegularExpression
    {
        protected IRegularExpression lhs;
        protected IRegularExpression rhs;

        public BinaryExpression(IRegularExpression lhs, IRegularExpression rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        public abstract void Accept(IRegularExpressionVisitor visitor);
    }
}
