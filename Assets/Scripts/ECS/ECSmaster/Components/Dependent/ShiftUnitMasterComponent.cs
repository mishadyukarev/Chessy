using Photon.Realtime;

public struct ShiftUnitMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyPreviousCellIN;
    private int[] _xySelectedCellIN;
    private Player _playerIN;

    private bool _isShiftedOUT;


    public ShiftUnitMasterComponent(StartValuesGameConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
    {
        _cellManager = cellManager;
        _systemsMasterManager = systemsMasterManager;

        _xyPreviousCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _xySelectedCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _playerIN = default;

        _isShiftedOUT = default;
    }


    public bool ShiftUnit(int[] xyPreviousCell, int[] xySelectedCell, Player player)
    {
        _cellManager.CopyXYinTo(xyPreviousCell, _xyPreviousCellIN);
        _cellManager.CopyXYinTo(xySelectedCell, _xySelectedCellIN);
        _playerIN = player;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(ShiftUnitMasterSystem));

        return _isShiftedOUT;
    }

    public void Unpack(out int[] xyPreviousCell, out int[] xySelectedCell, out Player player)
    {
        xyPreviousCell = _xyPreviousCellIN;
        xySelectedCell = _xySelectedCellIN;
        player = _playerIN;
    }

    public void Pack(bool isShifted)
    {
        _isShiftedOUT = isShifted;
    }
}
