public class OrExpression : BinaryExpression
{
    public OrExpression(IRegularExpression lhs, IRegularExpression rhs) : base(lhs, rhs) { }

    public override void Accept(IRegularExpressionVisitor visitor)
    {
        visitor.Or(_lhs, _rhs);
    }

    public override string ToString()
    {
        return _lhs.ToString() + "|" + _rhs.ToString();
    }
}
