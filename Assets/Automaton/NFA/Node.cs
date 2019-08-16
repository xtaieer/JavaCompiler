using System.Collections.Generic;

public class Node
{
    private List<Node> emptyTransitionNodes = new List<Node>();
    static HashSet<Node> IsSearched = new HashSet<Node>(); // Node在多线程环境中使用不安全

    public AdjacencyNode Head
    {
        get;
        set;
    }

    public Node() { }

    public IEnumerable<Node> EmptyClosureIterator()
    {
        IsSearched.Clear();
        return EmptyClosure();
    }

    private IEnumerable<Node> EmptyClosure()
    {
        yield return this;
        foreach (Node node in emptyTransitionNodes)
        {
            if (IsSearched.Contains(node))
            {
                continue;
            }
            IsSearched.Add(node);
            yield return node;
            foreach (Node n in node.EmptyClosure())
            {
                yield return n;
            }
        }
    }

    public IEnumerable<Node> ClosureIterator(char c)
    {
        IsSearched.Clear();
        return Closure(c);
    }

    private IEnumerable<Node> Closure(char c)
    {
        AdjacencyNode h = Head;
        while (h != null)
        {
            if (h.edge.Match(c))
            {
                if (!IsSearched.Contains(h.node))
                {
                    IsSearched.Add(h.node);
                    yield return Head.node;
                    foreach (Node node in h.node.EmptyClosure())
                    {
                        yield return node;
                    }
                }
            }
            h = h.Next;
        }
    }

    public void AddEmptyTransition(Node node)
    {
        emptyTransitionNodes.Add(node);
    }
}

public class AdjacencyNode
{
    private AdjacencyNode next = null;

    public Edge edge
    {
        get;
        private set;
    }

    public Node node
    {
        get;
        private set;
    }

    public AdjacencyNode Next
    {
        get;
        private set;
    }

    public AdjacencyNode(Node node, Edge edge, AdjacencyNode next)
    {
        this.node = node;
        this.edge = edge;
        this.next = next;
    }
}
