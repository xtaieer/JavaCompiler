namespace Xtaieer.Regular
{
    /// <summary>
    /// 正则表达式的抽象语法树的一个叶子节点，代表一个字符
    /// </summary>
    public class SingleCharLeafExpression : IRegularExpression
    {
        private char c;

        /// <summary>
        /// 使用一个字符构造抽象语法树的叶子节点
        /// </summary>
        /// <param name="c"></param>
        public SingleCharLeafExpression(char c)
        {
            this.c = c;
        }

        /// <summary>
        /// 访问者模式的实现。以所代表的字符为参数调用<c>IRegularExpressionVisitor.SingleCharLeaf</c>方法
        /// </summary>
        /// <param name="visitor">访问者</param>
        void IRegularExpression.Accept(IRegularExpressionVisitor visitor)
        {
            visitor.SingleCharLeaf(c);
        }
    }
}
