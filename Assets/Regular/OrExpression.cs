namespace Xtaieer.Regular
{
    /// <summary>
    /// 代码正则表达式的抽象语法树中的或运算的节点
    /// </summary>
    public class OrExpression : BinaryExpression
    {
        /// <summary>
        /// 使用左右两个操作数构造一个或运算节点
        /// </summary>
        /// <param name="lhs">左操作数</param>
        /// <param name="rhs">右操作数</param>
        public OrExpression(IRegularExpression lhs, IRegularExpression rhs) : base(lhs, rhs) { }

        /// <summary>
        /// 访问者模式的实现，以左右操作数为参数调用<c>IRegularExpressionVisitor.Or</c>方法
        /// </summary>
        /// <param name="visitor">访问者</param>
        public override void Accept(IRegularExpressionVisitor visitor)
        {
            visitor.Or(lhs, rhs);
        }
    }
}
