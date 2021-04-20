
using Photon.Realtime;

public struct SetterUnitMasterComponent
{
    private int[] _xyCellIN;
    private UnitTypes _unitTypeIN;
    private Player _playerIN;

    private bool _isSettedOUT;

    private StartValuesConfig _nameValueManager;
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;


    public SetterUnitMasterComponent(StartValuesConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
    {
        _xyCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _unitTypeIN = default;
        _playerIN = default;

        _isSettedOUT = default;

        _nameValueManager = nameValueManager;
        _cellManager = cellManager;
        _systemsMasterManager = systemsMasterManager;
    }


    public bool TrySetUnit(int[] xyCell, UnitTypes unitType, Player player)
    {
        _cellManager.CopyXYinTo(xyCell, _xyCellIN);
        _unitTypeIN = unitType;
        _playerIN = player;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(SetterUnitMasterSystem));

        return _isSettedOUT;
    }

    public void GetValues(out int[] xyCell, out UnitTypes unitType, out Player player)
    {
        xyCell = _xyCellIN;
        unitType = _unitTypeIN;
        player = _playerIN;
    }

    public void SetValues(bool isSetted)
    {
        _isSettedOUT = isSetted;
    }
}
