namespace Xtaieer.Automaton.Nfa
{
    public class NFABuilder : INFABuilder<Node, Edge>
    {
        NFA nfa = new NFA();
#if DEBUG
        bool finished = false;
#endif

        public void AddAccept(Node node)
        {
            CheckFinished();
            nfa.AddAcceptNode(node);
        }

        public void AddTransition(Node from, Node to, Edge edge)
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

        public void SetStart(Node start)
        {
            CheckFinished();
            nfa.Start = start;
        }

        public Node Merge(Node node1, Node node2)
        {
            return nfa.MergeNode(node1, node2);
        }

        public Node GenerateNode()
        {
            CheckFinished();
            return nfa.GenerateNode();
        }

        public NFA GetResult()
        {
#if DEBUG
            finished = true;
#endif
            return nfa;
        }

        void CheckFinished()
        {
#if DEBUG
            if (finished)
            {
                throw new System.InvalidOperationException("构造已经完成，不能有新的操作");
            }
#endif
        }
    }
}
