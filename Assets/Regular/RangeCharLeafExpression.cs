namespace Xtaieer.Regular
{
    /// <summary>
    /// 正则表达式的抽象语法树的一个类型的叶子节点，代表一个范围内的所有字符
    /// </summary>
    public class RangeCharLeafExpression : IRegularExpression
    {
        private char from;
        private char to;

        /// <summary>
        /// 使用一个闭区间为范围构造一个叶子节点
        /// </summary>
        /// <param name="from">区间的起点（包含）</param>
        /// <param name="to">区间的终点（包含）</param>
        public RangeCharLeafExpression(char from, char to)
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// 访问者实现，以区间的起点和终点为参数调用<c>IRegularExpressionVisitor.RangeCharLeaf</c>方法
        /// </summary>
        /// <param name="visitor">访问者</param>
        void IRegularExpression.Accept(IRegularExpressionVisitor visitor)
        {
            visitor.RangeCharLeaf(from, to);
        }
    }
}
