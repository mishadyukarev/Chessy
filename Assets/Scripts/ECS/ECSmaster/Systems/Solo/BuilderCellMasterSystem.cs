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

                    if (playerIN.IsMasterClient)
                    {
                        _zoneComponentRef.Unref().XYMasterZone = InstanceGame.SupportGameManager.FinderWay.TryGetXYAround(xyCellIN);
                    }
                    else _zoneComponentRef.Unref().XYOtherZone = InstanceGame.SupportGameManager.FinderWay.TryGetXYAround(xyCellIN);



                    break;

                case BuildingTypes.Farm:

                    if (CellEnvironmentComponent(xyCellIN).HaveFood)
                    {
                        if (playerIN.IsMasterClient)
                        {
                            if (CanBuildFarm(playerIN, _economyMasterComponentRef))
                            {
                                CellBuildingComponent(xyCellIN).SetBuilding(buildingTypeIN, playerIN);
                                _economyBuildingsMasterComponentRef.Unref().AmountFarmMaster += 1; // !!!!!

                                CellUnitComponent(xyCellIN).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            if (CanBuildFarm(playerIN, _economyMasterComponentRef))
                            {
                                CellBuildingComponent(xyCellIN).SetBuilding(buildingTypeIN, playerIN);
                                _economyBuildingsMasterComponentRef.Unref().AmountFarmOther += 1; // !!!!!

                                CellUnitComponent(xyCellIN).AmountSteps = 0;
                            }
                        }
                    }
                    isBuilded = true;

                    break;

                case BuildingTypes.Woodcutter:

                    if (CellEnvironmentComponent(xyCellIN).HaveTree)
                    {
                        if (playerIN.IsMasterClient)
                        {
                            if (CanBuildWoodcutter(playerIN, _economyMasterComponentRef))
                            {
                                CellBuildingComponent(xyCellIN).SetBuilding(buildingTypeIN, playerIN);
                                _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterMaster += 1; // !!!!!

                                CellUnitComponent(xyCellIN).AmountSteps -= CellUnitComponent(xyCellIN).MaxAmountSteps;
                            }
                        }
                        else
                        {
                            if (CanBuildWoodcutter(playerIN, _economyMasterComponentRef))
                            {
                                CellBuildingComponent(xyCellIN).SetBuilding(buildingTypeIN, playerIN);
                                _economyBuildingsMasterComponentRef.Unref().AmountWoodcutterOther += 1; // !!!!!

                                CellUnitComponent(xyCellIN).AmountSteps -= CellUnitComponent(xyCellIN).MaxAmountSteps;
                            }
                        }

                    }
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

    private bool CanBuildFarm(Player player, EcsComponentRef<EconomyMasterComponent> economyMasterComponentRef)
    {
        if (player.IsMasterClient)
        {
            if (economyMasterComponentRef.Unref().GoldMaster >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM
                && economyMasterComponentRef.Unref().FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM
                && economyMasterComponentRef.Unref().WoodMaster >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM
                && economyMasterComponentRef.Unref().OreMaster >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM
                && economyMasterComponentRef.Unref().IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM)
            {
                economyMasterComponentRef.Unref().GoldMaster -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                economyMasterComponentRef.Unref().FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                economyMasterComponentRef.Unref().WoodMaster -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                economyMasterComponentRef.Unref().OreMaster -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                economyMasterComponentRef.Unref().IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;

                return true;
            }
            else return false;
        }
        else
        {
            if (economyMasterComponentRef.Unref().GoldOther >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM
                && economyMasterComponentRef.Unref().FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM
                && economyMasterComponentRef.Unref().WoodOther >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM
                && economyMasterComponentRef.Unref().OreOther >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM
                && economyMasterComponentRef.Unref().IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM)
            {
                economyMasterComponentRef.Unref().GoldOther -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                economyMasterComponentRef.Unref().FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                economyMasterComponentRef.Unref().WoodOther -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                economyMasterComponentRef.Unref().OreOther -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                economyMasterComponentRef.Unref().IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;

                return true;
            }
            else return false;
        }
    }

    private bool CanBuildWoodcutter(Player player, EcsComponentRef<EconomyMasterComponent> economyMasterComponentRef)
    {
        if (player.IsMasterClient)
        {
            if (economyMasterComponentRef.Unref().GoldMaster >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER
                && economyMasterComponentRef.Unref().FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER
                && economyMasterComponentRef.Unref().WoodMaster >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER
                && economyMasterComponentRef.Unref().OreMaster >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER
                && economyMasterComponentRef.Unref().IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER)
            {
                economyMasterComponentRef.Unref().GoldMaster -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                economyMasterComponentRef.Unref().FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                economyMasterComponentRef.Unref().WoodMaster -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                economyMasterComponentRef.Unref().OreMaster -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                economyMasterComponentRef.Unref().IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;

                return true;
            }
            else return false;
        }
        else
        {
            if (economyMasterComponentRef.Unref().GoldOther >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER
                && economyMasterComponentRef.Unref().FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER
                && economyMasterComponentRef.Unref().WoodOther >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER
                && economyMasterComponentRef.Unref().OreOther >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER
                && economyMasterComponentRef.Unref().IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER)
            {
                economyMasterComponentRef.Unref().GoldOther -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                economyMasterComponentRef.Unref().FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                economyMasterComponentRef.Unref().WoodOther -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                economyMasterComponentRef.Unref().OreOther -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                economyMasterComponentRef.Unref().IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;

                return true;
            }
            else return false;
        }
    }
}
