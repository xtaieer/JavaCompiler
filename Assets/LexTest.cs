using Xtaieer.Lex;
using UnityEngine;
using System.IO;

public class LexTest : MonoBehaviour
{
    private class Token
    {
        private string lex;

        public Token(string lex)
        {
            this.lex = lex;
        }

        public override string ToString()
        {
            return lex;
        }
    }

    private const string DIGIT_REGULAR = "([1-9][0-9]*|0)(.[0-9]+)?";
    private const string PLUS_REGULAR = "%+";
    private const string SUB_REGULAR = "-";
    private const string MUL_REGULAR = "%*";
    private const string DIV_REGULAR = "/";
    private const string LEFT_BRACKET_REGULAR = "%(";
    private const string RIGHT_BRACKET_REGULAR = "%)";

    private Token DigitAction(string lex, int ling, int column)
    {
        return new Token(lex);
    }

    [SerializeField]
    private string exp;

    [SerializeField]
    private string regular;

    private void Start()
    {
        Test();
    }

    [ContextMenu("Test")]
    public void Test()
    {
        LexicalAnalyzer<Token>.RegularAndAction[] regularAndActions = new LexicalAnalyzer<Token>.RegularAndAction[]
        {
            new LexicalAnalyzer<Token>.RegularAndAction(PLUS_REGULAR, DigitAction),
            new LexicalAnalyzer<Token>.RegularAndAction(SUB_REGULAR, DigitAction),
            new LexicalAnalyzer<Token>.RegularAndAction(MUL_REGULAR, DigitAction),
            new LexicalAnalyzer<Token>.RegularAndAction(DIV_REGULAR, DigitAction),
            new LexicalAnalyzer<Token>.RegularAndAction(LEFT_BRACKET_REGULAR, DigitAction),
            new LexicalAnalyzer<Token>.RegularAndAction(RIGHT_BRACKET_REGULAR, DigitAction),
            new LexicalAnalyzer<Token>.RegularAndAction(DIGIT_REGULAR, DigitAction)
        };
        LexicalAnalyzer<Token> lexicalAnalyzer = new LexicalAnalyzer<Token>(regularAndActions);
        lexicalAnalyzer.SetInput(new StringReader(exp));
        Token token = lexicalAnalyzer.NextToken();
        while(token != null)
        {
            Debug.Log(token);
            token = lexicalAnalyzer.NextToken();
        }
    }

    [ContextMenu("TestNfa")]
    private void TestNfa()
    {
        LexicalAnalyzer<Token>.RegularAndAction[] regularAndActions = new LexicalAnalyzer<Token>.RegularAndAction[]
{
            new LexicalAnalyzer<Token>.RegularAndAction(DIGIT_REGULAR, DigitAction),
        //    new LexicalAnalyzer<Token>.RegularAndAction(PLUS_REGULAR, DigitAction),
            new LexicalAnalyzer<Token>.RegularAndAction(SUB_REGULAR, DigitAction)
    //    new LexicalAnalyzer<Token>.RegularAndAction(MUL_REGULAR, DigitAction),
    //   new LexicalAnalyzer<Token>.RegularAndAction(DIV_REGULAR, DigitAction),
    //    new LexicalAnalyzer<Token>.RegularAndAction(LEFT_BRACKET_REGULAR, DigitAction),
    //    new LexicalAnalyzer<Token>.RegularAndAction(RIGHT_BRACKET_REGULAR, DigitAction)
};
        LexicalAnalyzer<Token> lexicalAnalyzer = new LexicalAnalyzer<Token>(regularAndActions);

       // LexicalAnalyzer<Token> lexicalAnalyzer = new LexicalAnalyzer<Token>(new LexicalAnalyzer<Token>.RegularAndAction[] { new LexicalAnalyzer<Token>.RegularAndAction(regular, (arg1, arg2, arg3) => { return null; })});
        Debug.Log(lexicalAnalyzer.Accept(exp));
    }
}
