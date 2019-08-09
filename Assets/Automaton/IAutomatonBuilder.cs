public interface IAutomatonBuilder
{
    Node GenerateNode();
    Node Merge(Node node1, Node node2);
    void AddTransition(Node from, Node to, Edge edge);
    void SetStart(Node start);
    void Accept(Node node);
    IAutomaton GetResult();
}
