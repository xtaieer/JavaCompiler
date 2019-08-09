using UnityEngine;
using Regular;

public class NFATest : MonoBehaviour
{
    [SerializeField]
    private string regular;

    [SerializeField]
    private string str;
    
    [ContextMenu("Test")]
    private void Test()
    {
        RegularParser parser = new RegularParser();
        IRegularExpression exp = parser.Parse(regular);
        RegularPrinter printer = new RegularPrinter();
        exp.Accept(printer);
        printer.Result();
    }
}
