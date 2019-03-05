using System.Collections.Generic;

public class NFA
{
    public class Node
    {
        private Node() { }
        private AdjacencyNode _head = null;
        private List<Node> _emptyTransitionNodes = new List<Node>();
    }

    public class AdjacencyNode
    {
        private char _edge;
        private AdjacencyNode _next = null;
    }
}
