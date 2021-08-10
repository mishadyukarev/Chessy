using Leopotam.Ecs;

internal sealed class LeftBuildingUISystem : IEcsInitSystem, IEcsRunSystem
{
    //private EcsFilter<SelectorComponent> _selectorFilter = default;
    //private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;
    //private EcsFilter<BuildZoneViewUICom> _buildZoneUIFilter = default;
    //internal EcsComponentRef<SelectorComponent> SelComRef => _selectorFilter.Get1Ref(0);

    public void Init()
    {
        //_buildZoneUIFilter.Get1(0).AddListenerToCreateUnit(UnitTypes.Pawn, delegate { BuyUnit(UnitTypes.Pawn); });
        //_buildZoneUIFilter.Get1(0).AddListenerToCreateUnit(UnitTypes.Rook, delegate { BuyUnit(UnitTypes.Rook); });
        //_buildZoneUIFilter.Get1(0).AddListenerToCreateUnit(UnitTypes.Bishop, delegate { BuyUnit(UnitTypes.Bishop); });

        //_buildZoneUIFilter.Get1(0).AddListenerToMelt(delegate { MeltOre(); });
        //_buildZoneUIFilter.Get1(0).AddListenerToUpgradeUnits(ToggleSelectorUpgradeUnit);

        //_buildZoneUIFilter.Get1(0).AddListenerToBuildUpgrade(BuildingTypes.Farm, delegate { UpgradeBuilding(BuildingTypes.Farm); });
        //_buildZoneUIFilter.Get1(0).AddListenerToBuildUpgrade(BuildingTypes.Woodcutter, delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
        //_buildZoneUIFilter.Get1(0).AddListenerToBuildUpgrade(BuildingTypes.Mine, delegate { UpgradeBuilding(BuildingTypes.Mine); });
    }



    public void Run()
    {
        //ref var selCom = ref _selectorFilter.Get1(0);

        //if (selCom.IsSelectedCell && CellBuildDataSystem.BuildTypeCom(selCom.XySelectedCell).Is(BuildingTypes.City))
        //{
        //    if (CellBuildDataSystem.OwnerCom(selCom.XySelectedCell).HaveOwner)
        //    {
        //        if (CellBuildDataSystem.OwnerCom(selCom.XySelectedCell).IsMine)
        //        {
        //            _buildZoneUIFilter.Get1(0).SetActiveZone(true);
        //        }
        //        else _buildZoneUIFilter.Get1(0).SetActiveZone(false);
        //    }
        //}
        //else
        //{
        //    _buildZoneUIFilter.Get1(0).SetActiveZone(false);
        //}
    }


    //private void BuyUnit(UnitTypes unitType)
    //{
    //    if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.CreateUnitToMaster(unitType);
    //}
    //private void ToggleSelectorUpgradeUnit()
    //{
    //    if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient))
    //    {
    //        if (SelComRef.Unref().SelectorType == SelectorTypes.UpgradeUnit)
    //        {
    //            SelComRef.Unref().SelectorType = SelectorTypes.StartClick;
    //        }
    //        else
    //        {
    //            SelComRef.Unref().SelectorType = SelectorTypes.UpgradeUnit;
    //        }
    //    }
    //}

    //private void UpgradeBuilding(BuildingTypes buildingType)
    //{
    //    if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.UpgradeBuildingToMaster(buildingType);
    //}

    //private void MeltOre()
    //{
    //    if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.MeltOreToMaster();
    //}
}
