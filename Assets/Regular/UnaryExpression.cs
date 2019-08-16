namespace Xtaieer.Regular
{
    /// <summary>
    /// 正则表达式的抽象语法树中单目运算的节点公共父类
    /// </summary>
    public abstract class UnaryExpression : IRegularExpression
    {
        // 单目运算的操作数
        protected IRegularExpression operand;

        public UnaryExpression(IRegularExpression operand)
        {
            this.operand = operand;
        }

        public abstract void Accept(IRegularExpressionVisitor visitor);
    }
}
