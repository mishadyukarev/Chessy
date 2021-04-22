using Leopotam.Ecs;
using Photon.Realtime;

internal class BuilderCellMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<BuilderCellMasterComponent> _builderCellMasterComponentRef = default;

    private EcsComponentRef<EconomyMasterComponent.UnitMasterComponent> _economyUnitsMasterComponentRef;
    private EcsComponentRef<EconomyMasterComponent.BuildingsMasterComponent> _economyBuildingsMasterComponentRef;


    internal BuilderCellMasterSystem(ECSmanager eCSmanager, SupportGameManager supportManager) : base(eCSmanager, supportManager)
    {
        _builderCellMasterComponentRef = eCSmanager.EntitiesMasterManager.BuilderCellMasterComponentRef;

        _economyUnitsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyUnitsMasterComponentRef;
        _economyBuildingsMasterComponentRef = eCSmanager.EntitiesMasterManager.EconomyBuildingsMasterComponentRef;

        _startValues = supportManager.StartValuesConfig;
    }


    public void Run()
    {
        _builderCellMasterComponentRef.Unref().Unpack(out int[] xyCellIN, out BuildingTypes buildingTypeIN, out Player playerIN);

        bool isBuilded;

        if (!CellEnvironmentComponent(xyCellIN).HaveMountain && CellUnitComponent(xyCellIN).HaveAmountSteps)
        {
            CellBuildingComponent(xyCellIN).SetBuilding(buildingTypeIN);
            CellUnitComponent(xyCellIN).AmountSteps -= _startValues.AMOUNT_FOR_TAKE_UNIT;

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
