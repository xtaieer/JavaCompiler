namespace Xtaieer.Regular
{
    public abstract class UnaryExpression : IRegularExpression
    {
        protected IRegularExpression operand;

        public UnaryExpression(IRegularExpression operand)
        {
            this.operand = operand;
        }

        public abstract void Accept(IRegularExpressionVisitor visitor);
    }
}
