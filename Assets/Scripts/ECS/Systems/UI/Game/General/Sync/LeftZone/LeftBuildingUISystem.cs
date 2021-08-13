using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class LeftBuildingUISystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;
    private EcsFilter<BuildLeftZoneViewUICom> _buildZoneUIFilter = default;
    private EcsFilter<TakerUnitsDataUICom, TakerUnitsViewUICom> _takerUIFilter = default;

    internal EcsComponentRef<SelectorComponent> SelComRef => _selectorFilter.Get1Ref(0);

    private EcsFilter<CellBuildDataComponent, OwnerComponent> _cellBuildFilter = default;

    public void Init()
    {
        _buildZoneUIFilter.Get1(0).AddListenerToMelt(delegate { MeltOre(); });
        //_buildZoneUIFilter.Get1(0).AddListenerToUpgradeUnits(ToggleSelectorUpgradeUnit);

        //_buildZoneUIFilter.Get1(0).AddListenerToBuildUpgrade(BuildingTypes.Farm, delegate { UpgradeBuilding(BuildingTypes.Farm); });
        //_buildZoneUIFilter.Get1(0).AddListenerToBuildUpgrade(BuildingTypes.Woodcutter, delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
        //_buildZoneUIFilter.Get1(0).AddListenerToBuildUpgrade(BuildingTypes.Mine, delegate { UpgradeBuilding(BuildingTypes.Mine); });
    }



    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);

        ref var selCellUnitDataCom = ref _cellBuildFilter.Get1(selCom.IdxSelectedCell);
        ref var selOwnerCellUnitCom = ref _cellBuildFilter.Get2(selCom.IdxSelectedCell);

        if (selCom.IsSelectedCell && selCellUnitDataCom.IsBuildType(BuildingTypes.City))
        {
            if (selOwnerCellUnitCom.HaveOwner)
            {
                if (selOwnerCellUnitCom.IsMine)
                {
                    _buildZoneUIFilter.Get1(0).SetActiveZone(true);
                }
                else _buildZoneUIFilter.Get1(0).SetActiveZone(false);
            }
        }
        else
        {
            _buildZoneUIFilter.Get1(0).SetActiveZone(false);
        }
    }



    private void ToggleSelectorUpgradeUnit()
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient))
        {
            if (SelComRef.Unref().CellClickType == CellClickTypes.UpgradeUnit)
            {
                SelComRef.Unref().CellClickType = CellClickTypes.Start;
            }
            else
            {
                SelComRef.Unref().CellClickType = CellClickTypes.UpgradeUnit;
            }
        }
    }

    private void UpgradeBuilding(BuildingTypes buildingType)
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.UpgradeBuildingToMaster(buildingType);
    }

    private void MeltOre()
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.MeltOreToMaster();
    }
}
