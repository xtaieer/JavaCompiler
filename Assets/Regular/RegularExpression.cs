namespace Xtaieer.Regular
{
    /// <summary>
    /// 代表正则表达式的抽象语法树的节点，使用了访问者模式来访问语法树中的各个节点
    /// </summary>
    public interface IRegularExpression
    {
        /// <summary>
        /// 访问者模式的实现，可以用于遍历抽象语法树
        /// </summary>
        /// <param name="visitor">访问者</param>
        void Accept(IRegularExpressionVisitor visitor);
    }
}
