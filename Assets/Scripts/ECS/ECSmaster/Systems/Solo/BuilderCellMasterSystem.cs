using Leopotam.Ecs;
using Photon.Realtime;

internal struct BuilderCellMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyCellIN;
    private BuildingTypes _buildingTypeIN;
    private Player _playerIN;

    private bool _isSettedOUT;

    internal BuilderCellMasterComponent(StartValuesGameConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
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
internal class BuilderCellMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<BuilderCellMasterComponent> _builderCellMasterComponentRef = default;

    private EcsComponentRef<EconomyMasterComponent.UnitsMasterComponent> _economyUnitsMasterComponentRef;
    private EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> _economyBuildingsMasterComponentRef;


    internal BuilderCellMasterSystem(ECSmanager eCSmanager, SupportGameManager supportManager) : base(eCSmanager, supportManager)
    {
        _builderCellMasterComponentRef = eCSmanager.EntitiesMasterManager.BuilderCellMasterComponentRef;

        _economyUnitsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyUnitsMasterComponentRef;
        _economyBuildingsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyBuildingsMasterComponentRef;

        _startValuesGameConfig = supportManager.StartValuesGameConfig;
    }


    public void Run()
    {
        _builderCellMasterComponentRef.Unref().Unpack(out int[] xyCellIN, out BuildingTypes buildingTypeIN, out Player playerIN);

        bool isBuilded;

        if (!CellEnvironmentComponent(xyCellIN).HaveMountain && CellUnitComponent(xyCellIN).HaveAmountSteps)
        {
            CellBuildingComponent(xyCellIN).SetBuilding(buildingTypeIN);
            CellUnitComponent(xyCellIN).AmountSteps -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

            isBuilded = true;
            _builderCellMasterComponentRef.Unref().Pack(isBuilded);
        }
        else
        {
            isBuilded = false;
            _builderCellMasterComponentRef.Unref().Pack(isBuilded);
        }


        if (playerIN.IsMasterClient)
        {
            _economyBuildingsMasterComponentRef.Unref().IsBuildedCityMaster = isBuilded;
            _economyBuildingsMasterComponentRef.Unref().XYsettedCityMaster = xyCellIN;
        }
        else
        {
            _economyBuildingsMasterComponentRef.Unref().IsBuildedCityOther = isBuilded;
            _economyBuildingsMasterComponentRef.Unref().XYsettedCityOther = xyCellIN;
        }
    }
}
