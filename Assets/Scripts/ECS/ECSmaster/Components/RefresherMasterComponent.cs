internal struct RefresherMasterComponent
{
    private bool _isDone;
    private bool _isRefreshed;

    internal bool IsDone
    {
        get { return _isDone; }
        set { _isDone = value; }
    }
    internal bool IsRefreshed
    {
        get { return _isRefreshed; }
        set { _isRefreshed = value; }
    }
}
