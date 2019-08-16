public abstract class Edge
{
    public abstract bool Match(char c);
}

public class SingleCharEdge : Edge
{
    private char c;
    public SingleCharEdge(char c)
    {
        this.c = c;
    }

    public override bool Match(char c)
    {
        return this.c == c;
    }
}

public class RangeCharEdge : Edge
{
    private char from;
    private char to;
    public RangeCharEdge(char from, char to)
    {
        this.from = from;
        this.to = to;
    }

    public override bool Match(char c)
    {
        return c >= from && c <= to;
    }
}

public class RandomCharEdge : Edge
{
    public override bool Match(char c)
    {
        return true;
    }
}

public class Negation : Edge
{
    private Edge edge;
    public Negation(Edge edge)
    {
        this.edge = edge;
    }

    public override bool Match(char c)
    {
        return !edge.Match(c);
    }
}
