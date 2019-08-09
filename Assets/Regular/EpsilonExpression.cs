namespace Xtaieer.Regular
{
    public class EpsilonExpression : IRegularExpression
    {
        void IRegularExpression.Accept(IRegularExpressionVisitor visitor)
        {
            visitor.Epsilon();
        }
    }
}
