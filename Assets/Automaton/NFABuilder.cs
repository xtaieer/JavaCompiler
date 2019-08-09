public class NFABuilder : IAutomatonBuilder
{
    NFA nfa = new NFA();
#if DEBUG
    bool finished = false;
#endif

    void IAutomatonBuilder.Accept(Node node)
    {
        CheckFinished();
        nfa.AddAcceptNode(node);
    }

    void IAutomatonBuilder.AddTransition(Node from, Node to, Edge edge)
    {
        CheckFinished();
        if (edge == null)
        {
            nfa.AddEmptyTransition(from, to);
        }
        else
        {
            nfa.AddTransition(from, to, edge);
        }
    }

    void IAutomatonBuilder.SetStart(Node start)
    {
        CheckFinished();
        nfa.Start = start;
    }

    Node IAutomatonBuilder.Merge(Node node1, Node node2)
    {
        return nfa.MergeNode(node1, node2);
    }

    Node IAutomatonBuilder.GenerateNode()
    {
        CheckFinished();
        return nfa.GenerateNode();
    }

    IAutomaton IAutomatonBuilder.GetResult()
    {
#if DEBUG
        finished = true;
#endif
        return nfa;
    }

    void CheckFinished()
    {
#if DEBUG
        if(finished)
        {
            throw new System.InvalidOperationException("构造已经完成，不能有新的操作");
        }
#endif
    }
}
