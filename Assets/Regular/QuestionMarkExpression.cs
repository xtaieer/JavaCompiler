namespace Xtaieer.Regular
{
    /// <summary>
    /// 代表正则表达式的抽象语法树的?运算的节点
    /// </summary>
    public class QuestionMarkExpression : UnaryExpression
    {
        /// <summary>
        /// 使用一个操作数构造?运算的节点
        /// </summary>
        /// <param name="operand">?运算的操作数</param>
        public QuestionMarkExpression(IRegularExpression operand) : base(operand) { }

        /// <summary>
        /// 访问者模式的实现，以操作数为参数调用<c>IRegularExpressionVisitor.QuestionMark</c>方法
        /// </summary>
        /// <param name="visitor">访问者</param>
        public override void Accept(IRegularExpressionVisitor visitor)
        {
            visitor.QuestionMark(operand);
        }
    }
}
