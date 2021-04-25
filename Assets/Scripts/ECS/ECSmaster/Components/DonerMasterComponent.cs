
public struct DonerMasterComponent
{
    private bool _isDoneMasterIN;
    private bool _isDoneOtherIN;

    public bool IsDoneMaster
    {
        get { return _isDoneMasterIN; }
        set { _isDoneMasterIN = value; }
    }
    public bool IsDoneOther
    {
        get { return _isDoneOtherIN; }
        set { _isDoneOtherIN = value; }
    }
}
