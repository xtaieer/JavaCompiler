namespace Xtaieer.Regular
{
    public static class RegularExpressionExtension
    {
        public static IRegularExpression Concat(this IRegularExpression lhs, IRegularExpression rhs)
        {
            return new ConcatExpression(lhs, rhs);
        }

        public static IRegularExpression Or(this IRegularExpression lhs, IRegularExpression rhs)
        {
            return new OrExpression(lhs, rhs);
        }

        public static IRegularExpression Closure(this IRegularExpression operand)
        {
            return new ClosureExpression(operand);
        }
    }
}
