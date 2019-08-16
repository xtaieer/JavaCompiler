namespace Xtaieer.Regular
{
    /// <summary>
    /// 代表正则表达式的抽象语法书的闭包运算的节点
    /// </summary>
    public class ClosureExpression : UnaryExpression
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operand">闭包运算的操作数</param>
        public ClosureExpression(IRegularExpression operand) : base(operand) { }

        /// <summary>
        /// 访问者模式的实现，以闭包操作的操作数为参数调用<c>IRegularExpressionVisitor.Closure</c>方法
        /// </summary>
        /// <param name="visitor">访问者</param>
        public override void Accept(IRegularExpressionVisitor visitor)
        {
            visitor.Closure(operand);
        }
    }
}
