namespace Xtaieer.Regular
{
    public class RangeCharLeafExpression : IRegularExpression
    {
        private char from;
        private char to;

        public RangeCharLeafExpression(char from, char to)
        {
            this.from = from;
            this.to = to;
        }

        void IRegularExpression.Accept(IRegularExpressionVisitor visitor)
        {
            visitor.RangeCharLeaf(from, to);
        }
    }
}
