public abstract class BinaryExpression : IRegularExpression
{
    protected IRegularExpression _lhs;
    protected IRegularExpression _rhs;

    public BinaryExpression(IRegularExpression lhs, IRegularExpression rhs)
    {
        _lhs = lhs;
        _rhs = rhs;
    }
}
