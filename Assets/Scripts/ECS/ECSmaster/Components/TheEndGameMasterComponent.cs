internal struct TheEndGameMasterComponent
{
    private bool _isAliveKingMaster;
    private bool _isAliveKingOther;

    private bool _isAliveCityMaster;
    private bool _isAliveCityOther;

    internal bool IsAliveKingMaster
    {
        get { return _isAliveKingMaster; }
        set { _isAliveKingMaster = value; }
    }
    internal bool IsAliveKingOther
    {
        get { return _isAliveKingOther; }
        set { _isAliveKingOther = value; }
    }
}
