namespace Xtaieer.Regular
{
    /// <summary>
    /// 代表正则表达式的抽象语法树的连接运算的节点
    /// </summary>
    public class ConcatExpression : BinaryExpression
    {
        /// <summary>
        /// 接受左操作数和右操作数的构造
        /// </summary>
        /// <param name="lhs">左操作数</param>
        /// <param name="rhs">右操作数</param>
        public ConcatExpression(IRegularExpression lhs, IRegularExpression rhs) : base(lhs, rhs) { }

        /// <summary>
        /// 访问者模式的实现，会以左右操作数为参数调用<c>IRegularExpressionVisitor.Concat</c>方法
        /// </summary>
        /// <param name="visitor">访问者</param>
        public override void Accept(IRegularExpressionVisitor visitor)
        {
            visitor.Concat(lhs, rhs);
        }
    }
}
