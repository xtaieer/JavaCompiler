namespace Xtaieer.Regular
{
    public class QuestionMarkExpression : UnaryExpression
    {
        public QuestionMarkExpression(IRegularExpression operand) : base(operand) { }

        public override void Accept(IRegularExpressionVisitor visitor)
        {
            visitor.QuestionMark(operand);
        }
    }
}
