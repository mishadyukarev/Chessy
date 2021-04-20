using Photon.Realtime;


public struct AttackUnitMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyPreviousCellIN;
    private int[] _xySelectedCellIN;
    private Player _fromPlayerIN;

    private bool _isAttackedOUT;

    public AttackUnitMasterComponent(StartValuesConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
    {
        _cellManager = cellManager;
        _systemsMasterManager = systemsMasterManager;

        _xyPreviousCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _xySelectedCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _fromPlayerIN = default;

        _isAttackedOUT = default;
    }


    public bool AttackUnit(in int[] xyPreviousCellIN, in int[] xySelectedCellIN, in Player fromPlayerIN)
    {
        _cellManager.CopyXYinTo(xyPreviousCellIN, _xyPreviousCellIN);
        _cellManager.CopyXYinTo(xySelectedCellIN, _xySelectedCellIN);
        _fromPlayerIN = fromPlayerIN;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(AttackUnitMasterSystem));

        return _isAttackedOUT;
    }

    public void Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN, out Player fromPlayerIN)
    {
        xyPreviousCellIN = _xyPreviousCellIN;
        xySelectedCellIN = _xySelectedCellIN;
        fromPlayerIN = _fromPlayerIN;
    }

    internal void Pack(bool isAttackedOUT) => _isAttackedOUT = isAttackedOUT;
}
