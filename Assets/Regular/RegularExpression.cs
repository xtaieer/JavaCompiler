namespace Xtaieer.Regular
{
    public interface IRegularExpression
    {
        void Accept(IRegularExpressionVisitor visitor);
    }
}
