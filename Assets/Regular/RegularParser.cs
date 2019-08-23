namespace Xtaieer.Regular
{
    /// <summary>
    /// 正则表达式解析类，提供把字符串表示的正则表达式解析成正则表达式的抽象语法树
    /// </summary>
    public class RegularParser
    {
        /*
         * 终结符 运算符："|"、"*"、"("、")"、"+"、"?"、"%"  ,非运算符的其他字符 char
         * S -> Exp
         * Exp -> Concat Exp'
         * Exp' -> "|" Concat Exp' | ε
         * Concat -> Closur Concat'
         * Concat' -> Closur Concat' | ε
         * Closur -> Factor Closur'
         * Closur' -> * | ε
         * Factor -> char | (Exp) 
         * 
         * S -> Exp                        S.val = Exp.val
         * Exp -> Concat Exp'              Exp.val = Exp'.sval, Exp'.ival = Concat.sval
         * Exp'1 -> "|" Concat Exp'2       Exp'2.ival = Exp'1.ival | Concat.sval, Exp'1.sval = Exp'2.sval
                  | ε                     Exp'1.sval = Exp'1.ival
         * Concat -> Closur Concat'        Concat.val = Concat'.sval, Concat'.inval = Closur.val
         * Concat'1 -> Closur Concat'2     Concat'2.ival = Concat'1 & Closur.val, Concat'1.sval = Concat'2.sval
                  | ε                     Concat'1.sval = Concat'1.ival
         * Closur -> Factor Closur'        Closur.val = F(Factor.val, Closur'.val)
         * Closur' -> *                    Closur'.val = * 
         *        | +                      Closur'.val = + 
         *        | ?                      Closur'.val = ?
                  | ε                     Closur'.val = null
         * Factor -> char                  Factor.var = char
                   | (Exp)                 Factor.val = Exp.val
                   | [char1-char2]         Factor.val = char1 to char2
                   | %char                 Factor.val = char
         */

        private char current;
        private int index;
        private string regular;

        /// <summary>
        /// 字符串表示的正则表达式解析成正则表达式的抽象语法树
        /// </summary>
        /// <param name="regular">字符串表示的正则表达式</param>
        /// <returns>抽象语法树的根节点</returns>
        public static IRegularExpression Parse(string regular)
        {
            RegularParser parser = new RegularParser();

            parser.regular = regular;
            parser.index = 0;
            // TODO regular的为空的处理
            parser.current = regular[0];
            return parser.Exp();
        }

        private IRegularExpression Exp()
        {
            IRegularExpression exp = Concat();
            return ExpT(exp);
        }

        private IRegularExpression ExpT(IRegularExpression exp)
        {
            if (current == '|')
            {
                Match('|');
                IRegularExpression concat = Concat();
                return ExpT(new OrExpression(exp, concat));
            }
            return exp;
        }

        private IRegularExpression Concat()
        {
            IRegularExpression closur = Closur();
            return ConcatT(closur);
        }

        private IRegularExpression ConcatT(IRegularExpression exp)
        {
            if (current != '*' && current != '|' && current != ')' && current != '\0')
            {
                IRegularExpression closur = Closur();
                return ConcatT(new ConcatExpression(exp, closur));
            }
            return exp;
        }

        private IRegularExpression Closur()
        {
            IRegularExpression factor = Factor();
            char c = ClosurT();
            if (c == '*')
            {
                return new ClosureExpression(factor);
            }
            else if (c == '?')
            {
                return new QuestionMarkExpression(factor);
            }
            else if (c == '+')
            {
                return new PositiveClosureExpression(factor);
            }
            else
            {
                return factor;
            }
        }

        private char ClosurT()
        {
            if (current == '*')
            {
                Match('*');
                return '*';
            }
            else if (current == '?')
            {
                Match('?');
                return '?';
            }
            else if (current == '+')
            {
                Match('+');
                return '+';
            }
            return '\0';
        }

        private IRegularExpression Factor()
        {
            if (current == '(')
            {
                Match('(');
                IRegularExpression exp = Exp();
                Match(')');
                return exp;
            }
            if (current == '[')
            {
                Match('[');
                char from = '\0', to = '\0';
                if (!IsOperator(current))
                {
                    from = current;
                    Match(current);
                }
                Match('-');
                if (!IsOperator(current))
                {
                    to = current;
                    Match(current);
                }
                Match(']');
                return new RangeCharLeafExpression(from, to);
            }
            if (current == '%')
            {
                Match('%');
                char c = current;
                Match(current);
                return new SingleCharLeafExpression(c);
            }
            if(current == '.')
            {
                Match(current);
                return new RangeCharLeafExpression((char)0, (char)255);
            }
            else
            {
                if (!IsOperator(current))
                {
                    char c = current;
                    Match(current);
                    return new SingleCharLeafExpression(c);
                }
                else
                {
                    // Error;
                }
            }
            return null;
        }

        private IRegularExpression RangeChar()
        {
            return null;
        }

        private bool IsOperator(char c)
        {
            return c == '*' || c == '|' || c == ')' || c == '(';
        }

        private void Match(char c)
        {
            if (current == c)
            {
                index++;
                if (index < regular.Length)
                {
                    current = regular[index];
                }
                else
                {
                    current = '\0';
                }
            }
            else
            {
                Error(c, current);
            }
        }

        private void Error(char expect, char actual)
        {
            Error("期望值为" + expect + ",实际值却是" + actual);
        }

        private void Error(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
