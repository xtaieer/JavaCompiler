using UnityEngine;
using Xtaieer.Automaton.Nfa;
using Xtaieer.Regular;
using Xtaieer.Automaton;

public class NFATest : MonoBehaviour
{
    [SerializeField]
    private string regular;

    [SerializeField]
    private string str;
    
    [ContextMenu("Test")]
    private void Test()
    {
        //(+|-)?([1-9][0-9]*|0)(.[0-9]*)?
        RegularParser parser = new RegularParser();
        IRegularExpression exp = parser.Parse(regular);
        RegularPrinter printer = new RegularPrinter();
        exp.Accept(printer);
        printer.Result();
    }

    [ContextMenu("Match")]
    private void Match()
    {
        RegularParser parser = new RegularParser();
        IRegularExpression exp = parser.Parse(regular);
        NFADirector director = new NFADirector();
        IAutomaton automaton = director.Generate(exp, new NFABuilder());
 //       Debug.Log(automaton.IsAccept(str));
    }
}
