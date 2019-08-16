namespace Xtaieer.Automaton.Nfa
{
    public interface INFABuilder<Node, Edge>
    {
        Node GenerateNode();
        Node Merge(Node node1, Node node2);
        void AddTransition(Node from, Node to, Edge edge);
        void SetStart(Node start);
        void AddAccept(Node node);
//        IAutomaton GetResult();
    }
}
