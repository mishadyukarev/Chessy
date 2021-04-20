
using Photon.Realtime;

public struct RefresherMasterComponent
{
    private bool _isDoneMasterIN;
    private bool _isDoneOtherIN;
    private bool _isRefreshedOUT;

    private SystemsMasterManager _systemsMasterManager;


    public bool IsDoneMasterIN => _isDoneMasterIN;
    public bool IsDoneOtherIN => _isDoneOtherIN;
    public bool IsRefreshedOUT => _isRefreshedOUT;


    public RefresherMasterComponent(SystemsMasterManager systemsMasterManager)
    {
        _isDoneMasterIN = default;
        _isDoneOtherIN = default;
        _isRefreshedOUT = default;

        _systemsMasterManager = systemsMasterManager;
    }


    public bool TryRefreshed(Player player, bool isDone)
    {
        if (player.IsMasterClient) _isDoneMasterIN = isDone;
        else { _isDoneOtherIN = isDone; }


        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(RefresherMasterSystem));

        return _isRefreshedOUT;
    }

    public void GetValues(out bool isDoneMaster, out bool isDoneOther)
    {
        isDoneMaster = _isDoneMasterIN;
        isDoneOther = _isDoneOtherIN;
    }

    internal void SetValues(bool isRefreshed)
    {
        _isRefreshedOUT = isRefreshed;
    }

    internal void ResetValues()
    {
        _isDoneMasterIN = default;
        _isDoneOtherIN = default;
        _isRefreshedOUT = default;
    }
}
