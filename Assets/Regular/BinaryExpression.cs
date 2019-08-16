namespace Xtaieer.Regular
{
    /// <summary>
    /// 正则表达式的抽象语法树中二元操作符的公共父类
    /// </summary>
    public abstract class BinaryExpression : IRegularExpression
    {
        // 左操作数
        protected IRegularExpression lhs;

        // 右操作数
        protected IRegularExpression rhs;

        /// <summary>
        /// 使用左操作数和右操作数构造
        /// </summary>
        /// <param name="lhs">左操作数</param>
        /// <param name="rhs">右操作数</param>
        public BinaryExpression(IRegularExpression lhs, IRegularExpression rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        public abstract void Accept(IRegularExpressionVisitor visitor);
    }
}
