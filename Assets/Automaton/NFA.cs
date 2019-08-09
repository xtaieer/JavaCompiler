using System.Collections.Generic;
using System;

public class NFA : IAutomaton
{
    private HashSet<Node> nodes = new HashSet<Node>();
    private HashSet<Node> acceptNodes = new HashSet<Node>();

    public Node Start
    {
        get;
        set;
    }

    public Node GenerateNode()
    {
        Node node = new Node();
        nodes.Add(node);
        return node;
    }

    public void AddTransition(Node from, Node to, Edge edge)
    {
        CheckNodeValid(from, "from");
        CheckNodeValid(to, "to");

        AdjacencyNode an = new AdjacencyNode(to, edge, from.Head);
        from.Head = an;
    }

    public void AddEmptyTransition(Node from, Node to)
    {
        CheckNodeValid(from, "from");
        CheckNodeValid(to, "to");

        from.AddEmptyTransition(to);
    }

    public void AddAcceptNode(Node node)
    {
        CheckNodeValid(node, "node");

        acceptNodes.Add(node);
    }

    public bool Accept(Node node)
    {
        CheckNodeValid(node, "node");

        return acceptNodes.Contains(node);
    }

    public Node MergeNode(Node node1, Node node2)
    {
        CheckNodeValid(node1, "node1");
        CheckNodeValid(node2, "node2");

        AddEmptyTransition(node1, node2);
        return node1;
    }

    public bool IsAccept(string str)
    {
        HashSet<Node> already = new HashSet<Node>();
        List<Node> oldNodes = new List<Node>();
        List<Node> newNodes = new List<Node>();
        foreach(Node node in  Start.EmptyClosureIterator())
        {
            oldNodes.Add(node);
        }

        for(int i = 0; i < str.Length; i ++)
        {
            already.Clear();
            char c = str[i];
            foreach(Node old in oldNodes)
            {
                foreach(Node newNode in old.ClosureIterator(c))
                {
                    foreach(Node n in newNode.EmptyClosureIterator())
                    {
                        if(!already.Contains(n))
                        {
                            already.Add(n);
                            newNodes.Add(n);
                        }
                    }
                }
            }
            List<Node> temp = oldNodes;
            oldNodes = newNodes;
            newNodes = temp;
            newNodes.Clear();
        }
        foreach(Node old in oldNodes)
        {
            if(Accept(old))
            {
                return true;
            }
        }
        return false;
    }

    bool IAutomaton.IsAccept(string str)
    {
        return IsAccept(str);
    }

    private void CheckNodeValid(Node node, string name)
    {
#if DEBUG
        if (node == null)
        {
            throw new ArgumentNullException(name + "节点不能为空");
        }
        if (!nodes.Contains(node))
        {
            throw new ArgumentException(name + "不是该NFA的节点");
        }
#endif
    }
}
