namespace Xtaieer.Regular
{
    /// <summary>
    /// 正则表达式的抽象语法树的访问者接口，提供了所实现的正则表达式的操作
    /// </summary>
    public interface IRegularExpressionVisitor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        void RangeCharLeaf(char from, char to);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        void SingleCharLeaf(char c);

        /// <summary>
        /// 连接操作，由<c>ConcatExpression</c>节点使用
        /// </summary>
        /// <param name="lhs">左操作数</param>
        /// <param name="rhs">右操作数</param>
        void Concat(IRegularExpression lhs, IRegularExpression rhs);
        
        /// <summary>
        /// 或操作，由<c>OrExpression</c>节点使用
        /// </summary>
        /// <param name="lhs">左操作数</param>
        /// <param name="rhs">由操作数</param>
        void Or(IRegularExpression lhs, IRegularExpression rhs);

        /// <summary>
        /// 闭包操作，由<c>ClosureExpression</c>节点使用
        /// </summary>
        /// <param name="operand">闭包操作的操作数</param>
        void Closure(IRegularExpression operand);

        /// <summary>
        /// 正闭包操作，由<c>PositiveClosureExpression</c>节点使用
        /// </summary>
        /// <param name="operand">正闭包操作的操作数</param>
        void PositiveClosure(IRegularExpression operand);

        /// <summary>
        /// 问号操作（不知道这个专业名词是啥），由<c>QuestionMarkExpression</c>节点使用
        /// </summary>
        /// <param name="operand">问号操作的操作数</param>
        void QuestionMark(IRegularExpression operand);


        void Epsilon();
    }
}
