using Photon.Realtime;


internal struct GetterUnitMasterComponent
{
    private SystemsMasterManager _systemsMasterManager;

    private UnitTypes _unitTypeIN;
    private Player _playerIN;

    private bool _isGettedOUT;



    internal GetterUnitMasterComponent(SystemsMasterManager systemsMasterManager)
    {
        _systemsMasterManager = systemsMasterManager;

        _unitTypeIN = default;
        _playerIN = default;

        _isGettedOUT = default;
    }


    internal bool TryGetUnit(UnitTypes unitTypeIN, Player playerIN)
    {
        _unitTypeIN = unitTypeIN;
        _playerIN = playerIN;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Inventor, nameof(GetterUnitMasterSystem));

        return _isGettedOUT;
    }

    internal void Unpack(out UnitTypes unitTypeIN, out Player playerIN)
    {
        unitTypeIN = _unitTypeIN;
        playerIN = _playerIN;
    }

    internal void Pack(bool isGettedOUT) => _isGettedOUT = isGettedOUT;
}
