using static MainGame;

public struct DonerComponent
{
    public bool IsDoneMaster;
    public bool IsDoneOther;

    internal bool NeedSetKing;

    internal bool IsCurrentDone
    {
        get
        {
            if (InstanceGame.IsMasterClient) return IsDoneMaster;
            else return IsDoneOther;
        }
        set
        {
            if (InstanceGame.IsMasterClient) IsDoneMaster = value;
            else IsDoneOther = value;
        }
    }
}
