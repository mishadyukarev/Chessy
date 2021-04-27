using Leopotam.Ecs;
using Photon.Realtime;
using static MainGame;

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
    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponentRef = default;
    private EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> _economyBuildingsMasterComponentRef;


    internal BuilderCellMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _builderCellMasterComponentRef = eCSmanager.EntitiesMasterManager.BuilderCellMasterComponentRef;

        _economyMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyMasterComponentRef;
        _economyUnitsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyUnitsMasterComponentRef;
        _economyBuildingsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyBuildingsMasterComponentRef;

        _startValuesGameConfig = InstanceGame.StartValuesGameConfig;
    }


    public void Run()
    {
        _builderCellMasterComponentRef.Unref().Unpack(out int[] xyCellIN, out BuildingTypes buildingTypeIN, out Player playerIN);

        bool isBuilded;

        if (!CellEnvironmentComponent(xyCellIN).HaveMountain && CellUnitComponent(xyCellIN).HaveMaxSteps && !CellBuildingComponent(xyCellIN).HaveBuilding)
        {
            switch (buildingTypeIN)
            {
                case BuildingTypes.None:
                    isBuilded = false;
                    break;

                case BuildingTypes.City:

                    CellBuildingComponent(xyCellIN).SetBuilding(buildingTypeIN, playerIN);
                    isBuilded = true;
                    CellUnitComponent(xyCellIN).AmountSteps -= CellUnitComponent(xyCellIN).MaxAmountSteps;

                    break;

                case BuildingTypes.Farm:

                    if (CellEnvironmentComponent(xyCellIN).HaveFood)
                    {
                        if (playerIN.IsMasterClient)
                        {
                            if(_economyMasterComponentRef.Unref().GoldMaster >= 120)
                            {
                                _economyMasterComponentRef.Unref().GoldMaster -= 120;

                                CellBuildingComponent(xyCellIN).SetBuilding(buildingTypeIN, playerIN);
                                if (playerIN.IsMasterClient) _economyBuildingsMasterComponentRef.Unref().AmountFarmsMaster += 1; // !!!!!
                                else _economyBuildingsMasterComponentRef.Unref().AmountFarmsOther += 1; // !!!!!

                                CellUnitComponent(xyCellIN).AmountSteps -= CellUnitComponent(xyCellIN).MaxAmountSteps;
                            }
                        }
                        else
                        {
                            if (_economyMasterComponentRef.Unref().GoldOther >= 120)
                            {
                                _economyMasterComponentRef.Unref().GoldOther -= 120;

                                CellBuildingComponent(xyCellIN).SetBuilding(buildingTypeIN, playerIN);
                                if (playerIN.IsMasterClient) _economyBuildingsMasterComponentRef.Unref().AmountFarmsMaster += 1; // !!!!!
                                else _economyBuildingsMasterComponentRef.Unref().AmountFarmsOther += 1; // !!!!!

                                CellUnitComponent(xyCellIN).AmountSteps -= CellUnitComponent(xyCellIN).MaxAmountSteps;
                            }
                        }

                    }
                    isBuilded = true;

                    break;

                case BuildingTypes.Woodcutter:
                    isBuilded = true;
                    break;

                case BuildingTypes.Mine:
                    isBuilded = true;
                    break;

                default:
                    isBuilded = false;
                    break;
            }          
            _builderCellMasterComponentRef.Unref().Pack(isBuilded);
        }
        else
        {
            isBuilded = false;
            _builderCellMasterComponentRef.Unref().Pack(isBuilded);
        }

        if (buildingTypeIN == BuildingTypes.City)
        {
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
}
