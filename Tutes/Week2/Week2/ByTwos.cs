// Implement ISeries.
public class ByTwos : ISeries
{
    private int _start;
    private int _val;

    public ByTwos() : this(0)
    { }

    public ByTwos(int start)
    {
        SetStart(start);
    }

    public int GetNext()
    {
        _val += 2;
        return _val;
    }

    public void Reset()
    {
        _val = _start;
    }

    public void SetStart(int x)
    {
        _start = x;
        _val = _start;
    }
}
