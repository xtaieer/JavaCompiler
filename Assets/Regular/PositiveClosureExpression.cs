namespace Xtaieer.Regular
{
    /// <summary>
    /// 代表正则表达式的抽象语法树的正闭包运算的节点
    /// </summary>
    public class PositiveClosureExpression : UnaryExpression
    {
        /// <summary>
        /// 使用单个操作数构造正闭包运算的节点
        /// </summary>
        /// <param name="operand">正闭包运算的操作数</param>
        public PositiveClosureExpression(IRegularExpression operand) : base(operand) { }

        /// <summary>
        /// 访问者模式的实现，以操作数为参数调用<c>IRegularExpressionVisitor.PositiveClosure</c>方法
        /// </summary>
        /// <param name="visitor"></param>
        public override void Accept(IRegularExpressionVisitor visitor)
        {
            visitor.PositiveClosure(operand);
        }
    }
}
