internal struct ReadyComponent
{
    private bool _isReady;

    internal bool IsReady
    {
        get { return _isReady; }
        set { _isReady = value; }
    }
}
