using Photon.Realtime;

internal struct BuilderCellMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyCellIN;
    private BuildingTypes _buildingTypeIN;
    private Player _playerIN;

    private bool _isSettedOUT;

    internal BuilderCellMasterComponent(StartValuesConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
    {
        _cellManager = cellManager;
        _systemsMasterManager = systemsMasterManager;

        _xyCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _buildingTypeIN = default;
        _playerIN = default;

        _isSettedOUT = default;
    }


    internal bool Build(int[] xyCellIN, BuildingTypes buildingTypeIN, Player playerIN)
    {
        _cellManager.CopyXYinTo(xyCellIN, _xyCellIN);
        _buildingTypeIN = buildingTypeIN;
        _playerIN = playerIN;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(BuilderCellMasterSystem));

        return _isSettedOUT;
    }

    internal void Unpack(out int[] xyCellIN, out BuildingTypes buildingTypeIN, out Player playerIN)
    {
        xyCellIN = _xyCellIN;
        buildingTypeIN = _buildingTypeIN;
        playerIN = _playerIN;
    }

    internal void Pack(in bool isSetted)
    {
        _isSettedOUT = isSetted;
    }
}
