internal struct ReadyMasterComponent
{
    private bool _isReadyMaster;
    private bool _isReadyOther;

    internal bool IsReadyMaster
    {
        get { return _isReadyMaster; }
        set { _isReadyMaster = value; }
    }
    internal bool IsReadyOther
    {
        get { return _isReadyOther; }
        set { _isReadyOther = value; }
    }
}
