using Leopotam.Ecs;
using Photon.Realtime;
using static MainGame;

internal struct BuilderCellMasterComponent
{
    private CellBaseOperations _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyCellIN;
    private BuildingTypes _buildingTypeIN;
    private Player _playerIN;

    private bool _isSettedOUT;

    internal BuilderCellMasterComponent(StartValuesGameConfig nameValueManager, CellBaseOperations cellManager, SystemsMasterManager systemsMasterManager)
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

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Multiple, nameof(BuilderCellMasterSystem));

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



internal class BuilderCellMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<BuilderCellMasterComponent> _builderCellMasterComponentRef = default;
    private EcsComponentRef<ZoneComponent> _zoneComponentRef = default;

    private EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> _economyUnitsMasterComponentRef;
    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> _economyBuildingsMasterComponentRef;




    internal BuilderCellMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _builderCellMasterComponentRef = eCSmanager.EntitiesMasterManager.BuilderCellMasterComponentRef;
        _zoneComponentRef = eCSmanager.EntitiesGeneralManager.ZoneComponentRef;

        _economyMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyMasterComponentRef;
        _economyUnitsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyUnitsMasterComponentRef;
        _economyBuildingsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyBuildingsMasterComponentRef;

        _startValuesGameConfig = InstanceGame.StartValuesGameConfig;
    }


    public void Run()
    {
        
    }

    
}
