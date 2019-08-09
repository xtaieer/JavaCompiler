namespace Xtaieer.Regular
{
    public class SingleCharLeafExpression : IRegularExpression
    {
        private char c;

        public SingleCharLeafExpression(char c)
        {
            this.c = c;
        }

        void IRegularExpression.Accept(IRegularExpressionVisitor visitor)
        {
            visitor.SingleCharLeaf(c);
        }
    }
}
