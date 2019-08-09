namespace Xtaieer.Regular
{
    public interface IRegularExpressionVisitor
    {
        void RangeCharLeaf(char form, char to);
        void SingleCharLeaf(char c);
        void Concat(IRegularExpression lhs, IRegularExpression rhs);
        void Or(IRegularExpression lhs, IRegularExpression rhs);
        void Closure(IRegularExpression operand);
        void QuestionMark(IRegularExpression operand);
        void Epsilon();
    }
}
