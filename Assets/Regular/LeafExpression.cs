namespace Xtaieer.Regular
{
    public class LeafExpression : IRegularExpression
    {
        private char[] chars;

        public LeafExpression(params char[] chars)
        {
            this.chars = chars;
        }

        public LeafExpression(string chars) : this(chars.ToCharArray()) { }

        public void Accept(IRegularExpressionVisitor visitor)
        {
        }
    }
}
